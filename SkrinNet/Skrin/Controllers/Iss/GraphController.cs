using Skrin.BLL.Iss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers.Iss
{
    public class GraphController : Controller
    {
        // GET: Graph
        public async Task<JsonResult> Index(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                return null;
            return Json(await GraphRepository.GetGraphDataAsync(ticker), JsonRequestBehavior.AllowGet);
        }
    }
}