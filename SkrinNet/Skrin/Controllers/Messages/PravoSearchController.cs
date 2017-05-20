using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Skrin.BLL;
using Skrin.BLL.Iss;
using Skrin.BLL.Messages;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.BLL.Root;
using Skrin.Models;
using Skrin.Models.Iss;
using Skrin.Models.Iss.Content;
using Skrin.Models.Pravo;


namespace Skrin.Controllers.Messages
{
    public class PravoSearchController : BaseController
    {

        private enum Key { canSearch, canMsg };

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static PravoSearchController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Mess);
            roles.Add(Key.canMsg.ToString(), AccessType.Pred | AccessType.Mess | AccessType.Ka | AccessType.KaPlus | AccessType.Emitent);
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            ViewBag.LocPath = "/dbsearch/pravosearch/";
            ViewBag.Title = "СКРИН-Контрагент: Арбитражные дела.";
            ViewBag.isSearch = "0";
            string isSearch = HttpContext.Request["s"];
            if (isSearch == "1")
            {
                string ticker = HttpContext.Request["t"];
                string id = HttpContext.Request["id"];
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Select convert(varchar(10),a.update_date,104) as ldt, us.ogrn from pravo..[case] a inner join pravo..[side] pcs with(nolock)  on pcs.case_id=a.id inner join searchdb2..union_search us with(nolock) on us.ogrn=pcs.ogrn and pcs.inn=us.inn and not opf in (90,99,98)  where a.id=@id and ticker=@ticker", con);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
                    cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            ViewBag.ogrn = reader.getStrValue(1, "");
                            ViewBag.dt = reader.getStrValue(0, "");
                            ViewBag.isSearch = "1";
                        }
                    }
                    con.Close();
                }

            }

            ViewBag.isMsg = "0";
            ViewBag.Msg_reg_no = "";
            ViewBag.Msg_id = "";
            ViewBag.Msg_src = "";
            string reg_no = HttpContext.Request["r"];
            if (reg_no != null && reg_no != "")
            {
                ViewBag.Msg_reg_no = reg_no;
                ViewBag.Msg_id = HttpContext.Request["id"];
                ViewBag.Msg_src = HttpContext.Request["a"];
                ViewBag.isMsg = "1";
            }

            ViewBag.Description = "Поиск арбитражных дел.";

            return View();
        }

        public ActionResult PravoSearch(PravoSObject so)
        {
            UserSession us = HttpContext.GetUserSession();

            //Пользователей без прав дальше первой страницы не пускаем
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                so.page = 1;
                so.ins_DBeg = "";
                so.ins_DEnd = "";
            }


            so.page = Convert.ToInt32(so.page) - 1;
            so.rcount = Convert.ToInt32(so.rcount);

            so.ins_DBeg = (!String.IsNullOrEmpty(so.ins_DBeg)) ? so.ins_DBeg : "";
            so.ins_DEnd = (!String.IsNullOrEmpty(so.ins_DEnd)) ? so.ins_DEnd : "";
            so.ins_DBeg_ts = (so.ins_DBeg != "") ? Convert.ToInt64((so.ins_DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.ins_DEnd_ts = (so.ins_DEnd != "") ? Convert.ToInt64((so.ins_DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;

            so.last_DBeg_ts = (!String.IsNullOrEmpty(so.last_DBeg)) ? Convert.ToInt64((so.last_DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.last_DEnd_ts = (!String.IsNullOrEmpty(so.last_DEnd)) ? Convert.ToInt64((so.last_DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;
            so.search_txt = !String.IsNullOrEmpty(so.search_txt) ? so.search_txt.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("undefined", "") : "";
            so.search_txt = Helper.FullTextString(so.search_txt);
            so.search_txt = so.search_txt.Replace("*", "");
            so.side_type = !String.IsNullOrEmpty(so.side_type) ? so.side_type : "";
            so.side_type_excl = Convert.ToInt32(so.side_type_excl);
            so.ac_type = !String.IsNullOrEmpty(so.ac_type) ? so.ac_type : "";
            so.ac_type_excl = Convert.ToInt32(so.ac_type_excl);
            so.disput_type = !String.IsNullOrEmpty(so.disput_type) ? so.disput_type : "";
            so.disput_type_excl = Convert.ToInt32(so.disput_type_excl);


            PravoSearchQueryGenerator qg = new PravoSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxPravoPort, qo.CharasterSet, Configs.SphinxPravoServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");
        }

        public ActionResult Counter()
        {
            PravoSObject so = new PravoSObject();
            PravoSearchQueryGenerator qg = new PravoSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetCount();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxPravoPort, qo.CharasterSet, Configs.SphinxPravoServer);
            string result = searcher.SearchHtml();

            return Content(result);
        }

        public async Task<ActionResult> GetMessages(string ids)
        {
            string[] items = ids.Split(',');
            string ret = "";
            foreach (string item in items)
            {
                if (!String.IsNullOrWhiteSpace(item))
                {
                    string[] id = item.Split('_');
                    if (id[1] == "1")
                    {
                        XSLGenerator g = new XSLGenerator("skrin_content_output..ShowCase2", new Dictionary<string, object> { { "@id", id[0] }, { "@iss", "" } }, "tab_content/pravo/showcase2", null);
                        ret += "<br/><hr><br/>" + await g.GetResultAsync();
                        //return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XSLGenerator g = new XSLGenerator("skrin_content_output..ShowCase", new Dictionary<string, object> { { "@id", id[0] }, { "@iss", "" } }, "tab_content/pravo/showcase", null);
                        ret += "<br/><hr><br/>" + await g.GetResultAsync();
                        //return Content(await g.GetResultAsync());
                    }
                }
            }
            return Content(ret);
        }

        public ActionResult PravoSearch_elastic(PravoSObject so)
        {
            UserSession us = HttpContext.GetUserSession();

            //Пользователей без прав дальше первой страницы не пускаем
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                so.page = 1;
                so.ins_DBeg = "";
                so.ins_DEnd = "";
            }

            so.page = Convert.ToInt32(so.page) - 1;
            if (so.page < 0) so.page = 0;
            so.rcount = Convert.ToInt32(so.rcount);

            so.ins_DBeg = (!String.IsNullOrEmpty(so.ins_DBeg)) ? so.ins_DBeg : "";
            so.ins_DEnd = (!String.IsNullOrEmpty(so.ins_DEnd)) ? so.ins_DEnd : "";
            so.ins_DBeg_ts = (so.ins_DBeg != "") ? Convert.ToInt64((so.ins_DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.ins_DEnd_ts = (so.ins_DEnd != "") ? Convert.ToInt64((so.ins_DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;

            so.last_DBeg_ts = (!String.IsNullOrEmpty(so.last_DBeg)) ? Convert.ToInt64((so.last_DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.last_DEnd_ts = (!String.IsNullOrEmpty(so.last_DEnd)) ? Convert.ToInt64((so.last_DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;
            so.job_no = (!String.IsNullOrEmpty(so.job_no)) ? so.job_no : "";
            so.search_txt = !String.IsNullOrEmpty(so.search_txt) ? so.search_txt.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("undefined", "") : "";
            so.search_txt = Helper.FullTextString(so.search_txt);
            so.search_txt = so.search_txt.Replace("*", "");
            so.side_type = !String.IsNullOrEmpty(so.side_type) ? so.side_type : "";
            so.side_type_excl = Convert.ToInt32(so.side_type_excl);
            so.ac_type = !String.IsNullOrEmpty(so.ac_type) ? so.ac_type : "";
            so.ac_type_excl = Convert.ToInt32(so.ac_type_excl);
            so.disput_type = !String.IsNullOrEmpty(so.disput_type) ? so.disput_type : "";
            so.disput_type_excl = Convert.ToInt32(so.disput_type_excl);

            string url = "/pravo/case/_search";
            string json = GetQuery(so);
            //System.IO.File.AppendAllText("c:\\net\\1.txt", json + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
            //return Content(json);

            ElasticClient ec = new ElasticClient();
            JObject r = ec.GetQuery(url, json);
            return Content(r.ToString());
        }

        public ActionResult Counter_elastic()
        {
            PravoSObject so = new PravoSObject();
            string url = "/pravo/case/_search";
            string json = "{\"query\":{\"match_all\":{}}, \"size\":0}";

            ElasticClient ec = new ElasticClient();
            JObject r = ec.GetQuery(url, json);

            return Content(((Int64)r["hits"]["total"]).ToString());
        }

        private string GetQuery(PravoSObject so)
        {
            string res = "";
            string must = "";
            string should = "";
            string filter = "";
            string must_not = "";
            int fr = so.page * so.rcount;

            DateTime? ins_DBeg = null;
            DateTime? ins_DEnd = null;
            try { ins_DBeg = DateTime.ParseExact(so.ins_DBeg, "dd.MM.yyyy", null); }
            catch { }
            try { ins_DEnd = DateTime.ParseExact(so.ins_DEnd, "dd.MM.yyyy", null); }
            catch { }

            DateTime? last_DBeg = null;
            DateTime? last_DEnd = null;
            try { ins_DBeg = DateTime.ParseExact(so.last_DBeg, "dd.MM.yyyy", null); }
            catch { }
            try { ins_DEnd = DateTime.ParseExact(so.last_DEnd, "dd.MM.yyyy", null); }
            catch { }

            if (ins_DBeg != null || ins_DEnd != null)
            {
                if (filter != "") filter += ",";
                filter += " {\"range\": {\"reg_date\": {";
                if (ins_DBeg != null) filter += "\"gte\": \"" + String.Format("{0:dd.MM.yyyy}", ins_DBeg) + "\"";
                if (ins_DBeg != null && ins_DEnd != null) filter += ",";
                if (ins_DEnd != null) filter += "\"lte\": \"" + String.Format("{0:dd.MM.yyyy}", ins_DEnd) + "\"";
                filter += "}}}";
            }

            if (last_DBeg != null || last_DEnd != null)
            {
                if (filter != "") filter += ",";
                filter += " {\"range\": {\"upd_date\": {";
                if (last_DBeg != null) filter += "\"gte\": \"" + String.Format("{0:dd.MM.yyyy}", last_DBeg) + "\"";
                if (last_DBeg != null && ins_DEnd != null) filter += ",";
                if (last_DEnd != null) filter += "\"lte\": \"" + String.Format("{0:dd.MM.yyyy}", last_DEnd) + "\"";
                filter += "}}}";
            }

            if (so.job_no != "")
            {
                if (must != "") must += ",";
                must += " {\"match\": {\"instance.instance_reg_no\": {\"query\": \"" + so.job_no + "\", \"operator\": \"and\" } }}";
            }

            if (so.ac_type != "")
            {
                if (so.ac_type_excl == 0)
                {
                    if (filter != "") filter += ",";
                    filter += " {\"terms\": {\"court_id\":[" + so.ac_type + "] }}";
                }
                else
                {
                    if (must_not != "") must_not += ",";
                    must_not += " {\"terms\": {\"court_id\":[" + so.ac_type + "] }}";
                }
            }

            if (so.disput_type != "")
            {
                if (so.disput_type_excl == 0)
                {
                    if (filter != "") filter += ",";
                    filter += " {\"terms\": {\"disput_type_id\":[" + so.disput_type + "] }}";
                }
                else
                {
                    if (must_not != "") must_not += ",";
                    must_not += " {\"terms\": {\"disput_type_id\":[" + so.disput_type + "] }}";
                }
            }

            if (so.search_txt != "")
            {
                if (must != "") must += ",";
                must += "{\"nested\": { \"path\": \"side\", \"query\": {\"bool\": { ";

                if (so.search_txt != "")
                    must += "\"must\": {\"multi_match\": {\"fields\": [\"side.name\", \"side.inn\", \"side.ogrn\"], \"query\": \"" + so.search_txt.Replace("\\", "\\\\") + "\", \"type\": \"cross_fields\", \"operator\": \"and\" } } ";

                if (so.side_type != "")
                {
                    if (so.side_type_excl == 0)
                        must += " ,\"filter\": ";
                    else
                        must += " ,\"must_not\": ";
                    must += "{\"terms\": {\"side.side_type_id\": [" + so.side_type + "] } }";
                }
                must += "} } , \"inner_hits\": {} } } ";
            }

            if (so.grp != 0)
            {
                string s = GetGroupQuery(so.grp, so.side_type, so.side_type_excl);
                if (s != "")
                {
                    if (should != "") should += ",";
                    should += s;
                }
            }

            if (must == "") must = "{\"match_all\": {}}";
            res = "{\"query\": {\"bool\": { ";
            res += "\"must\": [" + must + "] ";
            if (should != "")
            {
                res += ",\"should\" : [  " + should + " ], \"minimum_should_match\" : 1 ";
            }
            if (filter != "")
            {
                res += ",\"filter\" : [  " + filter + " ] ";
            }
            if (must_not != "")
            {
                res += ",\"must_not\" : [ " + must_not + " ] ";
            }
            res += " } } ";
            res += ",\"size\": \"20\", \"from\": \"" + fr + "\"";
            res += ",\"sort\": [{\"reg_date\": {\"order\": \"desc\"}}] }";

            return res;
        }

        private string GetGroupQuery(int group_id, string side_type, int side_type_excl)
        {
            string res = "";
            string inn = "";
            string ogrn = "";
            string match = "";
            string side_filter = "";

            if (side_type != "")
            {
                if (side_type_excl == 0)
                    side_filter = " ,\"filter\": ";
                else
                    side_filter = " ,\"must_not\": ";
                side_filter += "{\"terms\": {\"side.side_type_id\": [" + side_type + "] } }";
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = "SELECT u.inn, u.ogrn, u.name, u.short_name from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@group_id", SqlDbType.BigInt).Value = group_id;
                    cmd.CommandTimeout = 300;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.ReadEmptyIfDbNull("inn") != "")
                            inn += ",\"" + (string)reader["inn"] + "\"";
                        if (reader.ReadEmptyIfDbNull("ogrn") != "")
                            ogrn += ",\"" + (string)reader["ogrn"] + "\"";
                        //if (reader.ReadEmptyIfDbNull("name") != "")
                        //    match += " {\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(reader.ReadEmptyIfDbNull("name")) + "\", \"operator\": \"and\" } }},";
                        //if (reader.ReadEmptyIfDbNull("short_name") != "")
                        //    match += " {\"match\": {\"side.name\": {\"query\": \"" + Helper.FullTextString(reader.ReadEmptyIfDbNull("short_name")) + "\", \"operator\": \"and\" } }},";
                    }
                    reader.Close();
                    con.Close();

                    if (inn.Length + ogrn.Length == 0) return "";

                    if (inn.Length > 0)
                    {
                        inn = inn.Substring(1);
                        res += "{\"nested\":  { \"path\": \"side\", \"query\": { \"bool\": {\"must\": {\"match_all\": {}}, \"filter\": {\"terms\": {\"side.inn\": [" + inn + "] }}" + side_filter + " }}}},";
                    }
                    if (ogrn.Length > 0)
                    {
                        ogrn = ogrn.Substring(1);
                        res += "{\"nested\":  { \"path\": \"side\", \"query\": { \"bool\": {\"must\": {\"match_all\": {}}, \"filter\": {\"terms\": {\"side.ogrn\": [" + ogrn + "] }}" + side_filter + " }}}},";
                    }
                    res = res.Substring(0, res.Length - 1);
                }
            }
            catch
            {
                return "";
            }

            return res;
        }
    }
}