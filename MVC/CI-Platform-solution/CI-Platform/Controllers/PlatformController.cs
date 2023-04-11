using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class PlatformController : Controller
    {
        public readonly IPlatformRepository _platform;
        public readonly IVolunteerRepository _volunteer;
        public readonly IStoryRepository _story;


        public PlatformController(IPlatformRepository platform, IVolunteerRepository volunteer, IStoryRepository story)
        {

            //_userRepository = userRepository;
            _platform = platform;
            _volunteer = volunteer;
            _story = story;

        }

        [HttpGet]
    

        public IActionResult PlatformLandingPage()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            //ViewBag.City = _platform.GetCitys();

            List<Mission> missionDeails = _platform.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;

            ViewBag.cont = missionDeails.Count;


            List<City> Cities = _platform.GetCitys();
            ViewBag.Cities = Cities;
            List<Country> Countries = _platform.GetCountry();
            ViewBag.Countries = Countries;
            List<MissionTheme> Themes = _platform.GetMissionTheme();
            ViewBag.Themes = Themes;
            List<MissionSkill> Skills = _platform.GetSkills();
            ViewBag.Skills = Skills;



            PlatformModel ms = _platform.GetMissions();


            return View(ms);
        }
        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int PageIndex =1)
        {
            List<Mission> cards = _platform.Filter(cityId, countryId, themeId, skillId, search, sort);

            int PageSize = 3;

            int TotalRecords = (int)Math.Ceiling(cards.Count() / (double)PageSize);

            List<Mission> records = cards.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();



            PlatformModel platformModel = new PlatformModel();
            {
                platformModel.Mission = records;
                platformModel.totalcount = TotalRecords;
            }

            return PartialView("_FilterMissionPartial", platformModel);


        }
        public bool AddFav(int MissionId)
        {

            int UId = (int)HttpContext.Session.GetInt32("UId");

            bool favorite = _platform.AddFav(UId, MissionId);

            return favorite;



            //return RedirectToAction("PlatformLandingPage", "Home");
        }




        public IActionResult Volunteering_Mission(int mid, int pageIndex = 1)
        {


            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                var UId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.uid = UId;
            }

            VolunteerModel volunteerModel = _volunteer.DisplayModel(mid, pageIndex);




            return View(volunteerModel);
        }


        public void AddComment(int obj, string comnt)
        {

            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool comntAdded = _volunteer.addComment(obj, UserId, comnt);
            if (comntAdded)
            {
                ViewBag.ComntAdded = "Comment Added";
            }
            else
            {
                ViewBag.ComntAdded = "Comment not Added";
            }

        }



        [HttpPost]
        public bool applyMission(int missionId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            var apply = _volunteer.applyMission(missionId, UserId);
            if (apply == true)
            {
                //TempData["success"] = "Applied Successfully...";
                return apply;
            }
            //TempData["error"] = "You've already Applied... ";
            return false;
        }


        public void RecommandToCoWorker(List<int> toUserId, int mid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

            _volunteer.RecommandToCoWorker(FromUserId, toUserId, mid);



        }

        //Star Rating
        public JsonResult MissionRating(int mid, int rating)
        {
            int userId = (int)HttpContext.Session.GetInt32("UId");
            bool success = _volunteer.MissionRating(userId, mid, rating);
            return Json(success);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
