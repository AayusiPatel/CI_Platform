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
        public int userPages = 1;

        public List<Mission> missions = new List<Mission>();
        public int missinPages = 1;

        public List<CmsPage> cms = new List<CmsPage>();
        public int cmsPages = 1;

        public List<MissionApplication> missionApplications = new List<MissionApplication>();
        public int mApplicationPages = 1;

        public List<Story> stories = new List<Story>();
        public int storyPages = 1;

        public List<Skill> skills = new List<Skill>();
        public int skillPages = 1;

        public List<MissionTheme> themes = new List<MissionTheme>();
        public int ThemePages = 1;
       




        public CmsPage cmsPage { get; set; } = new CmsPage();
        public Mission mission  { get; set; } = new Mission();
        public MissionTheme missionTheme { get; set; } = new MissionTheme();

        public Skill skill { get; set; } = new Skill();

    }
}
