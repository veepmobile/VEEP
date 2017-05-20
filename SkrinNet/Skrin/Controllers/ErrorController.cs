using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Authorization;

namespace Skrin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult BlockedIp()
        {
            Response.StatusCode = 403;
            return View((object)Utilites.GetIP(HttpContext));
        }


        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;

            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}