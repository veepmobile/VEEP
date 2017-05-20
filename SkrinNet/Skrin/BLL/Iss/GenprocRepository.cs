using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content.GenProc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Skrin.BLL.Iss
{
    public class GenprocRepository
    {
        public static GenprocResultXML CreateGenprocCheckXML(Filters filters)
        {
            GenprocResultXML ret = new GenprocResultXML();
            GenprocResult ret1 = new GenprocResult();
            GenprocResultXML genproc = GenprocRepository.GetGenprocCheckXML(filters);
            try
            {

                if (genproc != null && filters.Organ == null && filters.Purpose == null)
                {
                    ret.GenprocCheckXML = genproc.GenprocCheckXML;

                }
                else
                {
                    GenprocResult search = GenprocRepository.SearchGenprocCheck(filters);
                    if (search != null)
                    {
                        ret1.GenprocCheck = search.GenprocCheck;

                        if (String.IsNullOrEmpty(filters.Purpose) && String.IsNullOrEmpty(filters.Organ))
                        {
                            GenprocRepository.SaveGenprocCheck(filters, search);
                        }

                        //Теперь соберем структуру XML
                        StringBuilder sb = new StringBuilder("<dates inn=\"" + filters.INN + "\" ogrn=\"" + filters.OGRN + "\" name=\"" + filters.SearchName.Replace("\"", "&quot;") + "\"><years>");
                        for (int i = DateTime.Now.Year; i > 2009; i--)
                        {
                            sb.Append("<val y=\"" + i.ToString() + "\" sel=\"" + ((filters.Year == i) ? "1" : "0") + "\" />");
                        }
                        sb.Append("</years><months>");

                        for (int i = 0; i < 13; i++)
                        {
                            sb.Append("<val m=\"" + i.ToString() + "\" sel=\"" + ((i == filters.Month) ? "1" : "0") + "\" />");
                        }
                        sb.Append("</months><genproc>");
                        sb.Append(ret1.GenprocCheck);
                        sb.Append(search.OrganCheck);
                        sb.Append(search.PurposeCheck);
                        sb.Append("</genproc></dates>");
                        ret.GenprocCheckXML = sb.ToString();
                        /*
                            genproc = GenprocRepository.GetGenprocCheckXML(filters);
                        ret.GenprocCheckXML = genproc.GenprocCheckXML;*/
                    }
                    else
                    {
                        ret.GenprocCheckXML = null;
                    }
                }
            }
            catch
            {
                ret.GenprocCheckXML = null;
            }

            //return ret;
            return (ret.GenprocCheckXML!=null) ? ret : GetNoFindXML(filters);
        }

        private static GenprocResultXML GetNoFindXML(Filters filters)
        {
            GenprocResultXML ret = new GenprocResultXML();

            StringBuilder sb = new StringBuilder("<dates inn=\"" + filters.INN + "\" ogrn=\"" + filters.OGRN + "\" name=\"" + filters.SearchName.Replace("\"", "&quot;") + "\"><years>");
            for (int i = DateTime.Now.Year; i > 2009; i--)
            {
                sb.Append("<val y=\"" + i.ToString() + "\" sel=\"" + ((filters.Year == i) ? "1" : "0") + "\" />");
            }
            sb.Append("</years><months>");

            for (int i = 0; i < 13; i++)
            {
                sb.Append("<val m=\"" + i.ToString() + "\" sel=\"" + ((i == filters.Month) ? "1" : "0") + "\" />");
            }
            sb.Append("</months><genproc><data cnt=\"0\" warning=\"По Вашему запросу ничего не найдено\" /><purpose /><organ /></genproc></dates>");
            ret.GenprocCheckXML = sb.ToString();

            return ret;
        }

        /// <summary>
        /// Получаем результат поиска при его наличии (с учетом времени устаревания 1 сутки) в XML
        /// </summary>
        public static GenprocResultXML GetGenprocCheckXML(Filters filters)
        {
            GenprocResultXML result = new GenprocResultXML();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("naufor.dbo.genproc_checking_get_XML", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = filters.INN;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = filters.OGRN;
                cmd.Parameters.Add("@cmonth", SqlDbType.Int).Value = filters.Month;
                cmd.Parameters.Add("@cyear", SqlDbType.Int).Value = filters.Year;
                con.Open();
                try
                {
                    XmlReader reader = cmd.ExecuteXmlReader();
                    if (reader.Read())
                    {
                        result.GenprocCheckXML = (string)reader.ReadOuterXml();
                        return result;
                    }
                }
                catch
                {
                    return null;
                }
                return null;
            }
        }


        /// <summary>
        /// Записываем результат поиска списка после получения с сайта
        /// </summary>
        public static void SaveGenprocCheck(Filters filters, GenprocResult result)
        {
            if (result != null && String.IsNullOrEmpty(filters.Purpose) && String.IsNullOrEmpty(filters.Organ))
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("naufor.dbo.genproc_checking_set_xml", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = filters.INN;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = filters.OGRN;
                    cmd.Parameters.Add("@cmonth", SqlDbType.Int).Value = filters.Month;
                    cmd.Parameters.Add("@cyear", SqlDbType.Int).Value = filters.Year;
                    cmd.Parameters.Add("@search_result", SqlDbType.VarChar).Value = result.GenprocCheck;
                    cmd.Parameters.Add("@purpose", SqlDbType.VarChar).Value = result.PurposeCheck;
                    cmd.Parameters.Add("@organ", SqlDbType.VarChar).Value = result.OrganCheck;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Получаем детали проверки при их наличии (с учетом времени устаревания 1 сутки)
        /// </summary>
        public static CheckDetails GetDetails(int cyear, int id, string org)
        {
            CheckDetails details = new CheckDetails();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("naufor.dbo.genproc_details_get_xml", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@check_id", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@check_year", SqlDbType.Int).Value = cyear;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    details.CheckId = id;
                    details.CheckYear = cyear;
                    details.CheckOrgan = org;
                    details.CheckHtml = reader["html"] != DBNull.Value ? reader["html"].ToString() : "";
                    DateTime dt = reader["extract_date"] != DBNull.Value ? (DateTime)reader["extract_date"] : DateTime.Today;
                    details.ExtractDate = dt.ToShortDateString();
                    return details;
                }
                return null;
            }
        }

        /// <summary>
        /// При отсутствии данных скачиваем детали проверки с сайта прокуратуры
        /// </summary>
        public static CheckDetails LoadDetails(int cyear, int id, string org)
        {
            string result = "";
            try
            {
                string url = "http://plan.genproc.gov.ru/plan" + cyear.ToString() + "/detail.php?ID=" + id;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, */*";
                request.Headers.Add("Accept-Language", "ru-RU");
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";
                request.AllowAutoRedirect = false;
                request.Timeout = 20000;
                request.Host = "plan.genproc.gov.ru";
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                result = reader.ReadToEnd();
                result = ConvertString(result, Encoding.GetEncoding(response.CharacterSet), Encoding.Default);
                reader.Close();
            }
            catch (Exception e)
            {
                Helper.SendEmail(e.Message, "Ошибка считывания деталей проверок");
                return null;
            }
            if (result != "")
            {
                return ParserDetails(result, cyear, id, org);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Парсим детали проверки
        /// </summary>
        public static CheckDetails ParserDetails(string value, int cyear, int id, string org)
        {
            if (value.Contains("ничего не найдено") || value == "")
            {
                return null;
            }
            int firstIndex = value.IndexOf("<div class=\"registry-plan\">");
            int lastIndex;
            //if (cyear == 2010)
            //{
            //    lastIndex = value.LastIndexOf("<p align=\"right\"><a href=\"/\">Вернуться");
            //}
            //else
            //{
                lastIndex = value.LastIndexOf("</main>");
            //}
            value = value.Substring(0, lastIndex);
            value = value.Substring(firstIndex);
            value = value.Replace("*", "");

            CheckDetails details = new CheckDetails();
            details.CheckHtml = value;
            details.CheckId = id;
            details.CheckYear = cyear;
            details.CheckOrgan = org;
            details.ExtractDate = DateTime.Today.ToShortDateString();

            SaveCheckDetails(details);

            return details;
        }

        /// <summary>
        /// Записываем детали проверки в базу
        /// </summary>
        public static void SaveCheckDetails(CheckDetails details)
        {
            if (details != null)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("naufor.dbo.genproc_details_set_xml", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@check_id", SqlDbType.Int).Value = details.CheckId;
                    cmd.Parameters.Add("@check_year", SqlDbType.Int).Value = details.CheckYear;
                    cmd.Parameters.Add("@organ", SqlDbType.VarChar).Value = details.CheckOrgan;
                    cmd.Parameters.Add("@html", SqlDbType.VarChar).Value = details.CheckHtml;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region http

        /// <summary>
        /// При отсутствии данных скачиваем список ID проверок с сайта прокуратуры
        /// </summary>
        public static GenprocResult SearchGenprocCheck(Filters filters)
        {
            string url = "";
            string m = "";
            string beginDate = "";
            string endDate = "";
            if (filters.Month != 0)
            {
                m = filters.Month.ToString();
                if (filters.Month < 10)
                {
                    beginDate = "01.0" + m + "." + filters.Year.ToString();
                    endDate = DateTime.DaysInMonth(filters.Year, filters.Month).ToString() + ".0" + m + "." + filters.Year.ToString(); // последний день месяца
                }
                else
                {
                    beginDate = "01." + m + "." + filters.Year.ToString();
                    endDate = DateTime.DaysInMonth(filters.Year, filters.Month).ToString() + "." + m + "." + filters.Year.ToString(); // последний день месяца
                }

            }
            string captcha_id = "";
            string c_result = "";
            string parse_result = "";
            string captcha_fullpath = "";
            string captcha = "";
            if (filters.Year > 2014)
            {
                //Значит надо лезть за капчей
                String DS = "http://plan.genproc.gov.ru/plan" + filters.Year.ToString() + "/";
                var resp = GetPage(DS);
                string ress = GetResponseContent(resp);
                Match match = Regex.Match(ress, "/bitrix/tools/captcha\\.php\\?captcha_code=(.*?)\"");

                if (!match.Success)
                    throw new Exception("Ошибка. Не найден блок каптчи");

                captcha_id = match.Groups[1].Value;
                using (WebClient wc = new WebClient())
                {
                    captcha_fullpath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/") + captcha_id + ".jpg";
                    wc.Encoding = Encoding.UTF8;
                    wc.DownloadFile(string.Format("http://plan.genproc.gov.ru/bitrix/tools/captcha.php?captcha_code={0}", captcha_id), captcha_fullpath);
                    CaptureRecognizer cr = new CaptureRecognizer(captcha_fullpath, "de03663cff2f122be3dd395cf59c2f7a");
                    c_result = cr.GetVal();
                    System.IO.File.Delete(captcha_fullpath);

                    if (!CaptureRecognizer.ParseCapthaResult(c_result, out parse_result))
                    {
                        throw new Exception("Ошибка определения каптчи: " + parse_result);
                    }
                    captcha = parse_result;
                }

            }
            switch (filters.Year)
            {
                case 2017:
                    url = "http://plan.genproc.gov.ru/plan2017/?ogrn=" + filters.OGRN + "&inn=" + filters.INN + "&name=&month=" + m.ToString() + "&control=&address=&captcha_code=" + captcha_id + "&captcha_word=" + parse_result + "&set_filter=Y";
                    break;
                case 2016:
                    url = "http://plan.genproc.gov.ru/plan2016/?ogrn=" + filters.OGRN + "&inn=" + filters.INN + "&name=&month=" + m.ToString() + "&control=&address=&captcha_code=" + captcha_id + "&captcha_word=" + parse_result + "&set_filter=Y";
                    break;
                case 2015:
                    url = "http://plan.genproc.gov.ru/plan2015/?ogrn=" + filters.OGRN + "&inn=" + filters.INN + "&name=&month=" + m.ToString() + "&control=&address=&captcha_code=" + captcha_id + "&captcha_word=" + parse_result + "&set_filter=Y";
                    break;
                case 2014:
                    url = "http://plan.genproc.gov.ru/plan2014/?ogrn=" + filters.OGRN + "&inn=" + filters.INN + "&name=&month=" + m.ToString() + "&control=&set_filter=Y";
                    break;
                case 2013:
                    url = "http://plan.genproc.gov.ru/plan2013/index.php?arrFilter_pf%5BINN%5D=" + filters.INN + "&arrFilter_ff%5BNAME%5D=&arrFilter_pf%5Bcode_subject%5D=&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_FROM%5D=" + beginDate + "&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_TO%5D=" + endDate + "&arrFilter_pf%5Bname_control_organiz%5D=&set_filter=%C8%F1%EA%E0%F2%FC&set_filter=Y";
                    break;
                case 2012:
                    url = "http://plan.genproc.gov.ru/plan2012/index.php?arrFilter_pf%5Bnumber_verify_system%5D=&arrFilter_ff%5BNAME%5D=&arrFilter_pf%5BINN%5D=" + filters.INN + "&arrFilter_pf%5Bcode_subject%5D=&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_FROM%5D=" + beginDate + "&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_TO%5D=" + endDate + "&arrFilter_pf%5Bname_KO%5D=&set_filter=%C8%F1%EA%E0%F2%FC&set_filter=Y";
                    break;
                case 2011:
                    url = "http://plan.genproc.gov.ru/plan2011/index.php?arrFilter_pf%5Bnumber_verify_system%5D=&arrFilter_ff%5BNAME%5D=&arrFilter_pf%5BINN%5D=" + filters.INN + "&arrFilter_pf%5Bcode_subject%5D=&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_FROM%5D=" + beginDate + "&arrFilter_pf%5Bdate_verify_start%5D%5BDATE_TO%5D=" + endDate + "&arrFilter_pf%5Bname_KO%5D=&set_filter=%C8%F1%EA%E0%F2%FC&set_filter=Y";
                    break;
                case 2010:
                    url = "http://plan.genproc.gov.ru/plan2010/index.php?NUMPP=&NAME=&INN=" + filters.INN + "&REGION=&DATE_FROM=" + beginDate + "&DATE_TO=" + endDate + "&ISP_NAME=&set_filter=%C8%F1%EA%E0%F2%FC";
                    break;
                default:
                    string month = (filters.Month == 0) ? "" : filters.Month.ToString();
                    url = "http://plan.genproc.gov.ru/plan" + filters.Year.ToString() + "/?ogrn=" + filters.OGRN + "&inn=" + filters.INN + "&name=&month=" + month + "&control=&set_filter=Y";
                    break;
            }
            string result = "";
            string cookies = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, */*";
                request.Headers.Add("Accept-Language", "ru-RU");
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";
                request.AllowAutoRedirect = false;
                request.Timeout = 20000;
                request.Host = "plan.genproc.gov.ru";
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cookies = (String.IsNullOrEmpty(response.Headers["Set-Cookie"])) ? "" : response.Headers["Set-Cookie"];

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                result = reader.ReadToEnd();
                result = ConvertString(result, Encoding.GetEncoding(response.CharacterSet), Encoding.Default);
                reader.Close();
            }
            catch (Exception ee)
            {
                string eee = ee.Message;
                //return null;
            }
            GenprocResult ret = new GenprocResult();
            ret = ParserText(filters, result);

            /*
            switch (filters.Year)
            {

                case 2014:
                    ret = ParserText(filters, result);
                    break;
                case 2013:
                    ret = ParserText(filters, result);
                    break;
                case 2012:
                    ret = ParserText2012(filters, result);
                    break;
                case 2011:
                    ret = ParserText2012(filters, result);
                    break;
                case 2010:
                    ret = ParserText2012(filters, result);
                    break;
                default:
                    ret = ParserText(filters, result);
                    break;
            }
            */

            return ret;
        }


        public static HttpWebResponse GetPage(string url) // get запрос
        {
            CookieContainer container = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = container;
            //if (coo != null)
            //{
            //    req.CookieContainer.Add(coo);
            //}
            req.KeepAlive = true;
            req.Referer = url;
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:29.0) Gecko/20100101 Firefox/29.0";
            req.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            return response;
        }

        public static string GetResponseContent(HttpWebResponse response)  // response to stream
        {
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public static string ConvertString(string value, Encoding src, Encoding trg)
        {
            Decoder dec = src.GetDecoder();
            byte[] ba = trg.GetBytes(value);
            int len = dec.GetCharCount(ba, 0, ba.Length);
            char[] ca = new char[len];
            dec.GetChars(ba, 0, ba.Length, ca, 0);
            return new string(ca);
        }

        /// <summary>
        /// Парсим список проверок (2010-2014)
        /// </summary>
        public static GenprocResult ParserText(Filters filters, string value)
        {
            filters.Purpose = ClearString(filters.Purpose);
            filters.Organ = ClearString(filters.Organ);
            GenprocResult ret = new GenprocResult();
            StringBuilder sb = new StringBuilder("");
            string warning = "";

        try
        { 
            int firstIndex = value.IndexOf("<div class=\"registry-result\">");
            int lastIndex = value.IndexOf("</main>");
            int no_find = value.IndexOf("не найдено");
            int notes = 0;
            if (value.Contains("найдено слишком"))
            {
                notes = -1;
            }
            if (firstIndex < 0 || no_find > 0)
            {
                warning = "По Вашему запросу ничего не найдено";
                ret.GenprocCheck = sb.Append("<data cnt=\"0\" warning=\"" + warning + "\"></data>").ToString();
                return ret;
            }            
            value = value.Substring(0, lastIndex);
            value = value.Substring(firstIndex);
            firstIndex = value.IndexOf("</h2>");
            value = value.Substring(firstIndex).Replace("<div class=\"registry-result__item\">", "*").Replace("</div>", "");
            var items = value.Split('*');
            int n = items.Length - 1;
            //if (notes < 0)
            //{
            //    n = 20;
            //}
            if (n == 0)
            {
                warning = "По Вашему запросу ничего не найдено";
                ret.GenprocCheck = sb.Append("<data cnt=\"0\" warning=\"" + warning + "\"></data>").ToString();
                return ret;
            }

            List<CheckDetails> list_cd = new List<CheckDetails>();
            List<string> list_purpose = new List<string>();
            List<string> list_organ = new List<string>();

            for (int i = 1; i <= n; i++)
            {
                CheckDetails cd = new CheckDetails();
                cd.CheckId = 0;
                cd.CheckMonth = 0;
                cd.CheckYear = filters.Year;
                cd.CheckDate = filters.Year.ToString();
                cd.CheckAddress = "-";
                cd.CheckOrgan = "-";
                cd.PurposeCheck = "-";
                cd.CheckOrganOther = "-";
                if (items[i].Contains("ID"))
                {
                    //<a href="detail.php?ID=299600">
                    string item = items[i].Substring(items[i].IndexOf("ID="), items[i].IndexOf("</a>"));
                    //item = items[i].Substring(0, items[i].IndexOf("\">")).Substring(items[i].IndexOf("ID=")).Replace("ID=", "").Replace("\"", "").Replace(">", "");
                    item = item.Substring(0, item.IndexOf("\">")).Replace("ID=", "").Replace("\"", "").Replace(">", "");
                    cd.CheckId = item != "" ? Convert.ToInt32(item) : 0;
                }
                int i_len = items[i].IndexOf("</p>");
                int i_st = items[i].IndexOf("<p class=\"registry-result__text\">");
                items[i] = items[i].Substring(i_st);
                items[i] = items[i].Replace("*", "").Replace("</p>", "").Replace("<br />", "*").Replace("<p class=\"registry-result__text\">", "").Replace("\n", "").Replace("\t", "");
                var rows = items[i].Split('*');
                for (int j = 0; j < rows.Length; j++)
                {
                    firstIndex = rows[j].IndexOf(":");
                    if (rows[j].Contains("Месяц"))
                    {
                        var cols = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Split(',');
                        var str = new StringBuilder(cols[0].ToString());
                        str[0] = char.ToUpper(str[0]);
                        cd.CheckMonth = GetMonthNumber(str.ToString());
                        cd.CheckDate = str.ToString() + " " + filters.Year.ToString();
                    }

                    if (rows[j].Contains("Дата"))
                    {
                        cd.CheckDate = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Replace("&nbsp;", " ");
                        cd.CheckDate = ClearString(cd.CheckDate);
                        var cols = cd.CheckDate.Split('.');
                        //var month = new StringBuilder(cols[1].ToString());
                        try 
                        { 
                            cd.CheckDay = Int32.Parse(cols[0]);
                            cd.CheckMonth = Int32.Parse(cols[1]);
                        }
                        catch
                        {
                            cd.CheckDay = 0;
                            cd.CheckMonth = 0;
                        }
                    }

                    if (rows[j].Contains("нахожден"))
                    {
                        cd.CheckAddress = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Replace("&nbsp;", " "); 
                        lastIndex = cd.CheckAddress.IndexOf("<br>");
                        cd.CheckAddress = lastIndex > 0 ? cd.CheckAddress.Substring(0, lastIndex) : cd.CheckAddress;
                    }

                    if (rows[j].Contains("Цель"))
                    {
                        cd.PurposeCheck = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Replace(":", "").Replace("&nbsp;", " "); 
                        cd.PurposeCheck = ClearString(cd.PurposeCheck);
                    }

                    if (rows[j].Contains("органа"))
                    {
                        cd.CheckOrgan = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Replace("&nbsp;", " "); 
                        cd.CheckOrgan = ClearString(cd.CheckOrgan);
                        lastIndex = cd.CheckOrgan.IndexOf("<br>");
                        cd.CheckOrgan = lastIndex > 0 ? cd.CheckOrgan.Substring(0, lastIndex) : cd.CheckOrgan;
                    }

                    if (rows[j].Contains("совместно"))
                    {
                        cd.CheckOrganOther = rows[j].Substring(firstIndex).Replace(": ", "").Replace(":", "").Replace("&nbsp;", " "); 
                        lastIndex = cd.CheckOrganOther.IndexOf("<br>");
                        cd.CheckOrganOther = lastIndex > 0 ? cd.CheckOrganOther.Substring(0, lastIndex) : cd.CheckOrganOther;
                    }

                }
                if (cd != null)
                {
                    if (filters.Month == 0 || (filters.Month == cd.CheckMonth))
                    {
                        list_purpose.Add(cd.PurposeCheck);
                        list_organ.Add(cd.CheckOrgan);
                        if (String.IsNullOrEmpty(filters.Purpose) || cd.PurposeCheck.Contains(filters.Purpose))
                        {
                            if (String.IsNullOrEmpty(filters.Organ) || cd.CheckOrgan.Contains(filters.Organ))
                            {
                                list_cd.Add(cd);
                            }
                        }
                    }
                }
            }


            var list_sorted = list_cd.OrderBy(e => e.CheckMonth).OrderBy(e => e.CheckDay);


            if (notes < 0 && list_cd.Count == n)
            {
                warning = "По Вашему запросу найдено слишком большое количество плановых проверок.&lt;br/&gt;Попробуйте уточнить поисковый запрос, сократив период проверки.";
            }
            else
            {
                if (value.Contains("ничего не найдено") || list_cd.Count == 0)
                {
                    warning = "По Вашему запросу ничего не найдено";
                    ret.GenprocCheck = sb.Append("<data cnt=\"0\" warning=\"" + warning + "\"></data>").ToString();
                    return ret;
                }
                else
                {
                    warning = "Найдено " + list_cd.Count + " записей";
                }
            }
            /*sb.Append(warning);

            sb.Append("<div class='thead3' id='thead3'>" +
            "<div class='th3' style='width:100px;'>Месяц даты начала проведения проверки</div>" +
            "<div class='th3' style='width:150px;'>Место нахождения объекта</div>" +
            "<div class='th3' style='width:220px;'>Цель проведения проверки</div>" +
            "<div class='th3' style='width:220px;'>Наименование органа государственного контроля</div>" +
            "<div class='th3' style='width:220px; border-right:solid 1px #e2e2e2;'>Наименование органа государственного контроля (надзора), органа муниципального контроля, с которым проверка проводится совместно</div></div>");
        */
            sb.Append("<year>" + filters.Year.ToString() + "</year>");
            sb.Append("<data cnt=\"" + list_cd.Count + "\" warning=\"" + warning + "\">");
            foreach (var item in list_sorted)
            {
                /*sb.Append("<div class='srclist_tbl'>");
                sb.Append("<div class='tabtr' style='width:96px; text-align:left;'><a style='cursor:pointer;' onclick='ShowCase(" + item.CheckId + ", \"" + item.CheckOrgan.Trim() + "\")'>" + item.CheckDate + "</a></div>");
                sb.Append("<div class='tabtr' style='width:146px; text-align:left;'>" + item.CheckAddress + "</div>");
                sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.PurposeCheck + "</div>");
                sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.CheckOrgan + "</div>");
                sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.CheckOrganOther + "</div>");
                sb.Append("</div>");*/
                sb.Append("<item hid=\"" + item.CheckId + "\" horgan=\"" + item.CheckOrgan.Trim().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;", " ") + "\" dt=\"" + item.CheckDate + "\" address=\"" + item.CheckAddress.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;", " ") + "\" purpose=\"" + item.PurposeCheck.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;", " ") + "\" organ=\"" + item.CheckOrgan.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;", " ") + "\"  organ_other=\"" + item.CheckOrganOther.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;", " ") + "\"/>");

            }
            sb.Append("</data>");


            ret.GenprocCheck = sb.ToString();

            // Выпадающий список Цель проверки
            var plist = list_purpose.Select(e => new { Name = e }).Distinct().OrderBy(e => e.Name);
            if (plist != null)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<purpose>");
                foreach (var item in plist)
                {
                    //str.Append(item.Name + "|");
                    str.Append("<item name=\"" + item.Name + "\"/>");
                }
                str.Append("</purpose>");
                ret.PurposeCheck = str.ToString();
                //ret.PurposeCheck = ret.PurposeCheck.Substring(0, ret.PurposeCheck.LastIndexOf('|'));
            }
            // Выпадающий список Проверяющий орган
            var olist = list_organ.Select(e => new { Name = e }).Distinct().OrderBy(e => e.Name);
            if (olist != null)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<organ>");
                foreach (var item in olist)
                {
                    //str.Append(item.Name + "|");
                    str.Append("<item name=\"" + item.Name + "\"/>");
                }
                str.Append("</organ>");
                ret.OrganCheck = str.ToString();
                // ret.OrganCheck = ret.OrganCheck.Substring(0, ret.OrganCheck.LastIndexOf('|'));
            }
        }
        catch(Exception e)
        {
            string err = e.Message;
        }
    
    return ret;
        }

        /// <summary>
        /// Парсим список проверок (2011-2012)
        /// </summary>
        public static GenprocResult ParserText2012(Filters filters, string value)
        {
            filters.Purpose = ClearString(filters.Purpose);
            filters.Organ = ClearString(filters.Organ);
            GenprocResult ret = new GenprocResult();
            StringBuilder sb = new StringBuilder("");
            string warning = "";

            int firstIndex = value.IndexOf("<div class=\"news-list\">");
            int lastIndex;
            int notes = 0;
            if (value.Contains("<strong class=\"msg\">"))
            {
                lastIndex = value.LastIndexOf("<strong class=\"msg\">");
                if (value.Contains("найдено слишком"))
                {
                    notes = -1;
                }
            }
            else
            {
                lastIndex = value.LastIndexOf("<td class=\"rightbar\">");
            }
            value = value.Substring(0, lastIndex);
            value = value.Substring(firstIndex).Replace("news-item", "*").Replace("</p>", "");
            var items = value.Split('*');
            int n = items.Length - 1;
            //if (notes < 0)
            //{
            //    n = 20;
            //}
            if (n == 0)
            {
                warning = "По Вашему запросу ничего не найдено";
                ret.GenprocCheck = sb.Append("<data cnt=\"0\" warning=\"" + warning + "\"></data>").ToString();
                return ret;
            }

            List<CheckDetails> list_cd = new List<CheckDetails>();
            List<string> list_purpose = new List<string>();
            List<string> list_organ = new List<string>();

            for (int i = 1; i <= n; i++)
            {
                CheckDetails cd = new CheckDetails();
                cd.CheckId = 0;
                cd.CheckMonth = 0;
                cd.CheckYear = filters.Year;
                cd.CheckDate = filters.Year.ToString();
                cd.CheckAddress = "-";
                cd.CheckOrgan = "-";
                cd.PurposeCheck = "-";
                cd.CheckOrganOther = "-";
                if (items[i].Contains("ID"))
                {
                    string item = items[i].Substring(0, items[i].IndexOf("\"><b>")).Substring(items[i].IndexOf("ID=")).Replace("ID=", "").Replace("\"", "").Replace(">", "");
                    cd.CheckId = item != "" ? Convert.ToInt32(item) : 0;
                }

                items[i] = items[i].Replace("*", "").Replace("<small>", "*").Replace("</small>", "").Replace("\n", "").Replace("\t", "").Replace("</div>", "").Replace("<td>", "").Replace("</td>", "");
                var rows = items[i].Split('*');
                for (int j = 0; j < rows.Length; j++)
                {
                    firstIndex = rows[j].IndexOf(":&nbsp;");

                    if (rows[j].Contains("Дата"))
                    {
                        if (filters.Year == 2010)
                        {
                            var cols = rows[j].Substring(rows[j].IndexOf(":")).Replace(":", "");
                            if (cols.Contains("2010"))
                            {
                                cd.CheckDate = cols.Substring(0, cols.IndexOf("2010") + 4).Trim();
                                try
                                {
                                    cd.CheckMonth = Convert.ToDateTime(cd.CheckDate).Month;
                                }
                                catch
                                {
                                    cd.CheckMonth = 0;
                                }
                            }
                            else
                            {
                                cd.CheckDate = cols.Trim();
                                cd.CheckMonth = 0;
                            }
                        }
                        else
                        {
                            var cols = rows[j].Substring(firstIndex).Replace(":&nbsp;", "").Split('г');
                            cd.CheckDate = cols[0].Trim();
                            cd.CheckMonth = Convert.ToDateTime(cd.CheckDate).Month;
                        }
                    }

                    if (rows[j].Contains("(надзора)"))
                    {
                        cd.CheckOrgan = rows[j].Substring(firstIndex).Replace(":&nbsp;", "").Replace("<br />", "").Trim();
                        lastIndex = cd.CheckOrgan.IndexOf("<p");
                        cd.CheckOrgan = lastIndex > 0 ? cd.CheckOrgan.Substring(0, lastIndex).Trim() : cd.CheckOrgan.Trim();
                        cd.CheckOrgan = ClearString(cd.CheckOrgan);
                    }

                    if (filters.Year == 2010 && rows[j].Contains("согласование"))
                    {
                        cd.CheckOrgan = rows[j].Substring(rows[j].IndexOf(":")).Replace(":", "").Replace("<br />", "").Trim();
                        lastIndex = cd.CheckOrgan.IndexOf("<p");
                        cd.CheckOrgan = lastIndex > 0 ? cd.CheckOrgan.Substring(0, lastIndex).Trim() : cd.CheckOrgan.Trim();
                        cd.CheckOrgan = ClearString(cd.CheckOrgan);
                    }

                }
                if (cd != null)
                {
                    if (filters.Month == 0 || (filters.Month == cd.CheckMonth))
                    {
                        list_purpose.Add(cd.PurposeCheck);
                        list_organ.Add(cd.CheckOrgan);
                        if (String.IsNullOrEmpty(filters.Purpose) || cd.PurposeCheck.Contains(filters.Purpose))
                        {
                            if (String.IsNullOrEmpty(filters.Organ) || cd.CheckOrgan.Contains(filters.Organ))
                            {
                                list_cd.Add(cd);
                            }
                        }
                    }
                }
            }

            var list_sorted = list_cd.OrderBy(e => e.CheckMonth);

            if (notes < 0 && list_cd.Count == n)
            {
                warning = "По Вашему запросу найдено слишком большое количество плановых проверок.<br/>Попробуйте уточнить поисковый запрос, сократив период проверки.";
            }
            else
            {
                if (value.Contains("ничего не найдено") || list_cd.Count == 0)
                {
                    warning = "По Вашему запросу ничего не найдено";
                    ret.GenprocCheck = sb.Append("<data cnt=\"0\" warning=\"" + warning + "\"></data>").ToString();
                    return ret;
                }
                else
                {
                    warning = "Найдено " + list_cd.Count + " записей";

                }
            }
            //sb.Append(warning);
            sb.Append("<data cnt=\"" + list_cd.Count + "\" warning=\"" + warning + "\">");

            /*sb.Append("<div class='thead3' id='thead3'>" +
            "<div class='th3' style='width:100px;'>Месяц даты начала проведения проверки</div>" +
            "<div class='th3' style='width:150px;'>Место нахождения объекта</div>" +
            "<div class='th3' style='width:220px;'>Цель проведения проверки</div>" +
            "<div class='th3' style='width:220px;'>Наименование органа государственного контроля</div>" +
            "<div class='th3' style='width:220px; border-right:solid 1px #e2e2e2;'>Наименование органа государственного контроля (надзора), органа муниципального контроля, с которым проверка проводится совместно</div></div>");
            */
            foreach (var item in list_sorted)
            {
                /*  sb.Append("<div class='srclist_tbl'>");
                  sb.Append("<div class='tabtr' style='width:96px; text-align:left;'><a style='cursor:pointer;' onclick='ShowCase(" + item.CheckId + ", \"" + item.CheckOrgan.Trim() + "\")'>" + item.CheckDate + "</a></div>");
                  sb.Append("<div class='tabtr' style='width:146px; text-align:left;'>" + item.CheckAddress + "</div>");
                  sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.PurposeCheck + "</div>");
                  sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.CheckOrgan + "</div>");
                  sb.Append("<div class='tabtr' style='width:216px; text-align:left;'>" + item.CheckOrganOther + "</div>");
                  sb.Append("</div>");
               
                 */
                sb.Append("<item hid=\"" + item.CheckId + "\" dt=\"" + item.CheckDate + "\" address=\"" + item.CheckAddress + "\" purpose=\"" + item.PurposeCheck + "\" organ=\"" + item.CheckOrgan + "\"  organ_other=\"" + item.CheckOrganOther + "\"/>");

            }
            sb.Append("</data>");
            ret.GenprocCheck = sb.ToString();

            // Выпадающий список Цель проверки
            var plist = list_purpose.Select(e => new { Name = e }).Distinct().OrderBy(e => e.Name);
            if (plist != null)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<purpose>");
                foreach (var item in plist)
                {
                    //str.Append(item.Name + "|");
                    str.Append("<item name=\"" + item.Name + "\"/>");
                }
                str.Append("</purpose>");
                ret.PurposeCheck = str.ToString();

            }
            // Выпадающий список Проверяющий орган
            var olist = list_organ.Select(e => new { Name = e }).Distinct().OrderBy(e => e.Name);
            if (olist != null)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<organ>");
                foreach (var item in olist)
                {
                    //str.Append(item.Name + "|");
                    str.Append("<item name=\"" + item.Name + "\"/>");
                }
                str.Append("</organ>");
                ret.OrganCheck = str.ToString();

            }
            return ret;
        }

        protected static string ClearString(string source)
        {
            if (source == null)
            {
                return null;
            }
            return source.Replace("&quot;", "").Replace("\"", "").Replace("&lt;", "").Replace("&gt;", "").Replace("&amp;lt;", "").Replace("&amp;gt;", "").Replace("<", "").Replace(">", "");
        }

        protected static int GetMonthNumber(string month)
        {
            int cmonth = 0;
            switch (month)
            {
                case "Январь":
                    cmonth = 1;
                    break;
                case "Февраль":
                    cmonth = 2;
                    break;
                case "Март":
                    cmonth = 3;
                    break;
                case "Апрель":
                    cmonth = 4;
                    break;
                case "Май":
                    cmonth = 5;
                    break;
                case "Июнь":
                    cmonth = 6;
                    break;
                case "Июль":
                    cmonth = 7;
                    break;
                case "Август":
                    cmonth = 8;
                    break;
                case "Сентябрь":
                    cmonth = 9;
                    break;
                case "Октябрь":
                    cmonth = 10;
                    break;
                case "Ноябрь":
                    cmonth = 11;
                    break;
                case "Декабрь":
                    cmonth = 12;
                    break;
                default:
                    cmonth = 0;
                    break;
            }

            return cmonth;
        }

        #endregion
    }
}