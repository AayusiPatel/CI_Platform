using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

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

        public User forgot(User obj)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.Equals(obj.Email.ToLower()) && u.DeletedAt == null);

            #region Genrate Token
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            #endregion Genrate Token

            #region Update Password Reset Table
            PasswordReset entry = new PasswordReset();
            entry.Email = obj.Email;
            entry.Token = finalString;
            _db.PasswordResets.Add(entry);
            _db.SaveChanges();
            #endregion Update Password Reset Table

            #region Send Mail
            var mailBody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:7228/User/ResetPwd?token=" + finalString + "'>Reset Password</a></h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("payushi.tatva@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("payushi.tatva@gmail.com", "dvmiwwylvnkgdinq");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail

            return user;
        }


        public PasswordReset reset(User obj, string token)
        {
            var validToken = _db.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validToken != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == validToken.Email);
                user.Password = obj.Password;
                _db.Users.Update(user);
                _db.SaveChanges();

            }
            return validToken;
        }

        //public User ForgotPwd(User obj)
        //{
        //    var reset = _db.Users.Where(u => u.Email == obj.Email);

        //    return reset;
        //}

        //public bool ResetPwd(User obj)
        //{
        //    if(_db.Users.Any(u => u.Email == obj.Email)){
        //        _db.Users.Update(obj);
        //        _db.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

       
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
