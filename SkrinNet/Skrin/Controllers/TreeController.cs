using Skrin.BLL.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class TreeController : Controller
    {
        // GET: Tree
        public async Task<JsonResult> GetNodes(int act, string id, int src)
        {
            switch (act)
            {
                case 0:
                    //Здесь id должно иметь единственное значение
                    return Json(await TreeRepository.GetCodeLinesAsync(src, int.Parse(id)), JsonRequestBehavior.AllowGet);
                case 1:
                    return Json(await TreeRepository.GetExpandedCodeLinesAsync(src, id), JsonRequestBehavior.AllowGet);
                default:
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> TreeSearch(int src,string st)
        {
            return Json(await TreeRepository.GetShortCodeLinesAsync(src, st), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TreeSelector(int src, string nodes)
        {
            ViewBag.src = src;
            ViewBag.nodes = nodes;
            return View();
        }

        public async Task<ActionResult> GetResultString(int src, string id)
        {
            return Content(await TreeRepository.GetResultStringAsync(src, id));
        }
    }
}