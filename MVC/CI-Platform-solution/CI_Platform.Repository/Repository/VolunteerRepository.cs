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
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class VolunteerRepository : IVolunteerRepository
    {
        public readonly CiPlatformContext _db;
        public readonly IPlatformRepository _platform;


        public VolunteerRepository(CiPlatformContext db, IPlatformRepository platform)
        {
            _db = db;
            _platform = platform;

        }


        public List<MissionMedium> media(int mid)
        {
            List<MissionMedium> photos = _db.MissionMedia.Where(x=>x.MissionId==mid).ToList();
            return photos;
        }

        public List<MissionDocument> document(int mid)
        {
            List<MissionDocument> documents = _db.MissionDocuments.Where(x => x.MissionId == mid).ToList();
            return documents;
        }

    //    public List<MissionApplication> volunteers(int mid)
    //    {

    //        List<MissionApplication> missionApplications = _db.MissionApplications.ToList();
    //        List<User> users = _db.Users.ToList();
           

    //        List<MissionApplication> application_user = 
    //            ( from e in missionApplications
    //              join f in users on e.UserId equals f.UserId into tbl1
             
    //              where e.MissionId == mid
    //              select  e ).ToList();

            
        
    //        return application_user;
    //}


        public int avgRating(int mid)
        {
            List<MissionRating> ratings = _db.MissionRatings.Where(x => x.MissionId == mid).ToList();
            int abc = (int)ratings.Average(x => x.Rating);   
            return abc;
        }


        //public bool addComment(VolunteerModel obj, int uid)
        //{
        //    long mid = obj.mission.MissionId;
        //    string commentDescription = obj.commentDescription;


        //    Comment comment = new Comment();
        //    {
        //        comment.MissionId = mid;
        //        comment.UserId = uid;
        //        comment.CommentDescription = commentDescription;
        //    }
        //    _db.Comments.Add(comment);
        //    _db.SaveChanges();

        //    return true;

        //}

        public bool addComment(int mid, int uid, string comnt)
        {



            Comment comment = new Comment();
            {
                comment.MissionId = mid;
                comment.UserId = uid;
                comment.CommentDescription = comnt;
            }
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return true;

        }

        public bool applyMission(int mid, int uid)
        {
            MissionApplication application = new();
            application.MissionId = mid;
            application.UserId = uid;



            var applicable = _db.MissionApplications.FirstOrDefault(a => a.MissionId == mid && a.UserId == uid);

            if (applicable == null)
            {
                application.CreatedAt = DateTime.Now;
                application.AppliedAt = DateTime.Now;

                _db.MissionApplications.Add(application)
;

                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid)
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

                MissionInvite invite = new MissionInvite();
                {
                    invite.FromUserId = FromUserId;
                    invite.ToUserId = user;
                    invite.MissionId = mid;
                }
                _db.Add(invite);
                _db.SaveChanges();

               

                #region Send Mail
                var mailBody = "<h1></h1><br><h2><a href='" + "https://localhost:7228/Platform/Volunteering_Mission?mid=" + mid + "'>Check Out this Mission!</a></h2>";

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromEmailId));
                email.To.Add(MailboxAddress.Parse(toEmailId));
                email.Subject = "Reset Your Password";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("payushi.tatva@gmail.com", "dvmiwwylvnkgdinq");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail
            }
        }

        //Star Rating
        public bool MissionRating(int userId, int mid, int rating)
        {
            MissionRating ratingCheck = _db.MissionRatings.FirstOrDefault(a => a.UserId == userId && a.MissionId == mid);
            
            if (ratingCheck != null)
            {
                //MissionRating missionRating = new MissionRating();
                //ratingCheck.UserId = userId;
                //ratingCheck.MissionId = mid;
                ratingCheck.Rating = rating;
                ratingCheck.UpdatedAt = DateTime.Now;

                _db.MissionRatings.Update(ratingCheck);
                _db.SaveChanges();

                return false;
            }
            else
            {
                MissionRating missionRating = new MissionRating();
                missionRating.UserId = userId;
                missionRating.MissionId = mid;
                missionRating.Rating = rating;



                _db.MissionRatings.Add(missionRating);
                _db.SaveChanges();
                return true;
            }
        }



        public VolunteerModel DisplayModel(int mid,int pageIndex)
        {
            List<Mission> missions = _platform.GetMissionDetails();
            Mission mission = missions.FirstOrDefault(x => x.MissionId == mid);
            List<MissionMedium> Photos = media(mid);
            List<MissionDocument> documents = document(mid);
            List<Mission> relatedMissions = missions.Where(  x => x.OrganizationName == mission.OrganizationName || x.ThemeId == mission.ThemeId || x.CountryId == mission.CountryId  ).ToList();
            relatedMissions.Remove(mission);
            int rating = avgRating(mid);
            List<MissionSkill> missionSkills = _db.MissionSkills.Include(m => m.Skill).Where(x => x.MissionId == mid).ToList();
            //List <MissionApplication> apps = volunteers(mid);
            List<MissionApplication> apps = _db.MissionApplications.Include(m => m.User).Where(x => x.MissionId == mid).ToList();

            List<Comment> comments = _db.Comments.Include(m => m.User).Where(x => x.MissionId == mid).OrderByDescending(x => x.CreatedAt).ToList();
            int pageSize = 2;
            List<MissionApplication> recentVolunteres = _db.MissionApplications.Include(m => m.User).Where(x => x.MissionId == mid).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            List<User> users = _db.Users.ToList();
            List<FavoriteMission> favoriteMissions = _db.FavoriteMissions.ToList();

            //int pagesize = 3;
            //data.Users = data.Users.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();


            VolunteerModel volunteerModel = new VolunteerModel();
            {
                volunteerModel.mission = mission;
                volunteerModel.missionMedias = Photos;
                volunteerModel.missionDocuments = documents;
                volunteerModel.relatedMissions = relatedMissions;
                volunteerModel.rating = rating;
                volunteerModel.missionSkills = missionSkills;
                volunteerModel.missionApplicaton = apps;
                 volunteerModel.comments = comments;
                volunteerModel.CoWorkers = users;
                volunteerModel.favoriteMissions = favoriteMissions;
                volunteerModel.volunteres = recentVolunteres;
            }
            return volunteerModel;
        }


    }
}
