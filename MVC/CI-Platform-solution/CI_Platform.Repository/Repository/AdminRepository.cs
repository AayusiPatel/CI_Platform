using CI_Platform.Entities.Data;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public readonly CiPlatformContext _db;



        public AdminRepository(CiPlatformContext db)
        {
            _db = db;

        }

        public AdminViewModel displayModel()
        {
            AdminViewModel adminModel = new AdminViewModel();

            {
                adminModel.users = _db.Users.ToList();
                adminModel.missions = _db.Missions.ToList();
                adminModel.cms = _db.CmsPages.ToList();
            }

            return adminModel;
        }


    }
}
