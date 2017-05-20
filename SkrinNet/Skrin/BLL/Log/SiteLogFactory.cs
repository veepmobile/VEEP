using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Skrin.BLL.Log
{
    public static class SiteLogFactory
    {
        public static SiteLog Create(UserSession us, HttpContextBase context)
        {
            HttpMethod method=context.Request.HttpMethod.ToLower() == "post" ? HttpMethod.POST : HttpMethod.GET;
            string post_params = null;            
            if (method == HttpMethod.POST)
            {
                List<string> form_values = new List<string>();
                foreach (string key in context.Request.Form.AllKeys)
                {
                    form_values.Add(string.Format("{0}={1}", key, context.Request.Form[key]));
                }
                if (form_values.Count == 0)
                {

                    var bytes = new byte[context.Request.InputStream.Length];
                    context.Request.InputStream.Read(bytes, 0, bytes.Length);
                    context.Request.InputStream.Position = 0;
                    post_params = Encoding.UTF8.GetString(bytes);

                }
                else
                {
                    post_params = string.Join("&", form_values);
                }
            }
            else
            {
                post_params = context.Request.QueryString.ToString();
            }
            return new SiteLog
            {
                UserId = us.UserId,
                IP = Utilites.GetIP(context),
                HttpMethod = method,
                InsertDate = DateTime.Now,
                PostParams = post_params,
                SiteId = 1,
                Url = context.Request.Path
            };
        }

    }
}