using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Skrin.Models;
using Skrin.Models.Disclosure;
using Skrin.BLL.Infrastructure;
using Skrin.Models;
using Skrin.Models.Iss.Content;

namespace Skrin.BLL.Messages
{
    public class DisclosureQueryGenerator
    {
        private DisclosureSearchObject _so;

        public DisclosureQueryGenerator(DisclosureSearchObject so)
        {
            _so = so;
        }

        public SphinxQueryObject GetQuery()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9440;
            qo.Port = Convert.ToInt32(Configs.SphinxDisclosurePort);
            qo.CharasterSet = "utf8";
            qo.Count = (_so.page < 0) ? 10000 : _so.rcount;
            qo.Skip = (_so.page < 0) ? -1 : (int)_so.page * qo.Count;

            string query_pattern = "select id,news_id,agency_id,reg_date_date,reg_date,reg_date_ts,event_type_id,headline,name,ticker,title from disclosure where match (' _all_ {0}') {1} order by reg_date_ts desc limit {2},{3} OPTION max_matches=10000; show meta;";

            if (_so.page < 0)
            {
                //var dfrom = (_so.DBeg_ts > 0) ? " where reg_date_ts >= " + _so.DBeg_ts.ToString() : "";
                //var dto = (_so.DEnd_ts > 0) ? " and reg_date_ts <= " + _so.DEnd_ts.ToString() : "";
                //query_pattern = "select * from disclosure" + dfrom + " " + dto + " order by reg_date_ts desc limit 0,20 OPTION max_matches=10000; show meta;";
                query_pattern = "select id,news_id,agency_id,reg_date_date,reg_date,reg_date_ts,event_type_id,headline,name,ticker,title from disclosure order by reg_date_ts desc limit 0,20 OPTION max_matches=10000; show meta;";
            }


            SphinxCondition cond = _GetCondition();
            qo.Query = string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count);

            return qo;
        }

        private SphinxCondition _GetCondition()
        {
            SphinxCondition ret = new SphinxCondition();
            ret.Match = "";
            ret.Where = "";

            if (!String.IsNullOrEmpty(_so.search_text))
            {
                ret.Match += "\"" + _so.search_text + "\"";
            }

            if (!String.IsNullOrEmpty(_so.search_name))
            {
                ret.Match += " @search_name \"" + _so.search_name + "\"";
            }


            string types = DisclosureRepository.GetDisclosureTypes(_so.type_id);
            if (!String.IsNullOrEmpty(types))
            {
                if (_so.types_excl == 0)
                {
                    ret.Where += " and event_type_id in (" + types + ")";
                }
                else
                {
                    ret.Where += " and event_type_id not in (" + types + ")";
                }
            }

            if (!String.IsNullOrWhiteSpace(_so.DBeg))
            {
                ret.Where += " and reg_date_ts >= " + _so.DBeg_ts.ToString();

            }

            if (!String.IsNullOrWhiteSpace(_so.DEnd))
            {
                ret.Where += " and reg_date_ts <= " + _so.DEnd_ts.ToString();
            }

            if (_so.grp != 0)
            {
                string ids = IssuersList(_so.grp);
                if (ids != null)
                {
                    ret.Match += " @ticker " + ids;
                }
            }

            return ret;
        }

        public SphinxQueryObject GetDBeg()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9440;
            qo.Port = Convert.ToInt32(Configs.SphinxDisclosurePort);
            qo.CharasterSet = "utf8";
            qo.Query = "select reg_date as dt from disclosure order by reg_date_ts desc limit 0,1";
            return qo;
        }

        public SphinxQueryObject GetCount()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9440;
            qo.Port = Convert.ToInt32(Configs.SphinxDisclosurePort);
            qo.CharasterSet = "utf8";
            qo.Query = "select count(*) as cnt from disclosure";
            return qo;
        }

        //Группы компаний
        private string IssuersList(int group_id)
        {
            if (group_id == 0)
                return null;

            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    //string sql = "select u.id from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id";
                    string sql = "SELECT STUFF((select '|' + CAST(u.ticker as nvarchar(32)) from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id FOR XML PATH('')),1,1,'')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@group_id", SqlDbType.BigInt).Value = group_id;
                    cmd.CommandTimeout = 300;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = (string)reader[0];
                    }
                }
                if (ret != "")
                {
                    return ret;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}