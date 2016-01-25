using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DateSite.Models;
using Repositories;

namespace DateSite.Controllers
{
    public class FriendController : Controller
    {
        private FriendRepository _friendRepository = new FriendRepository();
        private UsersRepository _usersRepository = new UsersRepository();
        
        // GET: Friend
        public ActionResult Friendlist(int id)
        {
            FriendListModel data = new FriendListModel();
            var friends = _friendRepository.getFriends(id);
            foreach(var friend in friends)  // Bygger upp vännerlistan för användaren.
            {
                data.friends.Add(_usersRepository.getUserByID(friend));
            }
            var frequests = _friendRepository.getFRequests(id);
            foreach (var friend in frequests) // Bygger upp friendrequestlistan för användaren.
            {
                data.requests.Add(_usersRepository.getUserByID(friend));
            }
            

            return View(data);
        }

        public ActionResult FriendlistAccept(int idd, int friendid)
        {
            int? inputid = idd;
            _friendRepository.addFriend(idd, friendid);
            return RedirectToAction("Friendlist", new { id = inputid });
        }

        public ActionResult FriendlistDeny(int idd, int friendid)
        {
            int? inputid = idd;
            _friendRepository.DenyFriend(idd, friendid);
            return RedirectToAction("Friendlist", new { id = inputid });
        }

        public ActionResult Numberfriends(int id)
        {
            var frequests = _friendRepository.getFRequests(id);
            var totalreq = frequests.Count.ToString();
            return Content(totalreq);
        } 
    }
}