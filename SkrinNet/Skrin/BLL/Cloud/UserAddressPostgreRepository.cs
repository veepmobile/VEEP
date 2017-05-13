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
    public class UserAddressPostgreRepository:IUserAddressRepository
    {

        private ICloudLogger _logger;

        public UserAddressPostgreRepository(ICloudLogger logger)
        {
            _logger = logger;
        }

        static string constring = Configs.PostgreConnectionString;

        public async Task<IEnumerable<UserAddress>> GetAsync(int user_id, string issuer_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT ""Id"", ""UpdateDate"", ""UserData""
                            FROM public.""UserData"" 
                            WHERE ""IsDeleted""=false and   ""DataType""=3 and ""UserId""=@UserId and ""IssuerId""=@IssuerId";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = issuer_id;
                    List<UserAddress> addresses = new List<UserAddress>();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            addresses.Add(new UserAddress
                            {
                                user_data = new UserData
                                {
                                    id = (Guid)reader["Id"],
                                    update_date = (DateTime)reader["UpdateDate"],
                                    user_id = user_id,
                                    issuer_id = issuer_id
                                },
                                address_data = JsonConvert.DeserializeObject<UserAddressData>((string)reader.ReadNullIfDbNull("UserData"))
                            });
                        };
                    }
                    return addresses;
                }
            }
        }

        public async Task<UserAddress> UpdateAsync(UserAddress address)
        {
            address.user_data.update_date = DateTime.Now;
            if (address.user_data.id == null)
            {
                address.user_data.id = Guid.NewGuid();
                await _InsertUserAddress(address);
                await _logger.LogAsync(CloudActions.AddAddress, address.user_data, address.address_data);
            }
            else
            {
                await _UpdateUserAddress(address);
                await _logger.LogAsync(CloudActions.UpdateAddress, address.user_data, address.address_data);
            }
            return address;
        }

        public async Task<UserAddress> DeleteAsync(Guid address_id)
        {
            var deleted_address = await _SelectAddress(address_id);
            if (deleted_address != null)
            {
                await _DeleteAddress(address_id);
                await _logger.LogAsync(CloudActions.DeleteAddress, deleted_address.user_data, deleted_address.address_data);
            }
            return deleted_address;
        }

        private async Task _InsertUserAddress(UserAddress address)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"INSERT INTO public.""UserData"" (""Id"", ""DataType"", ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData"") 
                                VALUES 
                               (@Id, 3, @UserId, @IssuerId, @UpdateDate, @UserData);";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = address.user_data.id;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = address.user_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = address.user_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = address.user_data.update_date;
                    cmd.Parameters.Add("@UserData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(address.address_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task _UpdateUserAddress(UserAddress address)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"UPDATE public.""UserData""
                                SET  
                                ""UserId""=@UserId, 
                                ""IssuerId""=@IssuerId, 
                                ""UpdateDate""=@UpdateDate, 
                                ""UserData""=@UserData
                                WHERE ""Id""=@Id;";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = address.user_data.id;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = address.user_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = address.user_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = address.user_data.update_date;
                    cmd.Parameters.Add("@UserData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(address.address_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<UserAddress> _SelectAddress(Guid address_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT  ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData""
                                        FROM public.""UserData""
                                        WHERE ""Id""=@Id and ""IsDeleted""=false";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = address_id;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UserAddress
                            {
                                user_data = new UserData
                                {
                                    id = address_id,
                                    issuer_id = (string)reader["IssuerId"],
                                    user_id = (int)reader["UserId"],
                                    update_date = (DateTime)reader["UpdateDate"]
                                },
                                address_data = JsonConvert.DeserializeObject<UserAddressData>((string)reader.ReadNullIfDbNull("UserData"))
                            };
                        }
                    }
                    return null;
                }
            }
        }

        private async Task _DeleteAddress(Guid address_id)
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
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = address_id;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}