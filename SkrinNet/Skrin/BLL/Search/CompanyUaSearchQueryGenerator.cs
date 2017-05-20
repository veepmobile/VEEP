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
    public class CompanyUaSearchQueryGenerator
    {
        private CompaniesSearchObject _so;

        public CompanyUaSearchQueryGenerator(CompaniesSearchObject so)
        {
            _so = so;
        }

        public SphinxQueryObject GetQuery()
        {

            SphinxQueryObject qo = new SphinxQueryObject();
            //qo.Port = 9365; //SearchReq228 основной
            //qo.Port = 9367; //SearchReq228_2 старый
            qo.Port = Convert.ToInt32(Configs.SphinxSearchUAPort); 
            qo.CharasterSet = "utf8";
            if (_so.page_no < 0)
            {
                qo.Count = 10000;
                qo.Skip = 0;
            }
            else
            {
                qo.Count = _so.rcount;
                qo.Skip = (int)_so.page_no * qo.Count;
            }
            string query_pattern = "";
            //1 - Скрин предприятие, 9 - Сообщения, 2 - Скрин эмитент, 7 - Блумберг, 10 - Дил+У
            /*
            string emitent = _so.is_emitent ? "and isEmitent=1" : "";
            if (_so.page_no == -1001)
            {
                //query_pattern = "select issuer_id, type_id from searchreq2 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select issuer_id, type_id from searchreq3 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
            }
            if (_so.page_no == -2001)
            {
                //query_pattern = "select name, nm, inn, region, okpo, okved, ogrn, reg_date, reg_org_name, legal_address, ruler, legal_phone, legal_fax, legal_email, www, del from searchreq2 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select name, nm, inn, region, okpo, okved, ogrn, reg_date, reg_org_name, legal_address, ruler, legal_phone, legal_fax, legal_email, www, del from searchreq3 where match (' _all_ {0}') " + emitent + " {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
            }
            */
            if (_so.page_no >= 0)
            {
                //query_pattern = "select * from searchreq2 where match (' _all_ {0}') {1} order by group_id asc, uniq desc, bones asc limit {2},{3} OPTION max_matches=10000; show meta;";
                query_pattern = "select name, shortname, rulername, rulertitle, edrpou, address, region, area, koatuu_name,mainkveddescr,RegNo,regdate,regorg,address, phone,fax,email,web from idx_ua_skrin where match (' _all_ {0}') {1} order by SortedName asc limit {2},{3} OPTION max_matches=10000; show meta;";
            }
            else
            {
                query_pattern = "select name, edrpou, area, mainkveddescr kved,RegNo,regdate,regorg,  address addr,  rulername ruler, phone,fax,email,web from idx_ua_skrin where match (' _all_ {0}') {1} order by SortedName asc limit {2},{3} OPTION max_matches=10000; show meta;";
               
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
                /*
                switch (_so.strict)
                {
                    case 0:
                        if (_so.company == "-")
                        {
                            _so.company = _so.company.Replace("-", "\"-\"");
                        }
                        _so.company = _so.company.Replace("<", "").Replace(">", "").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"");
                        _so.company = _so.company.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        _so.company = _so.company.Trim().Replace(" ", " SENTENCE ");
                        ret.Match += " @search_name " + _so.company + "* "; //по началу слова
                        break;
                    case 3:
                        _so.company = _so.company.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        ret.Match += " @search_name =\"&amp;&amp;" + _so.company + "&amp;&amp;\""; //строгий поиск
                        break;
                    default:
                        if (_so.company == "-")
                        {
                            _so.company = _so.company.Replace("-", "\"-\"");
                        }
                        _so.company = _so.company.Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("^", "\"^\"").Replace("|", "\"|\"");
                        _so.company = _so.company.Trim().Replace(" ", " SENTENCE ");
                        ret.Match += " @search_name  \"" + _so.company + "\" ";
                        break;
                }*/
                if (_so.company == "-")
                {
                    _so.company = _so.company.Replace("-", "\"-\"");
                }
                _so.company = _so.company.Trim().Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"").Replace("^", "\"^\"").Replace("|", "\"|\"").Replace(" ", " SENTENCE ");
                ret.Match += " @search_name  " + _so.company + " ";
            }

            if (!string.IsNullOrWhiteSpace(_so.ruler))
            {
                _so.ruler = _so.ruler.Trim().Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"").Replace("^", "\"^\"").Replace("|", "\"|\"").Replace(" ", " SENTENCE ");
                ret.Match += " @rusrulername " + _so.ruler;
            }

            if (!string.IsNullOrWhiteSpace(_so.address))
            {
                //_so.address = _so.address.Replace("г", "").Replace("ул", "").Replace("д", "").Replace(".", "").Replace(",", "");
                //ret.Match += " @legal_address \"" + _so.address + "\"";
                _so.address = _so.address.Trim().Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"").Replace("^", "\"^\"").Replace("|", "\"|\"").Replace(" ", " SENTENCE ");
                ret.Match += " @search_address " + _so.address + " ";
            }

            if (!string.IsNullOrEmpty(_so.phone))
            {
                _so.phone = _so.phone.Trim().Replace("<", "\"&lt;\"").Replace(">", "\"&gt;\"").Replace("@", "\"@\"").Replace("$", "\"$\"").Replace("~", "\"~\"").Replace("^", "\"^\"").Replace("|", "\"|\"").Replace(" ", " SENTENCE ");
                ret.Match += " @search_phone (" + PhonesList(_so.phone) + ") ";
            }

            if (!string.IsNullOrEmpty(_so.industry))
            {
                if (_so.ind_main == 1)
                {
                    ret.Match += " @mainkved (" + OkvedList(_so.industry) + ")";
                }
                else
                {
                    ret.Match += " @allKveds (" + OkvedList(_so.industry) + ")";
                }
                           
/*                
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
                }*/
            }

            if (!string.IsNullOrEmpty(_so.regions))
            {
                switch (_so.is_okato)
                {
                    case 1:
                        if (_so.reg_excl >= 0)
                        {
                            ret.Match += " @areaid (" + OkatoList(_so.regions) + ")";
                        }
                        else
                        {
                            ret.Match += " @areaid (" + OkatoList(_so.regions) + ")";
                        }
                        break;
                    case 0:
                        //_so.regions = _so.regions.Replace(",", "|");
                        if (_so.reg_excl >= 0)
                        {
                            ret.Match += " @koatuu_full (" + OkatoList(_so.regions) + ")";
                        }
                        else
                        {
                            ret.Match += " @koatuu_full !(" + OkatoList(_so.regions) + ")";
                        }
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_so.okopf))
            {
                if (_so.okopf_excl >= 0)
                {
                    ret.Where += " and opf in (" + _so.okopf + ")";
                }
                else
                {
                    ret.Where += " and !opf in (" + _so.okopf + ")";
                }
            }

            if (!string.IsNullOrEmpty(_so.okfs))
            {
                if (_so.okfs_excl >= 0)
                {
                    ret.Match += " @kfv (" + OkfsList(_so.okfs) + ")";
                }
                else
                {
                    ret.Match += " @kfv !(" + OkfsList(_so.okfs) + ")";
                }
            }
            /*
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
            */
            if (!string.IsNullOrWhiteSpace(_so.dbeg))
            {
                ret.Where += " and searchregdate >=" + _so.dbeg.UnixTimeStamp();
            }
            if (!string.IsNullOrWhiteSpace(_so.dend))
            {
                ret.Where += " and searchregdate <=" + _so.dend.UnixTimeStamp();
            }
            /*
            if (_so.group_id != 0)
            {
                string ids = IssuersList(_so.group_id);
                if (ids != null)
                {
                    ret.Where += " and id in (" + ids + ")";
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
            */
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
        //private string okato_list = "";
        private string OkatoList(string okato)
        {
            string _ret = "";
            string[] _okt = okato.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if (_okt.Length > 0)
            {
                _ret = "\"" + String.Join("\"|\"", _okt) + "\"";
            }
            return _ret; /*
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
            }*/
        }

        private string OkfsList(string kfv)
        {
            string _ret = "";
            string[] _kfvs = kfv.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            if (_kfvs.Length > 0)
            {
                _ret = "\"" + string.Join("\"|\"", _kfvs) + "\""; 
            }
            return _ret;
        }

        //Список ОКВЭД
        private string okved_list = "";
        private string OkvedList(string okveds)
        {
            if (!String.IsNullOrEmpty(okveds))
            {
                var list = okveds.Split(',');
                foreach (var item in list)
                {
                    string str = item.ToString();
                    if (!okved_list.Contains(str))
                    {
                        okved_list += "|\"" + str.Replace(".","D") + "*\"";
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
        /*
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
        }*/

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
        /*
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
        }*/
    }
}