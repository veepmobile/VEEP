using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Authorization
{
    public class CheckRequestAttribute:FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserSession us = filterContext.HttpContext.GetUserSession();
            us.CheckRequestCount();
        }
    }
}