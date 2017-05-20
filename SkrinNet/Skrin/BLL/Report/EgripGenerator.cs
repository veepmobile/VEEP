using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace Skrin.BLL.Report
{
    public class EgripGenerator
    {
        private string _ogrnip;

        public EgripGenerator(string ogrnip)
        {
            _ogrnip = ogrnip;
        }

        public string Generate()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.OmitXmlDeclaration = true;

                using (XmlWriter xw = XmlWriter.Create(sw, writerSettings))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    string xsl_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/xsl/temple_egrip.xsl");
                    xslt.Load(xsl_path);
                    using (XmlReader xr = XmlReader.Create(new StringReader(GetEgripXml())))
                    {
                        xslt.Transform(xr, xw);
                    }
                }
                return sw.ToString();
            }
        }

        private string GetEgripXml()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "fsns2..FL_doc_output_string_ogrnip";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrnip", SqlDbType.VarChar, 15).Value = _ogrnip;
                SqlParameter rep = cmd.Parameters.Add("@rep", SqlDbType.VarChar, int.MaxValue);
                rep.Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                string ret = rep.Value.ToString();
                return ret;
            }
        }
    }
}