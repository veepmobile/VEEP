using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class HorisonrtalTable
    {
        public HorisonrtalTable()
        {
            Format = "<div class=\"subcaption\">{0}</div>";
        }
        public Dictionary<string, ColumnData> Columns { get; set; }
        public string TableAttributes { get; set; }
        public string Header { get; set; }
        public string Format { get; set; }
        public string FormatedHeader
        {
            get { return string.Format(Format, Header); }
        }

        public int RowCount
        {
            get
            {
                if (Columns.Count == 0)
                    return 0;
                return Columns.First().Value.ColumnValues.Count;
            }
        }

        public int HeadRowCnt
        {
            get
            {
                foreach (var cl in Columns)
                {
                    if (cl.Value.Column_Colspan > 1)
                    {
                        return 2;
                    }
                }
                return 1;
            }
        }
    }

    public class ColumnData
    {
        public ColumnData()
        {
            HeaderFormat = "{0}";
            ValueFormat = "{0}";
            ColumnValues = new List<ColumnValue>();
        }

        public ColumnData(string h, string hf, string vf)
        {
            Header = h;
            HeaderFormat = (string.IsNullOrWhiteSpace(hf) ? "{0}" : hf);
            ValueFormat = (string.IsNullOrWhiteSpace(vf) ? "{0}" : vf);
            ColumnValues = new List<ColumnValue>();
        }

        public ColumnData(string h, string hf, string vf, int colspan, string gh, string gf)
        {
            Header = h;
            HeaderFormat = (string.IsNullOrWhiteSpace(hf) ? "{0}" : hf);
            ValueFormat = (string.IsNullOrWhiteSpace(vf) ? "{0}" : vf);

            Column_Colspan = colspan;
            Column_Grp_Header = gh;
            Column_Grp_Header_Format = (string.IsNullOrWhiteSpace(gf) ? "{0}" : gf); ;

            ColumnValues = new List<ColumnValue>();
        }
        public ColumnData(string h, string hf, string vf, int colspan, string gh, string gf, string ca)
        {
            Header = h;
            HeaderFormat = (string.IsNullOrWhiteSpace(hf) ? "{0}" : hf);
            ValueFormat = (string.IsNullOrWhiteSpace(vf) ? "{0}" : vf);
            Column_Colspan = colspan;
            Column_Grp_Header = gh;
            Column_Grp_Header_Format = (string.IsNullOrWhiteSpace(gf) ? "{0}" : gf); ;
            ColumnAttributes = ca;
            ColumnValues = new List<ColumnValue>();

        }
        public ColumnData(string h, string ha, string hf, string va, string vf, int colspan, string gh, string ga, string gf)
        {
            Header = h;
            HeaderAttributes = ha;
            HeaderFormat = (string.IsNullOrWhiteSpace(hf) ? "{0}" : hf);
            ColumnAttributes = va;
            ValueFormat = (string.IsNullOrWhiteSpace(vf) ? "{0}" : vf);

            Column_Colspan = colspan;
            Column_Grp_Header = gh;
            Column_Grp_Header_Attributes = ga;
            Column_Grp_Header_Format = (string.IsNullOrWhiteSpace(gf) ? "{0}" : gf);

            ColumnValues = new List<ColumnValue>();
        }

        public string Header { get; set; }
        public string HeaderFormat { get; set; }
        public string HeaderAttributes { get; set; }
        public string ColumnAttributes { get; set; }
        public string ValueFormat { get; set; }

        public int Column_Colspan { get; set; }
        public string Column_Grp_Header { get; set; }
        public string Column_Grp_Header_Attributes { get; set; }
        public string Column_Grp_Header_Format { get; set; }

        public List<ColumnValue> ColumnValues { get; set; }

        public string FormatedHeader
        {
            get { return string.Format(HeaderFormat, Header); }
        }

        public string FormatedGroupHeader
        {
            get { return string.Format(Column_Grp_Header_Format, Column_Grp_Header); }
        }
    }

    public class ColumnValue
    {
        public ColumnValue()
        {
            Format = "{0}";
        }

        public ColumnValue(object v, string f)
        {
            Value = v;
            Format = (string.IsNullOrWhiteSpace(f) ? "{0}" : f);
        }

        public object Value { get; set; }
        public string Format { get; set; }

        public string FormatedValue
        {
            get
            {
                if (Value == null)
                {
                    return "";
                }
                else
                {
                    var _cu = CultureInfo.CreateSpecificCulture("ru-RU");
                    return string.Format(_cu, Format, Value);
                }
            }
        }
    }
}