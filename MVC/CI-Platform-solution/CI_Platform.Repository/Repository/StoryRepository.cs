using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CI_Platform.Repository.Repository
{
    public class StoryRepository : IStoryRepository
    {
        public readonly CiPlatformContext _db;
        public StoryRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public StoryModel stories()
        {
            List<Story> stories = _db.Stories.Include(m => m.StoryMedia).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            //List<StoryMedium> storyMedia = _db.StoryMedia.ToList();
            StoryModel storyModel = new StoryModel();
            {
                storyModel.stories = stories;

            }
            return storyModel;
        }

        public StoryModel storyDetails(int sid, int uid)
        {
            List<Story> stories = _db.Stories.Include(m => m.StoryMedia).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            Story story = stories.FirstOrDefault(x=>x.StoryId == sid);
            List<StoryMedium> photos = _db.StoryMedia.Where(x=>x.StoryId == sid).ToList();
            List<User> coWorkers = _db.Users.Where(x => x.UserId != uid).ToList();
            //List<StoryMedium> storyMedia = _db.StoryMedia.ToList();
            StoryModel storyModel = new StoryModel();
            {
                storyModel.Story = story;
                storyModel.storiesMedium = photos;
                storyModel.CoWorkers = coWorkers;
            }
            return storyModel;
        }


        public List<Story> StoryFilter(string? search)
        {
            List<Story> cards = new List<Story>();
            var missioncards = _db.Stories.Include(m => m.StoryMedia).Include(m => m.Mission).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            var Missionskills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<int> temp = new List<int>();



            if (search != null)
            {
              


                 foreach (var n in missioncards)
                    {

                        var title = n.Title.ToLower();
                        if (title.Contains(search.ToLower()))
                        {
                            cards.Add(n);
                        }


                    }
                

            }
            if(search == null)
            {
                cards = missioncards;
            }
            return cards;


        }


        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int sid)
        {
            var fromUser = _db.Users.FirstOrDefault(u => u.UserId == FromUserId && u.DeletedAt == null);
            var fromEmailId = fromUser.Email;
            //if (user1 == null)
            //{
            //    return null;
            //}

            foreach (var user in ToUserId)
            {
                var toUser = _db.Users.FirstOrDefault(u => u.UserId == user && u.DeletedAt == null);
                var toEmailId = toUser.Email;

                StoryInvite invite = new StoryInvite();
                {
                    invite.FromUserId = FromUserId;
                    invite.ToUserId = user;
                    invite.StoryId = sid;
                }
                _db.Add(invite);
                _db.SaveChanges();



                #region Send Mail
                var mailBody = "<h1></h1><br><h2><a href='" + "https://localhost:7228/Story/StoryDetail?sid=" + sid + "'>Check Out this Mission!</a></h2>";

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromEmailId));
                email.To.Add(MailboxAddress.Parse(toEmailId));
                email.Subject = "Check this Story";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("hetshah2207@gmail.com", "lpoqtojvkcgkwdms");
                //smtp.Authenticate("payushi.tatva@gmail.com", "jatwiurqnjgeceeg");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail
            }
        }

    }

}




