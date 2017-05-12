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
using Skrin.BLL.Report;

namespace Skrin.Controllers.Iss
{
    public class BancruptcyController :  BaseController
    {

        public async Task<ActionResult> GetMessageTypesAsync(SearchObject so)
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
                BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
                QueryObject qo = bg.GetQueryTypes(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessageDatesAsync(SearchObject so)
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
                BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
                QueryObject qo = bg.GetQueryDates(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }


        public async Task<ActionResult> GetAllIds(SearchObject so)
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
                BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
                QueryObject qo = bg.GetQueryAllIds(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> BancruptcySearchAsync(SearchObject so)
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
                BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
                QueryObject qo = bg.GetQuerySearch(so);
                UnionSphinxClient client = new UnionSphinxClient(qo);
                return Content(client.SearchResult());
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessage(string ids, string ticker, int src)
        {
            if (ids != null)
            {
                if (src == 1 || src == -1)
                {
                    XSLGenerator g = new XSLGenerator("skrin_content_output..getBancruptcyEFRSB", new Dictionary<string, object> { { "@ids", ids } }, "tab_content/bancruptcy/messEFRSB", new Dictionary<string, object> { { "ids", ids } });
                    return Content(await g.GetResultAsync());
                }
                else
                {
                    return View("BancruptcyMessage", await ContentRepository.GetBancruptcyMessage(ids, ticker));
                }
            }
            return Content("");
        }

        public async Task<ActionResult> GetMessagesSelected(string ids, string ticker)
        {

            string result = "<div id=\"t_content\"><style>#protocol_box .profile_table {float: none;}</style><div id=\"protocol_box\"><h1>" + SqlUtiltes.GetShortName(ticker) + "</h1><h2>Сообщение о банкротстве</h2><br/><div style=\"width: 951px;\" class=\"tline\"></div>";
            if (ids != null)
            {
                string[] parts = ids.Split(',');
                foreach (var item in parts)
                {
                    if(!String.IsNullOrWhiteSpace(item))
                    {
                        string[] num = item.Split(':');
                        MessagesIds id = new MessagesIds { Ids = num[0], Source = Convert.ToInt32(num[1]) };
                        if (id.Source == 1 || id.Source == -1)
                        {
                            XSLGenerator g = new XSLGenerator("skrin_content_output..getBancruptcyEFRSB", new Dictionary<string, object> { { "@ids", id.Ids } }, "tab_content/bancruptcy/messagesEFRSB", new Dictionary<string, object> { { "ids", id.Ids } });
                            result += await g.GetResultAsync();
                        }
                        else
                        {
                            result += await ContentRepository.GetMessages(id.Ids);
                        }
                    }
                }
                result += "<div class=\"explain\" style=\"clear:both;padding-top:10px; \">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div></div></div>";
            }
            return Content(result);
        }

    }
}