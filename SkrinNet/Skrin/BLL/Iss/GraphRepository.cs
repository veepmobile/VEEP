using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Iss
{
    public class GraphRepository
    {
        public static async Task<GraphData> GetGraphDataAsync(string ticker)
        {
            GraphData base_data = await _GetDataFromBase(ticker);
            base_data.al = _NormilizeData(base_data.al, 30, 70,100);
            base_data.ali = _NormilizeData(base_data.ali, 30, 70,100);
            base_data.os = _NormilizeData(base_data.os, 10, 50,70);
            base_data.osi = _NormilizeData(base_data.osi, 10, 50,70);
            return base_data;
        }


        private static async Task<GraphData> _GetDataFromBase(string ticker)
        {
             using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
             {
                 SqlCommand cmd = new SqlCommand("skrin_net..graf_coeff_ind", con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = ticker;
                 con.Open();
                 GraphData gd = new GraphData();
                 SqlDataReader reader = await cmd.ExecuteReaderAsync();
                 while (await reader.ReadAsync())
                 {
                     gd.year.Add((int)reader["y"]);
                     gd.al.Add((decimal?)reader.ReadNullIfDbNull("al"));
                     gd.ali.Add((decimal?)reader.ReadNullIfDbNull("ali"));
                     gd.os.Add((decimal?)reader.ReadNullIfDbNull("os"));
                     gd.osi.Add((decimal?)reader.ReadNullIfDbNull("osi"));                     
                 }
                 return gd;
             }             
        }

        static List<decimal?> _NormilizeData(List<decimal?> input_data, decimal bad_val, decimal good_val, decimal max_val)
        {
            bool need_max_normilize = input_data.Max() > max_val;
            bool need_min_normilize = input_data.Min() < 0;
            if (need_max_normilize)
            {

                decimal decrement_factor = (max_val - good_val) / ((decimal)input_data.Max() - good_val);
                for (int i = 0; i < input_data.Count; i++)
                {
                    if (input_data[i] > good_val)
                    {
                        decimal tmp = (decimal)input_data[i];
                        input_data[i] = good_val + (tmp - good_val) * decrement_factor;
                    }
                }
            }
            if (need_min_normilize)
            {
                decimal decrement_factor = bad_val / (bad_val - (decimal)input_data.Min());
                for (int i = 0; i < input_data.Count; i++)
                {
                    if (input_data[i] < bad_val)
                    {
                        decimal tmp = (decimal)input_data[i];
                        input_data[i] = bad_val - (bad_val - tmp) * decrement_factor;
                    }
                }
            }

            for (int i = 0; i < input_data.Count; i++)
            {
                if (input_data[i] != null)
                {
                    input_data[i] = Decimal.Round(input_data[i].Value);
                }
            }

            return input_data;
        }
    }
}