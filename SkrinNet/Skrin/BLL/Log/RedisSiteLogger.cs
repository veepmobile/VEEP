using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Log
{
    public class RedisSiteLogger:ISiteLogger
    {
        public void Log(SiteLog siteLog)
        {
            RedisStore.Push("SITE_LOG", siteLog);
        }
    }
}