using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public class CookieHelper
    {
        public static void AddCookie(HttpResponseBase response, string cookie_name, string cookie_value, DateTime? expires = null)
        {
            //Если cookie уже добавлена, то ничего не делаем
            if (response.Cookies.AllKeys.Contains(cookie_name))
                return;

            HttpCookie cookie = new HttpCookie(cookie_name, cookie_value);
            if (expires != null)
            {
                cookie.Expires = expires.Value;
            }
            response.Cookies.Add(cookie);
        }

        public static string GetCookieVal(HttpRequestBase request, string cookie_name)
        {
            if (!request.Cookies.AllKeys.Contains(cookie_name))
                return null;

            return request.Cookies[cookie_name].Value;
        }

        public static void RemoveCookie(HttpResponseBase response, string cookie_name)
        {
            HttpCookie cookie;
            if (response.Cookies.AllKeys.Contains(cookie_name))
            {
                cookie = response.Cookies[cookie_name];
            }
            else
            {
                cookie = new HttpCookie(cookie_name);
                response.Cookies.Add(cookie);
            }
            cookie.Expires = DateTime.Now.AddDays(-1);
        }
    }
}