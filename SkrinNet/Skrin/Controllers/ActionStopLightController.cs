using serch_contra_bll.ActionStoplight;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Skrin.Models.Iss.StopLight;
using Skrin.BLL.Authorization;

namespace Skrin.Controllers
{
    public class ActionStopLightController : BaseController
    {
        private enum Key { CanShow, CanShowStopLight };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static ActionStopLightController()
        {
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }

        
        // GET: ActionStopLight
        public  ActionResult Index(string ticker)
        {
            bool open_company = open_companies.Contains(ticker.ToLower());
            UserSession us = HttpContext.GetUserSession();
            ViewBag.CanShowStopLight = open_company || us.HasRole(roles, Key.CanShowStopLight.ToString());
            bool open_profile = us.HasRole(roles, Key.CanShow.ToString()) || open_company;

            if (open_profile)
            {
                var stoplight = new ActionStopLightCreator(ticker, Configs.ConnectionString).Create();
                var asl = ASL.Create(stoplight);
                ViewBag.TotalCount = asl==null ? (int?)null : asl.total_count;
                return View(asl);
            }
            else
            {
                ViewBag.TotalCount = -1;
                return View((ASL)null);
            }
        }
    }
}