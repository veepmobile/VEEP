using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Skrin.BLL.Infrastructure;
using Skrin.Models.ProfileIP;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;

namespace Skrin.BLL.IP
{
    public class ProfileIPRightMenuRepository
    {
        private readonly string _ticker;
        private readonly int _user_id;
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();
        private static readonly string _constring = Configs.ConnectionString;

        private ProfileIpRightMenu _prm = null;

        private static readonly bool _need_watching = Configs.NeedWatchingTimout;
        private readonly string REPLACER = Configs.Replacer;
        private readonly string DATEREPLACER = Configs.DateReplacer;
        int i_bank1 = 0;
        int i_bank2 = 0;
        int i_pravo1 = 0;


        public ProfileIPRightMenuRepository(string ticker, int user_id)
        {
            _ticker = ticker;
            _user_id = user_id;
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
                _prm.timeouts.Add(action_name, stopwatch.ElapsedMilliseconds);
            }
        }


        #region close


        public ProfileIpRightMenu GetClosedProfile()
        {
            _prm.ogrnip = _ticker;
            GetFakeProfile();

            return _prm;
        }

        private void GetMainFakeData()
        {
            Stopwatch stopwatch = new Stopwatch();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2_Free", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = _prm.ogrnip;
                try
                {
                    con.Open();
                    stopwatch.Start();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {
                            /*Сейчас в профиле доступно ФИО, регион, ОГРНИП. Нужно что бы ОКПО стал также доступен не авторизованным.*/
                            _prm.id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            _prm.ogrnip = (rd["ogrnip"] != DBNull.Value) ? (string)rd["ogrnip"] : "";
                            _prm.rd = DATEREPLACER;
                            _prm.inn = REPLACER;
                            _prm.is_pravo_sphinx = true;
                            _prm.oktmo = REPLACER;
                            _prm.okpo = (rd["okpo"] != DBNull.Value) ? (string)rd["okpo"] : "Нет данных";
                            _prm.issuer_id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                        }
                    }
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();                   
                }
            }
        }

        private void GetFakeProfile()
        {         
            _ActionWrapper(GetMainFakeData);

            if (_prm.ogrnip == null)
                return;       

            _prm.egrip = new EgripData2 { egrip_date = DATEREPLACER, egrip_istoday = true, status = REPLACER, status_date = DATEREPLACER };
            _prm.egrip_is_black = false;
            _prm.egrip_links = new EgripLinksData2 { egrip_date = DATEREPLACER, id = "", is_today = true };
            _prm.EGRIPPDF = new List<EGRIPPDFCls>();
            _prm.zak_data = new ZakupkiData
            {
                type = 6,
                customer_cnt = long.MinValue,
                customer_sum = decimal.MinValue,
                supplier_cnt = long.MinValue,
                supplier_sum = decimal.MinValue,
                participiant_cnt = long.MinValue,
                participiant_sum = decimal.MinValue
            };
            _prm.unf_data = new UnfairData { rec_id = "", start_date = DATEREPLACER };
            _prm.pravo_data = new PravoData
            {
                applicant_cnt = long.MinValue,
                applicant_sum = decimal.MinValue,
                defendant_cnt = long.MinValue,
                defendant_sum = decimal.MinValue
            };
            _prm.is_bancruptcy = true;
            _prm.is_pravo_sphinx = true;
        }

        #endregion

        #region open

        private void _GetRightMainInfo()
        {
            _prm = new ProfileIpRightMenu(_ticker);
            Stopwatch stopwatch = new Stopwatch();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIP2", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = _prm.ogrnip;
                try
                {
                    con.Open();
                    stopwatch.Start();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {
                            _prm.id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            _prm.ogrnip = (rd["ogrnip"] != DBNull.Value) ? (string)rd["ogrnip"] : "";
                            _prm.rd = (rd["rd"] != DBNull.Value) ? (string)rd["rd"] : "";
                            _prm.inn = (rd["inn"] != DBNull.Value) ? (string)rd["inn"] : "";
                            string address = rd.ReadEmptyIfDbNull("regorg_address");
                            address = address.Replace(",,,", ",").Replace(",,", ",");                 
                            _prm.oktmo = (rd["oktmo"] != DBNull.Value) ? (string)rd["oktmo"] : "Нет данных";
                            _prm.fio = (rd["fio"] != DBNull.Value) ? (string)rd["fio"] : "";
                            _prm.fio = _prm.fio.Replace("+", "").Replace("-", "").Replace(",", "").Replace(".", "").Replace(";", "").Replace("[", "").Replace("]", "");
                            _prm.issuer_id = (rd["issuer_id"] != DBNull.Value) ? (string)rd["issuer_id"] : "";
                            _prm.okpo = (rd["okpo"] != DBNull.Value) ? (string)rd["okpo"] : "Нет данных";
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
                    _prm.timeouts.Add("getMainData", stopwatch.ElapsedMilliseconds);
                }
            }
        }

        public ProfileIpRightMenu GetProfileRightMenu()
        {
            _ActionWrapper(_GetRightMainInfo);
            Action[] actions = new Action[] { 
                getEGRIPPDF,
                is_egrip_black, 
                getProfileZakupkiStats,
                getProfileUnfair,
                getProfilePravoStats,
                getProfileIsBankruptcy,
                get_sphinx_existatnce_bankrot,
                get_sphinx_existatnce_arb
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

            _prm.is_bancruptcy = (i_bank1 != 0 || i_bank2 != 0);
            _prm.is_pravo_sphinx = (i_pravo1 != 0);

            return _prm;
        }

        private void getEGRIPPDF()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _prm.EGRIPPDF = new List<EGRIPPDFCls>();
            _prm.user_id = _user_id;
            _prm.ogrnip = _ticker;
            if (_prm.user_id > 0)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("select * from ( " +
                        "Select convert(varchar(10),q_date,104) as dt,  convert(varchar(10),q_date,112) as dts, reg_code, case convert(varchar(10),q_date,112) when convert(varchar(10),getdate(),112) then 1 else 0 end as isToday,q_date as data,'1' as is_pdf from fsns_free..egruldoc_queue a where reg_code=@ogrn and document_status=2  " +
                        "union all " +
                        "Select convert(varchar(10),extract_date,104) as dt,convert(varchar(10),extract_date,112) as dts, ogrnip,0,extract_date, '0' as is_PDF from fsns2..fl where ogrnip=@ogrn " +
                        "union all " +
                        "select convert(varchar(10),q_date,104) as dt,convert(varchar(10),q_date,112) as dts,ogrnip, case when q_date=cast(GETDATE() as DATE) then 1 else 0 end as isToday,q_date as data,'1' as is_pdf  from FSNS_Free_fl..save_egrip where ogrnip=@ogrn and is_test=@is_test) a order by data desc", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 15).Value = _prm.ogrnip;
                        cmd.Parameters.Add("@is_test", SqlDbType.Bit).Value = Configs.IsTest;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {

                                if ((int)rd["isToday"] == 1)
                                {
                                    _prm.EGRIPPDFdate = (string)rd["dts"];
                                    _prm.EGRIPPDFdate_string = (string)rd["dt"];
                                    _prm.EGRIPPDFisToday = true;
                                    _prm.EGRIPPDFis_pdf = (string)rd["is_pdf"];

                                }
                                else
                                {
                                    _prm.EGRIPPDF.Add(new EGRIPPDFCls((string)rd["dt"], (string)rd["dts"], (string)rd["reg_code"], (string)rd["is_pdf"]));
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
            else
            {
                stopwatch.Stop();
                //      Prof.timeouts.Add("getEGRIPPDF", stopwatch.ElapsedMilliseconds);
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
                cmd.Parameters.AddWithValue("@ogrn", _prm.ogrnip);
                cmd.Parameters.AddWithValue("@inn", _prm.inn);
                try
                {
                    con.Open();

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        _prm.egrip_is_black = rd.HasRows;
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

        private void getProfileZakupkiStats()
        {
            if (!string.IsNullOrEmpty(_prm.inn))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileZakupkiStats", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar, 15).Value = _prm.inn;
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                _prm.zak_data = new ZakupkiData
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

        private void getProfilePravoStats()
        {
            if (!string.IsNullOrWhiteSpace(_prm.inn) && !string.IsNullOrWhiteSpace(_prm.ogrnip))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfilePravoStats", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrn", _prm.ogrnip);
                    cmd.Parameters.AddWithValue("@inn", _prm.inn);
                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (rd.Read())
                            {
                                _prm.pravo_data = new PravoData
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
            if (!string.IsNullOrWhiteSpace(_prm.inn) && !string.IsNullOrWhiteSpace(_prm.ogrnip))
            {
                //Сообщения о банкротстве -  по ОГРНИП и ИНН
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                _prm.is_bancruptcy = false;
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = new SqlCommand("web_shop..getProfileIsBankruptcy", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrn", _prm.ogrnip);
                    cmd.Parameters.AddWithValue("@inn", _prm.inn);
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

        private void getProfileUnfair()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileUnfairInn", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inn", _prm.inn);
                //cmd.Parameters.AddWithValue("@issuer_id", Prof.id);
                cmd.Parameters.AddWithValue("@type_id", -1);
                try
                {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rd.Read())
                        {
                            _prm.unf_data = new UnfairData
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

        #endregion


        #region sphinx

        private void get_sphinx_existatnce_bankrot()
        {
            try
            {
                string _name = "";
                int type = 1;
                _name = _prm.fio.Replace("'", "");

                if (!string.IsNullOrWhiteSpace(_name))
                {
                    //int port = 0;
                    string character_set = "utf8";
                    string pattern = "";
                    //port = 9385;
                    pattern = "SELECT id from bankruptcy where match('\"" + _name + "\"') limit 1";

                    SphynxSearcher searcher = new SphynxSearcher(pattern, Configs.SphinxBankruptcyPort, character_set, Configs.SphinxBankruptcyServer);
                    string json_text = searcher.SearchJson();

                    var json_result = JObject.Parse("{" + json_text + "}");
                    var search_result = (JArray)json_result["results"];
                    if (search_result.Count > 0)
                    {
                        switch (type)
                        {
                            case 1:
                                i_bank2 = 1;
                                break;
                            case 2:
                                i_pravo1 = 1;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе get_sphinx_existatnce: " + ex.ToString());
            }
        }


        private void get_sphinx_existatnce_arb()
        {
            try
            {
                string _name = "";
                int type = 2;
                _name = _prm.fio.Replace("'", "");

                if (!string.IsNullOrWhiteSpace(_name))
                {
                    //int port = 0;
                    //string charaster_set = "cp1251";
                    string pattern = "";
                    //port = 9311;
                    pattern = "SELECT id from pravo4 where match('\"" + _name + "\"') limit 1";
                    string character_set = "utf8";

                    //SphynxSearcher searcher = new SphynxSearcher(pattern, port.ToString(), charaster_set);
                    SphynxSearcher searcher = new SphynxSearcher(pattern, Configs.SphinxPravoPort, character_set, Configs.SphinxPravoServer);
                    string json_text = searcher.SearchJson();

                    var json_result = JObject.Parse("{" + json_text + "}");
                    var search_result = (JArray)json_result["results"];
                    if (search_result.Count > 0)
                    {
                        switch (type)
                        {
                            case 1:
                                i_bank2 = 1;
                                break;
                            case 2:
                                i_pravo1 = 1;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе get_sphinx_existatnce: " + ex.ToString());
            }
        }

        #endregion
    }
}