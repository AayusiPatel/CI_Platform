using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class AdminViewModel
    {
        public List<User> users = new List<User>();
        public List<Mission> missions = new List<Mission>();
        public List<CmsPage> cms = new List<CmsPage>();
        public List<MissionApplication> missionApplications = new List<MissionApplication>();
        public List<Story> stories = new List<Story>();



        public CmsPage cmsPage = new CmsPage();
        public Mission mission = new Mission();

    }
}
