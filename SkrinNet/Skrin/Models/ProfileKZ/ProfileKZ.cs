using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.ProfileKZ
{
    public class ProfileKZ
    {
        public ProfileKZ()
        {
            timeouts = new Dictionary<string, long>();
        }


        public int user_id { get; set; }
        public int data_access { get; set; }
        public string name { get; set; }
        public string Code { get; set; }

        public string fullname { get; set; }
        public string name_short { get; set; }
        public string area { get; set; }
        public string fulladdress { get; set; }
        public string phone { get; set; }
        public string main_deal { get; set; }
        public string stat { get; set; }
        public string reg_number { get; set; }
        public string address { get; set; }
        public string updatedate { get; set; }

        /// <summary>
        /// Функция запрета доступа
        /// </summary>
        public string denied_access_function { get; set; }

        public Dictionary<string, long> timeouts { get; set; }
    }

    public class ProfileKZCodes
    {
        public string r_number { get; set; }
        public string r_date { get; set; }
        public string r_date2 { get; set; }
        public string code { get; set; }
        public string kato { get; set; }
        public string bin { get; set; }
        public string ownership { get; set; }
        public string ectype { get; set; }
        public string size { get; set; }
        public string updatedate { get; set; }
        public Dictionary<string, string> OKEDS { get; set; }
        public string kato_name { get; set; }
    }

    public class ProfileKZEmployments
    {
        public string date { get; set; }
        public string pcount { get; set; }
    }

    public class ProfileKZControls
    {
        public string Manager { get; set; }
        public List<DateReps> dates { get; set; }
    }

    public class DateReps
    {
        public string daterep { get; set; }
        public int cur { get; set; }
    }


    //public class ProfileKZData
    //{
    //    public string fullname { get; set; }
    //    public string name_short { get; set; }
    //    public string area { get; set; }
    //    public string fulladdress { get; set; }
    //    public string phone { get; set; }
    //    public string main_deal { get; set; }
    //    public string stat { get; set; }
    //    public string reg_number { get; set; }
    //    public string address { get; set; }
    //    public string updatedate { get; set; }

    //}

}