using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Company;
using Skrin.BLL.Company;

namespace Skrin.Controllers
{
    public class AnnonceController : Controller
    {
        // GET: Annonce
        public ActionResult Check()
        {
            return View();
        }

        public ActionResult TPrice()
        {
            return View();
        }

        public ActionResult Marketing()
        {
            return View();
        }

        public ActionResult Action()
        {
            return View();
        }
        
        public ActionResult Feedback(string theme)
        {
            ViewBag.Theme = theme;
            return View();
        }

        
        public ActionResult FeedbackSend(string feedback)
        {
            FeedbackCall parsed_feedback = JsonConvert.DeserializeObject<FeedbackCall>(feedback);
            AskManager.SendEmail(parsed_feedback);
            return Content("<strong>Ваш запрос отправлен в Отдел продаж и маркетинга. В ближайшее время специалисты СКРИН с Вами свяжутся.</strong>");
        }
    }
}