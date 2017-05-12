using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.Models.Authentication;
using Skrin.BLL.Infrastructure;
using System.Threading.Tasks;
using Skrin.Models.Iss;
using Skrin.Models.Search;
using System.Text;
using Skrin.BLL.Authorization;

namespace Skrin.BLL.Root
{
    public static class AuthenticateSqlUtilites
    {
        /// <summary>
        /// Список групп пользователя
        /// </summary>
        /// <param name="user_id">Идентификатор пользователя</param>
        /// <returns>Список с наименованием группы и количеством компаний в группе</returns>
        public static List<ExtGroup> GetExtGroupList(int? user_id)
        {
            List<ExtGroup> groups = new List<ExtGroup> { new ExtGroup { code = 0, txt = "Нет", val = "" } };
            if(user_id.HasValue && user_id!=0)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = "select ul.ListID,Replace(ListName,'\"','') as listName, isnull(ul_count,0) as cnt,cast(isnull(ul_count,0) as varchar(50)) + ' ЮЛ ' + " +
                                    "cast(isnull(ip_count,0)  as varchar(50)) + ' ИП'  as cnt_disp  from  security.dbo.secUserLists ul   where UserId=@user_id ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value=user_id;
                    con.Open();
                    using (SqlDataReader reader=cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            groups.Add(new ExtGroup
                            {
                                code = (int)reader["ListID"],
                                txt = reader.ReadEmptyIfDbNull("listName"),
                                val = reader.ReadEmptyIfDbNull("cnt_disp")
                                //val = ((int)reader["cnt"]).ToString()

                            });
                        }
                    }
                }
            }
            return groups;
        }

        public static String GetEgrulMonitoringList(int user_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select STUFF((select ','+issuer_id from web_shop.dbo.users_egrul_ogrn where user_id=@user_id for xml path ('')),1,1,'')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return reader.ReadEmptyIfDbNull(0);
                }
            }
            return "";
        }

        /// <summary>
        /// Список групп пользователя 
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static List<ListGroup> GetGroupList(int user_id)
        {
            List<ListGroup> ret = new List<ListGroup>();
            if (user_id > 0)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = @"select sul.ListID,sul.ListName, 
                        ul_count + ip_count as Count, cast(isnull(ul_count,0) as varchar(50)) + ' ЮЛ ' +
                        cast(isnull(ip_count,0)  as varchar(50)) + ' ИП'  as cnt_disp
				        from security.dbo.secUserLists sul 
				        where userid=@user_id 
				        order by ListName";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(new ListGroup
                            {
                                lid = (int)reader["ListID"],
                                name = (string)reader.ReadEmptyIfDbNull("ListName"),
                                cnt_disp = (string)reader["cnt_disp"],
                                cnt = (int)reader["Count"]
                            });
                        }
                    }

                }
            }
            return ret;
        }

        /// <summary>
        /// Список групп пользователя, включающих данную компанию 
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static List<ListGroup> GetGroupList(int user_id, string issuer_id)
        {
            List<ListGroup> ret = new List<ListGroup>();
            if (user_id > 0)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
              string sql = @"select sul.ListID,sul.ListName, 
                        ul_count + ip_count as Count, cast(isnull(ul_count,0) as varchar(50)) + ' ЮЛ ' +
                        cast(isnull(ip_count,0)  as varchar(50)) + ' ИП'  as cnt_disp
				        from security.dbo.secUserLists sul  inner join security..secUserListItems_Join j on j.ListID=sul.ListID
				        where userid=@user_id  and j.issuerID=@issuer_id  
			        order by ListName";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(new ListGroup
                            {
                                lid = (int)reader["ListID"],
                                name = (string)reader.ReadEmptyIfDbNull("ListName"),
                                cnt_disp = (string)reader["cnt_disp"],
                                cnt = (int)reader["Count"]
                            });
                        }
                    }

                }
            }
            return ret;
        }

        public static void SaveGroup(int user_id,ref int? list_id,ref string list_name, string iss, out int count,bool? is1000)
        {
            var iss_list = iss.Split(',');
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"WINDOWS-1251\"?><selection>");
            for (int i = 0; i < (iss_list.Length - ((iss_list.Length > 1) ? 1 : 0)); i++)
            {
                var a = iss_list[i].Split('_');
                if (a.Length == 2 && a[0] != "on" && (a[0].Length == 32 || !a[0].IsNaN()) && !a[1].IsNaN())
                {
                    sb.AppendLine("<selitem issuerid = \"" + a[0] + "\" typeid=\"" + a[1] + "\"></selitem>");
                }

            }
            sb.AppendLine("</selection>");

            count = 0;
            if (list_id == null)
                list_id = 0;
            if (user_id == 0)
                return;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net.dbo.groups_add_list";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user_id;

                var ListIdPar = cmd.Parameters.AddWithValue("@ListID", list_id);
                ListIdPar.SqlDbType = SqlDbType.Int;
                ListIdPar.Direction = ParameterDirection.InputOutput;

                var ListNamePar = cmd.Parameters.AddWithValue("@ListName", list_name);
                ListNamePar.SqlDbType = SqlDbType.VarChar;
                ListNamePar.Direction = ParameterDirection.InputOutput;

                cmd.Parameters.Add("@Selection", SqlDbType.Text).Value = sb.ToString();

                var CountPar = cmd.Parameters.AddWithValue("@cnt", count);
                CountPar.SqlDbType = SqlDbType.Int;
                CountPar.Direction = ParameterDirection.InputOutput;

                con.Open();
                cmd.ExecuteNonQuery();

                list_id = (int)ListIdPar.Value;
                list_name = Convert.IsDBNull(ListNamePar.Value) ? null : (string)ListNamePar.Value;
                count = (int)CountPar.Value;
            }
        }
        /// <summary>
        /// Максимальное Суммарное количество предприятий в группах, предназначенных для рассылки обновлений
        /// </summary>
        /// <returns></returns>
        public static int GetMaxCountForSending()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Select value from skrin_subscription..Consts where id=1", con);
                con.Open();
                return Int32.Parse((string)cmd.ExecuteScalar());
            }
        }

        public static string GetGroupInfo(string issuer_id, int user_id)
        {
            if(!string.IsNullOrWhiteSpace(issuer_id) && user_id>0)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = @"SELECT Replace(Replace(STUFF((SELECT ', ' + '<a href=''/dbsearch/dbsearchru/companies/userlists/'' target=''_blank''>' + listName + '</a>' from security..secUserListItems_Join a 
                                inner join security..secUserLists b on a.listID=b.listID 
							    where userid=@user_id and issuerID=@issuer_id
							   FOR XML PATH('')),1,1,''),'&lt;','<'), '&gt;','>')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar).Value = issuer_id;
                    using(SqlDataReader reader=cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if(reader.Read())
                        {
                            return reader.ReadEmptyIfDbNull(0);
                        }
                    }
                }
            }
            return "";
        }

        public static async Task SetUserReportLog(int user_id, ReportLimitType report_type,string report_code)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..reports_set_UserReportLog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@ReportId", SqlDbType.Int).Value = (int)report_type;
                cmd.Parameters.Add("@ReportCode", SqlDbType.VarChar, 50).Value = report_code;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<UserReportLimit> CheckReportLimit(int user_id, ReportLimitType report_type,string report_code)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..reports_check_limits", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@report_type", SqlDbType.Int).Value = (int)report_type;
                cmd.Parameters.Add("@report_code", SqlDbType.VarChar, 50).Value = report_code;
                con.Open();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    UserReportLimit rl = new UserReportLimit {Type=report_type};
                    if(await reader.ReadAsync())
                    {
                        rl.DayLimit = (int)reader["DayLimit"];
                        rl.MonthLimit = (int)reader["MonthLimit"];
                    }
                    return rl;
                }
            }
        }

        /// <summary>
        /// Сопоставляем AccessType и access_id из базы
        /// </summary>
        /// <param name="access_id"></param>
        /// <returns></returns>
        public static AccessType? GetAccessType(int access_id)
        {
            switch (access_id)
            {
                case 1:
                    return AccessType.Pred;
                case 2:
                    return AccessType.Emitent;
                case 7:
                    return AccessType.Bloom;
                case 9:
                    return AccessType.Mess;
                case 10:
                    return AccessType.Deal;
                case 11:
                    return AccessType.Ka;
                case 13:
                    return AccessType.KaPlus;
                case 14:
                    return AccessType.KaPoln;
                case 15:
                    return AccessType.MonEgrul;
                case 16:
                    return AccessType.TPrice;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Список ролей, которым доступна работа с группами
        /// </summary>
        /// <returns></returns>
        public static AccessType? GetGroupRoles()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id  from skrin_net..Skrin_AccessTypes where SiteId=1 and GroupLimit>0", con);
                con.Open();
                AccessType? at=null;
                using (var reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(at==null){
                            at=GetAccessType((int)reader[0]);
                        }else{
                            var b_at=GetAccessType((int)reader[0]);
                            if(b_at!=null){
                                at = at | b_at;
                            }                            
                        }
                       
                    }
                }
                return at;
            }

        }

        /// <summary>
        /// Список ролей, которым доступен мониторинг ЕГРЮЛ
        /// </summary>
        /// <returns></returns>
        public static AccessType? GetMonitorRoles()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id  from skrin_net..Skrin_AccessTypes where SiteId=1 and EgrulMonitorLimit>0", con);
                con.Open();
                AccessType? at = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (at == null)
                        {
                            at = GetAccessType((int)reader[0]);
                        }
                        else
                        {
                            var b_at=GetAccessType((int)reader[0]);
                            if (b_at != null)
                            {
                                at = at | b_at;
                            }
                        }

                    }
                }
                return at;
            }

        }

        /// <summary>
        /// Список ролей, которым доступен мониторинг сообщений
        /// </summary>
        /// <returns></returns>
        public static AccessType? GetMessageMonitorRoles()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id  from skrin_net..Skrin_AccessTypes where SiteId=1 and MessageGroupLimit>0", con);
                con.Open();
                AccessType? at = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (at == null)
                        {
                            at = GetAccessType((int)reader[0]);
                        }
                        else
                        {
                            var b_at = GetAccessType((int)reader[0]);
                            if (b_at != null)
                            {
                                at = at | b_at;
                            }
                        }

                    }
                }
                return at;
            }

        }

        /// <summary>
        /// Список ролей, которым доступен мониторинг обновлений
        /// </summary>
        /// <returns></returns>
        public static AccessType? GetUpdateMonitorRoles()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id  from skrin_net..Skrin_AccessTypes where SiteId=1 and UpdateGroupLimit>0", con);
                con.Open();
                AccessType? at = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (at == null)
                        {
                            at = GetAccessType((int)reader[0]);
                        }
                        else
                        {
                            var b_at = GetAccessType((int)reader[0]);
                            if (b_at != null)
                            {
                                at = at | b_at;
                            }
                        }

                    }
                }
                return at;
            }

        }
        /// <summary>
        /// Проверяет кол-во доступных для пользователя выписок,и если их > 0, то добавляет текущий код в "счетчик" пользователя
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="reg_code">код ИНН/ОГРН/ОГРНИП</param>
        /// <returns></returns>
        public static async Task<int> GetPaidEgrulRestAsync(int user_id, string reg_code)
        {
            if(user_id<=0)
                return 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..user_get_paid_egrul_rest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@reg_code", SqlDbType.VarChar, 15).Value = reg_code;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        return (int)reader[0];
                    }
                }
                return 0;
            }
        }
    }
}