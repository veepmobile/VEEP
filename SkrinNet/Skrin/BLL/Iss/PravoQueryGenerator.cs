using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Iss
{
    public class PravoQueryGenerator
    {
        private PravoSearchObject _so;
        private CompanyData _company;

        public PravoQueryGenerator(PravoSearchObject so, CompanyData company)
        {
            _so = so;
            _company = company;
        }

        public QueryObject GetQueryCourts(PravoSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "select cname from pravo4 where match('{0}') group by cname order by cname asc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }

        public QueryObject GetQueryDtypes(PravoSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9306;
            //qo.CharasterSet = "cp1251";
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "SELECT ctype_id, ctype_name from pravo4_types where match('{0}') group by ctype_id order by ctype_id asc";
            SphinxCondition cond = _GetConditionTypes(_company.SearchedName, _company.SearchedName2);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }

        public QueryObject GetPravoQuerySearch(PravoSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Count = 20;
            qo.Skip = (so.page - 1) * 20;
            string query_pattern = "SELECT reg_date,reg_no,cname,case_id,ext_ist_list,ext_otv_list,reg_date_ts,ext_third_list, ext_over_list,s_case_sum, disput_type_categ,case_type  from pravo4 where match ('{0}') {1} order by reg_date_ts desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        public QueryObject GetPravoIpQuerySearch(PravoSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9311;
            qo.Port = Convert.ToInt32(Configs.SphinxPravoPort);
            qo.CharasterSet = "utf8";
            qo.Count = 20;
            qo.Skip = (so.page - 1) * 20;
            string query_pattern = "SELECT reg_date,reg_no,cname,case_id,ext_ist_list,ext_otv_list,reg_date_ts,ext_third_list, ext_over_list,s_case_sum, disput_type_categ,case_type  from pravo4 where match ('{0}') {1} order by reg_date_ts desc";
            //string query_pattern = "SELECT reg_date,reg_no,cname,case_id,ext_ist_list,ext_otv_list,reg_date_ts,ext_third_list, ext_over_list,s_case_sum, disput_type_categ,case_type  from pravo4 where match ('\"{0}\"') {1} order by reg_date_ts desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }


        private SphinxCondition _GetCondition(string search_name, string search_name2, PravoSearchObject so = null)
        {
            SphinxCondition ret = new SphinxCondition();

            search_name = search_name.Clear().Replace("*", "");
            string search = " (\"" + Helper.FullTextString(search_name) + "\"";
            if (!string.IsNullOrEmpty(search_name2))
            {
                search_name2 = search_name2.Clear().Replace("*", "");
                search += " | \"" + Helper.FullTextString(search_name2) + "\"";
            }
            search += ") ";

            if (so == null)
            {
                ret.Match = search;
            }
            else
            {
                if (!string.IsNullOrEmpty(so.kw))
                {
                    so.kw = " \"" + so.kw.Clear() + " \"";
                }
                string codes = "";
                if (!string.IsNullOrEmpty(_company.INN))
                {
                    codes += " | \"" + _company.INN + "\"";
                }
                if (!string.IsNullOrEmpty(_company.OGRN))
                {
                    codes += " | \"" + _company.OGRN + "\"";
                }
                if (so.mode == 1) //поиск по ИНН и ОГРН
                {
                    if (codes.IndexOf("|") == 1)
                    {
                        codes = codes.Substring(2, codes.Length - 2);
                    }
                    if (!string.IsNullOrEmpty(so.kw))
                    {
                        ret.Match = SearchName(so.kw + "(" + codes + ")");
                    }
                    else
                    {
                        ret.Match = SearchName(codes);
                    }
                }
                else //поиск по названию
                {
                    ret.Match = SearchName(search + so.kw);
                }

                if (!string.IsNullOrEmpty(so.dfrom))
                {
                    so.dfrom = so.dfrom + " 00:00:00";
                    ret.Where += " and reg_date_ts >=" + so.dfrom.UnixDateTimeStamp();
                }
                if (!string.IsNullOrEmpty(so.dto))
                {
                    so.dto = so.dto + " 23:59:00";
                    ret.Where += " and reg_date_ts <=" + so.dto.UnixDateTimeStamp();
                }

                if (!String.IsNullOrEmpty(_so.job_no))
                {
                    ret.Match += " @reg_no_list \"" + _so.job_no + "\"";
                }

                if (!String.IsNullOrEmpty(_so.ac_name) && _so.ac_name != "-1" && _so.ac_name != "undefined")
                {
                    ret.Where += " and cname='" + _so.ac_name + "'";
                }

                if (!String.IsNullOrEmpty(_so.dcateg_id) && _so.dcateg_id != "-1")
                {
                    ret.Where += " and disput_type_categ=" + _so.dcateg_id;
                }
            }
            return ret;
        }

        private SphinxCondition _GetConditionTypes(string search_name, string search_name2)
        {
            SphinxCondition ret = new SphinxCondition();
            ret.Match = " \"" + search_name + "\"";
            if (!string.IsNullOrEmpty(search_name2))
            {
                ret.Match += " | \"" + search_name2 + "\"";
            }
            return ret;
        }

        private string SearchName(string txt)
        {
            string ret = "";

            if (!String.IsNullOrEmpty(txt) && txt != "**")
            {
               // txt = Helper.FullTextString(txt);
                txt = txt.Replace("*", "").Replace("(","").Replace(")","");
                        switch (_so.dtype)
                        {
                            case "-1":
                                ret += " " + txt + " ";
                                break;
                            case "0":
                                ret += " @ext_ist_list (" + txt + ") ";
                                break;
                            case "1":
                                ret += " @ext_otv_list (" + txt + ") ";
                                break;
                            case "2":
                                ret += " @ext_third_list (" + txt + ") ";
                                break;
                            case "3":
                                ret += " @ext_over_list (" + txt + ") ";
                                break;
                            default:
                                ret += txt;
                                break;
                        }
                return ret;
            }
            else
            {
                return "";
            }

        }

        public static SummaryData GetSummaryData(string ticker)
        {
            SummaryData sumdata = new SummaryData();
            sumdata.YearsData = new List<YearSummary>();
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("pravo.dbo.GetSummaryData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@iss", SqlDbType.VarChar).Value = ticker;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        YearSummary ydata = new YearSummary();
                        ydata.year = (int)reader["year"];
                        ydata.ocnt = (reader["ocnt"] != DBNull.Value) ? (int)reader["ocnt"] : 0;
                        ydata.osumma = (reader["osumma"] != DBNull.Value) ? (decimal)reader["osumma"] : 0;
                        ydata.icnt = (reader["icnt"] != DBNull.Value) ? (int)reader["icnt"] : 0;
                        ydata.isumma = (reader["isumma"] != DBNull.Value) ? (decimal)reader["isumma"] : 0;
                        if (ydata != null)
                        {
                            sumdata.YearsData.Add(ydata);
                        }
                    }
                    return sumdata;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}