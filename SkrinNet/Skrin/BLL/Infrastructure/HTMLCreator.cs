using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace Skrin.BLL.Infrastructure
{
    public class HTMLCreator
    {
        private string _xsl_filename;
        private SqlCommand _cmd;
        private string _root_elem;

        public HTMLCreator(string xsl_filename)
        {
            _xsl_filename = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Content/xsl/{0}.xsl",xsl_filename));           
        }


        public string GetHtml(string xml)
        {
            using (StringWriter sw = new StringWriter())
            {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(true, true);

                    xslt.Load(_xsl_filename, settings, new XmlUrlResolver());
                    using (XmlReader xr = XmlReader.Create(new StringReader(xml)))
                    {
                        xslt.Transform(xr,null, sw);
                    }
                    //string ret = sw.ToString();
                return sw.ToString();
            }
        }

        public string GetHtml(SqlCommand cmd,string root_elem = "<profile />")
        {
            XMLCreator xc = new XMLCreator(cmd, root_elem);
            string xml_text = xc.Generate().OuterXml;
            return GetHtml(xml_text);
        }
    }
}