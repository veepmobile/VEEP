using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Skrin.Models;
using Skrin.Models.Iss;
using Skrin.BLL;
using Skrin.Controllers;
using Skrin.BLL.DebtBLL;
using Skrin.Models.Iss.Debt;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Skrin.BLL.Search;
using System.Text.RegularExpressions;

namespace Skrin.Controllers.Debt
{
    public class DebtController : BaseController
    {
        //protected string es_url_var = "elasticsearcher_server_test";
        protected string url = "/fssp/debt/_search";

        public List<string> SearchJson(string query, string port = "9340", string charaster_Set = "cp1251")
        {
            try
            {
                DebtSphynxSearcher searcher = new DebtSphynxSearcher(query, port, charaster_Set);
                List<string> res = searcher.SearchJson();
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActionResult> DebtorSearchAsync(Skrin.Models.Iss.Debt.SearchObject so)
        {
            List<JObject> rdop = new List<JObject>();
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
                DebtQueryGenerator qg = new DebtQueryGenerator(so, company, adress, names);
                string json = qg.GetQuery();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                // + Костыль для лэйблов!
                switch(so.queryoption)
                {
                    case TypeOfQuery.AdressOrName:
                        so.queryoption = TypeOfQuery.AdressAndName;
                        qg = new DebtQueryGenerator(so, company, adress, names);
                        json = qg.GetListOfIdAndVersion();
                        rdop.Add(await ec.GetQueryAsync(url, json));
                        so.queryoption = TypeOfQuery.NameNoInter;
                        qg = new DebtQueryGenerator(so, company, adress, names);
                        json = qg.GetListOfIdAndVersion();
                        rdop.Add(await ec.GetQueryAsync(url, json));
                        break;
                    case TypeOfQuery.AdressXorName:
                        so.queryoption = TypeOfQuery.NameNoInter;
                        qg = new DebtQueryGenerator(so, company, adress, names);
                        json = qg.GetListOfIdAndVersion();
                        rdop.Add(await ec.GetQueryAsync(url, json));
                        break;
                    case TypeOfQuery.Adress:
                        so.queryoption = TypeOfQuery.AdressAndName;
                        qg = new DebtQueryGenerator(so, company, adress, names);
                        json = qg.GetListOfIdAndVersion();
                        rdop.Add(await ec.GetQueryAsync(url, json));
                        break;
                    case TypeOfQuery.Name:
                        so.queryoption = TypeOfQuery.AdressAndName;
                        qg = new DebtQueryGenerator(so, company, adress, names);
                        json = qg.GetListOfIdAndVersion();
                        rdop.Add(await ec.GetQueryAsync(url, json));
                        break;
                }
                if (r["hits"] != null)
                {
                    if (rdop.Count == 0)
                    {
                        return Content("{\"resultat\":[" + r["hits"].ToString() + "]}");
                    }
                    else 
                    {
                        string rdopstring = "";
                        foreach (var obj in rdop)
                        {
                            if (obj["hits"] != null && obj["hits"]["hits"] != null)
                            {
                                rdopstring += obj["hits"]["hits"].ToString() + ",";
                            }
                        }
                        return Content("{\"resultat\":[" + r["hits"].ToString() + (!string.IsNullOrEmpty(rdopstring) ? "," + rdopstring.TrimEnd(',') : "") + "]}");
                    }
                }
                // - Костыль для лэйблов!
                else
                {
                    return Content("");
                }
            }
            return Content("");
        }
        public async Task<ActionResult> DebtorSearchPODFT(Skrin.Models.Iss.Debt.SearchObject so)
        {
            List<JObject> rdop = new List<JObject>();
                var company = await Skrin.BLL.Root.SqlUtiltes.GetCompanyAsync(so.iss);
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
                DebtQueryGenerator qg = new DebtQueryGenerator(so, company, adress, names);
                string json = qg.GetQuery();
                return Content(json);
        }
        // - для skrin - параметр список id через запятую
        // параметр список vers (0 - FSSP, 1 - FSSP_Archive) через запятую
        public JsonResult DebtorDetailsJson(string ids, string vers)
        {
            try
            {
                if (!String.IsNullOrEmpty(ids) && !String.IsNullOrEmpty(vers))
                {
                    var list = ids.Split(',');
                    var listvers = vers.Split(',');
                    if (list.Length == listvers.Length)
                    {
                        var debtor = DebtSqlData.GetDebtorDetails(list, listvers);
                        return Json(debtor, JsonRequestBehavior.AllowGet);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> DebtExport(string so_string)
        {
            Skrin.Models.Iss.Debt.SearchObject so;
            try
            {
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
                    DebtQueryGenerator qg = new DebtQueryGenerator(so, company, adress, names);
                    string json = qg.GetExport();
                    ElasticClient ec = new ElasticClient();
                    JObject r = ec.GetQuery(url, json);
                    List<string> listid = new List<string>();
                    List<string> listver = new List<string>();
                    if (r["hits"]["hits"] != null)
                    {
                        for (int i = 0; i < r["hits"]["hits"].Count(); i++)
                        {
                            listid.Add(r["hits"]["hits"][i]["_source"]["id"].ToString());
                            listver.Add(r["hits"]["hits"][i]["_source"]["version"].ToString());
                        }
                    }
                    var details = DebtSqlData.GetDebtorDetails(listid.ToArray(), listver.ToArray());
                    var memoryStream = new MemoryStream();
                    ExportExcel exp = new ExportExcel();
                    exp.ExportDebt(details).SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream, "application/vnd.ms-excel", "debt_result.xlsx");
                }

                return Content("");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        #region LoaderMetods

        /// <summary>
        /// Формирование списка Регионы
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<ActionResult> LoadRegions(string iss)
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
                DebtQueryGenerator qg = new DebtQueryGenerator(company, adress, names);
                string json = qg.GetRegions();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                JArray res = new JArray();
                int n = r["aggregations"]["regions"]["buckets"].Count();
                for (int i = 0; i < n; i++)
                {
                    JObject region = new JObject();
                    string reg_id = (string)r["aggregations"]["regions"]["buckets"][i]["key"];
                    region.Add("region_id", reg_id);
                    region.Add("name", (string)r["aggregations"]["regions"]["buckets"][i]["regionsnames"]["buckets"][0]["key"]);
                    res.Add(region);
                }
                return Content(res.ToString());
            }
            return Content("");
        }

        /// <summary>
        /// Формирование списка Предмет исполнения
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<ActionResult> LoadPredmet(string iss)
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
                DebtQueryGenerator qg = new DebtQueryGenerator(company, adress, names);
                string json = qg.GetPredmets();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                JArray res = new JArray();
                int n = r["aggregations"]["predmets"]["buckets"].Count();
                for (int i = 0; i < n; i++)
                {
                    JObject predmet = new JObject();
                    predmet.Add("predmet", (string)r["aggregations"]["predmets"]["buckets"][i]["key"]);
                    res.Add(predmet);
                }
                return Content(res.ToString());
            }

            return Content("");
        }

        /// <summary>
        /// Формирование списка Адреса
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<ActionResult> LoadAddress(string iss)
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
                DebtQueryGenerator qg = new DebtQueryGenerator(company, adress, names);
                string json = qg.GetAdressess();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                JArray res = new JArray();
                int n = r["aggregations"]["addresses"]["buckets"].Count();
                for (int i = 0; i < n; i++)
                {
                    JObject predmet = new JObject();
                    predmet.Add("address", (string)r["aggregations"]["addresses"]["buckets"][i]["key"]);
                    res.Add(predmet);
                }
                return Content(res.ToString());
            }

            return Content("");
        }

        /// <summary>
        /// Формирование списка Должник
        /// </summary>
        /// <param name="iss">идентификатор ЮЛ</param>
        public async Task<ActionResult> LoadDebtor(string iss)
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
                DebtQueryGenerator qg = new DebtQueryGenerator(company, adress, names);
                string json = qg.GetDebtors();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                JArray res = new JArray();
                int n = r["aggregations"]["debtors"]["buckets"].Count();
                for (int i = 0; i < n; i++)
                {
                    JObject predmet = new JObject();
                    predmet.Add("debtor", (string)r["aggregations"]["debtors"]["buckets"][i]["key"]);
                    res.Add(predmet);
                }
                return Content(res.ToString());
            }

            return Content("");
        }

        /// <summary>
        /// Формирование сводной таблицы
        /// </summary>
        /// <param name="so">SearchObject</param>
        public async Task<ActionResult> LoadSummaryTable(Skrin.Models.Iss.Debt.SearchObject so)
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
                DebtQueryGenerator qg = new DebtQueryGenerator(so, company, adress, names);
                string json = qg.GetSummaryTable();
                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                JArray res = new JArray();
                int n = r["aggregations"]["summarytable"]["buckets"].Count();
                for (int i = 0; i < n; i++)
                {
                    JObject predmet = new JObject();
                    predmet.Add("year", (string)r["aggregations"]["summarytable"]["buckets"][i]["key_as_string"]);
                    int m = r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"].Count();
                    for (int j = 0; j < m; j++)
                    {
                        if ((int)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["key"] == 0)
                        {
                            predmet.Add("nowcnt", (string)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["doc_count"]);
                            predmet.Add("nowsum", (string)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["summofdebts"]["value"]);
                        }
                        else if ((int)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["key"] == 1)
                        {
                            predmet.Add("oldcnt", (string)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["doc_count"]);
                            predmet.Add("oldsum", (string)r["aggregations"]["summarytable"]["buckets"][i]["versions"]["buckets"][j]["summofdebts"]["value"]);
                        }
                    }
                    res.Add(predmet);
                }
                return Content(res.ToString());
            }

            return Content("");
        }

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
        #endregion

    }
}