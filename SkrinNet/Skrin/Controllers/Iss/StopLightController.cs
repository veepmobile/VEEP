using serch_contra_bll.StopLightFreeEgrul;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Iss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers.Iss
{
    public class StopLightController : BaseController
    {
        private enum Key { CanShow,CanShowStopLight };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static StopLightController()
        {
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }


        public ActionResult Index(string ogrn)
        {
            bool open_company = open_companies_ogrn.Contains(ogrn);
            UserSession us = HttpContext.GetUserSession();
            ViewBag.CanShowStopLight = open_company || us.HasRole(roles, Key.CanShowStopLight.ToString());
            bool open_profile = us.HasRole(roles, Key.CanShow.ToString()) || open_company;

            if (open_profile)
            {

                if (string.IsNullOrEmpty(ogrn))
                    return Content("");
                StopLightData data = new StopLightData(ogrn, Configs.ConnectionString);
                if (data.Rating == ColorRate.NoRating)
                    return Content("");
                int factors_count = 0;
                if (data.Rating == ColorRate.Red)
                    factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
                if (data.Rating == ColorRate.Yellow)
                    factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
                ViewBag.factors_count = factors_count;
                return View(data);
            }
            else
            {
                ViewBag.factors_count = "*";
                return View(new StopLightData()
                {
                    Factors = null,
                    Rating = ColorRate.NoRating
                });
            }
        }


        public ActionResult IndexIP(string ogrn)
        {
            if (string.IsNullOrEmpty(ogrn))
                return Content("");
            StopLightIPData data = new StopLightIPData(ogrn, Configs.ConnectionString);
            if (data.Rating == ColorRate.NoRating)
                return Content("");
            int factors_count = 0;
            if (data.Rating == ColorRate.Red)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
            if (data.Rating == ColorRate.Yellow)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
            ViewBag.factors_count = factors_count;
            return View(data);
        }


        public JsonResult Info(string ogrn)
        {
            if (string.IsNullOrEmpty(ogrn))
            {
                return Json(new { rating = ColorRate.NoRating }, JsonRequestBehavior.AllowGet);
            }
            StopLightData data = new StopLightData(ogrn, Configs.ConnectionString);
            int factors_count = 0;

            if (data.Rating == ColorRate.Red)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
            if (data.Rating == ColorRate.Yellow)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
            return Json(new { rating = data.Rating.ToString(), count = factors_count, red_count = data.FactorCounts[ColorRate.Red], yellow_count = data.FactorCounts[ColorRate.Yellow], green_count = data.FactorCounts[ColorRate.Green] }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InfoIp(string ogrn)
        {
            if (string.IsNullOrEmpty(ogrn))
            {
                return Json(new { rating = ColorRate.NoRating }, JsonRequestBehavior.AllowGet);
            }
            StopLightIPData data = new StopLightIPData(ogrn, Configs.ConnectionString);
            int factors_count = 0;

            if (data.Rating == ColorRate.Red)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
            if (data.Rating == ColorRate.Yellow)
                factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
            return Json(new { rating = data.Rating.ToString(), count = factors_count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowInfo(StopLightData data, StopFactorTypes factor)
        {
            if (StopLightUtilites.HasInfo(data, factor))
            {
                switch (factor)
                {
                    case StopFactorTypes.EgrulExistance:
                        return View("EgrulInfo", data.EgrulExistance_Info);
                    case StopFactorTypes.Legacy1:
                        return View("LegacyInfo", data.Legacy1_Info);
                    case StopFactorTypes.Bankruptcy:
                        return View("BankruptcyInfo", data.Bankruptcy_Info);
                    case StopFactorTypes.CommingStop:
                        return View("CommingStopInfo", data.CommingStop_Info);
                    case StopFactorTypes.StopAction:
                        return View("StopInfo", data.StopAction_Info);
                    case StopFactorTypes.Legacy2:
                        return View("RegEgrulInfo", data.Legacy2_Info);
                    case StopFactorTypes.Disqualification1:
                        return View("DisqualInfo", data.Disqualification1_Info);
                    case StopFactorTypes.Disqualification2:
                        return View("RegEgrulInfo", data.Disqualification2_Info);
                    case StopFactorTypes.AuthorizedReduce:
                        return View("AuthorizedReduceInfo", data.AuthorizedReduce_Info);
                    case StopFactorTypes.CompromiseRecords:
                        return View("RegEgrulInfo", data.CompromiseRecords_Info);
                    case StopFactorTypes.UnfairSupplier:
                        return View("UnFairInfo", StopLightUtilites.GetUnfairTable(data.UnfairSupplier_Info));
                    case StopFactorTypes.Account:
                        return Content("");
                    case StopFactorTypes.Profit:
                        ViewBag.BalanceYear = data.Account_Info.BalanceYear;
                        return View("ProfitInfo", data.Profit_Info);
                    case StopFactorTypes.AssetsCost:
                        ViewBag.BalanceYear = data.Account_Info.BalanceYear;
                        return View("AssetsInfo", data.AssetsCost_Indo);
                    case StopFactorTypes.LegacyYellow:
                        return View("LegacyInfo", data.YellowLegacy_Info);
                    case StopFactorTypes.BankruptcyYellow:
                        return View("BankruptcyInfo", data.BankruptcyYellow_Info);
                    default:
                        return Content("");
                }
            }
            return Content("");
        }

        public ActionResult ShowIPInfo(StopLightIPData data, StopFactorIPTypes factor)
        {
            if (StopLightUtilites.HasIPInfo(data, factor))
            {
                switch (factor)
                {
                    case StopFactorIPTypes.EgripExistance:
                        return View("EgripInfo", data.EgripExistance_Info);
                    case StopFactorIPTypes.Bankruptcy:
                        return View("BankruptcyIPInfo", data.Bankruptcy_Info);
                    case StopFactorIPTypes.Legacy1:
                        return View("LegacyIPInfo", data.Legacy1_Info);
                    case StopFactorIPTypes.StopAction:
                        return View("StopIPInfo", data.StopAction_Info);
                    case StopFactorIPTypes.Legacy2:
                        return View("RegEgripInfo", data.Legacy2_Info);
                    case StopFactorIPTypes.Disqualification2:
                        return View("RegEgripInfo", data.Disqualification_Info);
                    case StopFactorIPTypes.CompromiseRecords:
                        return View("RegEgripInfo", data.CompromiseRecords_Info);
                    case StopFactorIPTypes.UnfairSupplier:
                        return View("UnFairInfo", StopLightUtilites.GetUnfairIpTable(data.UnfairSupplier_Info));
                }
            }
            return Content("");
        }
    }
}