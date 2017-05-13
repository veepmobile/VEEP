using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.BLL.Infrastructure;
using System.Text.RegularExpressions;
using Skrin.Models.Authentication;
using Skrin.Models.Iss;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;
using Skrin.Models.Search;
using Skrin.Models.UA;
using SKRIN;
using Skrin.Models.Iss.Debt;

namespace Skrin.BLL.Root
{
    public static class SqlUtiltes
    {
        /// <summary>
        /// Краткое имя компании
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public static string GetShortName(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select ISNULL(short_name,name) name from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using(SqlDataReader reader=cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if(reader.Read())
                    {
                        return (string)reader.ReadNullIfDbNull(0);
                    }
                }
            }
            return null;
        }

        public static string GetFullName(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select ISNULL(name,short_name) name from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        return (string)reader.ReadNullIfDbNull(0);
                    }
                }
            }
            return null;
        }

        public static List<Tuple<string,int>> GetBones(string input)
        {
            List<Tuple<string,int>> ret=new List<Tuple<string,int>>();
            if(!string.IsNullOrEmpty(input))
            {
                input=Regex.Replace(input,"[\\\";\\-\\.,«»\\\']*","").Trim();
                if(!string.IsNullOrEmpty(input))
                {
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        string sql = "select top 7 bones,count(*) as cnt, min(group_id) as group_id from searchdb2..union_search where bones like @input group by bones order by group_id,bones";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@input", SqlDbType.VarChar).Value = input+"%";
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ret.Add(new Tuple<string, int>(reader.ReadEmptyIfDbNull("bones"), (int)reader["cnt"]));
                            }
                        }
                    }
                }
            }
            return ret;
        }


        public static List<Tuple<string, int>> GetBonesUA(string input)
        {
            List<Tuple<string, int>> ret = new List<Tuple<string, int>>();
            if (!string.IsNullOrEmpty(input))
            {
                input = Regex.Replace(input, "[\\\";\\-\\.,«»\\\']*", "").Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        string sql = "select top 7 SortedName as bones,count(*) as cnt from ua3.dbo.union_Issuers where SortedName like @input group by SortedName order by SortedName";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@input", SqlDbType.VarChar).Value = input + "%";
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ret.Add(new Tuple<string, int>(reader.ReadEmptyIfDbNull("bones"), (int)reader["cnt"]));
                            }
                        }
                    }
                }
            }
            return ret;
        }


        public static List<Tuple<string, int>> GetBonesKZ(string input)
        {
            List<Tuple<string, int>> ret = new List<Tuple<string, int>>();
            if (!string.IsNullOrEmpty(input))
            {
                input = Regex.Replace(input, "[\\\";\\-\\.,«»\\\']*", "").Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        string sql = "select top 7 SortedName as bones,count(*) as cnt from kz.dbo.ut_RfEnterprise where SortedName like @input and Avail=1 group by SortedName order by SortedName";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@input", SqlDbType.VarChar).Value = input + "%";
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ret.Add(new Tuple<string, int>(reader.ReadEmptyIfDbNull("bones"), (int)reader["cnt"]));
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public static async Task<CompanyShortInfo> GetShortInfoAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select ISNULL(us.short_name,us.name) name, convert(varchar(10), update_date, 104) as update_date, us.issuer_id,us.type_id,
                        inn,us.ogrn,us.legal_address,us.okpo,us.ruler,us.legal_phone,us.www
                        from searchdb2..union_search us
                        where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if(await rd.ReadAsync())
                    {
                        return new CompanyShortInfo
                        {
                            ticker = ticker,
                            name = rd.ReadEmptyIfDbNull("name"),
                            update_date = rd.ReadEmptyIfDbNull("update_date"),
                            issuer_id=(string)rd["issuer_id"],
                            type_id=(int)rd["type_id"],
                            inn=rd.ReadEmptyIfDbNull("inn"),
                            ogrn = rd.ReadEmptyIfDbNull("ogrn"),
                            address = rd.ReadEmptyIfDbNull("legal_address"),
                            okpo = rd.ReadEmptyIfDbNull("okpo"),
                            ruler = rd.ReadEmptyIfDbNull("ruler"),
                            phone = rd.ReadEmptyIfDbNull("legal_phone"),
                            www = rd.ReadEmptyIfDbNull("www")
                        };
                    }
                    return null;
                }
            }
        }
        /*
        public static bool IsEnableForEmitent(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 1 from searchdb2..Group_Emitent g join
                 searchdb2..union_search us on us.issuer_id=g.issuer_id where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return rd.Read();
                }
            }
        }
         */

        public static async Task<string> GetTabAccesses(int id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select accesses  from menu_tabs..skrin_menu_tabs where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetUaTabAccesses(int id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select accesses  from menu_tabs..ua3_menu_tabs where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetIpTabAccesses(int id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select accesses  from [skrin_net]..[skrin_ip_menu_tabs] where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetOgrnAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select ogrn from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetInnAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select inn from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// 1-Ogrn,2-Inn
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public static async Task<Tuple<string,string>> GetOgrnInnAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select ogrn,inn from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return new Tuple<string, string>((string)rd.ReadNullIfDbNull("ogrn"), (string)rd.ReadEmptyIfDbNull("inn"));
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetIssuerIdAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select issuer_id from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<CompanyData> GetCompanyAsync(string ticker)
        {
            if (String.IsNullOrEmpty(ticker))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select isnull(short_name,name) name, naufor.dbo.ClearOpf(name) search_name1,
                                naufor.dbo.ClearOpf(short_name) search_name2, region_id region, inn, ogrn  from searchdb2.dbo.union_search where ticker=@iss";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new CompanyData
                    {
                        Ticker = ticker,
                        Name = (string)reader.ReadNullIfDbNull("name"),
                        SearchedName = (string)reader.ReadNullIfDbNull("search_name1"),
                        SearchedName2 = (string)reader.ReadNullIfDbNull("search_name2"),
                        IsCompany = true,
                        Region = (reader["region"] != DBNull.Value) ? Convert.ToInt32(reader["region"]) : 0,
                        INN = (string)reader.ReadNullIfDbNull("inn"),
                        OGRN = (string)reader.ReadNullIfDbNull("ogrn")
                    };

                }
                return null;
            }
        }

        public static async Task<List<DebtorAdress>> GetAdressAsync(string ogrn)
        {
            if (String.IsNullOrEmpty(ogrn))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<DebtorAdress> ret = new List<DebtorAdress>();
                string sql = "Declare @T table (zip varchar (255) null, region varchar (255) null, region_name varchar (255) null, district_name varchar (255) null, city_name varchar (255) null, locality_name varchar (255) null, street_name varchar (255) null, house varchar (255) null, addr_date datetime null, extract_date datetime null) insert into @T select INDEKS as zip, REGION_KOD_KL as region, REGION_NAME as region_name, RAION_NAME as district_name,GOROD_NAME as city_name,NASPUNKT_NAME as locality_name, STREET_NAME as street_name, DOM as house, B.ADDRESS_DTSTART, extract_date from FSNS_Pravo..ADDRESS A inner join FSNS_Pravo..UL B on A.id=B.UL_ADDRESS_id where B.OGRN =" +
                    "@ogrn" +
                    " union select zip, region, region_name, district_name, city_name, locality_name, street_name, Replace(Replace(house,'ДОМ ',''),'Д ','') as house, grn_date, ul2_extract_date from FSNS_Free..UL2_Address A where A.UL2_ogrn = " +
                    "@ogrn" +
                    " select zip, region, region_name, district_name, city_name, locality_name, street_name, house from @T a inner join (Select addr_date,max(extract_date ) as dt from @T group by addr_date ) b on a.addr_date=b.addr_date and a.extract_date=b.dt order by a.addr_date DESC";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 32).Value = ogrn;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var element = new DebtorAdress();
                    element.zip = (string)reader.ReadNullIfDbNull(0);
                    element.region = (string)reader.ReadNullIfDbNull(1);
                    element.region_name = (string)reader.ReadNullIfDbNull(2);
                    element.district_name = (string)reader.ReadNullIfDbNull(3);
                    element.city_name = (string)reader.ReadNullIfDbNull(4);
                    element.locality_name = (string)reader.ReadNullIfDbNull(5);
                    element.street_name = (string)reader.ReadNullIfDbNull(6);
                    string house = (string)reader.ReadNullIfDbNull(7);
                    element.house = (!String.IsNullOrEmpty(house) ? house.Replace("ДОМ ", "").Replace("Д ", "") : null);
                    ret.Add(element);
                }
                return ret;
            }
        }

        public static async Task<List<DebtorName>> GetCompanyNamesAsync(string ogrn, string inn = null)
        {
            if (String.IsNullOrEmpty(ogrn) && String.IsNullOrEmpty(inn))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<DebtorName> ret = new List<DebtorName>();
                string sql = "";
                if (!String.IsNullOrEmpty(ogrn))
                {
                    sql = "select distinct naufor.dbo.ClearOpf(name), naufor.dbo.ClearOpf(short_name) from fsns_free..UL2 where ogrn=@ogrn " +
                        "UNION select distinct naufor.dbo.ClearOpf(namep) as name, naufor.dbo.ClearOpf(names) as short_name from FSNS_Pravo..UL where ogrn=@ogrn";
                }
                else if (!String.IsNullOrEmpty(inn))
                {
                    sql = "select distinct naufor.dbo.ClearOpf(name), naufor.dbo.ClearOpf(short_name) from fsns_free..UL2 where inn=@inn";
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                if (!String.IsNullOrEmpty(ogrn))
                {
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 32).Value = ogrn;
                }
                else
                {
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar, 32).Value = inn;
                }
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string shortn = (string)reader.ReadNullIfDbNull(0);
                    string longn = (string)reader.ReadNullIfDbNull(1);
                    var element1 = new DebtorName();
                    bool isanychanges = false;
                    if (!String.IsNullOrEmpty(shortn))
                    {
                        if (shortn.ToUpper().Contains("ГОРОДА "))
                        {
                            element1.shortname = shortn.ToUpper().Replace("ГОРОДА ", " Г."); isanychanges = true;
                        }
                        if (shortn.ToUpper().Contains("Г."))
                        {
                            element1.shortname = shortn.ToUpper().Replace("Г.", "ГОРОДА "); isanychanges = true;
                        }
                    }
                    if (!String.IsNullOrEmpty(longn))
                    {
                        if (longn.ToUpper().Contains("ГОРОДА "))
                        {
                            element1.longname = longn.ToUpper().Replace("ГОРОДА ", " Г."); isanychanges = true;
                        }
                        if (longn.ToUpper().Contains("Г."))
                        {
                            element1.longname = longn.ToUpper().Replace("Г.", "ГОРОДА "); isanychanges = true;
                        }
                    }
                    if (isanychanges)
                    {
                        ret.Add(element1);
                    }
                    var element = new DebtorName();
                    element.shortname = shortn;
                    element.longname = longn;
                    ret.Add(element);
                }
                return ret;
            }
        }

        public static async Task<CompanyData> GetIPAsync(string ticker)
        {
            if (String.IsNullOrEmpty(ticker))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select top 1 (isnull(lastName + ' ','') + isnull(firstName+' ','') + isnull(middleName,'')) as fio, inn, ogrnip from FSNS_Free_FL..FL where ogrnip=@iss and IsLast=1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new CompanyData
                    {
                        Ticker = ticker,
                        Name = (string)reader["fio"],
                        SearchedName = (string)reader["fio"],
                        SearchedName2 = null,
                        IsCompany = false,
                        INN = (string)reader["inn"],
                        OGRN = (string)reader["ogrnip"]
                    };

                }
                return null;
            }

        }       

        //Сообщения о банкротстве
        public static async Task<BancryptcyMessage> GetBancruptcyMessageAsync(string ids, string ticker)
        {
            if (String.IsNullOrEmpty(ids))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select  contents, convert(varchar(10), a.reg_date, 104) as reg_date,b.name,inn,ogrn  from naufor..Bankruptcy2 a inner join naufor..Skrin_Sources b on skrin_source_id=b.id  
left join naufor..Bankruptcy2_join c on c.Bankruptcy_id=a.id where a.id in (@ids)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ids", SqlDbType.VarChar, 1000).Value = ids;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                BancryptcyMessage msg = new BancryptcyMessage();
                msg.ISS = ticker;
                msg.CompanyName = GetShortName(ticker);
                List<MessageItem> list = new List<MessageItem>();
                while (await reader.ReadAsync())
                {
                    MessageItem item = new MessageItem
                    {
                        SourceName = (string)reader["name"],
                        RegDate = (string)reader["reg_date"],
                        Contents = (string)reader["contents"]
                    };
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
                if (list != null)
                {
                    msg.MessagesList = new List<MessageItem>(list);
                }
                return msg;
            }
        }

        public static async Task<List<NRARating>> GetNRARatingsAsync(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"Select distinct isnull(LocalRating,'-') LocalRating,isnull(InternationalRating,'-') InternationalRating,isnull(ForeCast,'-') ForeCast,
                                RatingTypeID,convert(varchar(10),Rating_Date,104) Rating_DateStr,Rating_Date
                                from naufor..nra_companies  where issuer_id=@issuer_id
                                order by Rating_Date desc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                List<NRARating> ratings = new List<NRARating>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    ratings.Add(new NRARating
                    {
                        LocalRating = (string)reader["LocalRating"],
                        InternationalRating = (string)reader["InternationalRating"],
                        ForeCast = (string)reader["ForeCast"],
                        RatingTypeID = (int)reader["RatingTypeID"],
                        Rating_Date = (string)reader["Rating_DateStr"]
                    });
                }
                return ratings;
            }

        }

        public static async Task<List<ULDetails>> GetUlDetailsAsync(string iss_list)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<ULDetails> ret = new List<ULDetails>();
                if (!string.IsNullOrWhiteSpace(iss_list))
                {
                   /* string sql = string.Format(@"select case when len(isnull(a.short_name,''))=0 then a.name else a.short_name end  nm, isnull(a.inn,'') inn 
                        ,isnull(r.name,'') rname, isnull(a.okpo,'') okpo,isnull(o.name,'') as okved,isnull(ogrn,'') ogrn, 
                        isnull(convert(varchar(10),a.reg_date,104),'') rd, isnull(a.reg_org_name,'') reg_orf, 
                        isnull(a.legal_address,'') addr,isnull(ruler,'') ruler, 
                        isnull(a.legal_phone,'') phone,isnull(a.legal_fax,'') as fax,isnull(a.legal_email,'') as email 
                        ,isnull(www,'') as www, case when delete_period is null then '' else 'Удалено из реестра Росстата ' + convert(varchar(10),delete_period,104) end as del  
                        from searchdb2..union_search a left join naufor..regions r on r.id=a.region_id left join searchdb2..okveds o on o.kod=a.okved left join gks..gks_issuers gi on gi.id=a.gks_id where issuer_id in ({0}) order by so,a.type_id,bones", string.Join(",", iss_list.ParamValsString().Keys));
                    */

                    string sql = string.Format(@"select case when len(isnull(a.short_name,''))=0 then a.name else a.short_name end  nm, isnull(a.inn,'') inn 
                    ,isnull(r.name,'') rname, isnull(a.okpo,'') okpo
                    ,isnull((SELECT CAST(okveds.okved3_code AS nvarchar(20)) FROM searchdb2.dbo.okved okveds WHERE okveds.us_id = a.ID and okveds.main=1),'') AS okved_code
                    ,isnull((SELECT CAST(okveds.okved3_name AS nvarchar(500)) FROM searchdb2.dbo.okved okveds WHERE okveds.us_id = a.ID and okveds.main=1),'') AS okved
                    ,isnull(ogrn,'') ogrn, isnull(convert(varchar(10),a.reg_date,104),'') rd, isnull(a.reg_org_name,'') reg_orf
                    ,isnull(a.legal_address,'') addr,isnull(ruler,'') ruler,isnull(a.legal_phone,'') phone,isnull(a.legal_fax,'') as fax,isnull(a.legal_email,'') as email ,isnull(www,'') as www, case when delete_period is null then '' else 'Удалено из реестра Росстата ' + convert(varchar(10),delete_period,104) end as del  
                    from searchdb2..union_search a left join naufor..regions r on r.id=a.region_id left join searchdb2..okveds o on o.kod=a.okved left join gks..gks_issuers gi on gi.id=a.gks_id where issuer_id in ({0}) order by so,a.type_id,bones", string.Join(",", iss_list.ParamValsString().Keys));

                    SqlCommand cmd = new SqlCommand(sql, con);
                    foreach (var p in iss_list.ParamValsString())
                    {
                        cmd.Parameters.Add(p.Key, SqlDbType.VarChar).Value = p.Value;
                    }
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add(new ULDetails
                            {
                                nm = reader.ReadEmptyIfDbNull("nm"),
                                inn = reader.ReadEmptyIfDbNull("inn"),
                                region = reader.ReadEmptyIfDbNull("rname"),
                                okpo = reader.ReadEmptyIfDbNull("okpo"),
                                okved_code = reader.ReadEmptyIfDbNull("okved_code"),
                                okved = reader.ReadEmptyIfDbNull("okved"),
                                ogrn = reader.ReadEmptyIfDbNull("ogrn"),
                                reg_date = reader.ReadEmptyIfDbNull("rd"),
                                reg_org_name = reader.ReadEmptyIfDbNull("reg_orf"),
                                legal_address = reader.ReadEmptyIfDbNull("addr"),
                                ruler = reader.ReadEmptyIfDbNull("ruler"),
                                legal_phone = reader.ReadEmptyIfDbNull("phone"),
                                legal_fax = reader.ReadEmptyIfDbNull("fax"),
                                legal_email = reader.ReadEmptyIfDbNull("email"),
                                www = reader.ReadEmptyIfDbNull("www"),
                                del = reader.ReadEmptyIfDbNull("del"),

                            });
                        }
                    }
                }
                return ret;
            }
        }


        public static List<string> GetGKSYearList()
        {
            List<string> years = new List<string>();
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query q = sql.OpenQuery("SELECT year from GKS..GKS_QIV_Years");
                while (q.Read())
                {
                    years.Add(((int)q.GetFieldAsInt("year")).ToString());
                }
            }
            return years;
        }

        public static List<string> GetNauforYearList()
        {
            List<string> years = new List<string>();
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
//                Query q = sql.OpenQuery("SELECT (cast(quarter as varchar(2)) + 'кв. ' + cast(year as varchar(4))) as quartyear FROM  (select year,quarter from naufor..quart_indic_values where year>1997 group by year,quarter) a  ORDER BY year desc,quarter desc");
                Query q = sql.OpenQuery("SELECT (cast(quarter as varchar(2)) + 'кв. ' + cast(year as varchar(4))) as quartyear FROM  shopview.[dbo].[nqiv_periods]  ORDER BY year desc,quarter desc");
                while (q.Read())
                {
                    years.Add(((string)q.GetFieldAsString("quartyear")).ToString());
                }
            }
            return years;
        }

        public static List<string> GetMSFOYearList()
        {
            List<string> years = new List<string>();
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query q = sql.OpenQuery("select year from naufor..msfo_quart_indic_values group by year ORDER BY year desc");
                while (q.Read())
                {
                    years.Add(((int)q.GetFieldAsInt("year")).ToString());
                }
            }
            return years;
        }

        public static async Task<string> GetKZNameAsync(string Code)
        {
            if (String.IsNullOrEmpty(Code))
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"SELECT ISNULL(NameShort,NameFull) Name FROM [KZ]..[ut_RfEnterprise] WHERE Code = @Code";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = Code;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return reader[0] != DBNull.Value ? (string)reader[0] : "";
                }
                return null;
            }

        }

        public static async Task<List<KZDetails>> GetKZDetailsAsync(string iss_list)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<KZDetails> ret = new List<KZDetails>();
                if (!string.IsNullOrWhiteSpace(iss_list))
                {
                    string sql = "select isnull(a.NameShort,a.NameFull) Name,a.Code, a.RegionName, "
                        + "MainDeal=case when a.Code_OKED is not null then do.Name "
                        + "else (select top 1 Dscr from kz.dbo.ut_HistEnterpriseAct ha join kz.dbo.ut_ClssActivity ca "
                        + "on ca.Id=ha.Id_Activity where Code_Main=a.Code order by DateBegin desc) collate database_default end, "
                        + "CodeTax,Convert(varchar(256),DateReg,104) DateReg, FullAddress=case "
                        + "when a.Code_KATO is null then Post+', '+ kz.dbo.getfullPlace(Id_Place)+' '+ Address "
                        + "else kz.dbo.getKatoPath(a.Code_KATO) end, Manager, Phone, Fax, Email from kz.dbo.ut_RfEnterprise a "
                        + "left join kz.dbo.Dic_OKED do on a.Code_OKED=do.Code "
                        + "where a.Code in (" + string.Join(",", iss_list.ParamValsString().Keys) + ")";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    foreach (var p in iss_list.ParamValsString())
                    {
                        cmd.Parameters.Add(p.Key, SqlDbType.VarChar).Value = p.Value;
                    }
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add(new KZDetails() { 
                                Name = reader.ReadEmptyIfDbNull("Name"),
                                Code = reader.ReadEmptyIfDbNull("Code"),
                                RegionName = reader.ReadEmptyIfDbNull("RegionName"),
                                MainDeal = reader.ReadEmptyIfDbNull("MainDeal"),
                                CodeTax = reader.ReadEmptyIfDbNull("CodeTax"),
                                DateReg = reader.ReadEmptyIfDbNull("DateReg"),
                                FullAddress = reader.ReadEmptyIfDbNull("FullAddress"),
                                Manager = reader.ReadEmptyIfDbNull("Manager"),
                                Phone = reader.ReadEmptyIfDbNull("Phone"),
                                Fax = reader.ReadEmptyIfDbNull("Fax"),
                                Email = reader.ReadEmptyIfDbNull("Email")
                            });
                        }
                    }
                }
                return ret;
            }
        }
        public static List<Tuple<string, string>> GetUaOkfs()
        {
            List<Tuple<string, string>> ret = new List<Tuple<string, string>>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                //string sql = "Select Code as id, RusDescr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as sel from UA2.dbo.Dic_Kfv a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + sel + "')) b on b.kod=a.Code  order by Code";
                string sql = "Select RusDescr, Code from UA3..Dic_Kfv order by Code";
                SqlCommand cmd = new SqlCommand(sql, con);
                try {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            ret.Add(new Tuple<string, string>(rd.GetString(0), rd.GetString(1)));
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            return ret;
        }

        public static async Task<CompanyUAShortInfo> GetUAShortInfoAsync(string edrpou)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select ISNULL(us.shortname, us.name) name
                        from ua3..union_Issuers us
                        where edrpou=@edrpou";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 8).Value = edrpou;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return new CompanyUAShortInfo
                        {
                            edrpou = edrpou,
                            name = rd.ReadEmptyIfDbNull("name"),
                        };
                    }
                    return null;
                }
            }
        }

        public static void ProcLog(string proc_text,string error)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = @"INSERT into logs2..Proc_Log
                                (proc_text, error)
                                VALUES
                                (@proc_text, @error)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@proc_text", proc_text);
                    cmd.Parameters.AddWithValue("@error", error.DBVal());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        public static async Task<List<UADetails>> GetUADetailsAsync(string iss_list)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<UADetails> ret = new List<UADetails>();
                if (!string.IsNullOrWhiteSpace(iss_list))
                {
                    string sql = "select a.name,Edrpou,isnull(ar.Name,'') area,isnull(mainkveddescr,'') kved ,isnull(RegNo,'') RegNmbr,"
                                   +" isNull(convert(varchar(50),RegDate,104),'') RegDate, isnull(RegOrg,'') RegBody,"
                                   +"  Address,"
                                   +" isnull(RusRulerName,'') ruler,isnull(Phone,'') phone,isnull(Fax,'') fax,isnull(EMail,'') email,"
                                   +" isnull(Web,'') web "
                                   +" from UA3.dbo.vw_union_Issuers_4search a "
                                   +" left join UA3.dbo.Dic_Area ar on a.AreaId=ar.Id "
                                   +"         where Edrpou in (" + string.Join(",", iss_list.ParamValsString().Keys) + ")";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    foreach (var p in iss_list.ParamValsString())
                    {
                        cmd.Parameters.Add(p.Key, SqlDbType.VarChar).Value = p.Value;
                    }
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add(new UADetails()
                            {
                                //Наименование	ЕДРПОУ	Регион	Отрасль	Регистрационный номер	Дата гос.регистрации	Орган гос. регистрации	Адрес	Руководитель	Телефон	Факс	E-mail	Сайт																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																			

                                name = reader.ReadEmptyIfDbNull("name"),
                                edrpou = reader.ReadEmptyIfDbNull("Edrpou"),
                                region = reader.ReadEmptyIfDbNull("area"),
                                industry = reader.ReadEmptyIfDbNull("kved"),
                                regno = reader.ReadEmptyIfDbNull("RegNmbr"),
                                regdate = reader.ReadEmptyIfDbNull("RegDate"),
                                regorg = reader.ReadEmptyIfDbNull("RegBody"),
                                addr = reader.ReadEmptyIfDbNull("Address"),
                                ruler = reader.ReadEmptyIfDbNull("ruler"),
                                phone = reader.ReadEmptyIfDbNull("phone"),
                                fax = reader.ReadEmptyIfDbNull("fax"),
                                email = reader.ReadEmptyIfDbNull("email"),
                                www = reader.ReadEmptyIfDbNull("web")
                                
                            });
                        }
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Пытается найти уникальный тикер по огрн или окпо
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public static string FindTrueTicker(string ticker) 
        {
            return FindTickerByRegCode("ul2_ogrn", ticker) ?? FindTickerByRegCode("okpo", ticker);
        }


        private static string FindTickerByRegCode(string reg_code,string ticker)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql=string.Format("SELECT top 2 ticker from searchdb2..union_search where {0}=@ticker",reg_code);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar).Value = ticker;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.SingleVal("ticker");
                }
            }
        }


        public static async Task<DateTime?> GetLastEgrulExtractDateAsync(string ogrn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT extract_date from FSNS_Free..UL2 where ogrn=@ogrn and IsLast=1", con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (DateTime?)reader.ReadNullIfDbNull(0);
                    }
                }
                return null;
            }
        }

        public static async Task SaveEgrulReport(string ogrn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"if not exists (SELECT 1 from FSNS_Free..save_egrul where ogrn=@ogrn AND @is_test=is_test)
                                BEGIN
	                                insert into FSNS_Free..save_egrul
	                                (ogrn,is_test)
	                                VALUES
	                                (@ogrn,@is_test)
                                end";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                cmd.Parameters.Add("@is_test", SqlDbType.Bit).Value = Configs.IsTest;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Дата скаченной платной выписки ЕГРЮЛ
        /// </summary>
        /// <param name="ogrn"></param>
        /// <returns></returns>
        public static async Task<DateTime?> GetFsns2EgrulDateAsync(string ogrn)
        {
            if (ogrn == null || ogrn.Trim().Length != 13)
                return null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "SELECT extract_date from fsns2..ul2 where ogrn=@ogrn";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if(await reader.ReadAsync())
                    {
                        return (DateTime)reader[0];
                    }
                }
                return null;
            }
        }

        public static async Task<DateTime?> GetLastEgripExtractDateAsync(string ogrnip)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT extract_date from FSNS_Free_FL..FL where ogrnip=@ogrnip and IsLast=1", con);
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = ogrnip;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (DateTime?)reader.ReadNullIfDbNull(0);
                    }
                }
                return null;
            }
        }
        public static async Task SaveEgripReport(string ogrn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"if not exists (SELECT 1 from FSNS_Free_FL..save_egrip where ogrnip=@ogrn AND @is_test=is_test)
                                BEGIN
	                                insert into FSNS_Free_fl..save_egrip
	                                (ogrnip,is_test)
	                                VALUES
	                                (@ogrn,@is_test)
                                end";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 15).Value = ogrn;
                cmd.Parameters.Add("@is_test", SqlDbType.Bit).Value = Configs.IsTest;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}