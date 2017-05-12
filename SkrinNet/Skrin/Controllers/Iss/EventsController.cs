using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Iss;
using System.Threading.Tasks;

namespace Skrin.Controllers.Iss
{
    public class EventsController : Controller
    {
        // GET: Events
        public async Task<ActionResult> GetMessageDatesAsync(string issuer_id)
        {
            return Json(await EventsRepository.GetCorpCalAsync(issuer_id));
        }

        public async Task<ActionResult> GetMessageTypesAsync()
        {           
            return Json(await EventsRepository.GetEventTypesAsync());
        }

        public async Task<ActionResult> SearchAsync(string issuer_id, string type_id, string dfrom, string dto, int page)
        {
            return Json(await EventsRepository.EventSearchAsync(issuer_id,type_id,dfrom,dto,page));      
        }
    }
}