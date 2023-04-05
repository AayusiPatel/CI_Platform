using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class ProfileController : Controller
    {
       
        public readonly IProfileRepository _profile;

      
        public ProfileController(IProfileRepository profile)
        {
            _profile = profile;
       }
        public IActionResult UserProfile()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            string avatar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avatar = avatar;
            if(name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
              ProfileViewModel pm =  _profile.getUser(UserId);
                return View(pm);
            }


            return View();
        }
    }
}
