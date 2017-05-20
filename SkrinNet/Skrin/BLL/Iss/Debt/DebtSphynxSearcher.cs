using System;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Skrin.BLL.DebtBLL
{
    public class DebtSphynxSearcher
    {
        private string connectionstring = "Server=skrinsql1;Port=9340;Character Set=cp1251";
        //private string connectionstring = "Server=194.247.149.35;Port=9340;Character Set=cp1251";

        private string _sql;

        public int totalFound;

        public DebtSphynxSearcher(string sql, string port, string charaster_set)
        {
            _sql = sql.Replace("\"","").Replace("/",@"\\/").Replace("-"," ").Replace("!","");
            connectionstring = string.Format("Server=skrinsql1;Port={0};Character Set={1}", port, charaster_set);
            //connectionstring = string.Format("Server=194.247.149.35;Port={0};Character Set={1}", port, charaster_set);
            totalFound = 0;
        }
        

        public List<string> SearchJson()
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
                        row += string.Format("\"{0}\":\"{1}\",", reader.GetName(i), reader.GetValue(i));
                    }
                    row = row.Substring(0, row.Length - 1);
                    row += "}";
                    ret.Add(row);
                }

                string result =" { ";
                List<string> ext_data = new List<string>();
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        ext_data.Add("\"" + reader[0] + "\":\"" + reader[1] + "\"");
                       // if(reader[0].ToString() == "total_found")
                      //  { 
                      //      totalFound=Convert.ToInt32(reader[1]);
                      //  }
                    }
                }
                if (ext_data.Count > 0)
                {
                    result += string.Join(",", ext_data);
                }
                result += "}";
                ret.Add(result);

                return ret;
            }
        }

    }
}