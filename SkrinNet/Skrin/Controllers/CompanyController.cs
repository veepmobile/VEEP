using Newtonsoft.Json;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Company;
using Skrin.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
        public ActionResult Ask()
        {
            return View(AskManager.GetThemes());
        }

        [HttpPost]
        public ActionResult Ask(string ask)
        {
            Ask parsed_ask = JsonConvert.DeserializeObject<Ask>(ask);
            AskManager.SendEmail(parsed_ask);
            if (parsed_ask.subscribe && !string.IsNullOrWhiteSpace(parsed_ask.info.email))
            {
                AskManager.Subscribe(parsed_ask.info.email);
            }
            return Content("<strong>Ваш запрос отправлен в Отдел продаж и маркетинга. В ближайшее время специалисты СКРИН с Вами свяжутся.</strong>");
        }

        public ActionResult Confirm(string em)
        {
            if (string.IsNullOrWhiteSpace(em))
            {
                return RedirectToAction("Index", "Home");
            }
            AskManager.ConfirmEmail(em);
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Access()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BaseSkrin()
        {
            return View();
        }    

        [HttpGet]
        public ActionResult Api()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Monitor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rules()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Faq()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Programs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Block()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Block(AskInfo info)
        {
            //Ask parsed_ask = JsonConvert.DeserializeObject<Ask>(ask);
            AskManager.SendEmailBlock(info);
            return Content("<strong>Ваш запрос отправлен в Отдел продаж и маркетинга. В ближайшее время специалисты СКРИН с Вами свяжутся.</strong>");
        }

        public ActionResult AskUser(AskInfo info)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId > 0)
            {
                info.login = us.User.Login;
                AskManager.SendEmail(info, AskUserType.Authenticated);
            }
            else
            {
                AskManager.SendEmail(info, AskUserType.NonAuthenticated);
            }
            return Content("<h4>Ваш запрос отправлен в Отдел продаж и маркетинга. В ближайшее время специалисты СКРИН с Вами свяжутся.</h4>");
        }
    }
}