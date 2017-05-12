using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Skrin.BLL.Root;
using Newtonsoft.Json;

namespace Skrin.Controllers
{
    public class QIVParamController : Controller
    {
        public async Task<JsonResult> GetNodes(int year, ParamType type)
        {
            return Json(await QIVParamRepository.GetParams(year, type), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetNauforNodes(int year, ParamType type)
        {
            return Json(await QIVParamRepository.GetNauforParams(year, type), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetMsfoNodes(ParamType type)
        {
            return Json(await QIVParamRepository.GetMsfoParams(type), JsonRequestBehavior.AllowGet);
        }

        public async Task<int> GetTypeCount(int year)
        {
            return await QIVParamRepository.GetTypeCount(year);
        }

        public async Task<int> GetDefaultYear()
        {
            return await QIVParamRepository.GetDefaultPeriod();
        }

        public async Task<string> GetNauforYear()
        {
            return await QIVParamRepository.GetNauforPeriod();
        }

        public async Task<int> GetMsfoYear()
        {
            return await QIVParamRepository.GetMsfoPeriod();
        }

        public async Task<JsonResult> GetDefaultParam(int period)
        {
            return Json(await QIVParamRepository.GetDefaultParam(period), JsonRequestBehavior.AllowGet);
            //            return Content(await QIVParamRepository.GetDefaults(period));

//            Object Data = JsonConvert.DeserializeObject(await QIVParamRepository.GetDefaults());
//            JsonResult ret = Json(Data, JsonRequestBehavior.AllowGet);
//            JsonResult ret = new JsonResult { Data = JsonConvert.DeserializeObject(await QIVParamRepository.GetDefaults()) };
//            return ret;
        }

        public async Task<JsonResult> GetNauforParam(int period)
        {
            return Json(await QIVParamRepository.GetNauforParam(period), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetMSFOParam(int period)
        {
            return Json(await QIVParamRepository.GetMSFOParam(period), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCodeName(int id, int type_id)
        {
            var code_name = await QIVParamRepository.GetIndicNameAsync(id, type_id);
            return Json(new { code = code_name.Item1, name = code_name.Item2 });
        }

        public async Task<ActionResult> GetCodeNames(int id, int type_id, string tab_name)
        {
            var code_name = await QIVParamRepository.GetCodeNameAsync(id, type_id, tab_name);
            return Json(new { code = code_name.Item1, name = code_name.Item2 });
        }

        public ActionResult QIVParamSelector(int year, string id)
        {
            ViewBag.year = year;
            ViewBag.elem_id = id;
            string view_name = "QIVParamSelector";
            int n = id.Length;
            string qiv_type = id.Remove(n - 1);
            switch (qiv_type)
            {
                case "gks":
                    view_name = "QIVParamSelector";
                    break;
                case "naufor":
                    view_name = "QIVNauforParamSelector";
                    break;
                case "msfo":
                    view_name = "QIVMsfoParamSelector";
                    break;
            }

            return View(view_name);
        }

    }
}