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
            return View(vm);
        }
       
    }
}
