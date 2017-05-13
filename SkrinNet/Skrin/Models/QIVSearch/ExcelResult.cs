using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Skrin.Models.QIVSearch
{
    /*
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

        public object GetVal(int i_col, int i_row)
        {
            var column = _columns[i_col];
            string val = column.GetVal(i_row);
            if ((column.Header.field_type == ExcelFieldType.Number) && (!String.IsNullOrWhiteSpace(val)))
            {
                return Decimal.Parse(val, CultureInfo.InvariantCulture);
            }
            return "'" + val;
        }



        public void AddHeader(int i, string header_name, string field_name, string f_type)
        {
            ExcelFieldType ft = f_type == "n" ? ExcelFieldType.Number : ExcelFieldType.String;

            var column = new ExcelColumn(new ExcelHeader
            {
                 field_type=ft,
                 field_name=field_name,
                 header_name=header_name
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
     * */
}