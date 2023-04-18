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
    public class AdminRepository : IAdminRepository
    {
        public readonly CiPlatformContext _db;



        public AdminRepository(CiPlatformContext db)
        {
            _db = db;

        }

        public AdminViewModel displayModel()
        {
            AdminViewModel adminModel = new AdminViewModel();

            {
                adminModel.users = _db.Users.ToList();
                adminModel.missions = _db.Missions.ToList();
                adminModel.cms = _db.CmsPages.ToList();
                adminModel.missionApplications = _db.MissionApplications.ToList();  
                adminModel.stories = _db.Stories.ToList();
            }

            return adminModel;
        }

        public List<User> searchUser(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<User> users = _db.Users.Where(m => m.FirstName.ToLower().Contains(obj) || m.LastName.ToLower().Contains(obj)).ToList();

                return users;
            }
            return _db.Users.ToList();
        }

      

        public List<CmsPage> searchCms(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<CmsPage> cms = _db.CmsPages.Where(m => m.Title.ToLower().Contains(obj)).ToList();

                return cms;
            }

            return _db.CmsPages.ToList();
        }

        public List<Mission> searchMission(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<Mission> missions = _db.Missions.Where(m => m.Title.ToLower().Contains(obj)).ToList();

                return missions;
            }

            return  _db.Missions.ToList();
        }

        public List<MissionApplication> searchMissionApplication(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<MissionApplication> missionApplication = _db.MissionApplications.Include(m => m.Mission).Include(m => m.User)
                    .Where(m => m.Mission.Title.ToLower().Contains(obj) || m.User.LastName.ToLower().Contains(obj) || m.User.FirstName.ToLower().Contains(obj))
                    .ToList();

                return missionApplication;
            }

            return _db.MissionApplications.Include(m => m.Mission).Include(m => m.User).ToList();
        }
        public List<Story> searchStory(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<Story> story = _db.Stories.Include(m => m.Mission).Include(m => m.User)
                    .Where(m => m.Title.ToLower().Contains(obj) || m.Mission.Title.ToLower().Contains(obj) || m.User.LastName.ToLower().Contains(obj) || m.User.FirstName.ToLower().Contains(obj))
                    .ToList();

                return story;
            }

            return _db.Stories.Include(m => m.Mission).Include(m => m.User).ToList();
        }

        public AdminViewModel EditForm(int id,int page)
        {
            AdminViewModel am = new AdminViewModel();
            if (page == 2)
            {
                
                {
                    am.cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == id);
                }
            }
            if (page == 3)
            {

                {
                    am.mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);
                }
            }
            return am;
        }
    }
}
