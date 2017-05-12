using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class CompanyData
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string SearchedName { get; set; }
        public string SearchedName2 { get; set; }
        public string INN { get; set; }
        public string OGRN { get; set; }
        public bool IsCompany { get; set; }
        public int Region { get; set; }
        public int Mode { get; set; } // 1 - поиск по ИНН и ОГРН, 2 - поиск по наименованию

        public string ShownSearchedName
        {
            get
            {
                return SearchedName + (SearchedName2 == null ? "" : " (" + SearchedName2 + ")");
            }
        }
    }
}