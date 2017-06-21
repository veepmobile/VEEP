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
    public class ReportsController : Controller
    {
        //Исходная страница со списком отчетов
       [HttpGet]
        public ActionResult Index()
        {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Title = "Отчеты";
            ViewBag.PageID = 7;

            return View();
        }

       [HttpGet]
       public ActionResult AccountNew()
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
          // filter.FilterRestaurantID = 202930001;

           ViewBag.Filter = filter;
           Session["Filters"] = filter;

           ViewBag.Title = "Отчеты: Новые посетители";
           ViewBag.PageID = 7;

           //int year = DateTime.Today.Year;
          // DateTime begindate = filter.FilterBeginDate = (filter.FilterBeginDate.Year == 1) ? new DateTime(year, 1, 1) : filter.FilterBeginDate;
          // DateTime enddate = filter.FilterEndDate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
          // TimeSpan time = new TimeSpan(23, 59, 59);
          // enddate = enddate.Add(time);

         //List<ReportAccountNew> report_accounts = new List<ReportAccountNew>();
           //report_accounts = AccountData.GetNewAccounts(begindate, enddate);

           return View();
       }


       [HttpPost]
       public ActionResult AccountNewLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Отчеты: Новые посетители";
           ViewBag.PageID = 7;

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);

           try
           {
               List<ReportAccountNew> report_accounts = new List<ReportAccountNew>();
               report_accounts = AccountData.GetNewAccounts(begindate, enddate);
               if (report_accounts != null)
               {
                   Session["report_accounts"] = report_accounts;
                   return PartialView(report_accounts);
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
       public ActionResult ExcelExport(string dfrom, string dto)
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
               var ws = new Worksheet("Новые пользователи");

               // Adding Headers
               ws.AddCell(0, 0, "Дата создания", 0);
               ws.AddCell(0, 1, "Телефон клиента", 0);
               ws.AddCell(0, 2, "ФИО клиента", 0);
               ws.AddCell(0, 3, "Дата заказа", 0);
               ws.AddCell(0, 4, "Ресторан", 0);
               ws.AddCell(0, 5, "Стол", 0);
               ws.AddCell(0, 6, "Официант", 0);
               ws.AddCell(0, 7, "Номер заказа в системе", 0);
               ws.AddCell(0, 8, "Номер заказа в банке", 0);

               // get data
               List<ReportAccountNew> list = AccountData.GetNewAccounts(begindate, enddate);
               if (list != null)
               {
                   int n = 0;
                   for (int i = 0; i < list.Count; i++)
                   {
                       ws.AddCell(i + n + 1, 0, (list[i].AccountReport.CreateDate.Year > 1970) ? (list[i].AccountReport.CreateDate.ToShortDateString() + " " + list[i].AccountReport.CreateDate.ToShortTimeString()) : "", 0);
                       ws.AddCell(i + n + 1, 1, list[i].AccountReport.PhoneNumber, 0);
                       ws.AddCell(i + n + 1, 2, list[i].AccountReport.FirstName + " " + list[i].AccountReport.LastName, 0);
                       if (list[i].AccountOrders != null && list[i].AccountOrders.Count > 0)
                       {
                           n++;
                           for (int j = 0; j < list[i].AccountOrders.Count; j++)
                           {
                               DateTime cdate = list[i].AccountOrders[j].OrderDate;
                               ws.AddCell(i + n + 1, 0, "", 0);
                               ws.AddCell(i + n + 1, 1, "", 0);
                               ws.AddCell(i + n + 1, 2, "", 0);
                               ws.AddCell(i + n + 1, 3, (cdate.Year > 1970) ? (cdate.ToShortDateString() + " " + cdate.ToShortTimeString()) : "", 0);
                               ws.AddCell(i + n + 1, 4, list[i].AccountOrders[j].RestaurantName, 0);
                               ws.AddCell(i + n + 1, 5, list[i].AccountOrders[j].TableID, 0);
                               ws.AddCell(i + n + 1, 6, list[i].AccountOrders[j].Waiter.Name, 0);
                               ws.AddCell(i + n + 1, 7, list[i].AccountOrders[j].OrderNumberService, 0);
                               ws.AddCell(i + n + 1, 8, list[i].AccountOrders[j].OrderNumberBank, 0);
                               n++;
                           }
                       }
                   }
                   wb.AddWorksheet(ws);

                   // generate xml 
                   var workstring = wb.ExportToXML();

                   // Send to user file
                   return new ExcelResult("AccountNew.xls", workstring);
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