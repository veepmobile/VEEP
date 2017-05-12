using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class TradeInfo
    {
        public Dictionary<int, string> Headers { get; set; }

        public List<TradeInfoItem> Items { get; set; }
    }


    public class TradeInfoItem
    {
        public string RegDate { get; set; }
        public string Code { get; set; }
        public string TradePlaceName { get; set; }
        public int IssueTypeId { get; set; }

        public Dictionary<int, string> Values { get; set; }

        public TradeInfoItem(int val_counts)
        {
            Values = new Dictionary<int, string>();
            for (int i =   1; i <= val_counts; i++)
            {
                Values.Add(i, "");
            }
        }
    }


    public class TradeInitValues
    {
        public string minrd { get; set; }
        public string maxrd { get; set; }

        public string dstart { get; set; }
        public string dend { get; set; }

        public List<InitVal> exchange_list { get; set; }

        public List<InitVal> issues_list { get; set; }
    }


    public class TradeSearchObject
    {
        public string ticker {get;set;} 
        public string sDate {get;set;} 
        public string tDate {get;set;}
        public int currency {get;set;}
        public string exchange_list {get;set;}
        public string issues_list { get; set; }
    }


    public enum Currency
    {
        RUB=1,USD=2
    }

    public enum Exchange
    {
        RTS=101,
        MICEX=2,
        RTSBoard=10,
        LSE=11
    }

    public enum IssueType
    {
        OrdinaryShare=1,
        PreferenceShare=2,
        Bond=3,
        DepositaryReceipt=4 
    }


}