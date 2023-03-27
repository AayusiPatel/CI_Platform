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
            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
            }
            //if (string.IsNullOrEmpty(Convert.ToString(UserId)))
            //{
            //    {
            //        Response.Redirect(Page.ResolveUrl("~/default.aspx"));
            //    }
            //}
            StoryModel model = _story.storyDetails(sid, 19);
            //StoryModel model = _story.storyDetails(sid , UserId);

            return View(model);
        }

        public void RecommandToCoWorker(List<int> toUserId, int sid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

          _story.RecommandToCoWorker(FromUserId, toUserId, sid);



        }


        public IActionResult StoryApply()
        {
            return View();
        }
    }
}
