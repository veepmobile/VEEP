using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.ProfileFL
{
    public class ProfileFLModel
    {
        public ProfileFLModel()
        {
            isBancruptcy = false;
        }
        public string fio { get; set; }
        public string inn { get; set; }
        public int user_id { get; set; }
        public bool isBancruptcy { get; set; }
        public List<IPData> ip_inn { get; set; }
        public List<IPData> ip_fio { get; set; }
        public List<FLData> founder_inn { get; set; }
        public List<FLData> founder_fio { get; set; }
        public List<FLData> manager_inn { get; set; }
        public List<FLData> manager_fio { get; set; }
    }
    public class IPData
    {
        public string fio { get; set; }
        public string subrf { get; set; }
        public string ogrnip { get; set; }
        public string inn { get; set; }
        public string gd { get; set; }
        public string sd { get; set; }
        public string id { get; set; }
    }
    public class FLData
    {
       public string fio { get; set; }
       public string share { get; set; }
       public string share_percent { get; set; }
       public string position { get; set; }
       public string gd { get; set; }
       public string ul_name { get; set; }
       public string ogrn { get; set; }
       public string ul_status { get; set; }
       public string ticker { get; set; }
       public string remark { get; set; }
    }
}