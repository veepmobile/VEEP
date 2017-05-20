using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using SkrinService.Domain.AddressSearch;
using SkrinService.Domain.AddressSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class MassRegController : Controller
    {
        public JsonResult Index(string id)
        {
            try
            {
                Searcher srch = new Searcher(id, Configs.ConnectionString);
                HomeListEdit hl = srch.MassReg();
                return Json(hl, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [OutputCache(CacheProfile = "AjaxCache")]
        public JsonResult OgrnSearch(string ticker)
        {
            try
            {
                MassRegResult result = new MassRegResult(ticker);
                return Json(result, JsonRequestBehavior.AllowGet);
                //return Json(result,"text/plain",System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                QueueStatus status;
                string errortext = ex.Message;
                if (ex.Message.Substring(0, 3) == "#_#")
                {
                    errortext = errortext.Substring(3);
                    status = QueueStatus.ErrorInAddress;
                }
                else
                {
                    status = QueueStatus.ErrorInSearch;
                }
                MassRegResult result = new MassRegResult(status);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetRegions()
        {
            Kladr result = AddressFinder.GetRegions();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}