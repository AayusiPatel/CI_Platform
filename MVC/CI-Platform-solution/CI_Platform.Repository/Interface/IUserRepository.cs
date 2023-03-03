using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUserRepository
    {
        public User Login(Login obj);
        public bool Registration(UserModel obj);
        //public bool ResetPwd(User obj);
        public User forgot(ForgotPwd obj);
        public PasswordReset reset(ResetPwd obj, string token);
    }
}
