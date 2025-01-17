﻿using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using MailKit.Security;

using Microsoft.AspNetCore.Http;

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
        public StoryModel stories(int PageIndex)
        {
            int PageSize = 3;
            List<Story> stories = _db.Stories
                .Include(m => m.StoryMedia)
                .Include(m => m.Mission.Theme)
                .Include(m => m.User)
                .Where(m => m.DeletedAt == null && m.Status == "PUBLISHED")
               .ToList();

            List<Story> records = stories.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            int TotalRecords = (int)Math.Ceiling(stories.Count() / (double)PageSize);
            StoryModel storyModel = new StoryModel();
            {
                storyModel.stories = records;
                storyModel.totalcount = TotalRecords;
            }
            return storyModel;
        }
        public StoryModel storyDetails(int sid, int uid)
        {
            List<Story> stories = _db.Stories
                .Include(m => m.StoryMedia)
                .Include(m => m.Mission.Theme)
                .Include(m => m.User)
                .Where(m => m.DeletedAt == null && m.Status == "PUBLISHED")
                .ToList();
            Story story = stories.FirstOrDefault(x => x.StoryId == sid);
            List<StoryMedium> photos = _db.StoryMedia.Where(x => x.StoryId == sid && x.DeletedAt == null).ToList();
            List<User> coWorkers = _db.Users.Where(x => x.UserId != uid && x.DeletedAt == null).ToList();
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
            List<Story> missioncards = _db.Stories
                .Include(m => m.StoryMedia)
                .Include(m => m.Mission)
                .Include(m => m.Mission.Theme)
                .Include(m => m.User)
                .Where(m => m.DeletedAt == null && m.Status == "PUBLISHED").ToList();
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
            if (search == null)
            {
                cards = missioncards;
            }
            return cards;
        }

        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int sid)
        {
            var fromUser = _db.Users.FirstOrDefault(u => u.UserId == FromUserId && u.DeletedAt == null);
            var fromEmailId = fromUser.Email;
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
                smtp.Authenticate("payushi.tatva@gmail.com", "leipwwhwrkqbemqk");
                //smtp.Authenticate("payushi.tatva@gmail.com", "jatwiurqnjgeceeg");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail
            }
        }
        public List<MissionApplication> missionsSStory(int ud)
        {
            List<MissionApplication> missions = _db.MissionApplications.Include(x => x.Mission).Where(x => x.UserId == ud).ToList();
            return missions;
        }
        public bool saveStory(ShareStory obj, int status, int uid)
        {
            Story story = _db.Stories.FirstOrDefault(x => x.UserId == uid && x.MissionId == obj.MissionId && x.Status == "DRAFT") ;
            if (story == null)
            {
                if (_db.Stories.Any(x => x.UserId == uid && x.MissionId == obj.MissionId && x.Title == obj.Stitle))
                {
                    return false;
                }
                Story str = new Story();
                {
                    str.Title = obj.Stitle;
                    str.Description = obj.Sdescription;
                    str.UserId = uid;
                    str.MissionId = obj.MissionId;
                    str.PublishedAt = obj.PublishedAt;
                }
                if (status == 1)
                {
                    str.Status = "DRAFT";
                }
                if (status == 2)
                {
                    str.Status = "PENDING";
                }

                _db.Stories.Add(str);
                _db.SaveChanges();
            }
            if (story != null)
            {
                {
                    story.Title = obj.Stitle;
                    story.Description = obj.Sdescription;
                    story.UserId = uid;
                    story.MissionId = obj.MissionId;
                    story.PublishedAt = obj.PublishedAt;
                }
                if (status == 1)
                {
                    story.Status = "DRAFT";
                }
                if (status == 2)
                {
                    story.Status = "PENDING";
                }

                _db.Stories.Update(story);
                _db.SaveChanges();
            }
            return true;
        }
        public async Task<bool> saveImage(ShareStory obj, int uid)
        {
            int sid = (int)_db.Stories.FirstOrDefault(x => x.UserId == uid && x.MissionId == obj.MissionId && x.Title == obj.Stitle).StoryId;

            if (obj.url != null)
            {
                StoryMedium checkUrl = _db.StoryMedia.Where(media => media.StoryId == sid && media.Type == "video").FirstOrDefault();


                if (checkUrl != null)
                {
                    checkUrl.Path = obj.url;
                    checkUrl.UpdatedAt = DateTime.Now;
                    _db.StoryMedia.Update(checkUrl);
                    _db.SaveChanges();
                }
                else
                {
                    StoryMedium forUrl = new StoryMedium();
                    {
                        forUrl.StoryId = sid;
                        forUrl.Type = "video";
                        forUrl.Path = obj.url;
                    }
                    _db.StoryMedia.Add(forUrl);
                    _db.SaveChanges();
                }
            }

            if (obj.file != null)
            {
                List<StoryMedium> check = _db.StoryMedia.Where(media => media.StoryId == sid && media.Type == "png").ToList();
                _db.RemoveRange(check);
                var filePaths = new List<string>();
                foreach (var formFile in obj.file)
                {
                    StoryMedium mediaobj = new StoryMedium();
                  
                    mediaobj.StoryId = sid;
                    mediaobj.Path = formFile.FileName;
                    mediaobj.Type = "png";
                    //subview.StorySend.StoryMedia.Add(mediaobj);

                    _db.StoryMedia.Add(mediaobj);
                    _db.SaveChanges();

                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", formFile.FileName); //we are using Temp file name just for the example. Add your own file path.

                        if (File.Exists(filePath) == false)
                        {

                            filePaths.Add(filePath);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                formFile.CopyToAsync(stream);
                            }
                        }
                    }


                }
            }
            return true ;
        }

        public ShareStory getData(int mid, int uid)
        {
            ShareStory obj = new ShareStory();
            Story story = _db.Stories.FirstOrDefault(m => m.MissionId == mid && m.UserId == uid && m.Status == "DRAFT");

            if (story != null)
            {
                List<StoryMedium> images = _db.StoryMedia.Where(media => media.StoryId == story.StoryId && media.Type == "png").ToList();

                List<string> displayImage = new List<string>();
                foreach (var image in images)
                {
                    displayImage.Add(image.Path);
                    story.StoryMedia.Remove(image);
                }

                StoryMedium? url = _db.StoryMedia.FirstOrDefault(media => media.StoryId == story.StoryId && media.Type == "video");
                
                {
                    obj.Stitle = story.Title;
                    obj.Sdescription = story.Description;
                    obj.PublishedAt = story.PublishedAt;
                    //obj.file = storiesMedium;
                    obj.story = story;
                    obj.displayImages = displayImage;
                    if(url != null)
                        obj.url = url.Path;
                }
                story.StoryMedia.Remove(url);

                return obj;
            }

            return null;
        }

    }

}




