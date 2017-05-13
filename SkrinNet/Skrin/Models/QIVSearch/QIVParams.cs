using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.QIVSearch
{
    public class QIVParams
    {
        public int id { get; set; }
        public string line_code { get; set; }
        public string name { get; set; }
        public int type_id { get; set; }
        public bool isFolder { get; set; }
    }

    public class QIVSearchLines
    {
        public int period { get; set; }
        public int param_id { get; set; }
        public int? param_type { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }
    public enum QIVSearchType { GKS = 0, NAUFOR = 1, MSFO = 2}

    public enum QIVResultType { Web = 0, Group = 1, Excel = 2 }

    public class QIVQuery
    {
        public string select_text { get; set; } 
        public string from_text { get; set; }
        public string where_text { get; set; }   
        public List<SKRIN.SQLParam> sqlparams { get; set; }//Параметры запроса

    }
    
    public class QIVSearchParams
    {
        public QIVSearchType qiv_type { get; set; }    //вид отчетности: 0 - gks, 1 - naufor, 2 - msfo
        public string regions { get; set; }     //код региона
        public int is_okato { get; set; }       //1 - код окато, 0 - регистрация в налоговой
        public int reg_excl { get; set; }       //-1 - кроме региона
        public string industry { get; set; }    //отрасль
        public int ind_excl { get; set; }       //-1 - кроме отрасли
        public int ind_main { get; set; }       //-1 - оквэд основной
        public string okopf { get; set; }       //опф
        public int okopf_excl { get; set; }     //-1 - кроме опф
        public string okfs { get; set; }        //окфс - форма собственности
        public int okfs_excl { get; set; }      //-1 - кроме окфс
        public string dbeg { get; set; }        //дата присвоения ОГРН - с ...
        public string dend { get; set; }        //дата присвоения ОГРН - по ...
        public int group_id { get; set; }       //id группы, 0 - все предприятия
        public int page_no { get; set; }        //номер страницы
        public int rcount { get; set; }         //количество записей на странице
        public string top1000 { get; set; }     //
        public string group_name { get; set; }  //наименование группы
        public string andor { get; set; }
        public string issuers { get; set; }     //список issuer_id для экспорта в excel
        public int rgstr { get; set; }          //1 - в реестре росстата
        public int sort_col { get; set; }      //номер колонки для сортировки
        public int sort_direct { get; set; }      //направление сортировки 1 - desc, 0 - asc
        public List<QIVSearchLines> param_lines { get; set; }
    }

    public class QIVSearchResultLine
    {
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        public string issuer_id { get; set; }
        public string type_id { get; set; }
        public string region_name { get; set; }
        public List<string> param_name; // показатели наименование
        public List<decimal?> param_values; //показатели значение
        public string full_name { get; set; }
        public string opf_name { get; set; }
        public string okfs_name { get; set; }
        public string okpo { get; set; }
        public string ogrn_date { get; set; }
        public string reg_org_name { get; set; }
        public string okved_name { get; set; }
        public string okved_code { get; set; }
        public string legal_address { get; set; }
        public string ruler { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string www { get; set; }
     }

    public class QIVSearchResult
    {
        public int total;
        public List<QIVSearchResultLine> results; //
    }


}