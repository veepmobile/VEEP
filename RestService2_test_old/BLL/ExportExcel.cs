using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RestService.BLL;
using RestService.Models;

namespace RestService.BLL
{
    public class ExportExcel
    {
        /*
        public XLWorkbook ExportOrders(List<Order> list)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Orders");

            List<string> aHead = new List<string> { "ФИО", "Номер телефона", "Дата", "ID заказа в системе", "ID заказа в банке", "Официант", "Стол", "Дата заказа" };
            List<int> aWidth = new List<int> { 30, 30, 20, 30, 30, 30, 10, 20 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }

            for (int i = 0; i <= list.Count - 1; i++)
            {
                ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Cell(i + 2, 1).Value = list[i].FIO;
                ws.Cell(i + 2, 2).Value = list[i].PhoneNumber;
                ws.Cell(i + 2, 3).Value = list[i].CreateDate.ToShortDateString();
                ws.Cell(i + 2, 4).Value = list[i].OrderNumberService;
                ws.Cell(i + 2, 5).Value = list[i].OrderNumberBank;
                ws.Cell(i + 2, 6).Value = list[i].Waiter.Name;
                ws.Cell(i + 2, 7).Value = list[i].TableID;
                ws.Cell(i + 2, 8).Value = list[i].OrderDate.ToShortDateString();

            }

            return wb;
        }

        */
    }
}