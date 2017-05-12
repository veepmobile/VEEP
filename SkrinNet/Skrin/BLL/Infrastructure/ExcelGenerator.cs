using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using System.Globalization;


namespace Skrin.BLL.Infrastructure
{
    public class ExcelResult
    {
        private Dictionary<int, ExcelColumn> _columns;

        public ExcelResult()
        {
            _columns = new Dictionary<int, ExcelColumn>();
        }

        public string GetFieldName(int i)
        {
            return _columns[i].Header.field_name;
        }

        public int GetFieldCount()
        {
            return _columns.Count;
        }

        public int GetRowCount()
        {
            return _columns[0].Count;
        }

        public string GetHeaderName(int i)
        {
            return _columns[i].Header.header_name;
        }

        public int? GetColWidth(int i)
        {
            return _columns[i].Header.width;
        }

        public object GetVal(int i_col, int i_row)
        {
            var column = _columns[i_col];
            string val = column.GetVal(i_row);
            if ((column.Header.field_type == ExcelFieldType.Number) && (!String.IsNullOrWhiteSpace(val)))
            {
                return Decimal.Parse(val.Replace(',','.'), CultureInfo.InvariantCulture);
            }
            return "'" + val;
        }



        public void AddHeader(int i, string header_name, string field_name, string f_type, int? f_width = null)
        {
            ExcelFieldType ft = f_type == "n" ? ExcelFieldType.Number : ExcelFieldType.String;

            var column = new ExcelColumn(new ExcelHeader
            {
                 field_type = ft,
                 field_name = field_name,
                 header_name = header_name,
                 width = f_width
            });
            _columns.Add(i, column);
        }

        public void AddValue(int i, string val)
        {
            _columns[i].AddVal(val);
        }
    }


    public class ExcelColumn
    {
        public ExcelHeader Header { get; set; }

        private List<string> _items;

        public ExcelColumn(ExcelHeader header)
        {
            _items = new List<string>();
            Header = header;
        }

        public void AddVal(string val)
        {
            _items.Add(val);
        }

        public string GetVal(int i)
        {
            return _items[i];
        }

        public int Count
        {
            get { return _items.Count; }
        }

    }

    public class ExcelHeader
    {
        public string header_name { get; set; }
        public string field_name { get; set; }
        public ExcelFieldType field_type { get; set; }
        public int? width { get; set; }
    }


    public class ExcelVal
    {
        public string StringVal { get; set; }
        public decimal? NumberVal { get; set; }
    }

    public enum ExcelFieldType
    {
        String, Number 
    }

    public class ExcelGenerator
    {
        public static XLWorkbook SimpleExcel(ExcelResult result, string worksheet_name="Лист", bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add(worksheet_name);

            int i_count = result.GetFieldCount();

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            for (int i = 0; i < i_count; i++)
            {
                ws.Column(i + 1).Width = (result.GetColWidth(i) == null) ? (i == 0 ? 100 : 50) : ((double)result.GetColWidth(i));
                ws.Cell(1, i + 1).Value = result.GetHeaderName(i);
                ws.Cell(1, i + 1).Style.Alignment.WrapText = true;
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
                        if (ws.Cell(i + 2, j + 1).DataType == XLCellValues.Number)
                        {
                            ws.Cell(i + 2, j + 1).Style.NumberFormat.SetFormat("#,##0.00##################################################");
                            ws.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                    }
                }
            }

            return wb;

        }

    }
}