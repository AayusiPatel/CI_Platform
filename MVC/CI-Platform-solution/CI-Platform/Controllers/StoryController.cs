using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class StoryController : Controller
    {

        public readonly IPlatformRepository _platform;
    
        public readonly IStoryRepository _story;


        public StoryController(IPlatformRepository platform,  IStoryRepository story)
        {

            //_userRepository = userRepository;
            _platform = platform;
           
            _story = story;

        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult StoryListing()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;

            StoryModel model = _story.stories();
            return View(model);
        }
        public IActionResult StoryDetail(int sid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;

            //if (name != null)
            //{
            //    int UserId = (int)HttpContext.Session.GetInt32("UId");

            //}

            int UserId = (int)HttpContext.Session.GetInt32("UId");



            //if (string.IsNullOrEmpty(Convert.ToString(UserId)))
            //{
            //    {
            //        Response.Redirect(Page.ResolveUrl("~/default.aspx"));
            //    }
            //}



            //StoryModel model = _story.storyDetails(sid, 19);
            StoryModel model = _story.storyDetails(sid, UserId);

            return View(model);
        }

        public void RecommandToCoWorker(List<int> toUserId, int sid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

          _story.RecommandToCoWorker(FromUserId, toUserId, sid);



        }


        public IActionResult StoryApply()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                List<MissionApplication> misShareStory = _story.missionsSStory(UserId);

                ShareStory ss = new ShareStory();
                {
                    ss.missions = misShareStory;
                }
                return View(ss);
            }


            return View();
        }

        [HttpPost]
        public IActionResult StoryApply(ShareStory obj , int command)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool abc = _story.saveStory(obj,command,UserId);
            if (command == 1)
            {
                if (abc == false)
                {
                    return RedirectToAction("StoryListing", "Story");
                }

                List<MissionApplication> misShareStory = _story.missionsSStory(UserId);

                obj.missions = misShareStory;
                return View(obj);
            }
            if(command == 2)
            {
                return RedirectToAction("StoryListing", "Story");
            }
            return View();
        }
    }
}
