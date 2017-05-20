using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;


namespace Skrin.BLL.Iss.Zakupki
{
    public class ZakupkiQueryGenerator
    {
        private ZakupkiSearchObject _so;
        private int _p_size = 100;

        public string SearchString { get; set; }

        public ZakupkiQueryGenerator(ZakupkiSearchObject so)
        {
            _so = so;
        }


        private string _GenerateMainMatch()
        {
            if ((_so.IsSup == true && _so.IsCus == true && _so.IsPar == true) || (_so.IsSup == false && _so.IsCus == false && _so.IsPar == false))
                return "supl_short_search,cust_short_search,cust_not_short_search,part_short_search";
            List<string> ret = new List<string>();
            if (_so.IsCus)
            {
                ret.Add("cust_short_search");
                ret.Add("cust_not_short_search");
            }
            if (_so.IsSup)
                ret.Add("supl_short_search");
            if (_so.IsPar)
                ret.Add("part_short_search");
            return String.Join(",", ret);
        }

        public string GetSearchString()
        {
            string pattern = @"select notification_id,contract_orig_id,not_publish_date,not_product, source,pur_num,lot_num,not_sum,dif_sum,dif_per,st_not_sum,region_name, not_type,not_status_name,not_cust_json,part_json,contr_data_json   
                                from zakupkiZ_all where match('@({3}) {0}') {1} 
                                group by group_id
                                order by sort_publish_date_ts desc
                                limit " + ((_so.isAll == 1) ? "10000 OPTION max_matches=10000;" : "{2} {4};") + " show meta;";
            SphinxCondition cond = _GetCondition();
            return string.Format(pattern, cond.Match, cond.Where, _so.page > 0 ? (_so.page * _p_size).ToString() + "," : "", _GenerateMainMatch(), _p_size.ToString());
        }

        public string GetSearchAllString()
        {
            string pattern = @"select notification_id,contract_orig_id,not_publish_date,not_product, source,pur_num,lot_num,not_sum,dif_sum,dif_per,st_not_sum,region_name, not_type,not_status_name,not_cust_json,part_json,contr_data_json   
                                from zakupkiZ_all where match('@({2}) {0}') {1} 
                                group by group_id
                                order by sort_publish_date_ts desc
                                limit 10000 OPTION max_matches=10000; show meta;";
            SphinxCondition cond = _GetCondition();
            return string.Format(pattern, cond.Match, cond.Where, _GenerateMainMatch());
        }

        public List<string> GetUnionSearchStrings()
        {
            string pattern = @"select notification_id,contract_orig_id,not_publish_date,not_product, source,pur_num,lot_num,not_sum,dif_sum,dif_per,st_not_sum,region_name, not_type,not_status_name,not_cust_json,part_json,contr_data_json,sort_publish_date_ts   
                                from zakupkiZ_all where match('@({2}) {0}') {1} 
                                group by group_id ";
            List<string> ret = new List<string>();
            for (int i = 1; i < 3; i++)
            {
                SphinxCondition c = _GetUnionCondition(i);
                ret.Add(string.Format(pattern, c.Match, c.Where, _GenerateMainMatch()));
            }
            return ret;
        }

        private SphinxCondition _GetUnionCondition(int type)
        {
            SphinxCondition ret = new SphinxCondition();

            ret.Where = "";
            ret.Match = _so.inn;

            if (!string.IsNullOrEmpty(_so.contragent))
            {
                if (_so.is_contrname)
                    ret.Match += " @(supl_short_search,cust_short_search,cust_not_short_search,part_short_search)  " + _so.contragent.Clear();
                else
                {
                    ret.Match += " @(supl_okved,cust_okved,cust_not_okved,part_okved) " + ZakupkiRepository.GetCodeAsync(_so.contragent, ZakupkiCodeType.Okved, _so.contragent_excl == 1);
                }
            }

            if (!string.IsNullOrEmpty(_so.dfrom))
            {
                ret.Where += string.Format(" and {0} >=" + _so.dfrom.UnixTimeStamp(), type == 1 ? "contract_publish_date_ts" : "not_publish_date_ts");
            }
            if (!string.IsNullOrEmpty(_so.dto))
            {
                ret.Where += string.Format(" and {0} <=" + _so.dto.UnixTimeStamp(), type == 1 ? "contract_publish_date_ts" : "not_publish_date_ts");
            }

            List<int> sources = new List<int>();

            if (_so.Is44)
                sources.Add(3);
            if (_so.Is94)
                sources.Add(1);
            if (_so.Is223)
                sources.Add(2);


            if (sources.Count > 0 && sources.Count < 3)
            {
                ret.Where += " and source in (" + String.Join(",", sources) + ") ";
            }

            if (!string.IsNullOrEmpty(_so.reg_num))
            {
                ret.Match += string.Format("( @contr_regnum {0} | @pur_num {0}) ", _so.reg_num);
            }

            if (!string.IsNullOrEmpty(_so.status))
            {
                ret.Where += " and u_status in (" + _so.status + ") ";
            }

            if (!string.IsNullOrEmpty(_so.sfrom))
            {
                ret.Where += string.Format(" and {0} >=" + _so.sfrom + ".00", type == 1 ? "contract_sum" : "not_sum");
            }

            if (!string.IsNullOrEmpty(_so.sto))
            {
                ret.Where += string.Format(" and {0} <=" + _so.sto + ".00", type == 1 ? "contract_sum" : "not_sum");
            }

            if (!string.IsNullOrEmpty(_so.product))
            {
                if (_so.is_product)
                    ret.Match += " @product_search " + _so.product.Clear();
                else
                    ret.Match += " @okdp_list " + ZakupkiRepository.GetCodeAsync(_so.product, ZakupkiCodeType.Okdp, _so.product_excl == 1);
            }
            return ret;
        }

        private SphinxCondition _GetCondition()
        {
            SphinxCondition ret = new SphinxCondition();

            ret.Where = "";
            ret.Match = _so.inn;

            if (!string.IsNullOrEmpty(_so.contragent))
            {
                if (_so.is_contrname)
                    ret.Match += " @(supl_short_search,cust_short_search,cust_not_short_search,part_short_search)  " + _so.contragent.Clear();
                else
                {
                    ret.Match += " @(supl_okved,cust_okved,cust_not_okved,part_okved) " + ZakupkiRepository.GetCodeAsync(_so.contragent, ZakupkiCodeType.Okved, _so.contragent_excl == 1);
                }
            }



            List<int> sources = new List<int>();

            if (!((_so.Is44 == true && _so.Is94 == true && _so.Is223 == true) || (_so.Is44 == false && _so.Is94 == false && _so.Is223 == false))) //если не все true или все false
            {
                if (_so.Is44)
                    sources.Add(3);
                if (_so.Is94)
                    sources.Add(1);
                if (_so.Is223)
                    sources.Add(2);

                ret.Where += " and source in (" + String.Join(",", sources) + ") ";
            }

            if (!string.IsNullOrEmpty(_so.reg_num))
            {
                ret.Match += string.Format("( @contr_regnum {0} | @pur_num {0}) ", _so.reg_num);
            }

            if (!string.IsNullOrEmpty(_so.status))
            {
                ret.Where += " and u_status in (" + _so.status + ") ";
            }


            if (!string.IsNullOrEmpty(_so.product))
            {
                if (_so.is_product)
                    ret.Match += " @product_search " + _so.product.Clear();
                else
                    ret.Match += " @okdp_list " + ZakupkiRepository.GetCodeAsync(_so.product, ZakupkiCodeType.Okdp, _so.product_excl == 1);
            }
            return ret;
        }
    }
}