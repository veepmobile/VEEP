using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class CompanyShortInfo
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public string update_date { get; set; }
        public string issuer_id { get; set; }
        public int type_id { get; set; }

        /*extra fields*/
        public string address { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string okpo { get; set; }
        public string ruler { get; set; }
        public string phone { get; set; }
        public string www { get; set; }
        public string city { get; set; }
    }
}