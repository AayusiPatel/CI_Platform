using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult UserProfile(ProfileViewModel obj)
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
                bool pm = _profile.updateUser(obj,UserId);
                obj.skill = _profile.getUser(UserId).skill;
                obj.userSkills = _profile.getUser(UserId).userSkills;
                return View(obj);
            }


            return View(obj);
        }
    }
}
