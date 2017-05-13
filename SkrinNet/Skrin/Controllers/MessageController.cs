using Skrin.BLL.Root;
using Skrin.BLL.Iss;
using Skrin.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Skrin.BLL.Report;

namespace Skrin.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Event(int id, int agency_id, string ticker)
        {
            EventInfo ei = NewsRepository.GetEventInfo(id, agency_id);
            if(ei!=null && !string.IsNullOrEmpty(ticker))
            {
                ei.Ticker = ticker;
                ei.ShortFirmName = SqlUtiltes.GetShortName(ticker);
            }
            return View(ei);
        }

        public async Task<ActionResult> EventsSelected(string events_id,string ss, string iss_code, string ms)
        {
            string sHtml = await AdditionalRepository.GetEventMessage(iss_code,ss,events_id);
            return Content(sHtml);
        }

        public async Task<ActionResult> AuditorsEvent(string ticker, string aud_id)
        {
            XSLGenerator g = new XSLGenerator("skrin_content_output..auditors_ns", new Dictionary<string, object> { { "@iss", ticker }, { "@type", 0 },{"@aud_id",aud_id}}, 
                "tab_content/auditors/auditor", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
            return Content(await g.GetResultAsync());  
        }

        public async Task<ActionResult> AuditorClients(string id)
        {          
            return Content(await AdditionalRepository.GetAuditorsClients(id));
        }

        public async Task<ActionResult> RegisterSkrin(string ticker, string id)
        {
            XSLGenerator g = new XSLGenerator("skrin_content_output..RegHolder_details", new Dictionary<string, object> { { "@id", id } },
                "tab_content/registr/register", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
            return Content(await g.GetResultAsync());
        }

        public async Task<ActionResult> GetRegister(string reg_id, int reg_type)
        {
            return Content(await AdditionalRepository.GetRegisterMessage(reg_id,reg_type));
        }     

        public async Task<ActionResult> ActionsSkrin(int type_id, string ids, string iss)
        {
            var content = await AdditionalRepository.ActionSkrinXmlAsync(ids, iss,type_id);
            return Content(content);
        }

        public async Task<ActionResult> ShowUnfair(string inn)
        {
            if (inn.Length == 12)
            {
                var g = new XSLGenerator("skrin_content_output..unfair_providers_records_inn", new Dictionary<string, object> { { "@inn", inn } }, "tab_content/ShowUnfair", new Dictionary<string, object> { { "inn", inn }, { "PDF", -1 } });
                return Content(await g.GetResultAsync());  
            }
            else
            {
                var g = new XSLGenerator("skrin_content_output..unfair_providers_records", new Dictionary<string, object> { { "@route_id", inn } }, "tab_content/ShowUnfair", new Dictionary<string, object> { { "inn", inn }, { "PDF", -1 } });
                return Content(await g.GetResultAsync());  
            }         
           
        }

        public async Task<ActionResult> ShowFas(string ticker)
        {
            var g = new XSLGenerator("skrin_content_output..FAS_info", new Dictionary<string, object> { { "@iss", ticker} }, "fas_items", new Dictionary<string, object> {{ "PDF", -1 } });
            return Content(await g.GetResultAsync());  
        }

        public async Task<ActionResult> ShowPassport(string num)
        {
            return Content(await AdditionalRepository.GetPassport(num));
        }
    }
}