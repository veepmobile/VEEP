using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Iss
{
    public class DocRegRepository
    {
        /// <summary>
        /// Получаем результат поиска при его наличии (с учетом времени устаревания 1 сутки)
        /// </summary>
        public static async Task<string> GetDocRegAsync(string code)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.get_docreg2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return (string)reader.ReadNullIfDbNull(0);
                }
                return null;
            }
        }

        
    }
}