using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.BLL.Iss
{
    public class ProfileRightMenuRepository
    {
        private readonly string _ticker;
        private readonly int _user_id;
        private readonly bool _is_open;

        private static readonly string _constring = Configs.ConnectionString;



        private static readonly bool _need_watching = Configs.NeedWatchingTimout;
        private readonly string REPLACER = Configs.Replacer;
        private readonly string DATEREPLACER = Configs.DateReplacer;

        private ProfileRightMenu _prm = null;


        public ProfileRightMenuRepository(string ticker, int user_id, bool is_open)
        {
            _ticker = ticker;
            _user_id = user_id;
            _is_open = is_open;
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

        public ProfileRightMenu GetProfileRightMenu()
        {
            _ActionWrapper(_GetRightMainInfo);
            Action[] actions = new Action[] { 
                _GetEGRULPDF,
                _GetProfileUserReports,
                _GetProfileZakupkiStatus,
                _GetUnfair,
                _GetPravoStats,
                _GetDependantsCount,
                _GetBranchesCount,
                _IsEgrulExists,
                _GetMonopolist
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

            return _prm;
        }

        private void _GetRightMainInfo()
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
                        _prm = new ProfileRightMenu(_ticker)
                        {
                            ogrn = (string)rd.ReadNullIfDbNull("ogrn"),
                            inn = rd.ReadEmptyIfDbNull("inn"),
                            group_id = (int)rd["group_id"],
                            opf = (rd["opf"] != DBNull.Value) ? (int?)rd["opf"] : null,
                            issuer_id = rd.ReadEmptyIfDbNull("issuer_id"),
                            uniq = ((byte?)rd.ReadNullIfDbNull("uniq") == 1),
                            egrul_is_possible = ((int)rd["egrul_is_possible"] == 1),
                            legal_address = (string)rd.ReadNullIfDbNull("legal_address")
                        };
                    }
                }
            }
        }


        private void _GetEGRULPDF()
        {
            _prm.EGRULPDF = new List<EGRULPDF>();
            if (_user_id == 0 || _prm.ogrn == null)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(@"select * from ( 
                        Select convert(varchar(10),q_date,104) as dt,  convert(varchar(10),q_date,112) as dts, reg_code, case convert(varchar(10),q_date,112) when convert(varchar(10),getdate(),112) then 1 else 0 end as isToday,q_date as data,'1' as is_pdf from fsns_free..egruldoc_queue a where reg_code=@ogrn and document_status=2  
                        union all 
                        Select convert(varchar(10),extract_date,104) as dt,convert(varchar(10),extract_date,112) as dts, ogrn,0,extract_date, '0' as is_PDF from fsns2..ul2 where ogrn=@ogrn
                        union all 
                        select convert(varchar(10),q_date,104) as dt,convert(varchar(10),q_date,112) as dts,ogrn, case when q_date=cast(GETDATE() as DATE) then 1 else 0 end as isToday,q_date as data,'1' as is_pdf  from FSNS_Free..save_egrul where ogrn=@ogrn and is_test=@is_test
                        ) a order by data desc ", con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = _prm.ogrn;
                cmd.Parameters.Add("@is_test", SqlDbType.Bit).Value = Configs.IsTest;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        if ((int)rd["isToday"] == 1 && (string)rd["is_pdf"]=="1")
                        {
                            _prm.egrul_today_exists = true;

                        }
                        _prm.EGRULPDF.Add(new EGRULPDF((string)rd["dt"], (string)rd["dts"], (string)rd["reg_code"], (string)rd["is_pdf"]));
                    }
                }
            }
        }

        private void _IsEgrulExists()
        {
            if (_prm.ogrn == null || !_prm.egrul_is_possible)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"
                    SELECT top 1 a from
                    (
                    select TOP 1 1 a from FSNS_Free..ul2 where ogrn=@ogrn and IsLast=1
                    UNION all
                    SELECT top 1 1 a from fsns2..ul2 where ogrn=@ogrn 
                    UNION ALL
                    SELECT top 1 1 a from fsns_free..egruldoc_queue  where reg_code=@ogrn and document_status=2
                    )t
                    ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = _prm.ogrn;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    _prm.egrul_is_exists = rd.Read();
                }
            }
        }

        private void _GetProfileUserReports()
        {
            _prm.URep = new List<UserReport>();
            if (_user_id == 0)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_UserReports", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", _user_id);
                cmd.Parameters.AddWithValue("@issuer_id", _prm.issuer_id);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _prm.URep.Add(new UserReport((string)rd["id"], rd.ReadEmptyIfDbNull("dt"), rd.ReadEmptyIfDbNull("filename")));
                    }
                }
            }
        }


        private void _GetProfileZakupkiStatus()
        {
            if (!_is_open)
            {
                _prm.zak_data = new ZakupkiData
                {
                    type = 8,
                    customer_cnt = long.MinValue,
                    customer_sum = decimal.MinValue,
                    supplier_cnt = long.MinValue,
                    supplier_sum = decimal.MinValue,
                    participiant_cnt = long.MinValue,
                    participiant_sum = decimal.MinValue
                };
                return;
            }
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_ZakupkiStats2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@issuer_id", _prm.issuer_id);
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
                }
            }
        }

        private void _GetUnfair()
        {
            if (!_prm.uniq || !_is_open || (_prm.inn == null) || (_prm.inn == ""))
                return;

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_Unfair", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inn", _prm.inn);
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
                }
            }
        }

        private void _GetPravoStats()
        {

            if (!_is_open)
            {
                _prm.pravo_data = new PravoData
                {
                    applicant_cnt = long.MinValue,
                    applicant_sum = decimal.MinValue,
                    defendant_cnt = long.MinValue,
                    defendant_sum = decimal.MinValue
                };
                return;
            }


            if ((!_prm.uniq) || (_prm.ogrn == null) || (_prm.inn == null))
                return;

            int applicant_cnt = 0;
            decimal applicant_sum = 0;
            int defendant_cnt = 0;
            decimal defendant_sum = 0;
            string url = "/pravo/case/_search";
            for (int side_type_id = 0; side_type_id <= 1; side_type_id++)
            {
                string json = "{\"query\": {\"nested\": {\"path\": \"side\", \"query\": {\"bool\": " +
                    "  {\"must\":[" +
                    "    {\"term\": {\"side.inn\": \"" + _prm.inn + "\"}}, " +
                    "    {\"term\": {\"side.ogrn\": \"" + _prm.ogrn + "\"}} " +
                    "  ], " +
                    "  \"filter\": {\"term\": {\"side.side_type_id\": " + side_type_id + " } } " +
                    "}} }}, " +
                    "\"aggs\": {\"agg\": {\"sum\": {\"field\": \"case_sum\"} } }, " +
                    "\"size\": \"0\" }";
                ElasticClient ec = new ElasticClient();
                JObject r = ec.GetQuery(url, json);
                if (side_type_id == 0)
                {
                    applicant_cnt = (int)r["hits"]["total"];
                    applicant_sum = (decimal)r["aggregations"]["agg"]["value"];
                }
                else
                {
                    defendant_cnt = (int)r["hits"]["total"];
                    defendant_sum = (decimal)r["aggregations"]["agg"]["value"];
                }
            }

            if (applicant_cnt != 0 || defendant_cnt != 0)
            {
                _prm.pravo_data = new PravoData();
                _prm.pravo_data.applicant_cnt = applicant_cnt;
                _prm.pravo_data.applicant_sum = applicant_sum;
                _prm.pravo_data.defendant_cnt = defendant_cnt;
                _prm.pravo_data.defendant_sum = defendant_sum;
            }

            /*using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_PravoStats", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", _prm.ogrn);
                cmd.Parameters.AddWithValue("@inn", _prm.inn);
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
                }
            }*/
        }

        private void _GetMonopolist()
        {
            if (!_prm.uniq || !_is_open)
                return;
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_monopolist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inn", _prm.inn);
                con.Open();
                var regions = new List<string>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regions.Add(reader.ReadEmptyIfDbNull(0));
                    }
                    if (regions.Count > 0)
                        _prm.monoploist_regions = regions;
                }
            }
        }

        private void _GetDependantsCount()
        {
            if (!_is_open)
            {
                _prm.subsids_count = int.MinValue;
                return;
            }

            if (_prm.group_id == 0)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_DependantsCountSkrin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _prm.issuer_id;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            _prm.subsids_count = (int)reader[0];
                    }
                }
            }
        }

        private void _GetBranchesCount()
        {
            if (!_is_open)
            {
                _prm.branch_count = int.MinValue;
                return;
            }


            if (_prm.group_id == 0)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_BranchesCountSkrin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _prm.issuer_id;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            _prm.branch_count = (int)reader[0];
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_prm.inn))
                    return;

                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_net..content_get_profile_BranchesCountGKS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inn", _prm.inn);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            _prm.branch_count = (int)reader[0];
                    }
                }
            }
        }
    }
}