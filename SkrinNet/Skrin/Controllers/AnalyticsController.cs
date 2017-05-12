using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Analytics;
using Skrin.Models.Analytics;
using System.Threading.Tasks;

namespace Skrin.Controllers
{
    public class AnalyticsController : BaseController
    {
        // GET: Analytics
        public ActionResult Index()
        {
            ViewBag.Title = "СКРИН-Контрагент: Обзоры рынка, предприятий и отраслей.";
            ViewBag.Description = "Обзоры рынка, предприятий и отраслей.";
            return View();
        }

        public async Task<ActionResult> reliability()
        {
            int type_id = 1;
            AnalyticQueryGenerator qg = new AnalyticQueryGenerator(null);
            return View("rating", await qg.GetRating(type_id));
        }

        public async Task<ActionResult> credit()
        {
            int type_id = 2;
            AnalyticQueryGenerator qg = new AnalyticQueryGenerator(null);
            return View("rating", await qg.GetRating(type_id));
        }


        public async Task<ActionResult> ReviewsSearchAsync(SearchObject so = null)
        {
            AnalyticQueryGenerator qg = new AnalyticQueryGenerator(so);
            return Json(await qg.Search());
        }

        public async Task<ActionResult> Stats(string url, string doc, string author)
        {
            AnalyticQueryGenerator qg = new AnalyticQueryGenerator(null);
            if (await qg.writeStatistics(doc,author))
            {
                string newUrl = url + "?id=" + doc + "&doc_type=2";
                return Redirect(newUrl);
            }
            return null;
        }    

        public async Task<ActionResult> GetTypes()
        {
            AnalyticQueryGenerator qg = new AnalyticQueryGenerator(null);
            return Json(await qg.GetRevTypes());
        }

    }
}