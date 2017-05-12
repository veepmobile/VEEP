using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Cloud
{
    interface IUserAddressRepository
    {
        Task<IEnumerable<UserAddress>> GetAsync(int user_id, string issuer_id);
        Task<UserAddress> UpdateAsync(UserAddress address);
        Task<UserAddress> DeleteAsync(Guid address_id);
    }
}