using CI_Platform.Entities.Models;
using Microsoft.AspNetCore.Http;
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
        public List<Skill> Dmissionskills = new List<Skill>();
        public IFormFile? defaultImg { get; set; }
        public List<IFormFile>? missionMedia { get; set; }
        public List<IFormFile>? missiondoc { get; set; }
        public List<long> missionSkills { get; set; } =new List<long>();
        public MissionTheme missionTheme { get; set; } = new MissionTheme();

        public User adminUser { get; set; } = new User();
        public IFormFile? Avatarfile { get; set; }
        public List<City> Citys = new List<City>();
        public List<Country> countries = new List<Country>();
        public Skill skill { get; set; } = new Skill();

    }
}
