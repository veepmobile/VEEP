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
using Skrin.BLL.Iss;
using Skrin.BLL.Messages;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.BLL.Root;
using Skrin.Models;
using Skrin.Models.Iss;
using Skrin.Models.Iss.Content;
using Skrin.Models.Vestnik;

namespace Skrin.Controllers.Messages
{
    public class VestnikSearchController : BaseController
    {
        private enum Key { canSearch };

        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();

        static VestnikSearchController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Mess);
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            ViewBag.LocPath = "/dbsearch/vestniksearch/";
            ViewBag.Title = "СКРИН-Контрагент: Сообщения о госрегистрации.";
            ViewBag.Description = "Поиск сообщений о госрегистрации юридических лиц.";

            return View();
        }

        public ActionResult VestnikSearch(VestnikSObject so)
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
            so.search_text = !String.IsNullOrEmpty(so.search_text) ? so.search_text.Replace("%20", " ").Replace("\"", "").Replace("\'", "").Trim() : "";
            so.types = !String.IsNullOrEmpty(so.types) ? so.types : "";
            so.types_excl = Convert.ToInt32(so.types_excl);

            VestnikSearchQueryGenerator qg = new VestnikSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxVestnikPort, qo.CharasterSet, Configs.SphinxVestnikServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");
        }

        protected string GetBeginDate()
        {
            VestnikSObject so = new VestnikSObject();
            VestnikSearchQueryGenerator qg = new VestnikSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetDBeg();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxVestnikPort, qo.CharasterSet, Configs.SphinxVestnikServer);
            string result = searcher.SearchHtml();
            return result.Substring(0, 10);
        }

        public ActionResult Counter()
        {
            VestnikSObject so = new VestnikSObject();
            VestnikSearchQueryGenerator qg = new VestnikSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetCount();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxVestnikPort, qo.CharasterSet, Configs.SphinxVestnikServer);
            string result = searcher.SearchHtml();

            return Content(result);
        }
        public async Task<ActionResult> GetMessage(string id)
        {
            if (id != null)
            {
                VestnikMessage message = await VestnikSearchRepository.GetVestnikMessage(id);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<ActionResult> GetMessagesSelected(string ids)
        {
            List<VestnikMessage> list = new List<VestnikMessage>();
                if (ids != null)
                {
                    string[] parts = ids.Split(',');
                    foreach (var item in parts)
                    {
                        if (!String.IsNullOrWhiteSpace(item))
                        {
                            VestnikMessage message = await VestnikSearchRepository.GetVestnikMessage(item);
                            if (item != null)
                            {
                                list.Add(message);
                            }
                        }
                    }
                    return Json(list, JsonRequestBehavior.AllowGet);
                }

            return null;
        }
    }
}