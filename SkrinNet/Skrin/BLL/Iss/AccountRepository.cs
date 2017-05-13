using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Skrin.BLL.Iss
{
    public class AccountRepository
    {
        public static async Task<string> GetResultAsync(string inn)
        {
            string SearchResult = null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.[CheckAccounts_get]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = inn;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                {
                    if (await reader.ReadAsync())
                    {
                        SearchResult = (String)reader.ReadNullIfDbNull(0);
                    }
                }
            }
            return SearchResult;
        }
    }
}