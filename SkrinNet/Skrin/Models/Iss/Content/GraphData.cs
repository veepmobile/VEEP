using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class GraphData
    {
        
        public List<int>year{get;set;}
        public List<decimal?>al {get;set;}
        public List<decimal?> os { get; set; }
        public List<decimal?>ali { get; set; }
        public List<decimal?> osi { get; set; }

        public GraphData()
        {
            year = new List<int>();
            al = new List<decimal?>();
            ali = new List<decimal?>();
            os = new List<decimal?>();
            osi = new List<decimal?>();
        }
    }


   
}