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
using System.Threading.Tasks;
using Skrin.BLL;
using Skrin.BLL.Messages;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models;
using Skrin.Models.Iss;
using Skrin.Models.Disclosure;

namespace Skrin.Controllers.Messages
{
    public class DisclosureController : BaseController
    {
        private enum Key { canSearch };

        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();


        static DisclosureController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Mess);
        }

        public ActionResult Index(string id="",string agency="")
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            ViewBag.LocPath = "/dbsearch/disclosure/";
            ViewBag.Title = "СКРИН-Контрагент: Cущественные факты.";
            ViewBag.Description = "Поиск существенных фактов.";

            return View();
        }

        public ActionResult DisclosureSearch(DisclosureSearchObject so)
        {
            UserSession us = HttpContext.GetUserSession();

            //Пользователей без прав дальше первой страницы не пускаем
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                so.page = 1;
                so.DBeg = "";
                so.DEnd = "";
            }


            so.page = Convert.ToInt32(so.page) - 1;
            so.rcount = Convert.ToInt32(so.rcount);

            string dfrom = so.DBeg;
            so.DBeg = (!String.IsNullOrEmpty(so.DBeg)) ? so.DBeg : ((so.page < 0) ? GetBeginDate() : "");
            so.DEnd = (!String.IsNullOrEmpty(so.DEnd)) ? so.DEnd : ((so.page < 0 && !String.IsNullOrEmpty(dfrom)) ? "" : so.DEnd);
            so.DBeg_ts = (so.DBeg != "") ? Convert.ToInt64((so.DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.DEnd_ts = (so.DEnd != "") ? Convert.ToInt64((so.DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;

            so.search_name = !String.IsNullOrEmpty(so.search_name) ? so.search_name.Replace("%20", " ").Replace("\"", "").Replace("\'", "").Trim() : "";
            so.search_text = !String.IsNullOrEmpty(so.search_text) ? so.search_name.Replace("%20", " ").Replace("\"", "").Replace("\'", "").Trim() : "";
            so.type_id = !String.IsNullOrEmpty(so.type_id) ? so.type_id : "";
            so.types_excl = Convert.ToInt32(so.types_excl);

            DisclosureQueryGenerator qg = new DisclosureQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxDisclosurePort, qo.CharasterSet, Configs.SphinxDisclosureServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");
        }

        protected string GetBeginDate()
        {
            DisclosureSearchObject so = new DisclosureSearchObject();
            DisclosureQueryGenerator qg = new DisclosureQueryGenerator(so);
            SphinxQueryObject qo = qg.GetDBeg();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxDisclosurePort, qo.CharasterSet, Configs.SphinxDisclosureServer);
            string result = searcher.SearchHtml();
            return result.Substring(0, 10);
        }

        public ActionResult Counter()
        {
            DisclosureSearchObject so = new DisclosureSearchObject();
            DisclosureQueryGenerator qg = new DisclosureQueryGenerator(so);
            SphinxQueryObject qo = qg.GetCount();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxDisclosurePort, qo.CharasterSet, Configs.SphinxDisclosureServer);
            string result = searcher.SearchHtml();

            return Content(result);
        }


        public ActionResult GetDisclosureMsg(string id, string agency_id, string ticker, string kw)
        {
            DisclosureItem item = DisclosureRepository.GetDisclosureMsg(id, agency_id, ticker, kw);

            if (item != null)
            {
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult GetDisclosureSelected (string ids)
        {
            string[] items = ids.Split(',');
            List<DisclosureItem> list = new List<DisclosureItem>();
            foreach(string item in items)
            {
                if (!String.IsNullOrWhiteSpace(item))
                {
                    string[] id = item.Split('_');
                    DisclosureItem di = DisclosureRepository.GetDisclosureMsg(id[0], id[1], id[2], "");
                    if (di != null)
                    {
                        list.Add(di);
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public static async Task<string> GetDisclosureDateAsync(string doc_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select convert(varchar(10),reg_date,104) as rd from naufor..issuer_docs where doc_id='" + doc_id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }
    }
}