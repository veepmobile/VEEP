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
    public class EgrulGenerator
    {
        private string _ogrn;

        public EgrulGenerator(string ogrn)
        {
            _ogrn = ogrn;
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
                    string xsl_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/xsl/temple_egrul.xsl");
                    xslt.Load(xsl_path);
                    using (XmlReader xr = XmlReader.Create(new StringReader(GetEgrulXml())))
                    {
                        xslt.Transform(xr, xw);
                    }
                }
                return sw.ToString();
            }
        }

        private string GetEgrulXml()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.doc_output_string", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = _ogrn;
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