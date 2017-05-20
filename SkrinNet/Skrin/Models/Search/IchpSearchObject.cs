using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Search
{
    public class IchpSearchObject
    {
        public string ruler { get; set; }       //руководитель
        public string regions { get; set; }     //код региона
        public int is_okato { get; set; }       //1 - код окато, 0 - регистрация в налоговой
        public int reg_excl { get; set; }       //-1 - кроме региона
        public string industry { get; set; }    //отрасль
        public int ind_excl { get; set; }       //-1 - кроме отрасли
        public int page_no { get; set; }        //номер страницы
        public int rcount { get; set; }         //количество записей на странице
        public int user_id { get; set; }        //user_id
        public int group_id { get; set; }
        public string group_name { get; set; }  //наименование группы
        public string top1000 { get; set; }     //
    }

    public class FLDetails
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string region { get; set; }
        public string okpo { get; set; }
        public string okved { get; set; }
        public string ogrnip { get; set; }
        public string stoping { get; set; }
        public string typeip { get; set; } 
        /// <summary>
        /// Дополнительное поле дающее общее кол-во найденных ИП
        /// </summary>
        public int search_count { get; set; }

    }
}