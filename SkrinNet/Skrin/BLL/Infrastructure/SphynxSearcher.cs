using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public class SphynxSearcher
    {
        private string connectionstring;
        private string _sql;

        public SphynxSearcher(string sql, string port, string charaster_set)
        {
            _sql = sql.Replace("/", @"\\/");
            connectionstring = string.Format("Server={2};Port={0};Character Set={1}", port, charaster_set, Configs.SphinxServer);
        }

        public SphynxSearcher(string sql, string port, string charaster_set, string server)
        {
            _sql = sql.Replace("/", @"\\/");
            connectionstring = string.Format("Server={2};Port={0};Character Set={1}", port, charaster_set, server);
        }

        public string SearchJson()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionstring))
                {
                    MySqlCommand cmd = new MySqlCommand(_sql, con);
                    cmd.CommandTimeout = 600;
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<string> ret = new List<string>();
                    while (reader.Read())
                    {
                        string row = "{";
                        int i_cnt = reader.FieldCount;
                        for (int i = 0; i < i_cnt; i++)
                        {
                            string field_name = reader.GetName(i);
                            if (field_name.EndsWith("_json"))
                            {
                                row += string.Format("\"{0}\":{1},", field_name, reader.ReadNullJSONIfDbNull(i));
                            }
                            else
                            {
                                row += string.Format("\"{0}\":\"{1}\",", field_name, reader.ReadNullIfDbNullReplaced(i));
                            }

                        }
                        row = row.Substring(0, row.Length - 1);
                        row += "}";
                        ret.Add(row);
                    }
                    string result = string.Format("\"results\":[{0}]", string.Join(",", ret));
                    List<string> ext_data = new List<string>();
                    while (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            ext_data.Add("\"" + reader[0] + "\":\"" + reader[1] + "\"");
                        }
                    }
                    if (ext_data.Count > 0)
                    {
                        result += "," + string.Join(",", ext_data);
                    }

                    //Helper.SendEmail(string.Format("Тест поиска Sphinx: \n. Поисковый запрос: {0}\n ConnectionString:{1} \n", _sql, connectionstring), "Тест поиска Sphinx");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Helper.SendEmail(string.Format("Ошибка поиска Sphinx: {0} \n. Поисковый запрос: {1}\n ConnectionString:{2} \n", ex, _sql, connectionstring), "Ошибка поиска Sphinx");
                throw;
            }
        }

        public string SearchXML()
        {
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, con);
                cmd.CommandTimeout = 600;
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> ret = new List<string>();
                while (reader.Read())
                {
                    string row = "<row>";
                    int i_cnt = reader.FieldCount;
                    for (int i = 0; i < i_cnt; i++)
                    {
                        row += string.Format("<{0}>{1}</{0}>", reader.GetName(i), reader.ReadNullIfDbNullReplaced(i));
                    }
                    row += "</row>";
                    ret.Add(row);
                }
                string result = string.Format("<cur_count>{0}</cur_count><results>{1}</results>", ret.Count, string.Join("", ret));
                List<string> ext_data = new List<string>();
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        ext_data.Add(string.Format("<{0}>{1}</{0}>", reader[0], reader[1]));
                    }
                }
                if (ext_data.Count > 0)
                {
                    result += string.Format("<meta_data>{0}</meta_data>", string.Join("", ext_data));
                }
                return result;
            }
        }

        public string SearchHtml()
        {
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                string result = "";
                MySqlCommand cmd = new MySqlCommand(_sql, con);
                cmd.CommandTimeout = 600;
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader.ReadNullIfDbNull(0).ToString();
                    }
                }
                catch { }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
    }
}