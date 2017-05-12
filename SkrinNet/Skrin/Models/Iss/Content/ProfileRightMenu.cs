using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class ProfileRightMenu
    {

        public string ogrn { get; set; }        
        public int group_id { get; set; }
        public string issuer_id { get; set; }
        public string inn { get; set; }
        public int? opf { get; set; }
        public bool uniq { get; set; }
        public string ticker { get; private set; }
        public bool egrul_is_possible { get; set; }
        /// <summary>
        /// Возможно ли сформировать выписку по данной компании
        /// </summary>
        public bool egrul_is_exists { get; set; }
        public string legal_address { get; set; }
        /// <summary>
        /// Существует сегодняшняя выписка pdf
        /// </summary>
        public bool egrul_today_exists { get;set; }

        public List<EGRULPDF> EGRULPDF { get; set; }
        public List<UserReport> URep { get; set; }
        public List<Choice> choice { get; set; }
        public ZakupkiData zak_data { get; set; }
        public UnfairData unf_data { get; set; }
        public PravoData pravo_data { get; set; }
        public List<string> monoploist_regions { get; set; }

        public int? subsids_count { get; set; }
        public int? branch_count { get; set; }

        public Dictionary<string, long> timeouts { get; set; }


        public ProfileRightMenu(string ticker)
        {
            timeouts = new Dictionary<string, long>();
            this.ticker = ticker;
        }

    }

    public class EGRULPDF
    {

        public EGRULPDF(string d, string ds, string os, string is_pdf)
        {

            this.dt = d;
            this.datestring = ds;
            this.ogrn = os;
            this.is_pdf = is_pdf;
        }

        public string dt { get; set; }
        public string datestring { get; set; }
        public string ogrn { get; set; }
        public string is_pdf { get; set; }
    }

    public class UserReport
    {
        public UserReport(string i, string d, string f)
        {
            this.id = i;
            this.dt = d;
            this.filename = f;
        }
        public string id { get; set; }
        public string dt { get; set; }
        public string filename { get; set; }
    }

    public class Choice
    {
        public Choice(int id, string name, bool selected, bool disabled)
        {
            this.id = id;
            this.name = name;
            this.selected = selected;
            this.disabled = disabled;
        }
        public int id { get; set; } //="1" 
        public string name { get; set; } //="Профиль" 
        public bool selected { get; set; } //="1" 
        public bool disabled { get; set; } //="1"
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

    public class UnfairData
    {
        public string rec_id { get; set; }
        public string start_date { get; set; }
    }

    public class PravoData
    {
        public long? applicant_cnt { get; set; }
        public decimal? applicant_sum { get; set; }
        public long? defendant_cnt { get; set; }
        public decimal? defendant_sum { get; set; }
    }

}