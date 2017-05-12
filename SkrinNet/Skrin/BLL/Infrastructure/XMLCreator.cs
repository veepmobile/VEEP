using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Skrin.BLL.Infrastructure
{
    public class XMLCreator
    {
        private string _conString;
        private SqlCommand _cmd;
        private string _root_element;


        public XMLCreator(SqlCommand cmd, string root_element)
        {
            _cmd = cmd;
            _conString = Configs.ConnectionString;
            _root_element = root_element;
        }

        public XmlDocument Generate()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"windows-1251\" ?>" + _root_element);
            Regex ReWs = new Regex("\\s");
            string xml_text = "";
            string xml_temp = "";
            XmlElement root = doc.DocumentElement;
            XmlDocumentFragment xf = doc.CreateDocumentFragment();
            using (SqlConnection con = new SqlConnection(_conString))
            {
                _cmd.Connection = con;
                con.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            xml_temp = (reader[0].ToString());
                            xml_text += xml_temp;
                        }
                    }
                    while (reader.NextResult());
                }
                xf.InnerXml = ReWs.Replace(xml_text, " ");
                root.AppendChild(xf);
            }
            return doc;
        }
    }
}