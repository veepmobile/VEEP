using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models
{
    public class SphinxQueryObject
    {
        public string Query { get; set; }
        public int Count { get; set; }
        public int Skip { get; set; }
        public int Port { get; set; }
        public string CharasterSet { get; set; }

        public SphinxQueryObject()
        {
            Query = "";
            Count = 100;
            Skip = 0;
            Port = 9306;
            CharasterSet = "cp1251";
        }

        public string SphinxQuery
        {
            get
            {
                return "query=" + HttpUtility.UrlEncode(Query) + "&count=" + Count.ToString() + "&skip=" + Skip.ToString() + "&port=" + Port.ToString() + "&charaster_set=" + CharasterSet;
            }
        }
    }
}