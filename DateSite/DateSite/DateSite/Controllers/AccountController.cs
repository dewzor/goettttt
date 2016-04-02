using DateSite.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DateSite.Controllers
{
    public class AccountController : Controller
    {

        public UsersRepository _usersRepository = new UsersRepository();
        BrowseModel data = new BrowseModel();

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

            //settar en profilemodell för insertion i databasen
            Profiles profile = new Profiles();
            profile.About = model.About;
            profile.Age = Int32.Parse(model.Age);
            profile.Email = model.Email;
            profile.Gender = model.Gender;
            profile.Lastname = model.Lastname;
            profile.Firstname = model.Firstname;

            //settar en securitymodell för insertion i databasen
            SECURITY security = new SECURITY();
            security.USERNAME = model.Username;
            security.PASSWORD = model.Password;
            security.VISIBILITY = true;

            _usersRepository.insertUser(profile, security);  //lägger till användare i databasen

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {

            SECURITY _user = new SECURITY(); //en securitymodell skapas, där användarens security details lagras
            _user.USERNAME = user.Username;
            _user.PASSWORD = user.Password;
            var usr = _usersRepository.loginUser(_user);  //kollar i databas om tupel med användarnamn och lösenord matchar

            if (usr != null)
            {
            Session["UserID"] = usr.PID.ToString();  //initierar sessionvariabel för senare användning
            Session["Username"] = usr.USERNAME.ToString(); //initierar sessionvariabel för senare användning
                FormsAuthentication.SetAuthCookie(usr.PID.ToString(), false);  //skapar en authentication ticket
                return RedirectToAction("Profile", "Manage", new { @ID = usr.PID }); //redirectar till profileaction i controllern och skickar med id för användaren
            }
            else
            {
                @ViewData["LoginError"] = "Incorrect login details";
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

        [HttpGet]
        public ActionResult Login(string lang)
        {  
            if (lang != null)  //settar culture   //////////////
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);  
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }

            return View();
        }

        public ActionResult Login()
        {
            data.randomProfiles = _usersRepository.getRandomProfiles();
            return View(data);
        }

        /// <summary>
        /// clearar session variable och dödar authenticationcookie
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Language(string lang)
        {
            var cookie = new HttpCookie("lang", lang) { HttpOnly = true };

            Response.AppendCookie(cookie);
            return RedirectToAction("Login", "Account", new { culture = lang });

        }
    }
}