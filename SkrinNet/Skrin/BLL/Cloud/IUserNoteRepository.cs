using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skrin.BLL.Cloud
{
    interface IUserNoteRepository
    {
        Task<IEnumerable<UserNote>> GetAsync(int user_id, string issuer_id);
        Task<UserNote> UpdateAsync(UserNote note);
        Task<UserNote> DeleteAsync(Guid note_id);
    }
}
