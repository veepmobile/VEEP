using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Analytics
{
    public class rating
    {
        public int position { get; set; }
        public string issuer_id { get; set; }
        public string ticker { get; set; }
        public string short_name { get; set; }
        public string LocalRating { get; set; }
        public string InternationalRating { get; set; }
        public string ForeCast { get; set; }
        public string rd { get; set; }
    }
}