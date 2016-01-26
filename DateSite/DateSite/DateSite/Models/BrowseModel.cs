using Repositories;
using System.Collections.Generic;

namespace DateSite.Models
{
    public class BrowseModel
    {
        public List<UserModel> profiles { get; set; }
        public List<string> countries { get; set; }
        public List<UserModel> randomProfiles { get; set; }
    }
}