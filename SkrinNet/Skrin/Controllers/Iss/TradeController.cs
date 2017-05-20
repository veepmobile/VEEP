using Newtonsoft.Json;
using Skrin.BLL.Iss;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System.IO;
using Skrin.BLL.Root;


namespace Skrin.Controllers.Iss
{
    public class TradeController : BaseController
    {
        private enum Key { canAccess };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        static TradeController()
        {
            roles.Add(Key.canAccess.ToString(), AccessType.Pred | AccessType.Emitent | AccessType.KaPoln);
        }

        // GET: Trade
        public async Task<TradeInfo> InnerSearch(TradeSearchObject so)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canAccess.ToString()))
            {
                return null;
            }

            so.exchange_list = so.exchange_list ?? "";
            so.issues_list = so.issues_list ?? "";
            List<Exchange> exchange_list = so.exchange_list.Split(',').Select(p => (Exchange)int.Parse(p)).ToList();
            List<IssueType> issues_list = so.issues_list.Split(',').Select(p => (IssueType)int.Parse(p)).ToList();
            DateTime? sDate = null;
            try
            {
                sDate = DateTime.ParseExact(so.sDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }

            DateTime? tDate = null;
            try
            {
                tDate = DateTime.ParseExact(so.tDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
            return await TradeRepository.GetTradeInfoAsync(so.ticker, sDate, tDate, (Currency)so.currency, exchange_list, issues_list);
        }

        // GET: Trade
        public async Task<ActionResult> Search(TradeSearchObject so)
        {
            TradeInfo ti=await InnerSearch(so);
            return Content(JsonConvert.SerializeObject(ti));
        }

        public async Task<ActionResult> FullView(TradeSearchObject so)
        {
            TradeInfo ti = await InnerSearch(so);
            ViewBag.CompanyName = SqlUtiltes.GetShortName(so.ticker); 
            return View(ti);
        }

        public async Task<ActionResult> DoSearchToExcel(string string_params)
        {
            UserSession us = HttpContext.GetUserSession();
            int user_id = us.UserId;
            if (!us.HasRole(roles, Key.canAccess.ToString()))
            {
                return null;
            }

            TradeSearchObject so = JsonConvert.DeserializeObject<TradeSearchObject>(string_params);
            TradeInfo ti = await InnerSearch(so);

            var excel_result = TradeRepository.TradeInfoToExcel(ti);
            var fs = new MemoryStream();
            ExcelGenerator.SimpleExcel(excel_result).SaveAs(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return File(fs, "application/vnd.ms-excel", "trade_result.xlsx");
        }
 

    }
}