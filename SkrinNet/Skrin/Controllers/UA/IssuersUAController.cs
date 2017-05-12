using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL.UA;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.Models.UA;
using Skrin.Models;
using Skrin.BLL.Root;
using System.Text;
using System.IO;
using System.Net;


namespace Skrin.Controllers.UA
{
    public class IssuersUAController : BaseController
    {
        private enum Key { CanShowStopLight, CanShowLinks, CanCreateShortReport, CanCreateFullReport, CanCreateRTSReport,CanCheck };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        
        private const string DESKTOP_COOKIE = "use_desktop";

        static IssuersUAController()
        {
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanShowLinks.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanCreateShortReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
            roles.Add(Key.CanCreateFullReport.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanCheck.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Mess | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }

        public IssuersUAController()
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
        public async Task<ActionResult> Index(string edrpou)
        {
            ViewBag.UseDesktop = UseDesktop;
            ViewBag.OpenCompany = false;
            CompanyUAShortInfo ci = await SqlUtiltes.GetUAShortInfoAsync(edrpou);
            if (ci != null)
            {
                return View(ci);
            }
            return HttpNotFound("Данной организации не найдено");
        }

        public ActionResult ProfileMain(string edrpou)
        {
            UserSession us = HttpContext.GetUserSession();
            ViewBag.UseDesktop = UseDesktop;

            if (string.IsNullOrEmpty(edrpou))
                return Content("");

            var rep = new ProfileUaRepository(edrpou);
            UAProfile profile = (us.UserId>0) ?  rep.GetProfile() : rep.GetClosedProfile();

            ViewBag.CanShowStopLight = us.HasRole(roles, Key.CanShowStopLight.ToString());
            ViewBag.CanShowLinks = us.HasRole(roles, Key.CanShowLinks.ToString());
           
            ViewBag.OpenProfile = us.UserId>0;
            ViewBag.ShowInfo = us.UserId > 0;

            return View(profile);
        }

        public async Task<ActionResult> tab(int id, string edrpou, int pg)
        {

            if (string.IsNullOrEmpty(edrpou))
                return Content("");

            string tab_roles = await SqlUtiltes.GetUaTabAccesses(id);
            if (tab_roles == null)
            {
                return new HttpNotFoundResult("Страницы с данным идентификатором не существует");
            }

            var avail_for_roles = tab_roles.Split(',').Select(p => (AccessType)Enum.Parse(typeof(AccessType), p)).ToList();
            UserSession us = HttpContext.GetUserSession();
            ViewBag.UseDesktop = UseDesktop;

            UAReportModel _rep = null;
            switch (id)
            {
                case 2:
                    _rep = await new UAReportRepository(edrpou, UAReportType.AnnualReports).GetReportListAsync();
                    break;
                case 3:
                    _rep = await new UAReportRepository(edrpou, UAReportType.QuartReports).GetReportListAsync();
                    break;
            }
            return View("UAReportList", _rep);
        }

        public async Task<ActionResult> Documents(string doc_id)
        {
            UserSession us = HttpContext.GetUserSession();

            var rep = new UAReportRepository();
            UADocument _doc = await rep.getDocument(doc_id);
            if (_doc == null) { throw new Exception("Document not found"); }
            if (us.UserId == 0) { throw new Exception("Access denied"); }
            
            string file_path = string.Format(@"{0}{1}\{2}\{3}", Configs.UADocPath, _doc.edrpou, doc_id, _doc.file_name);
            string _file_ext = System.IO.Path.GetExtension(_doc.file_name).Substring(1);
            FileType file_type = ContentTypeCollection.GetFileType(_file_ext);
            string content_type = ContentTypeCollection.GetContentType(_file_ext);

            if (System.IO.File.Exists(file_path))
            {
                switch (file_type)
                {
                    case FileType.Binary:
                    case FileType.Text:
                    case FileType.Img:
                        return File(file_path, content_type);
                    case FileType.Xml:
                        try
                        {
                            string xml_content = await Utilites.GetFileContent(file_path, Encoding.GetEncoding("Windows-1251"));
                            HTMLCreator creator = new HTMLCreator(_doc.file_name.Replace(".xml", ""));
                            return Content(creator.GetHtml(xml_content), ContentTypeCollection.GetContentType("html"));
                        }
                        catch
                        {
                            return File(file_path, content_type);
                        }
                        break;
                }
            }
//            file_path = this.Request.MapPath("../../doc.txt") ;
            content_type = "text/html";
            return File(file_path, content_type);
        }

/*
        public ActionResult ProfileRightMenu(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();

            if (string.IsNullOrEmpty(ticker))
                return Content("");

            var rep = new ProfileRightMenuRepository(ticker, us.UserId);
            ProfileRightMenu profile=rep.GetProfileRightMenu();

            bool open_company = open_companies.Contains(ticker.ToLower());

            bool can_create_full_report=us.HasRole(roles, Key.CanCreateFullReport.ToString());
            bool can_create_short_report = us.HasRole(roles, Key.CanCreateShortReport.ToString());
            bool can_check = us.HasRole(roles, Key.CanCheck.ToString());
            ViewBag.CanCreateReport = can_create_full_report || can_create_short_report;
            ViewBag.CanCheck = open_company || can_check;

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
*/

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