

using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CI_Platform.Controllers
{
    public class CommonController : Controller
    {

        public readonly IUserRepository _userRepository;
        public readonly IPlatformRepository _platform;
        public readonly IStoryRepository _story;

        public CommonController(IUserRepository userRepository, IPlatformRepository platform, IStoryRepository story)
        {

            _userRepository = userRepository;
            _platform = platform;
            _story = story;

        }


        public IActionResult Index()
        {
            return View();
        }



        public JsonResult GetCitys(int countryId)
        {

            List<City> city = _platform.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

       

        public IActionResult AddFav(int MissionId)
        {

            int UId = (int)HttpContext.Session.GetInt32("UId");

            bool favorite = _platform.AddFav(UId, MissionId);



            return RedirectToAction("PlatformLandingPage", "Home");
        }

        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = _platform.Filter(cityId, countryId, themeId, skillId, search, sort);
            int cnt = cards.Count;
            ViewBag.cont = cnt;
            PlatformModel platformModel = new PlatformModel();
            {
                platformModel.Mission = cards;
            }

            if (cards.Count == 0)
            {
                return PartialView("_NoMissionFound");
            }


            //return PartialView("_GridPartial", platformModel);
        
            return PartialView("_FilterMissionPartial", platformModel);
        }



        //public IActionResult Filter(List<int>? cityId, List<int>? countryId)
        //{
        //    List<Mission> cards = new List<Mission>();
        //    var missioncards = _platform.GetMissionDetails();
        //    if (cityId.Count != 0)
        //    {
        //        foreach (var n in cityId)
        //        {
        //            foreach (var item in missioncards)
        //            {
        //                if (item.CityId == n)
        //                {
        //                    cards.Add(item);
        //                }

        //            }
        //        }
        //    }
        //    else if (countryId.Count != 0)
        //    {
        //        foreach (var n in countryId)
        //        {
        //            foreach (var item in missioncards)
        //            {
        //                if (item.CountryId == n)
        //                {
        //                    cards.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in missioncards)
        //        {
        //            cards.Add(item);
        //        }
        //    }
        //    return PartialView("_FilterMissionPartial", cards);
        //}
        public IActionResult StoryFilter(string? search)
        {

            List<Story> cards = _story.StoryFilter(search);

            StoryModel sModel = new StoryModel();
            {
                sModel.stories = cards;
            }

            return PartialView("_FilterStory",sModel);
        }


    }
}
