using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models.Iss;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.QIVSearch;


namespace Skrin.BLL.Iss
{
    public class TradeRepository
    {
        public static async Task<TradeInfo> GetTradeInfoAsync(string ticker, DateTime? sDate, DateTime? tDate,Currency currency,List<Exchange> exchange_list,List<IssueType> issues_list)
        {
            var ret = new TradeInfo
            {
                Headers = await _GetHeaders(currency),
                Items=new List<TradeInfoItem>()
            };

            string cur = currency == Currency.USD ? "-1" : "1";


            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = string.Format(@"select v.reg_date, t.trade_place_id, v.indic_id, v.value, idx.no, t.code, idx.price_flag, 
                            p.name + case when p.id in (1,10) then ' ' +p1.name else '' end as trade_place_name, 
                            isNull(i.reg_no, '') as reg_no, convert(varchar(10), i.reg_date, 104) as reg_no_date, 
                            t.issue_type_id, convert(varchar(10), v.reg_date, 104) as reg_date_ru from naufor..TP_Codes2 t with(nolock) 
                            inner join naufor..Stock_Indic_Values2 v with(nolock) ON t.trade_place_id = v.trade_place_id AND t.id=v.tp_code_id 
                            inner join ( select case price_flag when 0 then indic_id else {0}*indic_id end as id, no, price_flag from naufor..Stock_indics_show_plus ) idx on idx.id = v.indic_id 
                            left join naufor..Trade_Places p with(nolock) on v.trade_place_id = p.id 
                            left join naufor..Trade_Places2 p1 with(nolock) on v.trade_place2_id = p1.id 
                            left outer join naufor..Issues i with(nolock) on t.issue_id = i.id 
                            WHERE t.issuer_id = (Select id from naufor..issuers where rts_code=@ticker)  {1}  {2} {3}
                            GROUP BY v.reg_date, t.trade_place_id, p.name + case when p.id in (1,10) then ' ' +p1.name else '' end, 
                            v.indic_id, v.value, i.reg_no, i.reg_date, idx.no, t.code, idx.price_flag, t.issue_type_id ORDER BY 1 desc, 2, 6, 5", cur,_GetDateWhere(sDate,tDate),
                            _GetExchageWhere(exchange_list),_GetIssuesWhere(issues_list));

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DateTime? cur_date = null;
                string cur_code = "";
                TradeInfoItem item=null;
                while(await reader.ReadAsync())
                {
                    DateTime reader_date = (DateTime)reader["reg_date"];
                    if ((cur_date != reader_date) || (cur_code != (string)reader.ReadNullIfDbNull("code")))
                    {
                        cur_date = reader_date;
                        cur_code = (string)reader.ReadNullIfDbNull("code");
                        item = new TradeInfoItem(ret.Headers.Count);
                        item.RegDate = reader_date.ToRusString();
                        item.Code = cur_code;
                        item.TradePlaceName = (string)reader.ReadNullIfDbNull("trade_place_name");
                        item.IssueTypeId = (int)(byte)reader["issue_type_id"];
                        ret.Items.Add(item);
                    }
                    int no = (int)reader["no"];
                    item.Values[no] = ((double?)reader.ReadNullIfDbNull("value")).NullableToString("#,##0.######################");
                }
            }

            
            return ret;
        }

        public static ExcelResult TradeInfoToExcel(TradeInfo item)
        {
            ExcelResult res = new ExcelResult();

            if (item != null)
            {
                res.AddHeader(0, "Дата", "p0", "s", 15);
                res.AddHeader(1, "Биржевой код", "p1", "s",20);
                res.AddHeader(2, "Орг. торг.", "p2", "s",10);
                int c = 3;
                int i = c;
                foreach( KeyValuePair<int, string> kvp in item.Headers)
                {
                    res.AddHeader(i, kvp.Value, "p"+i.ToString(), "n",20);
                    i++;
                }
                foreach (TradeInfoItem it in item.Items)
                {
                    res.AddValue(0, it.RegDate);
                    res.AddValue(1, it.Code);
                    res.AddValue(2, it.TradePlaceName);
                    i = c;
                    foreach( KeyValuePair<int, string> kvp in item.Headers)
                    {
                        res.AddValue(i, Helper.ClearAllSpaces(it.Values[kvp.Key].Replace(",", ".")));
                        i++;
                    }
                }
            }
            return res;
        }


        public static async Task<TradeInitValues> GetTradesIntitValuesAsync(string ticker)
        {
            string issuer_id = await SqlUtiltes.GetIssuerIdAsync(ticker);
            var min_max = await _GetMaxMinDate(issuer_id);
            var start_end = await _GetStartEndDate(issuer_id);

            return new TradeInitValues
            {
                minrd = min_max.Item1,
                maxrd = min_max.Item2,
                dstart = start_end.Item1,
                dend = start_end.Item2,
                exchange_list = await _GetExchangeList(issuer_id),
                issues_list = await _GetIssuesList(issuer_id)
            };
        }

        private static async Task<Tuple<string,string>> _GetMaxMinDate(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_trades_get_maxmindate", con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if(await reader.ReadAsync())
                {
                    return new Tuple<string, string>(reader.ReadEmptyIfDbNull("minrd"), reader.ReadEmptyIfDbNull("maxrd"));
                }
                return new Tuple<string, string>("", "");
            }
        }

        private static async Task<Tuple<string, string>> _GetStartEndDate(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_trades_get_start_end_date", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (await reader.ReadAsync())
                {
                    return new Tuple<string, string>(reader.ReadEmptyIfDbNull("dstart"), reader.ReadEmptyIfDbNull("dend"));
                }
                return new Tuple<string, string>("", "");
            }
        }

        private static async Task<List<InitVal>> _GetExchangeList(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_trades_get_exchange_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                var ret = new List<InitVal>();
                while (await reader.ReadAsync())
                {
                    ret.Add(new InitVal
                    {
                        id = (string)reader["id"],
                        name = (string)reader["name"],
                        exists = (int)reader["ex"] == 1
                    });
                }
                return ret;
            }
        }

        private static async Task<List<InitVal>> _GetIssuesList(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..content_trades_get_issues_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                var ret = new List<InitVal>();
                while (await reader.ReadAsync())
                {
                    ret.Add(new InitVal
                    {
                        id = (string)reader["id"],
                        name = (string)reader["name"],
                        exists = (int)reader["ex"] == 1
                    });
                }
                return ret;
            }
        }

        private static async Task<Dictionary<int, string>> _GetHeaders(Currency currency)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select distinct  name,  
                        case when price_flag=1 and abs(id) in (4,5,6,16,17,21,20,23,25,26,28) then '*' else '' end as note, no, price_flag
                         from vStock_Indics_plus order by no";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                var ret = new Dictionary<int, string>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string header=(string)reader["name"];
                    if((int)reader["price_flag"]==1)
                    {
                        header += currency == Currency.RUB ? ", руб" : ", $";
                        header += (string)reader["note"];
                    }
                    ret.Add((int)reader["no"], header);
                }
                return ret;
            }
        }

        private static string _GetExchageWhere(List<Exchange> exchange_list)
        {
            if(exchange_list.Count==0)
                return "and ((v.trade_place_id in (10,1,2,11) and v.trade_place2_id!=2) or (v.trade_place_id = 10 and v.trade_place2_id = 2))";
            var ids = new List<int>();
            if (exchange_list.Contains(Exchange.RTS))
            {
                ids.Add(10);
                ids.Add(1);
            }
            if (exchange_list.Contains(Exchange.MICEX))
            {
                ids.Add(2);
            }
            if (exchange_list.Contains(Exchange.LSE))
            {
                ids.Add(11);
            }
            string ret = "";
            if (ids.Count > 0)
                ret += string.Format("(v.trade_place_id in ({0}) and v.trade_place2_id!=2)", string.Join(",", ids));
            if(exchange_list.Contains(Exchange.RTSBoard))
            {
                if(ret=="")
                {
                    return "and (v.trade_place_id = 10 and v.trade_place2_id = 2)";
                }
                return string.Format("and ({0} or (v.trade_place_id = 10 and v.trade_place2_id = 2))", ret);
            }
            return "and " + ret;
        }

        private static string _GetIssuesWhere(List<IssueType> issues_list)
        {
            if(issues_list.Count==0)
            {
                return ("and (t.issue_type_id in (0,1,3))");
            }
            List<string> ret = new List<string>();
            if(issues_list.Contains(IssueType.OrdinaryShare) && issues_list.Contains(IssueType.PreferenceShare))
            {
                ret.Add("t.issue_type_id = 0");
            }
            else
            {
                if(issues_list.Contains(IssueType.OrdinaryShare))
                {
                    ret.Add("(t.issue_type_id = 0 and t.sec_type_id IN (select id from naufor..Sec_Types_O_Shares))");
                }
                if(issues_list.Contains(IssueType.PreferenceShare))
                {
                    ret.Add("(t.issue_type_id = 0 and t.sec_type_id IN (select id from naufor..Sec_Types_P_Shares))");
                }
            }
            if(issues_list.Contains(IssueType.Bond))
            {
                ret.Add("t.issue_type_id = 1");
            }
            if(issues_list.Contains(IssueType.DepositaryReceipt))
            {
                ret.Add("t.issue_type_id = 3");
            }
            return string.Format("and ({0})", string.Join(" or ", ret));
        }

        private static string _GetDateWhere(DateTime? sDate, DateTime? tDate)
        {
            string ret="";
            if(sDate!=null)
            {
                ret+=string.Format(" and v.reg_date>='{0}'",sDate.Value.ToString("yyyyMMdd"));
            }
            if(tDate!=null)
            {
                ret += string.Format(" and v.reg_date<='{0}'", tDate.Value.ToString("yyyyMMdd"));
            }
            return ret;
        }

    }
}