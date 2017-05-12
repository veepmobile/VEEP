using ClosedXML.Excel;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Search
{
    public class ExportExcelIchp
    {
        public XLWorkbook ExportWorkbook(List<FLDetails> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "ФИО", "Полное наименование", "Регион", "ИНН", "ОГРНИП", "ОКПО", "Состояние" };
            List<int> aWidth = new List<int> { 50, 30, 30, 20, 20, 20, 50 };

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
                    ws.Cell(i + 2, 2).Value = list[i].typeip;
                    ws.Cell(i + 2, 3).Value = list[i].region;
                    //ws.Cell(i + 2, 4).Value = list[i].inn.ToString();
                    var cell1 = ws.Cell(i + 2, 4);
                    string inn = (string)list[i].inn;
                    cell1.Style.NumberFormat.Format = "#";
                    cell1.Value = inn;
                    var cell2 = ws.Cell(i + 2, 5);
                    string ogrnip = (string)list[i].ogrnip;
                    cell2.Style.NumberFormat.Format = "#";
                    cell2.Value = ogrnip;
                    ws.Cell(i + 2, 6).Value = list[i].okpo.ToString();
                    ws.Cell(i + 2, 7).Value = list[i].stoping;
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