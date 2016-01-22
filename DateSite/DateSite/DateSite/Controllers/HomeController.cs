using DateSite.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DateSite.Controllers
{
    public class HomeController : Controller
    {
        ManageRepository _manageRepository = new ManageRepository();
        UsersRepository _usersRepository = new UsersRepository();
        BrowseModel data = new BrowseModel();

        public ActionResult Index()
        {
            data.randomProfiles = _usersRepository.getRandomProfiles();
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewProfile()
        {
            ProfileModel profile = new ProfileModel();
            var userid = Convert.ToInt32(Session["UserID"]);
            profile.about = _manageRepository.getPAboutById(userid);
            profile.visible = _manageRepository.getHide(userid);
            profile.userid = userid;
            profile.pic = _manageRepository.getPic(userid);
            profile.name = _manageRepository.getName(userid);
            

            return View(profile);
        }
    }
}