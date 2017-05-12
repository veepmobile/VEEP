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
using Skrin.BLL.Search;
using Skrin.Models;
using Newtonsoft.Json;

namespace Skrin.Controllers
{
    public class DBSearchUaController : Controller
    {
        private enum Key { canSearch, canExport };
        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();


        static DBSearchUaController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.KaPoln);
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.KaPoln);
        }

        public ActionResult Companies()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/dbsearchua/companies/";
            ViewBag.Title = "СКРИН-Контрагент: база данных юридических лиц (предприятий) Украины. Поиск по реквизитам.";
            ViewBag.Description = "База данных юридических лиц (предприятий) Украины. Поиск по реквизитам.";
            ViewBag.kfss = SqlUtiltes.GetUaOkfs();
            return View();
        }

        public async Task<ActionResult> CompaniesGetExcel(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
                List<UADetails> details = await SqlUtiltes.GetUADetailsAsync(issuers);
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                MemoryStream memoryStream = new MemoryStream();
                ExportExcel exp = new ExportExcel();
                exp.ExportUAWorkbook(details, test).SaveAs(memoryStream);
                
                memoryStream.Position = 0;
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            else
            {
                return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
            }
        }
       

        [CheckRequest]
        public async Task<ActionResult> CompaniesDoSearch(CompaniesSearchObject so)
        {
            //проверим permition на сервере
            UserSession us = HttpContext.GetUserSession();
            bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
            //PageRoles pr = _GetRoles();


            //so.is_emitent = (pr.IsSet(RoleTypes.ShowEmitent) && !pr.IsSet(RoleTypes.ShowAll));

            so.regions = await SearchUaRepository.GetRegsAsync(so.regions, so.is_okato);
            so.industry = await SearchUaRepository.GetIndAsync(so.industry, so.is_okonh, so.ind_excl);
            so.okopf = await SearchUaRepository.GetOkopfAsync(so.okopf);
            //so.okfs = await SearchUaRepository.GetOkfsAsync(so.okfs);

            so.company = !String.IsNullOrWhiteSpace(so.company) ? so.company.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("!", "") : "";
            so.company = Helper.FullTextString(so.company);
            so.company = so.company.Replace("*", "");
            so.phone = !String.IsNullOrWhiteSpace(so.phone) ? so.phone.Replace("%20", "").Replace("(", "").Replace(")", "").Replace("-", "") : "";
            so.address = !String.IsNullOrWhiteSpace(so.address) ? so.address.Replace("%20", " ") : "";
            so.address = so.address.Replace(".", " ").Replace(" ул ", " ").Replace(" д ", " ").Replace(" к ", " ").Replace(" корп ", " ").Replace(" офис ", " ").Replace(" оф ", " ").Replace(" кв ", " ").Replace(" пр ", " ").Replace(" просп ", " ").Replace(" пер ", " ");
            so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Clear() : "";
            so.constitutor = "";
            so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
            so.industry = !String.IsNullOrWhiteSpace(so.industry) ? so.industry : "";
            so.okopf = !String.IsNullOrWhiteSpace(so.okopf) ? so.okopf : "";
            so.okfs = !String.IsNullOrWhiteSpace(so.okfs) ? so.okfs : "";
            so.trades = "";
            so.kod = "";
            so.dbeg = !String.IsNullOrWhiteSpace(so.dbeg) ? so.dbeg : "";
            so.dend = !String.IsNullOrWhiteSpace(so.dend) ? so.dend : "";
            so.top1000 = !String.IsNullOrEmpty(so.top1000) ? so.top1000 : "";
            so.group_name = "";
            so.fas = "";
            so.page_no = so.page_no == 0 ? 0 : so.page_no - 1;


            try
            {
                CompanyUaSearchQueryGenerator qg = new CompanyUaSearchQueryGenerator(so);
                SphinxQueryObject qo = qg.GetQuery();
                if (so.top1000 != "2")
                {
                    SphynxSearcher searcher = new SphynxSearcher(qo.Query, Configs.SphinxSearchUAPort, qo.CharasterSet, Configs.SphinxServer);
                    string result = searcher.SearchJson();
                    return Content("{" + result + "}");
                }
                else
                {
                    if (us.HasRole(roles, Key.canExport.ToString()))
                    {
                        SphynxSearcherDetailsUA searcher_details = new SphynxSearcherDetailsUA(qo.Query, Configs.SphinxSearchUAPort, qo.CharasterSet, Configs.SphinxServer);
                        List<UADetails> details = searcher_details.SearchDetails();
                        string filename = Guid.NewGuid().ToString() + ".xlsx";
                        string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                        MemoryStream memoryStream = new MemoryStream();
                        ExportExcel exp = new ExportExcel();
                        exp.ExportUAWorkbook(details, test).SaveAs(memoryStream);
                        memoryStream.Position = 0;
                        return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                    }
                    else
                    {
                        return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
                    }

                    
                    
                }
            }
            catch (Exception ex)
            {
                return Content(string.Format("<div style=\"display:none\" id=\"fnd\" val=\"-1\">{0}</div>Ошибка. Попробуйте позже. {0}", ex.Message));
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CompaniesDoSearchExcel(string so_string)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                CompaniesSearchObject so;
                bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
                try
                {
                    so = JsonConvert.DeserializeObject<CompaniesSearchObject>(so_string);
                    so.regions = await SearchUaRepository.GetRegsAsync(so.regions, so.is_okato);
                    so.industry = await SearchUaRepository.GetIndAsync(so.industry, so.is_okonh, so.ind_excl);
                    so.okopf = await SearchUaRepository.GetOkopfAsync(so.okopf);
                    //so.okfs = await SearchUaRepository.GetOkfsAsync(so.okfs);

                    so.company = !String.IsNullOrWhiteSpace(so.company) ? so.company.Replace("%20", " ").Replace(" - ", " ").Replace("№", "№ ").Replace("-D-", "+").Replace("!", "") : "";
                    so.company = Helper.FullTextString(so.company);
                    so.company = so.company.Replace("*", "");
                    so.phone = !String.IsNullOrWhiteSpace(so.phone) ? so.phone.Replace("%20", "").Replace("(", "").Replace(")", "").Replace("-", "") : "";
                    so.address = !String.IsNullOrWhiteSpace(so.address) ? so.address.Replace("%20", " ") : "";
                    so.address = so.address.Replace(".", " ").Replace(" ул ", " ").Replace(" д ", " ").Replace(" к ", " ").Replace(" корп ", " ").Replace(" офис ", " ").Replace(" оф ", " ").Replace(" кв ", " ").Replace(" пр ", " ").Replace(" просп ", " ").Replace(" пер ", " ");
                    so.ruler = !String.IsNullOrWhiteSpace(so.ruler) ? so.ruler.Replace("%20", " ").Clear() : "";
                    so.constitutor = "";
                    so.regions = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
                    so.industry = !String.IsNullOrWhiteSpace(so.industry) ? so.industry : "";
                    so.okopf = !String.IsNullOrWhiteSpace(so.okopf) ? so.okopf : "";
                    so.okfs = !String.IsNullOrWhiteSpace(so.okfs) ? so.okfs : "";
                    so.trades = "";
                    so.kod = "";
                    so.dbeg = !String.IsNullOrWhiteSpace(so.dbeg) ? so.dbeg : "";
                    so.dend = !String.IsNullOrWhiteSpace(so.dend) ? so.dend : "";
                    so.top1000 = !String.IsNullOrEmpty(so.top1000) ? so.top1000 : "";
                    so.group_name = "";
                    so.fas = "";
                    so.page_no = so.page_no == 0 ? 0 : so.page_no - 1;

                    CompanyUaSearchQueryGenerator qg = new CompanyUaSearchQueryGenerator(so);
                    SphinxQueryObject qo = qg.GetQuery();
                        SphynxSearcherDetailsUA searcher_details = new SphynxSearcherDetailsUA(qo.Query, Configs.SphinxSearchUAPort, qo.CharasterSet, Configs.SphinxServer);
                        List<UADetails> details = searcher_details.SearchDetails();
                        string filename = Guid.NewGuid().ToString() + ".xlsx";
                        string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                        MemoryStream memoryStream = new MemoryStream();
                        ExportExcel exp = new ExportExcel();
                        exp.ExportUAWorkbook(details, test).SaveAs(memoryStream);
                        memoryStream.Position = 0;
                        return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                   
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
    }

}