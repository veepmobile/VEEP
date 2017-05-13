using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;
using Skrin.Models;
using Skrin.Models.ProfileIP;
using Skrin.BLL.Infrastructure;
using Skrin.BLL;
using System.Threading.Tasks;

namespace Skrin.BLL.IP
{
    public class ProfileIpRepository
    {

        private readonly string _ticker;
        private static readonly string _constring = Configs.ConnectionString;
        private static readonly bool _need_watching = Configs.NeedWatchingTimout;
        private ProfileIP Prof = new ProfileIP();
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();
        int i_bank1 = 0;
        int i_bank2 = 0;
        int i_pravo1 = 0;
        const string REPLACER = "*****";
        const string DATEREPLACER = "**.**.****";


        public ProfileIpRepository(string ticker)
        {
            _ticker = ticker;
        }


        #region close


        public ProfileIP GetClosedProfile()
        {
            Prof.ogrnip = _ticker;
            GetFakeProfile();

            return Prof;
        }

        private void GetMainFakeData()
        {
            Stopwatch stopwatch = new Stopwatch();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_Free", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = Prof.ogrnip;
                try
                {
                    con.Open();
                    stopwatch.Start();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {

                            /*Сейчас в профиле доступно ФИО, регион, ОГРНИП. Нужно что бы ОКПО стал также доступен не авторизованным.*/
                            Prof.id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            Prof.ogrnip = (rd["ogrnip"] != DBNull.Value) ? (string)rd["ogrnip"] : "";
                            Prof.rd = DATEREPLACER;
                            Prof.inn = REPLACER;
                            Prof.fio = (rd["fio"] != DBNull.Value) ? (string)rd["fio"] : "";
                            Prof.fio = Prof.fio.Replace("+", " ").Replace(",", " ").Replace(".", " ").Replace(";", " ").Replace("[", " ").Replace("]", " ");
                            Prof.typeip_name = REPLACER;
                            Prof.citizenship_name = REPLACER;
                            Prof.regorg_address = REPLACER;
                            Prof.nalog_name = REPLACER;
                            Prof.nalog_record_date = DATEREPLACER;
                            Prof.regorg_address = REPLACER;
                            Prof.email = REPLACER;
                            Prof.okpo = (rd["okpo"] != DBNull.Value) ? (string)rd["okpo"] : "Нет данных";
                            Prof.okato = REPLACER;
                            Prof.okato_name = REPLACER;
                            Prof.oktmo = REPLACER;
                            Prof.gmc_status = REPLACER;
                            Prof.updt = DATEREPLACER;
                            Prof.issuer_id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                        }
                    }
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    //Prof.timeouts.Add("getProfileIP2_Free", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private void GetFakeProfile()
        {
            // GetMainFakeData();
            _ActionWrapper(GetMainFakeData);

            if (Prof.ogrnip == null)
                return;

            #region okveds

            Prof.okveds = new List<OkvedCls>();
            Prof.okveds.Add(new OkvedCls(REPLACER, REPLACER, 1));
            Prof.okveds.Add(new OkvedCls(REPLACER, REPLACER, 0));

            #endregion

            Prof.egrip = new EgripData2 { egrip_date = DATEREPLACER, egrip_istoday = true, status = REPLACER, status_date = DATEREPLACER };
            Prof.egrip_is_black = false;
            Prof.egrip_links = new EgripLinksData2 { egrip_date = DATEREPLACER, id = "", is_today = true };
            Prof.EGRIPPDF = new List<EGRIPPDFCls>();
            Prof.zak_data = new ZakupkiData
            {
                type = 6,
                customer_cnt = long.MinValue,
                customer_sum = decimal.MinValue,
                supplier_cnt = long.MinValue,
                supplier_sum = decimal.MinValue,
                participiant_cnt = long.MinValue,
                participiant_sum = decimal.MinValue
            };
            Prof.unf_data = new UnfairData { rec_id = "", start_date = DATEREPLACER };
            Prof.pravo_data = new PravoData
            {
                applicant_cnt = long.MinValue,
                applicant_sum = decimal.MinValue,
                defendant_cnt = long.MinValue,
                defendant_sum = decimal.MinValue
            };
            Prof.is_bancruptcy = true;
            Prof.is_pravo_sphinx = true;

            Action[] actions = new Action[]{              
                getProfileIP_Region,
                getProfileGoroda         
            };

            List<Action> wrapped_actions = new List<Action>();

            foreach (var action in actions)
            {
                wrapped_actions.Add(() =>
                {
                    _ActionWrapper(action);
                });
            }

            Parallel.Invoke(wrapped_actions.ToArray());

        }

        #endregion

        #region open

        private void GetMainData()
        {
            Stopwatch stopwatch = new Stopwatch();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = Prof.ogrnip;
                try
                {
                    con.Open();
                    stopwatch.Start();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {
                            //Prof.okato = rd.ReadEmptyIfDbNull("okato");
                            //Prof.id = (string)rd["id"];
                            Prof.id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            Prof.ogrnip = (rd["ogrnip"] != DBNull.Value) ? (string)rd["ogrnip"] : "";
                            Prof.rd = (rd["rd"] != DBNull.Value) ? (string)rd["rd"] : "";
                            Prof.inn = (rd["inn"] != DBNull.Value) ? (string)rd["inn"] : "";
                            Prof.fio = (rd["fio"] != DBNull.Value) ? (string)rd["fio"] : "";
                            Prof.fio = Prof.fio.Replace("+", " ").Replace(",", " ").Replace(".", " ").Replace(";", " ").Replace("[", " ").Replace("]", " ");
                            Prof.typeip_name = (rd["typeip_name"] != DBNull.Value) ? (string)rd["typeip_name"] : "";
                            Prof.citizenship_name = (rd["citizenship_name"] != DBNull.Value) ? (string)rd["citizenship_name"] : "";
                            Prof.regorg_address = (rd["regorg_address"] != DBNull.Value) ? (string)rd["regorg_address"] : "";
                            Prof.nalog_name = (rd["nalog_name"] != DBNull.Value) ? (string)rd["nalog_name"] : "";
                            Prof.nalog_record_date = (rd["nalog_record_date"] != DBNull.Value) ? (string)rd["nalog_record_date"] : "";
                            string address = rd.ReadEmptyIfDbNull("regorg_address");
                            address = address.Replace(",,,", ",").Replace(",,", ",");
                            Prof.regorg_address = address;
                            Prof.email = (rd["email"] != DBNull.Value) ? (string)rd["email"] : "";
                            Prof.okpo = (rd["okpo"] != DBNull.Value) ? (string)rd["okpo"] : "Нет данных";
                            Prof.okato = (rd["okato"] != DBNull.Value) ? (string)rd["okato"] + " : " : "Нет данных";
                            Prof.okato_name = (rd["okato_name"] != DBNull.Value) ? (string)rd["okato_name"] : "";
                            Prof.oktmo = (rd["oktmo"] != DBNull.Value) ? (string)rd["oktmo"] : "Нет данных";
                            Prof.gmc_status = (rd["gmc_status"] != DBNull.Value) ? (string)rd["gmc_status"] : "";
                            Prof.updt = (rd["updt"] != DBNull.Value) ? (string)rd["updt"] : "";
                            Prof.issuer_id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            //Prof.name = rd.ReadEmptyIfDbNull("name");
                            //Prof.mesto = rd.ReadEmptyIfDbNull("mesto");
                        }
                    }
                }
                catch (Exception e)
                {
                    string exc = e.Message;
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    Prof.timeouts.Add("getMainData", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        public ProfileIP GetProfile()
        {
            Prof.ogrnip = _ticker;
            Prof.data_access = 1;
            _ActionWrapper(GetMainData);
            getProfileIPState();        //Сведения о состоянии
            getProfileIPStoping();      //Сведения о прекращении деятельности
            getProfileIPvidreg();       //Наличие в ЕГРИП записей о правоспособности 

            if (!String.IsNullOrWhiteSpace(Prof.stoping))
            {
                Prof.statusIp = Prof.stoping + " на " + Prof.stoping_date.ToShortDateString();
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(Prof.vidreg) && Prof.vidreg_date > Prof.status_date)
                {
                    Prof.statusIp = Prof.vidreg + " на " + Prof.vidreg_date.ToShortDateString();
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(Prof.status))
                    {
                        Prof.statusIp = Prof.status + " на " + Prof.status_date.ToShortDateString();
                    }
                    else
                    {
                        Prof.statusIp = "Действующий*";
                        Prof.statusNotes = "*Сведения о прекращении деятельности (реорганизации, ликвидации) отсутствуют";
                    }
                }
            }


            Action[] actions = new Action[]{              
                getProfileIP_Region,
                getProfileGoroda,
                getProfileIP_Prev,
                getProfileIP_PFR,
                getProfileIP_FSS,
                getProfileIP_mainOKVED,
                getProfileIP_OKVEDs,
                getProfileIP_Egrip         
            };

            List<Action> wrapped_actions = new List<Action>();

            foreach (var action in actions)
            {
                wrapped_actions.Add(() =>
                {
                    _ActionWrapper(action);
                });
            }

            Parallel.Invoke(wrapped_actions.ToArray());

            Prof.is_bancruptcy = (i_bank1 != 0 || i_bank2 != 0);
            Prof.is_pravo_sphinx = (i_pravo1 != 0);

            return Prof;
        }


        private void getProfileIPState()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2State", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.status = (rd["status_name"] != DBNull.Value) ? (string)rd["status_name"] : "";
                                if (rd["grnip_date"] != DBNull.Value)
                                {
                                    Prof.status_date = (DateTime)rd["grnip_date"];
                                }
                                //Prof.status += (rd["grnip_date"] != DBNull.Value && (string)rd["grnip_date"] != "") ? " на " + (string)rd["grnip_date"] : "";
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIPState: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        // Prof.timeouts.Add("getProfileIPState", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Сведения о прекращении деятельности
        private void getProfileIPStoping()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2Stoping", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.stoping = (rd["status_name"] != DBNull.Value) ? (string)rd["status_name"] : "";
                                if (rd["grnip_date"] != DBNull.Value)
                                {
                                    Prof.stoping_date = (DateTime)rd["grnip_date"];
                                }
                                //Prof.stoping += (rd["grnip_date"] != DBNull.Value && (string)rd["grnip_date"] != "") ? " на " + (string)rd["grnip_date"] : "";
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIPStoping: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //  Prof.timeouts.Add("getProfileIPStoping", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Наличие в ЕГРИП записей о правоспособности 
        private void getProfileIPvidreg()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2vidreg", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.vidreg = (rd["name"] != DBNull.Value) ? (string)rd["name"] : "";
                                if (rd["grnip_date"] != DBNull.Value)
                                {
                                    Prof.vidreg_date = (DateTime)rd["grnip_date"];
                                }
                                //Prof.vidreg += (rd["grnip_date"] != DBNull.Value && (string)rd["grnip_date"] != "") ? " на " + (string)rd["grnip_date"] : "";
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP2vidreg: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //  Prof.timeouts.Add("getProfileIP2vidreg", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Сведения о предыдущей регистрации
        private void getProfileIP_Prev()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                List<RegistrationsPrev> list = new List<RegistrationsPrev>();
                Prof.prev_reg = list;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2prev", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inn", Prof.inn);
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    RegistrationsPrev prev = new RegistrationsPrev();
                                    prev.fio = (rd["fio"] != DBNull.Value) ? (string)rd["fio"] : "";
                                    prev.ogrnip = (rd["ogrnip"] != DBNull.Value) ? (string)rd["ogrnip"] : "";
                                    prev.inn = (rd["inn"] != DBNull.Value) ? (string)rd["inn"] : "";
                                    if (rd["reg_date"] != DBNull.Value)
                                    {
                                        prev.reg_date = (DateTime)rd["reg_date"];
                                    }
                                    if (rd["stop_date"] != DBNull.Value)
                                    {
                                        prev.stop_date = (DateTime)rd["stop_date"];
                                    }
                                    if (prev != null)
                                    {
                                        Prof.prev_reg.Add(prev);
                                    }
                                }
                            }

                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP2prev: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //    Prof.timeouts.Add("getProfileIP2prev", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Регистрация в ПФР
        private void getProfileIP_PFR()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_PFR", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.pfr_status = (rd["RegNumbr"] != DBNull.Value) ? (string)rd["RegNumbr"] : "";
                                Prof.pfr_name = (rd["PFName"] != DBNull.Value) ? (string)rd["PFName"] : "";
                                if (rd["DateReg"] != DBNull.Value)
                                {
                                    Prof.pfr_date = (DateTime)rd["DateReg"];
                                }
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP2_PFR: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //    Prof.timeouts.Add("getProfileIP2_PFR", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Регистрация в ФСС
        private void getProfileIP_FSS()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_FSS", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.fss_status = (rd["RegNumbr"] != DBNull.Value) ? (string)rd["RegNumbr"] : "";
                                Prof.fss_name = (rd["FSSName"] != DBNull.Value) ? (string)rd["FSSName"] : "";
                                if (rd["DateReg"] != DBNull.Value)
                                {
                                    Prof.fss_date = (DateTime)rd["DateReg"];
                                }
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP2_FSS: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //     Prof.timeouts.Add("getProfileIP2_FSS", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Основной ОКВЕД
        private void getProfileIP_mainOKVED()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                //using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_mainOKVED", con))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP3_mainOKVED", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                OkvedCls okved = new OkvedCls();
                                okved.kod = (rd["okved"] != DBNull.Value) ? (string)rd["okved"] : "";
                                okved.name = (rd["okved_name"] != DBNull.Value) ? (string)rd["okved_name"] : "";
                                Prof.main_okved = okved;
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        //error_list.Add("Ошибка в методе getProfileIP2_mainOKVED: " + ex.ToString());
                        error_list.Add("Ошибка в методе getProfileIP3_mainOKVED: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //      Prof.timeouts.Add("getProfileIP2_mainOKVED", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        //Дополнительные ОКВЕДы
        private void getProfileIP_OKVEDs()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection con = new SqlConnection(_constring))
            //using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileIP2_OKVEDs", con))
            using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileIP3_OKVEDs", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrnip", Prof.ogrnip);
                Prof.okveds = new List<OkvedCls>();
                try
                {
                    con.Open();

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Prof.okveds.Add(new OkvedCls(rd.GetString(0), rd.GetString(1), 0));
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    //error_list.Add("Ошибка в методе getProfileIP2_OKVEDs: " + ex.ToString());
                    error_list.Add("Ошибка в методе getProfileIP3_OKVEDs: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    //     Prof.timeouts.Add("getProfileIP2_OKVEDs", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private void getProfileIP_Region()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_Region", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = Prof.ogrnip;
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.region_name = rd.ReadEmptyIfDbNull("name");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP2_Region: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //    Prof.timeouts.Add("getProfileIP2_Region", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void getProfileGoroda()
        {
            if (!string.IsNullOrWhiteSpace(Prof.okato))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileGoroda", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@okato", Prof.okato);
                    try
                    {
                        con.Open();

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                Prof.goroda = new List<GorodaCls>();
                                while (rd.Read())
                                {
                                    Prof.goroda.Add(new GorodaCls(rd.GetInt32(0), rd.GetString(1), rd.ReadEmptyIfDbNull("fn")));
                                }
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileGoroda: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //      Prof.timeouts.Add("getProfileGoroda", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void getProfileIP_Egrip()
        {
            if (!string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileIP_Egrip", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = Prof.ogrnip;
                    Prof.okveds = new List<OkvedCls>();
                    try
                    {
                        con.Open();

                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.egrip = new EgripData2
                                {
                                    egrip_date = rd.ReadEmptyIfDbNull("egrip_date"),
                                    egrip_istoday = ((int)rd["egrip_istoday"]) == 1,
                                    status_date = rd.ReadEmptyIfDbNull("status_date"),
                                    status = rd.ReadEmptyIfDbNull("status")
                                };
                            }
                            else
                            {
                                Prof.egrip = new EgripData2
                                {
                                    egrip_date = "",
                                    egrip_istoday = false,
                                    status_date = "",
                                    status = "Нет данных"
                                };
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP_Egrip: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //   Prof.timeouts.Add("getProfileIP_Egrip", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void is_egrip_black()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileIsEgrulBlack", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", Prof.ogrnip);
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                try
                {
                    con.Open();

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        Prof.egrip_is_black = rd.HasRows;
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе is_egrul_black: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    //      Prof.timeouts.Add("is_egrul_black", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private void getEGRIPPDF()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Prof.EGRIPPDF = new List<EGRIPPDFCls>();

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("select * from ( " +
                                                    "Select convert(varchar(10),q_date,104) as dt,  convert(varchar(10),q_date,112) as dts, reg_code, case convert(varchar(10),q_date,112) when convert(varchar(10),getdate(),112) then 1 else 0 end as isToday,q_date as data,'1' as is_pdf from fsns_free..egruldoc_queue a where reg_code=@ogrn and document_status=2  " +
                                                    "union all " +
                                                    "Select convert(varchar(10),extract_date,104) as dt,convert(varchar(10),extract_date,112) as dts, ogrnip,0,extract_date, '0' as is_PDF from fsns2..fl where ogrnip=@ogrn) a order by data desc ", con))
            {
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 15).Value = Prof.ogrnip;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            if ((int)rd["isToday"] == 1)
                            {
                                Prof.EGRIPPDFdate = (string)rd["dts"];
                                Prof.EGRIPPDFdate_string = (string)rd["dt"];
                                Prof.EGRIPPDFisToday = true;
                                Prof.EGRIPPDFis_pdf = (string)rd["is_pdf"];

                            }
                            else
                            {
                                Prof.EGRIPPDF.Add(new EGRIPPDFCls((string)rd["dt"], (string)rd["dts"], (string)rd["reg_code"], (string)rd["is_pdf"]));
                            }
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getEGRIPPDF: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    //      Prof.timeouts.Add("getEGRIPPDF", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private void getProfileIP_EgripLinks()
        {
            if (!string.IsNullOrWhiteSpace(Prof.fio))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop.dbo.getProfileIP_EgripLinks", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar, 15).Value = Prof.inn;
                    cmd.Parameters.Add("@fio", SqlDbType.VarChar, 500).Value = Prof.fio;

                    try
                    {
                        con.Open();

                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.egrip_links = new EgripLinksData2
                                {
                                    egrip_date = rd.ReadEmptyIfDbNull("egrip_date"),
                                    is_today = ((int)rd["is_today"]) == 1,
                                    id = (string)rd["id"]
                                };
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIP_EgripLinks: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //     Prof.timeouts.Add("getProfileIP_EgripLinks", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void getProfileZakupkiStats()
        {
            if (!string.IsNullOrEmpty(Prof.inn))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileZakupkiStats", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar, 15).Value = Prof.inn;
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.zak_data = new ZakupkiData
                                {
                                    type = (int)rd["type"],
                                    customer_cnt = (long?)rd.ReadNullIfDbNull("customer_cnt"),
                                    customer_sum = (decimal?)rd.ReadNullIfDbNull("customer_sum"),
                                    supplier_cnt = (long?)rd.ReadNullIfDbNull("supplier_cnt"),
                                    supplier_sum = (decimal?)rd.ReadNullIfDbNull("supplier_sum"),
                                    participiant_cnt = (long?)rd.ReadNullIfDbNull("participiant_cnt"),
                                    participiant_sum = (decimal?)rd.ReadNullIfDbNull("participiant_sum")
                                };
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileZakupkiStats: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //    Prof.timeouts.Add("getProfileZakupkiStats", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void getProfileUnfair()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileUnfairInn", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inn", Prof.inn);
                //cmd.Parameters.AddWithValue("@issuer_id", Prof.id);
                cmd.Parameters.AddWithValue("@type_id", -1);
                try
                {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {
                            Prof.unf_data = new UnfairData
                            {
                                rec_id = (string)rd["rec_id"],
                                start_date = rd.ReadEmptyIfDbNull("start_date")
                            };
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileUnfairInn: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    //        Prof.timeouts.Add("getProfileUnfairInn", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private void getProfilePravoStats()
        {
            if (!string.IsNullOrWhiteSpace(Prof.inn) && !string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfilePravoStats", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrn", Prof.ogrnip);
                    cmd.Parameters.AddWithValue("@inn", Prof.inn);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                Prof.pravo_data = new PravoData
                                {
                                    applicant_cnt = (long?)rd.ReadNullIfDbNull("applicant_cnt"),
                                    applicant_sum = (decimal?)rd.ReadNullIfDbNull("applicant_sum"),
                                    defendant_cnt = (long?)rd.ReadNullIfDbNull("defendant_cnt"),
                                    defendant_sum = (decimal?)rd.ReadNullIfDbNull("defendant_sum")
                                };
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfilePravoStats: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //        Prof.timeouts.Add("getProfilePravoStats", stopwatch.ElapsedMilliseconds);
                    }
                }
            }
        }

        private void getProfileIsBankruptcy()
        {
            if (!string.IsNullOrWhiteSpace(Prof.inn) && !string.IsNullOrWhiteSpace(Prof.ogrnip))
            {
                //Сообщения о банкротстве -  по ОГРНИП и ИНН
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Prof.is_bancruptcy = false;
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIsBankruptcy", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrn", Prof.ogrnip);
                    cmd.Parameters.AddWithValue("@inn", Prof.inn);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                //Prof.is_bancruptcy = true;
                                i_bank1 = 1;
                            }
                            rd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error_list.Add("Ошибка в методе getProfileIsBankruptcy: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        stopwatch.Stop();
                        //         Prof.timeouts.Add("getProfileIsBankruptcy", stopwatch.ElapsedMilliseconds);
                    }
                }
            }

            //Сообщения о банкротстве -  по ОГРНИП и ИНН

        }

        #endregion


        private static string GetErrorText()
        {
            return "<div style='width:100%;height:400px;margin: 50px 30px;'><h1 style='margin:20px 0'>Техническая ошибка</h1><p>В настоящее время наши сотрудники работают над ее устранением.</p><p>Попробуйте зайти позже</p></div>";
        }

        private static string GetMissText()
        {
            return "<div style='width:100%;height:400px;margin: 50px 30px;'><h1 style='margin:20px 0'>Ошибочный запрос</h1><p>Данной страницы не существует.</p></div>";
        }

        private void _ActionWrapper(Action action)
        {
            if (!_need_watching)
            {
                action();
            }
            else
            {
                string action_name = action.Method.Name;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                action();
                stopwatch.Stop();
                Prof.timeouts.Add(action_name, stopwatch.ElapsedMilliseconds);
            }
        }

    }
}