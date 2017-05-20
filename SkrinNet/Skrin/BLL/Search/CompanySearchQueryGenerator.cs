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
using System.Text.RegularExpressions;
using System.Web;

namespace Skrin.BLL.Search
{
    public class CompanySearchQueryGenerator
    {
        private CompaniesSearchObject _so;
        private int _group_limit;

        public CompanySearchQueryGenerator(CompaniesSearchObject so,int group_limit)
        {
            _so = so;
            _group_limit = group_limit;
        }

        public SphinxQueryObject GetQuery()
        {

            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9365; //SearchReq228 основной
            //qo.Port = 9367; //SearchReq228_2 старый
            //qo.Port = 9361; //SearchReq228_2 новый
            //qo.Port = 9362; //SearchReq4 на 194.247.149.45
            //qo.Port = 9363; //SearchReq4 на 194.247.149.42
            qo.Port = Convert.ToInt32(Configs.SphinxSearchreqPort);
            qo.CharasterSet = "utf8";
            qo.Count = _so.rcount;
            qo.Skip = (_so.page_no < 0) ? 0 : (int)_so.page_no * qo.Count;
            if (qo.Skip >= 10000)
            {
                qo.Skip = 9999; 
            }
            string query_pattern = "";
            if (_so.page_no == -1001)
            {
                qo.Count = _group_limit;
                //query_pattern = "select issuer_id, type_id from searchreq2 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                //query_pattern = "select issuer_id, type_id from searchreq3 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select issuer_id, type_id from searchreq4 where match (' _all_ {0}')  {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
            }
            if (_so.page_no == -2001)
            {
                qo.Count = 10000;
                //query_pattern = "select name, nm, inn, region, okpo, okved, ogrn, reg_date, reg_org_name, legal_address, ruler, legal_phone, legal_fax, legal_email, www, del from searchreq2 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                //query_pattern = "select name, nm, inn, region, okpo, okved, ogrn, reg_date, reg_org_name, legal_address, ruler, legal_phone, legal_fax, legal_email, www, del from searchreq3 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select name, nm, inn, region, okpo, okved_code,okved, ogrn, reg_date, reg_org_name, legal_address, ruler, legal_phone, legal_fax, legal_email, www, del from searchreq4 where match (' _all_ {0}')  {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
            }
            if (_so.page_no >= 0)
            {
                //query_pattern = "select * from searchreq2 where match (' _all_ {0}') {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                //query_pattern = "select * from searchreq3 where match (' _all_ {0}') {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select name, nm, search_name, ticker, type_id, issuer_id, ogrn, inn, okpo, okved_code,okved, legal_address, ruler, manager_history, constr, const_list, uniq, isemitent, del, information, region from searchreq4 where match (' _all_ {0}') {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
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

            if (!string.IsNullOrWhiteSpace(_so.company) && _so.company != "**")
            {
                switch (_so.strict)
                {
                    case 3:
                        _so.company = _so.company.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        ret.Match += " @search_name =\"&&" + _so.company + "&&\""; //строгий поиск
                        break;
                    default:
                        if (_so.company == "-")
                        {
                            _so.company = _so.company.Replace("-", "\"-\"");
                        }
                        _so.company = _so.company.Replace("<", "").Replace(">", "").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"").Replace("-"," ");
                        _so.company = _so.company.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        _so.company = Regex.Replace(_so.company.Trim(), "\\s+", " SENTENCE ");
                        ret.Match += " @search_name " + _so.company + " "; //по целому слову
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(_so.ruler))
            {
                _so.ruler = Regex.Replace(_so.ruler.Trim(), "\\s+", " SENTENCE ");
                ret.Match += " @manager_history " + _so.ruler + " ";
            }

            if (!string.IsNullOrWhiteSpace(_so.address))
            {
                //_so.address = _so.address.Replace("г", "").Replace("ул", "").Replace("д", "").Replace(".", "").Replace(",", "");
                //ret.Match += " @legal_address \"" + _so.address + "\"";
                ret.Match += " @legal_address " + _so.address + " ";

            }

            if (!string.IsNullOrWhiteSpace(_so.constitutor))
            {
                _so.constitutor = Regex.Replace(_so.constitutor.Trim(), "\\s+", " SENTENCE ");
                ret.Match += " @const_list " + _so.constitutor + " ";
            }

            if (!string.IsNullOrWhiteSpace(_so.kod))
            {
                ret.Match += " @codes \"" + _so.kod + "\"";
            }

            if (!string.IsNullOrEmpty(_so.industry))
            {
                switch (_so.is_okonh)
                {
                    case 0:
                        if (_so.ind_main == 1)
                        {
                            if (_so.ind_excl >= 0)
                            {
                                ret.Match += " @okved_main (" + OkvedList(_so.industry) + ")";
                            }
                            else
                            {
                                ret.Match += " @okved_main !(" + OkvedList(_so.industry) + ")";
                            }
                        }
                        else
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
                        break;
                    case 1:
                        if (_so.ind_excl >= 0)
                        {
                            ret.Match += " @okonh_list (" + OkonhList(_so.industry) + ")";
                        }
                        else
                        {
                            ret.Match += " @okonh_list !(" + OkonhList(_so.industry) + ")";
                        }
                        break;
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

            if (!string.IsNullOrEmpty(_so.okopf))
            {
                if (_so.okopf_excl >= 0)
                {
                    ret.Match += " @opf (" + _so.okopf + ")";
                }
                else
                {
                    ret.Match += " @opf !(" + _so.okopf + ")";
                }
            }

            if(_so.filials > 0)
            {
                ret.Match += " @opf !(90)";
            }

            if (!string.IsNullOrEmpty(_so.okfs))
            {
                if (_so.okfs_excl >= 0)
                {
                    ret.Match += " @okfs (" + _so.okfs + ")";
                }
                else
                {
                    ret.Match += " @okfs !(" + _so.okfs + ")";
                }
            }

            if (!string.IsNullOrWhiteSpace(_so.fas))
            {
                if (!_so.fas.Contains("999"))
                {
                    if (_so.fas_excl >= 0)
                    {

                        ret.Match += " @fas_list (" + FasList(_so.fas) + ")";
                    }
                    else
                    {
                        ret.Match += " @fas_list !(" + FasList(_so.fas) + ")";
                    }
                }
                else
                {
                    if (_so.fas_excl >= 0)
                    {

                        ret.Match += " @fas_list !(\"x\")";
                    }
                    else
                    {
                        ret.Match += " @fas_list (\"x\")";
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(_so.dbeg))
            {
                ret.Where += " and reg_date_ts >=" + _so.dbeg.UnixTimeStamp();
            }
            if (!string.IsNullOrWhiteSpace(_so.dend))
            {
                ret.Where += " and reg_date_ts <=" + _so.dend.UnixTimeStamp();
            }

            if (!string.IsNullOrEmpty(_so.phone))
            {
                ret.Match += " @phone (" + PhonesList(_so.phone) + ") ";
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

            string reestr = "";
            //В реестре недобросовестных поставщиков: 1 - да, 0 - нет
            if (_so.bankrupt == 1)
            {
                reestr += "\"nedobr\"|";
                //ret.Where += " and nedobr=1";
            }

            //Наличие отчетности IAS-GAAP: 1 - да, 0 - нет
            if (_so.gaap == 1)
            {
                reestr += "\"gaap\"|";
                //ret.Where += " and gaap=1"; 
            }

            //В реестре Ростата: 1 - да, 0 - нет
            if (_so.rgstr == 1)
            {
                reestr += "\"rgstr\"|";
                //ret.Where += " and rgstr=1";
            }

            if (_so.trades != "")
            {
                if (_so.trades.Substring(1, 1) == "1")
                {
                    reestr += "\"ismmvb\"|"; //ММВБ
                }
                if (_so.trades.Substring(2, 1) == "1")
                {
                    reestr += "\"isRTSboard\"|";
                }
            }

            if (reestr != "")
            {
                ret.Match += " @reestr (" + reestr.Substring(0, reestr.Length - 1) + ")";
            }

            /*
                        //Листинг
                        if (_so.trades != "")
                        {
                            //if(_so.trades.Substring(0,1) == "1")
                            //{
                            //    ret.Where += " and isrts=1"; //РТС
                            //}
                            string listing = "";
                            if (_so.trades.Substring(1, 1) == "1")
                            {
                                listing += "\"ismmvb\"|";
                                //ret.Where += " and ismmvb=1"; //ММВБ
                            }
                            if (_so.trades.Substring(2, 1) == "1")
                            {
                                listing += "\"isRTSboard\"|";
                                //ret.Where += " and isRTSboard=1"; //РТС Board
                            }
                
                            if (listing != "")
                            {
                                ret.Match += " @listing (" + listing.Substring(0, listing.Length - 1) +")";
                            }
                        }
                        */

            return ret;
        }

        //Список ОКАТО
        private string okato_list = "";
        private string OkatoList(string okato)
        {
            if (!String.IsNullOrEmpty(okato))
            {
                var str = okato.Split('|').Distinct();
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

        //Список ОКВЭД
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

        //Список ОКОНХ
        private string okonh_list = "";
        private string OkonhList(string okonhs)
        {
            if (!String.IsNullOrEmpty(okonhs))
            {
                var list = okonhs.Split('|');
                foreach (var item in list)
                {
                    if (!okonh_list.Contains(item))
                    {
                        okonh_list += item.ToString() + "|";
                    }
                    GetOkonh(item);
                }
                if (okonh_list.Length > 0)
                {
                    return okonh_list.Substring(0, okonh_list.Length - 1);
                }
                else
                {
                    return okonhs;
                }
            }
            else
            {
                return "";
            }
        }
        private void GetOkonh(string parent_okonh)
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select a.kod as okonh from searchdb2..okonh a left join searchdb2..okonh b on b.id=a.parent_id where b.kod='" + parent_okonh + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((string)reader[0]);
                }
                con.Close();
                if (list.Count > 0)
                {
                    foreach (string item in list)
                    {
                        if (!okonh_list.Contains(item))
                        {
                            okonh_list += item.ToString() + "|";
                        }
                        GetOkonh(item);
                    }
                }
            }
        }

        //Лидер рынка (по регионам)
        private string fas_list = "";
        private string FasList(string fas)
        {
            if (!String.IsNullOrEmpty(fas))
            {
                var list = fas.Split('|').Distinct();
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        string str = item.ToString();
                        if (!fas_list.Contains(str))
                        {
                            fas_list += str + "|";
                        }
                    }
                    return fas_list.Substring(0, fas_list.Length - 1);
                }
                else
                {
                    return fas;
                }
            }
            else
            {
                return "";
            }
        }

        //Список телефонов
        private string phones_list = "";
        private string PhonesList(string phones)
        {
            if (!String.IsNullOrWhiteSpace(phones))
            {
                var list = phones.Replace(" ", "").Split(',').Distinct();
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        char[] c = item.ToCharArray();
                        StringBuilder sb = new StringBuilder("\"");
                        foreach (char x in c)
                        {
                            sb.Append(" AA" + x);
                        }
                        phones_list += sb.ToString() + "\",";
                    }
                }
                return phones_list.Trim().Replace("\" ", "\"").Substring(0, phones_list.Length - 2);
            }
            else
            {
                return "";
            }

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
                    string sql = "SELECT STUFF((select ','+ CAST(u.id as nvarchar(10)) from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id FOR XML PATH('')),1,1,'')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@group_id", SqlDbType.BigInt).Value = group_id;
                    cmd.CommandTimeout = 300;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = (string)reader[0];
                    }

                    //                    while (reader.Read())
                    //                    {
                    //                        ret += reader[0].ToString() + ",";
                    //                    }
                }
                if (ret != "")
                {
                    //                    return ret.Substring(0, ret.Length - 1);
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