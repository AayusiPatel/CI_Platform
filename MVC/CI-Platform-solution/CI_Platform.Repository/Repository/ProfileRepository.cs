using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
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
            User user = _db.Users.FirstOrDefault(user => user.UserId == uid);
            
            ProfileViewModel profile = new ProfileViewModel();
            {
                profile.FirstName = user.FirstName;
                profile.LastName = user.LastName;
                profile.Email = user.Email;
                profile.EmployeeId = user.EmployeeId;
                profile.Title = user.Title;
                profile.Department = user.Department;
                profile.ProfileText = user.ProfileText;
                profile.WhyIVolunteer = user.WhyIVolunteer;
                profile.CountryId = user.CountryId;
                profile.CityId = user.CityId;
                profile.LinkedInUrl = user.LinkedInUrl;
            }

            return profile;
        }
    }
}
