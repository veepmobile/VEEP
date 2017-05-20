using Skrin.BLL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using System.Threading.Tasks;
using Skrin.Models.Search;
using Skrin.BLL.Search;
using System.IO;

namespace Skrin.Controllers
{
    public class DBSearchKZController : Controller
    {

        private enum Key { canSearch, canExport};

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();


        static DBSearchKZController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.KaPoln);
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.KaPoln);
        }

        public ActionResult Companies(string address)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/dbsearchkz/companies/";
            ViewBag.Title = "СКРИН-Контрагент: база данных юридических лиц (предприятий) Казахстана. Поиск по реквизитам.";
            ViewBag.Description = "База данных юридических лиц (предприятий) Казахстана. Поиск по реквизитам.";

            return View();
        }

        [CheckRequest]
        public async Task<ActionResult> CompaniesDoSearch(CompaniesKzSearchObject so)
        {

            //проверим permition на сервере
            UserSession us = HttpContext.GetUserSession();
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return new HttpStatusCodeResult(403);
            }

            so.company = !String.IsNullOrWhiteSpace(so.company) ? so.company.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("!", "") : "";
            so.company = Helper.FullTextString(so.company);
            so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Clear() : "";
            so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
            so.industry = !String.IsNullOrWhiteSpace(so.industry) ? await CompanySearchKz.GetIndustryAsync(so.industry) : "";
            so.page_no = so.page_no == 0 ? 0 : so.page_no;
            so.regions = await CompanySearchKz.GetRegionsAsync(so.regions);

            so.econ = so.econ == "999" ? "" : so.econ;
            so.status = so.status == "999" ? "" : so.status;
            so.own = so.own == "999" ? "" : so.own;
            so.pcount = so.pcount == "999" ? "" : so.pcount;
            so.siz = so.siz == "999" ? "" : so.siz;

            try
            {
                if (so.top1000 != "2")
                {
                    return Json(await CompanySearchKz.DoSearch(so));
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return Content(string.Format("<div style=\"display:none\" id=\"fnd\" val=\"-1\">{0}</div>Ошибка. Попробуйте позже. {0}", ex.Message));
            }
        }

        public async Task<ActionResult> CompaniesGetExcel(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
                List<KZDetails> details = await SqlUtiltes.GetKZDetailsAsync(issuers);
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                MemoryStream memoryStream = new MemoryStream();
                ExportExcel exp = new ExportExcel();
                exp.ExportKZWorkbook(details,test).SaveAs(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            else
            {
                return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> CompaniesGetExcelTop1000(CompaniesKzSearchObject so)
        {
            UserSession us = HttpContext.GetUserSession();
            bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
            so.company = !String.IsNullOrWhiteSpace(so.company) ? so.company.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("!", "") : "";
            so.company = Helper.FullTextString(so.company);
            so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Clear() : "";
            so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
            so.industry = !String.IsNullOrWhiteSpace(so.industry) ? await CompanySearchKz.GetIndustryAsync(so.industry) : "";
            so.page_no = so.page_no == 0 ? 0 : so.page_no;
            so.regions = await CompanySearchKz.GetRegionsAsync(so.regions);
            List<KZDetails> details = await CompanySearchKz.DoSearchTop1000(so);     

            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                MemoryStream memoryStream = new MemoryStream();
                ExportExcel exp = new ExportExcel();
                exp.ExportKZWorkbook(details,test).SaveAs(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            else
            {
                return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
            }
        }


     
    }
}