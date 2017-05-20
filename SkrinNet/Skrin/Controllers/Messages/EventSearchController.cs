using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Skrin.BLL.Iss;
using Skrin.Models;

namespace Skrin.Controllers.Messages
{
    public class EventSearchController : BaseController
    {
        private enum Key { canSearch };

        private static Dictionary<string,AccessType> roles = new Dictionary<string,AccessType>();


        static EventSearchController()
        {
            roles.Add(Key.canSearch.ToString(),AccessType.Pred | AccessType.Mess );
        }

        public ActionResult Index()
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.RolesJson = us.GetRigthList(roles);
            ViewBag.LocPath = "/dbsearch/eventsearch/";
            ViewBag.Title = "СКРИН-Контрагент: Корпоративные события.";
            ViewBag.Description = "Поиск корпоративных событий.";

            return View();
        }

        public async Task<ActionResult> EventSearchAsync(string dfrom, string dto, string type_id, int types_excl, string search_name, int page, int page_length, int? grp)
        {
            UserSession us = HttpContext.GetUserSession();

            //Пользователей без прав дальше первой страницы не пускаем
            if (!us.HasRole(roles, Key.canSearch.ToString()))
            {
                page = 1;
                dfrom = "";
                dto = "";
            }

            if (dfrom == "") { dfrom = EventsRepository.EventStartDate(); }
            if (dto == "") { dto = dfrom; }
            return Json(await EventsRepository.EventsListSearchAsync(search_name, type_id, types_excl, getDate(dfrom), getDate(dto), page, page_length, grp));      
        }

        public ActionResult GetStartDate()
        {
            return Content(EventsRepository.EventStartDate());
        }

        protected string getDate (string cdata)
        {
            var cd = cdata.Split('.');
            return cd[2] + cd[1] + cd[0];
        }
    }
}