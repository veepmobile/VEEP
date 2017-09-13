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

        [HttpGet]
        public ActionResult MainReport()
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

            ViewBag.Title = "Отчеты: Сводный отчет";
            ViewBag.PageID = 7;

            return View();

        }

        [HttpPost]
        public ActionResult MainReportLoad(Filters filter)
        {
            ViewBag.Filter = filter;
            Session["Filters"] = filter;
            ViewBag.Title = "Отчеты: Сводный отчет";
            ViewBag.PageID = 7;

            DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
            DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
            TimeSpan time = new TimeSpan(23, 59, 59);
            enddate = enddate.Add(time);

            try
            {
                List<ReportMain> report_main = new List<ReportMain>();
                report_main = ReportData.GetMainReport(begindate, enddate);
                if (report_main != null)
                {
                    Session["report_main"] = report_main;
                    return PartialView(report_main);
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
        public ActionResult MainReportExport(string dfrom, string dto)
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
                var ws = new Worksheet("Сводный отчет");

                // Adding Headers
                ws.AddCell(0, 0, "Код страны", 0);
                ws.AddCell(0, 1, "Аккаунт", 0);
                ws.AddCell(0, 2, "Статус аккаунта", 0);
                ws.AddCell(0, 3, "ОС", 0);
                ws.AddCell(0, 4, "Модель телефона", 0);
                ws.AddCell(0, 5, "Банковская карта", 0);
                ws.AddCell(0, 6, "Дисконтная карта", 0);
                ws.AddCell(0, 7, "Дата заказа", 0);
                ws.AddCell(0, 8, "Ресторан", 0);
                ws.AddCell(0, 9, "Стол", 0);
                ws.AddCell(0, 10, "Сумма без скидки, руб.", 0);
                ws.AddCell(0, 11, "Скидка, руб.", 0);
                ws.AddCell(0, 12, "Сумма с учетом скидки, руб.", 0);
                ws.AddCell(0, 13, "Чаевые, %", 0);
                ws.AddCell(0, 14, "Чаевые, руб.", 0);
                ws.AddCell(0, 15, "Официант", 0);
                ws.AddCell(0, 16, "Результат платежа", 0);
                ws.AddCell(0, 17, "Банковская карта", 0);
                ws.AddCell(0, 18, "Дата создания аккаунта", 0);
                ws.AddCell(0, 19, "Дата изменения аккаунта", 0);
                ws.AddCell(0, 20, "Дата последнего использования", 0);

                // get data
                List<ReportMain> list = ReportData.GetMainReport(begindate, enddate);
                if (list != null)
                {
                    int n = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        ws.AddCell(i + n + 1, 0, list[i].PhoneCode, 0);
                        ws.AddCell(i + n + 1, 1, list[i].PhoneNumber, 0);
                        ws.AddCell(i + n + 1, 2, (list[i].IsValid == 1 ? "Активен" : "Не активен"), 0);
                        ws.AddCell(i + n + 1, 3, (list[i].OS == 1 ? "iOS" : "Андроид"), 0);
                        ws.AddCell(i + n + 1, 4, list[i].PhoneModel, 0);
                        ws.AddCell(i + n + 1, 5, list[i].BankCards, 0);
                        ws.AddCell(i + n + 1, 6, list[i].DiscountCards, 0);
                        ws.AddCell(i + n + 1, 7, (list[i].OrderDate.Year > 1970) ? (list[i].OrderDate.ToShortDateString() + " " + list[i].OrderDate.ToShortTimeString()) : "", 0);
                        ws.AddCell(i + n + 1, 8, list[i].RestaurantName, 0);
                        ws.AddCell(i + n + 1, 9, list[i].TableID, 0);
                        ws.AddCell(i + n + 1, 10, list[i].OrderTotal.ToString("0.00"), 0);
                        ws.AddCell(i + n + 1, 11, list[i].DiscountSum.ToString("0.00"), 0);
                        ws.AddCell(i + n + 1, 12, list[i].OrderSum.ToString("0.00"), 0);
                        ws.AddCell(i + n + 1, 13, list[i].TippingProcent.ToString("0.00"), 0);
                        ws.AddCell(i + n + 1, 14, list[i].TippingSum.ToString("0.00"), 0);
                        ws.AddCell(i + n + 1, 15, list[i].Waiter, 0);
                        ws.AddCell(i + n + 1, 16, list[i].PaymentResult, 0);
                        ws.AddCell(i + n + 1, 17, (list[i].CardMaskPan + " / " + list[i].CardExpiration + " / " + list[i].CardHolderName), 0);
                        ws.AddCell(i + n + 1, 18, (list[i].AccountCreateDate.Year > 1970) ? (list[i].AccountCreateDate.ToShortDateString()) : "", 0);
                        ws.AddCell(i + n + 1, 19, (list[i].AccountUpdateDate.Year > 1970) ? (list[i].AccountUpdateDate.ToShortDateString()) : "", 0);
                        ws.AddCell(i + n + 1, 20, (list[i].AccountLastDate.Year > 1970) ? (list[i].AccountLastDate.ToShortDateString()) : "", 0);
                    }
                    wb.AddWorksheet(ws);

                    // generate xml 
                    var workstring = wb.ExportToXML();

                    // Send to user file
                    return new ExcelResult("MainReport.xls", workstring);
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
                return RedirectToAction("Index", "Admin");
            }

            // Фильтр
            Filters filter = new Filters();

            /*
            if (Session["restaurant_id"] != null)
            {
                filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
            }*/
            //Список ресторанов
            List<Restaurant> restlist = RestaurantData.GetRestaurants();
            filter.FilterRestaurantID = ViewBag.FilterRestaurantID = 0; //по умолчанию все
            ViewBag.RestList = restlist;

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
            /*
            if (Session["restaurant_id"] != null)
            {
                filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
            }*/

            DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
            DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
            if (begindate == enddate)
            {
                TimeSpan time = new TimeSpan(23, 59, 59);
                enddate = enddate.Add(time);
            }
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

        [HttpGet]
        public ActionResult PrizeOther()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Фильтр
            Filters filter = new Filters();

            /*
            if (Session["restaurant_id"] != null)
            {
                filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
            }*/
            //Список ресторанов
            List<Restaurant> restlist = RestaurantData.GetRestaurants();
            filter.FilterRestaurantID = ViewBag.FilterRestaurantID = 0; //по умолчанию все
            ViewBag.RestList = restlist;

            //Начальный диапазон дат
            DateTime dfrom = DateTime.Now;
            filter.FilterBeginDate = ViewBag.beginDate = dfrom;

            DateTime dto = DateTime.Now;
            TimeSpan dto_time = new TimeSpan(23, 59, 59);
            dto = dfrom.Add(dto_time);
            filter.FilterEndDate = ViewBag.endDate = dto;

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Title = "Премия (повторная оплата)";
            ViewBag.PageID = 102;

            return View();
        }

        [HttpPost]
        public ActionResult PrizeOtherLoad(Filters filter)
        {
            ViewBag.Filter = filter;
            Session["Filters"] = filter;
            ViewBag.Title = "Премия (повторная оплата)";
            ViewBag.PageID = 102;
           /* if (Session["restaurant_id"] != null)
            {
                filter.FilterRestaurantID = ViewBag.FilterRestaurantID = (int)Session["restaurant_id"];
            }*/

            DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
            DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
            if (begindate == enddate)
            {
                TimeSpan time = new TimeSpan(23, 59, 59);
                enddate = enddate.Add(time);
            }
            try
            {
                List<Prize> prize = new List<Prize>();
                prize = LkData.GetPrizeOtherList(begindate, enddate, filter.FilterRestaurantID);
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
        public ActionResult PrizeOtherExport(string dfrom, string dto, int restaurant_id)
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
                var ws = new Worksheet("Премия (повторная оплата)");

                // Adding Headers
                ws.AddCell(0, 0, "Дата платежа", 0);
                ws.AddCell(0, 1, "Ресторан", 0);
                ws.AddCell(0, 2, "Стол", 0);
                ws.AddCell(0, 3, "Официант", 0);

                // get data
                List<Prize> list = LkData.GetPrizeOtherList(begindate, enddate, restaurant_id);
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
                    return new ExcelResult("PrizeOther.xls", workstring);
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