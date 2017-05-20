using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.ProfileIP
{
    public class ProfileModel
    {
    }

    public class OkvedCls
    {
        public OkvedCls()
        {
        }

        public OkvedCls(string k, string nm, int v)
        {
            this.kod = k;
            this.name = nm;
            this.vis = v;
        }

        public string kod { get; set; } //="34.10.2" 
        public string name { get; set; } //="Производство легковых автомобилей" 
        //public int cnt { get; set; } //="23" 
        public int vis { get; set; } //="1" 
    }

    public class GorodaCls
    {
        public GorodaCls()
        { }

        public GorodaCls(int so, string nm, string fn)
        {
            this.soato = so;
            this.name = nm;
            this.gerb = fn;
        }
        public int soato { get; set; } //="36440000" 
        public string name { get; set; } //="Тольятти"
        public string gerb { get; set; } //fn
    }

    public class ZakupkiData
    {
        public int type { get; set; }
        public long? customer_cnt { get; set; }
        public decimal? customer_sum { get; set; }
        public long? supplier_cnt { get; set; }
        public decimal? supplier_sum { get; set; }
        public long? participiant_cnt { get; set; }
        public decimal? participiant_sum { get; set; }
    }

    public class PravoData
    {
        public long? applicant_cnt { get; set; }
        public decimal? applicant_sum { get; set; }
        public long? defendant_cnt { get; set; }
        public decimal? defendant_sum { get; set; }
    }

    public class UnfairData
    {
        public string rec_id { get; set; }
        public string start_date { get; set; }
    }

}