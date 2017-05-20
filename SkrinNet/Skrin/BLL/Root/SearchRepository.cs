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
    public class SearchRepository
    {

        public static async Task<string> GetRegsAsync(string ids, int? type=0)
        {
            if(!string.IsNullOrWhiteSpace(ids))
            {
                string sql="";
                if(type==1)
                {
                    sql = @"SELECT STUFF((SELECT '|'+okato FROM  ( 
                     Select a.okato from naufor..okato a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b  
                     on (a.parentid=b.kod and exists(select 1 from naufor..okato c where c.id=b.kod)) or (b.kod=a.id and parentid!=0) 
                     ) o  ORDER BY okato FOR XML PATH('')),1,1,'')";
                }
                else
                {
                    sql = @"SELECT STUFF((SELECT '|'+right('00' + cast(kod as varchar(16)),2) FROM  ( 
                         Select a.kod from searchdb2..regions a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b 
                         on (a.parent_id=b.kod and exists(select 1 from searchdb2..regions c where c.id=b.kod)) or (b.kod=a.id and parent_id!=0) 
                         ) o ORDER BY kod FOR XML PATH('')),1,1,'')";
                }
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@ids", SqlDbType.VarChar).Value = ids;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                    if(await reader.ReadAsync())
                    {
                        return reader.ReadEmptyIfDbNull(0);
                    }
                }
            }
            return "";
        }

        public static async Task<string> GetRegsForSqlSearchAsync(string ids, int type)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                string sql = "";
                if (type == 1)
                {
                    sql = @"SELECT STUFF((SELECT ','+okato FROM  ( 
                     Select a.okato from naufor..okato a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b  
                     on (a.parentid=b.kod and exists(select 1 from naufor..okato c where c.id=b.kod)) or (b.kod=a.id and parentid!=0) 
                     ) o  ORDER BY okato FOR XML PATH('')),1,1,'')";
                }
                else
                {
                    sql = @"SELECT STUFF((SELECT ','+right('00' + cast(kod as varchar(16)),2) FROM  ( 
                         Select a.kod from searchdb2..regions a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b 
                         on (a.parent_id=b.kod and exists(select 1 from searchdb2..regions c where c.id=b.kod)) or (b.kod=a.id and parent_id!=0) 
                         ) o ORDER BY kod FOR XML PATH('')),1,1,'')";
                }
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
            return "";
        }

        public static async Task<string> GetIndAsync(string ids,int type,int is_not)
        {
            string sign = is_not == 0 ? "" : "!";
            if(!string.IsNullOrWhiteSpace(ids))
            {
                string sql = "";
                SqlCommand cmd = new SqlCommand();
                if(type==0)
                {
                    sql = string.Format(@"SELECT STUFF((SELECT '|{0}'+replace(kod,'.','D') FROM  ( 
                         Select a.kod from searchdb2..okveds a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b 
                         on (a.parentid=b.kod and exists(select 1 from searchdb2..okveds c where c.id=b.kod and c.parentid=0)) or (b.kod=a.id and parentid!=0) 
                         ) o ORDER BY o.kod FOR XML PATH('')),1,1,'')", sign);
                    cmd.Parameters.Add("@ids", SqlDbType.VarChar).Value = ids;
                }
                else
                {
                    sql = string.Format("SELECT STUFF((SELECT '|{0}'+kod FROM  searchdb2..okonh where id in ( {1} ) ORDER BY kod FOR XML PATH('')),1,1,'')", sign, string.Join(",", ids.ParamVals().Keys)); //okonh
                    foreach (var p in ids.ParamVals())
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                }
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
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
                    string sql = string.Format(@";WITH C ([ID], parent_id) AS  (SELECT B.ID, B.parent_id FROM searchdb2..okopf AS B WHERE B.[ID] in ({0})  
                                UNION ALL  SELECT D.[ID], D.parent_id FROM searchdb2..okopf AS D  INNER JOIN C ON c.ID = d.parent_ID) 
                                Select STUFF((Select '|' + cast(c.id as varchar(5)) from C where parent_id>0 FOR XML PATH('')),1,1,'')", string.Join(",", ids.ParamVals().Keys));
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

        public static async Task<string> GetOkfsAsync(string ids, bool for_sphynx)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sign = for_sphynx ? "|" : ",";

                    string sql = string.Format(@";WITH C ([ID], parent_id) AS  (SELECT B.ID, B.parent_id FROM searchdb2..okfs AS B WHERE B.[ID] in ({0})  
                    UNION ALL  SELECT D.[ID], D.parent_id FROM searchdb2..okfs AS D  INNER JOIN C ON c.ID = d.parent_ID) 
                    Select STUFF((Select '{1}' + cast(c.id as varchar(5)) from C where parent_id>0 FOR XML PATH('')),1,1,'')", string.Join(",", ids.ParamVals().Keys),sign);
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
    }
}