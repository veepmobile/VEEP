using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Skrin.BLL.Root
{
    public class SearchUaRepository
    {

        public static async Task<string> GetRegsAsync(string ids, int type)
        {
            if(!string.IsNullOrWhiteSpace(ids))
            {
                string sql="";
                if(type==0)
                {
                    sql = "select STUFF((SELECT ','+Koatuu from (Select Koatuu from  UA3..Dic_Koatuu where Id in (" + ids + ")) o  ORDER BY Koatuu  FOR XML PATH('')),1,1,'')";
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@ids", SqlDbType.VarChar).Value = ids;
                        con.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                        if (await reader.ReadAsync())
                        {
                            return reader.ReadEmptyIfDbNull(0);
                        }
                    }
                }
                else
                {
                    return ids;
                }
            }
            return "";
        }

        public static async Task<string> GetIndAsync(string ids,int type,int is_not)
        {
            //string sign = is_not == 0 ? "" : "!";
            if(!string.IsNullOrWhiteSpace(ids))
            {
                //string sql = "UA3..getKvedsForSearch '" + ids + "'";
                string sql = "SELECT STUFF((SELECT ','+Code FROM (select Code from UA3..Dic_Kved" +
                                                                 " where ActiveCode in (select Code from UA3..Dic_Kved" +
                                                                                      " where ParentId in(select id from (select Id, ParentId from UA3..Dic_Kved where ParentId in (3,4)) d join (Select kod from searchdb2..kodesplitter(0, @kvedlist)) b on d.ParentId=b.kod) " +
                                                                                              " or id in ( Select a.Id from UA3..Dic_Kved a inner join (Select kod from searchdb2..kodesplitter(0, @kvedlist)) b on (a.parentId=b.kod and exists(select 1 from UA3..Dic_Kved c where c.Id=b.kod and c.parentId in (0,3,4)) and a.parentId not in (3,4)) or (b.kod=a.Id and parentId not in (0,3,4))))" +
                                            ") o ORDER BY o.Code FOR XML PATH('')),1,1,'')";
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                using (SqlCommand cmd = con.CreateCommand())  
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@kvedlist", ids);
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader.ReadEmptyIfDbNull(0);
                        }
                    }
                }
            }
            return "";
        }

        public static async Task<string> GetOkopfAsync(string ids)
        {
            if(!string.IsNullOrWhiteSpace(ids))
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = ";WITH C (Id, parentId) AS  (SELECT B.Id, B.parentId FROM UA3..Dic_OPF AS B WHERE B.Id in (" + ids + ")  " +
                                    "UNION ALL  SELECT D.Id, D.parentId FROM UA3..Dic_OPF AS D  INNER JOIN C ON c.Id = d.parentId) " +
                                 "Select STUFF((Select ',' + cast(c.Id as varchar(5)) from C FOR XML PATH('')),1,1,'')";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    foreach (var p in ids.ParamVals())
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader.ReadEmptyIfDbNull(0);
                        }
                    }
                }
            }
            return "";
        }
/*
        public static async Task<string> GetOkfsAsync(string ids)
        {
            return ids; //.Replace(",", "|");
        }
 */
    }
}