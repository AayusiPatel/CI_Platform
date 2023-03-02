using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUserRepository
    {
        public bool Login(User obj);
        public bool Registration(User obj);
        //public bool ResetPwd(User obj);
        public User forgot(User obj);
        public PasswordReset reset(User obj, string token);
    }
}
