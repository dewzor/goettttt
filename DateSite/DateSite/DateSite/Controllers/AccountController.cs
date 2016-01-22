using DateSite.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DateSite.Controllers
{
    public class AccountController : Controller
    {

        public UsersRepository _usersRepository = new UsersRepository();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var completed = true;

            if (!completed)
            {
                ModelState.AddModelError("", "Fel fan");
                return View();
            }

            Profiles profile = new Profiles();
            profile.About = model.About;
            profile.Age = Int32.Parse(model.Age);
            profile.Email = model.Email;
            profile.Gender = model.Gender;
            profile.Lastname = model.Lastname;
            profile.Firstname = model.Firstname;

            SECURITY security = new SECURITY();
            security.USERNAME = model.Username;
            security.PASSWORD = model.Password;
            security.VISIBILITY = true;

            _usersRepository.insertUser(profile, security);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {

            SECURITY _user = new SECURITY();
            _user.USERNAME = user.Username;
            _user.PASSWORD = user.Password;
            var usr = _usersRepository.loginUser(_user);

            if (usr != null)
            {
            Session["UserID"] = usr.PID.ToString();
            Session["Username"] = usr.USERNAME.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is invalid.");
            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                ViewData["LoginError"] = "Could not login. Incorrect credentials.";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}