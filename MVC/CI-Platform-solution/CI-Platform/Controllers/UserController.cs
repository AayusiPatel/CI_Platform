using System;
using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
           
            User user = new User();
            {
                    user.FirstName = obj.FirstName;
                     user.LastName  = obj.LastName;
                    user.Email  = obj.Email;
                    user.Password = obj.Password;
                    user.PhoneNumber = obj.PhoneNumber;
            }
           if(_userRepository.Registration(user))
                return RedirectToAction("Index");


            TempData["UserExist"] = "This Email is already Registered.";
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User obj)
        {
            if (_userRepository.Login(obj))
            {
                return RedirectToAction("GridView", "Home");
            }
            else
            {
                TempData["LoginError"] = "Invalid Email or Password";
            }
            return View();

        }
        public IActionResult ForgotPwd()
        {
            return View();
        }




    [HttpPost]
        public IActionResult ForgotPwd(UserModel obj)
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
        public IActionResult ResetPwd(UserModel obj, string token)
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
