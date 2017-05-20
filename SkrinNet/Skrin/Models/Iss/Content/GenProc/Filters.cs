using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content.GenProc
{
    public class Filters
    {
        public string SearchName { get; set; }
        public string INN { get; set; }
        public string OGRN { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Purpose { get; set; }
        public string Organ { get; set; }
    }
}