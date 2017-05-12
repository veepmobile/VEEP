using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Skrin.Models.Iss.Menu;
using Skrin.BLL.Authorization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Skrin.BLL.Iss
{
    public class MenuRepository
    {

        private string _ticker;


        private string issuer_id, gks_id, inn, ogrn, okpo, search_name, search_name2;
        private int? group_id;
        private DateTime? create_date;
        private int? opf;

        private static string _constring = Configs.ConnectionString;
        private static bool _is_test = Configs.IsTest;

        public MenuRepository(string ticker)
        {
            _ticker = ticker;
        }


        public async Task<List<Menu>> GetProfileMenuAsync()
        {
            return await _GetMenuFromCache() ?? await _CreateProfileMenuAsync();
        }

        private async Task<List<Menu>> _CreateProfileMenuAsync()
        {
            bool success_create = true;
            object lockOn = new object();
            //парметры необходимые для проверки пунктов меню
            await _GetParams();
            //список пунктов меню из базы данных
            List<SQLMenu> menu_list = await _GetMenu();
            //список табов с запросами из базы данных
            List<TabQuery> queries = await _GetTabQueriesAsync();
            //проверка пунктов меню по запросам
            Parallel.ForEach(queries.Where(p => p.IsExist == null), q =>
            {
                try
                {
                    bool sql_exists = false, sphinx_exists = false, elastic_exists = false;
                    if (q.Query != null)
                    {
                        sql_exists = _IsQueryExists(q.Query);
                    }
                    if (q.QuerySphinx != null && q.QueryElastic == null) // на время отладки elastic !
                    {
                        IEnumerable<SphinxQuery> s_queries = JsonConvert.DeserializeObject<IEnumerable<SphinxQuery>>("[" + q.QuerySphinx + "]");
                        foreach (var query in s_queries)
                        {
                            if (_IsQuerySphinxExists(query))
                            {
                                sphinx_exists = true;
                                break;
                            }
                        }
                    }
                    if (q.QueryElastic != null)
                    {
                        elastic_exists = _IsQueryElasticExists(q.QueryElastic);
                    }
                    q.IsExist = sql_exists || sphinx_exists || elastic_exists;
                }
                catch (Exception ex)
                {
                    lock (lockOn)
                    {
                        success_create = false;
                        Helper.SendEmail(ex.ToString(), "SkrinNet - Ошибка построения меню -  tab_id: "+q.Id);
                    }
                    q.IsExist = false;
                }
            });

            return await _UpdateMenuInCache(_CreateMenu(menu_list, queries, 0),success_create);
        }


        private List<Menu> _CreateMenu(List<SQLMenu> sql_menus, List<TabQuery> tabs, int parent_id)
        {
            var menu = new List<Menu>();

            foreach (var sql_menu in sql_menus.Where(p => p.parent_id == parent_id).OrderBy(p => p.order))
            {
                var tab_list = tabs.Where(p => p.MenuId == sql_menu.id && p.IsExist == true).OrderBy(p => p.Order).ToList();
                //Если есть подчиненные меню или табы добавляем пункт в меню
                if (sql_menus.Any(p => p.parent_id == sql_menu.id) || tab_list.Count > 0)
                {
                    var m = new Menu
                    {
                        Id = sql_menu.id,
                        Name = sql_menu.name,
                        Order = sql_menu.order,
                        SubMenu = _CreateMenu(sql_menus, tabs, sql_menu.id),
                        Tabs = tab_list.Select(p => new Tab
                        {
                            Id = p.Id,
                            Accesses = p.Accesses,
                            Description = p.Description,
                            Link = p.Link,
                            Name = p.Name,
                            Order = p.Order
                        }).ToList()
                    };
                    //Дополнительная проверка, чтобы не было пустых sub_menu
                    if(m.SubMenu.Count>0 || m.Tabs.Count>0)
                    {
                        menu.Add(m);
                    }
                }
            }

            return menu;
        }


        private async  Task<List<Menu>>_UpdateMenuInCache(List<Menu> menu, bool need_to_save)
        {
            if (need_to_save)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("menu_tabs..skrin_menu_issuers_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                    cmd.Parameters.Add("@menu", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(menu);
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return menu;
             
        }

        private async Task<List<Menu>> _GetMenuFromCache()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT menu from [menu_tabs].[dbo].[skrin_menu_issuers] where ticker=@ticker and update_date > DATEADD(DAY,-1,getdate())", con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if(await reader.ReadAsync())
                    {
                        return JsonConvert.DeserializeObject<List<Menu>>((string)reader[0]);
                    }
                    return null;
                }
            }
        }


        private async Task _GetParams()
        {

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select issuer_id,gks_id,inn, ogrn,okpo,group_id,naufor.dbo.ClearOpf(name) search_name,naufor.dbo.ClearOpf(short_name) search_name2, create_date,opf from searchdb2..union_search where ticker=@ticker", con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        issuer_id = (string)reader.ReadNullIfDbNull("issuer_id");
                        gks_id = (string)reader.ReadNullIfDbNull("gks_id");
                        inn = (string)reader.ReadNullIfDbNull("inn");
                        ogrn = (string)reader.ReadNullIfDbNull("ogrn");
                        okpo = (string)reader.ReadNullIfDbNull("okpo");
                        group_id = (int?)reader.ReadNullIfDbNull("group_id");
                        search_name = (string)reader.ReadNullIfDbNull("search_name");
                        search_name2 = (string)reader.ReadNullIfDbNull("search_name2");
                        create_date = (DateTime?)reader.ReadNullIfDbNull("create_date");
                        opf = (int?)reader.ReadNullIfDbNull("opf");
                    }
                    else
                    {
                        throw new ArgumentException("Данный ticker не существует");
                    }
                }
            }
        }


        /// <summary>
        /// Загрузка данных от табах из базы
        /// </summary>
        /// <returns></returns>
        private async Task<List<TabQuery>> _GetTabQueriesAsync()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "select id, menu_id, s_order, name, description, link, query, query_sphinx, query_elastic, accesses from [menu_tabs].[dbo].[skrin_menu_tabs] where is_enable=1";
                if (opf == 90)
                {
                    sql = "select id, menu_id, s_order, name, description, link, query, query_sphinx, query_elastic, accesses from [menu_tabs].[dbo].[skrin_menu_tabs] where is_enable=1 and filials=1";
                }
                if (!_is_test)
                    sql += " and [is_test]=0";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                List<TabQuery> t_queries = new List<TabQuery>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string query = (string)reader.ReadNullIfDbNull("query");
                        string query_sphinx = (string)reader.ReadNullIfDbNull("query_sphinx");
                        string query_elastic = (string)reader.ReadNullIfDbNull("query_elastic");
                        string[] s_accesses = ((string)reader["accesses"]).Split(',');
                        var t_query = new TabQuery
                        {
                            Id = (int)reader["id"],
                            MenuId = (int)reader["menu_id"],
                            Order = (int)reader["s_order"],
                            Name = (string)reader["name"],
                            Description = (string)reader.ReadNullIfDbNull("description"),
                            Link = (string)reader["link"]
                        };
                        if (query == null && query_sphinx == null && query_elastic == null)
                        {
                            t_query.IsExist = true;
                        }
                        else
                        {
                            t_query.Query = "select top 1 1 " + query;
                            t_query.QuerySphinx = query_sphinx;
                            t_query.QueryElastic = query_elastic;
                        }
                        List<AccessType> accesses = new List<AccessType>();
                        foreach (string access in s_accesses)
                        {
                            AccessType a_type;
                            if (Enum.TryParse<AccessType>(access.Trim(), out a_type))
                            {
                                accesses.Add(a_type);
                            }
                        }
                        t_query.Accesses = accesses;
                        t_queries.Add(t_query);
                    }
                }
                return t_queries;
            }
        }

        /// <summary>
        /// Проверка конкретного запроса на существание данных
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private bool _IsQueryExists(string query)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id.DBVal();
                cmd.Parameters.Add("@gks_id", SqlDbType.VarChar, 32).Value = gks_id.DBVal();
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 15).Value = inn.DBVal();
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 15).Value = ogrn.DBVal();
                cmd.Parameters.Add("@okpo", SqlDbType.VarChar, 8).Value = okpo.DBVal();
                cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = group_id.DBVal();
                cmd.Parameters.Add("@create_date", SqlDbType.DateTime).Value = create_date.DBVal();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }
        }

        private bool _IsQuerySphinxExists(SphinxQuery sq)
        {
            //Флаг подстановки в запрос конкретного выражения
            bool has_any_replacement = false;

            var dic = new Dictionary<string, string>{
                {"@search_name@",string.Format("\"{0}\"",search_name)},
                {"@search_name2@",string.Format("\"{0}\"",search_name2)},
                {"@ogrn@",ogrn.IsNull()?"null":ogrn},
                {"@inn@",inn.IsNull()?"null":inn}
            };
            foreach (var rw in dic)
            {
                if (sq.query.IndexOf(rw.Key) >= 0)
                {
                    //если мы нашли шаблон заменителя и есть непустое слово-заменитель
                    //то подставляем его. Иначе просто удалим его
                    if(!string.IsNullOrWhiteSpace(rw.Value))
                    {
                        sq.query = sq.query.Replace(rw.Key, rw.Value);
                        has_any_replacement = true;
                    }
                    else
                    {
                        sq.query = sq.query.Replace(rw.Key, "");
                    }
                }
            }
            //Если в запросе не было ни одной подстановки конкретного значения то возвращаем false
            if (!has_any_replacement)
                return false;

            SphynxSearcher searcher = new SphynxSearcher(sq.query, sq.port, sq.charaster_set);
            string json_text = searcher.SearchJson();

            var json_result = JObject.Parse("{" + json_text + "}");
            var search_result = (JArray)json_result["results"];

            return search_result.Count > 0;
        }

        private bool _IsQueryElasticExists(string query)
        {
            bool has_any_replacement = false;

            var dic = new Dictionary<string, string>{
                {"@search_name@",search_name.IsNull()?"":Helper.FullTextString(search_name).Replace("\\","\\\\")},
                {"@search_name2@",search_name2.IsNull()?"":Helper.FullTextString(search_name2).Replace("\\","\\\\")},
                {"@ogrn@",ogrn.IsNull()?"":ogrn},
                {"@inn@",inn.IsNull()?"":inn}
            };
            foreach (var rw in dic)
            {
                if (query.IndexOf(rw.Key) >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(rw.Value))
                    {
                        query = query.Replace(rw.Key, rw.Value);
                        has_any_replacement = true;
                    }
                    else
                    {
                        query = query.Replace(rw.Key, "");
                    }
                }
            }

            if (!has_any_replacement)
                return false;

            string url = "/pravo/case/_search";
            ElasticClient ec = new ElasticClient();
            JObject r = ec.GetQuery(url, query);

            return (long)r["hits"]["total"] > 0;
        }
        /// <summary>
        /// Загрузка меню из базы данных
        /// </summary>
        /// <returns></returns>
        private async Task<List<SQLMenu>> _GetMenu()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select id, parent_id, s_order, name from menu_tabs.[dbo].[skrin_menu]", con);
                con.Open();
                List<SQLMenu> menu = new List<SQLMenu>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        menu.Add(new SQLMenu
                        {
                            id = (int)reader["id"],
                            parent_id = (int)reader["parent_id"],
                            order = (int)reader["s_order"],
                            name = (string)reader["name"]
                        });
                    }
                }
                return menu;
            }
        }

        /// <summary>
        /// Служебный класс с дополнительными полями для проверки существования этих табов у конкретного ЮЛ
        /// </summary>
        private class TabQuery : Tab
        {
            public int MenuId { get; set; }
            public bool? IsExist { get; set; }
            public string Query { get; set; }
            public string QuerySphinx { get; set; }
            public string QueryElastic { get; set; }
        }

        /// <summary>
        /// Проекция меню из базы данных
        /// </summary>
        private class SQLMenu
        {
            public int id { get; set; }
            public int parent_id { get; set; }
            public int order { get; set; }
            public string name { get; set; }
        }

        private class SphinxQuery
        {
            public string query { get; set; }
            public string port { get; set; }
            public string charaster_set { get; set; }
        }
    }
}