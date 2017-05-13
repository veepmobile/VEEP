using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login(string login,string password)
        {
            UserSession us = HttpContext.GetUserSession();
            AuthenticationType auth_result = us.Login(login, password);
            return Json(new { id = (int)auth_result, message = auth_result.GetDescription() });
        }

        public ActionResult Logout()
        {
            UserSession us = HttpContext.GetUserSession();
            us.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Tramp()
        {
            UserSession us = HttpContext.GetUserSession();
            return Json(us.AuthenticationResult);
        }
    }
}