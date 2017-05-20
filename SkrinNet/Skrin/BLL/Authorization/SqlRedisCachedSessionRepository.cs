using Newtonsoft.Json;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Skrin.BLL.Authorization
{
    public class SqlRedisCachedSessionRepository:IUserSessionRepository
    {
        private string _constring = Configs.ConnectionString;

        private const string BLACK_IP_KEY = "black_ip_{0}";
        private const string SESSION_KEY = "session_{0}";
        private const string USER_KEY = "user_{0}";
        private const string USER_INFO_KEY = "user_info_{0}";
        private const string EXTERNAL_USER_INFO_KEY = "external_user_info_{0}";
        private const string IP_KEY = "ip_{0}_user_{1}";

        

        public SkrinUser GetUser(string sessionId)
        {
            UserSource? source = sessionId.GetCookieSourceId() ?? UserSource.Skrin;
            int? user_id = _Get<int?>(string.Format(SESSION_KEY, sessionId.GetCookieSessionId()));
            if (user_id == null)
                return null;
            SkrinUser user = null;
            if (source == UserSource.Skrin)
            {
                user = _Get<SkrinUser>(string.Format(USER_INFO_KEY, user_id.Value));
                return user ?? CacheUser(_GetUserFromSql(user_id.Value));
            }
            else
            {
                user = _Get<SkrinUser>(string.Format(EXTERNAL_USER_INFO_KEY, user_id.Value));
                return user ?? CacheExternalUser(ExternalUserRepository.GetUser(user_id.Value));
            }
            
        }

        public SkrinUser GetUser(string login, string password)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(@"select Id, Login, Password, UsePCBinding, BoundIp, BoundBrowser,Email,IpRestriction,IsBlockedNow, UseCloud
                                                from skrin_net.[dbo].[Skrin_Users] where Login=@login and Password=@password", con);
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 100).Value = login;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = password;
                con.Open();
                SkrinUser user = null;
                string ipRestriction = null;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    
                    if (reader.Read())
                    {
                        user = new SkrinUser
                        {
                            Id = (int)reader["Id"],
                            Login = login,
                            Password = password,
                            Email = (string)reader.ReadEmptyIfDbNull("Email"),
                            UsePCBinding = (bool)reader["UsePCBinding"],
                            BoundIp = (string)reader.ReadNullIfDbNull("BoundIp"),
                            BoundBrowser = (string)reader.ReadNullIfDbNull("BoundBrowser"),
                            IsBlocked = (bool)reader["IsBlockedNow"],
                            UseCloud = (bool)reader["UseCloud"]
                        };
                        ipRestriction = (string)reader.ReadNullIfDbNull("IpRestriction");
                    }
                }
                if (user != null)
                {
                    user.SitesAccess = _GetUserAccess(user.Id, con);
                }
                if (user != null && !string.IsNullOrWhiteSpace(ipRestriction))
                {
                    user.HasIpRestriction = true;
                    user.AllowedIps = ipRestriction.Split(',').Select(p=>p.Trim()).ToList();
                }
                return CacheUser(user);
            }
        }

        public bool IsBlackIp(string ip)
        {
            //Проверяем по кешированному значению, если его нет, то достаем его из БД и кешируем на 5 минут
            string ip_key=string.Format(BLACK_IP_KEY,ip);
            bool? is_black = _Get<bool?>(ip_key);
            if(is_black.HasValue)
            {
                return is_black.Value;
            }
            is_black = _CheckIpForBlackListSQL(ip);
            RedisStore.Set(ip_key, is_black, new TimeSpan(0, 5, 0));
            return is_black.Value;
        }
        
        public bool UserHaveAnotherActiveSession(SkrinUser user, string sessionId)
        {
            string active_session_id = _Get<string>(string.Format(USER_KEY, user.Id));
            if (string.IsNullOrEmpty(active_session_id))
                return false;
            return active_session_id != sessionId.GetCookieSessionId();
        }

        public void UpdateUserSession(SkrinUser user, string sessionId)
        {
            RedisStore.Set(string.Format(SESSION_KEY, sessionId.GetCookieSessionId()), user.Id);
            RedisStore.Set(string.Format(USER_KEY, user.Id), sessionId.GetCookieSessionId(), new TimeSpan(0, 5, 0));
        }

        public void DeleteOldSessions(SkrinUser user, string sessionId)
        {
            return;
        }

        public void UpdateUser(SkrinUser user)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE [skrin_net].[dbo].[Skrin_Users]
                           SET 
                               [BoundIp] = @BoundIp
                              ,[BoundBrowser] = @BoundBrowser
                         WHERE [Id] = @user_id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user.Id;
                cmd.Parameters.Add("@BoundIp", SqlDbType.VarChar, 100).Value = user.BoundIp;
                cmd.Parameters.Add("@BoundBrowser", SqlDbType.VarChar, 100).Value = user.BoundBrowser;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void KillUserSession(string sessionId)
        {
            RedisStore.Db.KeyDelete(string.Format(USER_KEY, sessionId.GetCookieUserId()));
            RedisStore.Db.KeyDelete(string.Format(SESSION_KEY, sessionId.GetCookieSessionId())); 
        }

        public bool BlockForErrorLogin(string login)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..Skrin_BlockForErrorLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 100).Value = login;
                con.Open();
                return (bool)cmd.ExecuteScalar();
            }
        }


        public int AddRequest(string ip, string user)
        {
            string ip_key=string.Format(IP_KEY,ip,user);
            int? request_count = _Get<int?>(ip_key);
            if (request_count == null)
            {
                RedisStore.Set(ip_key, 1, new TimeSpan(0, 10, 0));
                return 1;
            }
            else
            {
                request_count++;
                RedisStore.Update(ip_key, request_count.Value, new TimeSpan(0, 10, 0));
                return request_count.Value;
            }
        }

        public void BlockIp(string ip,string user)
        {
            if (_IsItBadBot(ip))
            {
                //Добавим ip в базу
                if (_AddIpToBlock(ip, user))
                {
                    string message = string.Format("Заблокирован ip:{0},user_id:{1}", ip, user);
                    Helper.SendEmail(message, "СКРИН-КОНТРАГЕНТ: Блокировка IP", new List<string> { "sales@skrin.ru" });
                    //обновим кеш
                    RedisStore.Set(string.Format(BLACK_IP_KEY, ip), true, new TimeSpan(0, 5, 0));
                }
            }
        }

        #region private

        private T _Get<T>(string key)
        {
            var saved_val = RedisStore.Db.StringGet(key);
            if (string.IsNullOrEmpty(saved_val))
                return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(saved_val);
            }
            catch(Exception ex)
            {
                string error_message=string.Format("Ошибка десериализации объекта {0} в тип {1}: {2}",saved_val,typeof(T),ex);
                Helper.SendEmail(error_message, "Redis Ошибка десериализации");
                return default(T);
            }
        }

        

        private SkrinUser CacheUser(SkrinUser user)
        {
            if (user != null)
                RedisStore.Set(string.Format(USER_INFO_KEY, user.Id), user, new TimeSpan(0, 5, 0));
            return user;
        }

        private SkrinUser CacheExternalUser(SkrinUser user)
        {
            if (user != null)
                RedisStore.Set(string.Format(EXTERNAL_USER_INFO_KEY, user.Id), user, new TimeSpan(24, 0, 0));
            return user;
        }

        private SkrinUser _GetUserFromSql(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(@"select Id, Login, Password, UsePCBinding, BoundIp, BoundBrowser,Email,IpRestriction,IsBlockedNow, UseCloud
                                                from skrin_net.[dbo].[Skrin_Users] where Id=@user_id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                SkrinUser user = null;
                string ipRestriction = null;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        user = new SkrinUser
                        {
                            Id = user_id,
                            Login = (string)reader["Login"],
                            Password = (string)reader["Password"],
                            UsePCBinding = (bool)reader["UsePCBinding"],
                            Email=(string)reader.ReadNullIfDbNull("Email"),
                            BoundIp = (string)reader.ReadNullIfDbNull("BoundIp"),
                            BoundBrowser = (string)reader.ReadNullIfDbNull("BoundBrowser"),
                            IsBlocked = (bool)reader["IsBlockedNow"],
                            UseCloud = (bool)reader["UseCloud"]
                        };
                        ipRestriction = (string)reader.ReadNullIfDbNull("IpRestriction");
                    }
                }
                if (user != null)
                {
                    user.SitesAccess = _GetUserAccess(user_id, con);
                    _LogVisit(user.Id);
                }
                if (user != null && !string.IsNullOrWhiteSpace(ipRestriction))
                {
                    user.HasIpRestriction = true;
                    user.AllowedIps = ipRestriction.Split(',').Select(p => p.Trim()).ToList();
                }
                return user;
            }
        }


        private  void _LogVisit(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..update_last_visits", con);                
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user_id);
                cmd.Parameters.AddWithValue("@SiteId", 1);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Добавление списка подписок пользователя, применяемых на сайте СКРИН
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        private List<Access> _GetUserAccess(int user_id, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT ua.AccessTypeId,ua.IsOutOfDate,ua.IsTest,at.GroupLimit,at.EgrulMonitorLimit, at.MessageGroupLimit, at.UpdateGroupLimit, at.FileSizeLimit,at.EgrulGroupLimit  
                                            from [skrin_net].[dbo].[Skrin_UserAccess] ua
                                            join skrin_net..Skrin_AccessTypes at on ua.AccessTypeId=at.Id
                                            where [UserId]=@user_id", con);
            cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                List<Access> ret = new List<Access>();
                while (reader.Read())
                {
                    int a_type_Id = (int)reader["AccessTypeId"];
                    AccessType? a_type = AuthenticateSqlUtilites.GetAccessType(a_type_Id);
                    if (a_type != null)
                    {
                        ret.Add(new Access
                        {
                            IsOutOfDate = (bool)reader["IsOutOfDate"],
                            AccessType = a_type.Value,
                            IsTest = (bool)reader["IsTest"],
                            EgrulMonitorLimit = (int)reader["EgrulMonitorLimit"],
                            GroupLimit = (int)reader["GroupLimit"],
                            MessageGroupLimit = (int)reader["MessageGroupLimit"],
                            UpdateGroupLimit = (int)reader["UpdateGroupLimit"],
                            FileSizeLimit = (int)reader["FileSizeLimit"],
                            EgrulGroupLimit = (int)reader["EgrulGroupLimit"]
                        });
                    }
                }
                return ret;
            }
        }

       

        


        private bool _AddIpToBlock(string ip,string user)
        {
            int user_id = 0;
            int.TryParse(user, out user_id);

            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "skrin_net..block_ip";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.Add("@ip", SqlDbType.VarChar).Value = ip;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                return (bool)cmd.ExecuteScalar();
            }
        }


        private bool _CheckIpForBlackListSQL(string ip)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select 1 from skrin_net.[dbo].[Skrin_BlackIps] where Ip=@ip", con);
                cmd.Parameters.Add("@ip", SqlDbType.VarChar).Value = ip;
                con.Open();
                using(SqlDataReader reader=cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }
        }

        /// <summary>
        /// Проверка является ли данный бот допустимым для сканирования сайта
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private bool _IsItBadBot(string ip)
        {
            ip = ip.Trim();
            List<string> good_bots = _GetGoodBots();
            try
            {
                string dns_name = Dns.GetHostEntry(ip).HostName;
                //проверим входит ли бот в список допустимых
                bool is_in_good = false;
                foreach (string good_bot in good_bots)
                {
                    if (dns_name.IndexOf(good_bot, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        is_in_good = true;
                        break;
                    }
                }
                if (!is_in_good)
                    return true;
                //проверим действительно ли он допустимый
                List<string> proved_ips = Dns.GetHostEntry(dns_name).AddressList.Select(p => p.ToString()).ToList();
                if (proved_ips.Contains(ip))
                {
                    _AddToIpExceptions(ip, dns_name);
                    return false;
                }
                return true;
            }
            catch
            {
                //Если при проверке произошли ошибки, то считаем бота плохим
                return true;
            }
        }

        private List<string> _GetGoodBots()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select name from [skrin_net].[dbo].[Skrin_BlackIPs_GoodBots]", con);
                con.Open();
                var ret = new List<string>();
                using (var reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret.Add((string)reader[0]);
                    }
                }
                return ret;
            }
        }

        private void _AddToIpExceptions(string ip, string description)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"if not exists (select 1 from [skrin_net].[dbo].[Skrin_BlackIps_Exceptions] where Ip=@Ip)
                            BEGIN
	                            INSERT INTO [skrin_net].[dbo].[Skrin_BlackIps_Exceptions]
	                            (Ip, Description)
	                            VALUES
	                            (@Ip, @Description)
                            END";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@Ip", SqlDbType.VarChar).Value = ip;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar,250).Value = description;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        #endregion




        public void LogAuthResult(AuthLog log)
        {
            string stages = null;
            if (log.auth_result != AuthenticationType.Ok)
            {
                var sb = new StringBuilder();
                foreach (var stage in log.stages)
                {
                    sb.AppendFormat("<stage>{0}</stage>", stage);
                }
                stages = string.Format("<stages>{0}</stages>", sb);
            }
            string auth_type= log.auth_result==null ? null : log.auth_result.ToString();
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"insert into [logs2].[dbo].[Skrin_UserLogin_Log]
                        (login, password, ip, stages, auth_result, session_id, exception,browser)
                        VALUES
                        (@login, @password, @ip, @stages, @auth_result, @session_id, @exception,@browser)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 250).Value = log.login.DBVal();
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 250).Value = log.password.DBVal();
                cmd.Parameters.Add("@ip", SqlDbType.VarChar, 50).Value = log.ip.DBVal();
                cmd.Parameters.Add("@stages", SqlDbType.VarChar).Value = stages.DBVal();
                cmd.Parameters.Add("@auth_result", SqlDbType.VarChar, 100).Value = auth_type.DBVal();
                cmd.Parameters.Add("@session_id", SqlDbType.VarChar, 100).Value = log.session_id.DBVal();
                cmd.Parameters.Add("@exception", SqlDbType.VarChar).Value = log.exception.DBVal();
                cmd.Parameters.Add("@browser", SqlDbType.VarChar, 250).Value = log.browser.DBVal();
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public bool UserInUnlockQueue(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..users_check_unlock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", user_id);
                con.Open();
                return (bool)cmd.ExecuteScalar();
            }
        }


        public void KillActiveSession(SkrinUser user)
        {
            RedisStore.Db.KeyDelete(string.Format(USER_KEY, user.Id));
        }


        public void ConfirmCloudUsing(int user_id)
        {
            //обновим информацию в базе
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE [skrin_net].[dbo].[Skrin_Users]
                           SET 
                               UseCloud=1
                         WHERE [Id] = @user_id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            //после очистим кеш
            RedisStore.Del(string.Format(USER_INFO_KEY, user_id));
        }
    }

   
}