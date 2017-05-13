using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.Models.Iss.Content
{
    public class DocumentList
    {
        public string issuer_id { get; set; }
        public string ticker { get; set; }

        public List<DocumentGroup> Items { get; set; }

        public DocumentList()
        {
            Items = new List<DocumentGroup>();
        }
    }


    public class DocumentGroup
    {
        public int group_id { get; set; }
        public string group_name { get; set; }

        public List<DocumentType> Items { get; set; }

        public DocumentGroup()
        {
            Items = new List<DocumentType>();
        }
    }

    public class DocumentType
    {
        public int type_id { get; set; }
        public string type_name { get; set; }

        public List<DocumentItem> Items { get; set; }

        public DocumentType()
        {
            Items = new List<DocumentItem>();
        }
    }

    public class DocumentItem
    {
        public string doc_name { get; set; }
        public string file_name { get; set; }
        public string dt { get; set; }
        public string doc_id { get; set; }
        public int pages { get; set; }
        public DateTime? reg_date { get; set; }
        public string file_size { get; set; }


        private string _GetDocumentName()
        {
            return string.IsNullOrEmpty(doc_name) ? "Документ" : doc_name;
        }

        private int _GetDocumentExportType(int group_id)
        {
            //-1 - issuer_doc_types, -2 - issue_doc_types, -3 - annual_reports, -4 - quart_reports, -5 issuer_FSFR_doc_types, -6 Issuer_GAAPs, -7 Account_Forms, -8 Annual_BuhReports
            switch (group_id)
            {
                case 999:
                    return -999;
                case -1005:
                    return -5;
                case -1100:
                    return -5;
                default:
                    return -1;
            }                
//            return group_id == 999 ? -999 : -1;
        }

        public HtmlString GetLink(string ticker, int group_id)
        {
            int p = file_name.LastIndexOf(".");
            string extention = file_name.Substring(p+1, file_name.Length-p-1);
            if (pages == 0)
            {
                return string.Format("<a href='/Documents/Index?iss={0}&id={1}&fn={2}&doc_id={3}'><span class=\"{4} d_icon\"></span>{5}</a>",
                    ticker, doc_id, file_name.UrlEncode(), _GetDocumentExportType(group_id), ContentTypeCollection.GetStyle(extention), _GetDocumentName()).Html();
            }
            else
            {
                return string.Format("<a href=\"#\" onclick=\"showscans('{0}',{1},'{2}',1)\"><span class=\"{3} d_icon\"></span>{4}</a>",
                    doc_id, pages, ticker, ContentTypeCollection.GetStyle(extention), _GetDocumentName()).Html();
            }
        }
    }
}