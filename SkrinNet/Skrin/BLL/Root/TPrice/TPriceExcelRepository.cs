using ClosedXML.Excel;
using Skrin.Models.QIVSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;


namespace Skrin.BLL.Root.TPrice
{
    public class TPriceExcelRepository
    {
        public static XLWorkbook GetTpriceResultExcel(TPriceResult result)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Рентабельность");

            List<string> aHead = new List<string> { "Значение усредненного показателя", "Валовая рентабельность", "Валовая рентабельность затрат", "Рентабельность продаж", "Рентабельность затрат", "Рентабельность коммерческих и\n управленческих расходов", "Рентабельность активов"};
            List<int> aWidth = new List<int> { 100, 30, 30, 30, 30, 30, 30};

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }

            var i=2;

            foreach (var item in result.values)
            {
                ws.Row(i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Cell(i, 1).Value = item.name;
                ws.Cell(i, 2).Value = item.v1;
                ws.Cell(i, 3).Value = item.v2;
                ws.Cell(i, 4).Value = item.v3;
                ws.Cell(i, 5).Value = item.v4;
                ws.Cell(i, 6).Value = item.v5;
                ws.Cell(i, 7).Value = item.v6;
                i++;
            }

            return wb;
        }

        public static XLWorkbook GetTpriceSearchExcel(ExcelResult result, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Выборка");

            int i_count = result.GetFieldCount();

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            for (int i = 0; i < i_count; i++)
            {
                ws.Column(i + 1).Width = i==0 ? 100:50;
                ws.Cell(1, i + 1).Value = result.GetHeaderName(i);
            }

            if (!only_header)
            {
                for (int i = 0, i_max = result.GetRowCount(); i < i_max; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    for (int j = 0; j < i_count; j++)
                    {
                        ws.Cell(i + 2, j + 1).Value = result.GetVal(j, i);
                    }
                }
            }

            return wb;
            
        }
    }
}