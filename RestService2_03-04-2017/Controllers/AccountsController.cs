using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestService.BLL;
using RestService.Models;

namespace RestService.Controllers
{
    public class AccountsController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Title = "Аккаунты";
            ViewBag.PageID = 3;

            // Фильтр
            Filters filter = new Filters();

            //Начальный диапазон дат
            filter.FilterBeginDate = ViewBag.beginDate = new DateTime(DateTime.Now.Year, 1, 1);
            filter.FilterEndDate = ViewBag.endDate = DateTime.Today;
            TimeSpan time = new TimeSpan(24, 01, 01);
            filter.FilterEndDate = filter.FilterEndDate.Add(time);

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Restaurants = RestaurantData.GetRestaurants();

            List<Account> accounts = new List<Account>();
            accounts = AccountData.GetAccountsList(filter.FilterBeginDate, filter.FilterEndDate);

            return View(accounts);
        }

       
      [HttpPost]
      public ActionResult AccountsLoad(Filters filter)
      {
          ViewBag.Filter = filter;
          Session["Filters"] = filter;
          ViewBag.Title = "Аккаунты";
          ViewBag.PageID = 3;

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);

          try
          {
            List<Account> accounts = new List<Account>();
            accounts = AccountData.GetAccountsList(filter.FilterBeginDate, filter.FilterEndDate);
               if (accounts != null)
               {
                   Session["accounts"] = accounts;
                   return PartialView(accounts);
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
       

       public ActionResult ChangeAccountStatus(int id, int status_id)
       {
           string ret = "";

           ret = AccountData.ChangeAccountStatus(id, status_id);

           return Content(ret);
       }

       public ActionResult DeleteAccount(int id)
       {
           string ret = "";

           ret = AccountData.DeleteAccount(id);

           return Content(ret);
       }

        public ActionResult RedoPsw(int id, string phone)
        {
            string ret = "";

            ret = AccountData.RedoPsw(id, phone);

            return Content(ret);
        }

    }
}