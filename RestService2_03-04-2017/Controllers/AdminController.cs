using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RestService.Models;

namespace RestService.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {



            return View();
        }

        public ActionResult Login(string name, string psw)
        {
            Users user = new Users();
            user = AdminData.UserLogin(name, psw);
            string result = "";
            if (user != null && user.UserID != 0)
            {
                Session["user_id"] = user.UserID;
                Session["role_id"] = user.Roles;
                result = user.UserID.ToString();
            }
            else
            {
                result = "";
            }

            return Content(result);
        }


        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["role_id"] = null;
            return RedirectToAction("Index", "Admin");
        }
    }
}