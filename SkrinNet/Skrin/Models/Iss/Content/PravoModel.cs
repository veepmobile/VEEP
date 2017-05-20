using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class PravoSearchObject
    {
        public string iss { get; set; }
        public string kw { get; set; }
        public int page { get; set; }           //номер страницы
        public int rcount { get; set; }         //количество дел на странице
        public string dfrom { get; set; }     //дата поступления дела в суд: с...
        public string dto { get; set; }     //по ...
        public string dtype { get; set; }     //форма участия
        public string ac_name { get; set; }         //арбитражный суд
        public string dcateg_id { get; set; }     //категория дела
        public string job_no { get; set; }         //номер дела
        public bool isCompany { get; set; }
        public int mode { get; set; } // 1 - поиск по ИНН и ОГРН, 2 - поиск по наименованию
        public string rmode { get; set; } // надежность выбора
    }

    public class PravoModel
    {
        private CompanyData _companydata;
        public PravoModel(CompanyData companydata)
        {
            _companydata = companydata;
        }

        public CompanyData PravoCompany
        {
            get
            {
                return _companydata;
            }
        } 

    }

    public class PravoMessage
    {
        public string ISS { get; set; }
        public string CompanyName { get; set; }
        public List<PravoMessageItem> MessagesList { get; set; }
    }

    public class PravoMessageItem
    {
        public string SourceName { get; set; }
        public string RegDate { get; set; }
        public string Contents { get; set; }    
    }

    public class SummaryData
    {
        public List<YearSummary> YearsData { get; set; }
    }

    public class YearSummary
    {
        public int year { get; set; }               //год
        public int ocnt { get; set; }               //ответчик - количество дел
        public decimal osumma { get; set; }         //ответчик - сумма
        public int icnt { get; set; }               //истец - количество дел
        public decimal isumma { get; set; }         //истец - сумма
    }

    public class PravoDetail
    {
        public string reg_date { get; set; }        //дата поступления дела в суд
        public string reg_no { get; set; }          //номер дела
        public string cname { get; set; }           //арбитражный суд
        public string case_sum { get; set; }       //сумма иска
        public string disput_type_categ { get; set; }  //вид спора
        public string case_type { get; set; }
        public string side_type_name { get; set; }    //форма участия
        public string ext_ist_list { get; set; }    //истец
        public string ext_otv_list { get; set; }    //ответчик
        public string ext_third_list { get; set; }  //третья сторона
        public string ext_over_list { get; set; }   //иные лица
        public int rmode { get; set; }   //совпадение по: 1 - ИНН и ОГРН, 2 - ИНН или ОГРН, 3 - по наименованию
    }
}