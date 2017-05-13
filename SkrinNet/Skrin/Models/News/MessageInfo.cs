using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.News
{
    public class MessageInfo
    {
        public List<MessageInfoItem> items { get; set; }
        public string info_date { get; set; }

        public MessageInfo()
        {
            items = new List<MessageInfoItem>();
        }
    }

    public class MessageInfoItem
    {
        public string company_link { get; set; }
        public string company_title { get; set; }
        public string message_link { get; set; }
        public string message_title { get; set; }
        public string datetime { get; set; }
    }
}