using MySql.Data.MySqlClient;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Search
{
    public class SphynxSearcherIchpDetails
    {
        private string connectionstring = string.Format("Server={0};Port={1};Character Set=utf8", Configs.SphinxSearchIPServer, Configs.SphinxSearchIPPort);
        private string _sql;

        public SphynxSearcherIchpDetails(string sql, string port, string charaster_set, string server)
        {
            _sql = sql.Replace("/", @"\\/");
            connectionstring = string.Format("Server={2};Port={0};Character Set={1}", port, charaster_set, server);
        }

        public List<FLDetails> SearchDetails()
        {
            List<FLDetails> ret = new List<FLDetails>();
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, con);
                cmd.CommandTimeout = 600;
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FLDetails details = new FLDetails();
                    int i_cnt = reader.FieldCount;
                    for (int i = 0; i < i_cnt; i++)
                    {
                        string field_name = reader.GetName(i);
                        if (field_name == "fio")
                        {
                            details.fio = reader.ReadNullIfDbNullReplaced(i).ToString().Replace("\\", "");
                        }

                        if (field_name == "inn")
                        {
                            details.inn = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "region")
                        {
                            details.region = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "okpo")
                        {
                            details.okpo = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "ogrnip")
                        {
                            details.ogrnip = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "typeip")
                        {
                            if(reader["typeip"].ToString() == "1") { details.typeip = "Индивидуальный предприниматель"; }
                            if(reader["typeip"].ToString() == "2") { details.typeip = "Глава крестьянского фермерского хозяйства"; }
                        }
                        if (field_name == "stoping")
                        {
                            DateTime dt = DateTime.Now;
                            dt = dt.AddDays(-1);
                            details.stoping = (reader["stoping"].ToString() != "0") ? "Сведения о прекращении деятельности (ЕГРИП " + dt.ToRusString() + ")" : "";
                        }
                    }
                    ret.Add(details);
                }
            }

            return ret;
        }
    }
}