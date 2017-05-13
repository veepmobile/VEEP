using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class Profile
    {
        public Profile()
        {
            timeouts = new Dictionary<string, long>();
        }

        public string name { get; set; }
        public string short_name { get; set; }
        public string legal_address { get; set; }
        public string legal_phone { get; set; }
        public string legal_fax { get; set; }
        public string legal_email { get; set; }
        public string www { get; set; } 
        public string inn { get; set; } 
        public string ogrn { get; set; }
        public string gks_id { get; set; }
        public string ogrn_date { get; set; }
        public string reg_org { get; set; }
        public string fcsm_code { get; set; }
        public string first_okved { get; set; } 
        public string region { get; set; }
        public int type_id { get; set; }
        public int id { get; set; }
        public string okato { get; set; }
        public int? opf { get; set; }
        public string issuer_id { get; set; }
        public bool uniq { get; set; }
        public int group_id { get; set; }
        public string okpo { get; set; }
        public string kpp { get; set; }        
        public string ul2_ogrn { get; set; }
        public string ticker { get; set; }
        public string src { get; set; }
        public string logo_path { get; set; }
        public string eng_name { get; set; }
        public bool is_branch { get; set; }
        public string mass_reg { get; set; }
        public string disqual { get; set; }
        public string bad_addr { get; set; }

        public string free_egrul_status { get; set; }
        public string gks_stat { get; set; }

        /// <summary>
        /// Акции обыкновенные - цена последней сделки
        /// </summary>
        public string aoi { get; set; }
        /// <summary>
        /// Акции привилегированные - цена последней сделки
        /// </summary>
        public string api { get; set; }

        public string state_capital { get; set; }

        public int iss_type { get; set; }

        /// <summary>
        /// Для компании можно расчитать коэффициенты
        /// </summary>
        public bool has_factors { get; set; }


        public List<RulerData> ruler_list { get; set; }


        public List<EgrulManagerHistoryData> egrul_man_hist { get; set; }
        public List<EgrulNameHistoryData> egrul_name_hist { get; set; }
        public List<EgrulAddressHistoryData> egrul_addr_hist { get; set; }
        public List<EgrulCoownerHistoryData> egrul_coowner_hist { get; set; }
        public Dictionary<string, long> timeouts { get; set; }

        public Goroda gorod { get; set; }
        public List<Okved> okveds { get; set; }
        public List<MainBalance> main_balance { get; set; }

        public List<ConstGKS> consts { get; set; }
        public List<ConstEgrul> const_egrul { get; set; }

        public string const_egrul_share { get; set; }
        public string const_egrul_grn { get; set; }
        public string const_egrul_grn_date { get; set; }
        public string const_egrul_part { get; set; }
        public string const_egrul_part_ei { get; set; }
        public string const_egrul_ex_date { get; set; }

        public RSMPData RsmpData { get; set; }
    }

    public class RSMPData
    {
        public string IncludeDate { get; set; }
        public string IsNew { get; set; }
        public string SubjectType { get; set; }
    }

    public class EgrulManagerHistoryData
    {
        public string ds { get; set; }
        public string name { get; set; }
        public string fio { get; set; }
        public string inn { get; set; }
    }

    public class EgrulNameHistoryData
    {
        public string ds { get; set; }
        public string fullName { get; set; }
        public string shortName { get; set; }
    }

    public class EgrulAddressHistoryData
    {
        public string ds { get; set; }
        public string address { get; set; }
    }

    public class EgrulFounderHistoryData
    {
        public string ds { get; set; }
        public string df { get; set; }
        public string dc { get; set; }
        public string name { get; set; }
        public decimal? summa { get; set; }
        public Guid uid { get; set; }
        public int mode { get; set; }
        public string inn { get; set; }
    }

    public class EgrulCoownerHistoryData
    {
        public string dtstart { get; set; }
        public string name { get; set; }
        public decimal? summa { get; set; }
        public string ticker { get; set; }
        public string coowner_type { get; set; }
        public string inn { get; set; }
        public string share_head { get; set; } //=%
        public string share_part { get; set; } //=35
    }

    public class Goroda
    {
        public int soato { get; set; } //="36440000" 
        public string name { get; set; } //="Тольятти"
        public string gerb { get; set; } //fn
    }

    public class Okved
    {
        public string kod { get; set; } //="34.10.2" 
        public string name { get; set; } //="Производство легковых автомобилей" 
        public byte vis { get; set; } //="1" 
    }

    public class RulerData
    {
        public RulerData()
        {
            this.link = "";
            this.ruler = "";
            this.ruler_fio = "";
            this.ruler_inn = "";
        }
        public string link { get; set; }
        public string ruler { get; set; }
        public string ruler_fio { get; set; }
        public string ruler_inn { get; set; }
    }

    public class MainBalance
    {

        public MainBalance() { }
        public MainBalance(int y, int t, decimal? b1, decimal? b2, decimal? b3)
        {
            this.year = y;
            this.type_id = t;
            this.bal_1 = b1;
            this.bal_2 = b2;
            this.bal_3 = b3;
        }

        public int year { get; set; } //="2011" 
        public int type_id { get; set; } //="5" 
        public decimal? bal_1 { get; set; } //="174846000000.000" 
        public decimal? bal_2 { get; set; } //="3476000000.000" 
        public decimal? bal_3 { get; set; } //="3106000000.000" 
    }

    public class ConstGKS
    {
        public ConstGKS() { }
        public ConstGKS(string nm, decimal? sh, decimal? shp, string okpo, string inn, string tik)
        {
            this.name = nm;
            this.share = sh;
            this.share_pecuniary = shp;
            this.okpo = okpo;
            this.inn = inn;
            this.ticker = tik;
        }

        public string name { get; set; } //="РОСИМУЩЕСТВО" 
        public decimal? share { get; set; } //="51.1700" 
        public decimal? share_pecuniary { get; set; } //="568335339.0000" 
        public string okpo { get; set; } //="00083629" 
        public string inn { get; set; } //="7710723134" 
        public string ticker { get; set; } //="GOSIM"
    }

    public class ConstEgrul
    {
        public ConstEgrul() { }

        public ConstEgrul(string uid, string ogrn, string nm, string inn, string sh, string sh_head, string sh_part, string e_d, string tik, int o, string css, string grn, string grn_date, string encumbrance_type, string encumbrance_per)
        {
            this.uid = uid;
            this.ogrn = ogrn;
            this.name = nm;
            this.inn = inn;
            this.share = sh;
            this.share_head = sh_head;
            this.share_part = sh_part;
            this.extract_date = e_d;
            this.ticker = tik;
            this.ord = o;
            this.css = css;
            this.grn = grn;
            this.grn_date = grn_date;
            this.encumbrance_per = encumbrance_per;
            this.encumbrance_type = encumbrance_type;

        }

        public string uid { get; set; } //="72423D90-E315-4586-AEA6-AF94BB7019D6" 
        public string ogrn { get; set; } //="1027700092661" 
        public string name { get; set; } //="МИНИСТЕРСТВО ИМУЩЕСТВЕНЫХ ОТНОШЕНИЙ РФ" 
        public string inn { get; set; } //="7710144747" 
        public string share { get; set; } //="568335339" 
        public string share_head { get; set; } //=%
        public string share_part { get; set; } //=35
        public string extract_date { get; set; } //="2015-08-18T00:00:00" 
        public string ticker { get; set; } //="2015-08-18T00:00:00" 
        public int ord { get; set; } //="1"
        public string css { get; set; } //="ul" 
        public string grn { get; set; } //="1234567890123" 
        public string grn_date { get; set; } //="2015-08-18T00:00:00" 
        public string encumbrance_per { get; set; } //период обременения
        public string encumbrance_type { get; set; } //вид обременения
    }
}