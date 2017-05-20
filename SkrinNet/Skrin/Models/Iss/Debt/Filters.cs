using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Debt
{
    public class Filters
    {
        public string DebtorName { get; set; }
        public string FullName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string NumProizv { get; set; }
        public string PristavName { get; set; }
        public string Predmet { get; set; }
        public string RegionID { get; set; }
        public int Page { get; set; }
    }
}