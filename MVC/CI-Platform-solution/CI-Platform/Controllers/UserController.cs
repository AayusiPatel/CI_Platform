using CI_Platform.Data;
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
    }
}
