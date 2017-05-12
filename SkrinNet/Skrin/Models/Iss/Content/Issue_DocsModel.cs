using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class Issue_DocsModel
    {
        public string issuer_id { get; set; }
        public string ticker { get; set; }
        public List<Issue_DocItem> Items { get; set; }
        public Issue_DocsModel()
        {
            Items = new List<Issue_DocItem>();
        }
    }

    public class Issue_DocItem
    {
        /// <summary>
        /// Идентификатор выпуска
        /// </summary>
        public string issue_id { get; set; }
        /// <summary>
        /// Регистрационный номер выпуска
        /// </summary>
        public string issue_no { get; set; }
        /// <summary>
        /// Дата регистрации выпуска в текстовом виде
        /// </summary>
        public string issue_date { get; set; }

        /// <summary>
        /// Вид ценной бумаги
        /// </summary>
        public string sec_name { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        public List<ReportFile> Items { get; set; }
        public Issue_DocItem()
        {
            Items = new List<ReportFile>();
        }
    }

}