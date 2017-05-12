using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class DealInfo
    {
        public string Header { get; set; }

        public HtmlString Body { get; set; }

        public List<Period> Periods { get; set; }

        public DealInfoTypes InfoType { get; private set; }

        public string SelectedPeriod { get; set; }

        public string Ticker { get; private set; }

        public DealInfo(DealInfoTypes type,string ticker)
        {
            InfoType = type;
            Periods = new List<Period>();
            Ticker = ticker;
        }
    }


    public class Period
    {
        public string ShowDate { get; set; }
        public string ValueDate { get; set; }
    }

    public enum DealInfoTypes
    {
        Main,Plans
    }
}