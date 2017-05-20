using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Authentication
{
    public class ImportCodes
    {
        public int id { get; set; }
        public string name { get; set; }
        public CodeType code_type { get; set; }
        public bool branch_exclude { get; set; }

        public string codes { get; set; }

        
    }

    public enum CodeType
    {
        Inn = 1,
        Ogrn = 2,
        Ticker = 3
    }

   
}