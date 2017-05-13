using FastReport;
using FastReport.Export.Pdf;
using FreeEgrulHandlerLib.BLL;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.BLL.Root;
using Skrin.Models.Iss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class ReportController : BaseController
    {

        private enum Key { CanShowEgrul };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static ReportController()
        {
            roles.Add(Key.CanShowEgrul.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }

        [Route("Report/Egrul/{ogrn}")]
        public ActionResult Egrul(string ogrn)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.CanShowEgrul.ToString()))
            {
                EgrulGenerator eg = new EgrulGenerator(ogrn);
                string file_text = eg.Generate();
                var bytes = Encoding.GetEncoding("Windows-1251").GetBytes(file_text);
                return File(bytes, "application/rtf", ogrn + "_egrul.rtf");
            }
            return new HttpStatusCodeResult(403);
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> AskEgrul(string inn, string ogrn)
        {
            return await _AskEgrul(inn, ogrn);
        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> AskEgrulByTicker(string ticker)
        {
            var ci = await SqlUtiltes.GetOgrnInnAsync(ticker);
            if (ci == null)
            {
                return Json(new { status = 2, ogrn = "" }, JsonRequestBehavior.AllowGet);
            }
            return await _AskEgrul(ci.Item2, ci.Item1);
        }

        private async Task<ActionResult> _AskEgrul(string inn, string ogrn)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return Json(new { status = 5, ogrn = ogrn }, JsonRequestBehavior.AllowGet);
            }
            string reg_code = !string.IsNullOrWhiteSpace(ogrn) ? ogrn.Trim() : inn;
            if (string.IsNullOrWhiteSpace(reg_code))
            {
                return Json(new { status = 2, ogrn = "" }, JsonRequestBehavior.AllowGet);
            }
            int rest = await AuthenticateSqlUtilites.GetPaidEgrulRestAsync(us.UserId, reg_code.Trim());
            if (rest > 0)
            {
                var status = await GetEgrulStatusAsync(inn, ogrn);
                return Json(new { status = status.Item1, ogrn = status.Item2 },JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 5, ogrn = ogrn }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AskEgrulPdf(string ogrn)
        {
            UserSession us = HttpContext.GetUserSession();
            UserReportLimit limit = await AuthenticateSqlUtilites.CheckReportLimit(us.UserId, ReportLimitType.Egrul,ogrn);
            if (limit.CanReport)
            {
                await AuthenticateSqlUtilites.SetUserReportLog(us.UserId, ReportLimitType.Egrul, ogrn);
                var status = await GetErulPdfStatusAsync(ogrn, ReportLimitType.Egrul);
                int ret = status.Item1;
                if (ret == 2) //Это еще ничего не значит. Надо проверить: есть ли сам файл
                {
                    string path = Server.MapPath(string.Format("/EgrulDoc/ul/{1}/{0}_{1}.pdf", status.Item2, ogrn));
                    if(!System.IO.File.Exists(path))
                    {
                        ret = 2; //перед выгрузкой на боевой - поменять на "status = 0;"
                    }
                }
                return Json(new {status=ret,dt=status.Item2},JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(403);
        }


        [Route("Report/Egrip/{ogrnip}")]
        public ActionResult Egrip(string ogrnip)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.CanShowEgrul.ToString()))
            {
                EgripGenerator eg = new EgripGenerator(ogrnip);
                string file_text = eg.Generate();
                var bytes = Encoding.GetEncoding("Windows-1251").GetBytes(file_text);
                return File(bytes, "application/rtf", ogrnip + "_egrip.rtf");
            }
            return new HttpStatusCodeResult(403);
        }

        public async Task<ActionResult> AskEgrip(string inn, string ogrnip)
        {
            var status = await GetEgripStatusAsync(inn, ogrnip);
            return Json(new { status = status.Item1, ogrnip = status.Item2 });
        }

        public async Task<ActionResult> AskEgripPdf(string ogrnip)
        {
            UserSession us = HttpContext.GetUserSession();
            UserReportLimit limit = await AuthenticateSqlUtilites.CheckReportLimit(us.UserId, ReportLimitType.Egrul, ogrnip);
            if (limit.CanReport)
            {
                await AuthenticateSqlUtilites.SetUserReportLog(us.UserId, ReportLimitType.Egrip, ogrnip);
                var status = await GetErulPdfStatusAsync(ogrnip, ReportLimitType.Egrip);
                int ret = status.Item1;
                if (ret == 2) //Это еще ничего не значит. Надо проверить: есть ли сам файл
                {
                    string path = Server.MapPath(string.Format("/EgrulDoc/ul/{1}/{0}_{1}.pdf", status.Item2, ogrnip));
                    if (!System.IO.File.Exists(path))
                    {
                        ret = 2; //перед выгрузкой на боевой - поменять на "status = 0;"
                    }
                }
                return Json(new { status = ret, dt = status.Item2 });
            }
            return new HttpStatusCodeResult(403);
        }

        [Route("Report/EgrulPdf/{p1}/{p2}")]
        public ActionResult EgrulPdf(string p1, string p2)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.CanShowEgrul.ToString()))
            {
//                string path = Server.MapPath(string.Format("/EgrulDoc/ul/{1}/{0}_{1}.pdf", p1, p2));
                string path = Configs.EgrulPdfDocPath + string.Format(@"\ul\{1}\{0}_{1}.pdf", p1, p2);
                return File(path, "application/pdf", string.Format("{0}_{1}.pdf", p1, p2));
            }
            return new HttpStatusCodeResult(403);
        }

        [Route("Report/EgripPdf/{p1}/{p2}")]
        public ActionResult EgrilPdf(string p1, string p2)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.CanShowEgrul.ToString()))
            {
                //                string path = Server.MapPath(string.Format("/EgrulDoc/ul/{1}/{0}_{1}.pdf", p1, p2));
                string path = Configs.EgripPdfDocPath + string.Format(@"\ip\{1}\{0}_{1}.pdf", p1, p2);
                return File(path, "application/pdf");
            }
            return new HttpStatusCodeResult(403);
        }

        private async Task<Tuple<byte,string>> GetErulPdfStatusAsync(string regcode, ReportLimitType report_type)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns_free..[get_egruldoc_queue_status]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reg_code", SqlDbType.VarChar, 15).Value = regcode;
                cmd.Parameters.Add("@d_type", SqlDbType.TinyInt).Value = (int)report_type;
                con.Open();
                using(SqlDataReader reader=await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if(await reader.ReadAsync())
                    {
                        return new Tuple<byte, string>((byte)reader["st"], (string)reader["dt"]);
                    }
                    throw new Exception("status not found");
                }
            }
        }

        private async Task<Tuple<int, string>> GetEgrulStatusAsync(string inn, string ogrn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.[getQueueStatusAll]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 13).Value = inn;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        return new Tuple<int, string>((reader[0] != DBNull.Value ? (int)reader[0] : 1), (reader[1] != DBNull.Value ? (string)reader[1] : ""));
                    }
                    throw new Exception("status not found");
                }
            }
        }

        private async Task<Tuple<int, string>> GetEgripStatusAsync(string inn, string ogrnip)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.getQueueStatusEgrip", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = ogrnip;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 12).Value = inn;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        return new Tuple<int, string>((reader[0] != DBNull.Value ? (int)reader[0] : 1), (reader[1] != DBNull.Value ? (string)reader[1] : ""));
                    }
                    throw new Exception("status not found");
                }
            }
        }


        private MemoryStream GetEgrulStream(string ogrn, DateTime extract_date)
        {
            IEgrulRepository e_rep = new EgrulRepository(ogrn, extract_date, Configs.ConnectionString);
            var egrul = new UL2_Creator(e_rep).Create();
            var egrul_report = new EgrulReportCreator(egrul).Create();

            using (Report report = new Report())
            {
                PDFExport export = new PDFExport();
                report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Templates/egrul.frx"));
                report.RegisterData(egrul_report.CoreList, "CoreInfo");
                report.RegisterData(egrul_report.Rows, "EgrulRows");

                report.Report.Prepare();

                var stream = new MemoryStream();
                report.Report.Export(export, stream);
                stream.Position = 0;
                return stream;
            }
        }


        [Route("Report/GetEgrul/{ogrn}/")]
        public async Task<ActionResult> GetEgrul(string ogrn)
        {
            if (!string.IsNullOrWhiteSpace(ogrn))
            {
                DateTime? extract_date = await SqlUtiltes.GetLastEgrulExtractDateAsync(ogrn);
                if(extract_date!=null)
                {
                    return File(GetEgrulStream(ogrn,extract_date.Value), "application/pdf", ogrn + ".pdf");
                }
            }
            return HttpNotFound();
        }


        [Route("Report/SaveEgrul/{ogrn}/")]
        public async Task<ActionResult> SaveEgrul(string ogrn)
        {
            if (!string.IsNullOrWhiteSpace(ogrn))
            {
                DateTime? extract_date = await SqlUtiltes.GetLastEgrulExtractDateAsync(ogrn);
                if (extract_date != null)
                {
                    string save_path = Configs.EgrulPdfDocPath + string.Format(@"\ul\{0}\", ogrn);
                    if (!Directory.Exists(save_path))
                    {
                        Directory.CreateDirectory(save_path);
                    }
                    var file_path = Path.Combine(save_path, string.Format("{0}_{1}.pdf", DateTime.Now.ToString("yyyyMMdd"), ogrn));
                    var egrul_stream = GetEgrulStream(ogrn, extract_date.Value);
                    using (var fs=new FileStream(file_path,FileMode.Create))
                    {
                        egrul_stream.CopyTo(fs);
                    }
                    await SqlUtiltes.SaveEgrulReport(ogrn);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

               
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [Route("Report/GetEgrip/{ogrn}/")]
        public async Task<ActionResult> GetEgrip(string ogrn)
        {
            if (!string.IsNullOrWhiteSpace(ogrn))
            {
                DateTime? extract_date = await SqlUtiltes.GetLastEgripExtractDateAsync(ogrn);
                if (extract_date != null)
                {
                    return File(GetEgripStream(ogrn, extract_date.Value), "application/pdf", ogrn + ".pdf");
                }
            }
            return HttpNotFound();
        }
        [Route("Report/SaveEgrip/{ogrn}/")]
        public async Task<ActionResult> SaveEgrip(string ogrn)
        {
            if (!string.IsNullOrWhiteSpace(ogrn))
            {
                DateTime? extract_date = await SqlUtiltes.GetLastEgripExtractDateAsync(ogrn);
                if (extract_date != null)
                {
                    string save_path = Configs.EgripPdfDocPath + string.Format(@"\ip\{0}\", ogrn);
                    if (!Directory.Exists(save_path))
                    {
                        Directory.CreateDirectory(save_path);
                    }
                    var file_path = Path.Combine(save_path, string.Format("{0}_{1}.pdf", DateTime.Now.ToString("yyyyMMdd"), ogrn));
                    var egrip_stream = GetEgripStream(ogrn, extract_date.Value);
                    using (var fs = new FileStream(file_path, FileMode.Create))
                    {
                        egrip_stream.CopyTo(fs);
                    }
                    await SqlUtiltes.SaveEgripReport(ogrn);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }


            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        private MemoryStream GetEgripStream(string ogrn, DateTime extract_date)
        {
            IEgripRepository e_rep = new EgripRepository(ogrn, extract_date, Configs.ConnectionString);
            var egrip = new FL_Creator(e_rep).Create();
            var fl_report = new EgripReportCreator(egrip).Create();

            using (Report report = new Report())
            {
                PDFExport export = new PDFExport();
                report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Templates/egrip.frx"));
                report.RegisterData(fl_report.CoreList, "CoreInfo");
                report.RegisterData(fl_report.Rows, "EgrulRows");

                report.Report.Prepare();

                var stream = new MemoryStream();
                report.Report.Export(export, stream);
                stream.Position = 0;
                return stream;
            }
        }
    }
}