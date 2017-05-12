using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
        public class FedresursSearchObject
        {
            public string iss { get; set; }
            public string dfrom { get; set; }
            public string dto { get; set; }
            public string kw { get; set; }
            public string type { get; set; }
            public bool isCompany { get; set; }
            public int page { get; set; }
        }

        public class FedresursModel
        {
            private CompanyData _companydata;
            public FedresursModel(CompanyData companydata)
            {
                _companydata = companydata;
            }

            public CompanyData FedresursCompany
            {
                get
                {
                    return _companydata;
                }
            }
        }

        public class FedresursMessages
        {
            public List<FedresursMessageItem> MessagesList { get; set; }
        }

        public class FedresursMessageItem
        {
            public string MessNum { get; set; }
            public string CompanyName { get; set; }
            public string INN { get; set; }
            public string OGRN { get; set; }
            public string Address { get; set; }
            public string ShowPubDate { get; set; }
            public string TypeName { get; set; }
            public string Contents { get; set; }
            public string HtmlTable { get; set; }
            public List<MessageValue> MessValues { get; set; }
        }

        public class MessageValue
        {
            public string name { get; set; }
            public string value { get; set; }
        }
}