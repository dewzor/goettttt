using Repositories;
using System.Collections.Generic;

namespace DateSite.Models
{
    public class BrowseModel
    {
        public List<Profiles> profiles { get; set; }
        public List<string> countries { get; set; }
        public List<Profiles> randomProfiles { get; set; }
    }
}