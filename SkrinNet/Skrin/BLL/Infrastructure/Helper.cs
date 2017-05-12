using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Globalization;
using System.IO;

namespace Skrin.BLL.Infrastructure
{
    public static class Helper
    {
        public static void SendEmail(string message, string subject)
        {
            string fromAddress = "newskrin@skrin.ru";
            string toAddress = "applicationdelivery@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = subject;
                msg.Body = message;
                foreach (string copy in Configs.EmailCopies)
                {
                    msg.CC.Add(copy);
                }                
                smpt.Send(msg);
            }
            catch (Exception)
            {
            }
        }

        public static void SendEmail(string message, string subject,List<string> copies)
        {
            string fromAddress = "newskrin@skrin.ru";
            string toAddress = "applicationdelivery@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = subject;
                msg.Body = message;
                foreach (string copy in copies)
                {
                    msg.CC.Add(copy);
                }
                smpt.Send(msg);
            }
            catch (Exception)
            {
            }
        }

        public static void WriteLog(string message)
        {
            if ((Configs.LogPath !=null) && (Configs.LogPath != ""))
            {
                string filepath = Configs.LogPath + (Configs.LogPath[Configs.LogPath.Length-2].Equals('\\') ? @"\" : "")+ @"\skrinnet" + DateTime.Today.ToString("yyyymmdd") + ".log";
                try
                {

                    if (!Directory.Exists(Path.GetDirectoryName(filepath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

                    //определение текущей даты
                    DateTime dn = DateTime.Now;
                    message = "---------------------------------------------------------------\r\n" +dn.ToString() + "\r\n" + message;
                    StreamWriter sw = new StreamWriter(filepath, true);
                    sw.WriteLine(message);
                    sw.Flush();
                    sw.Close();
                }
                catch { }
            }
        }


        /// <summary>
        /// Защита от SQL-инъекций
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CheckInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            string[] black_list = new string[] { "--", "/*", "*/", "@@", "alter ", "begin ", "cast ", "create ", "cursor ", "declare ", "delete ", "drop ", "exec ", "execute ", "fetch ", "insert ", "open ", "table ", "update ", "<scrip", "</scrip" };

            foreach (string s in black_list)
            {
                if (input.ToLower().IndexOf(s) != -1)
                {
                    return null;
                }
            }

            return input.Replace("'", "''");
        }

        public static string FullTextString(string pn)
        {
            String value;
            if (String.IsNullOrWhiteSpace(pn))
                value = "";
            else
                value = pn.ToUpper();
            value = value.Replace(" - ", " ").Replace("№", "№ ").Replace("(", "").Replace(")", "").Trim();
            string pattern = "МУНИЦИПАЛЬНОЕ ДОШКОЛЬНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ |" +
    "ПРОИЗВОДСТВЕННЫЙ СЕЛЬСКОХОЗЯЙСТВЕННЫЙ КООПЕРАТИВ |" +
    "СЕЛЬСКОХОЗЯЙСТВЕННЫЙ ПРОИЗВОДСТВЕННЫЙ КООПЕРАТИВ |" +
    "СЕЛЬСКОХОЗЯЙСТВЕННЫЙ ПОТРЕБИТЕЛЬСКИЙ КООПЕРАТИВ |" +
    "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ |" +
    "ОБЩЕСТВО С ОГРАНИЧЕНОЙ ОТВЕТСТВЕННОСТЬЮ |" +
    "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ\"|" +
    "ИНДИВИДУАЛЬНЫЙ ЧАСТНЫЙ ПРЕДПРИНИМАТЕЛЬ |" +
    "ГОСУДАРСТВЕННОЕ УНИТАРНОЕ ПРЕДПРИЯТИЕ |" +
    "АКЦИОНЕРНОЕ ОБЩЕСТВО ЗАКРЫТОГО ТИПА |" +
    "КРЕСТЬЯНСКОЕ (ФЕРМЕРСКОЕ) ХОЗЯЙСТВО |" +
    "НАПРАВЛЕНИЙ РАЗВИТИЯ ВОСПИТАННИКОВ |" +
    "ДЕТСКИЙ САД ОБЩЕРАЗВИВАЮЩЕГО ВИДА |" +
    "ПЕРВИЧНАЯ ПРОФСОЮЗНАЯ ОРГАНИЗАЦИЯ |" +
    "КООПЕРАТИВ |^ФИРМА |ИНДИВИДУАЛЬНОЕ ЧАСТНОЕ ПРЕДПРИЯТИЕ |ТОВАРИЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ |ТОВАРИЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЮ ФИРМА |ТОВАРИЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕСТВЕННОСТЬЮ |" +
    "РЕЛИГИОЗНАЯ ИСЛАМСКАЯ ОРГАНИЗАЦИЯ |" +
    "РЕМОНТНО-СТРОИТЕЛЬНЫЙ КООПЕРАТИВ |" +
    "ТЕРРИТОРИАЛЬНАЯ АДМИНИСТРАЦИЯ |" +
    "МУНИЦИПАЛЬНОЕ УЧРЕЖДЕНИЕ ДОПОЛНИТЕЛЬНОГО ОБРАЗОВАНИЯ |МУДО |КРЕСТЬЯНСКОЕ (ФЕРМЕРСКОЕ) ХОЗЯЙСТВО |" +
    "ПУБЛИЧНОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО |" +
    "ЗАКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО |" +
    "ОТКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО |" +
    "СЕМЕЙНОЕ ЧАСТНОЕ ПРЕДПРИЯТИЕ |" +
    "ПРОИЗВОДСТВЕННЫЙ КООПЕРАТИВ |" +
    "ПРИОРИТЕТНЫМ ОСУЩЕСТВЛЕНИЕМ |" +
    "НЕКОММЕРЧЕСКОЕ ПАРТНЕРСТВО |" +
    "СТРУКТУРНОЕ ПОДРАЗДЕЛЕНИЕ |" +
    "САДОВОДЧЕСКОЕ ОБЪЕДИНЕНИЕ |" +
    "ОБЩЕСТВЕННАЯ ОРГАНИЗАЦИЯ |" +
    "ПРОФСОЮЗНАЯ ОРГАНИЗАЦИЯ |" +
    "УНИТАРНОЕ ПРЕДПРИЯТИЕ |" +
    "АКЦИОНЕРНОЕ ОБЩЕСТВО |" +
    "ПРОСТОЕ ТОВАРИЩЕСТВО |" +
    "ЧАСТНОЕ ПРЕДПРИЯТИЕ |" +
    "ПРОФСОЮЗНЫЙ КОМИТЕТ |" +
    "ГОСУДАРСТВЕННОЕ УП |" +
    "ЗАКРЫТОГО ТИПА |" +
    "МУНИЦИПАЛЬНОЕ |" +
    "ОТКРЫТОЕ АО |" +
    "ПРЕДПРИЯТИЕ |" +
    "АКЦИОНЕРНОЕ |" +
    "УНИТАРНОЕ |" +
    "ОБЩЕСТВО |" +
    "ОТКРЫТОЕ |" +
    "ЗАКРЫТОЕ |" +
    "ЗАКРЫТОЕ |" +
    "ДОЧЕРНЕЕ |" +
    "ПРОФСОЮЗ |" +
    "^ФИЛИАЛ |" +
    "^ПАО |" +
    "^АО |" +
    "^АООТ |" +
    "^АОЗТ |" +
    "^СОАО |" +
    "^CОАО |" +
    "^ЗАО |" +
    "^ООО |" +
    "^ОАО |" +
    "^ТОО |" +
    "^ФГУП |" +
    "^ГУП |" +
    "^ИЧП |" +
    "^МУЧ |" +
    "^Д/С |" +
    "^ППО |" +
    "^МОО |" +
    "^УП ";

            Regex re = new Regex(pattern);
            value = re.Replace(value, "");

            re = new Regex("[\\\",\\.]");
            value = re.Replace(value, " ");

            re = new Regex("[\\s]{2,}");
            value = re.Replace(value, " ").Trim();

            return value;
        }

        public static void SaveCache(string key, object val)
        {
            HttpContext.Current.Cache.Insert(key, val, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 5, 0));
        }

        public static decimal? GetDecimalFromText(string value)
        {
            if (value == null)
            {
                return (decimal?)null;
            }
            else
            {
                return Decimal.Parse(value.Replace(",", "."), NumberStyles.Any);
            }
        }

        public static string ClearAllSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static int? GetIntFromText(string value)
        {
            if (value == null)
            {
                return (int?)null;
            }
            else
            {
                return Int32.Parse(value, NumberStyles.Any);
            }
        }


    }   

}