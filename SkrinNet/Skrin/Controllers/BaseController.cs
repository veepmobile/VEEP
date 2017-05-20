using Skrin.BLL.Infrastructure;
using Skrin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class BaseController : Controller
    {

        protected static string[] open_companies = new string[] { "zaauf", "kzos", "kzms", "pmkop", "sanve", "sacis", "sztt", "tatn", "engpr", "cppsk", "avaz", "aflt", "1076820000971", "1066829018178", "1114222000078", "ctsro" };
        protected static string[] open_companies_ogrn = new string[] { "1061420001994", "1021603267674", "1025901844132", "1064704037310", "1026101758484", "1021400791763", "1026602314320", "1021601623702", "1082468041116", 
            "1057749440781", "1026301983113", "1027700092661", "1076820000971", "1066829018178", "1114222000078", "1027700277967" };

        protected JsonResult ErrorResponse(string error_message)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(error_message, MediaTypeNames.Text.Plain);
        }
    }
}