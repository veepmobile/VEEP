using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ULSearch
{
    #region Elastic
    

    public class BonesElastic
    {
        public string key { get; set; }
        public int doc_count { get; set; }
    }

    public class Bones
    {
        public string name { get; set; }
        public int cnt { get; set; }
    }

    public class ULDetailsExport : ULElasticBase
    {
        public string name { get; set; }
        public string short_name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }


    }




    #endregion


    public class SearchResultElastic
    {
        public ResultInfoElastic ResultInfo;   //результат поиска
        public List<ULDetailsElastic> Data;  //Список компаний
        //public List<Error> Errors;   //Ошибки

        public SearchResultElastic()
        {
            ResultInfo = new ResultInfoElastic();
            Data = new List<ULDetailsElastic>();
            //Errors = new List<Error>();
        }

    }

    public class ResultInfoElastic
    {
        int _total_found = 0;   // Всего найдено
        public int total_found
        {
            get { return _total_found; }
            set { _total_found = value; }
        }
        int _total_show = 0;   // Всего показывается - если _total_found > 10000, то _total_show=10000
        public int total_show
        {
            get { return _total_show; }
            set { _total_show = value; }
        }
        int _page = 0;   // Номер выводимой страницы
        public int page
        {
            get { return _page; }
            set { _page = value; }
        }
        int _pagecount = 0; // Количество страниц
        public int pagecount
        {
            get { return _pagecount; }
            set { _pagecount = value; }
        }
    }

}