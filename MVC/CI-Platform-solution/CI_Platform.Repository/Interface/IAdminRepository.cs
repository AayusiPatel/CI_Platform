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

        public List<User> searchUser(String? obj);

        public List<CmsPage> searchCms(String? obj);

        public List<Mission> searchMission(String? obj);

        public List<MissionApplication> searchMissionApplication(String? obj);

        public List<Story> searchStory(String? obj);

        public AdminViewModel EditForm(int id,int page);

    }
}
