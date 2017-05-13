using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Cloud
{
    public class UserAddress
    {
        public UserData user_data { get; set; }
        public UserAddressData address_data { get; set; }


        public UserAddress()
        {

        }

        public UserAddress(UserAddressContainer input)
        {
            user_data = new UserData(input);
            address_data = new UserAddressData
            {
                name = input.name,
                phone = input.phone,
                email = input.email,
                note = input.note,
                extrafields = input.extrafields
            };
        }
    }

    public class UserAddressData
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public List<UserAddressField> extrafields { get; set; }
    }

    public class UserAddressContainer : UserDataContainer
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public List<UserAddressField> extrafields { get; set; }

        public UserAddressContainer()
        {

        }

        public UserAddressContainer(UserAddress address)
            : base(address.user_data)
        {
            name = address.address_data.name;
            phone = address.address_data.phone;
            email = address.address_data.email;
            note = address.address_data.note;
            extrafields = address.address_data.extrafields ?? new List<UserAddressField>();
        }
    }

    public class UserAddressField
    {
        public int key { get; set; }
        public string value { get; set; }
    }
}