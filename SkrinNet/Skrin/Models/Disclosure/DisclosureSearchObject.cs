using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Disclosure
{
    public class DisclosureSearchObject
    {
        public int page { get; set; }           //номер страницы
        public int rcount { get; set; }         //количество сообщений на странице
        public string DBeg { get; set; }        //дата регистрации сообщения: с...
        public string DEnd { get; set; }        //по ...
        public Int64 DBeg_ts { get; set; }      //дата регистрации сообщения: с...
        public Int64 DEnd_ts { get; set; }      //по ...
        public string search_name { get; set; }    //наименование общества/инн/огрн/фсфр
        public string type_id { get; set; }     //тип сообщения
        public int types_excl { get; set; }   //кроме
        public string search_text { get; set; }          //текст
        public string id { get; set; } //ID сообщения для показа из URL
        public int grp { get; set; }          //группа 
        public string agency { get; set; } //ID агентства для показа из URL
    }

    public class DisclosureItem
    {
        public int id { get; set; } 
        public int Event_Type_id { get; set; } 
        public string header { get; set; }
        public DateTime Event_Date { get; set; }
        public string Event_Text { get; set; }
        public int Event_Type_Group_ID { get; set; } 
        public string Event_Type_name { get; set; }
        public string Event_Type_Group_name { get; set; }
        public string Firm_Id { get; set; }
        public string SHORT_NAME_RUS { get; set; }
        public string FULL_NAME_RUS { get; set; }
        public string ticker { get; set; }
        public DateTime insert_date { get; set; }
        public string update_date { get; set; }
    }


}

