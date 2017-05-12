using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System.Threading.Tasks;
using Skrin.BLL.Root;
using System.IO;
using SKRIN;
using Skrin.Models.QIVSearch;
using Newtonsoft.Json;



namespace Skrin.Controllers
{
    public class QIVSearchController : BaseController
    {
        private enum Key { canSearch, canExport, canAddToGroup };

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static QIVSearchController()
        {
            roles.Add(Key.canExport.ToString(), AccessType.Pred);
            roles.Add(Key.canAddToGroup.ToString(), AccessType.Pred);
            roles.Add(Key.canSearch.ToString(), AccessType.Pred);
        }

        public static List<string> GetGKSYearList()
        {
            List<string> years = new List<string>();
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query q = sql.OpenQuery("SELECT year from GKS..GKS_QIV_Years");
                while (q.Read())
                {
                    years.Add(((int)q.GetFieldAsInt("year")).ToString());
                }
            }
            return years;
        }

        public async Task<JsonResult> DoSearch(QIVSearchParams param)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return null;
            }
           return Json(await QIVParamRepository.DoSearch(param), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> DoSearchToGroup(QIVSearchParams param)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
            {
                return Content("");
            }
            int count = await QIVParamRepository.DoSearchToGroup(param,user_id,us.GroupLimit);
            if (count < 0)
                return Content("Суммарное количество предприятий в группах, предназначенных для рассылки обновлений, не может превышать " + us.GroupLimit + " шт.<br/>При текущем добавлении количество предприятий в группе составит " + Math.Abs(count) + "<br/>Запись прервана.");
            return Content("Количество предприятий в группе " + count);
        }

/*
        public async Task<ActionResult> DoSearchToExcel(QIVSearchParams param)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canExport.ToString()))
            {
                return null;
            }
            var excel_result = await QIVParamRepository.DoSearchToExcel(param);
            var fs = new MemoryStream();
            QIVParamRepository.GetQIVSearchExcel(excel_result).SaveAs(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return File(fs, "application/vnd.ms-excel", "qiv_result.xlsx");
        }
*/
        public async Task<ActionResult> DoSearchToExcel(string string_params)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canExport.ToString()))
            {
                return null;
            }
            bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);

            QIVSearchParams param;
            param = JsonConvert.DeserializeObject<QIVSearchParams>(string_params);
            var excel_result = await QIVParamRepository.DoSearchToExcel(param);
            var fs = new MemoryStream();
            ExcelGenerator.SimpleExcel(excel_result,"Лист", test).SaveAs(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return File(fs, "application/vnd.ms-excel", "qiv_result.xlsx");
        }
 
        // GET: QIVSearch
        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.Title = "СКРИН-Контрагент: Поиск по показателям.";
            ViewBag.Description = "Поиск по показателям бухгалтерской отчетности: РСБУ, МСФО.";
            return View("QIVSearch");
        }

        public async Task<ActionResult> GetTemplates()
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
            {
                return null;
            }
            return Json(await QIVParamRepository.GetTemplates(user_id), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTemplate(int id)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
            {
                return null;
            }
            return Content(await QIVParamRepository.GetTemplate(user_id, id), "aplication/json");
        }

        public async Task<ActionResult> SaveTemplate(string name, string template)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
            {
                return HttpNotFound();
            }
            await QIVParamRepository.SaveTemplate(user_id, name, template);
            return Content("1");
        }


    }
}