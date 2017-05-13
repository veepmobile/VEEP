using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;
using System.Data.SqlClient;
using System.Data;

namespace Skrin.BLL.Root
{
    public class MenuCreator
    {

        private string _xml_text;

        public MenuCreator(string path)
        {
            //Путь должен заканчиваться /
            path = path.EndsWith("/") ? path : path + "/";
            SqlCommand cmd = new SqlCommand("skrin_net..getDBSearch_Menu");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@href", SqlDbType.VarChar, 100).Value = path;
            XMLCreator xc = new XMLCreator(cmd, "<profile />");
            _xml_text = xc.Generate().OuterXml;
        }

        public IHtmlString GetRootBottomMenu()
        {
            HTMLCreator hc=new HTMLCreator("menu_bottom");
            return new HtmlString(hc.GetHtml(_xml_text));
        }

        public IHtmlString GetRootTopMenu()
        {
            HTMLCreator hc = new HTMLCreator("menu_top");
            return new HtmlString(hc.GetHtml(_xml_text));
        }
    }
}