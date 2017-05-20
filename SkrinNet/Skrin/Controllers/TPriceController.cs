using Skrin.BLL.Authorization;
using Skrin.BLL.Root;
using Skrin.BLL.Infrastructure;
using Skrin.Models.QIVSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Root.TPrice;
using System.IO;
using Newtonsoft.Json;

namespace Skrin.Controllers
{
    public class TPriceController : Controller
    {
        private enum Key { canSearch, canExport, canAddToGroup};

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();


        static TPriceController()
        {
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.TPrice );
            roles.Add(Key.canAddToGroup.ToString(), AccessType.Pred | AccessType.TPrice);
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.TPrice);
        }

        // GET: TPrice
        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.Title = "СКРИН-Контрагент: Расчет интервала рентабельности.";
            ViewBag.Description = "Контролируемые сделки, Расчет интервала рентабельности,Трансфертное ценообразование";
            ViewBag.RolesJson = us.GetRigthList(roles);
            return View();
        }

        public async Task<ActionResult> NewId(string id)
        {
            return Content(await TPriceRepository.GetNewId(id));
        }

        public async Task<ActionResult> Search(TPriceTemplateParams tp,TPriceExtraParams ep)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return null;
            }

            var so = new TPriceSO
            {
                extra_params = ep,
                template_params = tp
            };

            
            so.template_params.regions = await SearchRepository.GetRegsForSqlSearchAsync(so.template_params.regions, 1);
            so.template_params.okfs = await SearchRepository.GetOkfsAsync(so.template_params.okfs,false);

            ///Добавление в группу
            if (so.extra_params.page_no == -1000)
            {
                return Json(await TPriceRepository.GroupAsync(so, user_id));
            }
            return Json(await TPriceRepository.SearchAsync(so, user_id));
           
             
        }

        public async Task<ActionResult> SearchExcel(string string_params)
        {
             UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canExport.ToString()))
            {
                return null;
            }
            bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
            TPriceSO so;
            try
            {
                so = JsonConvert.DeserializeObject<TPriceSO>(string_params);
            }
            catch
            {
                return HttpNotFound();
            }

            var excel_result = await TPriceRepository.SearchExcelAsync(so, user_id);
            var fs = new MemoryStream();
            TPriceExcelRepository.GetTpriceSearchExcel(excel_result,test).SaveAs(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return File(fs, "application/vnd.ms-excel", "t_price_result.xlsx"); 

        }

        public async Task<ActionResult> Calculate(string id, string ids)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
            {
                return null;
            }
            return Json(await TPriceRepository.CalculateAsync(id, ids));
        }

        public ActionResult CalculateExcel(string result)
        {
            TPriceResult t_result;
            try
            {
                t_result = JsonConvert.DeserializeObject<TPriceResult>(result);
            }
            catch
            {
                return HttpNotFound();
            }

            var fs = new MemoryStream();
            TPriceExcelRepository.GetTpriceResultExcel(t_result).SaveAs(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return File(fs, "application/vnd.ms-excel", "t_price_result.xlsx"); 
        }

        public async Task<ActionResult> GetGroups()
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canAddToGroup.ToString()))
            {
                return null;
            }
            return Json(AuthenticateSqlUtilites.GetExtGroupList(user_id),JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTemplates()
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return null;
            }
            var templates = await TPriceRepository.GetTemplates(user_id);
            return Json(templates.Select(p => new { id = p.id, name = p.name }), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTemplate(int id)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return null;
            }
            return Content(await TPriceRepository.GetTemplate(user_id, id), "aplication/json");
        }

        public async Task<ActionResult> SaveTemplate(string name, string template)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return HttpNotFound();
            }
            await TPriceRepository.SaveTemplate(user_id, name, template);
            return Content("1");
        }
    }
}