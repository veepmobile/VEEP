using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Company
{
    public class Ask
    {
        public List<int> themes { get; set; }
        public AskInfo info { get; set; }
        public bool subscribe { get; set; }
    }

    public class AskInfo
    {
        public string login { get; set; }
        public string company { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
    }

    public class FeedbackCall
    {
        public AskInfo info { get; set; }
        public string theme { get; set; }
    }


 

}