using Skrin.BLL.Infrastructure;
using Skrin.BLL.Iss.Zakupki;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Skrin.Controllers.Iss
{
    public class ZakupkiController : BaseController
    {
        // GET: Zakupki
        public async Task<ActionResult> GetStatuses()
        {
            const string KEY = "ZakupkiStageGroup";
            List<ZakupkiStageGroup> zs = (List<ZakupkiStageGroup>)HttpContext.Cache[KEY];
            if (zs == null)
            {
                zs = await ZakupkiRepository.GetStageGroupsAsync();
                Helper.SaveCache(KEY, zs);
            }
            return Json(zs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(ZakupkiSearchObject so)
        {

            bool use_union = !(string.IsNullOrEmpty(so.dfrom) && string.IsNullOrEmpty(so.dto) && string.IsNullOrEmpty(so.sfrom) && string.IsNullOrEmpty(so.sto));
            ZakupkiQueryGenerator gen = new ZakupkiQueryGenerator(so);
            if (use_union)
            {
                List<string> queries = gen.GetUnionSearchStrings();
                string message = "";
                int i = 1;
                foreach (string q in queries)
                {
                    message += string.Format("Запрос {0}: {1}\n", i, q);
                    i++;
                }
                SphynxUnionSearcher usearcher = new SphynxUnionSearcher(queries, "9308", "cp1251", "sort_publish_date_ts", "desc", ((so.isAll == 1) ? 10000 : 100), so.page * 100, "int"); //so.page*100
                return Content("{" + usearcher.Search_Result + "}");
            }
            else
            {
                string query = gen.GetSearchString();
                SphynxSearcher searcher = new SphynxSearcher(query, "9308", "cp1251");
                return Content("{" + searcher.SearchJson() + "}");
            }
        }

        public async Task<JsonResult> GetNotData(string pur_num, int lot_num)
        {
            return Json(await ZakupkiRepository.GetNotDataAsync(pur_num, lot_num), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetShortNotData(string pur_num, int lot_num)
        {
            return Json(await ZakupkiRepository.GetShortNotDataAsync(pur_num, lot_num), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCont44Data(long contract_id)
        {
            return Json(await ZakupkiRepository.GetCont44DataAsync(contract_id), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCont223Data(Guid contract_id)
        {
            return Json(await ZakupkiRepository.GetCont223DataAsync(contract_id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ZakupkiDoSearchExcel(string so_string)
        {
            ZakupkiSearchObject so;
            string result = "";
                try
                {
                    so = JsonConvert.DeserializeObject<ZakupkiSearchObject>(so_string);

                    bool use_union = !(string.IsNullOrEmpty(so.dfrom) && string.IsNullOrEmpty(so.dto) && string.IsNullOrEmpty(so.sfrom) && string.IsNullOrEmpty(so.sto));
                    ZakupkiQueryGenerator gen = new ZakupkiQueryGenerator(so);
                    if (use_union)
                    {
                        List<string> queries = gen.GetUnionSearchStrings();
                        string message = "";
                        int i = 1;
                        foreach (string q in queries)
                        {
                            message += string.Format("Запрос {0}: {1}\n", i, q);
                            i++;
                        }
                        SphynxUnionSearcher usearcher = new SphynxUnionSearcher(queries, "9308", "cp1251", "sort_publish_date_ts", "desc", 10000, 0, "int");
                        result = usearcher.Search_Result;
                    }
                    else
                    {
                        string query = gen.GetSearchAllString();
                        SphynxSearcher searcher = new SphynxSearcher(query, "9308", "cp1251");
                        result = searcher.SearchJson();
                    }
                    JObject r = JObject.Parse("{" + result + "}");

                    //string ret = r.ToString();

                    List<ZakupkiDetail> details = GetZakupkiDetails(r);
                    var memoryStream = new MemoryStream();
                    ExportExcel exp = new ExportExcel();
                    exp.ExportZakupki(details).SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream, "application/vnd.ms-excel", "gz_result.xlsx");

                }
                catch
                {
                    return HttpNotFound();
                }
        }

        private List<ZakupkiDetail> GetZakupkiDetails(JObject r)
        {
            List<ZakupkiDetail> ret = new List<ZakupkiDetail>();
            try
            {
                for (int i = 0; i < r["results"].Count(); i++)
                {
                    ZakupkiDetail detail = new ZakupkiDetail();
                    bool has_notification = (int)r["results"][i]["notification_id"] != 0;
                    //bool has_contract = (int)r["results"][i]["contract_orig_id"] != 0;

                    string source = (string)r["results"][i]["source"];
                    switch (source)
                    {
                        case "1":
                            detail.sourse_fz = "ФЗ 94";
                            break;
                        case "2":
                            detail.sourse_fz = "ФЗ 223";
                            break;
                        case "3":
                            detail.sourse_fz = "ФЗ 44";
                            break;
                    }

                    detail.notification_id = (string)r["results"][i]["notification_id"];
                    detail.not_publish_date = (has_notification) ? (string)r["results"][i]["not_publish_date"] : "";
                    detail.not_product = (has_notification) ? (string)r["results"][i]["not_product"] : "";
                    detail.pur_num = (has_notification) ? (string)r["results"][i]["pur_num"] : "";
                    detail.lot_num = (has_notification) ? "Лот № " + (string)r["results"][i]["lot_num"] : "";
                    detail.st_not_sum = (has_notification) ? (string)r["results"][i]["st_not_sum"] : "";
                    detail.dif_sum = (has_notification) ? (string)r["results"][i]["dif_sum"] : "";
                    detail.dif_per = (has_notification) ? (string)r["results"][i]["dif_per"] : "";
                    detail.not_type = (has_notification) ? (string)r["results"][i]["not_type"] : "";
                    detail.not_status_name = (has_notification) ? (string)r["results"][i]["not_status_name"] : "";
                    detail.not_cust = (has_notification) ? (string)r["results"][i]["not_cust_json"]["name"] + " (ИНН: " + (string)r["results"][i]["not_cust_json"]["inn"] + ")" : "";
                    detail.not_part = "";
                    if (has_notification)
                    {
                        List<string> part = new List<string>();
                        for (int j = 0; j < r["results"][i]["part_json"].Count(); j++)
                        {
                            part.Add((string)r["results"][i]["part_json"][j]["name"] + " (ИНН: " + (string)r["results"][i]["part_json"][j]["inn"] + ")");
                        }
                        detail.not_part = string.Join(", ", part);
                    }
                    /*detail.contr_pub_date = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["pub_date"]:"";
                    detail.contr_reg_num = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["reg_num"]:"";
                    detail.contr_sum = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["sum"]:"";
                    detail.contr_stage = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["contr_stage"]:"";
                    detail.contr_placing = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["placing"]:"";
                    detail.contr_product_list = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["product_list"]:"";
                    detail.contr_customer = (has_contract)?(string)r["results"][i]["contr_data_json"][0]["customer"]["name"] + " (ИНН: " + (string)r["results"][i]["contr_data_json"][0]["customer"]["inn"] + ")":"";
                    detail.contr_supliers = "";*/
                   // if(has_contract)
                    if (r["results"][i]["contr_data_json"].Count() > 0)
                    {
                        detail.contr_pub_date = (string)r["results"][i]["contr_data_json"][0]["pub_date"];
                        detail.contr_reg_num = (string)r["results"][i]["contr_data_json"][0]["reg_num"];
                        detail.contr_sum = (string)r["results"][i]["contr_data_json"][0]["sum"];
                        detail.contr_stage = (string)r["results"][i]["contr_data_json"][0]["contr_stage"];
                        detail.contr_placing = (string)r["results"][i]["contr_data_json"][0]["placing"];
                        detail.contr_product_list = (string)r["results"][i]["contr_data_json"][0]["product_list"];
                        detail.contr_customer = (string)r["results"][i]["contr_data_json"][0]["customer"]["name"] + " (ИНН: " + (string)r["results"][i]["contr_data_json"][0]["customer"]["inn"] + ")";
                        detail.contr_supliers = "";
                        List<string> supliers = new List<string>();
                        for (int j = 0; j < r["results"][i]["contr_data_json"][0]["supliers"].Count(); j++)
                        {
                            supliers.Add((string)r["results"][i]["contr_data_json"][0]["supliers"][j]["name"] + " (ИНН: " + (string)r["results"][i]["contr_data_json"][0]["supliers"][j]["inn"] + ")");
                        }
                        detail.contr_supliers = string.Join(", ", supliers);
                    }
                    if (detail != null) { ret.Add(detail); }
                }
            }
            catch(Exception e)
            {
                string exc = e.Message;
            }
            return ret;
        }

    }
}