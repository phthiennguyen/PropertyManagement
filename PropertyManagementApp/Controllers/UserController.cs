using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagementApp.Models;
using System.Web.Security;

namespace PropertyManagementApp.Controllers
{
    public class UserController : Controller


    {
        private PRMDBEntities1 entities = new PRMDBEntities1();


        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            bool isExist = entities.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password);
            User u = entities.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (isExist)
            {
                FormsAuthentication.SetAuthCookie(u.UserId.ToString(), false);

                switch (u.UserType)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");

                    case "Manager":
                        return RedirectToAction("Index", "Manager");
                        

                    case "Tenant":
                        return RedirectToAction("Index", "Tenant");
                        
                }



                
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("UserName", "Username or Password is wrong");
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User u)
        {
            if(entities.Users.Any(x => x.UserName == u.UserName))
            {
                ModelState.AddModelError("UserName", "Username is alreay taken. Please choose a different one");
                return View(u);
            }
            if (ModelState.IsValid)
            {
                u.UserType = "Tenant";
                entities.Users.Add(u);
                entities.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }

            return View(u);



           
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        
    }
}