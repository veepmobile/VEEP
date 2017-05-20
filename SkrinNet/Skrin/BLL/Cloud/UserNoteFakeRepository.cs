using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Cloud
{
    public class UserNoteFakeRepository//:IUserNoteRepository
    {
        private ICloudLogger _logger;


        public UserNoteFakeRepository(ICloudLogger logger)
        {
            _logger = logger;
        }

        private static List<UserNote> _notes = new List<UserNote>{
            new UserNote{
                note_data=new UserNoteData{
                    content="Моя первая заметка. Здесь будет город-сад."
                },
                user_data=new UserData{
                    id=Guid.NewGuid(),
                    user_id=889,
                    issuer_id="3DC84DD11D61CC51C32567400032199E",
                    update_date=new DateTime(2916,12,12)
                }                
            }
        };


        public IEnumerable<UserNote> Get(int user_id, string issuer_id)
        {
            return _notes.Where(p => p.user_data.user_id == user_id && p.user_data.issuer_id == issuer_id).OrderBy(p => p.user_data.update_date);
        }

        public UserNote Update(UserNote note)
        {
            if (note.user_data.id == null)
            {
                note.user_data.id = Guid.NewGuid();
            }
            note.user_data.update_date = DateTime.Now;

            var exist_note = _notes.Where(p => p.user_data.id == note.user_data.id).FirstOrDefault();
            if (exist_note == null)
            {
                _notes.Add(note);
                _logger.LogAsync(CloudActions.AddNote, note.user_data, note.note_data);
            }
            else
            {
                exist_note.user_data.issuer_id = note.user_data.issuer_id;
                exist_note.user_data.user_id = note.user_data.user_id;
                exist_note.note_data.content = note.note_data.content;
                 _logger.LogAsync(CloudActions.UpdateNote, note.user_data, note.note_data);
            }
            return note;
        }


        public UserNote Delete(Guid note_id)
        {
            var exist_note = _notes.Where(p => p.user_data.id == note_id).FirstOrDefault();
            if (exist_note == null)
                return null;
            _notes.Remove(exist_note);
            _logger.LogAsync(CloudActions.DeleteNote, exist_note.user_data, exist_note.note_data);
            return exist_note;
        }
    }
}