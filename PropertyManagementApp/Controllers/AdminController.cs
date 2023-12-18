using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PropertyManagementApp.Models;
using System.Data.Entity;

namespace PropertyManagementApp.Controllers
{
    public class AdminController : Controller
    {
        private PRMDBEntities1 PRMDBEntities = new PRMDBEntities1();


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {

            List<User> users = PRMDBEntities.Users.ToList();

            return View(users);


        }

       

        // Get CreateManagerUser View
        public ActionResult CreateManagerUser()
        {
            return View();
        }

        // Get CreateManagerUser View
        public ActionResult CreateTenentUser()
        {
            return View();
        }

        // Post CreateManagerUser
        [HttpPost]
        public ActionResult CreateManagerUser(User user)
        {
            if (PRMDBEntities.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("Username", "This Username is already taken. Please choose a different one.");
            }
            if (ModelState.IsValid)
            {
                user.UserType = "Manager";
                PRMDBEntities.Users.Add(user);
                PRMDBEntities.SaveChanges();
                return RedirectToAction("Users");
            }
            return View();
        }

        // Post CreateTenantUser
        [HttpPost]
        public ActionResult CreateTenentUser (User user)
        {
            if (PRMDBEntities.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("Username", "This Username is already taken. Please choose a different one.");
            }
            if (ModelState.IsValid)
            {
                user.UserType = "Tenant";
                PRMDBEntities.Users.Add(user);
                PRMDBEntities.SaveChanges();
                return RedirectToAction("Users");
            }
            return View();
        }


        // Ediut User
        public ActionResult Edit(int id)
        {
            var data = PRMDBEntities.Users.Where(x => x.UserId == id).FirstOrDefault();
            return View(data);
        }

        //
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var data = PRMDBEntities.Users.Where(x => x.UserId == user.UserId).SingleOrDefault();
            if(data.Password == user.Password)
            {
                ModelState.AddModelError("Password", "You enter your old Password. Please choose a different one.");
                return View(user);
            }

            
            data.Password = user.Password; 
            PRMDBEntities.Entry(data).State = EntityState.Modified;
            PRMDBEntities.SaveChanges();

                
            
            return RedirectToAction("Users");



        }



        public ActionResult Details(int id)
        {
            var data = PRMDBEntities.Users.Where(x => x.UserId == id).SingleOrDefault();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = PRMDBEntities.Users.Where(x => x.UserId == id).SingleOrDefault();
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteUser(int id)
        {
            var data = PRMDBEntities.Users.Where(x => x.UserId == id).SingleOrDefault();

            PRMDBEntities.Users.Remove(data);
            PRMDBEntities.SaveChanges();

            return RedirectToAction("Users");
        }

        




   









        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}