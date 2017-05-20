using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Root;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using System.Threading.Tasks;
using System.Net;
using Skrin.Models.Iss;
using System.Data;
using System.Data.SqlClient;



namespace Skrin.Controllers
{
    public class OperationController : Controller
    {
        // action=22
        public ActionResult EgrulMonitoringList()
        {
            int user_id = HttpContext.GetUserSession().UserId;
            string result = user_id==0 ? "" : AuthenticateSqlUtilites.GetEgrulMonitoringList(user_id);
            return Content(result);
        }

        //action=21
        public ActionResult GetGroupInfo(string issuer_id)
        {
            int user_id = HttpContext.GetUserSession().UserId;
            var i_ud = issuer_id.IndexOf('_') > 0 ? issuer_id.Split('_')[0] : null;
            if(user_id>0 && i_ud!=null)
            {
                return Content(AuthenticateSqlUtilites.GetGroupInfo(i_ud, user_id));
            }
            return Content("");
        }

        //action9(Из контрагента)
        public async Task<ActionResult> GetReport(string url)
        {
            using(WebClient wc=new WebClient())
            {
                string result=await wc.DownloadStringTaskAsync(new Uri(url));
                return Content(result, "text/plain");
            }
        }
        //action5(Из контрагента) запрос лимита отчетов
        public async Task<ActionResult> CheckReportLimit(ReportLimitType report_type,string report_code)
        {
            int user_id = HttpContext.GetUserSession().UserId;
            return Json(await AuthenticateSqlUtilites.CheckReportLimit(user_id, report_type,report_code));
        }
        //Загрузка отчета
        public ActionResult LoadReport(string id)
        {
            string path = Configs.Cloud;
            string fn = "";
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString)){
                SqlCommand cmd = new SqlCommand(@"Select cast(user_id as varchar(25)) + '\' + issuer_id + '\' + id + '.pdf' as pt, filename as fn from skrin_net..UserReports   where id=@id",con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar,32).Value = id;
                cmd.Connection.Open();
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                path = Configs.Cloud + r.GetValue(0).ToString();
                fn=r.GetValue(1).ToString();
        }
            return File(path, "application/pdf", fn);
                

        }
        //Загрузка списка отчетов
        public async Task<ActionResult> ShowReports(string iss)
        {
            XSLGenerator g = new XSLGenerator("skrin_net..getReportList", new Dictionary<string, object> { { "@iss", iss }, { "@uid", HttpContext.GetUserSession().UserId } }, "profilereport", new Dictionary<string, object> { });
            return Content(await g.GetResultAsync());
        }
       //Удаление отчета
        public ActionResult DelURep(string id)
        {
            //удаляем с диска. Для этого нужен путь.
            string path = "";
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand(@"Select cast(user_id as varchar(25)) + '\' + issuer_id + '\' + id + '.pdf' as pt, filename as fn from skrin_net..UserReports   where id=@id", con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
                cmd.Connection.Open();
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                path = Configs.Cloud + r.GetValue(0).ToString();
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                cmd.Connection.Close();
                cmd.CommandText = @"Delete  from skrin_net..UserReports   where id=@id";
                cmd.Connection.Open();
                r = cmd.ExecuteReader();
                cmd.Connection.Close();
            }
            return Content("");
        }

    }
}