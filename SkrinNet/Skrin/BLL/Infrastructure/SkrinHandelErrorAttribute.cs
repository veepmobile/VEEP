using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Authorization;
using System.Text;
using System.IO;

namespace Skrin.BLL.Infrastructure
{
    public class SkrinHandelErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            List<string> form_values = new List<string>();
            foreach (string key in filterContext.RequestContext.HttpContext.Request.Form.AllKeys)
            {
                form_values.Add(string.Format("{0}={1}", key, filterContext.RequestContext.HttpContext.Request.Form[key]));
            }
 
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("Запрос: {0}", filterContext.RequestContext.HttpContext.Request.RawUrl));

            sb.AppendLine(string.Format("Метод: {0}", filterContext.RequestContext.HttpContext.Request.HttpMethod));

            if(form_values.Count>0){
                sb.AppendLine(string.Format("Тело запроса: {0}",string.Join("&",form_values)));
            }

            if (form_values.Count == 0 && filterContext.RequestContext.HttpContext.Request.HttpMethod.ToLower() == "post")
            {
                if (filterContext.RequestContext.HttpContext.Request.InputStream.CanSeek)
                {
                    filterContext.RequestContext.HttpContext.Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                }

                using (var reader = new StreamReader(filterContext.RequestContext.HttpContext.Request.InputStream))
                {
                    sb.AppendLine(string.Format("Тело запроса: {0}",reader.ReadToEnd()));
                }
            }

            

            UserSession us = filterContext.RequestContext.HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                sb.AppendLine("Пользователь не авторизован");
            }
            else
            {
                sb.AppendLine(string.Format("\nuserId={0}, login={1}", us.UserId, us.User.Login));
            }


            sb.AppendLine("Заголовки запроса:");

           
            var headers=filterContext.RequestContext.HttpContext.Request.Headers;

            foreach (var key in headers.AllKeys)
            {
                var vals=headers.GetValues(key);
                for (int i = 0; i < vals.Length; i++)
                {

                    if (i == 0)
                    {
                        sb.AppendLine(string.Format("{0}:{1}",key,vals[i]));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("{0}:{1}", new string(' ', key.Length), vals[i]));
                    }
                }
            }

            Helper.SendEmail(filterContext.Exception.ToString() + "\n" + sb.ToString() , "Ошибка в SkrinNet");
            
            Helper.WriteLog(filterContext.Exception.ToString() + "\r\n" + sb.ToString());
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}