using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DateSite.Models
{
    public class WallModel
    {
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Incorrect Characters")]
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