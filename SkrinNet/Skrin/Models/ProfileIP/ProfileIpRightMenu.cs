using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Iss.Content;

namespace Skrin.Models.ProfileIP
{
    public class ProfileIpRightMenu
    {

        public int user_id { get; set; }
        public int data_access { get; set; }
        public string okpo { get; set; }             
        public string oktmo { get; set; }                   //ОКТМО
        public string ogrnip { get; set; }                  //ОГРНИП
        public string id { get; set; }
        public string inn { get; set; }  
        public string name { get; set; }
        public string fio { get; set; }   
        public string rd { get; set; }                      //дата регистрации (ОГРНИП)

        public string issuer_id { get; set; }

        public EgripData2 egrip { get; set; }
        public bool egrip_is_black { get; set; }
        public EgripLinksData2 egrip_links { get; set; }
        public bool EGRIPPDFisToday;
        public string EGRIPPDFdate;
        public string EGRIPPDFdate_string;
        public string EGRIPPDFis_pdf;
        public List<EGRIPPDFCls> EGRIPPDF { get; set; }

        public ZakupkiData zak_data { get; set; }
        public UnfairData unf_data { get; set; }
        public PravoData pravo_data { get; set; }

        public bool is_bancruptcy { get; set; }
        public bool is_pravo_sphinx { get; set; }
        
        /// <summary>
        /// Функция запрета доступа
        /// </summary>
        public string denied_access_function { get; set; }

        public Dictionary<string, long> timeouts { get; set; }
        public List<Choice> choice { get; set; }
        public ProfileIpRightMenu(string ticker)
        {
            timeouts = new Dictionary<string, long>();
            this.ogrnip = ticker;
        }
    }

    public class EgripData2
    {
        public string egrip_date { get; set; }
        public string status_date { get; set; }
        public string status { get; set; }
        public bool egrip_istoday { get; set; }
    }

    public class EgripLinksData2
    {
        public string egrip_date { get; set; }
        public string id { get; set; }
        public bool is_today { get; set; }
    }

    public class RegistrationsPrev                      //Сведения о предыдущей регистрации ИП
    {
        public string fio { get; set; }                 //ФИО
        public string ogrnip { get; set; }              //ОГРНИП
        public string inn { get; set; }                 //ИНН
        public DateTime reg_date { get; set; }          //Дата присвоения ОРГНИП
        public DateTime stop_date { get; set; }         //Дата внесения записи о прекращении деятельности
    }

    public class EGRIPPDFCls
    {

        public EGRIPPDFCls(string d, string ds, string os, string is_pdf)
        {

            this.dt = d;
            this.datestring = ds;
            this.ogrnip = os;
            this.is_pdf = is_pdf;
        }

        public string dt { get; set; }
        public string datestring { get; set; }
        public string ogrnip { get; set; }
        public string is_pdf { get; set; }
    }
}