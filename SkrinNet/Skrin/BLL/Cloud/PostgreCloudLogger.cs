using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Cloud;
using System.Diagnostics;
using Newtonsoft.Json;
using Skrin.BLL.Infrastructure;
using NpgsqlTypes;
using Npgsql;
using System.Threading.Tasks;

namespace Skrin.BLL.Cloud
{
    public class PostgreCloudLogger:ICloudLogger
    {
        static string constring = Configs.PostgreConnectionString;

        public async Task LogAsync(CloudActions action, UserData u_data, object log_data)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"INSERT INTO public.""LogData"" (""LogDataId"" ,""ActionType"", ""UserId"", ""IssuerId"", ""UpdateDate"", ""LogData"") 
                                VALUES 
                               (@LogDataId,@ActionType, @UserId, @IssuerId, @UpdateDate, @LogData);";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@LogDataId", NpgsqlDbType.Uuid).Value = u_data.id;
                    cmd.Parameters.Add("@ActionType", NpgsqlDbType.Smallint).Value = (short)action;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = u_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = u_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("@LogData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(log_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
             
        }
    }
}