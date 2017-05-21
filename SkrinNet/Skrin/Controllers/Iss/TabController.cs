﻿using Skrin.BLL.Authorization;
using Skrin.BLL.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Iss;
using Skrin.Models.Iss.Debt;
using Skrin.BLL.ISS.Content;
using Skrin.BLL.Iss.Zakupki;

namespace Skrin.Controllers.Iss
{
    public class TabController : BaseController
    {
        public async Task<ActionResult> Index(int id, string ticker, int PG = 1, string period = "0")
        {
            //Список типов доступа разрешенных для данного таба
            string tab_roles = await SqlUtiltes.GetTabAccesses(id);
            if (tab_roles == null)
            {
                return new HttpNotFoundResult("Страницы с данным идентификатором не существует");
            }


            var avail_for_roles = new List<AccessType>();
            foreach (var tr in tab_roles.Split(','))
            {
                AccessType a_type;
                if (Enum.TryParse<AccessType>(tr.Trim(), out a_type))
                {
                    avail_for_roles.Add(a_type);
                }
            }



            //Проверим, доступен ли пользователю данный таб
            bool open_company = open_companies.Contains(ticker.ToLower());

            UserSession us = HttpContext.GetUserSession();

            if (!open_company)
            {

                if (us.UserId == 0)
                    return new HttpStatusCodeResult(403);

                bool access_flag = false;

                foreach (var at in us.User.SitesAccess.Where(p => !p.IsOutOfDate).Select(p => p.AccessType))
                {
                    if (avail_for_roles.Contains(at))
                    {
                        access_flag = true;
                        break;
                    }
                }

                if (!access_flag)
                    return new HttpStatusCodeResult(403);
            }

            string ogrn;
            DealsRepository drep = null;
            string issuer_id;
            var per = "";
            var curr = "1";
            var is_cons = "0";
            var fn = "1,2,3";
            var isXLS = !String.IsNullOrEmpty(this.HttpContext.Request["xls"]) ? this.HttpContext.Request["xls"] : "0";
            switch (id)
            {
                case 2:
                    XSLGenerator g = new XSLGenerator("skrin_net..[GetUpdates2]", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/updates/templ", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 3:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    var model = new RegDataModel(await ContentRepository.GetMaincodes(ogrn), await ContentRepository.GetNames(ogrn), await ContentRepository.GetNameHistory(ogrn), await ContentRepository.GetAddress(ogrn), await ContentRepository.GetAddressHistory(ogrn), await ContentRepository.GetStatus(ogrn), await ContentRepository.GetStoping(ogrn), await ContentRepository.GetRegInfo(ogrn), await ContentRepository.GetRegOldInfo(ogrn), await ContentRepository.GetRegOrgInfo(ogrn), await ContentRepository.GetRecord(ogrn), await ContentRepository.GetPFRegistration(ogrn), await ContentRepository.GetFSSRegistration(ogrn), await ContentRepository.GetOKVEDs(ogrn));
                    ViewBag.Fsns2EgrulDate=await SqlUtiltes.GetFsns2EgrulDateAsync(ogrn);
                    ViewBag.Ogrn = ogrn;
                    return View("EgrulRegData", model);
                case 4:
                    XSLGenerator g4 = new XSLGenerator("skrin_content_output..RegData_GKS", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/reg_date/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g4.GetResultAsync());
                case 5:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulCapital", new CapitalModel(await ContentRepository.GetCapital(ogrn), await ContentRepository.GetReduce(ogrn)));
                case 6:
                    g = new XSLGenerator("skrin_content_output..action_skrin", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/state_capital/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 7:
                    g = new XSLGenerator("skrin_content_output..actions_GKS", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/state_capital/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 8:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("CBCapital", new CapitalModel(await ContentRepository.GetCBCapital(ogrn), null));
                case 9:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulReorg", new ReorgModel(await ContentRepository.GetReorg(ogrn), await ContentRepository.GetPredecessors(ogrn), await ContentRepository.GetSuccessors(ogrn)));
                case 10:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulLicense", new LicenseModel(await ContentRepository.GetLicense(ogrn)));
                case 11:
                    issuer_id = await SqlUtiltes.GetIssuerIdAsync(ticker);
                    return View("NRARating", await SqlUtiltes.GetNRARatingsAsync(issuer_id));
                case 12:
                    g = new XSLGenerator("skrin_content_output..getIssuerEmployees", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/work/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 13:
                    drep = new DealsRepository(ticker);
                    return View("DealInfo", await drep.GetDealInfoAsync(DealInfoTypes.Main, period));
                case 14:
                    drep = new DealsRepository(ticker);
                    return View("DealInfo", await drep.GetDealInfoAsync(DealInfoTypes.Plans, period));
                case 15:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulManagement", new ManagementModel(await ContentRepository.GetManagers(ogrn), await ContentRepository.GetManagersHistory(ogrn), await ContentRepository.GetManagementCompanies(ogrn), await ContentRepository.GetManagementForeignCompanies(ogrn), await ContentRepository.GetManagementForeignCompanyFilials(ogrn), await ContentRepository.GetManagementForeignCompanyFilialsManager(ogrn)));
                case 16:
                    g = new XSLGenerator("skrin_content_output..getIspOrgGMC", new Dictionary<string, object> { { "@iss", ticker }, { "@period", period } }, "tab_content/isporg/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 17:
                    g = new XSLGenerator("skrin_content_output..getRulers_skrin", new Dictionary<string, object> { { "@iss", ticker }, { "@period", period } }, "tab_content/isporg/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 20:
                    g = new XSLGenerator("skrin_content_output..getRulersColleg_skrin", new Dictionary<string, object> { { "@iss", ticker }, { "@period", period } }, "tab_content/isporg/skrin_colleg", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 21:
                    g = new XSLGenerator("skrin_content_output..getRulers", new Dictionary<string, object> { { "@iss", ticker }, { "@period", period }, { "@data_type", "0" } }, "tab_content/sovdir/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 22:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulConstitutors", new ConstitutorsModel(await ContentRepository.GetConstitutors(ogrn), await ContentRepository.GetConstitutorsHistory(ogrn)));
                case 23:
                    g = new XSLGenerator("skrin_content_output..getParticipants", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "0" }, { "@period", period } }, "tab_content/members/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 24:
                    g = new XSLGenerator("skrin_content_output..getParticipants", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "1" }, { "@period", period } }, "tab_content/members/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 25:
                    g = new XSLGenerator("skrin_content_output..getDependants", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "0" }, { "@period", period } }, "tab_content/depend/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 26:
                    g = new XSLGenerator("skrin_content_output..getDependants", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "1" }, { "@period", period } }, "tab_content/depend/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 27:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }
                    return View("EgrulBranches", new BranchesModel(await ContentRepository.GetEgrulBranches(ogrn), await ContentRepository.GetEgrulOutputs(ogrn)));
                case 28:
                    g = new XSLGenerator("skrin_content_output..getBranches", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "0" }, { "@period", period } }, "tab_content/filials/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 29:
                    g = new XSLGenerator("skrin_content_output..getBranches", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "1" }, { "@period", period } }, "tab_content/filials/gmc", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 30:
                    g = new XSLGenerator("skrin_content_output..getBranches", new Dictionary<string, object> { { "@iss", ticker }, { "@src", "2" }, { "@period", period } }, "tab_content/filials/cb", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 31:
                    return View("ProfileLinks", await (new ProfileLinksRepository(ticker, us.UserId)).GetProfileLinksAsync());
                case 32:
                    return View("DocumentList", await (new DocumentListRepository(ticker, 5)).GetDocumentListAsync());
                case 33:
                    return View("DocumentList", await (new DocumentListRepository(ticker, -1005)).GetDocumentListAsync());
                case 35:
                    return View("Bancruptcy", new BancruptcyModel(await ContentRepository.GetBancruptcy(ticker)));
                case 36:
                    return View("Vestnik", new VestnikModel(await ContentRepository.GetVestnik(ticker)));
                case 37:
                    return View("Fedresurs", new FedresursModel(await ContentRepository.GetFedresurs(ticker)));
                case 38:
                    issuer_id = await SqlUtiltes.GetIssuerIdAsync(ticker);
                    return View("Bargains", new BargainsModel(issuer_id));
                case 39:
                    ViewBag.issuer_id = await SqlUtiltes.GetIssuerIdAsync(ticker);
                    return View("Events");
                case 40:
                    return View("GenProc");
                case 41:
                    return View("Pravo", new PravoModel(await ContentRepository.GetPravo(ticker)));
                case 42:
                    return View("Debt");
                case 48:
                    return View("Zakupki", (object)await SqlUtiltes.GetInnAsync(ticker));
                case 49:
                    var pr = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    var fo_no = !String.IsNullOrEmpty(this.HttpContext.Request.QueryString["fo_no"]) ? this.HttpContext.Request["fo_no"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "0";
                    if (isXLS == "0")
                    {
                        return Content(await FinparamsRepository.getRsbuXml(ticker, pr, fo_no, curr, fn, isXLS));
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await FinparamsRepository.getRsbuXml(ticker, pr, fo_no, curr, fn, isXLS));
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_fp_rsbu.xlsx");
                    }
                case 50:
                    pr = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    g = new XSLGenerator("skrin_content_output..finparams_msfo", new Dictionary<string, object> { { "@iss", ticker }, { "@periods", pr }, { "@form_no", "1,2" }, { "@curr", curr } }, "tab_content/finparams/msfo/" + (isXLS == "1" ? "xlsjson" : "templ"),
                    new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_fp_msfo.xlsx");
                    }
                case 51:
                    pr = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    g = new XSLGenerator("skrin_content_output..finparams_coeff2", new Dictionary<string, object> { { "@iss", ticker }, { "@periods", pr } }, "tab_content/finparams/koeff/" + (isXLS == "1" ? "xlsjson" : "templ"),
                    new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_koeff.xlsx");
                    }
                case 52:
                    return Content("Информация отсутствует");
                case 53:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "1,2,3";
                    is_cons = !String.IsNullOrEmpty(this.HttpContext.Request["cons"]) ? this.HttpContext.Request["cons"] : "0";
                    g = new XSLGenerator("skrin_content_output..rsbu_naufor2",
                        new Dictionary<string, object> { { "@iss", ticker }, 
                                                          { "@periods", per}, 
                                                          { "@form_no",fn},
                                                          { "@curr",curr},
                                                          { "@ext_data",is_cons}},
                                                       "tab_content/rsbu/skrin/" + (isXLS == "1" ? "xlsjson" : "templ"),
                        new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_org.xlsx");
                    }
                case 54:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "1,2";

                    g = new XSLGenerator("skrin_content_output..rsbu_gks3",
 new Dictionary<string, object> { { "@iss", ticker }, 
                                                          { "@periods", per}, 
                                                          { "@form_no",fn},
                                                          { "@curr",curr}
                           },
                                "tab_content/rsbu/gmc/" + (isXLS == "1" ? "xlsjson" : "templ"),
 new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });

                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_gmc.xlsx");
                    }

                case 55:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "1";

                    g = new XSLGenerator("skrin_content_output..rsbu_bank",
                                           new Dictionary<string, object> { { "@iss", ticker }, 
                                                          { "@periods", per}, 
                                                          { "@form_no",fn},
                                                          { "@curr",curr}
                           },
                                "tab_content/rsbu/cb/" + (isXLS == "1" ? "xlsjson" : "templ"),
                                 new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });

                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_" + ((fn == "1") ? "101" : "102") + "_cb.xlsx");
                    }
                case 56:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "1";

                    g = new XSLGenerator("skrin_content_output..rsbu_bank_cb_3",
                                           new Dictionary<string, object> { { "@iss", ticker }, 
                                                          { "@periods", per}, 
                                                          { "@form_no",fn},
                                                          { "@curr",curr}
                           },
                                "tab_content/rsbu/cby/" + (isXLS == "1" ? "xlsjson" : "templ"),
                                new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_cby" + ((fn == "2") ? "_3_4_5_6" : "_1_2") + ".xlsx");
                    }
                case 57:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    curr = !String.IsNullOrEmpty(this.HttpContext.Request["curr"]) ? this.HttpContext.Request["curr"] : "1";
                    fn = !String.IsNullOrEmpty(this.HttpContext.Request["fn"]) ? this.HttpContext.Request["fn"] : "1";

                    g = new XSLGenerator("skrin_content_output..rsbu_insurer",
                                            new Dictionary<string, object> { { "@iss", ticker }, 
                                                          { "@periods", per}, 
                                                          { "@form_grp",fn},
                                                          { "@curr",curr}
                           },
                                "tab_content/rsbu/insurer/" + (isXLS == "1" ? "xlsjson" : "templ"),
                                   new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });

                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_insurer_" + ((fn == "2") ? "_3_4_5_6" : "_1_2") + ".xlsx");
                    }
                case 58:
                    return View("ReportList", await (new ReportRepository(ticker, ReportType.BuhReports)).GetReportListAsync());
                case 59:
                    per = !String.IsNullOrEmpty(this.HttpContext.Request["per"]) ? this.HttpContext.Request["per"] : "";
                    g = new XSLGenerator("skrin_content_output..msfo_naufor", new Dictionary<string, object> { { "@iss", ticker }, { "@periods", per } }, "tab_content/msfo/" + (isXLS == "1" ? "xlsjson" : "skrin"),
                        new Dictionary<string, object> { { "iss", ticker }, { "tab", id }, { "id", id }, { "PDF", -1 } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_rsbu_insurer_" + ((fn == "2") ? "_3_4_5_6" : "_1_2") + ".xlsx");
                    }
                case 60:
                    return View("ReportList", await (new ReportRepository(ticker, ReportType.MSFO_Reports)).GetReportListAsync());

                case 61:
                    g = new XSLGenerator("skrin_content_output..auditors_ns", new Dictionary<string, object> { { "@iss", ticker }, { "@type", 0 } }, "tab_content/auditors/templ", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 62:
                    g = new XSLGenerator("skrin_content_output..auditors_ns", new Dictionary<string, object> { { "@iss", ticker }, { "@type", 1 } }, "tab_content/auditors/templ", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 63:
                    return View("ReportList", await (new ReportRepository(ticker, ReportType.AnnualReports)).GetReportListAsync());
                case 64:
                    return View("ReportList", await (new ReportRepository(ticker, ReportType.QuartReports)).GetReportListAsync());
                case 65:
                    return View("ReportList", await (new ReportRepository(ticker, ReportType.QuartReportsFSFR)).GetReportListAsync());
                case 66:
                    DocumentListRepository dlr = new DocumentListRepository(ticker, 0);
                    return View("DocumentList", await dlr.GetDocumentListAsync());
                case 67:
                    return View("DocumentList", await (new DocumentListRepository(ticker, 100)).GetDocumentListAsync());
                case 68:
                    return View("DocumentList", await (new DocumentListRepository(ticker, -1100)).GetDocumentListAsync());
                case 70:
                    g = new XSLGenerator("skrin_content_output..actions", new Dictionary<string, object> { { "@iss", ticker } }, "tab_content/actions/skrin/" + (isXLS == "1" ? "xlsjson" : "skrin"), new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "tab", id }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_actions_org" + ".xlsx");
                    }
                case 71:
                    g = new XSLGenerator("skrin_content_output..bonds", new Dictionary<string, object> { { "@iss", ticker }, { "@Det", 0 } }, "tab_content/bonds/skrin/" + (isXLS == "1" ? "xlsjson" : "templ"), new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "tab", id }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_bonds_org" + ".xlsx");
                    }
                case 72:
                    return Content("");
                case 73:
                    return Content("");
                case 74:
                    /*g = new XSLGenerator("skrin_content_output..bonds", new Dictionary<string, object> { { "@iss", ticker }, { "@Det", 0 } }, "tab_content/bonds/skrin/templ", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());*/
                    g = new XSLGenerator("skrin_content_output..bonds", new Dictionary<string, object> { { "@iss", ticker }, { "@Det", 0 } }, "tab_content/bonds/skrin/" + (isXLS == "1" ? "xlsjson" : "templ"), new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "tab", id }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_bonds_org" + ".xlsx");
                    }
                case 77:
                    g = new XSLGenerator("skrin_content_output..getADRs", new Dictionary<string, object> { { "@iss", ticker }, { "@is_details", 0 } }, "tab_content/adr/" + (isXLS == "1" ? "xlsjson" : "templ"), new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "tab", id }, { "id", id } });
                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_bonds_org" + ".xlsx");
                    }
                case 78:
                    ogrn = await SqlUtiltes.GetOgrnAsync(ticker);
                    return View("HorisontalTable", SkrinContentRepository.GetRegister(ogrn));
                case 79:
                    g = new XSLGenerator("skrin_content_output..register", new Dictionary<string, object> { { "@iss", ticker }, { "@mode", 0 } }, "tab_content/registr/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 80:
                    g = new XSLGenerator("skrin_content_output..register", new Dictionary<string, object> { { "@iss", ticker }, { "@mode", 1 } }, "tab_content/registr/dkk", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 81:
                    g = new XSLGenerator("skrin_content_output..dividends", new Dictionary<string, object> { { "@iss_id", ticker }, { "@is_bonds", 0 }, { "@tristate_content", 1 } }, "tab_content/dividends/skrin/" + (isXLS == "1" ? "xlsjson" : "skrin"), new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 }, { "tab", id }, { "id", id } });

                    if (isXLS == "0")
                    {
                        return Content(await g.GetResultAsync());
                    }
                    else
                    {
                        XLSBuilder x = new XLSBuilder(await g.GetResultAsync());
                        return File(x.MakeXLS(), "application/xlsx", ticker + "_bonds_org" + ".xlsx");
                    }
                case 83:
                    g = new XSLGenerator("skrin_content_output..yield", new Dictionary<string, object> { { "@iss", ticker }, { "@oper", 0 } }, "tab_content/yield/skrin", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(await g.GetResultAsync());
                case 86:
                    return View("Issue_DocsView", await (new Issue_DocsRepository(ticker)).GetIssue_DocsAsync());
                case 87:
                    return View("DocumentList", await (new DocumentListRepository(ticker, -1006)).GetDocumentListAsync());
                case 89:
                    return View("Trade", await TradeRepository.GetTradesIntitValuesAsync(ticker));
                case 90:
                    g = new XSLGenerator("skrin_content_output..getIssuerReviews", new Dictionary<string, object> { { "@iss", ticker }, { "@PG", PG } }, "tab_content/reviews", new Dictionary<string, object> { { "iss", ticker }, { "PDF", -1 } });
                    return Content(g.GetChangedResult(AdditionalRepository.GetReviewsTable(await g.GetXmlAsync(), ticker)));
                case 91:
                    return View("ProfileMultilinks", await (new ProfileMultilinksRepository(ticker, us.UserId)).GetProfileMultilinksAsync());
                default:
                    return new HttpNotFoundResult("Данная страница не реализована");
            }

        }


    }
}

/*
 
 * 1 - Профиль/Профиль
2 - Обновления/Обновления
3 - Регистрационные данные/Данные ЕГРЮЛ
4 - Регистрационные данные/Данные ГМЦ Росстата
5 - Размер уставного капитала/Данные ЕГРЮЛ
6 - Размер уставного капитала/Данные организации
7 - Размер уставного капитала/Данные ГМЦ Росстата
8 - Размер уставного капитала/Данные ЦБ РОССИИ
9 - Реорганизация/Данные ЕГРЮЛ
10 - Лицензии/Данные ЕГРЮЛ
11 - Кредитные рейтинги/Данные НРА
12 - Сотрудники/Данные организации
13 - Основная деятельность/Основная хозяйственная деятельность
14 - Основная деятельность/Планы будущей деятельности
15 - Лицо, имеющие право действовать без доверенности/Данные ЕГРЮЛ
16 - Лицо, имеющие право действовать без доверенности/Данные ГМЦ Росстата
17 - Лицо, имеющие право действовать без доверенности/Данные организации
--18 - Исполнительный орган/Данные ЕГРЮЛ
--19 - Исполнительный орган/Данные ГМЦ Росстата
20 - Исполнительный орган/Данные организации
21 - Совет директоров/Данные организации
22 - Учредители (участники)/Данные ЕГРЮЛ
23 - Учредители (участники)/Данные организации
24 - Учредители (участники)/Данные ГМЦ Росстата
25 - Дочерние и зависимые общества/Данные организации
26 - Дочерние и зависимые общества/Данные ГМЦ Росстата
27 - Филиалы и представительства/Данные ЕГРЮЛ
28 - Филиалы и представительства/Данные организации
29 - Филиалы и представительства/Данные ГМЦ Росстата
30 - Филиалы и представительства/Данные ЦБ РОССИИ
31 - Взаимосвязанные лица/Связи юридического лица
32 - Списки аффилированных лиц/Документы организации
33 - Списки аффилированных лиц/Документы ФСФР
34 - Списки аффилированных лиц/Данные организации
35 - Сообщения о банкротстве/Данные Коммерсантъ/Российской газеты/ЕФРСБ
36 - Вестник госрегистрации/Данные Вестника государственной регистрации
37 - Сведения о фактах деятельности/Данные ЕФРСФДЮЛ
38 - Сообщения и существенные факты/Существенные факты
39 - Календарь корпоративных событий/Корпоративные события
40 - Сводный план проверок/Сводный план проверок
41 - Картотека арбитражных дел/Картотека арбитражных дел
42 - Исполнительные производства/Исполнительные производства
--43 - Адреса массовой регистрации/Адреса массовой регистрации
--44 - Отсутствие ЮЛ по адресу, указанному в ЕГРЮЛ/Отсутствие ЮЛ по адресу, указанному в ЕГРЮЛ
--45 - Поиск ЮЛ по адресу/Поиск ЮЛ по адресу
--46 - Наличие дисквалифицированных лиц в исполнительном органе/Наличие дисквалифицированных лиц в исполнительном органе
47 - Сведения о представлении ИП заявлений для гос.регистрации в ФНС/Сведения о представлении ИП заявлений для гос.регистрации в ФНС
48 - Государственные контракты/Данные федерального казначейства
49 - Основные финансовые показатели/Финансовые показатели РСБУ
50 - Основные финансовые показатели/Финансовые показатели МСФО
51 - Основные финансовые показатели/Коэффициенты
52 - Основные финансовые показатели/Чистые активы
53 - Бухгалтерская отчетность по РСБУ/Данные организации
54 - Бухгалтерская отчетность по РСБУ/Данные ГМЦ Росстата
55 - Бухгалтерская отчетность по РСБУ/Данные Банка РОССИИ  (Форма 101 и 102) 
56 - Бухгалтерская отчетность по РСБУ/Данные Банка РОССИИ ( Годовая отчетность)
57 - Бухгалтерская отчетность по РСБУ/Данные Банка РОССИИ ( Годовая отчетность)
58 - Бухгалтерская отчетность по РСБУ/Документы организации
59 - Финансовая отчетность по МСФО/Данные организации
60 - Финансовая отчетность по МСФО/Документы организации
61 - Аудиторы/РСБУ
62 - Аудиторы/МСФО
63 - Годовые отчеты/Документы организации
64 - Квартальные отчеты/Документы организации
65 - Квартальные отчеты/Документы ФСФР
66 - Учредительные и внутренние документы/Документы организации
67 - Прочие документы/Документы организации
68 - Прочие документы/Документы ФСФР
69 - Прочие документы/Документы Бюджетные
70 - Акции/Данные организации
71 - Акции/Данные ЦБ РОССИИ
72 - Акции/Данные НРД
73 - Акции/Данные ДКК
74 - Облигации/Данные организации
75 - Облигации/Данные НРД
76 - Облигации/Данные ДКК
77 - Депозитарные расписки/Данные организации
78 - Регистратор/Данные ЕГРЮЛ
79 - Регистратор/Данные организации
80 - Регистратор/Данные ДКК
81 - Доход по акциям/Данные организации
82 - Доход по акциям/Данные ДКК
83 - Выплаты по облигациям/Данные организации
84 - Выплаты по облигациям/Данные ДКК
85 - Выплаты по облигациям/Данные НРД
86 - Эмиссионные документы/Документы организации
87 - Эмиссионные документы/Документы ФСФР
88 - Эмиссионные документы/Права владельцев
89 - Итоги торгов/Итоги торгов
90 - Аналитические обзоры/Аналитические обзоры
91 - Взаимосвязи по списку/Взаимосвязи по списку
         
 */