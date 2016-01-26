using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repositories;
using DateSite.Models;

namespace DateSite.Controllers
{
    public class WallController : ApiController
    {
        UsersRepository _usersRepository = new UsersRepository();
        // GET api/wall
        [HttpGet]
        public IEnumerable<WallModel> Get(string id)
        {
            using (var context = new UserDBEntities())
            {
                List<WallModel> temppost = new List<WallModel>();
                int idd = Int32.Parse(id);
                var posts = (from a in context.WALLPOST
                             where (a.PID == idd)
                             select a);
                foreach (var r in posts)
                {
                    WallModel post = new WallModel();
                    post.postid = r.POSTID;
                    post.authorid = r.FID;
                    post.post = r.POST;
                    post.walluserid = r.PID;
                    var poster = _usersRepository.getUserByID(post.authorid);
                    post.authorname = poster.Firstname;
                    temppost.Add(post);
                }
                return temppost;
            }
        }
        // SET api/wall
        [HttpPost]
        public void Post(WallModel post)
        {
            using (var context = new UserDBEntities())
            {
                WALLPOST newpost = new WALLPOST();
                newpost.PID = Convert.ToInt32(post.walluseridstring); // PID är id för användaren vars wall posten postas till.
                newpost.FID = Convert.ToInt32(post.authoridstring); // Friendid som postar posten.
                if(post.post.Length < 1000)
                newpost.POST = post.post;  // ..
                try
                {
                    context.WALLPOST.Add(newpost);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



    }
}
