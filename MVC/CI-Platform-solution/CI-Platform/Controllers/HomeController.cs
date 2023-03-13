

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
        
        public readonly IUserRepository _userRepository;
        public readonly IPlatformRepository _platform;
     
        public HomeController( IUserRepository userRepository, IPlatformRepository platform)
        {
           
            _userRepository = userRepository;
            _platform = platform;
         
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Registration()
        //{
        //    return View();
        //}


        //public IActionResult PlatformLandingPage()
        //{
        //    return View();
        //}


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


        //[HttpPost]
        //public IActionResult PlatformLandingPage()
        //{ 
        
        
        
        
        
        
        
        
        
        //}




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



        public IActionResult Gridview()
        {
            return View();
        }

        public IActionResult ListView()
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

            return View();
        }
        public IActionResult Volunteering_Mission()
        {
            return View();
        }

        public IActionResult No_Mission_Found()
        {
            return View();
        }
        public IActionResult day1()
        {
            return View();
        }

        //public IActionResult ForgotPwd()
        //{
        //    return View();
        //}
        //public IActionResult ResetPwd()
        //{
        //    return View();
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