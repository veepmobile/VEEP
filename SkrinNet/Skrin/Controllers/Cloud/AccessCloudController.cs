using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers.Cloud
{
    public class AccessCloudController : Controller
    {
        public ActionResult ConfirmCloudUsing()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId > 0)
            {
                us.ConfirmCloudUsing();
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public ActionResult CheckCloudUsing()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId > 0)
            {
                return Json(us.User.UseCloud, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}