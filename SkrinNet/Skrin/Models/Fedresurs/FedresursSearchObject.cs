using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Fedresurs
{
    public class FedresursSObject
    {
        public int page { get; set; }           //номер страницы
        public int rcount { get; set; }         //количество сообщений на странице
        public string DBeg { get; set; }        //дата регистрации сообщения: с...
        public string DEnd { get; set; }        //по ...
        public Int64 DBeg_ts { get; set; }      //дата регистрации сообщения: с...
        public Int64 DEnd_ts { get; set; }      //по ...
        public string search_name { get; set; }    //наименование общества/инн/огрн/фсфр
        public string search_text { get; set; }    //текст в сообщении
        public string types { get; set; }     //тип сообщения
        public int types_excl { get; set; }   //кроме
        public int grp { get; set; }   //группа
    }

}