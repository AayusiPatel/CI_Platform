
using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class UserController : Controller
    {
        private readonly CiPlatformContext _db;

        public UserController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User obj)
        {
            _db.Users.Add(obj);
            _db.SaveChanges();
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index(User obj)
        {
            if (_db.Users.Any(u => u.Email == obj.Email && u.Password == obj.Password))
            { return RedirectToAction("GridView", "Home"); }
            return View();
        }
    }
}
