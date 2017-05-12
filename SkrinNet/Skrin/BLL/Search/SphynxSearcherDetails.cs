using MySql.Data.MySqlClient;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Search
{
    public class SphynxSearcherDetails
    {
        private string connectionstring = string.Format("Server={0};Port=9367;Character Set=utf8", Configs.SphinxServer);
        private string _sql;

        public SphynxSearcherDetails(string sql, string port, string charaster_set,string server)
        {
            _sql = sql.Replace("/", @"\\/");
            connectionstring = string.Format("Server={2};Port={0};Character Set={1}", port, charaster_set, server);
        }

        public List<ULDetails> SearchDetails()
        {
            List<ULDetails> ret = new List<ULDetails>();
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, con);
                cmd.CommandTimeout = 600;
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ULDetails details = new ULDetails();
                    int i_cnt = reader.FieldCount;
                    for (int i = 0; i < i_cnt; i++)
                    {
                        string field_name = reader.GetName(i);
                        string company_name = "";
                        if (field_name == "name")
                        {
                            details.name = reader.ReadNullIfDbNullReplaced(i).ToString().Replace("\\", "");
                        }

                        if (field_name == "nm")
                        {
                            company_name = reader.ReadNullIfDbNullReplaced(i).ToString().Replace("\\", "");
                            if (company_name != "")
                            {
                                details.name = company_name;
                            }
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

                        if (field_name == "okved")
                        {
                            details.okved = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "okved_code")
                        {
                            details.okved_code = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "ogrn")
                        {
                            details.ogrn = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "reg_date")
                        {
                            //int ts = Convert.ToInt32(reader.GetName(i));
                            //DateTime reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts);
                            //details.reg_date = reg_date.ToShortDateString();
                            details.reg_date = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "reg_org_name")
                        {
                            details.reg_org_name = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "legal_address")
                        {
                            details.legal_address = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "ruler")
                        {
                            details.ruler = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "legal_phone")
                        {
                            details.legal_phone = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "legal_fax")
                        {
                            details.legal_fax = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "legal_email")
                        {
                            details.legal_email = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }
                        if (field_name == "www")
                        {
                            details.www = reader.ReadNullIfDbNullReplaced(i).ToString();
                        }

                        if (field_name == "del")
                        {
                            details.del = (reader["del"].ToString() != "0") ? "Удалено из реестра Росстата " + reader.ReadNullIfDbNullReplaced(i).ToString() : "";
                        }
                    }
                    ret.Add(details);
                }
            }

            return ret;
        }
    }
}