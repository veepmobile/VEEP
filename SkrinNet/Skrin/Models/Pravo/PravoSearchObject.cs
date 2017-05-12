using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Pravo
{
    public class PravoSObject
    {
        public int page { get; set; }           //номер страницы
        public int rcount { get; set; }         //количество дел на странице
        public string ins_DBeg { get; set; }     //дата поступления дела в суд: с...
        public string ins_DEnd { get; set; }     //по ...
        public string last_DBeg { get; set; }     //дата обновления: с...
        public string last_DEnd { get; set; }     //по ...
        public Int64 ins_DBeg_ts { get; set; }     //дата поступления дела в суд: с...
        public Int64 ins_DEnd_ts { get; set; }     //по ...
        public Int64 last_DBeg_ts { get; set; }     //дата обновления: с...
        public Int64 last_DEnd_ts { get; set; }     //по ...
        public string search_txt { get; set; }     //наименование общества/инн/огрн
        public string side_type { get; set; }     //форма участия
        public int side_type_excl { get; set; }   //кроме
        public string ac_type { get; set; }         //арбитражный суд
        public int ac_type_excl { get; set; }       //кроме
        public string job_no { get; set; }         //номер дела
        public string disput_type { get; set; }           //категория спора
        public int disput_type_excl { get; set; }      //кроме
        public int grp { get; set; }      //Группа

    }


}

