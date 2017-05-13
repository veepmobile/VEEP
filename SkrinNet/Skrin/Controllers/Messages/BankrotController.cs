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
using Skrin.BLL.Report;
using Skrin.BLL.Root;
using Skrin.Models;
using Skrin.Models.Iss;
using Skrin.Models.Iss.Content;
using Skrin.Models.Bankrot;

namespace Skrin.Controllers.Messages
{
    public class BankrotController : BaseController
    {
        private enum Key { canSearch };

        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();


        static BankrotController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Mess);
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            ViewBag.LocPath = "/dbsearch/bankrot/";
            ViewBag.Title = "СКРИН-Контрагент: Банкротства физических и юридических лиц.";
            ViewBag.Description = "Поиск банкротств физических и юридических лиц.";

            return View();
        }

        public ActionResult BankrotSearch(BankrotSearchObject so)
        {
            UserSession us = HttpContext.GetUserSession();

            //Пользователей без прав дальше первой страницы не пускаем
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                so.page = 1;
                so.DBeg = "";
                so.DEnd = "";
            }

            return BankrotSearch2(so);

           /* so.page = Convert.ToInt32(so.page) - 1;
            so.rcount = Convert.ToInt32(so.rcount);

            string dfrom = so.DBeg;
            so.DBeg = (!String.IsNullOrEmpty(so.DBeg)) ? so.DBeg : ((so.page < 0) ? GetBeginDate() : "");
            so.DEnd = (!String.IsNullOrEmpty(so.DEnd)) ? so.DEnd : ((so.page < 0 && !String.IsNullOrEmpty(dfrom)) ? "" : so.DEnd);
            so.DBeg_ts = (so.DBeg != "") ? Convert.ToInt64((so.DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.DEnd_ts = (so.DEnd != "") ? Convert.ToInt64((so.DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;

            so.search_name = !String.IsNullOrEmpty(so.search_name) ? so.search_name.Replace("%20", " ").Replace("\"", "").Replace("\'", "").Trim() : "";
            so.types = !String.IsNullOrEmpty(so.types) ? so.types : "";
            so.types_excl = Convert.ToInt32(so.types_excl);
            so.src = Convert.ToInt32(so.src);

            BankrotQueryGenerator qg = new BankrotQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxBankruptcyPort, qo.CharasterSet, Configs.SphinxBankruptcyServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");*/
        }
        public ActionResult BankrotSearch2(BankrotSearchObject so)
        {
            so.page = Convert.ToInt32(so.page) - 1;
            so.rcount = Convert.ToInt32(so.rcount);

            string dfrom = so.DBeg;
            so.DBeg = (!String.IsNullOrEmpty(so.DBeg)) ? so.DBeg : ((so.page < 0) ? GetBeginDate() : "");
            so.DEnd = (!String.IsNullOrEmpty(so.DEnd)) ? so.DEnd : ((so.page < 0 && !String.IsNullOrEmpty(dfrom)) ? "" : so.DEnd);
            so.DBeg_ts = (so.DBeg != "") ? Convert.ToInt64((so.DBeg + " 00:00:00").UnixDateTimeStamp()) : 0;
            so.DEnd_ts = (so.DEnd != "") ? Convert.ToInt64((so.DEnd + " 23:59:00").UnixDateTimeStamp()) : 0;

            so.search_name = !String.IsNullOrEmpty(so.search_name) ? so.search_name.Replace("%20", " ").Replace("\"", "").Replace("\'", "").Trim() : "";
            so.types = !String.IsNullOrEmpty(so.types) ? so.types : "";
            so.types_excl = Convert.ToInt32(so.types_excl);
            so.src = Convert.ToInt32(so.src);

            BankrotQueryGenerator qg = new BankrotQueryGenerator(so);
            SphinxQueryObject qo = qg.GetQuery();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxBankruptcyPort, qo.CharasterSet, Configs.SphinxBankruptcyServer);
            string result = searcher.SearchJson();

            return Content("{" + result + "}");
        }
        protected string GetBeginDate()
        {
            BankrotSearchObject so = new BankrotSearchObject();
            BankrotQueryGenerator qg = new BankrotQueryGenerator(so);
            SphinxQueryObject qo = qg.GetDBeg();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxBankruptcyPort, qo.CharasterSet, Configs.SphinxBankruptcyServer);
            string result = searcher.SearchHtml();
            return result.Substring(0, 10);
        }

        public ActionResult Counter()
        {
            BankrotSearchObject so = new BankrotSearchObject();
            BankrotQueryGenerator qg = new BankrotQueryGenerator(so);
            SphinxQueryObject qo = qg.GetCount();
            SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxBankruptcyPort, qo.CharasterSet, Configs.SphinxBankruptcyServer);
            string result = searcher.SearchHtml();

            return Content(result);
        }


        public async Task<ActionResult> GetBankrotMsg(string id, string src)
        {
            if (src == "1" || src == "-1")
            {
                //ЕФРСБ
                XSLGenerator g = new XSLGenerator("skrin_content_output..getBancruptcyEFRSB", new Dictionary<string, object> { { "@ids", id } }, "tab_content/bancruptcy/messEFRSB", new Dictionary<string, object> { { "id", id }, { "PDF", -1 } });
                string result = await g.GetResultAsync();
                return Content(result);
            }
            else
            {
                //Коммерсант, Российская газета
                BankrotItem item = BankrotRepository.GetBankruptcytMsg(id);
                if (item != null)
                {
                    return Json(item, JsonRequestBehavior.AllowGet);
                }
            }

            return null;
        }

        public async Task<ActionResult> GetBankrotSelected(string ids)
        {
            string[] items = ids.Split(',');
            string ret = "";
            foreach(string item in items)
            {
                if (!String.IsNullOrWhiteSpace(item))
                {
                    string[] id = item.Split('_');
                    string result = "";
                    if (id[1] == "1" || id[1] == "-1")
                    {                
                        //ЕФРСБ
                        XSLGenerator g = new XSLGenerator("skrin_content_output..getBancruptcyEFRSB", new Dictionary<string, object> { { "@ids", id[0] } }, "tab_content/bancruptcy/messEFRSB", new Dictionary<string, object> { { "id", id[0] }, { "PDF", -1 } });
                        result = await g.GetResultAsync();
                    }
                    else
                    {
                        //Коммерсант, Российская газета
                        BankrotItem mess = BankrotRepository.GetBankruptcytMsg(id[0]);
                        result = "<div  style=\"color:#3a61ad;font-weight:bold;\">СООБЩЕНИЕ О БАНКРОТСТВЕ</div><br/><table width=\"100%\"><tbody><tr><td align=\"left\">Источник данных: " + mess.source + "</td><td align=\"right\">Дата публикации сообщения: " + mess.reg_date + "</td></tr></tbody></table><span class=\"bluecaption\"></span><br>" + mess.contents + "<hr><div class=\"data_comment limitation\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div>";
                    }

                    if (result != null)
                    {
                        ret += result + "<hr><br/>"; ;
                    }
                }
            }

            return Content(ret);
        }

    }
}