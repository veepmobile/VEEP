using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using HttpUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestService.BLL;
using RestService.Models;
using RestService.CommService;
using Calabonga.Xml.Exports;

namespace RestService.Controllers
{
    public class AppController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}