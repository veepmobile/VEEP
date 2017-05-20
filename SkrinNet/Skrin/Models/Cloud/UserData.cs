using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Cloud
{
    public class UserData
    {
        public Guid? id { get; set; }
        public int user_id { get; set; }
        public string issuer_id { get; set; }
        public DateTime? update_date { get; set; }

        public UserData(UserDataContainer input)
        {
            Guid tmp_id;
            id = Guid.TryParse(input.id, out tmp_id) ? (Guid?)tmp_id : null;

            user_id = input.user_id;
            issuer_id = input.issuer_id;
            //update_date с клиента не нужна
            update_date = null;
        }

        public UserData()
        {

        }

    }

    public class UserDataContainer
    {
        public string id { get; set; }
        public int user_id { get; set; }
        public string issuer_id { get; set; }
        public string update_date { get; set; }

        public UserDataContainer()
        {

        }

        public UserDataContainer(UserData data)
        {
            id = data.id.Value.ToString();
            user_id = data.user_id;
            issuer_id = data.issuer_id;
            update_date = data.update_date.Value.ToString("dd.MM.yyyy");
        }

    }

}