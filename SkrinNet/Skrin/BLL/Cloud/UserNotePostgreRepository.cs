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
    public class UserNotePostgreRepository:IUserNoteRepository
    {

        private ICloudLogger _logger;

        public UserNotePostgreRepository(ICloudLogger logger)
        {
            _logger = logger;
        }

        static string constring = Configs.PostgreConnectionString;

        public async Task<IEnumerable<UserNote>> GetAsync(int user_id, string issuer_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT ""Id"", ""UpdateDate"", ""UserData""
                            FROM public.""UserData"" 
                            WHERE ""IsDeleted""=false and   ""DataType""=1 and ""UserId""=@UserId and ""IssuerId""=@IssuerId";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = issuer_id;
                    List<UserNote> notes = new List<UserNote>();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            notes.Add(new UserNote
                            {
                                user_data = new UserData
                                {
                                    id = (Guid)reader["Id"],
                                    update_date = (DateTime)reader["UpdateDate"],
                                    user_id = user_id,
                                    issuer_id = issuer_id
                                },
                                note_data = JsonConvert.DeserializeObject<UserNoteData>((string)reader.ReadNullIfDbNull("UserData"))
                            });
                        };
                    }
                    return notes;
                }
            }
        }

        

        public async Task<UserNote> UpdateAsync(UserNote note)
        {
            note.user_data.update_date = DateTime.Now;
            if(note.user_data.id==null)
            {
                note.user_data.id = Guid.NewGuid();
                await _InsertUserNote(note);
                await _logger.LogAsync(CloudActions.AddNote, note.user_data, note.note_data);
            }
            else
            {
                await _UpdateUserNote(note);
                await _logger.LogAsync(CloudActions.UpdateNote, note.user_data, note.note_data);
            }
            return note;
        }

        public async Task<UserNote> DeleteAsync(Guid note_id)
        {
            var deleted_note =await  _SelectNote(note_id);
            if (deleted_note != null)
            {
                await _DeleteNote(note_id);
                await _logger.LogAsync(CloudActions.DeleteNote, deleted_note.user_data, deleted_note.note_data);
            }
            return deleted_note;
        }

        private async Task _InsertUserNote(UserNote note)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"INSERT INTO public.""UserData"" (""Id"", ""DataType"", ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData"") 
                                VALUES 
                               (@Id, 1, @UserId, @IssuerId, @UpdateDate, @UserData);";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = note.user_data.id;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = note.user_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = note.user_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = note.user_data.update_date;
                    cmd.Parameters.Add("@UserData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(note.note_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task _UpdateUserNote(UserNote note)
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
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = note.user_data.id;
                    cmd.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = note.user_data.user_id;
                    cmd.Parameters.Add("@IssuerId", NpgsqlDbType.Varchar).Value = note.user_data.issuer_id;
                    cmd.Parameters.Add("@UpdateDate", NpgsqlDbType.Timestamp).Value = note.user_data.update_date;
                    cmd.Parameters.Add("@UserData", NpgsqlDbType.Jsonb).Value = JsonConvert.SerializeObject(note.note_data);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<UserNote> _SelectNote(Guid note_id)
        {
            using (var con = new NpgsqlConnection(constring))
            {
                con.Open();
                string sql = @"SELECT  ""UserId"", ""IssuerId"", ""UpdateDate"", ""UserData""
                                        FROM public.""UserData""
                                        WHERE ""Id""=@Id and ""IsDeleted""=false";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = note_id;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {                        
                        if (await reader.ReadAsync())
                        {
                            return new UserNote
                            {
                                user_data = new UserData
                                {
                                    id = note_id,
                                    issuer_id = (string)reader["IssuerId"],
                                    user_id = (int)reader["UserId"],
                                    update_date = (DateTime)reader["UpdateDate"]
                                },
                                note_data = JsonConvert.DeserializeObject<UserNoteData>((string)reader.ReadNullIfDbNull("UserData"))
                            };
                        }
                    }
                    return null;
                }
            }
        }

        private async Task _DeleteNote(Guid note_id)
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
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = note_id;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        

        
    }
}