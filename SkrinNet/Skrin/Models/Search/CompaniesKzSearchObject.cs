using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Search
{
    public class CompaniesKzSearchObject
    {
        public string company { get; set; }     //наименование
        public string ruler { get; set; }       //руководитель
        public string regions { get; set; }     //код региона
        public int reg_excl { get; set; } //искл.
        public string status { get; set; } // Статус
        public int stat_excl { get; set; }//искл.
        public string industry { get; set; }  //ОКЭД
        public int ind_excl { get; set; }  //исключить выбранные кведы
        public int ind_main { get; set; }  //только основной квед
        public string econ { get; set; }  //сектор экономики
        public int econ_excl { get; set; }//искл.
        public string own { get; set; }     //вид собственности
        public int own_excl { get; set; }//искл.
        public string siz { get; set; }     //размерность предприятия
        public int siz_excl { get; set; }//искл.
        public string pcount { get; set; } //численность предприятия
        public int pcount_excl { get; set; }//искл.
        public int page_no { get; set; }        //номер страницы
        public int rcount { get; set; }         //количество записей на странице
        public int user_id { get; set; }        //user_id
        public string top1000 { get; set; }     //
    }

    public class KZDetails
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string MainDeal { get; set; }
        public string CodeTax { get; set; }
        public string DateReg { get; set; }
        public string FullAddress { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    public class KZSearchResult
    {
        public string code { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string FullAddress { get; set; }
        public string Manager { get; set; }
        public string MainDeal { get; set; }
        public int access { get; set; }
        public int cnt { get; set; }
    }
}