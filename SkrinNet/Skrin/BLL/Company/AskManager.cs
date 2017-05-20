using Skrin.BLL.Infrastructure;
using Skrin.Models.Company;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Skrin.BLL.Company
{
    public class AskManager
    {
        private static string _constring = Configs.ConnectionString;

        private static Dictionary<int, string> _themses = new Dictionary<int, string>{
                {1,"База данных (подписка, поставка данных)"},
                {2,"ПОД/ФТ"},
                {3,"Раскрытие информации о Вашей компании в соответствии с требованиями Банка России"},
                {4,"Размещение рекламы"},
                {5,"Иное"}
            };

        public static Dictionary<int, string> GetThemes()
        {
            return _themses;
        }


        public static void SendEmail(Ask ask)
        {
            //string fromAddress = "newskrin@skrin.ru";
            string fromAddress = (string.IsNullOrEmpty(ask.info.email)) ? "sales@skrin.ru" : ask.info.email;
            string toAddress = "sales@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = "Вопросы незарегистрированных пользователей";
                msg.Body = GetHtmlText(ask);
                msg.IsBodyHtml = true;
                smpt.Send(msg);
            }
            catch (Exception)
            {

            }
        }

        public static void SendEmail(AskInfo info, AskUserType u_type)
        {
            string fromAddress = (string.IsNullOrEmpty(info.email)) ? "sales@skrin.ru" : info.email;
            string toAddress = "sales@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = u_type==AskUserType.Authenticated ? "Вопросы зарегистрированных пользователей" : "Вопросы незарегистрированных пользователей";
                msg.Body = GetHtmlText(info,u_type);
                msg.IsBodyHtml = true;
                smpt.Send(msg);
            }
            catch (Exception)
            {

            }
        }



        
        public static void SendEmail(FeedbackCall ask)
        {
            //string fromAddress = "newskrin@skrin.ru";
            string fromAddress = (string.IsNullOrEmpty(ask.info.email)) ? "sales@skrin.ru" : ask.info.email;
            string toAddress = "sales@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = "Вопросы незарегистрированных пользователей";
                msg.Body = GetHtmlText(ask);
                msg.IsBodyHtml = true;
                smpt.Send(msg);
            }
            catch (Exception)
            {

            }
        }

        public static void Subscribe(string email)
        {
            if(!string.IsNullOrWhiteSpace(email))
            {
                if(SubscribeEmail(email))
                {
                    var host = HttpContext.Current.Request.Url.Host;
                    string body = string.Format(@"<h1>Подтверждение Вашей подписки на новости</h1>
                                                 Для подтверждения Вашей подписки, нажмите <a href='http://{0}/company/confirm?em={1}'>сюда</a><br/>
                                                Если ссылка не работает скопируйте этот текст <b>http://{0}/company/confirm?em={1}</b><br> и вставте в строку адреса Вашего браузера.", host, email);
                    string fromAddress = "sales@skrin.ru";
                    string toAddress = email;
                    SmtpClient smpt = new SmtpClient(Configs.Smtp);
                    try
                    {
                        MailMessage msg = new MailMessage(fromAddress, toAddress);
                        msg.Subject = "Подтверждение Вашей подписки на новости";
                        msg.Body = body;
                        msg.IsBodyHtml = true;
                        smpt.Send(msg);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private static string GetHtmlText(Ask ask)
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Вопросы незарегистрированных пользователей</h1>");
            sb.Append("<h2>Интерес</h2><ul>");
            foreach (var item in ask.themes)
            {
                sb.AppendFormat("<li>{0}</li>", _themses[item]);
            }
            sb.Append("</ul><h2>Данные пользователя</h2>");
            if (ask.info.login.Length > 0)
            {
                sb.AppendFormat("<p><b>Лоштн:</b> {0}</p>", ask.info.login);
            }
            sb.AppendFormat("<p><b>Компания:</b> {0}</p>", ask.info.company);
            sb.AppendFormat("<p><b>Ф.И.О.:</b> {0}</p>", ask.info.fio);
            sb.AppendFormat("<p><b>Телефон:</b> {0}</p>", ask.info.phone);
            sb.AppendFormat("<p><b>E-mail:</b> {0}</p>", ask.info.email);
            sb.AppendFormat("<p><b>Комментарии/Вопрос:</b> {0}</p>", ask.info.comment);
            return sb.ToString();
        }

        private static string GetHtmlText(AskInfo info, AskUserType u_type)
        {
            var sb = new StringBuilder();

            if (u_type == AskUserType.NonAuthenticated)
            {
                sb.Append("<h1>Вопросы незарегистрированных пользователей</h1>");
            }
            else
            {
                sb.Append("<h1>Вопросы зарегистрированных пользователей</h1>");
            }

            sb.Append("<h2>Данные пользователя</h2>");
            if (u_type == AskUserType.Authenticated)
            {
                sb.AppendFormat("<p><b>Логин:</b> {0}</p>", info.login);
            }
            sb.AppendFormat("<p><b>Компания:</b> {0}</p>", info.company);
            sb.AppendFormat("<p><b>Ф.И.О.:</b> {0}</p>", info.fio);
            sb.AppendFormat("<p><b>Телефон:</b> {0}</p>", info.phone);
            sb.AppendFormat("<p><b>E-mail:</b> {0}</p>", info.email);
            return sb.ToString();
        }

        private static string GetHtmlText(FeedbackCall ask)
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Вопросы незарегистрированных пользователей</h1>");
            sb.Append("<h2>Интерес</h2><ul>");
            sb.AppendFormat("<li>{0}</li>", ask.theme);
            sb.Append("</ul><h2>Данные пользователя</h2>");
            sb.AppendFormat("<p><b>Компания:</b> {0}</p>", ask.info.company);
            sb.AppendFormat("<p><b>Ф.И.О.:</b> {0}</p>", ask.info.fio);
            sb.AppendFormat("<p><b>Телефон:</b> {0}</p>", ask.info.phone);
            sb.AppendFormat("<p><b>E-mail:</b> {0}</p>", ask.info.email);
            sb.AppendFormat("<p><b>Комментарии/Вопрос:</b> {0}</p>", ask.info.comment);
            return sb.ToString();
        }


        private static bool SubscribeEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                var cmd = new SqlCommand("skrin_net..subscribe_email", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                cmd.Parameters.Add("@new_user", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@new_user"].Value == 1;
            }
        }

        public static void SendEmailBlock(AskInfo info)
        {
            
            string fromAddress = (string.IsNullOrEmpty(info.email)) ? "sales@skrin.ru" : info.email;
            string toAddress = "sales@skrin.ru";
            SmtpClient smpt = new SmtpClient(Configs.Smtp);
            try
            {
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = "Блокировка IP-адреса";
                msg.Body = GetHtmlTextBlock(info);
                msg.IsBodyHtml = true;
                smpt.Send(msg);
            }
            catch (Exception)
            {

            }
        }

        private static string GetHtmlTextBlock(AskInfo ask)
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Блокировка IP-адреса</h1>");
            sb.Append("</ul><h2>Данные пользователя</h2>");
            sb.AppendFormat("<p><b>Логин:</b> {0}</p>", ask.login);
            sb.AppendFormat("<p><b>Компания:</b> {0}</p>", ask.company);
            sb.AppendFormat("<p><b>Ф.И.О.:</b> {0}</p>", ask.fio);
            sb.AppendFormat("<p><b>Телефон:</b> {0}</p>", ask.phone);
            sb.AppendFormat("<p><b>E-mail:</b> {0}</p>", ask.email);
            sb.AppendFormat("<p><b>Комментарии/Вопрос:</b> {0}</p>", ask.comment);
            return sb.ToString();
        }
        public static void ConfirmEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                var cmd = new SqlCommand("update Subscribe.dbo.Subscribers set Status=3 where email=@email", con);
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public enum AskUserType
    {
        Authenticated,
        NonAuthenticated
    }
}