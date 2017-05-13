using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class NRARating
    {
        public string LocalRating { get; set; }
        public string InternationalRating { get; set; }
        public string ForeCast { get; set; }
        public string Rating_Date { get; set; }

        public int RatingTypeID { get; set; }
    }
}