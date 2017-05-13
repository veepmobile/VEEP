using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public ActionResult Issuers(string ticker)
        {
            return RedirectPermanent(string.Format("/Issuers/{0}", ticker));
        }

        public ActionResult ProfileIp(string iss)
        {
            return RedirectPermanent(string.Format("/profileip/{0}", iss));
        }
    }
}