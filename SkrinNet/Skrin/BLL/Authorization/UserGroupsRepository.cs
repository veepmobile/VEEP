using Skrin.Models.Search;
using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Authentication;
using System.Text;

namespace Skrin.BLL.Authorization
{
    public class UserGroupsRepository
    {
        private class IssuerInfo
        {
            public string IssuerId { get; set; }
            public int TypeId { get; set; }

            public IssuerInfo()
            {

            }

            public IssuerInfo(string info)
            {
                var vals = info.Split('_');
                IssuerId = vals[0];
                TypeId = int.Parse(vals[1]);
            }

            public string ToXml()
            {
                return string.Format("<selitem issuerid = \"{0}\" typeid=\"{1}\"/>", IssuerId, TypeId);
            }

            public string ToSQL(string placer="")
            {
                return string.Format(" (IssuerID = '{0}' and {2}type_id = {1}) ",IssuerId, TypeId,placer);
            }
        }

        /// <summary>
        /// Список компаний в определенной группе
        /// </summary>
        public static async Task<List<ULDetails>> GetCompaniesInGroup(int group_id, int page = 1, int page_length = 20)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..user_list_get_companies_in_group", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = group_id;
                cmd.Parameters.Add("@page_no", SqlDbType.Int).Value = page;
                cmd.Parameters.Add("@page_length", SqlDbType.Int).Value = page_length;
                con.Open();
                var ret = new List<ULDetails>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ret.Add(new ULDetails
                        {
                            ticker = (string)reader["ticker"],
                            name = reader.ReadEmptyIfDbNull("name"),
                            inn = reader.ReadEmptyIfDbNull("inn"),
                            ogrn = reader.ReadEmptyIfDbNull("ogrn"),
                            okpo = reader.ReadEmptyIfDbNull("okpo"),
                            ruler = reader.ReadEmptyIfDbNull("ruler"),
                            legal_address = reader.ReadEmptyIfDbNull("legal_address"),
                            del = reader.ReadEmptyIfDbNull("del"),
                            okved = reader.ReadEmptyIfDbNull("okved"),
                            search_count = (int)reader["count"],
                            issuer_id = reader.ReadEmptyIfDbNull("issuer_id"),
                            type_id = reader.ReadEmptyIfDbNull("type_id"),
                            type_ka = reader.ReadEmptyIfDbNull("type_ka"),
                            ip_stop = reader.ReadEmptyIfDbNull("ip_stop")
                        });
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// Импорт кодов в новый/существующий список компаний
        /// </summary>
        public static async Task<Tuple<int,int>> ImportList(ImportCodes import_codes,int user_id, int group_limit)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string code in import_codes.codes.Split(','))
            {
                sb.Append(string.Format("{0},", code));
            }
            
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..user_list_import_list_string", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user_id;
                var list_id = cmd.Parameters.Add("@ListID", SqlDbType.Int);
                list_id.Value = import_codes.id;
                list_id.Direction = ParameterDirection.InputOutput;

                cmd.Parameters.Add("@ListName", SqlDbType.VarChar, 128).Value = import_codes.name;
                
                var res_count= cmd.Parameters.Add("@ResCount", SqlDbType.Int);
                res_count.Value = group_limit;
                res_count.Direction = ParameterDirection.InputOutput;
                var str = sb.ToString();
                cmd.Parameters.Add("@ImportMode", SqlDbType.Int).Value = (int)import_codes.code_type;
                cmd.Parameters.Add("@StringList", SqlDbType.Text).Value = str;
                cmd.Parameters.Add("@nofilials", SqlDbType.Bit).Value = import_codes.branch_exclude;
                con.Open();

                await cmd.ExecuteNonQueryAsync();

                return new Tuple<int, int>((int)list_id.Value, (int)res_count.Value);

            }
        }

        /// <summary>
        /// Удаление списка компаний
        /// </summary>
        public static async Task DeleteListAsync(int id)
        {
            if (id == 0)
                return;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..user_list_gelete_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@list_id", SqlDbType.Int).Value = id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Переименование списка компаний
        /// </summary>
        public static async Task RenameListAsync(int id, string name)
        {
            if (id == 0)
                return;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "update security.dbo.secUserLists Set ListName=@name where ListId =@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 250).Value = name;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Экспорт кодов из списка компаний
        /// </summary>
        public static async Task<List<string>> ExportListAsync(CodeType code_type, int id, List<string> issuer_infos)
        {
            string code = "";
            switch (code_type)
            {
                case CodeType.Inn:
                    code = "inn";
                    break;
                case CodeType.Ogrn:
                    code = "ogrn";
                    break;
                case CodeType.Ticker:
                    code = "ticker";
                    break;
            }
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<string> where_list=new List<string>();
                foreach (var item in issuer_infos)
                {
                    where_list.Add(new IssuerInfo(item).ToSQL("b."));
                }

                string where=where_list.Count==0 ? "": string.Format(" and ({0})",string.Join(" or ",where_list));


                string sql = string.Format("Select  {0}  from (SELECT  isnull(us.inn,ip.inn) inn,isnull(us.ogrn,ogrnip) as ogrn," +
                                            "isnull(us.ticker,ogrnip) as ticker " +
                                            "from security..secUserListItems_join b left join searchdb2..union_search us " +
                                            "on us.issuer_id=b.IssuerID " +
                                            "left join searchdb2..sphinx_search_ichp4 ip on ip.ogrnip=b.IssuerID " +
                                            "where ListID=@id {1}) a ", code, where);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                List<string> codes = new List<string>();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        string code_val = reader.ReadEmptyIfDbNull(0);
                        if (code_val != "")
                        {
                            codes.Add(code_val);
                        }
                        
                    }
                }
                return codes;
            }
        }

        
        /// <summary>
        /// Удаление списка компаний из группы
        /// </summary>
        public static async Task DeleteIssuersFromListAsync(int list_id,List<string> issuer_infos)
        {
            if (issuer_infos.Count == 0)
                return;

            List<string> where_list = new List<string>();
            foreach (var item in issuer_infos)
            {
                where_list.Add(new IssuerInfo(item).ToSQL());
            }

            string where = where_list.Count == 0 ? "" : string.Format(" and ({0})", string.Join(" or ", where_list));

            string sql = string.Format("delete from security.dbo.secUserListItems_Join_new  where ListId=@list_id  {0}", where);

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@list_id", SqlDbType.Int).Value = list_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Проверяет, принадлежит ли данная группа данному пользователю
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="list_id"></param>
        /// <returns></returns>
        public static bool IsGroupForUser(int user_id, int list_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select 1 from security.[dbo].[secUserLists] where UserID=@user_id and ListID=@list_id",con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@list_id", SqlDbType.Int).Value = list_id;
                con.Open();
                SqlDataReader reader=cmd.ExecuteReader();
                return reader.Read();
            }

        }


    }
}