using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Skrin.BLL;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Authorization;
using FastReport;
using FastReport.Export;
using FastReport.Export.Pdf;
using FastReport.Export.RichText;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.Controllers.Iss
{
    public class ProfileLinksController : Controller
    {
        public ActionResult Export(string iss, string url, int format = 1)
        {
            iss = iss.ToUpper();
            int source = 3;

            UserSession us = HttpContext.GetUserSession();

            if (us.UserId > 0)
            {
                try
                {   
                    Report report = new Report();
                    ExportBase export;
                    switch (format)
                    {
                        case 2:
                            export = new RTFExport();
                            break;
                        default:
                            export = new PDFExport();
                            break;
                    }

                    report.Load(System.Web.HttpContext.Current.Server.MapPath("/Templates//ProfileLinks.frx"));

                    DataSet dt = new DataSet();
                    FastReport.Data.DataSourceBase ds;

                    PasteNeoData(dt, report, "founder_to", new string[] { "ogrn", "inn", "name", "share", "share_percent", "ogrn_to", "name_to", "status_to", "ticker_to", "gd", "position" }, new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 2 }, new string[] { "ogrn", "inn", "name", "share", "share_percent", "ogrn", "name", "status", "ticker", "gd", "position" }, "ProfileLinks_Founder_To", iss);
                    PasteNeoData(dt, report, "manager_to", new string[] { "ogrn", "inn", "name", "address", "ogrn_to", "name_to", "status_to", "ticker_to", "gd", "share" }, new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 2 }, new string[] { "ogrn", "inn", "name", "address", "ogrn", "name", "status", "ticker", "gd", "share" }, "ProfileLinks_Manager_To", iss);
                    PasteNeoData(dt, report, "founder_fromfl", new string[] { "fio", "inn", "share", "share_percent", "gd" }, new int[] { 0, 1, 1, 1, 1 }, new string[] { "fio", "inn", "share", "share_percent", "gd" }, "ProfileLinks_Founder_FromFL", iss);
                    PasteNeoData(dt, report, "founder_fromul", new string[] { "ogrn", "inn", "name", "share", "share_percent", "ticker", "status", "gd" }, new int[] { 0, 0, 0, 1, 1, 0, 0, 1 }, new string[] { "ogrn", "inn", "name", "share", "share_percent", "ticker", "status", "gd" }, "ProfileLinks_Founder_FromUL", iss);
                    PasteNeoData(dt, report, "founder_from_tofl", new string[] { "fio_fn", "inn_fn", "fio", "inn", "share", "share_percent", "name", "ogrn", "status", "ticker", "position", "gd" }, new int[] { 0, 1, 0, 3, 3, 3, 2, 2, 2, 2, 4, 3 }, new string[] { "fio", "inn", "fio", "inn", "share", "share_percent", "name", "ogrn", "status", "ticker", "position", "gd" }, "ProfileLinks_Founder_From_ToFL", iss);
                    PasteNeoData(dt, report, "founder_from_toul", new string[] { "ogrn_fn", "inn_fn", "name_fn", "ogrn", "inn", "name", "share", "share_percent", "name_to", "ogrn_to", "status_to", "ticker_to", "position_to", "gd" }, new int[] { 0, 0, 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 4, 3 }, new string[] { "ogrn", "inn", "name", "ogrn", "inn", "name", "share", "share_percent", "name", "ogrn", "status", "ticker", "position", "gd" }, "ProfileLinks_Founder_From_ToUL", iss);
                    PasteNeoData(dt, report, "founder_from_manager_to", new string[] { "fio_fn", "inn_fn", "fio", "inn", "position", "name", "ogrn", "status", "ticker", "share", "gd" }, new int[] { 0, 0, 0, 3, 3, 2, 2, 2, 2, 4, 3 }, new string[] { "fio", "inn", "fio", "inn", "position", "name", "ogrn", "status", "ticker", "share", "gd" }, "ProfileLinks_Founder_From_Manager_To", iss);
                    PasteNeoData(dt, report, "successor_from", new string[] { "ogrn", "inn", "name", "ticker", "status", "gd" }, new int[] { 0, 0, 0, 0, 0, 1 }, new string[] { "ogrn", "inn", "name", "ticker", "status", "gd" }, "ProfileLinks_Successor_From", iss);
                    PasteNeoData(dt, report, "successor_to", new string[] { "ogrn", "inn", "name", "ticker", "status", "gd" }, new int[] { 0, 0, 0, 0, 0, 1 }, new string[] { "ogrn", "inn", "name", "ticker", "status", "gd" }, "ProfileLinks_Successor_To", iss);
                    PasteNeoData(dt, report, "manager_from", new string[] { "fio", "inn", "position", "gd" }, new int[] { 0, 1, 1, 1 }, new string[] { "fio", "inn", "position", "gd" }, "ProfileLinks_Manager_From", iss);
                    PasteNeoData(dt, report, "manager_from_founder_to", new string[] { "fio_mn", "inn_mn", "position_mn", "fio", "inn", "share", "share_percent", "name", "ogrn", "status", "ticker", "position", "gd" }, new int[] { 0, 1, 1, 0, 3, 3, 3, 2, 2, 2, 2, 4, 3 }, new string[] { "fio", "inn", "position", "fio", "inn", "share", "share_percent", "name", "ogrn", "status", "ticker", "position", "gd" }, "ProfileLinks_Manager_From_Founder_To", iss);
                    PasteNeoData(dt, report, "manager_from_to", new string[] { "fio_mn", "inn_mn", "position_mn", "fio", "inn", "position", "name", "ogrn", "status", "ticker", "share", "gd" }, new int[] { 0, 1, 1, 0, 3, 3, 2, 2, 2, 2, 4, 3 }, new string[] { "fio", "inn", "position", "fio", "inn", "position", "name", "ogrn", "status", "ticker", "share", "gd" }, "ProfileLinks_Manager_From_To", iss);

                    for (int i = 0; i < dt.Tables.Count; i++)
                    {
                        DataTable table = dt.Tables[i];
                        report.RegisterData(table, table.TableName);
                        ds = report.GetDataSource(table.TableName);
                        ds.Enabled = true;
                        ((DataBand)report.FindObject("Data_" + table.TableName)).DataSource = ds;
                    }

                    ds = report.GetDataSource("founder_fromfl");
                    ((DataBand)report.FindObject("Data_founder_fromfl2")).DataSource = ds;
                    ((DataBand)report.FindObject("Data_founder_fromfl3")).DataSource = ds;
                    ds = report.GetDataSource("founder_fromul");
                    ((DataBand)report.FindObject("Data_founder_fromul2")).DataSource = ds;
                    ds = report.GetDataSource("manager_from");
                    ((DataBand)report.FindObject("Data_manager_from2")).DataSource = ds;
                    ((DataBand)report.FindObject("Data_manager_from3")).DataSource = ds;
                    ((DataBand)report.FindObject("Data_manager_from4")).DataSource = ds;

                    report.SetParameterValue("@iss", iss);
                    report.SetParameterValue("@url", url);
                    report.SetParameterValue("@src", source);

                    report.Report.Prepare();

                    Stream stream = new MemoryStream();
                    report.Report.Export(export, stream);
                    report.Dispose();

                    stream.Position = 0;
                    switch (format)
                    {
                        case 2:
                            return File(stream, "application/rtf", iss + "_" + "ProfileLinks.rtf");
                        default:
                            return File(stream, "application/pdf", iss + "_" + "ProfileLinks.pdf");
                    }
                }
                catch (Exception ex)
                {
                    Helper.SendEmail(ex.ToString(), "error generate");
                    //System.IO.File.AppendAllText("c:\\net\\error.log", ex.ToString() + Environment.NewLine + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
                    return new HttpNotFoundResult("Ошибка генерации отчета");
                }
            }
            else
            {
                return new HttpStatusCodeResult(403);
            }

        }
        private void PasteNeoData(DataSet dt, Report report, string table_name, string[] fields, int[] item_n, string[] item_fields, string query_name, string iss)
        {
            Neo4jClient nc = new Neo4jClient();
            DataTable table;
            if (dt.Tables.IndexOf(table_name) < 0)
            {
                table = dt.Tables.Add(table_name);
                for (int i = 0; i < fields.Length; i++)
                {
                    table.Columns.Add(fields[i], System.Type.GetType("System.String"));
                }
            }
            else
                table = dt.Tables[table_name];

            JObject r = nc.GetBaseQuery(query_name, new string[] { "id" }, new string[] { "\"" + iss + "\"" });
            if (r == null)
            {
                return;
            }

            int n = r["results"][0]["data"].Count();
            for (int i = 0; i < n; i++)
            {
                JObject item_mn = (JObject)r["results"][0]["data"][i]["row"][0];
                DataRow row = table.NewRow();
                for (int j = 0; j < fields.Length; j++)
                {
                    if (r["results"][0]["data"][i]["row"][item_n[j]].Type != JTokenType.Null)
                        row[fields[j]] = (string)r["results"][0]["data"][i]["row"][item_n[j]][item_fields[j]];
                    else
                        row[fields[j]] = "";
                }
                table.Rows.Add(row);
            }
        }
    }
}