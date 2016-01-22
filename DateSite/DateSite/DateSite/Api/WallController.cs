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
        public IEnumerable<WallpostModel> Get(string idd)
        {
            using (var context = new UserDBEntities())
            {
                List<WallpostModel> temppost = new List<WallpostModel>();
                int id = Int32.Parse(idd);
                var i = 0;
                var posts = (from a in context.WALLPOST
                             where (a.PID == id)
                             select a);
                foreach (var r in posts)
                {
                    WallpostModel post = new WallpostModel();
                    post.postid = r.POSTID;
                    post.authorid = r.FID;
                    post.post = r.POST;
                    post.walluserid = r.PID;
                    temppost[i] = post;
                    i++;

                }
                return temppost;
            }
        }

        //    public void Set(int wallid, int postid, string post)
        //{
        //    using (var context = new UserDBEntities())
        //    {
        //        WALLPOST newpost = new WALLPOST();
        //        newpost.PID = wallid;
        //        newpost.FID = postid;
        //        newpost.POST = post;
        //        newpost.TIME = DateTime.Now;
        //        context.WALLPOST.Add(newpost);
        //    }
            

        //}
        


    }
}
