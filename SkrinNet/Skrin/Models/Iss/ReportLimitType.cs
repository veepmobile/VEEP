using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class UserReportLimit
    {
        public ReportLimitType Type { get; set; }
        public int DayLimit { get; set; }
        public int MonthLimit { get; set; }

        public bool CanReport
        {
            get { return DayLimit > 0 && MonthLimit > 0; }
        }
    }


    public enum ReportLimitType
    {
        Egrul=1,Egrip=2
    }
}