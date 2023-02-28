
using CI_Platform.Entities.Models;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository _userRepository)
        {
            _logger = logger;
            _userRepository = _userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Registration(User obj)
        //{
        //    _userRepository.Users.Add(obj);
        //    _userRepository.SaveChanges();
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Registration(User obj)
        //{
        //    //     if (obj == null)
        //    //         return BadRequest("Model is null");

        //    User user = new User()
        //    {
        //        FirstName = obj.FirstName,
        //        LastName = obj.LastName,
        //        Email = obj.Email,
        //        Password = obj.Password,
        //        PhoneNumber = obj.PhoneNumber,

        //    };
        //    if (obj.Email != null && obj.Password != null)
        //    {
        //        _userRepository.Registration(user);
        //        //     //User user = new User(response);

        //        //     //return Ok(bookModel);
        //        return RedirectToAction("Index", "Home");
        //            }
        //    return View();
        //}
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