using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Analytics
{
    public class SearchObject
    {
        public string dBeg { get; set; }
        public string dEnd { get; set; }
        public string anal_types { get; set; }
        public bool anal_types_excl { get; set; }
        public string author_search { get; set; }
        public string text_search { get; set; }
        public int? page_no { get; set; }
        public int? r_count { get; set; }
    }

    public class Result
    {
        public int total { get; set; }
        public List<SearchResult> s_result { get; set; }
    }

    public class SearchResult
    {
        public string id { get; set; }
        public string headline { get; set; }
        public string doc_id { get; set; }
        public string date { get; set; }
        public bool? is_daily { get; set; }
        public string file_name { get; set; }
        public string author_id { get; set; }
        public string author_name { get; set; }
        public string a_type { get; set; }
        public Int16 pages { get; set; }
        public long no { get; set; }
    }

    public class MenuTypes
    {
        public int id { get; set; }
        public string industry_id { get; set; }
        public bool? is_daily { get; set; }
        public bool? is_industry { get; set; }
    }

    public class ReviewTypes
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string name { get; set; }
    }
}