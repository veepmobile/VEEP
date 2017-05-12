using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.QIVSearch
{
    public class TPriceSO
    {
        public TPriceTemplateParams template_params;
        public TPriceExtraParams extra_params;
    }

    public class TPriceTemplateParams
    {
        public int pure_actives { get; set; }
        public int loss { get; set; }
        public int constitutors { get; set; }
        public decimal ncons { get; set; }
        public int subs { get; set; }
        public decimal nsubs { get; set; }
        public int nperiods { get; set; }
        public int only_suitable { get; set; }
        public int group_id { get; set; }
        public string regions { get; set; }
        public int reg_excl { get; set; }
        public string industry { get; set; }
        public int ind_excl { get; set; }
        public int ind_main { get; set; }
        public string okfs { get; set; }
        public int okfs_excl { get; set; }
        public int rcount { get; set; }
        public string andor { get; set; }
        public List<TpriceValParams> tparams { get; set; }
    }

    public class TpriceValParams
    {
        public int year { get; set; }
        public int param_id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int type_id { get; set; }
    }

    public class TPriceExtraParams
    {
        public int page_no { get; set; }
        public string sel { get; set; }
        public string group_name { get; set; }
        public string iss { get; set; }
    }

    /// <summary>
    /// Результаты поиска предприятий для расчета рентабельности
    /// </summary>
    public class TPriceULDetails:ULDetails
    {
        public int suit { get; set; }
        public string gks_id { get; set; }
        public string session_id { get; set; }
    }

    /// <summary>
    /// Результат расчета рентабельности
    /// </summary>
    public class TPriceResult
    {
        public List<TPriceVal> values { get; set; }

        public TPriceResult()
        {
            values = new List<TPriceVal>();
        }
    }

    /// <summary>
    /// Значение расчтеа рентабельности
    /// </summary>
    public class TPriceVal
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal v1 { get; set; }
        public decimal v2 { get; set; }
        public decimal v3 { get; set; }
        public decimal v4 { get; set; }
        public decimal v5 { get; set; }
        public decimal v6 { get; set; }
    }

    /// <summary>
    /// Пользовательский шаблон поиска
    /// </summary>
    public class TpriceTemplate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tcontent { get; set; }
    }
}