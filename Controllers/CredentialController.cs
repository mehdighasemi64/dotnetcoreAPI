using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : ControllerBase
    {
        [HttpPost("PasswordRecovery")]
        public ActionResult PasswordRecovery([FromBody] User user)
        {
            string err = "";

            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(key.ToArray());
            byte[] data = Convert.FromBase64String(token);

            using (var db = new soamanaDB())
            {
                User fetchedUser = db.Users.Where(x => x.Email == user.Email).ToList()[0];

                ResetCredential rc = new ResetCredential();
                rc.TokenHash = token;
                rc.UserName = fetchedUser.UserName;
                rc.ExpirationDate = DateTime.Now.AddDays(1);
                rc.TokenUsed = false;
                db.ResetCredentials.Add(rc);
                db.SaveChanges();
                try
                {
                    //Email.SendEmail(fetchedUser.Email, "Password recovery for SOMANA", "please click the link below for changing your password :  <br/> <br/> <a href=" + "http://www.somana.ir/ResetPassword?username=" + fetchedUser.UserName + "&" + "token=" + token + "> ClickHere </a>");
                    Email.SendEmail(fetchedUser.Email, "Password recovery for SOMANA", "please click the link below for changing your password :  <br/> <br/> <a href=" + "http://shop.somana.ir/ResetPassword?username=" + fetchedUser.UserName + "&" + "token=" + token + "> ClickHere </a>");
                    //Email.SendEmail(fetchedUser.Email, "Password recovery for SOMANA", "please click the link below for changing your password :  <br/> <br/> <a href=" + "http://localhost:4012/ResetPassword?username=" + fetchedUser.UserName + "&" + "token=" + token + "> ClickHere </a>");

                }

                catch (Exception e)
                {
                    err = e.InnerException.InnerException.ToString();
                }
            }
            if (err != "")
                return Ok(err);
            else
                return Ok(user);
        }

        [HttpPut("UpdateUser")]        
        public ActionResult UpdateUser(ResetPassData fetchedUser)
        {
            var db = new soamanaDB();
            ResetCredential rc = new ResetCredential();
            dynamic json = fetchedUser;
            string token = json.Token;
            string userName = json.UserName;
            string pass = json.Password;

            rc = db.ResetCredentials.Where(x => x.TokenHash == token && x.TokenUsed == false).OrderByDescending(x => x.ExpirationDate).FirstOrDefault();

            if ((rc.ExpirationDate.Value.Date).Subtract(DateTime.Now.Date).Days <= 1 && rc.TokenHash.ToString() == token && rc.TokenUsed == false)
            {
                User user = new User();
                user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();

                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                user.Password = savedPasswordHash;
                rc.TokenUsed = true;

                db.SaveChanges();
            }
            return Ok("OK");
        }
        

    }
}