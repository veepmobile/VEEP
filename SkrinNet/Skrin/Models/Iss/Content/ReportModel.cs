using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.Models.Iss.Content
{
    /// <summary>
    /// Модель для страниц с файлами годовых и квартальных отчетов 
    /// Годовые все в одной таблице
    /// Квартальные разбиты по годам
    /// </summary>
    public enum ReportView { Annual=0, Quarterly=1 };
    public class ReportModel
    {
        public string issuer_id { get; set; }
        public string ticker { get; set; }
        public ReportView report_view;
        public List<ReportGroup> Items { get; set; }
        public ReportModel()
        {
            Items = new List<ReportGroup>();
        }
    }
    /// <summary>
    /// Для годовых в единственном экпземпляре
    /// Для квартальных - список годов
    /// </summary>
    public class ReportGroup
    {
        /// <summary>
        /// Для годовых смысла не имеет = 0 
        /// Для кваратальных - цифра года
        /// </summary>
        public int? group_id { get; set; }
        /// <summary>
        /// Для годовых заголовок таблицы. Обычно null - нет заголовка
        /// Для квартальных Год 
        /// </summary>
        public string group_name { get; set; }
        public List<ReportItem> Items { get; set; }
        /// <summary>
        /// Список отчетов
        /// </summary>
        public ReportGroup()
        {
            Items = new List<ReportItem>();
        }
    }
    /// <summary>
    /// Строка с отчетом
    /// </summary>
    public class ReportItem
    {
        /// <summary>
        /// Идентификатор строки
        /// Для годовых - год в формате цифры
        /// Для квартальных - квартал
        /// </summary>
        public int? item_id { get; set; }
        /// <summary>
        /// Наименование строки - то что будет в первой колонке
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// Список файлов может быть несколько (например на русском и английском)
        /// </summary>
        public List<ReportFile> Items { get; set; }
        public ReportItem()
        {
            Items = new List<ReportFile>();
        }
    }
    /// <summary>
    /// Файл отчета
    /// </summary>
    public class ReportFile
    {
        public string doc_name { get; set; }
        public int export_type { get; set; }
        public string file_name { get; set; }
        public string doc_id { get; set; }
        public short pages { get; set; }
        public string file_size { get; set; }

        private string _GetDocumentName()
        {
            return string.IsNullOrEmpty(doc_name) ? "Документ" : doc_name;
        }

        public HtmlString GetLink(string ticker)
        {
            int p = file_name.LastIndexOf(".");
            string extention = file_name.Substring(p + 1, file_name.Length - p - 1);
            if (pages <= 0)
            {
                return string.Format("<a href='/Documents/Index?iss={0}&id={1}&fn={2}&doc_id={3}'><span class=\"{4} d_icon\"></span>{5}</a>",
                    ticker, doc_id, file_name.UrlEncode(), export_type, ContentTypeCollection.GetStyle(extention), _GetDocumentName()).Html();
            }
            else
            {
                return string.Format("<a href=\"#\" onclick=\"showscans('{0}',{1},'{2}',1)\"><span class=\"{3} d_icon\"></span>{4}</a>",
                    doc_id, pages, ticker, ContentTypeCollection.GetStyle(extention), _GetDocumentName()).Html();
            }
        }
    }
}
