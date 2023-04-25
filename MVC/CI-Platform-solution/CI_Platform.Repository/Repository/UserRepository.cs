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
using CI_Platform.Entities.ViewModels;
using System.Web.Helpers;

namespace CI_Platform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly CiPlatformContext _db;

      

        public UserRepository(CiPlatformContext db)
        {
            _db = db;

        }

        public bool adminCheck(String email)
        {
            bool admin = _db.Admins.Any(u => u.Email == email);

            return admin;
        }
        public User Login(Login obj)
        {
            User user = new User();
            {

                user.Email = obj.Email;
                user.Password = obj.Password;

            }
            User user1 = _db.Users.FirstOrDefault(u => u.Email == obj.Email);
            if (user1 != null && Crypto.VerifyHashedPassword(user1.Password, obj.Password))
            {
                return user1;
            }
            return null;
        }


        public bool Registration(UserModel obj)
        {
            User user = new User();
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.Email = obj.Email;
                user.Password = Crypto.HashPassword(obj.Password);
                user.PhoneNumber = obj.PhoneNumber;
            }
            if (_db.Users.Any(x => x.Email == obj.Email))
                return false;


            _db.Users.Add(user);
            _db.SaveChanges();
            return true;

            //return entry.Entity;
        }

        public User forgot(ForgotPwd obj)
        {

            User user = new User();
            {

                user.Email = obj.Email;


            }

            var user1 = _db.Users.FirstOrDefault(u => u.Email.Equals(obj.Email.ToLower()) && u.DeletedAt == null);

            if (user1 == null)
            {
                return null;
            }

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
            {
                entry.Email = obj.Email;
                entry.Token = finalString;
            }
            _db.PasswordResets.Add(entry);
            _db.SaveChanges();
            #endregion Update Password Reset Table

            #region Send Mail
            var mailBody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:7228/User/ResetPwd?token=" + finalString + "'>Reset Password</a></h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("payushi.tatva@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user1.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("payushi.tatva@gmail.com", "qgtaopriiacrebdf");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail

            return user1;
        }


        public PasswordReset reset(ResetPwd obj, string token)
        {
            User user = new User();
            {

                user.Password = obj.Password;


            }
            var validToken = _db.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validToken != null)
            {
                var user1 = _db.Users.FirstOrDefault(x => x.Email == validToken.Email);
                user1.Password = Crypto.HashPassword(obj.Password);
                _db.Users.Update(user1);
                _db.SaveChanges();

            }
            return validToken;
        }

        public bool checktime(string token)
        {
            PasswordReset pr = _db.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (pr == null)
            {
                return true;
            }
            DateTime dateTimeVariable = pr.CreatedAt;
            if (DateTime.Now.Subtract(dateTimeVariable).TotalHours > 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
