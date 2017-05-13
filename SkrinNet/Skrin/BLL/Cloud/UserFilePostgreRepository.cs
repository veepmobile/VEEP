using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Cloud;
using Skrin.BLL.Infrastructure;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Threading.Tasks;

namespace Skrin.BLL.Cloud
{
    public class UserFilePostgreRepository:IUserFileRepository
    {

        private ICloudLogger _logger;

        public UserFilePostgreRepository(ICloudLogger logger)
        {
            _logger = logger;
        }

        static string constring = Configs.PostgreConnectionString;

        public async Task<IEnumerable<UserFile>> GetAsync(int user_id, string issuer_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT ""Id"", ""UpdateDate"", ""UserData""
                            FROM public.""UserData"" 
                            WHERE ""IsDeleted""=false and   ""DataType""=2 and ""UserId""=@UserId and ""IssuerId""=@IssuerId";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = issuer_id;
                    List<UserFile> files = new List<UserFile>();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            files.Add(new UserFile
                            {
                                user_data = new UserData
                                {
                                    id = (Guid)reader["Id"],
                                    update_date = (DateTime)reader["UpdateDate"],
                                    user_id = user_id,
                                    issuer_id = issuer_id
                                },
                                file_data = JsonConvert.DeserializeObject<UserFileData>((string)reader.ReadNullIfDbNull("UserData"))
                            });
                        };
                    }
                    return files;
                }
            }
        }

        public async Task<UserFile> UpdateAsync(UserFile file)
        {
            await _InsertUserFile(file);
            await _logger.LogAsync(CloudActions.AddFile, file.user_data, file.file_data);
            return file;
        }

        public async Task<UserFile> DeleteAsync(Guid file_id)
        {
            var deleted_file = await GetAsync(file_id);
            if (deleted_file != null)
            {
                await _DeleteFile(file_id);
                await _logger.LogAsync(CloudActions.DeleteFile, deleted_file.user_data, deleted_file.file_data);
            }
            return deleted_file;
        }

        private async Task _InsertUserFile(UserFile file)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"INSERT INTO public.""UserData"" (""Id"", ""DataType"", ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData"") 
                                VALUES 
                               (@Id, 2, @UserId, @IssuerId, @UpdateDate, @UserData);";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = file.user_data.id;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = file.user_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = file.user_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = file.user_data.update_date;
                    cmd.Parameters.Add("@UserData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(file.file_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<UserFile> GetAsync(Guid file_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT  ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData""
                                        FROM public.""UserData""
                                        WHERE ""Id""=@Id and ""IsDeleted""=false";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = file_id;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UserFile
                            {
                                user_data = new UserData
                                {
                                    id = file_id,
                                    issuer_id = (string)reader["IssuerId"],
                                    user_id = (int)reader["UserId"],
                                    update_date = (DateTime)reader["UpdateDate"]
                                },
                                file_data = JsonConvert.DeserializeObject<UserFileData>((string)reader.ReadNullIfDbNull("UserData"))
                            };
                        }
                    }
                    return null;
                }
            }
        }

        private async Task _DeleteFile(Guid file_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"UPDATE public.""UserData""
                                SET  
                                ""IsDeleted""=true
                                WHERE ""Id""=@Id;";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = file_id;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}