using Skrin.BLL.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Skrin.BLL.Infrastructure
{
    public static class Extentions
    {
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }

        public static UserSession GetUserSession(this HttpContextBase context)
        {
            return (UserSession)context.Items["skrin_user_session"];
        }

        /// <summary>
        /// Вытаскивает значение sessionId из cookie
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCookieSessionId(this string input)
        {
            if (input == null)
                return null;
            return input.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        /// <summary>
        /// Вытаскивает значение UserId из cookie
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCookieUserId(this string input)
        {
            if (input != null)
            {
                string[] s = input.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length > 1)
                    return s[1];
            }
            return null;
        }

        /// <summary>
        /// Вытаскивает значение SourceId из cookie
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UserSource? GetCookieSourceId(this string input)
        {
            if (input != null)
            {                
                string[] s = input.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length > 2)
                {
                    try
                    {
                        return (UserSource)(int.Parse(s[2]));
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static object ReadNullIfDbNull(this IDataReader reader, int column)
        {
            object value = reader[column];
            return value == DBNull.Value ? null : value;
        }

        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ReadEmptyIfDbNull(this IDataReader reader, string column)
        {
            object value = reader[column];
            return value == DBNull.Value ? "" : (string)value;
        }

        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ReadEmptyIfDbNull(this IDataReader reader, int column)
        {
            object value = reader[column];
            return value == DBNull.Value ? "" : (string)value;
        }

        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static object ReadNullIfDbNull(this IDataReader reader, string column)
        {
            object value = reader[column];
            return value == DBNull.Value ? null : value;
        }

        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ReadNullJSONIfDbNull(this IDataReader reader, int column)
        {
            object value = reader[column];
            string ret_value = (string)(value == DBNull.Value ? null : value);
            if (string.IsNullOrEmpty(ret_value))
                return "[]";
            return ret_value;
        }

        /// <summary>
        /// Безопасно получает данные из ридера
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static object ReadNullIfDbNullReplaced(this IDataReader reader, int column)
        {
            Type dt = reader.GetFieldType(column);
            if (dt == typeof(Single))
                return reader.IsDBNull(column) ? null : reader.GetDouble(column).ToStandardNotationString();
            object value = reader[column];
            return value == DBNull.Value ? null : HttpUtility.JavaScriptStringEncode(value.ToString().Replace("\t", " ").Replace("\n", "<br/>").Replace("\r", " "));
        }

        /// <summary>
        /// Вытаскивает единственное значение из DataReader. В случае 0 или более 1-ого значения, возращается null
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string SingleVal(this IDataReader reader, string column)
        {
            string value = null;
            if (reader.Read())
            {
                value = (string)reader.ReadNullIfDbNull(column);
                if (reader.Read())
                {
                    return null;
                }
            }
            return value;
        }

        private static String ToStandardNotationString(this Single d)
        {
            //Keeps precision of double up to is maximum
            return d.ToString("0.#####################################################################################################################################################################");

        }
        private static String ToStandardNotationString(this double d)
        {
            //Keeps precision of double up to is maximum
            return d.ToString("0.#####################################################################################################################################################################################################################################################################################################################################");

        }

        /// <summary>
        /// Безопасно получает дату из ридера и  преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ReadDateToString(this IDataReader reader, string column)
        {
            object value = reader[column];
            if (value == DBNull.Value)
                return "";
            return ((DateTime)value).ToString("dd.MM.yyyy");
        }
        /// <summary>
        /// Безопасно получает дату из ридера и  преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ReadDateToString(this IDataReader reader, int column)
        {
            object value = reader[column];
            if (value == DBNull.Value)
                return "";
            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Безопасно преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRusString(this DateTime? input)
        {
            if (input == null)
                return "";
            return input.Value.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Безопасно преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRusString(this DateTime input)
        {
            return input.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Безопасно преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRusStringTime(this DateTime? input)
        {
            if (input == null)
                return "";
            return input.Value.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Безопасно преобразует в формат dd.MM.yyyy
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRusStringTime(this DateTime input)
        {
            return input.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Представляет сумму в виде округленного текста
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertTextSum(this decimal? input)
        {
            if (input == null)
                return "";

            if (input == decimal.MinValue)
                return Configs.Replacer+" р.";

            /*

            if (input > 1000000000)
            {
                return string.Format("{0} млрд.р.", Math.Round((decimal)(input.Value / 1000000000), 2));
            }
            if (input > 1000000)
            {
                return string.Format("{0} млн.р.", Math.Round((decimal)(input.Value / 1000000), 2));
            }
            if (input > 1000)
            {
                return string.Format("{0} тыс.р.", Math.Round((decimal)(input.Value / 1000), 2));
            }
            

            return string.Format("{0} р.", Math.Round(input.Value, 2));
            */
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            return string.Format(nfi, "{0:#,#.##} р.", input.Value);
        }

        /// <summary>
        /// Представляет сумму в виде округленного текста
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertTextSum(this string input)
        {
            try
            {
                decimal? d_input = Decimal.Parse(input);
                return d_input.ConvertTextSum();
            }
            catch
            {
                return input;
            }
        }


        
        /// <summary>
        /// Заменяет текстовую разметку на html разметку
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IHtmlString GetHtmlNewsText(this string input)
        {
            IDictionary<string,string> map = new Dictionary<string,string>(){
                {"\n","<br/>"},
                {"\t",""},
                {"\r",""},
                {"\"",""},
                {"“","&laquo;"},
                {"”","&raquo;"}
            };
            var regex = new Regex(String.Join("|",map.Keys));
            return new HtmlString(regex.Replace(input, m => map[m.Value]));
        }

        /// <summary>
        /// Кодирует в html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        /// <summary>
        /// Кодирует url
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlEncode(this string input)
        {
            return HttpUtility.UrlEncode(input);
        }

        /// <summary>
        /// "Параметризует" значения, указанные через запятую
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ParamVals(this string input)
        {
            string[] ids = input.Split(',');
            Dictionary<string, int> ret = new Dictionary<string, int>();
            for (int i = 0; i < ids.Length; i++)
            {
                ret.Add("@p" + i, int.Parse(ids[i]));
            }
            return ret;
        }

        /// <summary>
        /// Получает коллекцию имен/занчений собственных строк
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static NameValueCollection NameValCollection(this object input)
        {
            var fields = input.GetType().GetProperties();
            var nv = new NameValueCollection();
            foreach(var field in fields)
            {
                nv.Add(field.Name, field.GetValue(input).ToString());
            }
            return nv;
        }

        /// <summary>
        /// Получает коллекцию имен/занчений собственных строк в виде param=value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ParamCollection(this object input)
        {
            var fields = input.GetType().GetProperties();
            return string.Join("&", fields.Select(p => string.Format("{0}={1}", p.Name, p.GetValue(input))).ToArray());
        }

        /// <summary>
        /// Проверка является ли строка числом
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNaN(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return true;
            long result;
            return !Int64.TryParse(input, out result);
        }

        /// <summary>
        /// Если строка пустая или null, возвращает DBNull.Val. В противном случае возвращает исходное значение 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object DBVal(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return DBNull.Value;
            return input;
        }

        /// <summary>
        /// Если значение null, возвращает DBNull.Val. В противном случае возвращает исходное значение 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object DBVal<T>(this Nullable<T> input)where T : struct  
        {
            if (input == null)
                return DBNull.Value;
            return input.Value;
        }

        /// <summary>
        /// Если данные не пустые, выдаем данные определенного формата
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static HtmlString Formated(this string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new HtmlString("");
            return new HtmlString(string.Format(pattern, input));
        }


        public static string NullableToString(this decimal? input, string format=null)
        {
            if (input == null)
                return "";
            if (input == decimal.MinValue)
                return Configs.Replacer;
            return input.Value.ToString(format);
        }


        public static string NullableToString(this int? input, string format = null)
        {
            if (input == null)
                return "";
            if (input == int.MinValue)
                return Configs.Replacer;
            return input.Value.ToString(format);
        }

        public static string NullableToString(this double? input, string format = null)
        {
            if (input == null)
                return "";
            if (input == double.MinValue)
                return Configs.Replacer;
            return input.Value.ToString(format);
        }

        public static string NullableToString(this long? input, string format=null)
        {
            if (input == null)
                return "";
            if (input == long.MinValue)
                return Configs.Replacer;
            return input.Value.ToString(format);
        }

        public static string SaveReplace(this string input, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return input.Replace(oldValue, newValue);
        }

        /// <summary>
        /// Сравнение значения со значением по умолчанию для данного типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrDefault<T>(this Nullable<T> value) where T : struct  
        {
            if (value == null)
                return true;
            return object.Equals(value, default(T));
        }

        public static string getStrValue(this IDataReader rd, int fld_no, string dflt)
        {
            return rd.IsDBNull(fld_no) ? dflt : rd.GetString(fld_no);
        }

        public static int getIntValue(this IDataReader rd, int fld_no, int dflt)
        {
            return rd.IsDBNull(fld_no) ? dflt : rd.GetInt32(fld_no);
        }

        /// <summary>
        /// Выводит строку в формате HtmlString
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static HtmlString Html(this string input)
        {
            return new HtmlString(input);
        }
	/// <summary>
        /// Чистка строки для поиска в Сфинксе
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Clear(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return input.Replace("\"", " ").Replace("!", "").Replace("-", " ").Replace("'", " ").Replace("\"", " ").Replace("(", "").Replace(")", "");
        }
        /// <summary>
        /// Преобразует дату вида 'dd.MM.yyyy hh:mm:ss' в Unix Time 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Int64? UnixDateTimeStamp(this string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;
            DateTime dt;
            if (DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return (Int64?)Math.Floor((dt - new DateTime(1970, 1, 1)).TotalSeconds);
            }
            return null;
        }

        /// <summary>
        /// Преобразует дату вида 'dd.MM.yyyy' в Unix Time 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Int64? UnixTimeStamp(this string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;
            DateTime dt;
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return (Int64?)Math.Floor((dt - new DateTime(1970, 1, 1)).TotalSeconds);
            }
            return null;

        }

        /// <summary>
        /// "Параметризует" значения, указанные через запятую
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParamValsString(this string input)
        {
            string[] ids = input.Split(',');
            Dictionary<string, string> ret = new Dictionary<string, string>();
            for (int i = 0; i < ids.Length; i++)
            {
                ret.Add("@p" + i, ids[i].Replace("'", ""));
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static JToken TransformForSorting(this JToken input, string sort_type)
        {
            if (input.Type == JTokenType.String)
            {
                if (sort_type == "int")
                {
                    int result;
                    int.TryParse((string)input, out result);
                    return result;
                }
                else
                {
                    return (JToken)(((string)input).ToLower());
                }
            }

            return input;
        }

        /// <summary>
        /// Замена string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNull(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }


        public static string EmptyNull(this string input)
        {
            return input ?? "";
        }

        /// <summary>
        /// Безопасно обрезает строку, если указана максимальная длина берет ее и добавлет в конце точки
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static string SaveTrim(this string input,int? max_lenght=null)
        {
            if (input == null)
                return null;

            input = input.Trim();

            if (max_lenght!=null && input.Length > max_lenght)
            {
                input = input.Substring(0, max_lenght.Value) + "...";
            }

            return input;
        }

    }
}