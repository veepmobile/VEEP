using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Net.Mail;


namespace RestService.BLL
{
    public class Helper
    {
        // Генерация пароля
        public string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        // Генерация GUID
        public static string getNewGUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        // Хэширование пароля
        public string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        // Генерация SMS кода
        public string CreateRandomSMS()
        {
            string allowedChars = "0123456789";
            char[] chars = new char[4];
            Random rd = new Random();

            for (int i = 0; i < 4; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        // Перевод строки формата dd.mm.yy в дату
        public DateTime RUStr2Date(string sd)
        {
            IFormatProvider cult = new CultureInfo("ru-RU", false);
            DateTime dd = new DateTime(1901, 1, 1);
            try
            {
                dd = DateTime.ParseExact(sd, "dd.MM.yyyy HH:mm:ss", cult);
            }
            catch
            {
                string[] dd_hh = sd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] ddd = dd_hh[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                int d = 1;
                int m = 1;
                int y = 1901;
                int hh = 0;
                int mm = 0;
                int ss = 0;
                switch (ddd.Length)
                {
                    case 0:
                        break;
                    case 1:
                        y = getIntValue(ddd[0], 1901);
                        break;
                    case 2:
                        m = getIntValue(ddd[0], 1);
                        y = getIntValue(ddd[1], 1901);
                        break;
                    default:
                        d = getIntValue(ddd[0], 1);
                        m = getIntValue(ddd[1], 1);
                        y = getIntValue(ddd[2], 1901);
                        break;
                }
                if (dd_hh.Length > 1)
                {
                    string[] hhh = dd_hh[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (hhh.Length)
                    {
                        case 0:
                            break;
                        case 1:
                            hh = getIntValue(ddd[0], 0);
                            goto case 2;
                        case 2:
                            mm = getIntValue(ddd[1], 0);
                            goto default;
                        default:
                            ss = getIntValue(ddd[2], 0);
                            break;
                    }
                }
                dd = new DateTime(y, m, d, hh, mm, ss);
            }
            return dd;
        }

        public static int getIntValue(object o, int dflt)
        {
            int ret;
            try
            {
                if (o == null)
                    ret = dflt;
                else
                    ret = Convert.ToInt32(o);
            }
            catch
            {
                ret = dflt;
            }
            return ret;
        }

        public static ulong getULongValue(object o, ulong dflt)
        {
            ulong ret;
            try
            {
                if (o == null)
                    ret = dflt;
                else
                    ret = Convert.ToUInt64(o);
            }
            catch
            {
                ret = dflt;
            }
            return ret;
        }

        public static decimal getDecimalValue(object o, decimal dflt)
        {
            decimal ret;
            if (o == null)
                ret = dflt;
            else
            {
                try
                {
                    string sVal = o.ToString();
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    string DecSeparator = nfi.NumberDecimalSeparator;
                    string NotDecSeparator = DecSeparator == "," ? "." : ",";
                    sVal = sVal.Replace(NotDecSeparator, DecSeparator);
                    sVal = sVal.Replace(" ", "");
                    sVal = sVal.Replace("#", "");
                    ret = decimal.Parse(sVal);
                }
                catch
                {
                    ret = dflt;
                }
            }
            return ret;
        }

        public static float getFloatValue(object o, float dflt)
        {
            float ret;
            if (o == null)
                ret = dflt;
            else
            {
                try
                {
                    string sVal = o.ToString();
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    string DecSeparator = nfi.NumberDecimalSeparator;
                    string NotDecSeparator = DecSeparator == "," ? "." : ",";
                    sVal = sVal.Replace(NotDecSeparator, DecSeparator);
                    sVal = sVal.Replace(" ", "");
                    sVal = sVal.Replace("#", "");
                    ret = float.Parse(sVal.ToString());
                }
                catch
                {
                    ret = dflt;
                }
            }
            return ret;
        }

        public static string getStrValue(object s, string dflt)
        {
            string ret;
            try
            {
                if (s == null)
                    ret = dflt;
                else
                    ret = s.ToString();
            }
            catch
            {
                ret = dflt;
            }
            return ret;
        }

        public static DateTime getDateValue(object dd, DateTime dflt)
        {
            DateTime ret;
            try
            {
                ret = (dd == null) ? dflt : Convert.ToDateTime(dd);
            }
            catch
            {
                ret = dflt;
            }
            return ret;
        }

        public static string getSqlStrValue(object s, string dflt)
        {
            string ret = getStrValue(s, dflt);
            return ret.Replace("'", "''");
        }


        #region Logs
        public static void saveToLog(int? user_id, string user_key, string method_name, string method_params, string message, int is_error)
        {
           try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Helper hlp = new Helper();
                    SqlCommand cmd = new SqlCommand("Rest.dbo.SaveLog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    cmd.Parameters.Add("@method_name", SqlDbType.VarChar).Value = method_name;
                    cmd.Parameters.Add("@method_params", SqlDbType.VarChar).Value = method_params;
                    cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message;
                    cmd.Parameters.Add("@is_error", SqlDbType.Int).Value = is_error;
                    con.Open();
                    cmd.ExecuteNonQuery(); 
                }
           }
                catch (Exception ee1)
                {
                    string ee = ee1.Message;
                }

        }


        public static void saveToMessagesLog(int? user_id, string method_name, string method_params, string message, int is_error)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Helper hlp = new Helper();
                    SqlCommand cmd = new SqlCommand("Rest.dbo.SaveMessagesLog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@method_name", SqlDbType.VarChar).Value = method_name;
                    cmd.Parameters.Add("@method_params", SqlDbType.VarChar).Value = method_params;
                    cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message;
                    cmd.Parameters.Add("@is_error", SqlDbType.Int).Value = is_error;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ee1)
            {
                string ee = ee1.Message;
            }

        }

        public static void saveToAccountLog(int? user_id, string user_key, string message, string error)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Helper hlp = new Helper();
                    SqlCommand cmd = new SqlCommand("Rest.dbo.SaveAccountLog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message;
                    cmd.Parameters.Add("@error", SqlDbType.VarChar).Value = error;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ee1)
            {
                string ee = ee1.Message;
            }

        }

        #endregion

        #region Errors

        public static string GetError(int error_code, int language)
        {
            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Helper hlp = new Helper();
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetError", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@error_code", SqlDbType.Int).Value = error_code;
                    cmd.Parameters.Add("@language", SqlDbType.Int).Value = language;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = (reader["error"] != DBNull.Value) ? (reader["error"]).ToString() : "";
                    }
                }
            }
            catch
            {
                return "";
            }
            return ret;
        }
        #endregion
    }
}