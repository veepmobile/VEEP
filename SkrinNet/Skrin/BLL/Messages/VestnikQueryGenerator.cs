using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Skrin.Models;
using Skrin.Models.Disclosure;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using Skrin.Models.Vestnik;

namespace Skrin.BLL.Messages
{
    public class VestnikSearchQueryGenerator
    {
        private VestnikSObject _so;

        public VestnikSearchQueryGenerator(VestnikSObject so)
        {
            _so = so;
        }

        public SphinxQueryObject GetQuery()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Count = (_so.page < 0) ? 10000 : _so.rcount;
            qo.Skip = (_so.page < 0) ? -1 : (int)_so.page * qo.Count;

            string query_pattern = "select id,event_id,type_id,file_date,type_name,search_kodes,name,ticker from idx_vestnik where match ('{0}') {1}  order by search_file_date desc limit {2},{3} OPTION max_matches=10000; show meta;";

            if (_so.page < 0)
            {
                query_pattern = "select id,event_id,type_id,file_date,type_name,search_kodes,name,ticker from idx_vestnik order by search_file_date desc limit 0,20 OPTION max_matches=10000; show meta;";
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
                ret.Match += " @search_content \"" + _so.search_name + "\"";
                ret.Match += " @search_kodes \"" + _so.search_name + "\"";
            }


            string types = VestnikSearchRepository.GetVestnikTypes(_so.types);
            if (!String.IsNullOrEmpty(types))
            {
                if (_so.types_excl == 0)
                {
                    ret.Where += " and type_id in (" + types + ")";
                }
                else
                {
                    ret.Where += " and type_id not in (" + types + ")";
                }
            }

            if (!String.IsNullOrWhiteSpace(_so.DBeg))
            {
                ret.Where += " and search_file_date >= " + _so.DBeg_ts.ToString();

            }

            if (!String.IsNullOrWhiteSpace(_so.DEnd))
            {
                ret.Where += " and search_file_date <= " + _so.DEnd_ts.ToString();
            }

            if (_so.grp != 0)
            {
                string ids = IssuersList(_so.grp);
                if (ids != null)
                {
                    ret.Match += (!String.IsNullOrEmpty(_so.search_name) ? " " : " @search_kodes ") + ids;
//                    ret.Match +=  " @search_kodes " + ids;
                }
            }

            return ret;
        }

        public SphinxQueryObject GetDBeg()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Query = "select file_date as dt from idx_vestnik order by search_file_date desc limit 0,1";
            return qo;
        }

        public SphinxQueryObject GetCount()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Query = "select count(*) as cnt from idx_vestnik";
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
                    string sql = "SELECT STUFF((select '|' + CAST(u.inn as nvarchar(32)) + '|' + CAST(u.ogrn as nvarchar(32)) + '|\"' + CAST(u.Bones as nvarchar(1024)) + '\"' from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id FOR XML PATH('')),1,1,'')";
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