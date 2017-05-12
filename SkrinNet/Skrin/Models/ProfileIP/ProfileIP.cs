using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.ProfileIP
{
    public class ProfileIP
    {
        public ProfileIP()
        {
            timeouts = new Dictionary<string, long>();
        }

        public int user_id { get; set; }
        public int data_access { get; set; }


        public string okpo { get; set; }                    //ОКПО
        public string oktmo { get; set; }                    //ОКТМО
        public string okato { get; set; }                   //ОКАТО
        public string okato_name { get; set; }              //ОКАТО (расшифровка)
        public string ogrnip { get; set; }                  //ОГРНИП
        public string id { get; set; }
        public string inn { get; set; }
        public string fio { get; set; }                     //ФИО
        public string name { get; set; }
        public string rd { get; set; }                      //дата регистрации (ОГРНИП)
        public string mesto { get; set; }
        public string gmc_status { get; set; }              //статус Росстата
        public string updt { get; set; }                    //дата статуса Росстата
        public string issuer_id { get; set; }

        //public int citizenship_id { get; set; }             //гражданство, ID
        public string citizenship_name { get; set; }        //гражданство, наименование (1 - гражданин Российской Федерации; 2 - иностранный гражданин; 3 - лицо без гражданства)
        //public int typeip { get; set; }                     //вид ИП, ID
        public string typeip_name { get; set; }             //вид ИП, наименование (1 – индивидуальный предприниматель; 2 – глава крестьянского фермерского хозяйства)
        public string regorg_address { get; set; }          //адрес регистрирующего органа по месту жительства ИП

        public string nalog_name { get; set; }              //Наименование налогового органа
        public string nalog_record_date { get; set; }     //дата постановки на учет  в налоговом органе

        public string email { get; set; }                   //email
        public string status { get; set; }                   //сведения о состоянии
        public DateTime status_date { get; set; }           //дата сведений о состоянии
        public string stoping { get; set; }                   //сведения о прекращении деятельности
        public DateTime stoping_date { get; set; }           //дата сведений о прекращении деятельности
        public string vidreg { get; set; }                   //наличие в ЕГРИП записей о правоспособности
        public DateTime vidreg_date { get; set; }             //дата записей в ЕГРИП о правоспособности
        public string statusIp { get; set; }                   //выводимый статус
        public string statusNotes { get; set; }                   //комментарий к статусу
        public string pfr_status { get; set; }                   //регистрация в ПФР
        public DateTime pfr_date { get; set; }                   //дата регистрации в ПФР
        public string pfr_name { get; set; }                   //наименование рег.органа ПФР
        public string fss_status { get; set; }                   //регистрация в ФСС
        public DateTime fss_date { get; set; }                   //дата регистрации в ФСС
        public string fss_name { get; set; }                   //наименование рег.органа ФСС
        public OkvedCls main_okved { get; set; }                   //основной ОКВЕД

        public string region_name { get; set; }
        public List<GorodaCls> goroda { get; set; }
        public List<OkvedCls> okveds { get; set; }
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

        public List<RegistrationsPrev> prev_reg { get; set; }         //Сведения о предыдущей регистрации ИП

        /// <summary>
        /// Функция запрета доступа
        /// </summary>
        public string denied_access_function { get; set; }

        public Dictionary<string, long> timeouts { get; set; }
    }


    //public class EgripData2
    //{
    //    public string egrip_date { get; set; }
    //    public string status_date { get; set; }
    //    public string status { get; set; }
    //    public bool egrip_istoday { get; set; }
    //}

    //public class EgripLinksData2
    //{
    //    public string egrip_date { get; set; }
    //    public string id { get; set; }
    //    public bool is_today { get; set; }
    //}

    //public class RegistrationsPrev                      //Сведения о предыдущей регистрации ИП
    //{
    //    public string fio { get; set; }                 //ФИО
    //    public string ogrnip { get; set; }              //ОГРНИП
    //    public string inn { get; set; }                 //ИНН
    //    public DateTime reg_date { get; set; }          //Дата присвоения ОРГНИП
    //    public DateTime stop_date { get; set; }         //Дата внесения записи о прекращении деятельности
    //}

    //public class EGRIPPDFCls
    //{

    //    public EGRIPPDFCls(string d, string ds, string os, string is_pdf)
    //    {

    //        this.dt = d;
    //        this.datestring = ds;
    //        this.ogrnip = os;
    //        this.is_pdf = is_pdf;
    //    }

    //    public string dt { get; set; }
    //    public string datestring { get; set; }
    //    public string ogrnip { get; set; }
    //    public string is_pdf { get; set; }
    //}
}