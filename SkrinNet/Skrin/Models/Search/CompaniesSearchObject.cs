using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ULSearch;

namespace Skrin.Models.Search
{
    public class CompaniesSearchObject
    {
        public string company { get; set; }     //наименование
        public int strict { get; set; }         //строгий поиск=1
        public string phone { get; set; }       //телефон
        public string address { get; set; }     //адрес
        public string ruler { get; set; }       //руководитель
        public string constitutor { get; set; } //учредитель
        public string regions { get; set; }     //код региона
        public int is_okato { get; set; }       //1 - код окато, 0 - регистрация в налоговой
        public int reg_excl { get; set; }       //-1 - кроме региона
        public string industry { get; set; }    //отрасль
        public int is_okonh { get; set; }       //по коду оконх
        public int ind_excl { get; set; }       //-1 - кроме отрасли
        public int ind_main { get; set; }       //-1 - оквэд основной
        public string okopf { get; set; }       //опф
        public int okopf_excl { get; set; }     //-1 - кроме опф
        public string okfs { get; set; }        //окфс - форма собственности
        public int okfs_excl { get; set; }      //-1 - кроме окфс
        public string trades { get; set; }      //листинг: РТС + ММВБ + РТС Board
        public int gaap { get; set; }           //1 - Отчетность IAS-GAAP
        public int bankrupt { get; set; }       //1 - в реестре недобросовестных поставщиков
        public int msp { get; set; }            //1 - в реестре малых предприятий
        public int rsbu { get; set; }           //1 - Отчетность РСБУ
        public int status { get; set; }         //1 - Действующие по данным ЕГРЮЛ
        public string kod { get; set; }         //код (ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН)
        public string dbeg { get; set; }        //дата присвоения ОГРН - с ...
        public string dend { get; set; }        //дата присвоения ОГРН - по ...
        public int group_id { get; set; }       //id группы, 0 - все предприятия
        public int page_no { get; set; }        //номер страницы
        public int rcount { get; set; }         //количество записей на странице
        public int user_id { get; set; }        //user_id
        public string top1000 { get; set; }     //
        public string group_name { get; set; }  //наименование группы
        public string fas { get; set; }         //лидеры рынка - код региона
        public int fas_excl { get; set; }       //1 - кроме региона
        public int rgstr { get; set; }          //1 - в реестре росстата
        public int filials { get; set; }        //1 - кроме филиалов и представительств
        public int archive { get; set; }        //1 - искать в архиве
    }

    public class ULDetails
    {
        public string name { get; set; }
        public string nm { get; set; }
        public string inn { get; set; }
        public string region { get; set; }
        public string okpo { get; set; }
        public string okved_code { get; set; }
        public string okved { get; set; }
        public string ogrn { get; set; }
        public string reg_date { get; set; }
        public string reg_org_name { get; set; }
        public string legal_address { get; set; }
        public string ruler { get; set; }
        public string legal_phone { get; set; }
        public string legal_fax { get; set; }
        public string legal_email { get; set; }
        public string www { get; set; }
        public string del { get; set; }
        public string ticker { get; set; }
        public string issuer_id { get; set; }
        public string type_id { get; set; }
        public string ip_stop { get; set; }
        public string type_ka { get; set; }
        /// <summary>
        /// Дополнительное поле дающее общее кол-во найденных компаний
        /// </summary>
        public int search_count { get; set; }

    }

    public class UADetails
    {
        /*Наименование	ЕДРПОУ	Регион	Отрасль	Регистрационный номер	Дата гос.регистрации	Орган гос. регистрации	Адрес	Руководитель	Телефон	Факс	E-mail	Сайт*/
        public string name { get; set; }
        public string edrpou { get; set; }
        public string region { get; set; }
        public string industry { get; set; }
        public string regno { get; set; }
        public string regdate { get; set; }
        public string regorg { get; set; }
        public string addr { get; set; }
        public string ruler { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string www { get; set; }
    }


#region Elastic_old

    /*
    public class SearchListItem
    {
        public string name { get; set; }
        public string short_name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public int? region_id { get; set; }
        public string region { get; set; }
        public string okato { get; set; }
        public string okpo { get; set; }
        public string main_okved_code { get; set; }
        public string main_okved_name { get; set; }
        public int? main_okved_year { get; set; }
        public string opf { get; set; }
        public string okfs { get; set; }
        public string reg_date { get; set; }
        public string reg_org_name { get; set; }
        public string address { get; set; }
        public string ruler { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string www { get; set; }
        public string del_date { get; set; }
        public string ticker { get; set; }
        public string bones { get; set; }
        public string issuer_id { get; set; }
        public string gks_id { get; set; }
        public int? group_id { get; set; }
        public int? type_id { get; set; }
        public bool? is_mmvb { get; set; }
        public bool? is_rtsboard { get; set; }
        public bool? is_gaap { get; set; }
        public bool? is_gks { get; set; }
        public bool? is_nedobr { get; set; }
        public int? status { get; set; }
        public string status_name { get; set; }
        public string status_date { get; set; }
        public bool? uniq { get; set; }
        public bool? uniq_inn { get; set; }
        public int? msp_type { get; set; }
        public int? rsbu_year { get; set; }
        public List<Key> key_list { get; set; }
        public List<Constitutor> constitutor_list { get; set; }
        public List<Manager> manager_list { get; set; }
        public List<Address> address_list { get; set; }
        public List<Phone> phone_list { get; set; }
        public List<int> okved_id_list { get; set; }
        public List<string> okato_list { get; set; }
        public List<int> fas_list { get; set; }
    }


    public class Key
    {
        public int key_type { get; set; }
        public string key_value { get; set; }
        public bool is_old { get; set; }

    }

    public class Constitutor
    {
        public string name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public bool is_old { get; set; }
    }

    public class Manager
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public bool is_old { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
        public bool is_old { get; set; }
    }

    public class Phone
    {
        public string phone { get; set; }
        public bool is_old { get; set; }
    }

    */

#endregion


}