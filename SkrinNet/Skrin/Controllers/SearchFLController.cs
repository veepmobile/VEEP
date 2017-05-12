using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Search;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.Controllers
{
    public class SearchFLController : Controller
    {
        private enum Key { canSearch, canExport };

        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static SearchFLController()
        {
            roles.Add(Key.canSearch.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.canExport.ToString(), AccessType.Pred | AccessType.KaPoln);
        }
        public ActionResult Index(string fio)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/dbsearchru/fl/";

            List<string> insert_searches = new List<string>();

            if (!string.IsNullOrWhiteSpace(fio))
            {
                insert_searches.Add("$('#fio').val('" + fio + "');");
            }

            return View(insert_searches);
        }

        public async Task<ActionResult> Search(SearchFLObject so)
        {
            UserSession us = HttpContext.GetUserSession();
            if(!us.HasRole(roles, Key.canSearch.ToString()))
            {
                return new HttpStatusCodeResult(403);
            }

            try
            {
                string url = "/profilefl/_search";
                int fr = (so.page - 1) * 20;
                string json = "{\"query\": {\"bool\": {\"must\": {\"multi_match\": {" +
                    "  \"query\": \"" + so.fio + "\"," +
                    "  \"type\": \"cross_fields\"," +
                    "  \"fields\": [ \"fio\", \"inn\", \"ip.ogrnip\" ]," +
                    "  \"operator\": \"and\"} } } }, " +
                    (so.page > 0 ? "\"size\": \"20\", \"from\": \"" + fr + "\"," : "\"size\": \"10000\",") +
                    "\"sort\": [{\"FLID\": {\"order\": \"asc\"}}] }";

                ElasticClient ec = new ElasticClient();
                JObject r = await ec.GetQueryAsync(url, json);
                if (so.page > 0)
                {
                    int n = r["hits"]["hits"].Count();
                    for (int i = 0; i < n; i++)
                    {
                        ((JObject)r["hits"]["hits"][i]).Add("id", (string)r["hits"]["hits"][i]["_id"]);
                    }
                    return Content(r.ToString());
                }
                else
                {
                    List<FLInfo> infolist = LoadInfoList(r);
                    string filename = Guid.NewGuid().ToString() + ".xlsx";
                    string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        ExportExcelFL exp = new ExportExcelFL();
                        exp.ExportWorkbook(infolist).SaveAs(memoryStream);
                        memoryStream.Position = 0;
                        FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                        memoryStream.WriteTo(file);
                        file.Close();
                        memoryStream.Close();

                    }
                    return Content(filename);
                }
            }
            catch (Exception ex)
            {
                return Content("{\"error\" : \"Ошибка выполнения запроса\"}");
            }
        }

        public ActionResult GetExcel(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                string ids = "";
                string[] list = issuers.Split('|');
                bool test = !us.HasRole(roles, Key.canExport.ToString(), TestConsideration.NotTest);
                for (int i = 0; i < list.Length; i++)
                    ids += (i == 0 ? "" : ",") + "\"" + list[i] + "\"";

                string url = "/profilefl/_search";
                string json = "{\"query\": {\"ids\": {\"values\": [" + ids + "]}}," +
                    "\"size\": \"" + list.Length + "\"," +
                    "\"sort\": [{\"FLID\": {\"order\": \"asc\"}}] }";

                ElasticClient ec = new ElasticClient();
                JObject r = ec.GetQuery(url, json);
                List<FLInfo> infolist = LoadInfoList(r);

                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + filename;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ExportExcelFL exp = new ExportExcelFL();
                    exp.ExportWorkbook(infolist,test).SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                    memoryStream.WriteTo(file);
                    file.Close();
                    memoryStream.Close();

                }
                return Content(filename);
            }
            else
            {
                return Json(new { error = "Отсутствует доступ" }, JsonRequestBehavior.AllowGet);
            }
        }

        [DeleteFileAttribute]
        public ActionResult GetFile(string src, string page)
        {
            UserSession us = HttpContext.GetUserSession();

            if (us.HasRole(roles, Key.canExport.ToString()))
            {
                string filepath = Path.Combine(Server.MapPath("~/xlsreports/")) + src;

                return File(filepath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            return new HttpUnauthorizedResult();
        }
        private List<FLInfo> LoadInfoList(JObject r)
        {
            List<FLInfo> infolist = new List<FLInfo>();

            int n = r["hits"]["hits"].Count();
            for (int i = 0; i < n; i++)
            {
                FLInfo info = new FLInfo { fio = (string)r["hits"]["hits"][i]["_source"]["fio"], inn = (string)r["hits"]["hits"][i]["_source"]["inn"] };
                infolist.Add(info);
            }

            return infolist;
        }
    }
}