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
    public class TipController : Controller
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
            //filter.FilterRestNetworkID = 201;
            //filter.FilterRestaurantID = 202930001;

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Title = "Чаевые";
            ViewBag.PageID = 8;

            //ViewBag.Restaurants = RestaurantData.GetRestNetworkList();
            //ViewBag.Restaurants = RestaurantData.GetRestaurants();

            return View();
        }

        
       [HttpPost]
       public ActionResult TipLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Чаевые";
           ViewBag.PageID = 8;

           //временно
           //filter.FilterRestNetworkID = 201;
           //filter.FilterRestaurantID = 202930001;

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);
           try
           {
            List<Tip> tips = new List<Tip>();
            tips = OrderData.GetTipsList(begindate, enddate);
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
       public ActionResult TipExcel(string dfrom, string dto)
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
               ws.AddCell(0, 3, "Телефон клиента", 0);
               ws.AddCell(0, 4, "Номер заказа в ИМ", 0);
               ws.AddCell(0, 5, "Сумма заказа, руб.", 0);
               ws.AddCell(0, 6, "Чаевые, %", 0);
               ws.AddCell(0, 7, "Чаевые, руб.", 0);
               ws.AddCell(0, 8, "Официант", 0);

               // get data
               List<Tip> list = OrderData.GetTipsList(begindate, enddate);
               if (list != null)
               {
                   int n = 0;
                   for (int i = 0; i < list.Count; i++)
                   {
                       ws.AddCell(i + n + 1, 0, (list[i].PaymentDate.Year > 1970) ? (list[i].PaymentDate.ToShortDateString() + " " + list[i].PaymentDate.ToShortTimeString()) : "", 0);
                       ws.AddCell(i + n + 1, 1, list[i].RestaurantName, 0);
                       ws.AddCell(i + n + 1, 2, list[i].TableID, 0);
                       ws.AddCell(i + n + 1, 3, list[i].PhoneNumber, 0);
                       ws.AddCell(i + n + 1, 4, list[i].OrderNumber, 0);
                       ws.AddCell(i + n + 1, 5, list[i].OrderSum, 0);
                       ws.AddCell(i + n + 1, 6, list[i].TippingProcent.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 7, list[i].TippingSum.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 8, list[i].WaiterName, 0);                       
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


        
    }
}