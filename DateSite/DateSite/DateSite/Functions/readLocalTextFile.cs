using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DateSite.Functions
{
    public class readLocalTextFile
    {

        public List<string> getCountries()
        {
            List<string> countries = new List<string>();

            var location = AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Content/Text/countries.cntry";

            var lines = File.ReadLines(location);
            foreach (var line in lines)
            {
                countries.Add(line);
            }

            return countries;
        }
    }
}