using System;
using CI_Platform.Entities.Data;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CI_Platform.Entities.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
            if (ModelState.IsValid)
            {
                if (_userRepository.Registration(obj))
                {
                    TempData["Registered"] = "Registration Succesfull!";
                    return RedirectToAction("Index");
                }
                TempData["UserExist"] = "This Email is already Registered.";
            }

            return View();
        }
        public IActionResult Index(String returnUrl="")
        {
            Login login = new Login();
            {
                login.returnUrl = returnUrl;
            }
            return View(login);
        }

        [HttpPost]
        public IActionResult Index(Login obj)
        {
           
          

            if (ModelState.IsValid)
            {
                var loguser = _userRepository.Login(obj);

                if (loguser != null)
                {


                    //var options = new CookieOptions
                    //{
                    //    Expires = DateTime.Now.AddDays(30),
                    //    IsEssential = true,
                    //    HttpOnly = true,
                    //    SameSite = SameSiteMode.Strict
                    //};

                    //Response.Cookies.Append("Email", loguser.Email, new CookieOptions
                    //{
                    //    Expires = DateTime.UtcNow.AddDays(7),
                    //    IsEssential = true // Optional, but recommended.
                    //});


                    bool roleCheck = _userRepository.adminCheck(loguser.Email);
                    String role = "User";
                    if (roleCheck)
                    {
                         role = "Admin";
                    }
                   

                    var claims = new List<Claim>
                {
                        new Claim("role",role),
                         new Claim("Name", $"{loguser.FirstName} {loguser.LastName}"),
                        new Claim("Email", loguser.Email),
                        new Claim("Sid", loguser.UserId.ToString()),
                   
                };

                    var identity = new ClaimsIdentity(claims, "AuthCookie");
                    var Principle = new ClaimsPrincipal(identity);

                    HttpContext.User = Principle;

                    var abc = HttpContext.SignInAsync(Principle);

             



                    HttpContext.Session.SetString("Uname", loguser.FirstName + " " + loguser.LastName);
                    HttpContext.Session.SetInt32("UId", (int)loguser.UserId);
                    if(loguser.Avatar != null)
                         HttpContext.Session.SetString("Avatar", loguser.Avatar);
                    TempData["true"] = "Logged Successfully!";

                    
                    if (!string.IsNullOrEmpty(obj.returnUrl))
                    {
                        return LocalRedirect(obj.returnUrl);
                    }

                    return RedirectToAction("PlatformLandingPage", "Platform");
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
                bool time = _userRepository.checktime(token);
                if (!time)
                {
                    TempData["Message"] = " Your token is Expired ";
                    return RedirectToAction("Index");
                }
                var validToken = _userRepository.reset(obj, token);

                if (validToken != null)
                {
                    TempData["Message"] = "Your Password is changed";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Token not Found";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            HttpContext.Session.Clear();
            return RedirectToAction("Index" , "User");
        }



    }
}
