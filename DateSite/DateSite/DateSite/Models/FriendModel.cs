using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;

namespace DateSite.Models
{
    public class FriendModel
    {
        public string username { get; set; }
        public string age { get; set; }
        public string id { get; set; }
        public string gender { get; set; }
    }

    public class FriendListModel
    {
        public List<UserModel> friends = new List<UserModel>();
        public List<UserModel> requests = new List<UserModel>();
    }
}