using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class VestnikSearchObject
    {
        public string iss { get; set; }
        public string dfrom { get; set; }
        public string dto { get; set; }
        public string kw { get; set; }
        public string type { get; set; }
        public int page { get; set; }
        public bool isCompany { get; set; }
        public int mode { get; set; } // 1 - поиск по ИНН и ОГРН, 2 - поиск по наименованию
    }

    public class VestnikModel
    {
        private CompanyData _companydata;
        public VestnikModel(CompanyData companydata)
        {
            _companydata = companydata;
        }

        public CompanyData VestnikCompany
        {
            get
            {
                return _companydata;
            }
        } 

    }

    public class VestnikMessage
    {
        public string name { get; set; }
        public string ticker { get; set; }
        public string id { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string contents { get; set; }
        public string dt { get; set; }
        public string nomera { get; set; }
        public string region { get; set; }
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string orig_id { get; set; }
        public string orig_content { get; set; }
        public string orig_dt { get; set; }
        public string orig_nomera { get; set; }
        public string orig_region { get; set; }
        public string orig_type_name { get; set; }
        public string corr_id { get; set; }
        public string corr_content { get; set; }
        public string corr_dt { get; set; }
        public string corr_nomera { get; set; }
        public string corr_region { get; set; }
        public string corr_type_name { get; set; }
    }
}