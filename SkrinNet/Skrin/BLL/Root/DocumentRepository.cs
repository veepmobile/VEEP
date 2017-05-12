using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Root
{
    public class DocumentRepository
    {

        private static readonly string _constring = Configs.ConnectionString;

        public static async Task<string> GetAuthorIdAsync(string doc_id, int doc_type)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql=null;
                switch(doc_type)
                {
                    case 1:
                        sql = "select author_id from naufor..reviews where doc_id=@doc_id";
                        break;
                    case 2:
                        sql = "select author_id from naufor..Analitics where doc_id=@doc_id";
                        break;
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 32).Value = doc_id;
                con.Open();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        return (string)reader[0];
                    }
                }
                return null;
            }
        }

        public static async Task<string> GetIssuerIdAsync(string ticker)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "Select issuer_id from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (string)reader[0];
                    }
                }
                return null;
            }
        }

        public static async Task<string> GetFileNameAsync(string doc_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "select top 1  file_name from naufor..doc_pages_new where doc_id=@doc_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 32).Value = doc_id;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (string)reader[0];
                    }
                }
                return null;
            }
        }

        public static async Task<string> GetFileNameAsync(string doc_id, int page_no)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = "select  file_name from naufor..doc_pages_new where doc_id=@doc_id and no=@no";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 32).Value = doc_id;
                cmd.Parameters.Add("@no", SqlDbType.SmallInt).Value = page_no;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (string)reader[0];
                    }
                }
                return null;
            }
        }

        public static async Task<string> GetExportNameAsync(int type_id, string iss, string doc_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("naufor..genExportName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@type_id", SqlDbType.Int).Value = type_id;
                cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = iss;
                cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 32).Value = doc_id;
                con.Open();
                return (string)await cmd.ExecuteScalarAsync();
            }
        }
    }
}