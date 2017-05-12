using MySql.Data.MySqlClient;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Search
{
    class SphynxSearcherDetailsUA
    {
        private string connectionstring = string.Format("Server={0};Port=9367;Character Set=utf8", Configs.SphinxServer);
        private string _sql;

        public SphynxSearcherDetailsUA(string sql, string port, string charaster_set, string server)
        {
            _sql = sql.Replace("/", @"\\/");
            connectionstring = string.Format("Server={2};Port={0};Character Set={1}", port, charaster_set, server);
        }

        public List<UADetails> SearchDetails()
        {
            List<UADetails> ret = new List<UADetails>();
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, con);
                cmd.CommandTimeout = 600;
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UADetails details = new UADetails();
                    int i_cnt = reader.FieldCount;
                    for (int i = 0; i < i_cnt; i++)
                    {
                        string field_name = reader.GetName(i);
                        if (field_name == "name")
                        {
                            details.name = reader.ReadNullIfDbNullReplaced(i).ToString().Replace("\\", "");
                        }


                        if (field_name == "edrpou")
                        {
                            details.edrpou = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "region")
                        {
                            details.region = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "kved")
                        {
                            details.industry = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                       
                        if (field_name == "regno")
                        {
                            details.regno = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "regdate")
                        {
                            //int ts = Convert.ToInt32(reader.GetName(i));
                            //DateTime reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts);
                            //details.reg_date = reg_date.ToShortDateString();
                            details.regdate = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "regorg")
                        {
                            details.regorg = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "address")
                        {
                            details.addr = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "ruler")
                        {
                            details.ruler = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "phone")
                        {
                            details.phone = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "fax")
                        {
                            details.fax = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "email")
                        {
                            details.email = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "www")
                        {
                            details.www = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                       
                    }
                    ret.Add(details);
                }
            }

            return ret;
        }
    }
}
