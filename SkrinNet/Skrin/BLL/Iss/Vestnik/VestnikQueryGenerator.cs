using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Iss
{
    public class VestnikQueryGenerator
    {
        private VestnikSearchObject _so;
        private CompanyData _company;

        public VestnikQueryGenerator(VestnikSearchObject so, CompanyData company)
        {
            _so = so;
            _company = company;
        }

        public QueryObject GetQuerySearch(VestnikSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Count = 20;
            qo.Skip = (so.page - 1) * 20;
            string query_pattern = "select id,event_id,type_id,file_date,type_name from idx_vestnik where match ('{0}') {1}  order by search_file_date desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }


        public QueryObject GetQueryTypes(VestnikSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "select type_id,type_name from idx_vestnik where match('{0}') group by type_name order by type_name asc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }

        public QueryObject GetQueryDates(VestnikSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9396;
            qo.Port = Convert.ToInt32(Configs.SphinxVestnikPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "select min(search_file_date) minDate, max(search_file_date) maxDate from idx_vestnik where match('{0}')";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }
        

        private SphinxCondition _GetCondition(string search_name, string search_name2, VestnikSearchObject so = null)
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
                    codes += " | " + _company.INN;
                }
                if (!string.IsNullOrEmpty(_company.OGRN))
                {
                    codes += " | " + _company.OGRN;
                }
                if (so.mode == 1) //поиск по ИНН и ОГРН
                {
                    if (codes.IndexOf("|") == 1)
                    {
                        codes = codes.Substring(2, codes.Length - 2);
                    }
                    if (!string.IsNullOrEmpty(so.kw))
                    {
                        ret.Match = " @search_content \"" + so.kw + "\"" + " @search_kodes \"" + codes + "\"";
                    }
                    else
                    {
                        ret.Match = " @search_kodes \"" + codes + "\"";
                    }
                }
                else //поиск по названию
                {
                    ret.Match = " @search_content " + search + " \"" + so.kw + "\"";
                }
                if (!string.IsNullOrEmpty(so.dfrom))
                {
                    so.dfrom = so.dfrom + " 00:00:00";
                    ret.Where += " and search_file_date >=" + so.dfrom.UnixDateTimeStamp();
                }
                if (!string.IsNullOrEmpty(so.dto))
                {
                    so.dto = so.dto + " 23:59:00";
                    ret.Where += " and search_file_date <=" + so.dto.UnixDateTimeStamp();
                }

                if (!string.IsNullOrEmpty(so.type))
                {
                    ret.Where += " and type_id in (" + so.type + ")";
                }
            }

            return ret;
        }

    }
}