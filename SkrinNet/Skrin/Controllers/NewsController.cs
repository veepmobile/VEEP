using Skrin.BLL.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public async Task<ActionResult> Index(int? id)
        {
            var headers = await NewsRepository.GetNewsHeadersAsync();
            ViewBag.id = id ?? 0;
            return View(headers);
        }


        public async Task<ActionResult> GetNews(int id)
        {
            return Json(await NewsRepository.GetNewsAsync(id), JsonRequestBehavior.AllowGet);
        }
    }
}