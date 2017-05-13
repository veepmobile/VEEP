using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Skrin.BLL;
using Skrin.BLL.Root;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Authorization;
using Skrin.BLL.Iss;
using Skrin.BLL.Messages;
using Skrin.Models;
using Skrin.Models.ProfileFL;
using Skrin.Models.Bankrot;
using Skrin.Models.Iss.Content;
using FastReport;
using FastReport.Export;
using FastReport.Export.Pdf;
using FastReport.Export.RichText;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.Controllers
{
    public class ProfileFLController : Controller
    {
        private static string _constring = Configs.ConnectionString;
        protected ProfileFLModel Prof = new ProfileFLModel();
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();
        private enum Key { canAccess };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();
        static ProfileFLController()
        {
            roles.Add(Key.canAccess.ToString(), AccessType.Pred | AccessType.KaPlus | AccessType.KaPoln);
        }

        //
        // GET: /ProfileFL/
        public ActionResult Index(string fio, string inn)
        {
            Prof.fio = (string.IsNullOrEmpty(fio)) ? "" : fio.Trim();
            Prof.inn = (string.IsNullOrEmpty(inn) || (inn == "undefined")) ? "" : inn;

            UserSession us = HttpContext.GetUserSession();
            Prof.user_id = us.UserId;
            if ((Prof.user_id > 0) && us.HasRole(roles, Key.canAccess.ToString()))
            {
                Prof.isBancruptcy = CheckBancruptcy(fio, inn);
            }

            return View(Prof);
        }
        public async Task<ActionResult> ProfileMain(string fio, string inn)
        {
            Prof.fio = (string.IsNullOrEmpty(fio)) ? "" : fio.Trim();
            Prof.inn = (string.IsNullOrEmpty(inn) || (inn == "undefined")) ? "" : inn;
            if (Prof.fio.IndexOf('(') >= 0)
            {
                Prof.fio = Prof.fio.Substring(0, Prof.fio.IndexOf('(')).Trim();
            }
            Prof.inn = Prof.inn.Trim();

            UserSession us = HttpContext.GetUserSession();
            Prof.user_id = us.UserId;

            if ((Prof.user_id > 0) && us.HasRole(roles, Key.canAccess.ToString()))
                await GetRealProfile();
            else
                GetFakeProfile();

            if (error_list.Count > 0)
            {
                Helper.SendEmail(string.Join("\n", error_list), "Ошибка в профиле ФЛ");
                return Content("");
            }

            return View(Prof);
        }
        public ActionResult Bankruptcy(string fio, string inn)
        {
            Prof.fio = (string.IsNullOrEmpty(fio)) ? "" : fio.Trim();
            Prof.inn = (string.IsNullOrEmpty(inn) || (inn == "undefined")) ? "" : inn;

            UserSession us = HttpContext.GetUserSession();
            Prof.user_id = us.UserId;

            return View(Prof);
        }
        public ActionResult BancruptcySearch(SearchObject so)
        {
            CompanyData company = GetCompany(so);
            so.iss = "";

            BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
            QueryObject qo = bg.GetQuerySearch(so);
            UnionSphinxClient client = new UnionSphinxClient(qo);
            return Content(client.SearchResult());
        }

        public ActionResult Passports(string fio, string inn)
        {
            Prof.fio = (string.IsNullOrEmpty(fio)) ? "" : fio.Trim();
            Prof.inn = (string.IsNullOrEmpty(inn) || (inn == "undefined")) ? "" : inn;

            UserSession us = HttpContext.GetUserSession();
            Prof.user_id = us.UserId;

            return View(Prof);
        }

        public ActionResult BancruptcyGetMessageDates(SearchObject so)
        {
            CompanyData company = GetCompany(so);
            so.iss = "";

            BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
            QueryObject qo = bg.GetQueryDates(so);
            UnionSphinxClient client = new UnionSphinxClient(qo);
            return Content(client.SearchResult());
        }
        public ActionResult BancruptcyGetMessageTypes(SearchObject so)
        {
            CompanyData company = GetCompany(so);
            so.iss = "";

            BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
            QueryObject qo = bg.GetQueryTypes(so);
            UnionSphinxClient client = new UnionSphinxClient(qo);
            return Content(client.SearchResult());
        }

        public ActionResult BancruptcyGetAllIds(SearchObject so)
        {
            CompanyData company = GetCompany(so);
            so.iss = "";

            BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
            QueryObject qo = bg.GetQueryAllIds(so);
            UnionSphinxClient client = new UnionSphinxClient(qo);
            return Content(client.SearchResult());
        }

        public ActionResult Export(string fio, string inn, string url)
        {
            fio = (string.IsNullOrEmpty(fio)) ? "" : fio.Trim();
            inn = (string.IsNullOrEmpty(inn) || (inn == "undefined")) ? "" : inn.Trim();
            int source = 3;

            UserSession us = HttpContext.GetUserSession();

            if (us.UserId > 0)
            {
                try
                {
                    Report report = new Report();
                    PDFExport export = new PDFExport();

                    report.Load(System.Web.HttpContext.Current.Server.MapPath("/Templates//ProfileFL.frx"));

                    report.SetParameterValue("@fio", fio);
                    report.SetParameterValue("@inn", inn);
                    report.SetParameterValue("@url", url);
                    report.SetParameterValue("@src", source);

                    report.Report.Prepare();

                    Stream stream = new MemoryStream();
                    report.Report.Export(export, stream);
                    report.Dispose();

                    string filename = inn;
                    if (inn != "") filename = filename + "_";
                    filename = filename + "ProfileFL.pdf";
                    stream.Position = 0;
                    return File(stream, "application/pdf", filename);
                }
                catch (Exception ex)
                {
                    Helper.SendEmail(ex.ToString(), "error generate");
                    return new HttpNotFoundResult("Ошибка генерации отчета");
                }
            }
            else
            {
                return new HttpStatusCodeResult(403);
            }
        }

        private CompanyData GetCompany(SearchObject so)
        {
            CompanyData company = new CompanyData();
            company.Ticker = "";
            company.Name = "";
            company.SearchedName = so.iss;
            company.SearchedName2 = "";
            company.IsCompany = false;
            company.Region = 0;
            company.INN = "";
            company.OGRN = "";
            return (company);
        }
        private bool CheckBancruptcy(string fio, string inn)
        {
            CompanyData company = new CompanyData();
            company.Ticker = "";
            company.Name = "";
            company.SearchedName = fio;
            company.SearchedName2 = inn;
            company.IsCompany = false;
            company.Region = 0;
            company.INN = "";
            company.OGRN = "";

            SearchObject so = new SearchObject();
            so.iss = "";
            so.dfrom = "";
            so.dto = "";
            so.kw = "";
            so.type = "";
            so.src = 0;
            so.page = 1;
            so.isCompany = false;
            so.mode = 2;

            BancruptcyQueryGenerator bg = new BancruptcyQueryGenerator(so, company);
            QueryObject qo = bg.GetQuerySearch(so);
            UnionSphinxClient client = new UnionSphinxClient(qo);
            JObject resp = JObject.Parse(client.SearchResult());
            return ((long)resp["total"] > 0);
        }
        #region fake
        private void GetFakeProfile()
        {
            const string REPLACER = "*****";
            const string DATEREPLACER = "**.**.****";

            Prof.ip_inn = new List<IPData>();
            Prof.ip_inn.Add(new IPData
            {
                fio = REPLACER,
                subrf = REPLACER,
                ogrnip = REPLACER,
                inn = REPLACER,
                gd = DATEREPLACER,
                sd = DATEREPLACER
            });

            Prof.ip_fio = new List<IPData>();
            Prof.ip_fio.Add(new IPData
            {
                fio = REPLACER,
                subrf = REPLACER,
                ogrnip = REPLACER,
                inn = REPLACER,
                gd = DATEREPLACER,
                sd = DATEREPLACER
            });

            Prof.founder_inn = new List<FLData>();
            Prof.founder_inn.Add(new FLData
            {
                fio = REPLACER,
                share = REPLACER,
                position = REPLACER,
                gd = DATEREPLACER,
                ul_name = REPLACER,
                ogrn = "",
                ul_status = "",
                ticker = "",
                remark = REPLACER
            });

            Prof.founder_fio = new List<FLData>();
            Prof.founder_fio.Add(new FLData
            {
                fio = REPLACER,
                share = REPLACER,
                position = REPLACER,
                gd = DATEREPLACER,
                ul_name = REPLACER,
                ogrn = "",
                ul_status = "",
                ticker = "",
                remark = REPLACER
            });

            Prof.manager_inn = new List<FLData>();
            Prof.manager_inn.Add(new FLData
            {
                fio = REPLACER,
                share = REPLACER,
                position = REPLACER,
                gd = DATEREPLACER,
                ul_name = REPLACER,
                ogrn = "",
                ul_status = "",
                ticker = "",
                remark = REPLACER
            });

            Prof.manager_fio = new List<FLData>();
            Prof.manager_fio.Add(new FLData
            {
                fio = REPLACER,
                share = REPLACER,
                position = REPLACER,
                gd = DATEREPLACER,
                ul_name = REPLACER,
                ogrn = "",
                ul_status = "",
                ticker = "",
                remark = REPLACER
            });

        }
        #endregion

        #region real
        private async Task GetRealProfile()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(getProfileIP_inn());
            tasks.Add(getProfileIP_fio());
            tasks.Add(getProfileFounder_inn());
            tasks.Add(getProfileFounder_fio());
            tasks.Add(getProfileManager_inn());
            tasks.Add(getProfileManager_fio());

            for (int i = 0, i_max = tasks.Count; i < i_max; i++)
            {
                await tasks[i];
            }
        }

        private async Task getProfileIP_inn()
        {
            Prof.ip_inn = new List<IPData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLIP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 1);
                try
                {
                    await con.OpenAsync();
                    IPData ip;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (await rd.ReadAsync())
                        {
                            ip = new IPData();
                            ip.fio = rd.ReadEmptyIfDbNull("fio");
                            ip.subrf = rd.ReadEmptyIfDbNull("subrf");
                            ip.ogrnip = rd.ReadEmptyIfDbNull("ogrnip");
                            ip.inn = rd.ReadEmptyIfDbNull("inn");
                            ip.gd = rd.ReadEmptyIfDbNull("gd");
                            ip.sd = rd.ReadEmptyIfDbNull("sd");
                            ip.id = rd.ReadEmptyIfDbNull("id");
                            Prof.ip_inn.Add(ip);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLIP: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileIP_fio()
        {
            Prof.ip_fio = new List<IPData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLIP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 2);
                try
                {
                    await con.OpenAsync();
                    IPData ip;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (await rd.ReadAsync())
                        {
                            ip = new IPData();
                            ip.fio = rd.ReadEmptyIfDbNull("fio");
                            ip.subrf = rd.ReadEmptyIfDbNull("subrf");
                            ip.ogrnip = rd.ReadEmptyIfDbNull("ogrnip");
                            ip.inn = rd.ReadEmptyIfDbNull("inn");
                            ip.gd = rd.ReadEmptyIfDbNull("gd");
                            ip.sd = rd.ReadEmptyIfDbNull("sd");
                            ip.id = rd.ReadEmptyIfDbNull("id");
                            Prof.ip_fio.Add(ip);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLIP: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileFounder_inn()
        {
            Prof.founder_inn = new List<FLData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLFounder", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 1);
                try
                {
                    await con.OpenAsync();
                    FLData fl;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (await rd.ReadAsync())
                        {
                            fl = new FLData();
                            fl.fio = rd.ReadEmptyIfDbNull("fio");
                            if (rd.ReadEmptyIfDbNull("inn") != "") fl.fio = fl.fio + ", " + rd.ReadEmptyIfDbNull("inn");
                            fl.share = rd.ReadEmptyIfDbNull("share");
                            fl.share_percent = rd.ReadEmptyIfDbNull("share_percent");
                            fl.position = rd.ReadEmptyIfDbNull("position");
                            fl.gd = rd.ReadEmptyIfDbNull("gd");
                            fl.ul_name = rd.ReadEmptyIfDbNull("ul_name");
                            fl.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            fl.ul_status = rd.ReadEmptyIfDbNull("ul_status");
                            fl.ticker = rd.ReadEmptyIfDbNull("ticker");
                            if (rd.ReadEmptyIfDbNull("position") != "") fl.remark = "в этом же юр.лице " + rd.ReadEmptyIfDbNull("position");
                            else fl.remark = "";
                            Prof.founder_inn.Add(fl);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLFounder: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileFounder_fio()
        {
            Prof.founder_fio = new List<FLData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLFounder", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 2);
                try
                {
                    await con.OpenAsync();
                    FLData fl;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (await rd.ReadAsync())
                        {
                            fl = new FLData();
                            fl.fio = rd.ReadEmptyIfDbNull("fio");
                            if (rd.ReadEmptyIfDbNull("inn") != "") fl.fio = fl.fio + ", " + rd.ReadEmptyIfDbNull("inn");
                            fl.share = rd.ReadEmptyIfDbNull("share");
                            fl.share_percent = rd.ReadEmptyIfDbNull("share_percent");
                            fl.position = rd.ReadEmptyIfDbNull("position");
                            fl.gd = rd.ReadEmptyIfDbNull("gd");
                            fl.ul_name = rd.ReadEmptyIfDbNull("ul_name");
                            fl.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            fl.ul_status = rd.ReadEmptyIfDbNull("ul_status");
                            fl.ticker = rd.ReadEmptyIfDbNull("ticker");
                            if (rd.ReadEmptyIfDbNull("position") != "") fl.remark = "в этом же юр.лице " + rd.ReadEmptyIfDbNull("position");
                            else fl.remark = "";
                            Prof.founder_fio.Add(fl);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLFounder: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileManager_inn()
        {
            Prof.manager_inn = new List<FLData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLManager", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 1);
                try
                {
                    await con.OpenAsync();
                    FLData fl;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            fl = new FLData();
                            fl.fio = rd.ReadEmptyIfDbNull("fio");
                            if (rd.ReadEmptyIfDbNull("inn") != "") fl.fio = fl.fio + ", " + rd.ReadEmptyIfDbNull("inn");
                            fl.share = rd.ReadEmptyIfDbNull("share");
                            fl.share_percent = rd.ReadEmptyIfDbNull("share_percent");
                            fl.position = rd.ReadEmptyIfDbNull("position");
                            fl.gd = rd.ReadEmptyIfDbNull("gd");
                            fl.ul_name = rd.ReadEmptyIfDbNull("ul_name");
                            fl.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            fl.ul_status = rd.ReadEmptyIfDbNull("ul_status");
                            fl.ticker = rd.ReadEmptyIfDbNull("ticker");
                            if (rd.ReadEmptyIfDbNull("share") != "") fl.remark = "в этом же юр.лице учредитель";
                            else fl.remark = "";
                            Prof.manager_inn.Add(fl);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLManager: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileManager_fio()
        {
            Prof.manager_fio = new List<FLData>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileFLManager", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fio", Prof.fio);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                cmd.Parameters.AddWithValue("@st", 2);
                try
                {
                    await con.OpenAsync();
                    FLData fl;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (await rd.ReadAsync())
                        {
                            fl = new FLData();
                            fl.fio = rd.ReadEmptyIfDbNull("fio");
                            if (rd.ReadEmptyIfDbNull("inn") != "") fl.fio = fl.fio + ", " + rd.ReadEmptyIfDbNull("inn");
                            fl.share = rd.ReadEmptyIfDbNull("share");
                            fl.share_percent = rd.ReadEmptyIfDbNull("share_percent");
                            fl.position = rd.ReadEmptyIfDbNull("position");
                            fl.gd = rd.ReadEmptyIfDbNull("gd");
                            fl.ul_name = rd.ReadEmptyIfDbNull("ul_name");
                            fl.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            fl.ul_status = rd.ReadEmptyIfDbNull("ul_status");
                            fl.ticker = rd.ReadEmptyIfDbNull("ticker");
                            if (rd.ReadEmptyIfDbNull("share") != "") fl.remark = "в этом же юр.лице учредитель";
                            else fl.remark = "";
                            Prof.manager_fio.Add(fl);
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileFLManager: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion
    }
}