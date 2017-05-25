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
       public ActionResult TipsExport(string dfrom, string dto, int restaurant_id)
       {
           DateTime begindate = DateTime.Parse(dfrom);
           begindate = (begindate.Year == 1) ? new DateTime(1900, 1, 1) : begindate;
           DateTime enddate = DateTime.Parse(dto);
           enddate = (enddate.Year == 1) ? DateTime.Today : enddate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);

           try
           {
               var result = string.Empty;
               var wb = new Workbook();
               // properties
               wb.Properties.Created = DateTime.Today;

               // options sheets
               wb.ExcelWorkbook.ActiveSheet = 1;
               wb.ExcelWorkbook.DisplayInkNotes = false;
               wb.ExcelWorkbook.FirstVisibleSheet = 1;
               wb.ExcelWorkbook.ProtectStructure = false;
               wb.ExcelWorkbook.WindowHeight = 800;
               wb.ExcelWorkbook.WindowTopX = 0;
               wb.ExcelWorkbook.WindowTopY = 0;
               wb.ExcelWorkbook.WindowWidth = 600;

               // create style s1 for header
               var s1 = new Style("s1") { Font = new Font { Bold = true, Italic = true, Color = "#FF0000" } };
               wb.AddStyle(s1);

               // create style s2 for header
               var s2 = new Style("s2") { Font = new Font { Bold = true, Italic = true, Size = 12, Color = "#0000FF" } };
               wb.AddStyle(s2);

               // First sheet
               var ws = new Worksheet("Чаевые");

               // Adding Headers
               ws.AddCell(0, 0, "Дата заказа", 0);
               ws.AddCell(0, 1, "Ресторан", 0);
               ws.AddCell(0, 2, "Стол", 0);
               ws.AddCell(0, 3, "Сумма заказа, руб.", 0);
               ws.AddCell(0, 4, "Чаевые, %", 0);
               ws.AddCell(0, 5, "Чаевые, руб.", 0);
               ws.AddCell(0, 6, "Чаевые к выдаче, руб.", 0);
               ws.AddCell(0, 7, "Официант", 0);

               // get data
               List<Tip> list = LkData.GetTipsList(begindate, enddate, restaurant_id);
               if (list != null)
               {
                   int n = 0;
                   for (int i = 0; i < list.Count; i++)
                   {
                       ws.AddCell(i + n + 1, 0, (list[i].PaymentDate.Year > 1970) ? (list[i].PaymentDate.ToShortDateString() + " " + list[i].PaymentDate.ToShortTimeString()) : "", 0);
                       ws.AddCell(i + n + 1, 1, list[i].RestaurantName, 0);
                       ws.AddCell(i + n + 1, 2, list[i].TableID, 0);
                       ws.AddCell(i + n + 1, 3, list[i].OrderSum, 0);
                       ws.AddCell(i + n + 1, 4, list[i].TippingProcent.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 5, list[i].TippingSum.ToString("0.00"), 0);
                       decimal tips = Decimal.Round((list[i].TippingSum * 90) / 100);
                       ws.AddCell(i + n + 1, 6, tips.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 7, list[i].WaiterName, 0);
                   }
                   wb.AddWorksheet(ws);

                   // generate xml 
                   var workstring = wb.ExportToXML();

                   // Send to user file
                   return new ExcelResult("Tipping.xls", workstring);
               }
           }
           catch (Exception ex)
           {
               string error = ex.Message;
           }

           return null;
       }

       [HttpGet]
       public ActionResult Prize()
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

           ViewBag.Title = "Премия";
           ViewBag.PageID = 101;

           return View();
       }

       [HttpPost]
       public ActionResult PrizeLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Премия";
           ViewBag.PageID = 101;
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
               List<Prize> prize = new List<Prize>();
               prize = LkData.GetPrizeList(begindate, enddate, filter.FilterRestaurantID);
               if (prize != null)
               {
                   return PartialView(prize);
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
       public ActionResult PrizeExport(string dfrom, string dto, int restaurant_id)
       {
           DateTime begindate = DateTime.Parse(dfrom);
           begindate = (begindate.Year == 1) ? new DateTime(1900, 1, 1) : begindate;
           DateTime enddate = DateTime.Parse(dto);
           enddate = (enddate.Year == 1) ? DateTime.Today : enddate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);

           try
           {
               var result = string.Empty;
               var wb = new Workbook();
               // properties
               wb.Properties.Created = DateTime.Today;

               // options sheets
               wb.ExcelWorkbook.ActiveSheet = 1;
               wb.ExcelWorkbook.DisplayInkNotes = false;
               wb.ExcelWorkbook.FirstVisibleSheet = 1;
               wb.ExcelWorkbook.ProtectStructure = false;
               wb.ExcelWorkbook.WindowHeight = 800;
               wb.ExcelWorkbook.WindowTopX = 0;
               wb.ExcelWorkbook.WindowTopY = 0;
               wb.ExcelWorkbook.WindowWidth = 600;

               // create style s1 for header
               var s1 = new Style("s1") { Font = new Font { Bold = true, Italic = true, Color = "#FF0000" } };
               wb.AddStyle(s1);

               // create style s2 for header
               var s2 = new Style("s2") { Font = new Font { Bold = true, Italic = true, Size = 12, Color = "#0000FF" } };
               wb.AddStyle(s2);

               // First sheet
               var ws = new Worksheet("Премия");

               // Adding Headers
               ws.AddCell(0, 0, "Дата платежа", 0);
               ws.AddCell(0, 1, "Ресторан", 0);
               ws.AddCell(0, 2, "Стол", 0);
               ws.AddCell(0, 3, "Официант", 0);

               // get data
               List<Prize> list = LkData.GetPrizeList(begindate, enddate, restaurant_id);
               if (list != null)
               {
                   int n = 0;
                   for (int i = 0; i < list.Count; i++)
                   {
                       ws.AddCell(i + n + 1, 0, (list[i].PaymentDate.Year > 1970) ? (list[i].PaymentDate.ToShortDateString() + " " + list[i].PaymentDate.ToShortTimeString()) : "", 0);
                       ws.AddCell(i + n + 1, 1, list[i].RestaurantName, 0);
                       ws.AddCell(i + n + 1, 2, list[i].TableID, 0);
                       ws.AddCell(i + n + 1, 7, list[i].WaiterName, 0);
                   }
                   wb.AddWorksheet(ws);

                   // generate xml 
                   var workstring = wb.ExportToXML();

                   // Send to user file
                   return new ExcelResult("Prize.xls", workstring);
               }
           }
           catch (Exception ex)
           {
               string error = ex.Message;
           }

           return null;
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