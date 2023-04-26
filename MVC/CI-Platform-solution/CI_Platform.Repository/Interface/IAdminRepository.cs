using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IAdminRepository
    {
        public AdminViewModel displayModel();
        public List<Skill> searchSkill(String? obj);
        public List<MissionTheme> searchTheme(String? obj);
        public List<User> searchUser(String? obj);

        public List<CmsPage> searchCms(String? obj);

        public List<Mission> searchMission(String? obj);

        public List<MissionApplication> searchMissionApplication(String? obj);

        public List<Story> searchStory(String? obj);

        public AdminViewModel EditForm(int id,int page);

        public bool ApplicationApproval(int id, int status);

        public bool StoryApproval(int id, int status);
        public bool AddCms(AdminViewModel obj);
        public bool DeleteActivity(int id, int page);
        public bool AddSkill(AdminViewModel obj);
        public bool AddTheme(AdminViewModel obj);
        public bool AddUser(AdminViewModel obj);
        public bool AddMission(AdminViewModel obj);
        public List<Banner> searchBanner(String? obj);
        public bool AddBanner(AdminViewModel obj);
    }
}
