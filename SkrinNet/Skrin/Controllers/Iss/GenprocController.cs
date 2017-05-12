using Skrin.BLL.Infrastructure;
using Skrin.BLL.Iss;
using Skrin.BLL.Root;
using Skrin.Models.Iss.Content.GenProc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers.Iss
{
    public class GenprocController : Controller
    {
        // GET: Genproc
        public async Task<ActionResult> Index(string ticker, int? year, int? month, string organ)
        {
            var ogrn_inn=await SqlUtiltes.GetOgrnInnAsync(ticker);
            var name = SqlUtiltes.GetShortName(ticker);

            Filters filters=new Filters{
                SearchName=(!String.IsNullOrEmpty(name))?name:"",
                INN=ogrn_inn.Item2,
                OGRN=ogrn_inn.Item1==null ? "":ogrn_inn.Item1,
                Month=month==null ? 0:month.Value,
                Year=year==null ? DateTime.Now.Year : year.Value,
                Organ=string.IsNullOrEmpty(organ) ? null:organ
            };

            GenprocResultXML result = GenprocRepository.CreateGenprocCheckXML(filters);
            if (!String.IsNullOrEmpty(result.GenprocCheckXML))
            {
                HTMLCreator creator = new HTMLCreator("genproc");
                return Content(creator.GetHtml(string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><iss_profile>{0}</iss_profile>", result.GenprocCheckXML)));
            }
            return Content("");
        }

        public ActionResult Details(string cid, string name, string year, string org)
        {
            int id = !String.IsNullOrEmpty(cid) ? Convert.ToInt32(cid) : 0;
            if (id != 0)
            {
                int cyear = !String.IsNullOrEmpty(year) ? Convert.ToInt32(year) : Convert.ToInt32(DateTime.Now.Year);

                name = !String.IsNullOrEmpty(name) ? name.Replace("%20", " ") : "";
                org = !String.IsNullOrEmpty(org) ? org.Replace("%20", " ") : "";
                CheckDetails details = GenprocRepository.GetDetails(cyear, id, org);
                if (details == null)
                {
                    details = GenprocRepository.LoadDetails(cyear, id, org);
                }
                ViewBag.SearchName = name;
                return View(details);
            }
            return Content("Подробная информация о проверке отсутствует");
        }
    }
}