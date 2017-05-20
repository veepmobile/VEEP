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
using Skrin.BLL.KZ;
using Skrin.Models.ProfileKZ;


namespace Skrin.Controllers.KZ
{
    public class TabKzController : BaseController
    {
        // GET: TabKz
        public async Task<ActionResult> Index(int id, string ticker, int PG = 1, string period = "0")
        {
            UserSession us = HttpContext.GetUserSession();
            var KT = new KzQueryTabsRepository(ticker);

            switch (id)
            {
                case 2:                   
                    return View("Codes",await KT.GetCodes());                   
                case 3:
                    return View("Employments", await KT.GetPeople());
                case 4:
                    return View("Deals", await KT.GetDeals());
                case 5:
                    string date = !String.IsNullOrEmpty(this.HttpContext.Request["date"]) ? this.HttpContext.Request["date"] : "0";
                    return View("Controls", await KT.GetConstrols(date));
                default:
                    return new HttpNotFoundResult("Данная страница не реализована");
            }
        }
    }
}

/*
 2 - Коды
 3 - Сотрудники
 4 - Основная деятельность
 5 - Органы управления и контроля 
 */