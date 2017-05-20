using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Skrin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
               name: "SearchIchp",
               url: "dbsearch/dbsearchru/ichp/{action}/{sub_path}",
               defaults: new { controller = "SearchIchp", action = "Index", sub_path = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "SearchFL",
               url: "dbsearch/dbsearchru/fl/{action}/{sub_path}",
               defaults: new { controller = "SearchFL", action = "Index", sub_path = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Multilinks",
               url: "dbsearch/dbsearchru/multilinks/{action}/{sub_path}",
               defaults: new { controller = "Multilinks", action = "Index", sub_path = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "DbSearchRu",
               url: "dbsearch/dbsearchru/{action}/{sub_path}",
               defaults: new { controller = "DBSearchRu", action = "Companies", sub_path = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "DbSearchKZ",
               url: "dbsearch/dbsearchKZ/{action}/{sub_path}",
               defaults: new { controller = "DBSearchKZ", action = "Companies", sub_path = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "DbSearchUa",
               url: "dbsearch/dbsearchua/{action}/{sub_path}",
               defaults: new { controller = "DBSearchUa", action = "Companies", sub_path = UrlParameter.Optional }
               );
               
            routes.MapRoute(
                    name: "tprice",
                    url: "dbsearch/tprice/{action}",
                    defaults: new { controller = "TPrice", action = "Index" }
                );

            routes.MapRoute(
               name: "qivsearch",
               url: "dbsearch/qivsearch/{action}",
               defaults: new { controller = "QivSearch", action = "Index" }
               );

            routes.MapRoute(
                name: "iss",
                url: "issuers/{ticker}/reports",
                defaults: new { controller = "Redirect", action = "Issuers" }
                );

            routes.MapRoute(
                name: "iss_report",
                url: "issuers/{ticker}",
                defaults: new { controller = "Issuers", action = "Index" }
                );

            routes.MapRoute(
                    name: "profile",
                    url: "profile/{ticker}",
                    defaults: new { controller = "Redirect", action = "Issuers" }
                );

            routes.MapRoute(
                    name: "shop_report",
                    url: "{ticker}/reports",
                    defaults: new { controller = "Redirect", action = "Issuers" }
                );



            routes.MapRoute(
                name: "iss_UA",
                url: "UA/{edrpou}",
                defaults: new { controller = "IssuersUA", action = "Index" }
                );

            routes.MapRoute(
                name: "UA_Documents",
                url: "UA/Documents/{doc_id}",
                defaults: new { controller = "IssuersUA", action = "Documents" }
                );


            routes.MapRoute(
                    name: "usergroups",
                    url: "userlists/group",
                    defaults: new { controller = "UserLists", action = "Index" }
                );

            routes.MapRoute(
                    name: "monitoring",
                    url: "userlists/monitoring",
                    defaults: new { controller = "MonitorOperations", action = "Index" }
                );

            routes.MapRoute(
               name: "ip",
               url: "profileip/{iss}",
               defaults: new { controller = "ProfileIp", action = "Index" }
               );

            routes.MapRoute(
               name: "ip_redirect",
               url: "profile_ip/{iss}",
               defaults: new { controller = "Redirect", action = "ProfileIp" }
               );

            routes.MapRoute(
                name: "kz",
                url: "profilekz/{iss}",
                defaults: new { controller = "ProfileKZ", action = "Index" }
                );

            routes.MapRoute(
                name: "modules",
                url: "issuers/modules/{action}",
                defaults: new { controller = "Issuers" }
                );

            routes.MapRoute(
                name: "modulesip",
                url: "profileip/modules/{action}",
                defaults: new { controller = "profileip" }
              );
            routes.MapRoute(
                name: "moduleskz",
                url: "profilekz/modules/{action}",
                defaults: new { controller = "profilekz" }
              );
            routes.MapRoute(
                name: "modulesua",
                url: "ua/modules/{action}",
                defaults: new { controller = "IssuersUA" }
                );

            routes.MapRoute(
               name: "Messages",
               url: "dbsearch/{controller}/{action}",
               defaults: new { controller = "EventSearch", action = "Index" }
               );

            routes.MapRoute(
                name: "Rts-Tender",
                url: "rts-tender",
                defaults: new { controller = "ExternalUsers", action = "RtsTender" }
                );

            routes.MapRoute(
                name: "pw",
                url: "pw",
                defaults: new { controller = "Company", action = "Access" }
                );
            routes.MapRoute(
                name: "api",
                url: "api",
                defaults: new { controller = "Company", action = "Api" }
                );
            routes.MapRoute(
               name: "monitor",
               url: "monitor",
               defaults: new { controller = "Company", action = "monitor" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
