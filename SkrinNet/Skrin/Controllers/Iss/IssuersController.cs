using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Iss;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using Skrin.BLL.Root;
using System.Net;
using System.Data;
using System.Data.SqlClient;

namespace Skrin.Controllers.Iss
{
    public class IssuersController : BaseController
    {
        private enum Key { CanShow, CanShowLinks, CanCreateShortReport, CanCreateFullReport, CanCheck, CanAddToGroup, CanVis, CanFL };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        
        private const string DESKTOP_COOKIE = "use_desktop";

        static IssuersController()
        {
            roles.Add(Key.CanShowLinks.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanCreateShortReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanCreateFullReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanAddToGroup.ToString(), AuthenticateSqlUtilites.GetGroupRoles().Value);
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanCheck.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanVis.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanFL.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
        }

        public IssuersController()
        {
            ViewBag.LoginFunction = "need_login();return false;";
            ViewBag.AccessFunction = "no_rights();return false;";
            
        }

        /// <summary>
        /// Использовать десктопную версию для мобильников
        /// </summary>
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


        // GET: Issuers
        public async Task<ActionResult> Index(string ticker,int? id, int? tab_id)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                return RedirectToAction("Companies", "DBSearchRu");
            }
            ViewBag.UseDesktop = UseDesktop;
            ViewBag.OpenCompany = open_companies.Contains(ticker.ToLower());
            ViewBag.Script = id == null ? "" : string.Format(@"
                    INIT_FUNCT['gotomenu'] =function(){{
                       gotomenu({0}{1}); 
                    }};
                ",id,tab_id==null ? "":","+tab_id);

            UserSession us = HttpContext.GetUserSession();
            ViewBag.CanFL = us.HasRole(roles, Key.CanFL.ToString());
            CompanyShortInfo ci = await SqlUtiltes.GetShortInfoAsync(ticker);
            if (ci != null)
            {
                return View(ci);
            }
            string true_ticker = SqlUtiltes.FindTrueTicker(ticker);
            if (true_ticker !=null)
            {
                return Redirect("/issuers/"+true_ticker);
            }
            return HttpNotFound("Данной организации не найдено");
        }

        public  ActionResult ProfileMain(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.UseDesktop = UseDesktop;

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            bool open_company=open_companies.Contains(ticker.ToLower());

            ViewBag.OpenProfile = us.HasRole(roles, Key.CanShow.ToString()) || open_company;

            var rep=new ProfileRepository(ticker);
            Profile profile = ViewBag.OpenProfile ? rep.GetProfile() : rep.GetClosedProfile();

            ViewBag.CanShowLinks = us.HasRole(roles, Key.CanShowLinks.ToString());
            ViewBag.CanFL = us.HasRole(roles, Key.CanFL.ToString());
            ViewBag.CanVis = us.HasRole(roles, Key.CanVis.ToString());
           
            
            ViewBag.ShowInfo = open_company || us.UserId > 0 ;


            return View(profile);
        }

        public ActionResult ProfileRightMenu(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            bool open_company = open_companies.Contains(ticker.ToLower());
            ViewBag.OpenProfile = us.HasRole(roles, Key.CanShow.ToString()) || open_company;

            var rep = new ProfileRightMenuRepository(ticker, us.UserId, ViewBag.OpenProfile);
            ProfileRightMenu profile=rep.GetProfileRightMenu();

            

            bool can_create_full_report=us.HasRole(roles, Key.CanCreateFullReport.ToString());
            bool can_create_short_report = us.HasRole(roles, Key.CanCreateShortReport.ToString());
            bool can_check = us.HasRole(roles, Key.CanCheck.ToString());
            ViewBag.CanCreateReport = can_create_full_report || can_create_short_report;
            ViewBag.CanAddToGroup = us.HasRole(roles, Key.CanAddToGroup.ToString());
            ViewBag.CanCheck = can_check;
            ViewBag.CanVis = us.HasRole(roles, Key.CanVis.ToString());
            ViewBag.ReportAvailable = ReportAvailable(ticker);
            profile.choice=new List<Choice>{
                new Choice(1,"Профиль",true,true),
                new Choice(2,"Статистика  по государственным контрактам",false,!can_create_short_report),
                new Choice(3,"Сообщения о банкротстве",false,!can_create_full_report),
                new Choice(4,"Статистика арбитражных дел",false,!can_create_full_report),
                new Choice(5,"Вестник государственной регистрации",false,!can_create_full_report),
                new Choice(6,"Бухгалтерская отчетность",false,false),
                new Choice(5,"Финансовый анализ",false,!can_create_full_report),
            };




            

            return View(profile);
        }

        private bool ReportAvailable(string ticker)
        {
           
            bool retval = false;
            
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"select case when exists(select 1 from searchdb2..union_search a inner join fsns_free..ul2 b on a.ogrn=b.ogrn where uniq=1 and ticker=@ticker) then '1' else '0' end as ex", con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        retval = ((string)rd.ReadNullIfDbNull("ex") == "1") ? true : false;
                    }
                }
            }
            return retval;

        }


        public ActionResult DesktopVersion()
        {
            UseDesktop = true;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult MobileVersion()
        {
            UseDesktop = false;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}