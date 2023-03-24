

using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {

        //public readonly IUserRepository _userRepository;
        public readonly IPlatformRepository _platform;
        public readonly IVolunteerRepository _volunteer;
        public readonly IStoryRepository _story;


        public HomeController(IPlatformRepository platform, IVolunteerRepository volunteer, IStoryRepository story)
        {

            //_userRepository = userRepository;
            _platform = platform;
            _volunteer = volunteer;
            _story = story;

        }

        //[HttpPost]
        public IActionResult PlatformLandingPage()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            
            //ViewBag.City = _platform.GetCitys();

            List<Mission> missionDeails = _platform.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;

            ViewBag.cont = missionDeails.Count;

       
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;


            PlatformModel ms = _platform.GetMissions();


            return View(ms);
        }


   

            public JsonResult GetCitys(int countryId)
          {

            List<City> city = _platform.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
                 }

        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = _platform.Filter(cityId, countryId, themeId, skillId, search, sort);
            PlatformModel platformModel = new PlatformModel();
            {
                platformModel.Mission = cards;
            }

            return PartialView("_FilterMissionPartial", platformModel);


        }



        public bool AddFav(int MissionId)
        {

            int UId = (int)HttpContext.Session.GetInt32("UId");

            bool favorite = _platform.AddFav(UId, MissionId);

            return favorite;



            //return RedirectToAction("PlatformLandingPage", "Home");
        }



        public IActionResult Gridview()
        {
            return View();
        }

        public IActionResult ListView()
        {


            return View();
        }
        public IActionResult Volunteering_Mission(int mid, int pageIndex = 1)
        {


            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                var UId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.uid = UId;
            }

            VolunteerModel volunteerModel = _volunteer.DisplayModel(mid, pageIndex);




            return View(volunteerModel);
        }


        //[HttpPost]
        //public IActionResult Volunteering_Mission(VolunteerModel obj)
        //{
        //    int UserId = (int)HttpContext.Session.GetInt32("UId");
        //    bool comntAdded = _volunteer.addComment(obj, UserId);
        //    if (comntAdded)
        //    {
        //        ViewBag.ComntAdded = "Comment Added";
        //    }
        //    else
        //    {
        //        ViewBag.ComntAdded = "Comment not Added";
        //    }

        //    int mid = (int)obj.mission.MissionId;
        //    string name = HttpContext.Session.GetString("Uname");
        //    ViewBag.Uname = name;
        //    VolunteerModel volunteerModel = _volunteer.DisplayModel(mid);


        //    return View(volunteerModel);
        //}




        public void AddComment(int obj, string comnt)
        {

            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool comntAdded = _volunteer.addComment(obj, UserId, comnt);
            if (comntAdded)
            {
                ViewBag.ComntAdded = "Comment Added";
            }
            else
            {
                ViewBag.ComntAdded = "Comment not Added";
            }



        }



        [HttpPost]
        public bool applyMission(int missionId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            var apply = _volunteer.applyMission(missionId, UserId);
            if (apply == true)
            {
                //TempData["success"] = "Applied Successfully...";
                return apply;
            }
            //TempData["error"] = "You've already Applied... ";
            return false;
        }

        public void RecommandToCoWorker(List<int> toUserId, int mid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

            _volunteer.RecommandToCoWorker(FromUserId, toUserId, mid);



        }

        //Star Rating
        public JsonResult MissionRating(int mid, int rating)
        {
            int userId = (int)HttpContext.Session.GetInt32("UId");
            bool success = _volunteer.MissionRating(userId, mid, rating);
            return Json(success);
        }


        public IActionResult No_Mission_Found()
        {
            return View();
        }
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

        //public void StoryFilter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        //{

        //    List<Story> cards = _story.StoryFilter(cityId, countryId, themeId, skillId, search, sort);

        //    StoryModel sModel = new StoryModel();
        //    {
        //        sModel.stories = cards;
        //    }


        //}



        public IActionResult Privacy()
        {
            return View();
        }














        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
﻿