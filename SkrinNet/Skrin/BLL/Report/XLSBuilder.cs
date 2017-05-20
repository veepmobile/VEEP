using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Skrin.BLL.Report
{
    public class XLSBuilder
    {
       
        private string _json_string;
        string[] x = { "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH","AI","AJ","AK" };

        public XLSBuilder(string json_string)
        {

            _json_string = json_string;
        }
        public MemoryStream MakeXLS(){
            JObject json = JObject.Parse(_json_string);
        
            var wb = new XLWorkbook();
            IXLWorksheet xlsSheet;
            IXLRow xlsRow;
            IXLRange xlsRange;
            IXLStyle xlsStyle;
            IXLCell xlsCell;
            int i = 0, j = 0, cols = 0;
            float v=0;

                foreach(var ws in json["WorkBook"]){
                    xlsSheet = wb.Worksheets.Add(ws["WorkSheet"].ToString());
                    j = 1;
                    cols = 0;
                    foreach (var col in ws["Cols"])
                    {
                        cols++;
                        xlsSheet.Column(cols).Width = double.Parse(col.ToString())/5;
                   
                    }
                    
                    xlsRow = xlsSheet.Row(1);
                    xlsRow.Height = 50;
                    xlsRange = xlsSheet.Range(getRangeCoord(1, 1, 1, cols));
                    xlsRange.Row(1).Merge();
                    xlsStyle = SetStyle(xlsSheet.Cell("A1").Style, ws["Style"].ToString());
                    xlsRange.Style = xlsStyle;
                    xlsRange.Value = ws["IssName"] + "\n" + ws["Header"];
                    int k=0;
                     foreach(var row in  ws["SheetData"]){
                         int lr=0;
                         int lc=0;
                         k++;
                         int z=0;
                         foreach(var col in row["Row"] ){
                            z++;
                    
                            if (int.Parse(col["colspan"].ToString()) > 1 || (int.Parse(col["rowspan"].ToString()) > 1))
                            {
                                if (col.ToString().IndexOf("Index") >= 0)
                                {
                                    lc = int.Parse(col["Index"].ToString());
                                }
                                xlsRange = xlsSheet.Range(getRangeCoord(k + 2 + lr, 1 + lc, k + 2 + lr + int.Parse(col["rowspan"].ToString()) - 1, 1 + lc + int.Parse(col["colspan"].ToString()) - 1)).Merge();
                                xlsRange.Value = col["Data"].ToString();
                                xlsRange.Style = SetStyle(xlsSheet.Cell(x[z] + (k + 2).ToString()).Style, col["Style"].ToString());
                                lc = lc + int.Parse(col["colspan"].ToString());
                            }
                            else
                            {
                                if (col.ToString().IndexOf("Index") >= 0)
                                {
                                    lc += int.Parse(col["Index"].ToString()) + 1;

                                }
                                else
                                {
                                    lc++;
                                }
                                xlsCell = xlsSheet.Cell(x[lc] + (k + 2).ToString());
                                if (col["Data"].ToString() == "-")
                                {
                                    i = col["Style"].ToString().IndexOf("right");
                                    i = col["Data"].ToString().Length;
                                }
                                if (col["Style"].ToString().IndexOf("right") > 0 && !(float.TryParse(col["Data"].ToString(), out v)) && col["Data"].ToString().Length > 0 && col["Data"].ToString() != "-")
                                {
                                    xlsCell.Value = float.Parse(col["Data"].ToString(), new System.Globalization.CultureInfo("en-US"));
                                }
                                else
                                {
                                    if (col["Type"].ToString() == "D")
                                    {
                                        xlsCell.Style.DateFormat.Format = "dd.mm.yyyy";
                                    }
                                    xlsCell.Value = col["Data"].ToString();
                                    
                                }
                                xlsCell.Style = SetStyle(xlsCell.Style, col["Style"].ToString());
                                
                            }

                            xlsRow = xlsSheet.Row(k);
                          
                        }
                      
                    }


                     

                }
               
            MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
         private IXLStyle SetStyle(IXLStyle Obj, string style)
        {
            String json_style = @"{IssName         : {HAlign : 2, VAlign : 0, FontBold : 1, FontHeight : 12, FontColorIndex : 23, Wrap:true}," +
            "Header          : {HAlign : 2, VAlign : 0, FontBold : 1, FontHeight : 16, FontColorIndex :  0, Wrap:true}," +
            "subcaption      : {HAlign : 2, VAlign : 0, FontBold : 1, FontHeight : 10, FontColorIndex : 32, FillPatternBGColorIndex : 15, FromHtml : \"#FFccdaff\", Wrap:true}," +
            "razdel          : {HAlign : 2, VAlign : 0, FontBold : 1, FontHeight : 10, FontColorIndex :  0, Wrap:true}," +
            "TabHeader       : {HAlign : 2, VAlign : 0, FontBold : 1, FontHeight : 10, FontColorIndex :  0, FillPatternBGColorIndex : 48, FromHtml : \"#FFDDDDDD\" ,BorderStyle:1, Wrap:true}," +
            "bold            : {HAlign : 1, VAlign : 0, FontBold : 1, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:true}," +
            "odd             : {HAlign : 1, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, FillPatternBGColorIndex : 15, FromHtml : \"#FFF0F0F0\",BorderStyle:1, Wrap:true,FormatStringIndex:\"F4\"}," +
            "even            : {HAlign : 1, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:true,FormatStringIndex:\"F4\"}," +
            "even_date       : {HAlign : 2, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:true,FormatStringIndex:\"FD\"}," +
            "odd_right       : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, FillPatternBGColorIndex : 15, FromHtml : \"#FFF0F0F0\",BorderStyle:1, Wrap:false,FormatStringIndex:\"F2\"}," +
            "even_right      : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:false, FormatStringIndex:\"F2\"}," +
            "even_right8     : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:false, FormatStringIndex:\"F8\"}," +
            "odd_right_int   : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, FillPatternBGColorIndex : 15, FromHtml : \"#FFF0F0F0\",BorderStyle:1, Wrap:false,FormatStringIndex:\"F3\"}," +
            "even_right_int  : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:false, FormatStringIndex:\"F3\"}," +
            "odd_right_0     : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, FillPatternBGColorIndex : 15, FromHtml : \"#FFF0F0F0\",BorderStyle:1, Wrap:false,FormatStringIndex:\"F5\"}," +
            "even_right_0    : {HAlign : 3, VAlign : 1, FontBold : 0, FontHeight : 10, FontColorIndex :  0, BorderStyle:1, Wrap:false, FormatStringIndex:\"F5\"}}";
            JObject Styles = JObject.Parse(json_style);
    var s=Styles[style];
            string Nformat="";
           

  
    XLAlignmentHorizontalValues HA;
            switch(s["HAlign"].ToString()){
               
                case "1": HA=XLAlignmentHorizontalValues.Left;
                    break;
                case "2": HA = XLAlignmentHorizontalValues.Center;
                    break;
                case "3": HA=XLAlignmentHorizontalValues.Right;
                    break;
                default : HA=XLAlignmentHorizontalValues.Left;
                    break;
            }
    XLAlignmentVerticalValues VA;
    switch (s["VAlign"].ToString())
    {
        case "0":  VA = XLAlignmentVerticalValues.Center;
            break;
        case "1": VA = XLAlignmentVerticalValues.Top;
            break;
        default: VA = XLAlignmentVerticalValues.Center;
            break;
    }

    Obj.Alignment.Horizontal = HA;
    Obj.Alignment.Vertical = VA;
    Obj.Font.Bold = (s["FontBold"].ToString() == "1");
    Obj.Font.FontSize = double.Parse(s["FontHeight"].ToString());
    Obj.Font.FontColor = XLColor.FromIndex(int.Parse(s["FontColorIndex"].ToString()));
    if (s.ToString().IndexOf("FromHtml")>=0)
    {
        Obj.Fill.BackgroundColor = XLColor.FromHtml(s["FromHtml"].ToString());
    }
 
    if (s.ToString().IndexOf("BorderStyle")>=0)
    {
        Obj.Border.OutsideBorder = XLBorderStyleValues.Thin;
    }
    
    Obj.Alignment.WrapText = (s["Wrap"].ToString().ToLower() == "true") ? true : false;
    if (s.ToString().IndexOf("FormatStringIndex")>=0){
        switch (s["FormatStringIndex"].ToString())
        {
            case "F2": Obj.NumberFormat.Format = "_(#,##0.00_);[Red](#,##0.00)";
                break;
            case "F3": Obj.NumberFormat.Format = "_(#,##0_);[Red](#,##0)";
                break;
            case "F4":
                {
                    //Obj.NumberFormat.Format = "General";
                    break;
                }
            case "F5": Obj.NumberFormat.Format = "#,##0.#####";
                break;
            case "F8": Obj.NumberFormat.Format = "#,##0.#####";
                break;
            case "FD": Obj.DateFormat.Format = "dd.mm.yyyy";
                break;

        } 
        
        
    }
    
    return Obj;
        }

        private string getRangeCoord(int p1, int p2, int p3, int p4)
        {
            string retval = x[p2] + p1.ToString() + ":" + x[p4] + p3.ToString();
            return retval;
        }
    }
}