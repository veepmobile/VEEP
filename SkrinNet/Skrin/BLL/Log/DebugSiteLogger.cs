using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Log
{
    public class DebugSiteLogger:ISiteLogger
    {
        public void Log(SiteLog siteLog)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(siteLog));
        }
    }
}