using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{

    public class EventSearch
    {
        public string ticker { get; set; }
        public string short_name { get; set; }
        public string id { get; set; }
        public string rd { get; set; }
        public string reg_date { get; set; }
        public string name { get; set; }
        public string reestr_event_id { get; set; }
        public string ec_headline { get; set; }
        public string ec_date { get; set; }
        public string ECID { get; set; }
        public string ce_headline { get; set; }
        public string CEID { get; set; }
        public string ce_date { get; set; }
        public string enr_news { get; set; }
        public string ec_news { get; set; }
        public string ce_news { get; set; }
        public int event_count { get; set; }
    }

        public class EventTypes
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    
}