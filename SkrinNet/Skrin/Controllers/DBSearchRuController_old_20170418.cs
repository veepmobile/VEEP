using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skrin.Models;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.BLL.Iss;
using Skrin.BLL.Search;
using serch_contra_bll.StopLightFreeEgrul;
using serch_contra_bll.ActionStoplight;

namespace Skrin.Controllers
{
    public class DBSearchRuController : Controller
    {

        private enum Key { canShowAll, canExport, canAddToGroup };

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        private static string es_server_var = "elasticsearch_dbsearch_server";
        private static string es_index = ConfigurationManager.AppSettings["elasticsearch_dbsearch_index"];

        static DBSearchRuController()
        {
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.Emitent | AccessType.Bloom | AccessType.Deal | AccessType.KaPoln);
            roles.Add(Key.canAddToGroup.ToString(), AuthenticateSqlUtilites.GetGroupRoles().Value);
        }

        // GET: DBSearchRu
        public ActionResult Companies(string address, string name)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/dbsearchru/companies/";
            ViewBag.Title = "СКРИН-Контрагент: база данных юридических лиц (предприятий) России. Поиск по реквизитам.";
            ViewBag.Description = "База данных юридических лиц (предприятий) России. Поиск по реквизитам.";

            List<string> insert_searches = new List<string>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                insert_searches.Add("$('#addr').val('" + address + "');");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                insert_searches.Add("$('#comp').val('" + name + "');");
            }

            return View(insert_searches);
        }

        public async Task<ActionResult> CompaniesDoSearch(CompaniesSearchObject so)
        {
            //проверим permition на сервере
            UserSession us = HttpContext.GetUserSession();

            await ModifitySOAsync(so, us);

            try
            {
                string url = "/" + es_index + "/profile/_search";
                string json = GetQuery(so);
                //System.IO.File.AppendAllText("c:\\net\\1.txt", json + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
                //return Content(json);

                ElasticClient ec = new ElasticClient(es_server_var);
                JObject r = ec.GetQuery(url, json);
                return Content(r.ToString());
            }
            catch (Exception ex)
            {
                return Content(string.Format("<div style=\"display:none\" id=\"fnd\" val=\"-1\">{0}</div>Ошибка. Попробуйте позже. {0}", ex.Message));
            }
        }

        private async Task ModifitySOAsync(CompaniesSearchObject so, UserSession us)
        {

            //so.is_emitent = (pr.IsSet(RoleTypes.ShowEmitent) && !pr.IsSet(RoleTypes.ShowAll));

            so.regions = await SearchRepository.GetRegsAsync(so.regions, so.is_okato);
            so.industry = string.IsNullOrWhiteSpace(so.industry) ? "" : so.industry.Replace(',', '|');  //await SearchRepository.GetIndAsync(so.industry, so.is_okonh, so.ind_excl);
            so.okopf = await SearchRepository.GetOkopfAsync(so.okopf);
            so.okfs = await SearchRepository.GetOkfsAsync(so.okfs, true);

            so.company = !String.IsNullOrWhiteSpace(so.company) ? so.company.Replace(".", " ").Replace("%20", " ").Replace(" - ", " ").Replace("–", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("!", "").Replace("*", "").Replace("!", "").Replace("\'", " ").Replace("\"", " ").Replace("(", "").Replace(")", "").Replace("\t", " ") : "";
            //so.company = Helper.FullTextString(so.company);
            //so.company = so.company.Replace("*", "");
            so.phone = !String.IsNullOrWhiteSpace(so.phone) ? so.phone.Replace("%20", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("\t", " ") : "";
            so.address = !String.IsNullOrWhiteSpace(so.address) ? so.address.Replace("%20", " ").Replace("\t", " ") : "";
            so.address = so.address.Replace(".", " ").Replace(",", " ").Replace(" ул ", " ").Replace(" д ", " ").Replace(" к ", " ").Replace(" корп ", " ").Replace(" офис ", " ").Replace(" оф ", " ").Replace(" кв ", " ").Replace(" пр ", " ").Replace(" просп ", " ").Replace(" пер ", " ");
            so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Replace("\t", " ").Clear() : "";
            so.constitutor = !String.IsNullOrWhiteSpace(so.constitutor) ? so.constitutor.Replace("%20", " ").Replace("\t", " ").Clear() : "";
            so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
            so.industry = !String.IsNullOrWhiteSpace(so.industry) ? so.industry : "";
            so.okopf = !String.IsNullOrWhiteSpace(so.okopf) ? so.okopf : "";
            so.okfs = !String.IsNullOrWhiteSpace(so.okfs) ? so.okfs : "";
            so.trades = !String.IsNullOrWhiteSpace(so.trades) ? so.trades : "";
            so.kod = !String.IsNullOrWhiteSpace(so.kod) ? so.kod : "";
            so.kod = so.kod.Trim();
            so.dbeg = !String.IsNullOrWhiteSpace(so.dbeg) ? so.dbeg : "";
            so.dend = !String.IsNullOrWhiteSpace(so.dend) ? so.dend : "";
            so.top1000 = !String.IsNullOrEmpty(so.top1000) ? so.top1000 : "";
            so.group_name = !String.IsNullOrEmpty(so.group_name) ? so.group_name : "";
            so.fas = !String.IsNullOrEmpty(so.fas) ? so.fas : "";
            so.page_no = so.page_no == 0 ? 0 : so.page_no - 1;
        }

        public async Task<ActionResult> GetBones(string input)
        {
            try
            {
                string url = "/" + es_index + "/profile/_search";
                string json = GetQueryBones(input);
                ElasticClient ec = new ElasticClient(es_server_var);
                JObject r = await ec.GetQueryAsync(url, json);
                JArray a = new JArray();
                for (int i = 0; i < r["aggregations"]["agg"]["buckets"].Count(); i++)
                {
                    JObject item = new JObject();
                    item.Add("name", new JValue((string)r["aggregations"]["agg"]["buckets"][i]["key"]));
                    item.Add("cnt", new JValue((int)r["aggregations"]["agg"]["buckets"][i]["doc_count"]));
                    a.Add(item);
                }
                return Content(a.ToString());
            }
            catch (Exception e)
            {
                return Content("[]");
            }
        }

        public async Task<ActionResult> CompaniesDoSearchExcel(string so_string)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                CompaniesSearchObject so;
                try
                {
                    so = JsonConvert.DeserializeObject<CompaniesSearchObject>(so_string);
                    await ModifitySOAsync(so, us);
                    so.page_no = 0;
                    so.rcount = 10000;

                    string url = "/" + es_index + "/profile/_search";
                    string json = GetQuery(so);

                    ElasticClient ec = new ElasticClient(es_server_var);
                    JObject r = ec.GetQuery(url, json);

                    List<ULDetails> details = GetSearchDetails(r);
                    var memoryStream = new MemoryStream();
                    ExportExcel exp = new ExportExcel();
                    exp.ExportWorkbook(details).SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream, "application/vnd.ms-excel", "search_result.xlsx");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public async Task<ActionResult> CompaniesGetExcel(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                List<ULDetails> details = await SqlUtiltes.GetUlDetailsAsync(issuers);
                var memoryStream = new MemoryStream();
                ExportExcel exp = new ExportExcel();
                exp.ExportWorkbook(details).SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.ms-excel", "search_result.xlsx");
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult GetStopLight(string ogrn)
        {
            UserSession us = HttpContext.GetUserSession();
            JObject r = new JObject();

            int rating = 0;
            int factors_count = 0;

            if (us.UserId > 0)
            {
                StopLightData data = new StopLightData(ogrn, Configs.ConnectionString);
                if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Green)
                {
                    rating = 1;
                }
                if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Yellow)
                {
                    rating = 2;
                    factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
                }
                if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Red)
                {
                    rating = 3;
                    factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
                }
            }
            r.Add("rating", new JValue(rating));
            r.Add("factors_count", new JValue(factors_count));
            return Content(r.ToString());
        }

        public ActionResult GetActionStopLight(string ogrn, string inn)
        {
            UserSession us = HttpContext.GetUserSession();
            JObject r = new JObject();
            if (inn == null) inn = "";
            ActionStopLight data = null;
            if (us.UserId > 0)
            {
                data = new ActionStopLightCreator(ogrn, inn, Configs.ConnectionString).Create();
            }
            r.Add("total_count", new JValue(data == null ? null : data.TotalCount));
            return Content(r.ToString());
        }

        private string GetQuery(CompaniesSearchObject so)
        {
            string res = "";
            string must = "";
            string filter = "";
            string must_not = "";
            int fr = so.page_no * so.rcount;
            int rcount = so.rcount;

            if (fr + rcount > 10000) rcount = 10000 - fr;

            if (so.top1000 == "1")
            {
                fr = 0;
                rcount = 10000;
            }

            DateTime? reg_DBeg = null;
            DateTime? reg_DEnd = null;
            try { reg_DBeg = DateTime.ParseExact(so.dbeg, "dd.MM.yyyy", null); }
            catch { }
            try { reg_DEnd = DateTime.ParseExact(so.dend, "dd.MM.yyyy", null); }
            catch { }

            if (reg_DBeg != null || reg_DEnd != null)
            {
                if (reg_DEnd == null) reg_DEnd = DateTime.ParseExact("01.01.2100", "dd.MM.yyyy", null);
                filter += ", {\"range\": {\"reg_date\": {";
                if (reg_DBeg != null) filter += "\"gte\": \"" + String.Format("{0:dd.MM.yyyy}", reg_DBeg) + "\"";
                if (reg_DBeg != null && reg_DEnd != null) filter += ",";
                if (reg_DEnd != null) filter += "\"lte\": \"" + String.Format("{0:dd.MM.yyyy}", reg_DEnd) + "\"";
                filter += "}}}";
            }

            if (so.company != "")
            {
                string name = "";
                string opf = "";
                SplitOpf(so.company, out name, out opf);
                if (name != "")
                { 
                    must += ", {\"nested\": {\"path\": \"key_list\", \"query\": {\"bool\": {\"must\": [{\"match\": {\"key_list.key_value\": {\"query\": \"" + name.Replace("\\", "\\\\").Replace("\"", "") + "\", \"operator\" : \"and\"}}}" + (so.archive == 1 ? "" : ", {\"term\": {\"key_list.is_old\": false}}") + "] }}, \"inner_hits\": {\"sort\": {\"key_list.is_old\": {\"order\": \"asc\"}}} } } ";
                }
                if (opf != "")
                {
                    must += ", {\"nested\": {\"path\": \"key_list\", \"query\": {\"bool\": {\"must\": [{\"match\": {\"key_list.key_value\": \"" + opf + "\"}}" + (so.archive == 1 ? "" : ", {\"term\": {\"key_list.is_old\": false}}") + "] }} } }";
                }
            }

            if (so.ruler != "")
            {
                must += ", {\"nested\": {\"path\": \"manager_list\", \"query\": {\"bool\": {\"must\": [{\"multi_match\": {\"fields\": [\"manager_list.fio\", \"manager_list.inn\", \"manager_list.position\"], \"query\": \"" + so.ruler.Replace("\\", "\\\\").Replace("\"", "") + "\", \"type\": \"cross_fields\", \"operator\" : \"and\"}}" + (so.archive == 1 ? "" : ", {\"term\": {\"manager_list.is_old\": false}}") + "] }}, \"inner_hits\": {\"sort\": {\"manager_list.is_old\": {\"order\": \"asc\"}}} } } ";
            }

            if (so.constitutor != "")
            {
                must += ", {\"nested\": {\"path\": \"constitutor_list\", \"query\": {\"bool\": {\"must\": [{\"multi_match\": {\"fields\": [\"constitutor_list.name\", \"constitutor_list.inn\", \"constitutor_list.ogrn\"], \"query\": \"" + so.constitutor.Replace("\\", "\\\\").Replace("\"", "") + "\", \"type\": \"cross_fields\", \"operator\" : \"and\"}}" + (so.archive == 1 ? "" : ", {\"term\": {\"constitutor_list.is_old\": false}}") + "] }}, \"inner_hits\": {\"sort\": {\"constitutor_list.is_old\": {\"order\": \"asc\"}}} } } ";
            }

            if (so.address != "")
            {
                must += ", {\"nested\": {\"path\": \"address_list\", \"query\": {\"bool\": {\"must\": [{\"match\": {\"address_list.address\": {\"query\": \"" + so.address.Replace("\\", "\\\\").Replace("\"", "") + "\", \"operator\" : \"and\"}}}" + (so.archive == 1 ? "" : ", {\"term\": {\"address_list.is_old\": false}}") + "] }}, \"inner_hits\": {\"sort\": {\"address_list.is_old\": {\"order\": \"asc\"}}} } } ";
            }

            if (so.phone != "")
            {
                string phone = so.phone;
                phone = phone.Replace("+7", "8");
                if (phone.Length > 10)
                    if (phone.Substring(0, 1) == "7" || phone.Substring(0, 1) == "8") phone = phone.Substring(1);
                must += ", {\"nested\": {\"path\": \"phone_list\", \"query\": {\"bool\": {\"must\": [{\"wildcard\": {\"phone_list.phone\": \"*" + phone + "\"}}" + (so.archive == 1 ? "" : ", {\"term\": {\"phone_list.is_old\": false}}") + "] }}, \"inner_hits\": {\"sort\": {\"phone_list.is_old\": {\"order\": \"asc\"}}} } } ";
            }

            if (so.okopf != "")
            {
                string str = ", {\"terms\": {\"opf\":[" + so.okopf.Replace("|", ",") + "] }}";
                if (so.okopf_excl == 0)
                {
                    filter += str;
                }
                else
                {
                    must_not += str;
                }
            }

            if (so.okfs != "")
            {
                string str = ", {\"terms\": {\"okfs\":[" + so.okfs.Replace("|", ",") + "] }}";
                if (so.okfs_excl == 0)
                {
                    filter += str;
                }
                else
                {
                    must_not += str;
                }
            }

            if (so.industry != "")
            {
                if (so.ind_main == 1)
                {
                    string str = ", {\"terms\": {\"okved_id_list\":[" + so.industry.Replace("|", ",") + "] }}";
                    if (so.ind_excl == 0)
                    {
                        filter += str;
                    }
                    else
                    {
                        must_not += str;
                    }
                }
                else
                {
                    string str = ", {\"nested\": {\"path\": \"okved_list\", \"query\": {\"bool\": {\"must\": {\"terms\": {\"okved_list.okved_id_list\": [" + so.industry.Replace("|", ",") + "]}}}}, \"inner_hits\": {} } } ";
                    if (so.ind_excl == 0)
                    {
                        must += str;
                    }
                    else
                    {
                        must_not += str;
                    }
                }
            }

            if (so.regions != "")
            {
                if (so.is_okato == 0)
                {
                    string str = ", {\"terms\": {\"region_id\":[" + so.regions.Replace("|", ",") + "] }}";
                    if (so.reg_excl == 0)
                    {
                        filter += str;
                    }
                    else
                    {
                        must_not += str;
                    }
                }
                else
                {
                    string str = ", {\"terms\": {\"okato_list\":[\"" + so.regions.Replace("|", "\",\"") + "\"] }}";
                    if (so.reg_excl == 0)
                    {
                        filter += str;
                    }
                    else
                    {
                        must_not += str;
                    }
                }
            }

            if (so.trades != "")
            {
                string should = "";

                if (so.trades.Substring(1, 1) == "1")
                {
                    should += ", {\"term\": {\"is_mmvb\": true }}";
                }

                if (so.trades.Substring(2, 1) == "1")
                {
                    should += ", {\"term\": {\"is_rtsboard\": true }}";
                }

                if (should.Length > 0)
                {
                    should = should.Substring(1);
                    must += ", {\"bool\": {\"should\": [" + should + "], \"minimum_should_match\": 1 }}";
                }
            }

            if (so.gaap == 1)
            {
                filter += ", {\"term\": {\"is_gaap\": true }}";
            }

            if (so.bankrupt == 1)
            {
                filter += ", {\"term\": {\"is_nedobr\": true }}";
            }

            if (so.rgstr == 1)
            {
                filter += ", {\"term\": {\"is_gks\": true }}";
                //must_not += ", {\"exists\": {\"field\": \"del_date\"}}";
            }

            if (so.msp == 1)
            {
                filter += ", {\"terms\": {\"msp_type\": [1,2,3] }}";
            }

            if (so.rsbu == 1)
            {
                filter += ", {\"term\": {\"rsbu_year\": 2015 }}";
            }

            if (so.status == 1)
            {
                filter += ", {\"term\": {\"status\": 0 }}, {\"exists\": {\"field\": \"ogrn\"}}";
            }

            if (so.fas != "")
            {
                string str = ", {\"terms\": {\"fas_list\":[" + so.fas.Replace("|", ",") + "] }}";
                if (so.fas_excl == 0)
                {
                    filter += str;
                }
                else
                {
                    must_not += str;
                }
            }

            if (so.filials > 0)
            {
                must_not += ", {\"term\": {\"opf\": 90 }}";
            }

            if (so.group_id != 0)
            {
                string gl = GetGroupList(so.group_id);
                if (gl != "")
                {
                    filter += ", {\"terms\": {\"ticker\": [" + gl + "] }}";
                }
            }

            if (must == "") must = "{\"match_all\": {}}";
            else must = must.Substring(1);

            res = "{\"query\": {\"bool\": { ";
            res += "\"must\": [" + must + "] ";
            if (filter != "")
            {
                filter = filter.Substring(1);
                res += ",\"filter\" : [  " + filter + " ] ";
            }
            if (must_not != "")
            {
                must_not = must_not.Substring(1);
                res += ",\"must_not\" : [ " + must_not + " ] ";
            }
            res += " } } ";
            res += ",\"size\": \"" + rcount + "\", \"from\": \"" + fr + "\"";
            if (so.top1000 == "1")
                res += ",\"_source\": [\"issuer_id\",\"type_id\"]";
            //res += ",\"sort\": [{\"group_id\": {\"order\": \"asc\"}}, \"_score\", {\"uniq\": {\"order\": \"desc\"}}, {\"bones\": {\"order\": \"asc\"}}] }";
            res += ",\"sort\": [{\"status\": {\"order\": \"asc\"}}, {\"is_gks\": {\"order\": \"desc\"}}, {\"uniq\": {\"order\": \"desc\"}}, {\"_script\" : {\"type\": \"number\", \"script\": {\"lang\": \"painless\", \"inline\": \"def sc = 0; sc=doc['group_id'].value; if (sc>1) {sc=2} sc=2-sc; sc=sc*2+_score; return sc\"}, \"order\": \"desc\"}}, {\"bones\": {\"order\": \"asc\"}}] ";
            res += ",\"track_scores\": \"true\"}";

            return res;
        }

        private string GetGroupList(int group_id)
        {
            string res = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = "SELECT u.ticker from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@group_id", SqlDbType.BigInt).Value = group_id;
                    cmd.CommandTimeout = 300;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        res += "\"" + reader.ReadEmptyIfDbNull("ticker").ToUpper() + "\",";
                    }
                    if (res != "") res = res.Substring(0, res.Length - 1);
                }
            }
            catch
            {
                return "";
            }
            return res;
        }

        private string GetQueryBones(string s)
        {
            string res = "";
            res = "{\"query\": {\"prefix\": {\"bones\": \"" + s.Replace("\"", "").Replace("\\", "").Replace("\t", " ").ToUpper() + "\"}}, " +
              "\"aggs\" : {\"agg\": {\"terms\": {\"field\": \"bones\" , \"size\":7, \"order\" : {\"_term\": \"asc\" }}}}, " +
              "\"size\": \"0\" }";

            return res;
        }

        private List<ULDetails> GetSearchDetails(JObject r)
        {
            List<ULDetails> res = new List<ULDetails>();
            for (int i = 0; i < r["hits"]["hits"].Count(); i++)
            {
                ULDetails detail = new ULDetails();
                detail.name = NotNullStr((string)r["hits"]["hits"][i]["_source"]["name"]);
                detail.nm = NotNullStr((string)r["hits"]["hits"][i]["_source"]["short_name"]);
                detail.inn = NotNullStr((string)r["hits"]["hits"][i]["_source"]["inn"]);
                detail.region = NotNullStr((string)r["hits"]["hits"][i]["_source"]["region_name"]);
                detail.okpo = NotNullStr((string)r["hits"]["hits"][i]["_source"]["okpo"]);
                detail.okved_code = NotNullStr((string)r["hits"]["hits"][i]["_source"]["okved"]);
                detail.okved = NotNullStr((string)r["hits"]["hits"][i]["_source"]["okved_name"]);
                detail.ogrn = NotNullStr((string)r["hits"]["hits"][i]["_source"]["ogrn"]);
                detail.reg_date = NotNullStr((string)r["hits"]["hits"][i]["_source"]["reg_date"]);
                detail.reg_org_name = NotNullStr((string)r["hits"]["hits"][i]["_source"]["reg_org_name"]);
                detail.legal_address = NotNullStr((string)r["hits"]["hits"][i]["_source"]["address"]);
                detail.legal_phone = NotNullStr((string)r["hits"]["hits"][i]["_source"]["phone"]);
                detail.legal_fax = NotNullStr((string)r["hits"]["hits"][i]["_source"]["fax"]);
                detail.legal_email = NotNullStr((string)r["hits"]["hits"][i]["_source"]["email"]);
                detail.www = NotNullStr((string)r["hits"]["hits"][i]["_source"]["www"]);
                if ((string)r["hits"]["hits"][i]["_source"]["del_date"] != null) detail.del = "Удалено из реестра Росстата " + (string)r["hits"]["hits"][i]["_source"]["del_date"];
                else detail.del = "";
                detail.ticker = NotNullStr((string)r["hits"]["hits"][i]["_source"]["ticker"]);
                detail.issuer_id = NotNullStr((string)r["hits"]["hits"][i]["_source"]["issuer_id"]);
                detail.type_id = ((int)r["hits"]["hits"][i]["_source"]["type_id"]).ToString();

                string ruler = "";
                for (int j = 0; j < r["hits"]["hits"][i]["_source"]["manager_list"].Count(); j++)
                {
                    JObject m = (JObject)r["hits"]["hits"][i]["_source"]["manager_list"][j];
                    if (!(bool)m["is_old"])
                    {
                        if (ruler != "") ruler += ", ";
                        ruler += (string)m["fio"];
                        if ((string)m["position"] != null) ruler += " - " + (string)m["position"];
                        if ((string)m["inn"] != null) ruler += " (ИНН: " + (string)m["inn"] + ")";
                    }
                }
                detail.ruler = ruler;

                res.Add(detail);
            }

            return res;
        }
        private void SplitOpf(string company, out string name, out string opf)
        {
            string[] opf_list = { "АО", "ООО", "ЗАО", "ПАО", "АООТ", "АОЗТ", "СОАО", "ОАО", "ТОО", "ГУП", "ФГУП" };
            name = company;
            opf = "";
            int n = company.IndexOf(" ");
            if (n > 0)
            {
                string s = company.Substring(0, n).ToUpper();
                for (int i = 0; i < opf_list.Count(); i++)
                {
                    if (opf_list[i] == s)
                    {
                        opf = s;
                        name = company.Substring(n + 1).Trim();
                        break;
                    }
                }
            }
        }


        private string NotNullStr(string s)
        {
            if (s == null) s = "";
            return s;
        }
    }
}