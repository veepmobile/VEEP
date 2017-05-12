using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Authentication
{
    public class ExtGroup
    {
        public int code { get; set; }
        public string txt { get; set; }
        public string val { get; set; }
    }

    public class ListGroup
    {
        public int lid { get; set; }
        public string name { get; set; }
        public int cnt { get; set; }
        public string cnt_disp { get; set; }
    }
}