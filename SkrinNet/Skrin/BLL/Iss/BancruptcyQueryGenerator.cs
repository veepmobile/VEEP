using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Iss
{
    public class BancruptcyQueryGenerator
    {
        private SearchObject _so;
        private CompanyData _company;

        public BancruptcyQueryGenerator(SearchObject so, CompanyData company)
        {
            _so = so;
            _company = company;
        }

        public QueryObject GetQueryTypes(SearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9385;
            qo.Port = Convert.ToInt32(Configs.SphinxBankruptcyPort);
            qo.CharasterSet = "utf8";
            qo.OrderField = "mes_type";
            qo.OrderType = "asc";
            qo.OrderFieldType = "int";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "select mes_type, source, type_name as headers from bankruptcy where match ('{0}') and source=1 group by mes_type order by mes_type asc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }

        public QueryObject GetQueryDates(SearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9385;
            qo.Port = Convert.ToInt32(Configs.SphinxBankruptcyPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10;
            qo.Skip = 0;
            string query_pattern = "select min(reg_date_ts) minDate, max(reg_date_ts) maxDate from bankruptcy where match ('{0}') {1} order by reg_date_ts desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        public QueryObject GetQueryAllIds(SearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9385;
            qo.Port = Convert.ToInt32(Configs.SphinxBankruptcyPort);
            qo.CharasterSet = "utf8";
            qo.Count = 1000;
            qo.Skip = 0;
            string query_pattern = "select num as orig_id,tab from bankruptcy where match ('{0}') {1} order by reg_date_ts desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        public QueryObject GetQuerySearch(SearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9385;
            qo.Port = Convert.ToInt32(Configs.SphinxBankruptcyPort);
            qo.CharasterSet = "utf8";
            qo.Count = 20;
            qo.Skip = (so.page - 1) * 20;
            string query_pattern = "select num as orig_id,source_name,type_name,headers,reg_date,tab,reg_date_ts from bankruptcy where match ('{0}') {1} order by reg_date_ts desc";
            SphinxCondition cond = _GetCondition(_company.SearchedName, _company.SearchedName2, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        private SphinxCondition _GetCondition(string search_name, string search_name2, SearchObject so = null)
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
                    ret.Match = so.kw + " @code (" + codes + ") ";
                }
                else //поиск по названию
                {
                    ret.Match = search + so.kw + codes;
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
                if (!string.IsNullOrEmpty(so.type))
                {
                    ret.Where += " and mes_type in (" + so.type + ")";
                }
                if (so.src != 0)
                {
                    ret.Where += " and source =" + so.src;
                }

                if (so.isCompany)
                {
                    if (so.iss != "")
                    {
                        ret.Where += " and bankrupt_type in (0,1)";
                    }
                }
                else
                {
                    ret.Where += " and bankrupt_type in (0,2)";
                }
            }

            return ret;
        }
    }
}