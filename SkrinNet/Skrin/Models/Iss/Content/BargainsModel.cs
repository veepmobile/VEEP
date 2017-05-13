using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class BargainsSearch
    {
        public string iss { get; set; }
        public string dfrom { get; set; }
        public string dto { get; set; }  
        public string types { get; set; } 
        public int page { get; set; }
    }

    public class Bargain_types
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class BargainSearchResult
    {
        public int count { get; set; }
        public List<BargainSearchItem> Items { get; set; }
    }

    public class BargainSearchItem
    {
        public string total { get; set; }
        public string id { get; set; }
        public string file_name { get; set; }
        public string reg_date { get; set; }
        public string name { get; set; }
    }

    public class BargainsModel
    {
        public BargainsModel(string issuer_id)
        {
            this.issuer_id = issuer_id;
        }
        public string issuer_id { get; set; }
    }


}