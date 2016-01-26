using System.Web.Mvc;
using Repositories;
using DateSite.Functions;
using DateSite.Models;
using System;

namespace DateSite.Controllers
{
    public class BrowseProfilesController : Controller
    {
        private UsersRepository _usersRepository;
        public readLocalTextFile reader = new readLocalTextFile();
        private FriendRepository _friendRepository = new FriendRepository();

        public BrowseProfilesController()
        {
            _usersRepository = new UsersRepository();
        }

        /// <summary>
        /// browsar alla users i databasen som inte är invisible
        /// </summary>
        /// <returns></returns>
        public ActionResult Browse()
        {
            BrowseModel browseData = new BrowseModel();
            browseData.profiles = _usersRepository.fetchProfiles();
            browseData.countries = reader.getCountries(); 
            return View(browseData);
        }


        /// <summary>
        /// söker efter användare beroende på inmatat data
        /// </summary>
        /// <param name="searchtext"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Browse(string searchtext)
        {
            BrowseModel browseData = new BrowseModel();
            browseData.profiles = _usersRepository.findProfilesByName(searchtext);  //kollar om användaren finns 
            browseData.countries = reader.getCountries();
            return View(browseData);
        }


        /// <summary>
        /// view en user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BrowseUser(int id)
        {
            
            UserModel user = _usersRepository.getUserByID(id);
            ProfileModel profile = new ProfileModel();
            profile.about = user.About;
            profile.age = user.Age;
            profile.email = user.Email;
            profile.pic = user.Pic;
            profile.name = user.Firstname + user.Lastname;
            profile.userid = user.Id;
            return View(profile);
        }


        /// <summary>
        /// kollar på en användare, om man ej är inloggad så redirectas man till registersidan
        /// </summary>
        /// <param name="idd"></param>
        /// <returns></returns>
        public ActionResult BrowseUserRandom(int idd)
        {
            int? inputid = idd;
            var userid = Session["UserID"];
            if (userid != null)
                return RedirectToAction("BrowseUser", new { id = inputid });
            else
                return RedirectToAction("Register", "Account");
        }


        /// <summary>
        /// lägger till en vän
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        public ActionResult AddFriend(int id, int friendid)
        {
            _friendRepository.SendFRequest(id, friendid);
            return RedirectToAction("Browseuser", new { id = friendid });
        }
    }
}