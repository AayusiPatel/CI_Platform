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
    public class ProfileRepository : IProfileRepository
    {
        public readonly CiPlatformContext _db;


        public ProfileRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public ProfileViewModel getUser(int uid)
        {
            User user = _db.Users.Include(user => user.UserSkills).FirstOrDefault(user => user.UserId == uid);
            List<UserSkill> userSkills = _db.UserSkills.Include(uSkill => uSkill.Skill).Where(user => user.UserId == uid).ToList();

            List<Skill> skills = _db.Skills.ToList();
            foreach (var userSkill in userSkills)
            {
                skills = skills.Where(skill => skill.SkillId != userSkill.SkillId).ToList();
            }

            ProfileViewModel profile = new ProfileViewModel();
            {
                profile.FirstName = user.FirstName;
                profile.LastName = user.LastName;
                profile.Avatar = user.Avatar;
                profile.EmployeeId = user.EmployeeId;
                profile.Title = user.Title;
                profile.Department = user.Department;
                profile.ProfileText = user.ProfileText;
                profile.WhyIVolunteer = user.WhyIVolunteer;
                profile.CountryId = user.CountryId;
                profile.CityId = user.CityId;
                profile.LinkedInUrl = user.LinkedInUrl;
                profile.skill = skills;
                profile.userSkills = userSkills;

            }

            return profile;
        }

        public bool updateUser(ProfileViewModel user, int uid)
        {
            User profile = _db.Users.Include(user => user.UserSkills).FirstOrDefault(user => user.UserId == uid);

           
          

            {
                profile.FirstName = user.FirstName;
                profile.LastName = user.LastName;
                if (user.Avatarfile != null)
                    profile.Avatar = user.Avatarfile.FileName;
                profile.EmployeeId = user.EmployeeId;
                profile.Title = user.Title;
                profile.Department = user.Department;
                profile.ProfileText = user.ProfileText;
                profile.WhyIVolunteer = user.WhyIVolunteer;
                profile.CountryId = user.CountryId;
                profile.CityId = user.CityId;
                profile.LinkedInUrl = user.LinkedInUrl;

            }
            _db.Users.Update(profile);
            _db.SaveChanges();
            {
                List<UserSkill> skills = _db.UserSkills.Where(skill => skill.UserId == uid).ToList();
                _db.UserSkills.RemoveRange(skills);

                foreach (var skill in user.skillsToAdd)
                {
                    UserSkill addSkill = new UserSkill();
                    {
                        addSkill.UserId = uid;
                        addSkill.SkillId = skill;
                    }
                    _db.UserSkills.Add(addSkill);
                    _db.SaveChanges();
                }


                if (user.Password != null && user.OldPassword != profile.Password)
                {
                    return false;
                }

                if (user.Password != null && user.OldPassword == profile.Password)
                {
                   

                    profile.Password = user.Password;

                    _db.Users.Update(profile);
                    _db.SaveChanges();

                    PasswordReset pr = new PasswordReset();
                    {
                        pr.Email = profile.Email;
                        pr.Token = "UPDATEDFROMPROFILE";
                    }
                    _db.PasswordResets.Add(pr);
                    _db.SaveChanges();

                    return true;
                }




                return true;
            }

        }

        public bool ContactUs(ContactUsViewModel obj)
        {

            #region Send Mail
            var mailBody = "<h2>I hope this email finds you well. My name is " + obj.Name + " and I wanted to take a moment to you.</h1>" + "<h1>" + obj.Message + "</h1><br><h2>" + "</h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(obj.Email));
            email.To.Add(MailboxAddress.Parse("aayushippatel105@gmail.com"));
            email.Subject = obj.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("hetshah2207@gmail.com", "lpoqtojvkcgkwdms");
            smtp.Send(email)
;
            smtp.Disconnect(true);
            #endregion Send Mail

            return true;
        }


        //public bool PassRset(ProfilePassReset obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }

        //}

        public TimeSheetViewModel GetActivities(int uid)
        {
            TimeSheetViewModel tm = new TimeSheetViewModel();

            tm.timecards = _db.Timesheets.Include(m=>m.Mission).Where(t=>t.UserId == uid && t.Mission.MissionType == "Time").ToList();
            tm.goalcards = _db.Timesheets.Include(m => m.Mission).Where(t => t.UserId == uid && t.Mission.MissionType == "Goal").ToList();
            tm.timeMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Time" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();
            tm.goalMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Goal" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();
            return tm;
        }

        //public TimeSheetViewModel GetActivitiesMis(int uid)
        //{
        //    TimeSheetViewModel tm = new TimeSheetViewModel();

        //    tm.timeMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Time" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();
        //    tm.goalMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Goal" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();
        //    return tm;
        //}
        public TimeSheetViewModel GetActivity(int obj, int uid)
        {
            Timesheet timesheet = _db.Timesheets.FirstOrDefault(entry => entry.TimesheetId == obj);
           

            TimeSheetViewModel tVModel = new TimeSheetViewModel();
            {
                
                tVModel.Time = timesheet.Time;
                tVModel.DateVolunteereed = timesheet.DateVolunteereed;
                tVModel.Notes = timesheet.Notes;
                tVModel.MissionId = timesheet.MissionId;
                //tVModel.TimesheetId = timesheet.TimesheetId;
                //tVModel.Action = timesheet.Action;

                tVModel.timeMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Time" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();
                tVModel.goalMissions = _db.Missions.Include(m => m.MissionApplications).Where(t => t.MissionType == "Goal" && t.MissionApplications.Any(t => t.UserId == uid)).ToList();

            }

            return tVModel;
        }

        public bool AddActivity(TimeSheetViewModel obj,int uid)
        {
            Timesheet ts = new Timesheet();
            {
                ts.UserId = uid;
                ts.MissionId = obj.MissionId;
                ts.Time = obj.Time;
                ts.Action = obj.Action;
                ts.DateVolunteereed = obj.DateVolunteereed;
                ts.Notes = obj.Notes;

            }

            _db.Timesheets.Add(ts);
            _db.SaveChanges();

            return true;

        }

        public bool UpdateActivity(TimeSheetViewModel obj)
        {
            Timesheet ts = _db.Timesheets.FirstOrDefault(t=>t.TimesheetId == obj.TimesheetId);
            {
              
                ts.MissionId = obj.MissionId;
                ts.Time = obj.Time;
                ts.Action = obj.Action;
                ts.DateVolunteereed = obj.DateVolunteereed;
                ts.Notes = obj.Notes;

            }

            _db.Timesheets.Update(ts);
            _db.SaveChanges();

            return true;

        }
    }
}
