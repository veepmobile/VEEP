using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skrin.BLL.Search;
using Skrin.Models;

namespace Skrin.Controllers
{
    public class SearchIchpController: BaseController
    {
        private enum Key { canExport, canAddToGroup };

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static SearchIchpController()
        {
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.KaPoln);
            roles.Add(Key.canAddToGroup.ToString(), AuthenticateSqlUtilites.GetGroupRoles().Value);
        }

        public ActionResult Index(string ruler)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/dbsearchru/ichp/";
            ViewBag.Title = "СКРИН-Контрагент: база данных индивидуальных предпринимателей России. Поиск по реквизитам.";
            ViewBag.Description = "База данных индивидуальных предпринимателей России. Поиск по реквизитам.";

            List<string> insert_searches = new List<string>();

            if (!string.IsNullOrWhiteSpace(ruler))
            {
                insert_searches.Add("$('#ruler').val('" + ruler + "');");
            }

            return View(insert_searches);
        }

        [CheckRequest]
        public async Task<ActionResult> Search(IchpSearchObject so)
        {
            //проверим permition на сервере
            UserSession us = HttpContext.GetUserSession();
            //PageRoles pr = _GetRoles();

            await ModifitySOAsync(so);     

            try
            {
                SearchIchpQueryGenerator qg = new SearchIchpQueryGenerator(so);
                SphinxQueryObject qo = qg.GetQuery();
                    SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxSearchIPPort, qo.CharasterSet, Configs.SphinxSearchIPServer);
                    string result = searcher.SearchJson();
                    return Content("{" + result + "}");
            }
            catch (Exception ex)
            {
                return Content(string.Format("<div style=\"display:none\" id=\"fnd\" val=\"-1\">{0}</div>Ошибка. Попробуйте позже. {0}", ex.Message));
            }

        }

        private async Task ModifitySOAsync (IchpSearchObject so)
        {
            so.regions = await SearchRepository.GetRegsAsync(so.regions, so.is_okato);
            //so.industry = await SearchRepository.GetIndAsync(so.industry, 0, so.ind_excl);
            so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Clear() : "";
            so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
            so.industry = !String.IsNullOrWhiteSpace(so.industry) ? so.industry : "";
            so.top1000 = !String.IsNullOrEmpty(so.top1000) ? so.top1000 : "";
            so.page_no = so.page_no == 0 ? 0 : so.page_no - 1;
            so.group_name = !String.IsNullOrEmpty(so.group_name) ? so.group_name : "";
        }

        public async Task<ActionResult> DoSearchExcel(string so_string)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                IchpSearchObject so;
                bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
                try
                {
                    so = JsonConvert.DeserializeObject<IchpSearchObject>(so_string);

                    await ModifitySOAsync(so);     

                    SearchIchpQueryGenerator qg = new SearchIchpQueryGenerator(so);
                    SphinxQueryObject qo = qg.GetQuery();
                    SphynxSearcherIchpDetails searcher_details = new SphynxSearcherIchpDetails(qo.Query, Configs.SphinxSearchIPPort, qo.CharasterSet, Configs.SphinxSearchIPServer);
                    List<FLDetails> details = searcher_details.SearchDetails();
                    var memoryStream = new MemoryStream();
                    ExportExcelIchp exp = new ExportExcelIchp();
                    exp.ExportWorkbook(details,test).SaveAs(memoryStream);
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

        public ActionResult GetExcel(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                SearchIchpQueryGenerator qg = new SearchIchpQueryGenerator(null);
                SphinxQueryObject qo = qg.ExportGetQuery(issuers);
                SphynxSearcherIchpDetails searcher_details = new SphynxSearcherIchpDetails(qo.Query, Configs.SphinxSearchIPPort, qo.CharasterSet, Configs.SphinxSearchIPServer);
                List<FLDetails> details = searcher_details.SearchDetails();
                var memoryStream = new MemoryStream();
                    ExportExcelIchp exp = new ExportExcelIchp();
                    exp.ExportWorkbook(details).SaveAs(memoryStream);

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream, "application/vnd.ms-excel", "search_result.xlsx");
            }
            else
            {
                return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [DeleteFileAttribute]
        public ActionResult GetFile(string src, string page)
        {
            UserSession us = HttpContext.GetUserSession();

            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + src;

                return File(filepath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            return new HttpUnauthorizedResult();
        }

    }
}