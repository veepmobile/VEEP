using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public class Utilites
    {
        public static String GetIP(HttpContextBase context)
        {
            String ip =
                context.Request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }

        public static void SendEmail(string message, string subject)
        {
            string fromAddress = "service@skrin.ru";
            string toAddress = "applicationdelivery@skrin.ru";
            SmtpClient smpt = new SmtpClient("mail.skrin.ru");
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = subject;
                msg.Body = message;
                msg.CC.Add("kitaev-error@skrin.ru");
                smpt.Send(msg);
            }
            catch (Exception)
            {

            }
        }


        public static async Task<string> GetFileContent(string filepath,Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(filepath, Encoding.GetEncoding("Windows-1251")))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static string GetFileSize(string file_path)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = new FileInfo(file_path).Length;
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##}&nbsp;{1}", len, sizes[order]);
        }

        public static string TrimOKATO(string value)
        {
            string res = value.TrimEnd('0');
            if (res != null && res.Length < 2 && value.Length > 1)
            {
                res += "0";
            }
            return res;
        }

    }
}