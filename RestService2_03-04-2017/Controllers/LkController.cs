using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestService.BLL;
using RestService.Models;
using System.IO;
using Calabonga.Xml.Exports;

namespace RestService.Controllers
{
    public class LkController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       [HttpGet]
        public ActionResult Tips()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Lk");
            }

            // Фильтр
            Filters filter = new Filters();

            if (Session["restaurant_id"] != null)
            {
                filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
            }

            //Начальный диапазон дат
            DateTime dfrom = DateTime.Now;
            filter.FilterBeginDate = ViewBag.beginDate = dfrom;

            DateTime dto = DateTime.Now;
            TimeSpan dto_time = new TimeSpan(23, 59, 59);
            dto = dfrom.Add(dto_time);
            filter.FilterEndDate = ViewBag.endDate = dto;

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Title = "Чаевые";
            ViewBag.PageID = 100;

            return View();
        }

       [HttpPost]
       public ActionResult TipsLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Чаевые";
           ViewBag.PageID = 100;
           if (Session["restaurant_id"] != null)
           {
               filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
           }

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);
           try
           {
               List<Tip> tips = new List<Tip>();
               tips = LkData.GetTipsList(begindate, enddate, filter.FilterRestaurantID);
               if (tips != null)
               {
                   Session["tips"] = tips;
                   return PartialView(tips);
               }
               else
               {
                   return null;
               }
           }
           catch (Exception ex)
           {
               string error = ex.Message;
           }

           return null;
       }


       [HttpGet]
       public ActionResult Report()
       {
           // if (Session["user_id"] == null)
           // {
           //     return RedirectToAction("Index", "Lk");
           // }

           // Фильтр
           Filters filter = new Filters();

           //Начальный диапазон дат
           DateTime dfrom = DateTime.Now;
           filter.FilterBeginDate = ViewBag.beginDate = dfrom;

           DateTime dto = DateTime.Now;
           TimeSpan dto_time = new TimeSpan(23, 59, 59);
           dto = dfrom.Add(dto_time);
           filter.FilterEndDate = ViewBag.endDate = dto;

           ViewBag.Filter = filter;
           Session["Filters"] = filter;

           ViewBag.Title = "Отчеты";
           ViewBag.PageID = 101;

           return View();
       }

        #region Login


        public ActionResult Login(string name, string psw)
        {
            Users user = new Users();
            user = AdminData.UserLogin(name, psw);
            string result = "";
            if (user != null && user.UserID != 0)
            {
                Session["user_id"] = user.UserID;
                Session["restaurant_id"] = user.RestaurantID;
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
            return RedirectToAction("Index", "Lk");
        }



        #endregion



    }
}