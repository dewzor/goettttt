using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;
using System.ComponentModel.DataAnnotations;

namespace DateSite.Models
{
    public class ProfileModel
    {
        public string name { get; set; }
        public int age { get; set; }
        public string about { get; set; }
        public string email { get; set; }
        public bool visible { get; set; }
        public string pic { get; set; }
        public int userid { get; set; }
        public List<string> wallposts { get; set; }

        public HttpPostedFileBase File { get; set; } // Följande tre är för form i Manage/profile
        [MaxLength(1000, ErrorMessage = "Too long message")]
        public string Aboutbox { get; set; }
        public bool Visibility { get; set; }

    }
}