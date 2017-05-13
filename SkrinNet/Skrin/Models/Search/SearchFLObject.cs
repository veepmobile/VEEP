using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Search
{
    public class SearchFLObject
    {
        public string fio { get; set; }      //строка поиска
        public int page { get; set; }     //номер страницы
    }
    public class FLInfo
    {
        public string fio { get; set; }
        public string inn { get; set; }
    }
}