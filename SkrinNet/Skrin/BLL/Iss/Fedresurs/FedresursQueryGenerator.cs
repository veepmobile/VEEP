using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Iss
{
    public class FedresursQueryGenerator
    {
        private FedresursSearchObject _so;
        private CompanyData _company;

        public FedresursQueryGenerator(FedresursSearchObject so, CompanyData company)
        {
            _so = so;
            _company = company;
        }

        public QueryObject GetQueryTypes(FedresursSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9421;
            qo.Port = Convert.ToInt32(Configs.SphinxFedresursPort);
            qo.CharasterSet = "utf8";
            qo.OrderField = "type_id";
            qo.OrderType = "asc";
            qo.OrderFieldType = "int";
            qo.Count = 10000;
            qo.Skip = 0;
            string query_pattern = "select type_id, type_name from idx_fedresurs_skrin2 where match ('{0}') group by type_id order by type_id asc";
            SphinxCondition cond = _GetCondition(_company, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match));
            return qo;
        }

        public QueryObject GetQueryDates(FedresursSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9421;
            qo.Port = Convert.ToInt32(Configs.SphinxFedresursPort);
            qo.CharasterSet = "utf8";
            qo.Count = 10;
            qo.Skip = 0;
            string query_pattern = "select min(search_pub_date) minDate, max(search_pub_date) maxDate from idx_fedresurs_skrin2 where match ('{0}') {1} order by search_pub_date desc";
            SphinxCondition cond = _GetCondition(_company, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        public QueryObject GetQuerySearch(FedresursSearchObject so)
        {
            QueryObject qo = new QueryObject();
            //qo.Port = 9421;
            qo.Port = Convert.ToInt32(Configs.SphinxFedresursPort);
            qo.CharasterSet = "utf8";
            qo.Count = 20;
            qo.Skip = (so.page - 1) * 20;
            string query_pattern = "SELECT id, show_pub_date, type_name, type_id, search_pub_date, companies from idx_fedresurs_skrin2 where match ('{0}') {1} order by search_pub_date desc limit {2},{3} OPTION max_matches=10000; show meta;";
            SphinxCondition cond = _GetCondition(_company, so);
            qo.Queries.Add(string.Format(query_pattern, cond.Match, cond.Where, qo.Skip, qo.Count));
            return qo;
        }

        private SphinxCondition _GetCondition(CompanyData company, FedresursSearchObject so = null)
        {
            SphinxCondition ret = new SphinxCondition();

            string search = "";
            if (!string.IsNullOrEmpty(so.kw))
            {
                search += " @search_text \"" + Helper.FullTextString(so.kw.Trim().Clear()) + "\"";
            }
            if (!string.IsNullOrEmpty(_company.OGRN) && _company.OGRN != "")
            {
                search += " @search_name (\"" + _company.OGRN + "\"";
            }
            if (!string.IsNullOrEmpty(_company.INN) && _company.INN != "")
            {
                search += " | \"" + _company.INN + "\"";
            }
            search += ")";

            ret.Match = search;

            if (!string.IsNullOrEmpty(so.dfrom))
            {
                so.dfrom = so.dfrom + " 00:00:00";
                ret.Where += " and search_pub_date >=" + so.dfrom.UnixDateTimeStamp();
            }
            if (!string.IsNullOrEmpty(so.dto))
            {
                so.dto = so.dto + " 23:59:00";
                ret.Where += " and search_pub_date <=" + so.dto.UnixDateTimeStamp();
            }
            if (!string.IsNullOrEmpty(so.type))
            {
                ret.Where += " and type_id in (" + so.type + ")";
            }

            return ret;
        }
    }
}