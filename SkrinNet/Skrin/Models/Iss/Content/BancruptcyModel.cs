using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class SearchObject
    {
        public string iss { get; set; }
        public string dfrom { get; set; }
        public string dto { get; set; }
        public string kw { get; set; } 
        public string type { get; set; }
        public int src { get; set; }
        public int page { get; set; }
        public bool isCompany { get; set; }
        public int mode { get; set; } // 1 - поиск по ИНН и ОГРН, 2 - поиск по наименованию
    }

    public class BancruptcyModel
    {
        private CompanyData _companydata;
        public BancruptcyModel(CompanyData companydata)
        {
            _companydata = companydata;
        }

        public CompanyData BancruptcyCompany
        {
            get
            {
                return _companydata;
            }
        } 

    }

    public class BancryptcyMessage
    {
        public string ISS { get; set; }
        public string CompanyName { get; set; }
        public List<MessageItem> MessagesList { get; set; }
    }

    public class MessageItem
    {
        public string SourceName { get; set; }
        public string RegDate { get; set; }
        public string Contents { get; set; }    
    }

    public class MessagesIds
    {
        public string Ids { get; set; }
        public int Source { get; set; } 
    }
}