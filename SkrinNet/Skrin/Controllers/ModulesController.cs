using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models.Authentication;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public JsonResult GetBones(string input)
        {
            var ret = SqlUtiltes.GetBones(input).Select(p => new { name = p.Item1, cnt = p.Item2 }).ToList();
            if(ret.Count>0)
                return Json(ret, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetBonesUA(string input)
        {
            var ret = SqlUtiltes.GetBonesUA(input).Select(p => new { name = p.Item1, cnt = p.Item2 }).ToList();
            if (ret.Count > 0)
                return Json(ret, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBonesKZ(string input)
        {
            var ret = SqlUtiltes.GetBonesKZ(input).Select(p => new { name = p.Item1, cnt = p.Item2 }).ToList();
            if (ret.Count > 0)
                return Json(ret, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroups(string issuer_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return null;
            List<ListGroup> groups = string.IsNullOrEmpty(issuer_id) ? AuthenticateSqlUtilites.GetGroupList(us.UserId):AuthenticateSqlUtilites.GetGroupList(us.UserId,issuer_id);
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveGroup(int? id, string iss, string newname,bool? is1000)
        {
            if(!string.IsNullOrEmpty(iss))
            {
                UserSession us = HttpContext.GetUserSession();
                int count;
                AuthenticateSqlUtilites.SaveGroup(us.UserId, ref id, ref newname, iss, out count, is1000);
                if (count < 0)
                    return Content("Суммарное количество предприятий в группах, предназначенных для рассылки обновлений, не может превышать " + us.GroupLimit + " шт.<br/>При текущем добавлении количество предприятий в группе составит " + Math.Abs(count) + "<br/>Запись прервана.");
            }
            return Content("");
        }

        public JsonResult UpdateGroups()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return null;
            List<ExtGroup> groups = AuthenticateSqlUtilites.GetExtGroupList(us.UserId);
            return Json(groups, JsonRequestBehavior.AllowGet);
        }
    }
}