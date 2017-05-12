using Skrin.BLL.Authorization;
using Skrin.BLL.Iss;
using Skrin.BLL.Root;
using Skrin.Models.Iss;
using SkrinService.Domain.AddressSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;

namespace Skrin.Controllers.Iss
{
    public class CheckController : BaseController
    {
        // GET: Check
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> Accounts(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return Json(new AccountResult { QStatus = QueueStatus.NoRights }, JsonRequestBehavior.AllowGet);
            }
            string inn;
            var ogrn_inn = await SqlUtiltes.GetOgrnInnAsync(ticker);
            inn = ogrn_inn.Item2;
            if (inn == null)
            {
                return Json(new AccountResult { QStatus = QueueStatus.ErrorInSearch }, JsonRequestBehavior.AllowGet);
            }
            AccountResult retval = new AccountResult();
            var sr = await AccountRepository.GetResultAsync(inn);
            if (!string.IsNullOrEmpty(sr))
            {
                retval.SearchResult= sr;
                retval.IsFinded = 1;
                retval.QStatus = QueueStatus.Ready;
                JsonResult r = Json(retval, JsonRequestBehavior.AllowGet);
                return r;

            }
            else
            {
                await AccountRepository.GetResultAsync(inn);
                retval.IsFinded = 0;
                retval.SearchResult = null;
                retval.QStatus = QueueStatus.InQueue;
                JsonResult r = Json(retval, JsonRequestBehavior.AllowGet);
                return r;
            }
            
            

        }
        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> BadAddress(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return Json(new BadAddressResult { QStatus = QueueStatus.NoRights }, JsonRequestBehavior.AllowGet);
            }

            string ogrn, inn;
            var ogrn_inn = await SqlUtiltes.GetOgrnInnAsync(ticker);
            ogrn = ogrn_inn.Item1;
            inn = ogrn_inn.Item2;
            if(ogrn==null){
                return Json(new BadAddressResult { QStatus = QueueStatus.ErrorInSearch },JsonRequestBehavior.AllowGet);
            }

            BadAddressResult ret = new BadAddressResult();

            string badAddress = await BadAddressRepository.GetAddressAsync(ogrn, inn);
                if (!string.IsNullOrEmpty(badAddress))
                {
                    ret.BadAddress = badAddress;
                    ret.SearchResult = Result.Exist;
                    ret.QStatus = QueueStatus.Ready;
                }
                else
                {
                    await BadAddressRepository.SearchAddressAsync(ogrn, inn);
                    ret.BadAddress = null;
                    ret.SearchResult = Result.NotFound;
                    ret.QStatus = QueueStatus.InQueue;
                }
                return Json(ret, JsonRequestBehavior.AllowGet);
                
        }

        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> DisFind(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return Json(new DisfindSearchResult { ErrorType=2,IsFinded=-1 }, JsonRequestBehavior.AllowGet);
            }

            string ogrn, inn;
            var ogrn_inn = await SqlUtiltes.GetOgrnInnAsync(ticker);
            ogrn = ogrn_inn.Item1;
            inn = ogrn_inn.Item2;
            var sr=await DisFindRepository.GetResultAsync(ogrn, inn);
            sr.SearchOgrn=ogrn;
            return Json(sr, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> DocReg(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            if(us.UserId==0)
            {
                return Json(new DocRegResult { QStatus = QueueStatus.NoRights },JsonRequestBehavior.AllowGet);
            }

            try
            {
                string ogrn = ticker.Length == 15 ? ticker : await SqlUtiltes.GetOgrnAsync(ticker);
                string docReg = await DocRegRepository.GetDocRegAsync(ogrn);
                if (docReg != null)
                {
                    return Json(new DocRegResult
                    {
                        QStatus = QueueStatus.Ready,
                        Result = docReg,
                        Ogrn=ogrn
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new DocRegResult
                    {
                        QStatus = QueueStatus.InQueue,
                        Result = docReg,
                        Ogrn=ogrn
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new DocRegResult
                {
                    QStatus = QueueStatus.InQueue,
                    Result = null
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(CacheProfile = "AjaxCache")]
        public async Task<ActionResult> NalogDebt(string ticker)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return Json(new NalogDebtResult { QStatus = QueueStatus.NoRights }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string inn = ticker.Length == 10 ? ticker : await SqlUtiltes.GetInnAsync(ticker);
                string nalogDebt = await NalogDebtRepository.GetNalogDebtAsync(inn);
                if (!string.IsNullOrEmpty(nalogDebt))
                {
                    return Json(new NalogDebtResult
                    {
                        NalogDebt = nalogDebt,
                        SearchResult = Result.Exist,
                        QStatus = QueueStatus.Ready
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    await NalogDebtRepository.SearchNalogDebtAsync(inn);
                    return Json(new NalogDebtResult
                    {
                        NalogDebt = null,
                        SearchResult = Result.NotFound,
                        QStatus = QueueStatus.InQueue
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new NalogDebtResult
                {
                    NalogDebt = null,
                    SearchResult = Result.NotFound,
                    QStatus = QueueStatus.InQueue
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}