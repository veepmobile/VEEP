using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Cloud
{
    public class UserFile
    {
        public UserData user_data { get; set; }
        public UserFileData file_data { get; set; }
    }

    public class UserFileData
    {
        public string file_name { get; set; }
        public int file_size { get; set; }
    }

    public class UserFileContainer:UserDataContainer
    {
        public string file_name { get; set; }
        public int file_size { get; set; }

        public UserFileContainer()
        {

        }

        public UserFileContainer(UserFile file):base(file.user_data)
        {
            file_name = file.file_data.file_name;
            file_size = file.file_data.file_size;
        }
    }
}