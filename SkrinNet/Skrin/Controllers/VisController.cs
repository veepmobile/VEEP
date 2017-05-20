using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Iss;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using Skrin.BLL.Root;
using Skrin.BLL.Report;
using System.Net;


namespace Skrin.Controllers.vis
{
    public class VisController : BaseController
    {
        private enum Key { CanShow };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static VisController()
        {
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
        }

        // GET: index
        public async Task<ActionResult> Index(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();

            if (!us.HasRole(roles, Key.CanShow.ToString()))
            {
                return new HttpStatusCodeResult(403);
            }
           

            if (string.IsNullOrEmpty(ticker))
                return Content("");


            XSLGenerator g = new XSLGenerator("skrin_content_output..getIssName", new Dictionary<string, object> { { "@iss", ticker } },
                                 "vis_index", new Dictionary<string, object> { { "iss", ticker } });
            return Content(await g.GetResultAsync());
        }
        
        public async Task<ActionResult> GetNodes(string id, int is_first=0, int type=0, string p="", int pt=0)
        {
            UserSession us = HttpContext.GetUserSession();

            if (!us.HasRole(roles, Key.CanShow.ToString()))
            {
                return new HttpStatusCodeResult(403);
            }

            if (pt.ToString().IsNull())
            {
                pt = 0;
            }
            if (p.IsNull())
            {
                p = "";
            }
            XSLGenerator g = new XSLGenerator("skrin_content_output..Tree_json3", new Dictionary<string, object> { { "@iss", id }, {"is_first", is_first},{"@type", type},{"@parent", p},{"@parent_type",pt} }, 
                                 "json_fsns", new Dictionary<string, object> { { "iss", id }, { "isf", is_first} });
            return Content(await g.GetResultAsync());
        }
    }
}