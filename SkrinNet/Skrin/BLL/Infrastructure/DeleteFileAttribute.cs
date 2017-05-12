using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Skrin.BLL.Infrastructure
{
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();
            var filePathResult = filterContext.Result as FilePathResult;
            if (filePathResult != null && File.Exists(filePathResult.FileName))
            {
                File.Delete(filePathResult.FileName);
            }
        }
    }
}