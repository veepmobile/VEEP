using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.Models.UA
{
    /// <summary>
    /// Модель для страниц с файлами годовых и квартальных отчетов 
    /// Годовые все в одной таблице
    /// Квартальные разбиты по годам
    /// </summary>
    public enum UAReportView { Annual=0, Quarterly=1 };
    public class UAReportModel
    {
        public string edrpou { get; set; }
        public UAReportView report_view;
        public List<UAReportGroup> Items { get; set; }
        public UAReportModel()
        {
            Items = new List<UAReportGroup>();
        }
    }
    /// <summary>
    /// Для годовых в единственном экпземпляре
    /// Для квартальных - список годов
    /// </summary>
    public class UAReportGroup
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
        public List<UAReportItem> Items { get; set; }
        /// <summary>
        /// Список отчетов
        /// </summary>
        public UAReportGroup()
        {
            Items = new List<UAReportItem>();
        }
    }
    /// <summary>
    /// Строка с отчетом
    /// </summary>
    public class UAReportItem
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
        public List<UAReportFile> Items { get; set; }
        public UAReportItem()
        {
            Items = new List<UAReportFile>();
        }
    }
    /// <summary>
    /// Файл отчета
    /// </summary>
    public class UAReportFile
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

        public HtmlString GetLink(string edrpou)
        {
            int p = file_name.LastIndexOf(".");
            string extention = file_name.Substring(p + 1, file_name.Length - p - 1);
            return string.Format("<a href='/UA/Documents/{0}'><span class=\"{1} d_icon\"></span>{2}</a>",
                doc_id, ContentTypeCollection.GetStyle(extention), _GetDocumentName()).Html();
        }
    }

    public class UADocument
    {
        public string id {get;set;}
        public string edrpou {get;set;}
        //public int source_report_id {get;set;}
        public int year {get;set;}
        public int quart {get;set;}
        public bool is_year {get;set;}
        //public string source_id {get;set;}
        public string file_name {get;set;}
        public int file_id { get; set; }
    }
}
