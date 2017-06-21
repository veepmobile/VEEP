using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RestService.BLL;
using RestService.Models;

namespace RestService.Controllers
{
    public class RestaurantsController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {
            // Фильтр
            //Filters filter = new Filters();

            //Начальный диапазон дат
            //filter.FilterBeginDate = ViewBag.beginDate = new DateTime(DateTime.Now.Year, 1, 1);
            //filter.FilterEndDate = ViewBag.endDate = DateTime.Today;

            //ViewBag.Filter = filter;
            //Session["Filters"] = filter;

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
           

            ViewBag.Title = "Рестораны";
            ViewBag.PageID = 1;

            List<RestNetwork> rest_network = new List<RestNetwork>();
            rest_network = RestaurantData.GetRestNetwork("admin");

            return View(rest_network);
        }

        /*
       [HttpPost]
       public ActionResult RestaurantsLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Рестораны";
           ViewBag.PageID =1;

           //DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           //DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           try
           {
               List<RestNetwork> rest_network = new List<RestNetwork>();
               rest_network = RestaurantData.GetRestNetwork("admin");
               if (rest_network != null)
               {
                   Session["rest_network"] = rest_network;
                   return PartialView(rest_network);
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
        */


    }
}