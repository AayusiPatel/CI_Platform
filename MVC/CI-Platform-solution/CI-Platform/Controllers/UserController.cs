using System;
using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;


namespace CI_Platform.Controllers
{
    public class UserController : Controller
    {

        public readonly IUserRepository _userRepository;
        public readonly CiPlatformContext _db;

        public UserController(IUserRepository userRepository, CiPlatformContext db)
        {

            _userRepository = userRepository;
            _db = db;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Registration(User obj)
        {
           
            User user = new User();
            {
                    user.FirstName = obj.FirstName;
                     user.LastName  = obj.LastName;
                    user.Email  = obj.Email;
                    user.Password = obj.Password;
                    user.PhoneNumber = obj.PhoneNumber;
            }
            _userRepository.Registration(user);

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User obj)
        {
            if(_userRepository.Login(obj))
                 return RedirectToAction("GridView","Home");
            return View();

        }
        public IActionResult ForgotPwd()
        {
            return View();
        }




    [HttpPost]
        public IActionResult ForgotPwd(User obj)
        {
            User user = new User();
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.Email = obj.Email;
                user.Password = obj.Password;
                user.PhoneNumber = obj.PhoneNumber;

            }
            var fp = _userRepository.forgot(user);
            if (fp == null)
            {
                TempData["Message"] = "Invalid Email";
                return View();
            }
            TempData["Message"] = "Check your email to reset password";
        return RedirectToAction("Index", "User");
    }

        public IActionResult ResetPwd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPwd(User obj, string token)
        {
            User user = new User();
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.Email = obj.Email;
                user.Password = obj.Password;
                user.PhoneNumber = obj.PhoneNumber;

            }
            var validToken = _userRepository.reset(user, token);

            if (validToken != null)
            {
                TempData["Message"] = "Your Password is changed";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Token not Found";
            return RedirectToAction("Login");
        }
    }
}
