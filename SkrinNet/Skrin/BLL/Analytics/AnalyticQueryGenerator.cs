using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Analytics;
using Skrin.BLL.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace Skrin.BLL.Analytics
{
    public class AnalyticQueryGenerator
    {
        private SearchObject _so;
        public AnalyticQueryGenerator(SearchObject so)
        {
            if (so == null)
            {
                _so = new SearchObject();
                /*_so=new SearchObject{
                    anal_types=null,
                    dBeg=null,
                    dEnd=null,
                    anal_types_excl=false,
                    author_search=null,
                    text_search=null
                };*/
            }
            else
            {
                _so = new SearchObject()
                {
                    anal_types = Helper.CheckInput(so.anal_types),
                    anal_types_excl = so.anal_types_excl,
                    author_search = Helper.CheckInput(so.author_search),
                    dBeg = Helper.CheckInput(so.dBeg),
                    dEnd = Helper.CheckInput(so.dEnd),
                    text_search = Helper.CheckInput(so.text_search),
                    page_no = so.page_no,
                    r_count = so.r_count
                };
            }
        }

        private async Task<string> _GetQueryString()
        {
            int page_no = _so.page_no ?? 1;
            int r_count = _so.r_count ?? 20;
            int start = (page_no - 1) * r_count + 1;
            int end = page_no * r_count;


            string pattern = @";with rev as (		
                                select u.id, u.headline, u.doc_id,  isnull(u.insert_date,u.reg_date) date, u.daily, p.file_name,d.pages,
                                ISNULL(u.author_id, 'D0C1D724873E457BB67ED87D9BDA2843') author_id,a.short_name author_name,
                                ROW_NUMBER() OVER (order by u.insert_date desc)as RowNumber  
                                from naufor.dbo.union_reviews_analitics u
                                join naufor.dbo.Docs_New d on u.doc_id=d.id and u.is_english=0
                                join naufor.dbo.Doc_Pages_New p on u.doc_id=p.doc_id
                                left join naufor.dbo.Authors2 a on u.author_id=a.id
                                 {2})
                                select *,
                                a_type=case 
                                    when rev.daily=1 then 'Ежедневный обзор рынка' 
                                    when rev.daily=0 then 'Еженедельный обзор рынка' 
                                    else 
                                    'Обзоры предприятий и отраслей' + isnull((
	                                    select top 1 name from
	                                    (select ': '+isnull(i.short_name,i.name) name,1 ord
	                                    from naufor..review_object ro
		                                    inner join naufor.dbo.Issuers I on ro.object_id=i.id and ro.object_type=1
	                                    where ro.review_id=rev.id
	                                    UNION ALL
	                                    select ': '+i.name name, 2 ord 
	                                    from naufor..review_object ro
		                                    inner join naufor.dbo.Industries_New i on ro.object_id=i.id and ro.object_type=0
	                                    where ro.review_id=rev.id
	                                    )t order by ord
                                    ),'') end 
                                 from rev where rownumber between {0} and {1} order by rownumber";
            return string.Format(pattern, start, end,await  _GetCondition(true));

        }

        private async Task<string> _GetCountString()
        {
            string pattern = @"select count(*) 
                                from naufor.dbo.union_reviews_analitics u 
                                left join naufor.dbo.Authors2 a on u.author_id=a.id
                                where u.is_english=0   {0}";
            string result = string.Format(pattern, await _GetCondition(false));
            return  result;
        }

        private async Task<string> _GetCondition(bool is_where)
        {
            List<string> conditions = new List<string>();
            if (_so.dBeg != null)
                conditions.Add(string.Format("u.insert_date>=convert(datetime,'{0}',104)", _so.dBeg));
            if (_so.dEnd != null)
                conditions.Add(string.Format("u.insert_date < dateadd(dd,1,convert(datetime,'{0}',104))", _so.dEnd));
            if (_so.text_search != null)
                conditions.Add(string.Format("headline like '%{0}%'", _so.text_search));
            if (_so.author_search != null)
                conditions.Add(string.Format("a.short_name like '%{0}%'", _so.author_search));
            if (_so.anal_types != null)
            {
                string analt_conditions = await _GetAnalTypeConditions();
                if (!string.IsNullOrEmpty(analt_conditions))
                {
                    conditions.Add(analt_conditions);
                }
            }


            if (conditions.Count == 0)
            {
                return "";
            }
            return (is_where ? " where " : " and ") + string.Join(" and ", conditions);
        }

        private async Task<string> _GetAnalTypeConditions()
        {
            List<string> conditions = new List<string>();
            Dictionary<int, List<MenuTypes>> anal_dic = await GetAnalDic(_so.anal_types);
            //если отмечены все корневые условия, то при anal_types_excl=false не ставится никаких условий, а при anal_types_excl=true - запрещающее условие
            List<MenuTypes> level_list = anal_dic[1];
            if (level_list.Count == 2)
            {
                if (_so.anal_types_excl)
                    return "1!=1";
                else
                    return "";
            }
            if (level_list.Count == 1)
            {
                if (level_list[0].id == 1) //обзоры рынка
                {
                    conditions.Add("daily is not null");
                }
                else //Обзоры предприятий и отраслей:
                {
                    conditions.Add("daily is null");
                }
            }
            level_list = anal_dic[2];
            foreach (MenuTypes m in level_list)
            {
                if (m.is_daily != null && m.is_daily.Value)
                {
                    conditions.Add("daily = 1");
                    continue;
                }
                if (m.is_daily != null && !m.is_daily.Value)
                {
                    conditions.Add("daily = 0");
                    continue;
                }
                if (m.is_industry != null)
                {
                    if (m.industry_id != null)
                    {
                        conditions.Add(string.Format(@"u.id  in (select review_id from naufor..review_object a with(nolock) 
		                                            inner join naufor..industry_issuers_new b with(nolock) on 
			                                            ((a.object_id=b.issuer_id) or (a.object_id=b.industry_id)) 
			                                            and (b.industry_id='{0}' or b.industry_id 
			                                            in (select id from naufor..industries_new with(nolock) where parent_id='{0}')))", m.industry_id));
                        continue;
                    }
                    else
                    {
                        conditions.Add(@"u.id  in (
				                                    select review_id from NAUFOR..review_object with(nolock) where object_type=0 and object_id in 
				                                    (
					                                    select id from naufor..industries_new with(nolock) where id not in
					                                    (
						                                    select industry_id from skrin_content_output.dbo.analytic_menu where industry_id is not null
						                                    union
						                                    select id from naufor..industries_new  where 
							                                    parent_id in (select industry_id from skrin_content_output.dbo.analytic_menu  where industry_id is not null)
					                                    )
				                                    )
			                                    )");
                        continue;
                    }
                }
            }
            if (conditions.Count > 0)
            {
                string prefinx = _so.anal_types_excl ? " not " : "";
                return prefinx + " ( " + string.Join(" or ", conditions) + " ) ";
            }
            return "";
        }

        public async Task<Result> Search()
        {
            return await Search(await _GetQueryString(),await _GetCountString());
        }


        public static async Task<Result> Search(string sql_result, string sql_count)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql_result, con);
                    con.Open();
                    cmd.CommandTimeout = 10000;
                    List<SearchResult> results = new List<SearchResult>();
                    int tot = 0;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while ( await reader.ReadAsync())
                        {
                            results.Add(new SearchResult
                            {
                                id = (string)reader["id"],
                                headline = (string)reader.ReadNullIfDbNull("headline"),
                                doc_id = (string)reader["doc_id"],
                                date = ((DateTime?)reader.ReadNullIfDbNull("date")).HasValue ? ((DateTime?)reader.ReadNullIfDbNull("date")).Value.ToString("dd:MM:yyyy HH:mm") : null,
                                is_daily = (bool?)reader.ReadNullIfDbNull("daily"),
                                author_id = (string)reader["author_id"],
                                pages = (Int16)reader["pages"],
                                author_name = (string)reader.ReadNullIfDbNull("author_name"),
                                file_name = (string)reader.ReadNullIfDbNull("file_name"),
                                a_type = (string)reader.ReadNullIfDbNull("a_type"),
                                no = (long)reader.ReadNullIfDbNull("RowNumber")
                            });
                        }
                    }
                    cmd.CommandText = sql_count;
                    tot = (int)cmd.ExecuteScalar();
                    return new Result
                    {
                        total = tot,
                        s_result = results
                    };
                }
            }
            catch (Exception ex)
            {
                string errorstring = string.Format("Запрос1: {0} <br/> Запрос2: {1} <br/> Ошибка: {2}", sql_result, sql_count, ex);
                Helper.SendEmail(errorstring, "Ошибка поиска по аналитике");
                return null;
            }
        }

        /// <summary>
        /// Вытаскивает типы аналитики, разбитые по уровням
        /// </summary>
        /// <param name="a_types"></param>
        /// <returns></returns>
        public static async Task<Dictionary<int, List<MenuTypes>>> GetAnalDic(string a_types)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                Dictionary<int, List<MenuTypes>> dic = new Dictionary<int, List<MenuTypes>>();
                List<MenuTypes> types = new List<MenuTypes>();
                string sql = "select id,industry_id,is_daily, is_industry from skrin_content_output.dbo.analytic_menu where id in (" + a_types + ") and parent_id=0";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        types.Add(new MenuTypes
                        {
                            id = (int)reader["id"],
                            industry_id = (string)reader.ReadNullIfDbNull("industry_id"),
                            is_industry = (bool?)reader.ReadNullIfDbNull("is_industry"),
                            is_daily = (bool?)reader.ReadNullIfDbNull("is_daily")
                        }
                            );
                    }
                    dic.Add(1, types);
                }
                types = new List<MenuTypes>();
                sql = "select id,industry_id,is_daily, is_industry from skrin_content_output.dbo.analytic_menu where id in (" + a_types + ") and parent_id!=0";
                cmd = new SqlCommand(sql, con);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        types.Add(new MenuTypes
                        {
                            id = (int)reader["id"],
                            industry_id = (string)reader.ReadNullIfDbNull("industry_id"),
                            is_industry = (bool?)reader.ReadNullIfDbNull("is_industry"),
                            is_daily = (bool?)reader.ReadNullIfDbNull("is_daily")
                        }
                            );
                    }
                    dic.Add(2, types);
                }
                return dic;
            }
        }

        public async Task<bool> writeStatistics(string doc_id, string author_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {                
                string sql = "insert into naufor..AnalyticsStatsDaily (doc_id, author_id) values('@doc_id', '@author_id')";
                using (SqlCommand cmd = new SqlCommand(sql,con))
                {
                    con.Open();     
                    cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 100).Value = doc_id;
                    cmd.Parameters.Add("@author_id", SqlDbType.VarChar, 100).Value = author_id;
                    int res = await cmd.ExecuteNonQueryAsync();
                    if (res > 0)
                    {
                        return true;
                    } 
                }
            
            }
            return false;
        }

        public async Task<List<rating>> GetRating(int type_id)
        {
            List<rating> ratings = new List<rating>();
            int k = 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"Select  a.issuer_id,ticker,ISNULL(a.short_name,a.name) short_name,isnull(LocalRating,'-') LocalRating,isnull(InternationalRating,'-') InternationalRating,isnull(ForeCast,'-') ForeCast,convert(varchar(10),Rating_Date,104) rd  from 
                        searchdb2..union_search a inner join naufor..nra_companies b on a.issuer_id=b.issuer_id and RatingTypeID=@type_id and b.isactive=1
                        inner join (Select issuer_id,max(Rating_Date) dt from naufor..nra_companies where RatingTypeID=@type_id group by issuer_id)c on c.issuer_id=b.issuer_id and b.Rating_Date=c.dt
                        inner join naufor..nra_scale d on d.rating=b.LocalRating order by d.so";
                sql = sql.Replace("@type_id",type_id.ToString());
                SqlCommand cmd = new SqlCommand(sql, con);         
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        k++;
                        ratings.Add(new rating() {
                            position = k,
                            issuer_id = (string)reader["issuer_id"],
                            ticker = (string)reader["ticker"],
                            short_name = reader["short_name"] != DBNull.Value ? (string)reader["short_name"] : "",
                            LocalRating = (string)reader["LocalRating"],
                            InternationalRating = (string)reader["InternationalRating"],
                            ForeCast = (string)reader["ForeCast"],
                            rd = (string)reader["rd"]
                        });
                    }                    
                }
            }
            return ratings;
        }

        public async Task<List<ReviewTypes>> GetRevTypes()
        {
            List<ReviewTypes> types = new List<ReviewTypes>();         
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select id,name, (select top 1 1 from skrin_content_output..analytic_menu where parent_id=a.id) as hc,
                        parent_id from skrin_content_output..analytic_menu a where parent_id= 0 or parent_id in 
                        (select id from skrin_content_output..analytic_menu where parent_id=0) order by abs(parent_id),id";             
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {                      
                        types.Add(new ReviewTypes()
                        {
                            id = (Int32)reader["id"],
                            name = (string)reader["name"],
                            parent_id = (Int32)reader["parent_id"]
                        });
                    }
                }
            }
            return types;
        }

    }
}