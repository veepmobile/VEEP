using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Tree
{
    public class CodeLine
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool isFolder { get; set; }
        public int parent { get; set; }
        public string extra_info { get; set; }
        public int src { get; set; }
    }

    public class ExpandedCodeLine
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool isFolder { get; set; }
        public int expanded { get; set; }
        public int parent { get; set; }
        public int selected { get; set; }
        public string extra_info { get; set; }
        public int src { get; set; }
    }

    public class ShortCodeLine
    {
        public int id { get; set; }
        public string name { get; set; }
        public string kod { get; set; }
        public ShortCodeLineType type { get; set; }
    }

    public enum ShortCodeLineType
    {
        Text=1,
        Json=2
    }
}