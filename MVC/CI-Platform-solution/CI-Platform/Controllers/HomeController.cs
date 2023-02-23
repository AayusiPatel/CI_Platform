<<<<<<< HEAD
﻿using CI_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public IActionResult ForgotPwd()
        {
            return View();
        }
        public IActionResult ResetPwd()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
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
=======
﻿using CI_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
        public IActionResult day1()
        {
            return View();
        }

        public IActionResult ForgotPwd()
        {
            return View();
        }
        public IActionResult ResetPwd()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
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
>>>>>>> bef5d83fe5d7b190be8a85005db71854be521777
}