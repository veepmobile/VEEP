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
using Skrin.Models.ProfileKZ;
using Skrin.BLL.Infrastructure;
using Skrin.BLL;
using Skrin.BLL.KZ;
using Skrin.BLL.Authorization;
using Skrin.BLL.Root;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;
namespace Skrin.Controllers.Iss
{
    public class ProfileKZController : BaseController
    {

        private enum Key { CanShowStopLight, CanShowLinks, CanCreateShortReport, CanCreateFullReport, CanCreateRTSReport, CanCheck };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        private static string _constring = Configs.ConnectionString;
        protected ProfileKZ Prof = new ProfileKZ();
        private const string DESKTOP_COOKIE = "use_desktop";
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();

        static ProfileKZController()
        {
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanShowLinks.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanCreateShortReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanCreateFullReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanCheck.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }


        public ProfileKZController()
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

    
        public async Task<ActionResult> Index(string iss)
        {
            int? user_id = null;
            int? data_access = null;
            try
            {
                if (!string.IsNullOrEmpty(iss))
                {
                    Stopwatch stopwatch = new Stopwatch();
                    Prof.data_access = data_access == null ? 0 : data_access.Value;
                    Prof.timeouts = new Dictionary<string, long>();
                    Prof.user_id = user_id == null ? 0 : user_id.Value;
                    Prof.denied_access_function = "";
                    Prof.Code = iss;
                    switch (Prof.data_access)
                    {
                        case 0:
                            Prof.denied_access_function = "showLogin();";
                            break;
                        case 1:
                            Prof.denied_access_function = "access_denied();";
                            break;
                        case 4:
                            Prof.denied_access_function = "access_denied_rtst();";
                            break;
                    }

                    Prof.name = await SqlUtiltes.GetKZNameAsync(iss);

                    if (error_list.Count > 0)
                    {
                        Helper.SendEmail(string.Join("\n", error_list), "Ошибка в профиле ИП КА");
                        return Content(GetErrorText());
                    }

                    return View(Prof);
                }
            }
            catch (Exception ee)
            {
                Helper.SendEmail(ee.ToString() + "\n" + string.Format("Запрос: iss={0},user_id={1},data_access={2}", iss, user_id, data_access), "Ошибка в профиле KZ");
                return Content(GetErrorText());
            }
            return Content("");
        }


        public ActionResult ProfileMain(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.UseDesktop = UseDesktop;

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            var rep = new ProfileKzRepository(ticker);
            ProfileKZ profile = us.UserId > 0 ? rep.GetProfile() : rep.GetClosedProfile();

            ViewBag.CanShowLinks = us.HasRole(roles, Key.CanShowLinks.ToString());
            ViewBag.ShowInfo = us.UserId > 0;

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