using Skrin.BLL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models.Monitoring;
using FreeEgrulHandlerLib.Models;

namespace Skrin.Controllers
{
    public class MonitorOperationsController : Controller
    {

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            return View();
        }

        public ActionResult LoadSubForm(int i)
        {
            return View(i);
        }



        // GET: MonitorOperations
        public async Task<ActionResult> AddToEgrul(string id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return new  HttpUnauthorizedResult();
            int rc = await MonitorRepository.AddToEgrulMonitorAsync(id, us.UserId);
            return Json(rc);
        }

        public async Task<ActionResult> RemoveFromEgrul(string id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return new HttpUnauthorizedResult();
            await MonitorRepository.RemoveFromEgrulMonitorAsync(id, us.UserId);
            return Content("");
        }
        public async Task<ActionResult> RemoveFromMess(string id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return new HttpUnauthorizedResult();
            await MonitorRepository.RemoveFromMessMonitorAsync(id, us.UserId);
            return Content("");
        }

        public async Task<ActionResult> GetUpdateMonitorInfo()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Content("[]");
            var info = await MonitorRepository.GetUpdateMonitorInfoAsync(us.UserId);
            return Json(info);
        }

        public async Task<ActionResult> AddGroupForUpdate(List<SubscriptionInfo> si)
        {

            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0 )
                return Json(false);
            await MonitorRepository.AddGroupForUpdateAsync(user_id, si ?? new List<SubscriptionInfo>());
            return Json(true);
        }

        public async Task<ActionResult> AddGroupForMessType(List<MessageSubcriptionInfo> si)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
                return Json(false);
            await MonitorRepository.AddGroupForMessTypeAsync(user_id, si ?? new List<MessageSubcriptionInfo>());
            return Json(true);
        }

        public async Task<ActionResult> GetEgrulReportEmail()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json("");
            return Json(await MonitorRepository.GetReportEmailAsysnc(SubcriptionType.Egrul, us.UserId));
        }

        public async Task<ActionResult> GetMessageReportEmail()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return new HttpUnauthorizedResult();
            return Json(await MonitorRepository.GetReportEmailAsysnc(SubcriptionType.Messages, us.UserId));
        }

        public async Task<ActionResult> GetCompaniesInEgrulList()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(new List<EgrulMonitorULDetails>());
            return Json(await MonitorRepository.GetCompaniesInEgrulListAsync(us.UserId));
        }

        public async Task<ActionResult> EgrulUpdateEmail(string email)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0 || string.IsNullOrWhiteSpace(email))
                return Json(false);
            await MonitorRepository.EgrulUpdateEmailAsyc(email.Trim(), us.UserId);
            return Json(true);
        }

        public async Task<ActionResult> DeleteCompaniesFromEgrulList(string issuers)
        {
            UserSession us = HttpContext.GetUserSession();

             if (us.UserId == 0 || string.IsNullOrWhiteSpace(issuers))
                return Json(false);

             List<string> issuers_list = issuers.Split(',').ToList();
           
            foreach (var issuer in issuers_list)
            {
                await MonitorRepository.DeleteCompanyFromEgrulListAsync(us.UserId, issuer);
            }
            return Json(true);
        }

        public async Task<ActionResult> GetMessageMonitorGroup()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(new List<MessageMonitorGroup>());
            return Json(await MonitorRepository.GetMessageMonitorGroupAsync(us.UserId));
        }

        public async Task<ActionResult> GetMessageTypes()
        {
            var m_types = await MonitorRepository.GetMessageTypesAsync();
            return Json(m_types.Select(p => new {id=p.Key,name=p.Value}));
        }

        public async Task<ActionResult> GetEgrulMonitorGroup()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(new List<EgrulMonitorGroup>());
            return Json(await MonitorRepository.GetEgrulMonitorGroupAsync(us.UserId));
        }

        public ActionResult GetEgrulTypes()
        {
            var type_list = Enum.GetValues(typeof(EgrulGroups)).Cast<EgrulGroups>().Select(p =>new  {id=(int)p,name=p.GetDescription<EgrulGroups>(),isIP=0});
            var ip_type_list=Enum.GetValues(typeof(EgripGroups)).Cast<EgripGroups>().Select(p => new { id = (int)p, name = p.GetDescription<EgripGroups>(),isIP=1});
            type_list = type_list.Concat(ip_type_list);
            return Json(type_list, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddGroupForEgrulType(List<EgrulMonitorGroup> si)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (user_id == 0)
                return Json(false);
            await MonitorRepository.AddGroupForEgrulTypeAsync(user_id, si ?? new List<EgrulMonitorGroup>());
            return Json(true);
        }
    }
}