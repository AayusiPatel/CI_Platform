﻿using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;

namespace CI_Platform.Controllers
{
    //[Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public readonly IAdminRepository _admin;
        public AdminController(IAdminRepository admin)
        {


            _admin = admin;
          
        }
        public IActionResult AdminMain(int command = 1)
        {
            int pageIndex = 1;
            int pageSize = 5;
            AdminViewModel vm = _admin.displayModel();
            vm.userPages = (int)Math.Ceiling(vm.users.Count() / (double)pageSize);
            vm.users = vm.users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.cmsPages = (int)Math.Ceiling(vm.cms.Count() / (double)pageSize);
            vm.cms = vm.cms.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.missinPages = (int)Math.Ceiling(vm.missions.Count() / (double)pageSize);
            vm.missions = vm.missions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.mApplicationPages = (int)Math.Ceiling(vm.missionApplications.Count() / (double)pageSize);
            vm.missionApplications = vm.missionApplications.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.storyPages = (int)Math.Ceiling(vm.stories.Count() / (double)pageSize);
            vm.stories = vm.stories.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.ThemePages = (int)Math.Ceiling(vm.themes.Count() / (double)pageSize);
            vm.themes = vm.themes.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.skillPages = (int)Math.Ceiling(vm.skills.Count() / (double)pageSize);
            vm.skills = vm.skills.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            vm.BannerPage = (int)Math.Ceiling(vm.banners.Count() / (double)pageSize);
            vm.banners = vm.banners.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            vm.activeTab = command;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AdminMain(AdminViewModel obj, int command)
        //public IActionResult AddCms(AdminViewModel obj, int command)
        {
            if(command == 1)
            {
                bool status = _admin.AddUser(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if (status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }
            if (command == 3)
            {
                bool status = _admin.AddMission(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if (status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }

            if (command == 2)
            {
                bool status = _admin.AddCms(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if (status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }
            if (command == 6)
            {
                bool status = _admin.AddTheme(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if (status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }
            if (command == 7)
            {
                bool status = _admin.AddSkill(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if (status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }
          
            if (command == 8)
            {
                bool status = _admin.AddBanner(obj);
                if (!status)
                {
                    TempData["Error"] = "Please Try Again";
                }
                if(status)
                {
                    TempData["Success"] = "Succesfull!";
                }
            }
            return RedirectToAction("AdminMain","Admin", new { command = command });
        }

        public IActionResult DeleteActivity(int id,int page)
        {
            bool status = _admin.DeleteActivity(id,page);
            if (!status)
            {
                TempData["Error"] = "Please Try Again";
            }
            if (status)
            {
                TempData["Success"] = "Deleted Successfully!";
            }
            return RedirectToAction("AdminMain","Admin", new { command = page });
        }

        public IActionResult SearchAdmin(string obj , int page , int pageIndex = 1)
        {
            int pageSize = 5;
            AdminViewModel am = new AdminViewModel();
            if(page == 1)
            {
                List<User> users = _admin.searchUser(obj);
                am.users = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                am.userPages = (int)Math.Ceiling(users.Count() / (double)pageSize);
                return PartialView("_UserAdmin", am);
            }
            if (page == 2)
            {
                am.cms = _admin.searchCms(obj);
                am.cmsPages = (int)Math.Ceiling(am.cms.Count() / (double)pageSize);
                am.cms = am.cms.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_CmsAdmin", am);
            }
            if (page == 3)
            {
                am.missions = _admin.searchMission(obj);
                //ViewBag.totalPages = (int)Math.Ceiling(am.missions.Count() / (double)pageSize);
                am.missinPages = (int)Math.Ceiling(am.missions.Count() / (double)pageSize);
                am.missions = am.missions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_MissionAdmin", am);
            }
            if (page == 4)
            {
                am.missionApplications = _admin.searchMissionApplication(obj);
                am.mApplicationPages = (int)Math.Ceiling(am.missionApplications.Count() / (double)pageSize);
                am.missionApplications = am.missionApplications.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_MissionApplicationAdmin", am);
            }
            if (page == 5)
            {
                am.stories = _admin.searchStory(obj);
                am.storyPages = (int)Math.Ceiling(am.stories.Count() / (double)pageSize);
                am.stories = am.stories.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_StoryAdmin", am);
            }
            if (page == 6)
            {
                am.themes = _admin.searchTheme(obj);
                am.ThemePages = (int)Math.Ceiling(am.themes.Count() / (double)pageSize);
                am.themes = am.themes.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_ThemeAdmin", am);
            }
            if (page == 7)
            {
                am.skills = _admin.searchSkill(obj);
                am.skillPages = (int)Math.Ceiling(am.skills.Count() / (double)pageSize);
                am.skills = am.skills.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_SkillAdmin", am);
            }
            if (page == 8)
            {
                am.banners = _admin.searchBanner(obj);
                am.BannerPage = (int)Math.Ceiling(am.banners.Count() / (double)pageSize);
                am.banners = am.banners.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return PartialView("_BannerAdmin", am);
            }
            return RedirectToAction("Error");
        }

        public IActionResult EditForm(int id,int page)
        {

            AdminViewModel am = new AdminViewModel();

            am = _admin.EditForm(id,page);
            if(page == 1)
            {
                return PartialView("_UserAdmin", am);
            }

            if (page == 2)
            {
               
                return PartialView("_CmsAdmin", am);
            }
            if (page == 3)
            {
     
                return PartialView("_MissionAdmin", am);
            }
            if (page == 6)
            {
                return PartialView("_ThemeAdmin", am);
            }
            if (page == 7)
            {
                return PartialView("_SkillAdmin", am);
            }
            return View("Error");

        }

        public bool Approval(int id,int status,int page)
        {
            if(page == 4)
            {
                _admin.ApplicationApproval(id,status);
                if(status == 0)
                {
                    return false;
                }
                if (status == 1)
                {
                    return true;
                }
            }
            if(page == 5)
            {
                 _admin.StoryApproval(id, status);
                if (status == 0)
                {
                    return false;
                }
                if (status == 1)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
