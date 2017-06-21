using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestService.BLL;
using RestService.Models;

namespace RestService.Controllers
{
    public class MessagesController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Фильтр
            Filters filter = new Filters();

            //Начальный диапазон дат
            filter.FilterBeginDate = ViewBag.beginDate = new DateTime(DateTime.Now.Year, 1, 1);
            filter.FilterEndDate = ViewBag.endDate = DateTime.Today;

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Title = "Сообщения";
            ViewBag.PageID = 6;



            return View();
        }

        
       [HttpPost]
       public ActionResult MessagesLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Сообщения";
           ViewBag.PageID = 6;

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);
           try
           {
               List<Messages> messages = new List<Messages>();
               messages = PersonalData.SqlGetMessagesAdm(begindate, enddate);
               if (messages != null)
               {
                   Session["messages"] = messages;
                   return PartialView(messages);
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
        


    }
}