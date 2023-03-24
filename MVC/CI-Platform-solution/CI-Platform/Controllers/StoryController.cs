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
        public IActionResult StoryDetail()
        {
            return View();
        }
        public IActionResult StoryApply()
        {
            return View();
        }
    }
}
