using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Web.Security;
using Customer_Service_Admin_Panel.Model;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class HomeController : Controller
    {
        private CustomerServiceEntity dbEntity = new CustomerServiceEntity();

        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult UserDetail()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = dbEntity.Users.FirstOrDefault(s=>s.Email==user.Email);

                if(check == null)
                {
                    dbEntity.Configuration.ValidateOnSaveEnabled = false;
                    dbEntity.Users.Add(user);
                   
                    dbEntity.SaveChanges();
                   
                    return RedirectToAction("Login", "Home");

                }
            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(User user)
        {
            if (IsAuthenticatedUser(user.Email, user.HasedPassword))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Authentification fail");
            }

                return View(user);

        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Home");
        }


        private bool IsAuthenticatedUser(string email , string password)
        {
            bool IsValid = false;

                int count = dbEntity.Users.Where(a=> a.Email == email && a.HasedPassword == password).Count();

                if (count > 0)
                {
                    IsValid = true;
                }
            
            return IsValid;
        }

        private bool IsEmailExisting(string email)
        {
            bool IsEmail = false;

            int count = dbEntity.Users.Where(a=>a.Email == email).Count();

                if(count > 0)
                {
                    IsEmail = true;
                }
            
            return IsEmail;
        }
    }

}