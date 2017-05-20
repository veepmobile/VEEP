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
using Skrin.Models.Fedresurs;

namespace Skrin.Controllers.Messages
{
    public class FedresursSearchController : BaseController
    {
        private enum Key { canSearch };

        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();

        static FedresursSearchController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Mess);
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            ViewBag.LocPath = "/dbsearch/fedresurssearch/";
            ViewBag.Title = "СКРИН-Контрагент: Факты деятельности.";
            ViewBag.Description = "Поиск фактов деятельности.";

            return View();
        }

        public ActionResult FedresursSearch(FedresursSObject so)
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

            FedresursSearchQueryGenerator qg = new FedresursSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");
        }

        protected string GetBeginDate()
        {
            FedresursSObject so = new FedresursSObject();
            FedresursSearchQueryGenerator qg = new FedresursSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetDBeg();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
            string result = searcher.SearchHtml();
            return result.Substring(0, 10);
        }

        public ActionResult Counter()
        {
            FedresursSObject so = new FedresursSObject();
            FedresursSearchQueryGenerator qg = new FedresursSearchQueryGenerator(so);
            SphinxQueryObject qo = qg.GetCount();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
            string result = searcher.SearchHtml();

            return Content(result);
        }

        public async Task<ActionResult> GetMessage(string id, string ticker)
        {
            CompanyData company = new CompanyData();
            if (id != null)
            {
                if(ticker != "")
                {
                    company = await SqlUtiltes.GetCompanyAsync(ticker);
                }
                FedresursMessageItem message = await FedresursRepository.GetFedresursMessage(id, company);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<ActionResult> GetMessagesSelected(string ids)
        {
            List<FedresursMessageItem> list = new List<FedresursMessageItem>();
            CompanyData company = new CompanyData();
                if (ids != null)
                {
                    string[] items = ids.Split(',');
                    foreach (string item in items)
                    {
                        if (!String.IsNullOrWhiteSpace(item))
                        {
                            string[] id = item.Split('_');
                            if(id[1] != ""){
                                company = await SqlUtiltes.GetCompanyAsync(id[1]);
                            }
                            FedresursMessageItem message = await FedresursRepository.GetFedresursMessage(id[0], company);
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