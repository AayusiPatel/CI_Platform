using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Repository.Interface;

namespace CI_Platform.Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly CiPlatformContext _db;
        public UserRepository(CiPlatformContext db)
        {
            _db = db;
        }
        public void Registration(User obj)
        {
            var entry = _db.Users.Add(obj);
            _db.SaveChanges();
            //return entry.Entity;
        }
    }
}
