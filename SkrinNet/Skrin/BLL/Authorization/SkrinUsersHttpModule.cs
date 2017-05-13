using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Authorization;
using System.Diagnostics;
using System.Web.Routing;
using System.Web.Mvc;
using Skrin.Controllers;
using Skrin.BLL.Log;

namespace Skrin.BLL.Authorization
{
    public class SkrinUsersHttpModule:IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {         
            UserSession us = new UserSession(new System.Web.HttpContextWrapper(HttpContext.Current));
            HttpContext.Current.Items.Add("skrin_user_session", us);
          
            string ask = HttpContext.Current.Request.Url.LocalPath;
            
            if (ask != "/Authentication/Tramp")
            {
                ISiteLogger logger = new RedisSiteLogger();
                logger.Log(SiteLogFactory.Create(us, new System.Web.HttpContextWrapper(HttpContext.Current)));
            }
            
            if (us.AuthenticationResult == AuthenticationType.IsBlockedIp && ask != "/Company/Block")
            {
                HttpContext.Current.RewritePath("/Error/BlockedIp");
                //HttpContext.Current.Response.StatusCode = 403;
            }
        }

    }
}