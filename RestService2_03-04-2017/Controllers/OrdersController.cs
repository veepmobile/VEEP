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
    public class OrdersController : Controller
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

            ViewBag.Title = "Заказы";
            ViewBag.PageID = 4;

            //ViewBag.Restaurants = RestaurantData.GetRestNetworkList();
            //ViewBag.Restaurants = RestaurantData.GetRestaurants();

            return View();
        }

        
       [HttpPost]
       public ActionResult OrdersLoad(Filters filter)
       {
           ViewBag.Filter = filter;
           Session["Filters"] = filter;
           ViewBag.Title = "Заказы";
           ViewBag.PageID = 4;

           //временно
           //filter.FilterRestNetworkID = 201;
           //filter.FilterRestaurantID = 202930001;

           DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
           DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);
           try
           {
            List<Order> orders = new List<Order>();
            orders = OrderData.GetOrderList(begindate, enddate);
               if (orders != null)
               {
                   Session["orders"] = orders;
                   return PartialView(orders);
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

       public ActionResult OrdersDetails(int? id)
       {
           string details = "";

           details = OrderData.GetOrderDetails(id);

           return Content(details);
       }

        

        [HttpGet]
       public ActionResult OrdersExcel(string dfrom, string dto)
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
               var ws = new Worksheet("Заказы");

               // Adding Headers
               ws.AddCell(0, 0, "Дата заказа", 0);
               ws.AddCell(0, 1, "Ресторан", 0);
               ws.AddCell(0, 2, "Стол", 0);
               ws.AddCell(0, 3, "Телефон клиента", 0);
               ws.AddCell(0, 4, "Номер заказа в ИМ", 0);
               ws.AddCell(0, 5, "Номер заказа в сервисе", 0);
               ws.AddCell(0, 6, "Номер заказа в банке", 0);
               ws.AddCell(0, 7, "Сумма без скидки, руб.", 0);
               ws.AddCell(0, 8, "Скидка, руб.", 0);
               ws.AddCell(0, 9, "Скидка Veep, %", 0);
               ws.AddCell(0, 10, "Скидка Veep, руб.", 0);
               ws.AddCell(0, 11, "Сумма с учетом скидки, руб.", 0);
               ws.AddCell(0, 12, "Платеж в банк, руб.", 0);
               ws.AddCell(0, 13, "Статус заказа", 0);
               ws.AddCell(0, 14, "Официант", 0);

               // get data
               List<Order> list = OrderData.GetOrderList(begindate, enddate);
               if (list != null)
               {
                   int n = 0;
                   for (int i = 0; i < list.Count; i++)
                   {
                       ws.AddCell(i + n + 1, 0, (list[i].OrderDate.Year > 1970) ? (list[i].OrderDate.ToShortDateString() + " " + list[i].OrderDate.ToShortTimeString()) : "", 0);
                       ws.AddCell(i + n + 1, 1, list[i].RestaurantName, 0);
                       ws.AddCell(i + n + 1, 2, list[i].TableID, 0);
                       ws.AddCell(i + n + 1, 3, list[i].PhoneNumber, 0);
                       ws.AddCell(i + n + 1, 4, list[i].OrderNumber, 0);
                       ws.AddCell(i + n + 1, 5, list[i].OrderNumberService, 0);
                       ws.AddCell(i + n + 1, 6, list[i].OrderNumberBank, 0);
                       ws.AddCell(i + n + 1, 7, list[i].OrderPayment.OrderTotal.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 8, list[i].OrderPayment.DiscountSum.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 9, list[i].MainDiscountProc.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 10, list[i].MainDiscountSum.ToString("0.00"), 0);
                       ws.AddCell(i + n + 1, 11, ((Convert.ToDecimal(list[i].OrderPayment.OrderSum)).ToString("0.00")), 0);
                       ws.AddCell(i + n + 1, 12, (list[i].OrderPayment.PaymentBank > 0) ? list[i].OrderPayment.PaymentBank.ToString("0.00") : "", 0);
                       ws.AddCell(i + n + 1, 13, list[i].OrderStatus.StatusName, 0);
                       ws.AddCell(i + n + 1, 14, list[i].Waiter.Name, 0);                       
                   }
                   wb.AddWorksheet(ws);

                   // generate xml 
                   var workstring = wb.ExportToXML();

                   // Send to user file
                   return new ExcelResult("Orders.xls", workstring);
               }
           }
           catch (Exception ex)
           {
               string error = ex.Message;
           }

           return null;
       }

        /*
       [HttpGet]
       public ActionResult OrdersExcel(Filters filters)
       {
           DateTime begindate = (filters.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filters.FilterBeginDate;
           DateTime enddate = (filters.FilterEndDate.Year == 1) ? DateTime.Today : filters.FilterEndDate;
           TimeSpan time = new TimeSpan(23, 59, 59);
           enddate = enddate.Add(time);
           try
           {
            List<Order> orders = new List<Order>();
            orders = OrderData.GetOrderList(begindate, enddate);
               if (orders != null)
               {
                   string filename = Guid.NewGuid().ToString() + ".xlsx";
                   string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + "\\" + filename;
                   using (MemoryStream memoryStream = new MemoryStream())
                   {
                       ExportExcel exp = new ExportExcel();
                       exp.ExportOrders(orders).SaveAs(memoryStream);
                       memoryStream.Position = 0;
                       FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                       memoryStream.WriteTo(file);
                       file.Close();
                       memoryStream.Close();
                   }
                   //return File(Path.Combine(@"C:\inetpub\wwwroot\RestService\xlsreports", filename), "application/xls");
                   return Content(filename);
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