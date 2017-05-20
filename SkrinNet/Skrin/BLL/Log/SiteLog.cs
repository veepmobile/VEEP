using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Log
{
    public class SiteLog
    {
        public int UserId { get; set; }
        public string IP { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string Url { get; set; }
        public string PostParams { get; set; }
        public byte SiteId { get; set; }
        public DateTime InsertDate { get; set; }
        
    }


    public enum HttpMethod:byte{
        GET=1,
        POST=2
    } 

}