using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Skrin.BLL.Iss
{
    public class ProfileRepository
    {
        private readonly string _ticker;
        private static readonly string _constring = Configs.ConnectionString;
        
        private static readonly bool _need_watching = Configs.NeedWatchingTimout;
        private readonly string REPLACER = Configs.Replacer;
        private readonly string DATEREPLACER = Configs.DateReplacer;

        private Profile _profile = null;



        private static readonly List<string> _logos = new List<string> { "AFLT.png", "AVAZ.png", "sanve.png", "kzos.jpg", "KZMS.jpg", "NORTK.png", "SZTT.png", "TAYME.jpg", "tatn.png", "cppsk.png" };
        


        public ProfileRepository(string ticker)
        {
            _ticker = ticker;
        }



        #region open

        public Profile GetProfile()
        {
            _ActionWrapper(_GetMainInfo);
            Action[] actions = new Action[]{
                _GetProfileEgrulManagerHistory,
                _GetProfileEgrulNameHistory,
                _GetProfileEgrulAddressHistory,
                _GetProfileEgrulCoownerHistory,
                _GetEgrulStatus,
                _GetGksStatus,
                _GetGoroda,
                _GetOkveds,
                _GetStockInfo,
                _GetProfileQIVBalance,
                _HasFactors,
                _GetProfileConsGKS,
                _GetProfileConsEgrul,
                _GetStateCapital,
                _GetRsmpData,
                _GetEngName,
                _GetMassreg,
                _GetBadAddr,
                _GetDisqual
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

            return _profile;
        }


        private void _GetMainInfo()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile = new Profile
                        {
                            ticker = _ticker,
                            name = rd.ReadEmptyIfDbNull("NAME"),
                            short_name = rd.ReadEmptyIfDbNull("short_name"),
                            legal_address = rd.ReadEmptyIfDbNull("legal_address"),
                            legal_phone = rd.ReadEmptyIfDbNull("legal_phone"),
                            legal_fax = rd.ReadEmptyIfDbNull("legal_fax"),
                            legal_email = rd.ReadEmptyIfDbNull("legal_email"),
                            www = rd.ReadEmptyIfDbNull("www"),
                            inn = rd.ReadEmptyIfDbNull("inn"),
                            ogrn = rd.ReadEmptyIfDbNull("ogrn"),
                            ogrn_date = rd.ReadEmptyIfDbNull("ogrn_date"),
                            reg_org = rd.ReadEmptyIfDbNull("reg_org"),
                            fcsm_code = rd.ReadEmptyIfDbNull("fcsm_code"),
                            first_okved = rd.ReadEmptyIfDbNull("first_okved"),
                            region = rd.ReadEmptyIfDbNull("region"),
                            gks_id = (string)rd.ReadNullIfDbNull("gks_id"),
                            id = (int)rd["id"],
                            okato = rd.ReadEmptyIfDbNull("okato"),
                            opf = (int?)rd.ReadNullIfDbNull("opf"),
                            issuer_id = rd.ReadEmptyIfDbNull("issuer_id"),
                            uniq = ((byte?)rd.ReadNullIfDbNull("uniq") == 1),
                            group_id = (int)rd["group_id"],
                            okpo = rd.ReadEmptyIfDbNull("okpo"),
                            kpp = rd.ReadEmptyIfDbNull("kpp"),
                            type_id = (int)rd["type_id"],
                            logo_path = (_logos.Select(p=>_GetPureFileName(p)).Contains(_ticker.ToUpper()) && (Int16?)rd.ReadNullIfDbNull("information") == 4) ? string.Format("/Content/logos/{0}", _logos.Find(p=>_GetPureFileName(p)==_ticker.ToUpper())) : null,
                            ul2_ogrn = (string)rd.ReadNullIfDbNull("ul2_ogrn"),
                            is_branch = (int)rd["is_branch"] == 1,
                            src = rd.ReadEmptyIfDbNull("src")

                        };

                        string[] link_arr = rd.ReadEmptyIfDbNull("link_arr").Split(',');
                        string[] ruler_arr = rd.ReadEmptyIfDbNull("ruler").Split(',');
                        string[] ruler_fio_arr = rd.ReadEmptyIfDbNull("ruler_fio").Split(',');
                        string[] ruler_inn_arr = rd.ReadEmptyIfDbNull("ruler_inn").Split(',');

                        _profile.ruler_list = new List<RulerData>();

                        for (int i = 0; i < link_arr.Count(); i++)
                        {
                            RulerData ruler = new RulerData();
                            ruler.link = link_arr[i];
                            if (ruler_arr.Count() > i) ruler.ruler = ruler_arr[i].Trim();
                            if (ruler_fio_arr.Count() > i) ruler.ruler_fio = ruler_fio_arr[i].Trim();
                            if (ruler_inn_arr.Count() > i) ruler.ruler_inn = ruler_inn_arr[i].Trim();
                            _profile.ruler_list.Add(ruler);
                        }

                    }
                }
            }
        }
        private void _GetMassreg()
        {//Получаем данные о массовой регистрации
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("OpenData..GetMassReg", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.mass_reg = (string)rd.ReadNullIfDbNull("data");
                    }
                }
            }

        }
        private void _GetBadAddr()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("OpenData..GetBadAddr", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.bad_addr = (string)rd.ReadNullIfDbNull("BadAddr");
                    }
                }
            }
        }
        private void _GetDisqual()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("OpenData..GetDisqual", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.disqual = (string)rd.ReadNullIfDbNull("Disqual");
                    }
                }
            }
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
                _profile.timeouts.Add(action_name, stopwatch.ElapsedMilliseconds);
            }
        }

        private void _GetProfileEgrulManagerHistory()
        {
            _profile.egrul_man_hist = new List<EgrulManagerHistoryData>();

            if (string.IsNullOrEmpty(_profile.ogrn))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_EgrulManagerHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _profile.ogrn);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.egrul_man_hist.Add(new EgrulManagerHistoryData
                        {
                            ds = rd.ReadEmptyIfDbNull("dtstart"),
                            name = rd.ReadEmptyIfDbNull("name"),
                            fio = rd.ReadEmptyIfDbNull("fio"),
                            inn = rd.ReadEmptyIfDbNull("inn")
                        });
                    }
                }
            }
        }

        private void _GetProfileEgrulNameHistory()
        {
            _profile.egrul_name_hist = new List<EgrulNameHistoryData>();

            if (string.IsNullOrEmpty(_profile.ogrn))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_EgrulNameHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _profile.ogrn);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.egrul_name_hist.Add(new EgrulNameHistoryData
                        {
                            ds = rd.ReadEmptyIfDbNull("ds"),
                            fullName = rd.ReadEmptyIfDbNull("fullName"),
                            shortName = rd.ReadEmptyIfDbNull("shortName")
                        });
                    }
                }
            }
        }

        private void _GetProfileEgrulAddressHistory()
        {
            _profile.egrul_addr_hist = new List<EgrulAddressHistoryData>();

            if (string.IsNullOrEmpty(_profile.ogrn))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_EgrulAddressHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _profile.ogrn);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.egrul_addr_hist.Add(new EgrulAddressHistoryData
                        {
                            ds = rd.ReadEmptyIfDbNull("ds"),
                            address = rd.ReadEmptyIfDbNull("address")
                        });
                    }
                }
            }
        }

        private void _GetProfileEgrulCoownerHistory()
        {
            _profile.egrul_coowner_hist = new List<EgrulCoownerHistoryData>();

            if (string.IsNullOrEmpty(_profile.ogrn))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_EgrulCoownerHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _profile.ogrn);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.egrul_coowner_hist.Add(new EgrulCoownerHistoryData
                        {


                            dtstart = rd.ReadEmptyIfDbNull("dtstart"),
                            ticker = rd.ReadEmptyIfDbNull("ticker"),
                            name = rd.ReadEmptyIfDbNull("name"),
                            summa = (decimal?)rd.ReadNullIfDbNull("summa"),
                            coowner_type = rd.ReadEmptyIfDbNull("coowner_type"),
                            inn = rd.ReadEmptyIfDbNull("inn"),
                            share_head = rd.ReadEmptyIfDbNull("share_head"),
                            share_part = (rd.ReadEmptyIfDbNull("share_part") == "") ? "-" : rd.ReadEmptyIfDbNull("share_part")
                        });
                    }
                }
            }
        }

        private void _GetEngName()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select name_eng from naufor..Issuers where id=@issuer_id", con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _profile.issuer_id;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    _profile.eng_name = (string)reader.ReadNullIfDbNull(0);
                }
            }
        }

        private void _GetEgrulStatus()
        {
            if (_profile.ul2_ogrn == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_EgrulStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _profile.ul2_ogrn);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.free_egrul_status = rd.ReadEmptyIfDbNull("status");
                    }
                }
            }
        }        


        private void _GetGksStatus()
        {
            if (_profile.gks_id == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_GKSStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gks_id", _profile.gks_id);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.gks_stat = rd.ReadEmptyIfDbNull("gks_stat");
                    }
                }
            }
        }

        private void _GetOkveds()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_Okveds", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@us_id", _profile.id);
                con.Open();
                _profile.okveds = new List<Okved>();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.okveds.Add(new Okved
                        {
                            kod = rd.ReadEmptyIfDbNull("kod"),
                            name = rd.ReadEmptyIfDbNull("name"),
                            vis = (byte)rd["vis"]
                        });
                    }
                }
            }
        }

        private void _GetGoroda()
        {
            if (string.IsNullOrWhiteSpace(_profile.okpo))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_Goroda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@okato", _profile.okato);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.gorod = new Goroda
                        {
                            soato = (int)rd["soato"],
                            name = rd.ReadEmptyIfDbNull("name"),
                            gerb = rd.ReadEmptyIfDbNull("fn")
                        };
                    }
                }
            }
        }

        private void _GetStockInfo()
        {
            if (_profile.uniq)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_StockInfo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _profile.issuer_id;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            _profile.aoi = (string)reader.ReadNullIfDbNull("aoi");
                            _profile.api = (string)reader.ReadNullIfDbNull("api");
                        }
                    }
                }
            }
        }

        private void _GetProfileQIVBalance()
        {
            if (_profile.gks_id == null)
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_QIVBalance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gks_id", _profile.gks_id);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        _profile.main_balance = new List<MainBalance>();
                        while (rd.Read())
                        {
                            _profile.iss_type = rd.GetInt32(5);
                            _profile.main_balance.Add(new MainBalance(rd.GetInt32(0), _profile.iss_type, (decimal?)rd.ReadNullIfDbNull(2), (decimal?)rd.ReadNullIfDbNull(3), (decimal?)rd.ReadNullIfDbNull(4)));
                        }
                    }
                }
            }
        }

        private void _HasFactors()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_HasFactors", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _profile.issuer_id;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    _profile.has_factors = reader.Read();
                }
            }
        }

        private void _GetProfileConsEgrul()
        {
            if (_profile.uniq)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_ConstEgrul", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ogrn", _profile.ogrn);
                    cmd.Parameters.AddWithValue("@inn", _profile.inn);
                    con.Open();
                    ConstEgrul _ce;
                    _profile.const_egrul = new List<ConstEgrul>();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            _ce = new ConstEgrul();
                            _ce.uid = rd.GetGuid(0).ToString();
                            _ce.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            _ce.name = rd.ReadEmptyIfDbNull("name");
                            _ce.inn = rd.ReadEmptyIfDbNull("inn");
                            _ce.share_head = rd.ReadEmptyIfDbNull("share_head");
                            _ce.share_part = rd.ReadEmptyIfDbNull("share_part");
                            _ce.css = rd.ReadEmptyIfDbNull("css");
                            _ce.grn = rd.ReadEmptyIfDbNull("grn");
                            _ce.grn_date = rd.ReadDateToString("grn_date");
                            _ce.encumbrance_type = rd.ReadEmptyIfDbNull("encumbrance_type");
                            _ce.encumbrance_per = rd.ReadEmptyIfDbNull("encumbrance_per");
                            _ce.share = rd.ReadEmptyIfDbNull("share");
                            _ce.extract_date = rd.ReadDateToString("extract_date");
                            _ce.ticker = rd.ReadEmptyIfDbNull("ticker");
                            _ce.ord = (int)rd["ord"];
                            _profile.const_egrul.Add(_ce);
                        }
                    }
                }
            }
        }

        private void _GetProfileConsGKS()
        {
            if (_profile.gks_id == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_ConstGKS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gks_id", _profile.gks_id);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        _profile.consts = new List<ConstGKS>();
                        while (rd.Read())
                        {
                            _profile.consts.Add(new ConstGKS(rd.ReadEmptyIfDbNull(0), (decimal?)rd.ReadNullIfDbNull(1), (decimal?)rd.ReadNullIfDbNull(2), rd.ReadEmptyIfDbNull(3), rd.ReadEmptyIfDbNull(4), rd.ReadEmptyIfDbNull(5)));
                        }
                    }
                }
            }
        }

        private void _GetProfileConsEgrulShare()
        {
            if (_profile.ul2_ogrn == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"select convert(varchar(10),share_grn_date,104) as rd, share_grn,
                            web_shop.dbo.Format(sizeRub,0) as sizeRub,
                            case when not share_percent is null then '%' when not share_decimal is null then 'в виде десятичной дроби' when not share_numerator is null then 'в виде простой дроби' else '%' end as share_head,
                            case when not share_percent is null then web_shop.dbo.Format(share_percent,2) when not share_decimal is null then web_shop.dbo.Format(share_decimal,2) when not share_numerator is null then web_shop.dbo.Format(share_numerator,0) + '/' + web_shop.dbo.Format(share_denominator,0)  else '-' end as share_part
                            from fsns_free.dbo.UL2_AuthorizedShare where ul2_ogrn=@ogrn and isLast=1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = _profile.ul2_ogrn;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.const_egrul_share = rd.GetString(0);
                        _profile.const_egrul_grn = rd.ReadEmptyIfDbNull("share_grn");
                        _profile.const_egrul_grn_date = rd.ReadEmptyIfDbNull("rd");
                        _profile.const_egrul_share = rd.ReadEmptyIfDbNull("sizeRub");
                        _profile.const_egrul_part = rd.ReadEmptyIfDbNull("share_part");
                        _profile.const_egrul_part_ei = rd.ReadEmptyIfDbNull("share_head");
                    }
                }
            }
        }

        private void _GetRsmpData()
        {
            if (_profile.inn == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_RMSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 12).Value = _profile.inn;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if(rd.Read())
                    {
                        _profile.RsmpData = new RSMPData
                        {
                            IncludeDate = rd.ReadEmptyIfDbNull("IncludeDate"),
                            IsNew = (string)rd["IsNew"],
                            SubjectType = (string)rd["SubjectType"]
                        };
                    }
                }
            }
        }

        private void _GetStateCapital()
        {
            if (_profile.group_id == 0)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    string sql = @"select naufor.dbo.Issuer_Capitals_val(issuer_id,period) as val,convert(varchar(10),period,104) as dt, 
                                    is_active as ia from naufor..Issuer_Capitals Issuer_Capitals where issuer_id=@issuer_id order by is_active desc, period desc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _profile.issuer_id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            _profile.state_capital = rd.ReadEmptyIfDbNull(0).ConvertTextSum();
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_profile.ul2_ogrn))
                {
                    using (SqlConnection con = new SqlConnection(_constring))
                    {
                        string sql = "SELECT capital FROM FSNS_Free.[dbo].[UL2_Capital] where isLast=1 and UL2_ogrn = @ogrn";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = _profile.ul2_ogrn;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                _profile.state_capital = ((decimal?)rd.ReadNullIfDbNull(0)).ConvertTextSum();
                            }
                        }
                    }
                }

            }
        }


        #endregion open

        #region close


        public Profile GetClosedProfile()
        {

            _GetCloseMainInfo();


            //_GetProfileEgrulManagerHistory()
            _profile.egrul_man_hist = new List<EgrulManagerHistoryData>{new EgrulManagerHistoryData{
                ds=DATEREPLACER,name=REPLACER,fio=REPLACER,inn=REPLACER
            }};

            //_GetProfileEgrulNameHistory,
            _profile.egrul_name_hist = new List<EgrulNameHistoryData>{new EgrulNameHistoryData{
                ds=DATEREPLACER, shortName=REPLACER,fullName=REPLACER
            }};

            // _GetProfileEgrulAddressHistory,
            _profile.egrul_addr_hist = new List<EgrulAddressHistoryData>{new EgrulAddressHistoryData{
                ds=DATEREPLACER,address=REPLACER
            }};

            //_GetProfileEgrulCoownerHistory
            _profile.egrul_coowner_hist = new List<EgrulCoownerHistoryData>{new EgrulCoownerHistoryData{
                dtstart=DATEREPLACER,ticker="",name=REPLACER,summa=Decimal.MinValue,coowner_type="icon-building const_ico",inn=REPLACER,share_head=REPLACER,share_part=REPLACER
            }};

            //_GetEgrulStatus()
            _profile.free_egrul_status = REPLACER;

            //_GetGksStatus()
            _profile.gks_stat = REPLACER;

            //_GetOkveds()
            _profile.okveds = new List<Okved>{
                new Okved{kod=REPLACER,name=REPLACER,vis=1},new Okved{kod=REPLACER,name=REPLACER,vis=0}
            };

            //_GetGoroda()
            _profile.gorod = new Goroda { soato = 0, name = REPLACER, gerb = "" };

            //_GetStockInfo()
            _profile.aoi = REPLACER;
            _profile.api = REPLACER;

            //_GetProfileQIVBalance()
            List<int> years = _GetProfileYears();
            _profile.main_balance = new List<MainBalance>();
            _profile.iss_type = 2;
            for (int i = 0; i < 3; i++)
            {
                _profile.main_balance.Add(new MainBalance(years[i], _profile.iss_type, decimal.MinValue, decimal.MinValue, decimal.MinValue));
            }

            //_HasFactors()
            _profile.has_factors = false;

            //_GetProfileConsEgrul()
            _profile.const_egrul = new List<ConstEgrul> { new ConstEgrul { uid = REPLACER, extract_date = DATEREPLACER, inn = REPLACER, name = REPLACER, ogrn = REPLACER, ord = 1, share = REPLACER, ticker = "" } };

            //_GetProfileConsGKS()
            _profile.consts = new List<ConstGKS>() { new ConstGKS(REPLACER, decimal.MinValue, decimal.MinValue, REPLACER, REPLACER, "") };

            //_GetStateCapital()
            _profile.state_capital = REPLACER;

            //_GetRsmpData()
            _profile.RsmpData = new RSMPData { IncludeDate = REPLACER, IsNew = REPLACER, SubjectType = REPLACER };


            return _profile;
        }

        private void _GetCloseMainInfo()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile = new Profile
                        {
                            ticker = _ticker,
                            name = rd.ReadEmptyIfDbNull("NAME"),
                            short_name = rd.ReadEmptyIfDbNull("short_name"),
                            legal_address = rd.ReadEmptyIfDbNull("legal_address"),
                            legal_phone = REPLACER,
                            legal_fax = REPLACER,
                            legal_email = REPLACER,
                            www = REPLACER,
                            inn = rd.ReadEmptyIfDbNull("inn"),
                            ogrn = rd.ReadEmptyIfDbNull("ogrn"),
                            ogrn_date = DATEREPLACER,
                            reg_org = REPLACER,
                            fcsm_code = REPLACER,
                            first_okved = REPLACER,
                            region = REPLACER,
                            gks_id = (string)rd.ReadNullIfDbNull("gks_id"),
                            id = (int)rd["id"],
                            okato = REPLACER,
                            opf = (int?)rd.ReadNullIfDbNull("opf"),
                            issuer_id = rd.ReadEmptyIfDbNull("issuer_id"),
                            uniq = ((byte?)rd.ReadNullIfDbNull("uniq") == 1),
                            group_id = (int)rd["group_id"],
                            okpo = rd.ReadEmptyIfDbNull("okpo"),
                            kpp = rd.ReadEmptyIfDbNull("kpp"),
                            type_id = (int)rd["type_id"],
                            //logo_path = (_logos.Contains(_ticker.ToUpper()) && (Int16?)rd.ReadNullIfDbNull("information") == 4) ? string.Format("<img border=\"0\" src=\"/logos/{0}.jpg\" alt=\"logo\"/>", _ticker) : null,
                            ul2_ogrn = (string)rd.ReadNullIfDbNull("ul2_ogrn"),
                            src = rd.ReadEmptyIfDbNull("src")
                        };

                        string[] link_arr = rd.ReadEmptyIfDbNull("link_arr").Split(',');
                        string[] ruler_arr = rd.ReadEmptyIfDbNull("ruler").Split(',');
                        string[] ruler_fio_arr = rd.ReadEmptyIfDbNull("ruler_fio").Split(',');
                        string[] ruler_inn_arr = rd.ReadEmptyIfDbNull("ruler_inn").Split(',');

                        _profile.ruler_list = new List<RulerData>();

                        for (int i = 0; i < link_arr.Count(); i++)
                        {
                            RulerData ruler = new RulerData();
                            ruler.link = link_arr[i];
                            if (ruler_arr.Count() > i) ruler.ruler = ruler_arr[i].Trim();
                            if (ruler_fio_arr.Count() > i) ruler.ruler_fio = ruler_fio_arr[i].Trim();
                            if (ruler_inn_arr.Count() > i) ruler.ruler_inn = ruler_inn_arr[i].Trim();
                            _profile.ruler_list.Add(ruler);
                        }
                    }
                }

               
            }
        }

        private List<int> _GetProfileYears()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("skrin_net.dbo.content_get_profile_Years", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                List<int> ret = new List<int>();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ret.Add((int)rd[0]);
                    }
                    rd.Close();
                }
                return ret;
            }
        }


        #endregion


        private string _GetPureFileName(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.LastIndexOf('.')<0)
                return input;

            return input.Substring(0, input.LastIndexOf('.')).ToUpper();
        }
    }
}