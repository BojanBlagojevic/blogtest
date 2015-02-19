using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogZadatak.Controllers
{
    public class UserController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Login(BlogZadatak.Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect");
                }
            }
            
            return View(user);
        }

        [HttpGet]
        public ActionResult Registration()
        {
           
            return View();
        }
        
        [HttpPost]
        public ActionResult Registration(BlogZadatak.Models.UserModel user)
        {

            if (ModelState.IsValid)
            {
                using (var db = new MainDbEntities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(user.Password);

                    var sysUser = db.SystemUsers.Create();

                    sysUser.UserName = user.UserName;
                    sysUser.Password = encrpPass;
                    sysUser.PasswordSalt = crypto.Salt;
                    sysUser.Userid = Guid.NewGuid();

                    db.SystemUsers.Add(sysUser);
                    db.SaveChanges();


                    return RedirectToAction("Index", "Category");
                }

            }
             
            
            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string username, string pasword )
        {
            var crypto = new SimpleCrypto.PBKDF2();
           
            bool isvalid = false;

            using (var db = new MainDbEntities())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    if (user.Password == crypto.Compute(pasword, user.PasswordSalt))
                    {
                        isvalid = true;
                    }
                    
                }
            }


            return isvalid;
        }
    }
}
