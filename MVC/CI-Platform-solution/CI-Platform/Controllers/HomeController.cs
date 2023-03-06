
using CI_Platform.Entities.Models;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult PlatformLandingPage()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            
            //ViewBag.City = _platform.GetCitys();

            List<Mission> missionDeails = _platform.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;


            return View();
        }

        public IActionResult Gridview()
        {
            return View();
        }

        public IActionResult ListView()
        {
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