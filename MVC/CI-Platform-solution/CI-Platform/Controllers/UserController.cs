using System;
using CI_Platform.Entities.Data;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using CI_Platform.Entities.ViewModels;

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
        [ValidateAntiForgeryToken]
        public IActionResult Registration(UserModel obj)
        {

            //User user = new User();
            //{
            //        user.FirstName = obj.FirstName;
            //         user.LastName  = obj.LastName;
            //        user.Email  = obj.Email;
            //        user.Password = obj.Password;
            //        user.PhoneNumber = obj.PhoneNumber;
            //}
            if (ModelState.IsValid) { 
            if (_userRepository.Registration(obj))
            {
                TempData["Registered"] = "Registration Succesfull!";
                return RedirectToAction("Index");
            }
                TempData["UserExist"] = "This Email is already Registered.";
            }
           
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login obj)
        {
            //User user = new User();
            //{

            //    user.Email = obj.Email;
            //    user.Password = obj.Password;

            //}
            if (ModelState.IsValid)
            {
                var loguser = _userRepository.Login(obj);

                if (loguser != null)
                {
                    //var s_string = HttpContext.Session.GetString(obj.Email);

                    //string loggedin = null;
                    //HttpContext.Session.SetString(loggedin, s_string);
                    //ViewBag.LoggedIn = loggedin;
                    //ISession["var1"] = "obg";

                    HttpContext.Session.SetString("Uname", loguser.FirstName + " " + loguser.LastName);


                    return RedirectToAction("PlatformLandingPage", "Home");
                }
                else
                {
                    TempData["LoginError"] = "Invalid Email or Password";
                }
            }
           
            return View();

        }
        public IActionResult ForgotPwd()
        {
            return View();
        }




    [HttpPost]
        public IActionResult ForgotPwd(ForgotPwd obj)
        {
            //User user = new User();
            //{
            //    user.FirstName = obj.FirstName;
            //    user.LastName = obj.LastName;
            //    user.Email = obj.Email;
            //    user.Password = obj.Password;
            //    user.PhoneNumber = obj.PhoneNumber;

            //}
            if (ModelState.IsValid)
            {
                var fp = _userRepository.forgot(obj);
                if (fp == null)
                {
                    TempData["Message"] = "Invalid Email";
                    return View();
                }

                TempData["Message"] = "Check your email to reset password";
                return RedirectToAction("Index", "User");
            }
            return View();
    }

        public IActionResult ResetPwd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPwd(ResetPwd obj, string token)
        {
            //User user = new User();
            //{
            //    user.FirstName = obj.FirstName;
            //    user.LastName = obj.LastName;
            //    user.Email = obj.Email;
            //    user.Password = obj.Password;
            //    user.PhoneNumber = obj.PhoneNumber;

            //}
            if (ModelState.IsValid)
            {
                var validToken = _userRepository.reset(obj, token);

            if (validToken != null)
            {
                TempData["Message"] = "Your Password is changed";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Token not Found";
            return RedirectToAction("Login");
            }
            return View();
        }



     


        }
    }
