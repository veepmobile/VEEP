using Skrin.BLL.Infrastructure;
using Skrin.Models;
using Skrin.Models.Iss.Content;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Skrin.BLL.Search
{
    public class SearchIchpQueryGenerator
    {
        private IchpSearchObject _so;

        public SearchIchpQueryGenerator(IchpSearchObject so)
        {
            _so = so;
        }

        public SphinxQueryObject GetQuery()
        {

            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9378; 
            qo.CharasterSet = "utf8";
            qo.Count = (_so.page_no < 0) ? 10000 : _so.rcount;
            qo.Skip = (_so.page_no < 0) ? 0 : (int)_so.page_no * qo.Count;
            string query_pattern = "";
            //if (_so.page_no == -1001)
            //{
            //    query_pattern = "select fio, ogrnip, inn, okpo, typeip, region, stoping from searchichp2 where match (' _all_ {0}') {1} limit {2},{3} OPTION max_matches=10000; show meta;";
            //}
            //if (_so.page_no >= 0)
            //{
                //query_pattern = "select fio, ogrnip, inn, okpo, typeip, region, stoping from searchichp2 where match (' _all {0}') {1} order by fio asc limit  {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select fio, ogrnip, inn, okpo, typeip, region, stoping from searchichp4 where match (' _all {0}') {1} order by fio asc limit  {2},{3} OPTION max_matches=10000; show meta;";
            //}

            SphinxCondition cond = _GetCondition();
            qo.Query = string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count);

            return qo;
        }

        private SphinxCondition _GetCondition()
        {
            SphinxCondition ret = new SphinxCondition();
            ret.Match = "";
            ret.Where = "";

            if (!string.IsNullOrEmpty(_so.ruler))
            {
                        if (_so.ruler == "-")
                        {
                            _so.ruler = _so.ruler.Replace("-", "\"-\"");
                        }
                        _so.ruler = _so.ruler.Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"");
                        _so.ruler = _so.ruler.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        _so.ruler = _so.ruler.Trim().Replace(" ", " SENTENCE ");
                        ret.Match += " @seacher " + _so.ruler; //по целому слову
            }
            if (_so.group_id != 0)
            {
                string ids = IssuersList(_so.group_id);
                if (ids != null)
                {
                    ret.Where += " and id in (" + ids + ")";
                }
                else
                {
                    ret.Where += " and id in (-9999)";
                }
            }
            if (!string.IsNullOrEmpty(_so.industry))
            {
                if (_so.ind_excl >= 0)
                {
                    ret.Match += " @okved_list (" + OkvedList(_so.industry) + ")";
                }
                else
                {
                    ret.Match += " @okved_list !(" + OkvedList(_so.industry) + ")";
                }
            }

            if (!string.IsNullOrEmpty(_so.regions))
            {
                switch (_so.is_okato)
                {
                    case 0:
                        _so.regions = _so.regions.Replace(",", "|");
                        if (_so.reg_excl >= 0)
                        {
                            ret.Match += " @region_id (" + _so.regions + ")";
                        }
                        else
                        {
                            ret.Match += " @region_id !(" + _so.regions + ")";
                        }
                        break;
                    case 1:
                        if (_so.reg_excl >= 0)
                        {
                            ret.Match += " @okato_full (" + OkatoList(_so.regions) + ")";
                        }
                        else
                        {
                            ret.Match += " @okato_full !(" + OkatoList(_so.regions) + ")";
                        }
                        break;
                }
            }

            return ret;
        }

        private string okato_list = "";

        private string OkatoList(string okato)
        {
            if (!String.IsNullOrEmpty(okato))
            {
                var str = okato.Split(',').Distinct();
                if (str.Count() > 0)
                {
                    foreach (var item in str)
                    {
                        okato_list += item.ToString() + "|";
                    }
                    return okato_list.Substring(0, okato_list.Length - 1);
                }
                else
                {
                    return okato;
                }
            }
            else
            {
                return "";
            }
        }

        private string okved_list = "";

        private string OkvedList(string okveds)
        {
            if (!String.IsNullOrEmpty(okveds))
            {
                var list = okveds.Split('|');
                foreach (var item in list)
                {
                    string str = item.ToString();
                    if (!okved_list.Contains(str))
                    {
                        okved_list += "|\"" + str + "\"";
                    }
                }
                if (okved_list.Length > 0)
                {
                    return okved_list.Substring(1, okved_list.Length - 1);
                }
                else
                {
                    return okveds;
                }
            }
            else
            {
                return "";
            }
        }


        public SphinxQueryObject ExportGetQuery (string issuers)
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9378;
            qo.CharasterSet = "utf8";
            string query_pattern = "";
            query_pattern = "select fio, ogrnip, inn, okpo, typeip, region, stoping from searchichp2 where match (' _all {0}') order by fio asc OPTION max_matches=10000; show meta;";
            SphinxCondition cond = new SphinxCondition();
            cond.Match = issuers;
            qo.Query = string.Format(query_pattern, cond.Match);

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
                    string sql = "SELECT STUFF((select ',' + issuerid  from  security..secUserListItems_Join s  where s.ListID=@group_id and type_id=10 FOR XML PATH('')),1,1,'')";
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