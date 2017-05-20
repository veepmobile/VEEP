using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Text;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Search;
using Skrin.BLL.Root;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.Controllers
{
    public class MultilinksController : Controller
    {
        private enum Key { canAccess };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        static MultilinksController()
        {
            roles.Add(Key.canAccess.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.CanAccess = us.HasRole(roles, Key.canAccess.ToString());
            ViewBag.LocPath = "/dbsearch/dbsearchru/multilinks/";

            return View();
        }

        public async Task<ActionResult> Search(MultilinksObject so)
        {
            so.codes = (string.IsNullOrEmpty(so.codes)) ? "" : so.codes;

            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Content("{\"error\" : \"Нет прав доступа\"}");

            if (!us.HasRole(roles, Key.canAccess.ToString()))
                return Content("{\"error\" : \"Нет прав доступа\"}");

            try
            {
                MultilinksSearcher ms = new MultilinksSearcher();

                JObject r = await ms.GetLinks(so.codes, so.listid);

                return Content(r.ToString());
            }
            catch (Exception ex)
            {
                return Content("{\"error\" : \"Ошибка выполнения запроса: " + ex.ToString() + "\"}");
            }
        }

        public ActionResult GroupList()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null, JsonRequestBehavior.AllowGet);
            var group_list = AuthenticateSqlUtilites.GetGroupList(us.UserId).Select(p => new { id = p.lid, name = p.name, cnt = p.cnt }).ToList();
            //var group_list = AuthenticateSqlUtilites.GetGroupList(2806).Select(p => new { id = p.lid, name = p.name, cnt = p.cnt }).ToList();
            return Json(group_list, JsonRequestBehavior.AllowGet);
        }
    }
}