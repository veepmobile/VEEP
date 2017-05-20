using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Iss;
using Skrin.BLL.IP;
using Skrin.BLL.KZ;
using Skrin.BLL.UA;
using System.Threading.Tasks;
using Skrin.Models.Iss;
using Skrin.BLL.Root;

namespace Skrin.Controllers.Iss
{
    public class MenuController : Controller
    {
        // GET: Menu
        public async Task<ActionResult> GetMenu(string ticker)
        {
            CompanyShortInfo ci = await SqlUtiltes.GetShortInfoAsync(ticker);
            if (ci == null)
            {
                return HttpNotFound("Данной организации не найдено");
            }

            MenuRepository rep=new MenuRepository(ticker);
            return Json(await rep.GetProfileMenuAsync(),JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// GetIpMenu
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetIPMenu(string ticker)
        {
            MenuIPRepository rep = new MenuIPRepository(ticker);
            return Json(await rep.GetProfileMenuAsync(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetKZMenu
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetKZMenu(string ticker)
        {
            MenuKzRepository rep = new MenuKzRepository(ticker);
            return Json(await rep.GetProfileMenuAsync(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetKZMenu
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetUAMenu(string edrpou)
        {
            MenuUaRepository rep = new MenuUaRepository(edrpou);
            return Json(await rep.GetProfileMenuAsync(), JsonRequestBehavior.AllowGet);
        }
    }
}