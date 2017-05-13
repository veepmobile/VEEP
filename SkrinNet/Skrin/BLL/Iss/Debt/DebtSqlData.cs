using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Skrin.Models.Iss;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Debt;
using System.Threading.Tasks;

namespace Skrin.BLL.DebtBLL
{
    public class DebtSqlData
    {

        public static List<DebtItem> GetDebtorDetails(string[] ids, string[] vers)
        {
            List<DebtItem> list = new List<DebtItem>();
            for (int i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                var ver = vers[i];
                DebtItem debtor = new DebtItem();

                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = "";
                    if (ver == "0")
                    {
                        sql = "select a.*, b.name,(select top 1 insert_date from OpenData..FSSP_FilesTable where IsExport=1 and type=0 order by insert_date desc) as uptodate from OpenData..FSSP a LEFT OUTER JOIN naufor..Regions b on try_cast(a.region_id as int)=b.id where /*a.delete_date is null and*/ a.Id=@id";
                    }
                    if (ver == "1")
                    {
                        sql = "select a.*, b.name,(select top 1 insert_date from OpenData..FSSP_FilesTable where IsExport=1 and type=1 order by insert_date desc) as uptodate from OpenData..FSSP_Archive a LEFT OUTER JOIN naufor..Regions b on try_cast(a.region_id as int)=b.id where /*a.delete_date is null and*/ a.Id = @id";
                    }
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        debtor.Id = (id != "") ? Convert.ToInt32(id) : 0;
                        debtor.Status = (ver != "") ? Convert.ToInt16(ver) : Convert.ToInt16(-1);
                        debtor.DebtorName = (string)reader["debtor"];
                        debtor.DebtorAddress = (string)reader["adress"];
                        if (reader["nProizv"] != DBNull.Value)
                        {
                            debtor.NumProizv = (string)reader["nProizv"];
                        }
                        else
                        {
                            debtor.NumProizv = "-";
                        }
                        DateTime dt = new DateTime();
                        if (reader["date1"] != DBNull.Value)
                        {
                            dt = (DateTime)reader["date1"];
                            debtor.DateProizv = dt.ToShortDateString();
                        }
                        else
                        {
                            debtor.DateProizv = "-";
                        }
                        if (reader["nSvodPr"] != DBNull.Value)
                        {
                            debtor.NumSvodPr = (string)reader["nSvodPr"];
                        }
                        else
                        {
                            debtor.NumSvodPr = "-";
                        }
                        if (reader["type"] != DBNull.Value)
                        {
                            debtor.DocumentType = (string)reader["type"];
                        }
                        else
                        {
                            debtor.DocumentType = "-";
                        }
                        if (reader["nDoc"] != DBNull.Value)
                        {
                            debtor.DocumentNum = (string)reader["nDoc"];
                        }
                        else
                        {
                            debtor.DocumentNum = "-";
                        }
                        if (reader["date2"] != DBNull.Value)
                        {
                            dt = (DateTime)reader["date2"];
                            debtor.DocumentDate = dt.ToShortDateString();
                        }
                        else
                        {
                            debtor.DocumentDate = "-";
                        }
                        if (reader["req"] != DBNull.Value)
                        {
                            debtor.DocumentReq = (string)reader["req"];
                        }
                        else
                        {
                            debtor.DocumentReq = "-";
                        }
                        if (reader["predmet"] != DBNull.Value)
                        {
                            debtor.Predmet = (string)reader["predmet"];
                        }
                        else
                        {
                            debtor.Predmet = "-";
                        }
                        if (reader["dep"] != DBNull.Value)
                        {
                            debtor.PristavName = (string)reader["dep"];
                        }
                        else
                        {
                            debtor.PristavName = "-";
                        }
                        if (reader["name"] != DBNull.Value)
                        {
                            debtor.Region = (string)reader["name"];
                        }
                        else
                        {
                            debtor.Region = "-";
                        }
                        if (reader["adressPr"] != DBNull.Value)
                        {
                            debtor.PristavAddress = (string)reader["adressPr"];
                        }
                        else
                        {
                            debtor.PristavAddress = "-";
                        }
                        if (reader["uptodate"] != DBNull.Value)
                        {
                            debtor.UpdateDate = "<div align='right'>Дата обновления " + ((DateTime)reader["uptodate"]).ToShortDateString() + "</div>";
                        }
                        else
                        {
                            debtor.UpdateDate = "";
                        }
                        if (ver == "0")
                        {
                            if (reader["summ"] != DBNull.Value)
                            {
                                debtor.Sum = (decimal)reader["summ"];
                            }
                            else
                            {
                                debtor.Sum = 0;
                            }
                            debtor.CloseCause = "";
                            debtor.CloseDate = "";
                        }
                        if (ver == "1")
                        {
                            if (reader["cause"] != DBNull.Value)
                            {
                                debtor.CloseCause = (string)reader["cause"];
                            }
                            else
                            {
                                debtor.CloseCause = "-";
                            }
                            if (reader["date3"] != DBNull.Value)
                            {
                                dt = (DateTime)reader["date3"];
                                debtor.CloseDate = dt.ToShortDateString();
                            }
                            else
                            {
                                debtor.CloseDate = "-";
                            }
                            debtor.Sum = 0;
                        }
                        list.Add(debtor);
                    }
                }
            }
            return list;
        }
    }
}