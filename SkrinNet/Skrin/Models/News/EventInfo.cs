using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.News
{
    public class EventInfo
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public string Header { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventText { get; set; }
        public DateTime InsertDate { get; set; }
        public string EventGroupName { get; set; }
        public string EventTypeName { get; set; }
        public string FirmName { get; set; }
        public string Ticker { get; set; }
        public string ShortFirmName { get; set; }
    }
}