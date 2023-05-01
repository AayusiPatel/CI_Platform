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
        

        public UserController(IUserRepository userRepository)
        {

            _userRepository = userRepository;
            
        }

        public IActionResult Registration()
        {
            ViewBag.banners = _userRepository.banners();
            return View();
        }

        [HttpPost]
     
        public IActionResult Registration(UserModel obj)
        {
            ViewBag.banners = _userRepository.banners();

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
                var chk = _userRepository.Registration(obj);
                if (chk)
                {
                    TempData["Registered"] = "Registration Succesfull!";
                    return RedirectToAction("Index");
                }
                else { 
                TempData["UserExist"] = "This Email is already Registered.";
            }
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Index(String returnUrl="")
        {
            Login login = new Login();
            {
                login.returnUrl = returnUrl;
            }
            ViewBag.banners = _userRepository.banners();
            
            return View(login);
        }

        [HttpPost]
        public IActionResult Index(Login obj)
        {

            ViewBag.banners = _userRepository.banners();

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

                    if(loguser.Avatar == null)
                    {
                        loguser.Avatar = "user1.png";
                    }
                    var claims = new List<Claim>
                {
                        new Claim("role",role),
                         new Claim("Name", $"{loguser.FirstName} {loguser.LastName}"),
                        new Claim("Email", loguser.Email),
                        new Claim("Sid", loguser.UserId.ToString()),
                       new Claim(ClaimTypes.Role,role),
                        new Claim("Avtar", loguser.Avatar),

                };

                    var identity = new ClaimsIdentity(claims, "Cookie");
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
            ViewBag.banners = _userRepository.banners();
            return View();
        }




        [HttpPost]
        public IActionResult ForgotPwd(ForgotPwd obj)
        {
            ViewBag.banners = _userRepository.banners();
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
            ViewBag.banners = _userRepository.banners();
            return View();
        }

        [HttpPost]
        public IActionResult ResetPwd(ResetPwd obj, string token)
        {
            ViewBag.banners = _userRepository.banners();
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
            return View(obj);
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            HttpContext.Session.Clear();
            return RedirectToAction("Index" , "User");
        }



    }
}
