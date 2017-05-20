using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Skrin.BLL.Infrastructure;
using System.Threading.Tasks;

namespace Skrin.BLL.Iss
{
    public class NalogDebtRepository
    {

        /// <summary>
        /// Получаем результат поиска при его наличии (с учетом времени устаревания 1 сутки)
        /// </summary>
        public static async Task<string> GetNalogDebtAsync(string inn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.nalog_debt_get", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = inn;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    if (reader[0] != DBNull.Value)
                        return (string)reader[0];
                }
                return null;
            }
        }

        /// <summary>
        /// При отсутствии данных пишем в базу и ставим в очередь
        /// </summary>
        public static async Task SearchNalogDebtAsync(string inn)
        {
            if(inn != null && inn != "null")
            { 
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("fsns2.dbo.nalog_debt_set", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = inn;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}