using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CI_Platform.Controllers
{
    [Authorize]

    public class StoryController : Controller
    {

        public readonly IPlatformRepository _platform;

        public readonly IStoryRepository _story;


        public StoryController(IPlatformRepository platform, IStoryRepository story)
        {
            _platform = platform;
            _story = story;
        }
   
        public IActionResult StoryListing(int PageIndex = 1)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;

            StoryModel model = _story.stories(PageIndex);
            return View(model);
        }
        public IActionResult StoryDetail(int sid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;

            int UserId = (int)HttpContext.Session.GetInt32("UId");
            StoryModel model = _story.storyDetails(sid, UserId);
            return View(model);
        }

        public void RecommandToCoWorker(List<int> toUserId, int sid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

            _story.RecommandToCoWorker(FromUserId, toUserId, sid);



        }

        [Authorize]
        public IActionResult StoryApply()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                List<MissionApplication> misShareStory = _story.missionsSStory(UserId);

                ShareStory ss = new ShareStory();
                {
                    ss.missions = misShareStory;
                }
                return View(ss);
            }


            return View();
        }


        //[HttpPost]
        //public IActionResult SavedStory(int mid)
        //{
        //    string name = HttpContext.Session.GetString("Uname");
        //    ViewBag.Uname = name;
        //    int UserId = (int)HttpContext.Session.GetInt32("UId");
        //    // Check if the saved data exists in your data store based on the selected option
        //    var StoryModel = _story.getData(mid, UserId);
        //    List<MissionApplication> misShareStory = _story.missionsSStory(UserId);
        //    StoryModel.missions = misShareStory;
        //    return View("StoryApply",StoryModel);
        //}

        [HttpPost]
        public IActionResult StoryApply(ShareStory obj, int command)
        {
           
                string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            List<MissionApplication> misShareStory = _story.missionsSStory(UserId);
            obj.missions = misShareStory;
            if (command == 1)
            {
                if (ModelState.IsValid == false)
                {
                    return View(obj);
                }
                else
                {
                    bool abc = _story.saveStory(obj, command, UserId);
                    _story.saveImage(obj, UserId);
                    return View(obj);
                }
            }
            if (command == 2)
            {
                if (ModelState.IsValid == false)
                {
                    return View(obj);
                }
                else
                {

                    bool abc = _story.saveStory(obj, command, UserId);
                    _story.saveImage(obj, UserId);
                    return RedirectToAction("StoryListing", "Story");
                }
            }
            if (command == 3)
            {
                return RedirectToAction("StoryListing", "Story");
            }

            return View(obj);
        }


        public IActionResult StoryFilter(string? search, int PageIndex = 1)
        {

            List<Story> cards = _story.StoryFilter(search);
            int PageSize = 3;
            int TotalRecords = (cards.Count) / PageSize;
            List<Story> records = cards.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            StoryModel sModel = new StoryModel();
            {
                sModel.stories = records;
                sModel.totalcount = TotalRecords;
            }

            return PartialView("_FilterStory", sModel);
        }

        [HttpPost]
        public JsonResult CheckData(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            // Check if the saved data exists in your data store based on the selected option
            var StoryModel = _story.getData(mid, UserId);

            var dataExists = JsonConvert.SerializeObject(StoryModel);



            // Return a boolean value indicating whether the data exists
            return Json(dataExists);

            //return View("~/Story/StoryApply", StoryModel);
            //return RedirectToAction("StoryApply", StoryModel);
        }
    }
}
