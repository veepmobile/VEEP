using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Skrin.Models;
using Skrin.Models.Pravo;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Messages
{
    public class PravoSearchQueryGenerator
    {
        private PravoSObject _so;

        public PravoSearchQueryGenerator(PravoSObject so)
        {
            _so = so;
        }

        public SphinxQueryObject GetQuery()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Count = (_so.page < 0) ? 10000 : _so.rcount;
            qo.Skip = (_so.page < 0) ? -1 : (int)_so.page * qo.Count;
            string query_pattern = "";
            string sel = " case_type,reg_date,reg_no,cname,case_id,ext_ist_list,ext_otv_list,reg_date_ts,ext_third_list, ext_over_list, upd_date, disput_name, disput_type_categ ";

            query_pattern = "select" + sel + "from pravo4 where match (' _all_ {0}') {1} order by reg_date_ts desc limit {2},{3} OPTION max_matches=10000; show meta;";
            if (_so.page < 0)
            {
                var dfrom = (_so.ins_DBeg_ts > 0) ? " where reg_date_ts >= " + _so.ins_DBeg_ts.ToString() : "";
                var dto = (_so.ins_DEnd_ts > 0) ? " and reg_date_ts <= " + _so.ins_DEnd_ts.ToString() : "";
                query_pattern = "select case_type,reg_date,reg_no,cname,case_id,ext_ist_list,ext_otv_list,reg_date_ts,ext_third_list, ext_over_list, upd_date, disput_name, disput_type_categ from pravo4" + dfrom + " " + dto + " order by reg_date_ts desc limit 0,20 OPTION max_matches=10000; show meta;";
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
            if (!String.IsNullOrEmpty(_so.search_txt) && _so.search_txt != "**")
            {
                if (String.IsNullOrEmpty(_so.side_type))
                {
                    ret.Match += "\"" + _so.search_txt + "\"";
                }
                else
                {
                    ret.Match += SearchName(_so.search_txt);
                }
            }

            if (!String.IsNullOrEmpty(_so.job_no))
            {
                ret.Match += " @reg_no_list \"" + _so.job_no + "\"";
            }

            if (!String.IsNullOrEmpty(_so.ac_type) && _so.ac_type != "-1")
            {
                if (_so.ac_type_excl == 0)
                {
                    ret.Where += " and court_id in (" + _so.ac_type + ")";
                }
                else
                {
                    ret.Where += " and court_id not in (" + _so.ac_type + ")";
                }
            }

            if (!String.IsNullOrEmpty(_so.disput_type))
            {
                if (_so.disput_type_excl == 0)
                {
                    ret.Where += " and disput_type in (" + _so.disput_type + ")";
                }
                else
                {
                    ret.Where += " and disput_type not in (" + _so.disput_type + ")";
                }
            }

            if (!String.IsNullOrWhiteSpace(_so.ins_DBeg))
            {
                ret.Where += " and reg_date_ts >= " + _so.ins_DBeg_ts.ToString();

            }

            if (!String.IsNullOrWhiteSpace(_so.ins_DEnd))
            {
                ret.Where += " and reg_date_ts <= " + _so.ins_DEnd_ts.ToString();
            }


            if (!String.IsNullOrWhiteSpace(_so.last_DBeg))
            {
                ret.Where += " and update_date_ts >= " + _so.last_DBeg_ts.ToString();

            }

            if (!String.IsNullOrWhiteSpace(_so.last_DEnd))
            {
                ret.Where += " and update_date_ts <= " + _so.last_DEnd_ts.ToString();

            }

            if (_so.grp != 0)
            {
                string ids = IssuersList(_so.grp);
                if (ids != null)
                {
                    var str = _so.side_type.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Distinct();
                    if (str.Count() > 0)
                    {
                        var _fld = "";
                        foreach (var item in str)
                        {
                            switch (item)
                            {
                                //case "-1":
                                //    ret.Match += "@(ext_ist_list,ext_otv_list,ext_third_list,ext_over_list) " + ids;
                                //    break;
                                case "0":
                                    _fld += "ext_ist_list,";
                                    break;
                                case "1":
                                    _fld += "ext_otv_list,";
                                    break;
                                case "2":
                                    _fld += "ext_third_list,";
                                    break;
                                case "3":
                                    _fld += "ext_over_list,";
                                    break;
                            }
                        }
                        ret.Match += "@(" + _fld.Substring(0, _fld.Length - 1) + ") " + ids;
                    }
                    else {
                        ret.Match += "@(ext_ist_list,ext_otv_list,ext_third_list,ext_over_list) " + ids;
                    }
                }
            }
            return ret;
        }

        private string SearchName(string txt)
        {
            string ret = "";

            if (!String.IsNullOrEmpty(txt) && txt != "**")
            {
                txt = txt.Replace("*", "");
                txt = Helper.FullTextString(txt);
                txt = txt.Replace("|", "\"|\"");
                var str = _so.side_type.Split(',').Distinct();
                if (str.Count() > 0)
                {
                    foreach (var item in str)
                    {
                        switch (item)
                        {
                            case "-1":
                                ret += " (\"" + txt + "\") ";
                                break;
                            case "0":
                                ret += (_so.side_type_excl == 0) ? " @ext_ist_list (\"" + txt + "\") " : " @ext_ist_list !(\"" + txt + "\") ";
                                break;
                            case "1":
                                ret += (_so.side_type_excl == 0) ? " @ext_otv_list (\"" + txt + "\") " : " @ext_otv_list !(\"" + txt + "\") ";
                                break;
                            case "2":
                                ret += (_so.side_type_excl == 0) ? " @ext_third_list (\"" + txt + "\") " : " @ext_third_list !(\"" + txt + "\") ";
                                break;
                            case "3":
                                ret += (_so.side_type_excl == 0) ? " @ext_over_list (\"" + txt + "\") " : " @ext_over_list !(\"" + txt + "\") ";
                                break;
                        }
                    }
                }
                return ret;
            }
            else
            {
                return "";
            }
        }


        public SphinxQueryObject GetDBeg()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Query = "select reg_date from pravo4 order by reg_date_ts desc limit 0,1";
            return qo;
        }

        public SphinxQueryObject GetCount()
        {
            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Query = "select count(*) as cnt from pravo4";
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