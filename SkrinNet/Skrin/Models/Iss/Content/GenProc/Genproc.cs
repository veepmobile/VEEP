using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content.GenProc
{
    public class CheckDetails
    {
        public Int64 Id { get; set; }
        public string INN { get; set; }
        public Int32 CheckId { get; set; }                  // ID проверки с сайта генпрокуратуры
        public string PurposeCheck { get; set; }            // цель проведения проверки
        public int CheckDay { get; set; }               // число начала проверки
        public int CheckMonth { get; set; }               // месяц начала проверки
        public int CheckYear { get; set; }               // год начала проверки
        public string CheckDate { get; set; }              // дата начала проверки (строка месяц + год)
        public string CheckAddress { get; set; }              // место нахождения объекта
        public string CheckForms { get; set; }              // форма проведения проверки
        public string CheckOrgan { get; set; }              // проверяющий гос.орган
        public string CheckOrganOther { get; set; }         // др. органы, участвующие в проверке
        public string CheckHtml { get; set; }               // описание проверки в html
        public string ExtractDate { get; set; }             // дата выгрузки результатов поиска
    }

    public class GenprocResult
    {
        public string GenprocCheck { get; set; }             // результат поиска в html
        public string PurposeCheck { get; set; }            // цель проведения проверки
        public string OrganCheck { get; set; }            // проверяющий гос.орган
    }
    public class GenprocResultXML
    {

        public string GenprocCheckXML { get; set; }
    }
    public class GenprocJson
    {
        public string iss { get; set; }
        public Filters filter { get; set; }
        public GenprocResult result { get; set; }
    }
}