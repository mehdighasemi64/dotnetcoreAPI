using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;
using System.Security.Cryptography;
using System.Collections.Generic;


namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("AllUsers")]
        public List<User> AllUsers()
        {
            var db = new soamanaDB();
            var result = db.Users.ToList();
            return (result);
        }

        [HttpDelete("DeleteEducation")]
        public ActionResult DeleteEducation([FromBody] int userId)
        {
            using (var db = new soamanaDB())
            {
                User user = new User();
                user = db.Users.FirstOrDefault(x => x.UserId == userId);
                db.Users.Remove(user);                                
                db.SaveChanges();
            }
            return Ok(User);
        }

        [HttpPost("RegisterUser")]
        public ActionResult RegisterUser([FromBody] User user)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var result = user;
            string err = "";
            using (var db = new soamanaDB())
            {
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                user.Password = savedPasswordHash;
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    try
                    {
                        Email.SendEmail(user.Email, "Welcome To Somana Shop", "Your credential for login to somana is: <br/> <span>UserName:</span>" +
                        user.UserName + "<br/> <span> Password: </span>" + user.Password + "<br/> <span> <a href= " + "http://shop.somana.ir" + ">Somana Shop</a></span>");
                    }
                    catch (Exception e)
                    {
                        err = e.Message;
                    }
                }
                catch (Exception e)
                {
                    if (e.InnerException.InnerException.ToString().Contains("IX_Users"))
                        err = "somone got this user";
                }

            }
            if (err != "")
                return Ok(err);
            else
                return Ok(result);
        }

        [HttpPut("LoginUser")]
        public ActionResult LoginUser([FromBody] User user)
        {
            var db = new soamanaDB();
            var result = db.Users.Where(x => x.UserName == user.UserName).ToList();

            string savedPasswordHash = result[0].Password;

            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 100000);

            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    result = null;
            return Ok(result);
        }

        [HttpPatch("UpdateUser")]
        public ActionResult UpdateUser([FromBody] User fbuser)
        {
            var db = new soamanaDB();
            User user = new User();
            user = db.Users.Where(x => x.UserId == fbuser.UserId).FirstOrDefault();
            user.UserId = fbuser.UserId;
            user.UserName = fbuser.UserName;
            user.FirstName = fbuser.FirstName;
            user.LastName = fbuser.LastName;
            user.Mobile = fbuser.Mobile;
            user.Email = fbuser.Email;
            user.Password = fbuser.Password;
            user.Address = fbuser.Address;
           // db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("OK");
        }      
    }
}
