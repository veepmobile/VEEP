using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skrin.BLL.Cloud
{
    public interface ICloudLogger
    {
        Task LogAsync(CloudActions action, UserData u_data, object log_data);
    }
}
