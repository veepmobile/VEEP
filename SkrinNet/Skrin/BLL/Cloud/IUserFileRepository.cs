using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skrin.Models.Cloud;

namespace Skrin.BLL.Cloud
{
    interface IUserFileRepository
    {
        Task <IEnumerable<UserFile>> GetAsync(int user_id, string issuer_id);
        Task<UserFile> UpdateAsync(UserFile file);
        Task<UserFile> DeleteAsync(Guid file_id);
        Task<UserFile> GetAsync(Guid file_id);
    }
}
