using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateSite.Models
{
    public class WallModel
    {
        public string post { get; set; }
        public DateTime time { get; set; }
        public int authorid { get; set; }
        public string authoridstring { get; set; }
        public int walluserid { get; set; }
        public string walluseridstring { get; set; }
        public int postid { get; set; }
        public string authorname { get; set; }
        
    }
}