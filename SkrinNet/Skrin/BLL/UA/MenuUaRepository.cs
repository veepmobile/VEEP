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

namespace Skrin.BLL.UA
{
    public class MenuUaRepository
    {
        //отчеты в html [ua3].[dbo].[html_docs]
        //директория \skrinweb1\ua_docs


        private string _edrpou;

        private static string _constring = Configs.ConnectionString;
        private static bool _is_test = Configs.IsTest;

        public MenuUaRepository(string edrpou)
        {
            _edrpou = edrpou;
        }


        public async Task<List<Menu>> GetProfileMenuAsync()
        {
            return await _GetMenuFromCache() ?? await _CreateProfileMenuAsync();
        }

        private async Task<List<Menu>> _CreateProfileMenuAsync()
        {
            //парметры необходимые для проверки пунктов меню
//            await _GetParams();
            //список пунктов меню из базы данных
            List<SQLMenu> menu_list = await _GetMenu();
            //список табов с запросами из базы данных
            List<TabQuery> queries = await _GetTabQueriesAsync();
            //проверка пунктов меню по запросам
            Parallel.ForEach(queries.Where(p => p.IsExist == null), q =>
            {
                bool sql_exists=false, sphinx_exists=false;
                if (q.Query != null)
                {
                    sql_exists=_IsQueryExists(q.Query);
                }
                if(q.QuerySphinx!=null)
                {
                    IEnumerable<SphinxQuery> s_queries=JsonConvert.DeserializeObject<IEnumerable<SphinxQuery>>("["+q.QuerySphinx+"]");
                    foreach (var query in s_queries)
	                {
		               if(_IsQuerySphinxExists(query))
                       {
                           sphinx_exists=true;
                           break;
                       }
	                }
                }
                q.IsExist = sql_exists || sphinx_exists;
            });

            return await _UpdateMenuInCache(_CreateMenu(menu_list, queries, 0));
        }


        private List<Menu> _CreateMenu(List<SQLMenu> sql_menus, List<TabQuery> tabs, int parent_id)
        {
            var menu = new List<Menu>();

            foreach (var sql_menu in sql_menus.Where(p => p.parent_id == parent_id).OrderBy(p => p.order))
            {
                var tab_list = tabs.Where(p => p.MenuId == sql_menu.id && p.IsExist == true).ToList();
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


        private async  Task<List<Menu>>_UpdateMenuInCache(List<Menu> menu)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("menu_tabs..UA3_menu_issuers_update",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 32).Value = _edrpou;
                cmd.Parameters.Add("@menu",SqlDbType.VarChar).Value=JsonConvert.SerializeObject(menu);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }

            return menu;
             
        }

        private async Task<List<Menu>> _GetMenuFromCache()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT menu from [menu_tabs].[dbo].[UA3_menu_issuers] where edrpou=@edrpou", con);
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 32).Value = _edrpou;
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

/*
        private async Task _GetParams()
        {

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select edrpou, sourceId, sortedname, sortedname2 from ua3..union_issuers where edrpou=@edrpou", con);
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 32).Value = _edrpou;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        edrpou = (string)reader.ReadNullIfDbNull("edrpou");
                        sourceId = (string)reader.ReadNullIfDbNull("sourceId");
                        sortedname = (string)reader.ReadNullIfDbNull("sortedname");
                        sortedname2 = (string)reader.ReadNullIfDbNull("sortedname2");
                    }
                    else
                    {
                        throw new ArgumentException("Данный ЕДРПОУ не существует");
                    }
                }
            }
        }
*/

        /// <summary>
        /// Загрузка данных от табах из базы
        /// </summary>
        /// <returns></returns>
        private async Task<List<TabQuery>> _GetTabQueriesAsync()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "select id, menu_id, s_order, name, description, link, query, query_sphinx, accesses from [menu_tabs].[dbo].[UA3_menu_tabs] where is_enable=1";
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
                        if (query == null && query_sphinx==null)
                        {
                            t_query.IsExist = true;
                        }
                        else
                        {
                            t_query.Query = "select top 1 1 " + query;
                            t_query.QuerySphinx = query_sphinx;
                        }
                        List<AccessType> accesses = new List<AccessType>();
                        foreach (string access in s_accesses)
                        {
                            accesses.Add((AccessType)Enum.Parse(typeof(AccessType), access.Trim()));
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
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 8).Value = _edrpou.DBVal();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }
        }

        private bool _IsQuerySphinxExists(SphinxQuery sq)
        {
            /*
            //Флаг подстановки в запрос конкретного выражения
            bool has_any_replacement = false;

            var dic = new Dictionary<string, string>{
                {"@search_name@",string.Format("\"{0}\"",search_name)},
                {"@search_name2@",string.Format("\"{0}\"",search_name2)},
                {"@ogrn@",ogrn},
                {"@inn@",inn}
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
             */
            return false;
        }

        /// <summary>
        /// Загрузка меню из базы данных
        /// </summary>
        /// <returns></returns>
        private async Task<List<SQLMenu>> _GetMenu()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select id, parent_id, s_order, name from menu_tabs..[UA3_menu]", con);
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