using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skrin.Models.Iss.StopLight
{
    /// <summary>
    /// Контейнер Светофора для Pdf отчета
    /// </summary>
    public class StopLight
    {

        public List<StopLightRowData> RedStops { get; set; }
        public List<StopLightRowData> YellowStops { get; set; }

        public StopLightHeaderData header { get; set; }


        public List<StopLightHeaderData> headerList
        {
            get { return new List<StopLightHeaderData>() { header }; }
        }

    }


    public class StopLightHeaderData
    {
        public string red { get; set; }
        public string yellow { get; set; }
        public string green { get; set; }
        public string rating_date { get; set; }

        public string txt { get; set; }
    }

    public class StopLightRowData
    {
        public string phead { get; set; }
        public string cstop { get; set; }
        public string data { get; set; }
    }
}
