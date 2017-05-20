using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace Skrin.BLL.Report
{
    public class XSLGenerator
    {

        private string _proc;
        private Dictionary<string, object> _params;
        private string _xsl_path;
        private Dictionary<string, object> _xml_params;


        public XSLGenerator(string proc, Dictionary<string, object> param_collection, string xsl_path, Dictionary<string, object> xml_params=null)
        {
            _proc = proc;
            _params = param_collection;
            _xsl_path = xsl_path;
            _xml_params = xml_params;
        }

        public async Task<string> GetResultAsync()
        {
            string xml = await _GetXMLText();
            HTMLCreator creator = new HTMLCreator(_xsl_path);
            return creator.GetHtml(string.Format("<?xml version=\"1.0\" encoding=\"WINDOWS-1251\"?><iss_profile {1}><profile  xmlns:sql='urn:schemas-microsoft-com:xml-sql'>{0}</profile></iss_profile>", xml, _GetExtraAttrs(_xml_params))).Replace("href_", "href"); //обходим глюк с кирилицей в ссылках
        }

        public string GetChangedResult(string xml)
        {
            HTMLCreator creator = new HTMLCreator(_xsl_path);
            return creator.GetHtml(string.Format("<?xml version=\"1.0\" encoding=\"WINDOWS-1251\"?><iss_profile {1}><profile  xmlns:sql='urn:schemas-microsoft-com:xml-sql'>{0}</profile></iss_profile>", xml, _GetExtraAttrs(_xml_params)));
        }

        public async Task<string> GetXmlAsync()
        {
            return await _GetXMLText();
        }

        private string _GetExtraAttrs(Dictionary<string,object> extra_params)
        {
            if (extra_params == null || extra_params.Count == 0)
            {
                return "";
            }

            List<string> ret = new List<string>();

            foreach (var par in extra_params)
            {
                ret.Add(string.Format("{0}='{1}'", par.Key, par.Value));
            }

            return string.Join(" ", ret);
        }

        private async Task<string> _GetXMLText()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(_proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                foreach (var _par in _params)
                {
                    cmd.Parameters.AddWithValue(_par.Key, _par.Value);
                }
                con.Open();
                StringBuilder sb = new StringBuilder();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    do
                    {
                        while(await reader.ReadAsync())
                        {                
                            sb.Append(reader[0] != DBNull.Value ? (string)reader[0] : "");
                        }
                    } while (await reader.NextResultAsync());
                }
                return sb.ToString();
            }
        }

    }
}