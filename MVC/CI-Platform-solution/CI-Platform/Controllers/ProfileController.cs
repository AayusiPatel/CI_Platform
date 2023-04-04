using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult UserProfile()
        {
            return View();
        }
    }
}
