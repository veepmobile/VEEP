using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;
using Skrin.Models;
using Skrin.Models.ProfileIP;
using Skrin.BLL.Infrastructure;
using Skrin.BLL;
using Skrin.BLL.IP;
using Skrin.BLL.Authorization;
using Skrin.BLL.Root;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;

namespace Skrin.Controllers.Iss
{
    public class ProfileIPController : BaseController
    {

        private enum Key { CanShow, CanShowStopLight, CanCheck, CanShowProfileFL, CanAddToGroup };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        private static string _constring = Configs.ConnectionString;
        protected ProfileIP Prof = new ProfileIP();
        private const string DESKTOP_COOKIE = "use_desktop";
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();
        int i_bank1 = 0;
        int i_bank2 = 0;
        int i_pravo1 = 0;

        const string REPLACER = "*****";
        const string DATEREPLACER = "**.**.****";

        static ProfileIPController()
        {
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.Mess | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln | AccessType.MonEgrul);
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanCheck.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanShowProfileFL.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanAddToGroup.ToString(),AuthenticateSqlUtilites.GetGroupRoles().Value);
            
        }

        public ProfileIPController()
        {
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            
        }

        public bool UseDesktop
        {
            get
            {
                try
                {
                    string use_dectop = CookieHelper.GetCookieVal(HttpContext.Request, DESKTOP_COOKIE);
                    return use_dectop == "True";
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                CookieHelper.AddCookie(HttpContext.Response, DESKTOP_COOKIE, value.ToString(), DateTime.Now.AddYears(1));
            }
        }

        // GET: ProfileIP
        public async Task<ActionResult> Index(string iss)
        {

            if (!string.IsNullOrEmpty(iss))
            {
                Prof.ogrnip = iss;
                CompanyData data = await SqlUtiltes.GetIPAsync(iss);
                if (data != null)
                {
                    Prof.name = data.Name;
                    return View(Prof);
                }
            }
            return Content(GetMissText());          
        }

        public ActionResult ProfileMain(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.UseDesktop = UseDesktop;

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            var rep = new ProfileIpRepository(ticker);
            ViewBag.OpenProfile = us.HasRole(roles, Key.CanShow.ToString());
            ProfileIP profile = ViewBag.OpenProfile ? rep.GetProfile() : rep.GetClosedProfile();

            ViewBag.CanShowStopLight = us.HasRole(roles, Key.CanShowStopLight.ToString());


            return View(profile);
        }


        public ActionResult ProfileRightMenu(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            var rep = new ProfileIPRightMenuRepository(ticker, us.UserId);
            ProfileIpRightMenu profile = rep.GetProfileRightMenu();


            ViewBag.CanCheck = us.HasRole(roles, Key.CanCheck.ToString());
            ViewBag.CanShowProfileFL = us.HasRole(roles, Key.CanShowProfileFL.ToString());
            ViewBag.CanAddToGroup = us.HasRole(roles, Key.CanAddToGroup.ToString());

            return View(profile);
        }

        public static string GetErrorText()
        {
            return "<div style='width:100%;height:400px;margin: 50px 30px;'><h1 style='margin:20px 0'>Техническая ошибка</h1><p>В настоящее время наши сотрудники работают над ее устранением.</p><p>Попробуйте зайти позже</p></div>";
        }

        public static string GetMissText()
        {
            return "<div style='width:100%;height:400px;margin: 50px 30px;'><h1 style='margin:20px 0'>Ошибочный запрос</h1><p>Данной страницы не существует.</p></div>";
        }

    }
}