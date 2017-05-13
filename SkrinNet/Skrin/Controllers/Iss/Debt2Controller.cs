using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Skrin.Models.Iss.Debt;
using Skrin.BLL.Iss.Debt;
using Elasticsearch.Net;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.DebtBLL;
using Newtonsoft.Json;
using System.IO;
using Skrin.BLL.Search;
using Skrin.Models.Iss.Content;
using SKRIN;

namespace Skrin.Controllers.Iss
{
    public class Debt2Controller : Controller
    {
        public async Task<ActionResult> DebtorSearchAsync(Skrin.Models.Iss.Debt.SearchObject so)
        {
            var company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(so.iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                var res = await Elastic.OpenNamedQuery<DebtorListItem>(
                    Configs.ElasticDebtServerUri,
                    "fssp",
                    DebtQueryGenerator2.SetSearchTypeDelegate,
                    (new DebtQueryGenerator2(so, company, adress, names)).GetSearch()
                );
                return Content(Elastic.JsonConvertNest(res), "application/json");
            }

            return null;
        }


        [HttpPost]
        public async Task<JsonResult> DebtorDetailsJson(string so_string)
        {
            List<DebtorId> list = JsonConvert.DeserializeObject<List<DebtorId>>(so_string);
            return Json(await DebtQueryGenerator2.GetDetails(list));
        }

/*
        public async Task<ActionResult> DebtExport(Skrin.Models.Iss.Debt.SearchObject so)
        {
            try
            {
*/

        public async Task<ActionResult> DebtExport(string so_string)
        {
            Skrin.Models.Iss.Debt.SearchObject so;
            so = JsonConvert.DeserializeObject<Skrin.Models.Iss.Debt.SearchObject>(so_string);
            var company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(so.iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                so.step = "10000";

                var result = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDebtServerUri, "fssp", new DebtQueryGenerator2(so, company, adress, names).GetSearch());

                var details = await DebtQueryGenerator2.GetDetails( (from item in result.results select new DebtorId { id = item.id, ver = item.version }));

                var memoryStream = new MemoryStream();
                ExportExcel exp = new ExportExcel();
                exp.ExportDebt(details).SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.ms-excel", "debt_result.xlsx");
            }

            return Content("");
        }

        #region LoaderMetods

        /// <summary>
        /// Формирование списка Регионы
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<JsonResult> LoadRegions(string iss)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                DebtQueryGenerator2 qg = new DebtQueryGenerator2(company, adress, names);
                var qres = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDebtServerUri, "fssp", qg.GetRegions());

                var res = (
                    from r in qres.Aggs.Terms("regions").Buckets 
                    select new DebtElasticRegion() { 
                        region_id = r.Key, 
                        name = r.Terms("regionsnames").Buckets.First().Key 
                    }
                );
                return Json(res);
            }
            return null;
        }

        /// <summary>
        /// Формирование списка Предмет исполнения
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<JsonResult> LoadPredmet(string iss)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }

                DebtQueryGenerator2 qg = new DebtQueryGenerator2(company, adress, names);
                var qres = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDebtServerUri, "fssp", qg.GetPredmets());
                var res = (from item in qres.Aggs.Terms("predmets").Buckets where item.Key!="" select new DebtElasticPredmet() { predmet = item.Key });
                return Json(res);
            }

            return null;
        }

/*
        /// <summary>
        /// Формирование списка Адреса
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<JsonResult> LoadAddress(string iss)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                DebtQueryGenerator2 qg = new DebtQueryGenerator2(company, adress, names);
                var qres = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDBsearchServerUri, "fssp", qg.GetAdressess());
                var res = (List<DebtElasticAddress>)(from item in qres.Aggs.Terms("addresses").Buckets select item.Key);
                return Json(res);
            }

            return null;
        }

        /// <summary>
        /// Формирование списка Должник
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<JsonResult> LoadDebtor(string iss)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                DebtQueryGenerator2 qg = new DebtQueryGenerator2(company, adress, names);
                var qres = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDBsearchServerUri, "fssp", qg.GetDebtors());
                var res = (List<DebtElasticDebtor>)(from item in qres.Aggs.Terms("debtors").Buckets select item.Key);
                return Json(res);
            }

            return null;
        }
 */ 

        /// <summary>
        /// Формирование сводной таблицы
        /// </summary>
        /// <param name="so">SearchObject</param>
        public async Task<JsonResult> LoadSummaryTable(Skrin.Models.Iss.Debt.SearchObject so)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(so.iss);
            if (company != null)
            {
                var adress = await Skrin.BLL.Root.SqlUtiltes.GetAdressAsync(company.OGRN);
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }

//                DebtQueryGenerator2 qg = new DebtQueryGenerator2(company, adress, names);
                var qres = await Elastic.OpenQuery<DebtorListItem>(Configs.ElasticDebtServerUri, "fssp", (new DebtQueryGenerator2(so, company, adress, names)).GetSummaryTable());

                var res = new List<DebtElasticSummary>();
                foreach (var r in qres.Aggs.DateHistogram("summarytable").Buckets)
                {
                    long? nc = 0;
                    double? ns = 0;
                    long? oc = 0;
                    double? os = 0;
                    foreach (var v in r.Terms("versions").Buckets)
                    {
                        if (v.Key == "0")
                        {
                            nc = v.DocCount;
                            ns = v.Sum("summofdebts").Value;
                        }
                        if (v.Key == "1")
                        {
                            oc = v.DocCount;
                            os = v.Sum("summofdebts").Value;
                        }
                    }
                    res.Add(new DebtElasticSummary{
                        year = r.Date.Year,
                        nowcnt = nc,
                        nowsum = ns,
                        oldcnt = oc,
                        oldsum = os
                    });
                }
                return Json(res);
            }
            return null;
        }

/*
        /// <summary>
        /// Формирование списка keywords
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<ActionResult> LoadKeywords(string iss)
        {
            CompanyData company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(iss);
            string keywords = "";
            if (company != null)
            {
                var names = await Skrin.BLL.Root.SqlUtiltes.GetCompanyNamesAsync(company.OGRN, company.INN);
                if (names == null || names.Count == 0)
                {
                    names = new List<DebtorName>();
                    var name = new DebtorName();
                    name.longname = company.SearchedName;
                    name.shortname = company.SearchedName2;
                    names.Add(name);
                }
                foreach (var name in names)
                {
                    if (name != null)
                    {
                        keywords += name.longname + " ";
                        keywords += name.shortname + " ";
                    }
                }
                return Content(keywords.Trim());
            }
            return Content("");
        }
 */ 
        #endregion
    }
}