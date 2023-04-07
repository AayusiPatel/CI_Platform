using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                profile.Avatar= user.Avatar;
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

                return true;
            }
        }
    }
}
