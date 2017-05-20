using Skrin.BLL.Iss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL;
using Skrin.BLL.Root;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.BLL.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Skrin.Controllers.Iss
{
    public class PravoController : Controller
    {
        public async Task<ActionResult> GetCourtsAsync(PravoSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                PravoQueryGenerator pg = new PravoQueryGenerator(so, company);
                QueryObject qo = pg.GetQueryCourts(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetDtypesAsync(PravoSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                PravoQueryGenerator pg = new PravoQueryGenerator(so, company);
                QueryObject qo = pg.GetQueryDtypes(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> PravoSearchAsync(PravoSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
                if (company != null)
                {
                    PravoQueryGenerator pg = new PravoQueryGenerator(so, company);
                    QueryObject qo = pg.GetPravoQuerySearch(so);
                    UnionSphinxClient client = new UnionSphinxClient(qo);
                    return Content(client.SearchResult());
                }
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
                if (company != null)
                {
                    PravoQueryGenerator pg = new PravoQueryGenerator(so, company);
                    QueryObject qo = pg.GetPravoIpQuerySearch(so);
                    UnionSphinxClient client = new UnionSphinxClient(qo);
                    return Content(client.SearchResult());
                }
            }
            return Content("");
        }

        public ActionResult SummaryTable(string ticker)
        {
            try
            {
                SummaryData sumdata = PravoQueryGenerator.GetSummaryData(ticker);
                return Json(sumdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetMessage(string ids, string ticker, int src)
        {
            if (ids != null)
            {
                if (ticker == null || ticker == "undefined") { ticker = ""; }
                if (src == 1)
                {
                    XSLGenerator g = new XSLGenerator("skrin_content_output..ShowCase2", new Dictionary<string, object> { { "@id", ids }, { "@iss", ticker } }, "tab_content/pravo/showcase2", null);
                    return Content(await g.GetResultAsync());
                }
                else
                {
                    XSLGenerator g = new XSLGenerator("skrin_content_output..ShowCase", new Dictionary<string, object> { { "@id", ids }, { "@iss", ticker } }, "tab_content/pravo/showcase", null);
                    return Content(await g.GetResultAsync());
                    //return View("BancruptcyMessage", await ContentRepository.GetBancruptcyMessage(ids, ticker));
                }
            }
            return Content("");
        }

        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> GetCourts_elasticAsync(PravoSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                string url = "/pravo/case/_search";
                string json = GetQueryCourts(so, company);

                ElasticClient ec = new ElasticClient();
                JObject r = ec.GetQuery(url, json);

                JArray a = new JArray();
                for (int i = 0; i < r["aggregations"]["agg"]["buckets"].Count(); i++)
                {
                    JObject item = new JObject();
                    item.Add("cname", new JValue((string)r["aggregations"]["agg"]["buckets"][i]["key"]));
                    a.Add(item);
                }

                JObject res = new JObject();
                res.Add("results", a);

                return Content(res.ToString());
            }
            return Content("");
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> GetDtypes_elasticAsync(PravoSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                string url = "/pravo/case/_search";
                string json = GetQueryDTypes(so, company);

                ElasticClient ec = new ElasticClient();
                JObject r = ec.GetQuery(url, json);
                
                JArray a = new JArray();
                for (int i = 0; i < r["aggregations"]["nst"]["agg"]["buckets"].Count(); i++)
                {
                    JObject item = new JObject();
                    item.Add("ctype_id", new JValue((string)r["aggregations"]["nst"]["agg"]["buckets"][i]["key"]));
                    item.Add("ctype_name", new JValue((string)r["aggregations"]["nst"]["agg"]["buckets"][i]["agg2"]["buckets"][0]["key"]));
                    a.Add(item);
                }

                JObject res = new JObject();
                res.Add("results", a);

                return Content(res.ToString());
            }
            return Content("");
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> PravoSearch_elasticAsync(PravoSearchObject so)
        {
            so.kw = (!String.IsNullOrEmpty(so.kw)) ? so.kw : "";
            so.job_no = (!String.IsNullOrEmpty(so.job_no)) ? so.job_no : "";
            so.ac_name = (!String.IsNullOrEmpty(so.ac_name) && so.ac_name != "undefined") ? so.ac_name : "-1";
            so.dtype = (!String.IsNullOrEmpty(so.dtype) && so.dtype != "undefined") ? so.dtype : "-1";
            so.dcateg_id = (!String.IsNullOrEmpty(so.dcateg_id) && so.dcateg_id != "undefined") ? so.dcateg_id : "-1";

            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                string url = "/pravo/case/_search";
                string json = GetQuerySearch(so, company);
                //System.IO.File.AppendAllText("c:\\net\\1.txt", json + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
                //return Content(json);

                ElasticClient ec = new ElasticClient();
                JObject r = ec.GetQuery(url, json);

                return Content(r.ToString());
            }
            return Content("");
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> SummaryTable_elasticAsync(PravoSearchObject so)
        {
            so.kw = (!String.IsNullOrEmpty(so.kw)) ? so.kw : "";
            so.job_no = (!String.IsNullOrEmpty(so.job_no)) ? so.job_no : "";
            so.ac_name = (!String.IsNullOrEmpty(so.ac_name) && so.ac_name != "undefined") ? so.ac_name : "-1";
            so.dtype = (!String.IsNullOrEmpty(so.dtype) && so.dtype != "undefined") ? so.dtype : "-1";
            so.dcateg_id = (!String.IsNullOrEmpty(so.dcateg_id) && so.dcateg_id != "undefined") ? so.dcateg_id : "-1";
            string dtype = so.dtype;
            try
            {
                string url = "/pravo/case/_search";
                ElasticClient ec = new ElasticClient();
                SummaryData sumdata = new SummaryData();
                sumdata.YearsData = new List<YearSummary>();

                CompanyData company = await SqlUtiltes.GetCompanyAsync(so.iss);

                if (dtype == "-1" || dtype.IndexOf("0") >= 0)
                {
                    so.dtype = "0";
                    string json = GetQuerySumm(so, company);
                    //System.IO.File.AppendAllText("c:\\net\\1.txt", json + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));

                    JObject r = ec.GetQuery(url, json);
                    for (int i = 0; i < r["aggregations"]["agg"]["buckets"].Count(); i++)
                    {
                        YearSummary item = new YearSummary();
                        item.year = int.Parse((string)r["aggregations"]["agg"]["buckets"][i]["key_as_string"]);
                        item.icnt = (int)r["aggregations"]["agg"]["buckets"][i]["doc_count"];
                        item.isumma = (decimal)r["aggregations"]["agg"]["buckets"][i]["agg2"]["value"];
                        item.ocnt = 0;
                        item.osumma = 0;

                        sumdata.YearsData.Add(item);
                    }
                }

                if (dtype == "-1" || dtype.IndexOf("1") >= 0)
                {
                    so.dtype = "1";
                    string json = GetQuerySumm(so, company);

                    JObject r = ec.GetQuery(url, json);
                    for (int i = 0; i < r["aggregations"]["agg"]["buckets"].Count(); i++)
                    {
                        int year = int.Parse((string)r["aggregations"]["agg"]["buckets"][i]["key_as_string"]);
                        YearSummary item = null;

                        for (int j = 0; j < sumdata.YearsData.Count(); j++)
                        {
                            if (sumdata.YearsData[j].year == year)
                            {
                                item = sumdata.YearsData[j];
                                break;
                            }
                            if (sumdata.YearsData[j].year < year)
                            {
                                item = new YearSummary();
                                item.year = year;
                                item.icnt = 0;
                                item.isumma = 0;
                                sumdata.YearsData.Insert(j, item);
                                break;
                            }
                        }
                        if (item == null)
                        {
                            item = new YearSummary();
                            item.year = year;
                            item.icnt = 0;
                            item.isumma = 0;
                            sumdata.YearsData.Add(item);
                        }

                        item.ocnt = (int)r["aggregations"]["agg"]["buckets"][i]["doc_count"];
                        item.osumma = (decimal)r["aggregations"]["agg"]["buckets"][i]["agg2"]["value"];
                    }
                }

                return Json(sumdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> PravoExport_elasticAsync(string so_string)
        {
                PravoSearchObject so;
                try
                {
                    so = JsonConvert.DeserializeObject<PravoSearchObject>(so_string);
                    so.kw = (!String.IsNullOrEmpty(so.kw)) ? so.kw : "";
                    so.job_no = (!String.IsNullOrEmpty(so.job_no)) ? so.job_no : "";
                    so.ac_name = (!String.IsNullOrEmpty(so.ac_name) && so.ac_name != "undefined") ? so.ac_name : "-1";
                    so.dtype = (!String.IsNullOrEmpty(so.dtype) && so.dtype != "undefined") ? so.dtype : "-1";
                    so.dcateg_id = (!String.IsNullOrEmpty(so.dcateg_id) && so.dcateg_id != "undefined") ? so.dcateg_id : "-1";

                    CompanyData company = new CompanyData();
                    if (so.isCompany)
                    {
                        company = await SqlUtiltes.GetCompanyAsync(so.iss);
                    }
                    else
                    {
                        company = await SqlUtiltes.GetIPAsync(so.iss);
                    }

                        if (company != null)
                        {
                            string url = "/pravo/case/_search";
                            string json = GetQueryExport(so, company);
                            ElasticClient ec = new ElasticClient();
                            JObject r = ec.GetQuery(url, json);

                            string result = r.ToString();

                            List<PravoDetail> details = GetPravoDetails(r, company.INN, company.OGRN);
                            var memoryStream = new MemoryStream();
                            ExportExcel exp = new ExportExcel();
                            exp.ExportPravo(details, company).SaveAs(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            return File(memoryStream, "application/vnd.ms-excel", "pravo_result.xlsx");
                        }

                        return Content("");
                }
                catch
                {
                    return HttpNotFound();
                }
        }

        private string GetQueryCourts(PravoSearchObject so, CompanyData co)
        {
            string res = "";

            res = "{" + GetQueryMain(so, co) +
                ",\"aggs\" : {\"agg\": {\"terms\": {\"field\": \"court_name\" , \"size\":0, \"order\" : {\"_term\": \"asc\" }}}} " +
                ",\"size\": \"0\" }";

            return res;
        }
        private string GetQueryDTypes(PravoSearchObject so, CompanyData co)
        {
            string res = "";

            res = "{" + GetQueryMain(so, co) +
                ",\"aggs\" : { \"nst\" : { \"nested\" : {\"path\": \"side\" } " +
                "  ,\"aggs\" : {\"agg\": {\"terms\": {\"field\": \"side.side_type_id\", \"size\":0, \"order\" : {\"_term\": \"asc\" }} " +
                "    ,\"aggs\" : {\"agg2\": {\"terms\": {\"field\": \"side.side_type_name\"}}} }} }} " +
                ",\"size\": \"0\" }";

            return res;
        }
        private string GetQuerySearch(PravoSearchObject so, CompanyData co)
        {
            string res = "";
            int fr = (so.page - 1) * so.rcount;

            res = "{" + GetQueryMain(so, co, true);
            res += ",\"size\": \"" + so.rcount + "\", \"from\": \"" + fr + "\"";
            res += ",\"sort\": [{\"reg_date\": {\"order\": \"desc\"}}] }";

            return res;
        }
        private string GetQuerySumm(PravoSearchObject so, CompanyData co)
        {
            string res = "";

            res = "{" + GetQueryMain(so, co, true);
            res += ",\"aggs\": {\"agg\": {\"date_histogram\": {\"field\": \"reg_date\", \"interval\": \"year\", \"format\": \"yyyy\", \"order\" : {\"_key\": \"desc\" }} ";
            res += "  ,\"aggs\": {\"agg2\": {\"sum\": {\"field\": \"case_sum\"} } } }} ";
            res += ",\"size\": \"0\" }";

            return res;
        }
        private string GetQueryMain(PravoSearchObject so, CompanyData co, bool ismain = false)
        {
            string res = "";
            string must = "";
            string filter = "";
            string must_not = "";

            DateTime? dfrom = null;
            DateTime? dto = null;
            try { dfrom = DateTime.ParseExact(so.dfrom, "dd.MM.yyyy", null); }
            catch { }
            try { dto = DateTime.ParseExact(so.dto, "dd.MM.yyyy", null); }
            catch { }

            if (must != "") must += ",";
            must += "{\"nested\": {\"path\": \"side\", \"query\": {\"bool\": ";
            must += "{\"should\": [ ";
            if (so.rmode == "-1")
            {
                if (co.INN != "")
                    must += "{\"term\": {\"side.inn\": \"" + co.INN + "\"}},";
                if (co.OGRN != "")
                    must += "{\"term\": {\"side.ogrn\": \"" + co.OGRN + "\"}},";
                //if (co.SearchedName != "")
                //    must += "{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName) + "\", \"operator\": \"and\"}}},";
                //if (co.SearchedName2 != "")
                //    must += "{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName2) + "\", \"operator\": \"and\"}}},";
                if (co.SearchedName != "")
                    must += "{\"bool\":{\"must\":{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName).Replace("\\", "\\\\") + "\", \"operator\": \"and\"}}}, \"must_not\": [{\"exists\": {\"field\":\"side.inn\"}}, {\"exists\": {\"field\":\"side.ogrn\"}}] }},";
                if (co.SearchedName2 != "")
                    must += "{\"bool\":{\"must\":{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName2).Replace("\\", "\\\\") + "\", \"operator\": \"and\"}}}, \"must_not\": [{\"exists\": {\"field\":\"side.inn\"}}, {\"exists\": {\"field\":\"side.ogrn\"}}] }},";
                if (must.Substring(must.Length - 1) == ",")
                    must = must.Substring(0, must.Length - 1);
            }
            else
            {
                if (so.rmode.IndexOf("1") >= 0)
                {
                    must += "{\"bool\":{\"must\":[{\"term\": {\"side.inn\": \"" + co.INN + "\"}}, {\"term\": {\"side.ogrn\": \"" + co.OGRN + "\"}}] }},";
                }

                if (so.rmode.IndexOf("2") >= 0)
                {
                    if (co.INN != "")
                        must += "{\"bool\":{\"must\":{\"term\": {\"side.inn\": \"" + co.INN + "\"}}, \"must_not\": {\"exists\": {\"field\":\"side.ogrn\"}} }},";
                    if (co.OGRN != "")
                        must += "{\"bool\":{\"must\":{\"term\": {\"side.ogrn\": \"" + co.OGRN + "\"}}, \"must_not\": {\"exists\": {\"field\":\"side.inn\"}} }},";
                }

                if (so.rmode.IndexOf("3") >= 0)
                {
                    if (co.SearchedName != "")
                        must += "{\"bool\":{\"must\":{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName).Replace("\\", "\\\\") + "\", \"operator\": \"and\"}}}, \"must_not\": [{\"exists\": {\"field\":\"side.inn\"}}, {\"exists\": {\"field\":\"side.ogrn\"}}] }},";
                    if (co.SearchedName2 != "")
                        must += "{\"bool\":{\"must\":{\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(co.SearchedName2).Replace("\\", "\\\\") + "\", \"operator\": \"and\"}}}, \"must_not\": [{\"exists\": {\"field\":\"side.inn\"}}, {\"exists\": {\"field\":\"side.ogrn\"}}] }},";
                    if (co.INN != "")
                        must += "{\"bool\":{\"must\":{\"term\": {\"side.inn\": \"" + co.INN + "\"}}, \"filter\":{\"exists\": {\"field\":\"side.ogrn\"}}, \"must_not\": {\"term\": {\"side.ogrn\": \"" + co.OGRN + "\"}} }},";
                    if (co.OGRN != "")
                        must += "{\"bool\":{\"must\":{\"term\": {\"side.ogrn\": \"" + co.OGRN + "\"}}, \"filter\":{\"exists\": {\"field\":\"side.inn\"}}, \"must_not\": {\"term\": {\"side.inn\": \"" + co.INN + "\"}} }},";
                }
            }
            if (must.Substring(must.Length - 1) == ",")
                must = must.Substring(0, must.Length - 1);
            must += "], \"minimum_should_match\" : 1";
            if ((so.dtype != "-1") && ismain)
                must += ",\"filter\": {\"terms\": {\"side.side_type_id\": [" + so.dtype + "] } }";
            must += "}";
            must += "}, \"inner_hits\": {} }} ";

            if ((dfrom != null || dto != null) && ismain)
            {
                if (filter != "") filter += ",";
                filter += " {\"range\": {\"reg_date\": {";
                if (dfrom != null) filter += "\"gte\": \"" + String.Format("{0:dd.MM.yyyy}", dfrom) + "\"";
                if (dfrom != null && dto != null) filter += ",";
                if (dto != null) filter += "\"lte\": \"" + String.Format("{0:dd.MM.yyyy}", dto) + "\"";
                filter += "}}}";
            }

            if (ismain)
            {
                if (so.kw != "")
                {
                    if (must != "") must += ",";
                    must += "{\"nested\": {\"path\": \"side\", \"query\": ";
                    must += "{\"bool\": {\"must\": {\"multi_match\": {\"fields\": [\"side.name\", \"side.inn\", \"side.ogrn\"], \"query\": \"" + so.kw.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\", \"type\": \"cross_fields\", \"operator\": \"and\" }}}}";
                    must += "}} ";
                }

                if (so.job_no != "")
                {
                    if (must != "") must += ",";
                    must += " {\"match\": {\"instance.instance_reg_no\": {\"query\": \"" + ClearRegno(so.job_no.Replace(" ", "").ToUpper()).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\", \"operator\": \"and\" } }}";
                }

                if (so.ac_name != "-1")
                {
                    if (filter != "") filter += ",";
                    filter += " {\"terms\": {\"court_name\":[\"" + so.ac_name.Replace(",", "\",\"") + "\"] }}";
                }

                if (so.dcateg_id != "-1")
                {
                    if (filter != "") filter += ",";
                    filter += " {\"terms\": {\"disput_type_cat_id\":[" + so.dcateg_id + "] }}";
                }
            }

            res = "\"query\": {\"bool\": { ";
            res += "\"must\": [" + must + "] ";
            if (filter != "")
            {
                if (must != "") res += ",";
                res += "\"filter\" : [  " + filter + " ] ";
            }
            if (must_not != "")
            {
                if ((must != "") || (filter != "")) res += ",";
                res += "\"must_not\" : [ " + must_not + " ] ";
            }
            res += " } } ";

            return res;
        }
        static string ClearRegno(string reg_no)
        {
            string[] eng_char = { "A", "B", "C", "E", "H", "K", "M", "O", "P", "T", "X", " " };
            string[] rus_char = { "А", "В", "С", "Е", "Н", "К", "М", "О", "Р", "Т", "Х", "" };

            reg_no = reg_no.Trim().ToUpper();

            for (int i = 0; i < eng_char.Length; i++)
                reg_no = reg_no.Replace(eng_char[i], rus_char[i]);

            return reg_no;
        }

        private string GetQueryExport(PravoSearchObject so, CompanyData co)
        {
            string res = "";

            res = "{" + GetQueryMain(so, co, true);
            res += ",\"size\": \"" + so.rcount + "\", \"from\": \"0\"";
            res += ",\"sort\": [{\"reg_date\": {\"order\": \"desc\"}}] }";

            return res;
        }

        private List<PravoDetail> GetPravoDetails(JObject r, string inn, string ogrn)
        {
            List<PravoDetail> ret = new List<PravoDetail>();
            for (int i = 0; i < r["hits"]["hits"].Count(); i++)
            {
                PravoDetail detail = new PravoDetail();
                detail.reg_date = (string)r["hits"]["hits"][i]["_source"]["reg_date"];
                detail.reg_no = (string)r["hits"]["hits"][i]["_source"]["reg_no"];
                detail.disput_type_categ = (string)r["hits"]["hits"][i]["_source"]["disput_type_cat_name"];
                detail.case_sum = (string)r["hits"]["hits"][i]["_source"]["case_sum"];
                detail.cname = (string)r["hits"]["hits"][i]["_source"]["court_name"];
                detail.side_type_name = (string)r["hits"]["hits"][i]["inner_hits"]["side"]["hits"]["hits"][0]["_source"]["side_type_name"];
                List<string> ist_list = new List<string>();
                List<string> otv_list = new List<string>();
                detail.rmode = 3;
                for (int j = 0; j < r["hits"]["hits"][i]["_source"]["side"].Count(); j++)
                {
                    int side_type_id = (int)r["hits"]["hits"][i]["_source"]["side"][j]["side_type_id"];
                    switch(side_type_id)
                    {
                        case 0:
                            ist_list.Add((string)r["hits"]["hits"][i]["_source"]["side"][j]["name"]);
                            break;
                        case 1:
                            otv_list.Add((string)r["hits"]["hits"][i]["_source"]["side"][j]["name"]);
                            break;
                    }
                    if ((string)r["hits"]["hits"][i]["_source"]["side"][j]["inn"] == inn && (string)r["hits"]["hits"][i]["_source"]["side"][j]["ogrn"] == ogrn) { detail.rmode = 1; }
                    if ((string)r["hits"]["hits"][i]["_source"]["side"][j]["inn"] == inn && (string)r["hits"]["hits"][i]["_source"]["side"][j]["ogrn"] == null && detail.rmode > 2) { detail.rmode = 2;}
                    if ((string)r["hits"]["hits"][i]["_source"]["side"][j]["ogrn"] == ogrn && (string)r["hits"]["hits"][i]["_source"]["side"][j]["inn"] == null && detail.rmode > 2) { detail.rmode = 2;}
                }
                detail.ext_ist_list = string.Join(", ", ist_list);
                detail.ext_otv_list = string.Join(", ", otv_list);
                if(detail != null)
                {
                    ret.Add(detail);
                }
            }

            return ret;
        }
    }
}