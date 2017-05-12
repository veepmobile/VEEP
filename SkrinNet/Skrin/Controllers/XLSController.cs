using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FastReport;
using FastReport.Web;
using FastReport.Utils;
using FastReport.Export.Pdf;
using FastReport.Export.RichText;
using FastReport.Export.OoXML;


namespace Skrin.Controllers
{
    public class XLSController : Controller
    {
        // GET: XLS
        public ActionResult MSFO(String iss, String per)
        {
            Report report = new Report();
            report.Report.Load(this.Server.MapPath("~/App_Data/fr_tmpl/msfo.frx"));
            report.SetParameterValue("@iss", iss);
            report.SetParameterValue("@per", per);
            report.Report.Prepare();
            Stream stream = new MemoryStream();
            report.Report.Export(new Excel2007Export(), stream);
            stream.Position = 0;
            return File(stream, "application/xlsx", iss + "_msfo.xlsx");


            
        }
    }
}