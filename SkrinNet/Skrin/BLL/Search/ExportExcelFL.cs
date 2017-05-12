using ClosedXML.Excel;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Search
{
    public class ExportExcelFL
    {
        public XLWorkbook ExportWorkbook(List<FLInfo> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "ФИО", "ИНН" };
            List<int> aWidth = new List<int> { 50, 20 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            if (!only_header)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    ws.Cell(i + 2, 1).Value = list[i].fio;
                    var cell1 = ws.Cell(i + 2, 2);
                    string inn = (string)list[i].inn;
                    cell1.Style.NumberFormat.Format = "#";
                    cell1.Value = inn;
                }
            }


            return wb;
        }

        public void SaveStreamToFile(string filename, Stream stream)
        {
            if (stream.Length != 0)
                using (FileStream fileStream = File.Create(filename, (int)stream.Length))
                {
                    // Размещает массив общим размером равным размеру потока
                    // Могут быть трудности с выделением памяти для больших объемов
                    byte[] data = new byte[stream.Length];

                    stream.Read(data, 0, (int)data.Length);
                    fileStream.Write(data, 0, data.Length);
                }
        }
    }
}