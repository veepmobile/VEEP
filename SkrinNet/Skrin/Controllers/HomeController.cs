using Skrin.BLL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.Models.News;
using Skrin.BLL.Root;
using System.Threading.Tasks;
using System.Web.Caching;


namespace Skrin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Контроль версии библиотеки serch_contra_bll, при ее изменении необходимо менять имя константы
            var fake = serch_contra_bll.Variable.v4;
            //Контроль версии библиотеки FreeEgrulHandlerLib, при ее изменении необходимо менять имя константы
            var fake1 = FreeEgrulHandlerLib.Variable.v3;
            return View();
        }

        public async Task<JsonResult> GetNews(NewsType? type)
        {
            if (type == null)
                return Json(null,JsonRequestBehavior.AllowGet);

            if (type == NewsType.Statistc)
            {
                Statistic st = null;
                st = (Statistic)HttpContext.Cache[type.ToString()];
                if (st == null)
                {
                    st = await NewsRepository.GetStatisticAsync();
                    HttpContext.Cache.Insert(type.ToString(), st, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                }                
                return Json(st, JsonRequestBehavior.AllowGet);
            }
            MessageInfo mi = null;
            mi = (MessageInfo)HttpContext.Cache[type.ToString()];
            if (mi == null)
            {
                switch (type)
                {
                    case NewsType.SiteUpdates:
                        mi = await NewsRepository.GetSiteUpdatesAsync();                        
                        break;
                    case NewsType.LastReports:
                        mi = await NewsRepository.GetLastReportsAsync();
                        break;
                    case NewsType.LastEvents:
                        mi = await NewsRepository.GetLastEventsAsync();
                        break;
                    case NewsType.Analitics:
                        mi = await NewsRepository.GetAnaliticsAsync();
                        break;
                    case NewsType.UC:
                        mi = await NewsRepository.GetUCAsync();
                        break;
                    case NewsType.ULUpdates:
                        mi = await NewsRepository.GetULUpdatesAsync();
                        break;
                }
                if (type != NewsType.LastReports)
                {
                    Helper.SaveCache(type.ToString(), mi);
                }
                else
                {
                    HttpContext.Cache.Insert(type.ToString(), mi,null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 1, 0));
                }                    
            }
            return Json(mi, JsonRequestBehavior.AllowGet);
        }
    }
}