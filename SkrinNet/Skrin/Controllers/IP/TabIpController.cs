using Skrin.BLL.Authorization;
using Skrin.BLL.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Iss;


namespace Skrin.Controllers.IP
{
    public class TabIpController : BaseController
    {
        // GET: Tab
        public async Task<ActionResult> Index(int id, string ticker, int PG = 1, string period = "0")
        {
            string tab_roles = await SqlUtiltes.GetIpTabAccesses(id);
            if (tab_roles == null)
            {
                return new HttpNotFoundResult("Страницы с данным идентификатором не существует");
            }

            var avail_for_roles = tab_roles.Split(',').Select(p => (AccessType)Enum.Parse(typeof(AccessType), p)).ToList();           
            UserSession us = HttpContext.GetUserSession();

            var isXLS = !String.IsNullOrEmpty(this.HttpContext.Request["xls"]) ? this.HttpContext.Request["xls"] : "0";

            switch (id)
            {                
                case 2:                 
                    return View("Pravo", new PravoModel(await ContentRepository.GetPravoIp(ticker)));
                case 3:
                    var data = await SqlUtiltes.GetIPAsync(ticker);                   
                    return View("Zakupki", (object)data.INN);           
                case 4:
                    return View("Bancruptcy", new BancruptcyModel(await ContentRepository.GetBancruptcyIp(ticker)));
                case 5:
                    return View("Passport");
                default:
                    return new HttpNotFoundResult("Данная страница не реализована");
            }
           
        }
    }
}


/*
             1 - Профиль
             2 - Картотека арбитражных дел
             3 - Государственные контракты
             4 - Банкротства
             5 - Проверка паспортов
*/