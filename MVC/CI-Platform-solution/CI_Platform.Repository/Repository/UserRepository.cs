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
        public readonly CiPlatformContext _db;
       

        public UserRepository(CiPlatformContext db)
        {
            _db = db;

        }

        public bool Login(User obj)
        {
            var user = _db.Users.Any(u => u.Email == obj.Email && u.Password == obj.Password) ;


           return user;
        }


        public void Registration(User obj)
        {
          
             _db.Users.Add(obj);
            _db.SaveChanges();
            //return entry.Entity;
        }

        //public User ForgotPwd(User obj)
        //{
        //    var reset = _db.Users.Where(u => u.Email == obj.Email);
            
        //    return reset;
        //}

        public bool ResetPwd(User obj)
        {
            if(_db.Users.Any(u => u.Email == obj.Email)){
                _db.Users.Update(obj);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

       
        //public ActionResult ForgotPwd(string UserName)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (WebSecurity.UserExists(UserName))
        //        {
        //            string To = UserName, UserID, Password, SMTPPort, Host;
        //            string token = WebSecurity.GeneratePasswordResetToken(UserName);
        //            if (token == null)
        //            {
        //                // If user does not exist or is not confirmed.
        //                return View("Index");
        //            }
        //            else
        //            {
        //                //Create URL with above token
        //                var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = UserName, code = token }, "http") + "'>Reset Password</a>";
        //                //HTML Template for Send email
        //                string subject = "Your changed password";
        //                string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
        //                //Get and set the AppSettings using configuration manager.
        //                EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);
        //                //Call send email methods.
        //                EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
        //            }
        //        }
        //    }
        //    return View();
        //}
    }
}
