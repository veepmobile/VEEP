using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Iss
{
    public class BadAddressRepository
    {
        /// <summary>
        /// Получаем результат поиска при его наличии (с учетом времени устаревания 1 сутки)
        /// </summary>
        public static async Task<string> GetAddressAsync(string ogrn, string inn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.bad_address_get", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = ogrn;
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
        public static async Task SearchAddressAsync(string ogrn, string inn)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.bad_address_set", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = (inn != null && inn != "null") ? inn : null;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            }
        }
    }
}