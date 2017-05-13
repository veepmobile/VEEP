using Skrin.BLL.Iss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL;
using Skrin.BLL.Iss;
using Skrin.BLL.Root;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;

namespace Skrin.Controllers.Iss
{
    public class VestnikController : Controller
    {

        public async Task<ActionResult> VestnikSearchAsync(VestnikSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                VestnikQueryGenerator vg = new VestnikQueryGenerator(so, company);
                QueryObject qo = vg.GetQuerySearch(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetTypesAsync(VestnikSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                VestnikQueryGenerator pg = new VestnikQueryGenerator(so, company);
                QueryObject qo = pg.GetQueryTypes(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessageDatesAsync(VestnikSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.isCompany)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                VestnikQueryGenerator pg = new VestnikQueryGenerator(so, company);
                QueryObject qo = pg.GetQueryDates(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessage (string id, string ticker)
        {
            CompanyData company = new CompanyData();
            company = await SqlUtiltes.GetCompanyAsync(ticker);
            if (company != null)
            {
                VestnikMessage message = await VestnikRepository.GetVestnikMessage(id, company);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<ActionResult> GetMessagesSelected (string ids, string ticker)
        {
            List<VestnikMessage> list = new List<VestnikMessage>();
            CompanyData company = new CompanyData();
            company = await SqlUtiltes.GetCompanyAsync(ticker);
            if (company != null)
            {
                if (ids != null)
                {
                    string[] parts = ids.Split(',');
                    foreach (var item in parts)
                    {
                        if (!String.IsNullOrWhiteSpace(item))
                        {
                            VestnikMessage message = await VestnikRepository.GetVestnikMessage(item, company);
                            if (item != null)
                            {
                                list.Add(message);
                            }
                        }
                    }
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }

            return null;
        }
    }
}