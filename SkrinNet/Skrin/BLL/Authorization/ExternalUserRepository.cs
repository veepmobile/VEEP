using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.BLL.Infrastructure;
using System.Text;


namespace Skrin.BLL.Authorization
{
    public static class ExternalUserRepository
    {

        private static string _constring = Configs.ConnectionString;

        public static SkrinUser GetUser(int user_id)
        {
            //достать ключ пользователя из базы
            //проверить его валидность
            //в случае его правильности вернуть информацию о пользователе, иначе вернуть null
            if (user_id == 0)
            {
                return null;
            }
            var s_params = _GetServiceParams(user_id);
            if (s_params == null)
            {
                return null;
            }
            return ValidateKey(s_params.service_key, s_params.source, s_params.tagert);
        }

        /// <summary>
        /// Выдает user_id пользователя Скрин, на основании проверки на сайте-источнике
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SkrinUser ValidateKey(string key, UserSource source, string target = null)
        {

            AuthLog log=new AuthLog{
                 income_url=HttpContext.Current.Request.RawUrl,
                 source=source.ToString(),
                 target=target
            };
            try
            {
                SkrinUser user = null;
                //проверить валидность ключа
                switch (source)
                {
                    case UserSource.B2B:
                        var b2b_info = _GetB2BUserInfo(key,log);
                        //SaveAuthLog();
                        if (b2b_info.error != null)
                        {
                            return null;
                        }

                        user =  _UpdateB2BUserInfo(b2b_info, key);
                        log.user_id = user.Id;
                        return user;
                    case UserSource.Rts223:
                    case UserSource.Rts44:
                        var rts_info = _GetRTSInfo(key, source, target,log);
                        if (rts_info == null)
                        {
                            return null;
                        }
                        user = _UpdateRtsTenderInfo(rts_info, source, key, target);
                        log.user_id = user.Id;
                        return user;
                    default:
                        return null;
                }
            }
            finally
            {
                SaveAuthLog(log);
            }

        }


        private static void SaveAuthLog(AuthLog log)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    string sql = @"insert into [logs2].[dbo].[Skrin_ExteranlUsers_Log]
                            (income_url, outcome_url, user_info, source, target, external_user_id, user_id)
                            VALUES
                            (@income_url, @outcome_url, @user_info, @source, @target, @external_user_id, @user_id)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@income_url", SqlDbType.VarChar, 1024).Value = log.income_url.DBVal();
                    cmd.Parameters.Add("@outcome_url", SqlDbType.VarChar, 1024).Value = log.outcome_url.DBVal();
                    cmd.Parameters.Add("@user_info", SqlDbType.VarChar, 2000).Value = log.user_info.DBVal();
                    cmd.Parameters.Add("@source", SqlDbType.VarChar, 50).Value = log.source.DBVal();
                    cmd.Parameters.Add("@target", SqlDbType.VarChar, 100).Value = log.target.DBVal();
                    cmd.Parameters.Add("@external_user_id", SqlDbType.VarChar, 50).Value = log.external_user_id.DBVal();
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = log.user_id.DBVal();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Helper.SendEmail(ex.ToString(), "Ошибка записи лога ExternalUser");
            }
        }


        #region b2b

        /// <summary>
        /// Валидация и получение информации о клиенте b2b
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static B2BInfo _GetB2BUserInfo(string key,AuthLog log)
        {
            List<string> queries = new List<string>();
            B2BInfo b2b_info = new B2BInfo();
            string url_pattern = "http://www.b2b-center.ru/integration/json/User.Info?session_key={0}";
            var b2b_info_part = _GetB2BUserInfoPart(key, url_pattern,queries);
            if (b2b_info_part.error != null)
            {
                return b2b_info_part;
            }
            b2b_info.user_info = b2b_info_part.user_info;
            url_pattern = "http://www.b2b-center.ru/integration/json/Firm.Info?session_key={0}";
            b2b_info_part = _GetB2BUserInfoPart(key, url_pattern,queries);
            if (b2b_info_part.error != null)
            {
                return b2b_info_part;
            }
            b2b_info.firm_info = b2b_info_part.firm_info;
            url_pattern = "http://www.b2b-center.ru/integration/json/User.Contacts?session_key={0}";
            b2b_info_part = _GetB2BUserInfoPart(key, url_pattern,queries);
            if (b2b_info_part.error != null)
            {
                return b2b_info_part;
            }
            b2b_info.contacts = b2b_info_part.contacts;
            log.user_info = XMLGenerator<B2BInfo>.GetStringXML(b2b_info);
            log.outcome_url = string.Join("; ", queries);
            return b2b_info;
        }


        private static B2BInfo _GetB2BUserInfoPart(string key, string service_url, List<string> queries)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string url = string.Format(service_url, key);
                queries.Add(url);
                string info = client.DownloadString(url);
                return JsonConvert.DeserializeObject<B2BInfo>(info);
            }
        }




        /// <summary>
        /// Обновляем/добавляем информацию о пользователе b2b в нашей базе
        /// </summary>
        /// <param name="info"></param>
        /// <returns>user_id</returns>
        private static SkrinUser _UpdateB2BUserInfo(B2BInfo info, string key)
        {
            var e_info = new ExternalUserInfo(info);
            int user_id = _UpdateExternalUserInfo(e_info, UserSource.B2B, key, null);
            return new SkrinUser
            {
                Id = user_id,
                Login = e_info.ContactPerson,
                SitesAccess = new List<Access> { new Access { AccessType = AccessType.KaPlus, IsOutOfDate = false } }
            };
        }

        private static SkrinUser _UpdateRtsTenderInfo(RTSTenderInfo info, UserSource source, string key, string target)
        {
            var e_info = new ExternalUserInfo(info);
            int user_id = _UpdateExternalUserInfo(e_info, source, key, target);
            return new SkrinUser
            {
                Id = user_id,
                Login = e_info.ContactPerson,
                SitesAccess = new List<Access> { new Access { AccessType = AccessType.Ka, IsOutOfDate = false } }
            };
        }


        #endregion

        #region rts_tender


        private static RTSTenderInfo _GetRTSInfo(string key, UserSource source, string target,AuthLog log)
        {
            string url_pattern = "";
            if (source == UserSource.Rts223)
            {
                switch (target)
                {
                    //RTS-Tender.223 PRODUCTION
                    case "production":
                        url_pattern = "https://223.rts-tender.ru/customer/lk/webservices/skrin.ashx?token={0}&action=authentication";
                        break;
                    //RTS-Tender.223 TEST (Волгоград)
                    case "test_vlg":
                        url_pattern = "https://app-trunkfks-223.agilevlg-srv.ru/customer/lk/webservices/skrin.ashx?token={0}&action=authentication";
                        break;
                    //RTS-Tender.223 TEST (Москва)
                    case "test_msk":
                        url_pattern = "https://223-test.rts-tender.ru/customer/lk/webservices/skrin.ashx?token={0}&action=authentication";
                        break;
                    //RTS-Tender.223 DEMO
                    case "demo":
                        url_pattern = "https://223-demo.rts-tender.ru/customer/lk/webservices/skrin.ashx?token={0}&action=authentication";
                        break;
                }
            }
            if (source == UserSource.Rts44)
            {
                url_pattern = "https://www.rts-tender.ru/WebServices/Skrin.ashx?token={0}&action=authentication";
            }
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string url = string.Format(url_pattern, key);
                log.outcome_url = url;
                string info = client.DownloadString(url);
                var rts_info= JsonConvert.DeserializeObject<RTSTenderInfo>(info);
                log.user_info = XMLGenerator<RTSTenderInfo>.GetStringXML(rts_info);
                log.external_user_id = rts_info.Id;
                return rts_info;
            }
        }

        #endregion

        private static int _UpdateExternalUserInfo(ExternalUserInfo ui, UserSource source, string key, string target)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                var cmd = new SqlCommand("skrin_net..update_external_user", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExtenalUserId", ui.Id);
                cmd.Parameters.AddWithValue("@Source", (int)source);
                cmd.Parameters.AddWithValue("@SourceKey", key);
                cmd.Parameters.AddWithValue("@FullName", ui.FullName.DBVal());
                cmd.Parameters.AddWithValue("@ShortName", ui.ShortName.DBVal());
                cmd.Parameters.AddWithValue("@Country", ui.Country.DBVal());
                cmd.Parameters.AddWithValue("@Email", ui.Email.DBVal());
                cmd.Parameters.AddWithValue("@Inn", ui.Inn.DBVal());
                cmd.Parameters.AddWithValue("@Kpp", ui.Kpp.DBVal());
                cmd.Parameters.AddWithValue("@Ogrn", ui.Ogrn.DBVal());
                cmd.Parameters.AddWithValue("@Phone", ui.Phone.DBVal());
                cmd.Parameters.AddWithValue("@ContactPerson", ui.ContactPerson.DBVal());
                cmd.Parameters.AddWithValue("@SourceTarget", target.DBVal());
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@UserId"].Value;
            }
        }

        private class ServiceParams
        {
            public string service_key { get; set; }
            public UserSource source { get; set; }
            public string tagert { get; set; }

        }

        private class AuthLog
        {
            public string income_url { get; set; }
            public string outcome_url { get; set; }
            public string user_info { get; set; }
            public string source { get; set; }
            public string target { get; set; }
            public string external_user_id { get; set; }
            public int? user_id { get; set; }
        }

        private static ServiceParams _GetServiceParams(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                var cmd = new SqlCommand("select SourceKey, Source, SourceTarget from skrin_net..Skrin_ExteranlUsers where UserId=@UserId", con);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user_id;
                con.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        return new ServiceParams
                        {
                            service_key = (string)reader["SourceKey"],
                            source = (UserSource)(int)reader["Source"],
                            tagert = (string)reader.ReadNullIfDbNull("SourceTarget")
                        };
                    }
                }
                return null;
            }
        }

    }


    #region b2b_classes

    public class B2BInfo
    {
        public B2BErrorInfo error { get; set; }
        public B2BUserInfo user_info { get; set; }
        public B2BFirmInfo firm_info { get; set; }
        public B2BContactsInfo contacts { get; set; }
    }

    public class B2BErrorInfo
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class B2BUserInfo
    {
        public int id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string mid_name { get; set; }
    }

    public class B2BFirmInfo
    {
        public string name { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public int country { get; set; }
    }

    public class B2BContactsInfo
    {
        public string email { get; set; }
        public string phone { get; set; }
    }

    #endregion

    public class RTSTenderInfo
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Email { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
    }

    /// <summary>
    /// Обобщенный класс информации
    /// </summary>
    public class ExternalUserInfo
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public int? Country { get; set; }
        public string Email { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }

        public ExternalUserInfo(RTSTenderInfo rts_info)
        {
            this.Id = rts_info.Id;
            this.FullName = rts_info.FullName;
            this.ShortName = rts_info.ShortName;
            this.Email = rts_info.Email;
            this.Inn = rts_info.Inn;
            this.Kpp = rts_info.Kpp;
            this.Ogrn = rts_info.Ogrn;
            this.Phone = rts_info.Phone;
            this.ContactPerson = rts_info.ContactPerson;
        }

        public ExternalUserInfo(B2BInfo b_info)
        {
            this.Id = b_info.user_info.id.ToString();
            this.FullName = b_info.firm_info.name;
            this.Email = b_info.contacts.email;
            this.Inn = b_info.firm_info.inn;
            this.Kpp = b_info.firm_info.kpp;
            this.Phone = b_info.contacts.phone;
            this.ContactPerson = string.Join(" ", new List<string> { b_info.user_info.last_name, b_info.user_info.first_name, b_info.user_info.mid_name });
            this.Country = b_info.firm_info.country;
        }
    }

}