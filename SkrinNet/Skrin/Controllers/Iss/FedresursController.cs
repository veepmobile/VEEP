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
using Skrin.BLL.Root;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Messages;
using Skrin.BLL.Report;
using Skrin.Models;
using Skrin.Models.Fedresurs;

namespace Skrin.Controllers.Iss
{
    public class FedresursController : BaseController
    {
        public async Task<ActionResult> GetMessageTypesAsync(FedresursSearchObject so)
        {
            CompanyData company;
            if (so.iss.Length != 32)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                FedresursQueryGenerator bg = new FedresursQueryGenerator(so, company);
                QueryObject qo = bg.GetQueryTypes(so);
                SphynxSearcher searcher = new SphynxSearcher(qo.SqlQuery, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
                string result = searcher.SearchJson();
                return Content("{" + result + "}");
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessageDatesAsync(FedresursSearchObject so)
        {
            CompanyData company = new CompanyData();
            if (so.iss.Length != 32)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                FedresursQueryGenerator bg = new FedresursQueryGenerator(so, company);
                QueryObject qo = bg.GetQueryDates(so);
                SphynxSearcher searcher = new SphynxSearcher(qo.SqlQuery, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
                string result = searcher.SearchJson();
                return Content("{" + result + "}");
            }
            return Content("");
        }

        public async Task<ActionResult> FedresursSearchAsync(FedresursSearchObject so)
        {
            CompanyData company;
            if (so.iss.Length != 32)
            {
                company = await SqlUtiltes.GetCompanyAsync(so.iss);
            }
            else
            {
                company = await SqlUtiltes.GetIPAsync(so.iss);
            }
            if (company != null)
            {
                FedresursQueryGenerator bg = new FedresursQueryGenerator(so, company);
                QueryObject qo = bg.GetQuerySearch(so);
                SphynxSearcher searcher = new SphynxSearcher(qo.SqlQuery, Configs.SphinxFedresursPort, qo.CharasterSet, Configs.SphinxFedresursServer);
                string result = searcher.SearchJson();
                return Content("{" + result + "}");
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessage(string id, string ticker)
        {
            CompanyData company;
            company = await SqlUtiltes.GetCompanyAsync(ticker);
            if (company != null)
            {
                FedresursMessageItem message = await FedresursRepository.GetFedresursMessage(id, company);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<ActionResult> GetMessagesSelected(string ids, string ticker)
        {
            List<FedresursMessageItem> list = new List<FedresursMessageItem>();
            CompanyData company;
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
                            FedresursMessageItem message = await FedresursRepository.GetFedresursMessage(item, company);
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