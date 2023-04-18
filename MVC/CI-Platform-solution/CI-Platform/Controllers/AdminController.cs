using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class AdminController : Controller
    {
        public readonly IAdminRepository _admin;
        public AdminController(IAdminRepository admin)
        {


            _admin = admin;
          
        }
        public IActionResult AdminMain()
        {
            AdminViewModel vm = _admin.displayModel();
            ViewBag.totalPages = (int)Math.Ceiling(vm.users.Count() / (double)5);
            return View(vm);
        }

        public IActionResult SearchAdmin(string obj , int page , int pageIndex = 1)
        {
            int pageSize = 3;
            AdminViewModel am = new AdminViewModel();
            if(page == 1)
            {
                List<User> users = _admin.searchUser(obj);
                am.users = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalPages = (int)Math.Ceiling(users.Count() / (double)pageSize);
                return PartialView("_UserAdmin", am);
            }
            if (page == 2)
            {
                am.cms = _admin.searchCms(obj);
                return PartialView("_CmsAdmin", am);
            }
            if (page == 3)
            {
                am.missions = _admin.searchMission(obj);
                ViewBag.totalPages = (int)Math.Ceiling(am.missions.Count() / (double)pageSize);
                am.missions = am.missions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_MissionAdmin", am);
            }
            if (page == 4)
            {
                am.missionApplications = _admin.searchMissionApplication(obj);
                return PartialView("_MissionApplicationAdmin", am);
            }
            if (page == 5)
            {
                am.stories = _admin.searchStory(obj);
                return PartialView("_StoryAdmin", am);
            }
            return RedirectToAction("Error");
        }

        public IActionResult EditForm(int id,int page)
        {

            AdminViewModel am = new AdminViewModel();

            am = _admin.EditForm(id,page);

            if (page == 2)
            {
               
                return PartialView("_CmsAdmin", am);
            }
            if (page == 3)
            {
     
                return PartialView("_MissionAdmin", am);
            }
            return View("Error");

        }

    }
}
