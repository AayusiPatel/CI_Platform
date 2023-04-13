using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_Platform.Controllers
{
    public class ProfileController : Controller
    {

        public readonly IProfileRepository _profile;
        public readonly IPlatformRepository _platform;

        public ProfileController(IProfileRepository profile, IPlatformRepository platform)
        {
            _profile = profile;
            _platform = platform;
        }
        public IActionResult UserProfile()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            string avatar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avatar = avatar;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ProfileViewModel pm = _profile.getUser(UserId);
                return View(pm);
            }


            return View();

        }


        [HttpPost]
        public IActionResult UserProfile(ProfileViewModel obj, int resetPass)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            string avatar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avatar = avatar;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");

                bool pm = _profile.updateUser(obj, UserId);
                if (pm == false && resetPass == 1)
                {
                    TempData["error"] = "Old Password is Worng!";
                }
                obj = _profile.getUser(UserId);

                return View(obj);
            }


            return View(obj);
        }

        public bool ContactUs(ContactUsViewModel obj)
        {
            if (obj.Name == null || obj.Message == null || obj.Email == null || obj.Subject == null)
            {
                return false;
            }
            bool ContactUs = _profile.ContactUs(obj);
            
        
            return ContactUs;
        }



        public IActionResult TimeSheet()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            string avatar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avatar = avatar;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");

               TimeSheetViewModel tm = _profile.GetActivities(UserId);

                return View(tm);
            }

                return View();
        }

        [HttpPost]
        public IActionResult TimeSheet(TimeSheetViewModel obj)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");

            if (obj.TimesheetId == 0)
            {

                _profile.AddActivity(obj, UserId);
            }
            if (obj.TimesheetId != 0)
            {
                _profile.UpdateActivity(obj);

            }
            TimeSheetViewModel tm = _profile.GetActivities(UserId);

            return View(tm);
        }
        public IActionResult getActivity(int tid)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");

            TimeSheetViewModel tm = _profile.GetActivity(tid,UserId);


            if(tm.Action == null)
                return PartialView("TimesheetModel", tm);

            if(tm.Time == null)
                return PartialView("GoalsheetModel", tm);

            return View("Error");
        }

        public IActionResult deleteActivity(int tid)
        {
            _profile.DeleteActivity(tid);
            return RedirectToAction("TimeSheet" , "Profile");
        }


        }
}
