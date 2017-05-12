using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models
{
    public class Keyword : IEquatable<Keyword>
    {
        public string Key { get; set; }
        public string Docs { get; set; }
        public string Hits { get; set; }

        public List<int> Used { get; set; }

        public Keyword()
        {
            Used = new List<int>();
        }

        public bool Equals(Keyword other)
        {
            return Key == other.Key;
        }

        public string ToString(int i)
        {
            return string.Format("\"keyword[{0}]\":\"{1}\",\"docs[{0}]\":\"{2}\",\"hits[{0}]\":\"{3}\",\"used[{0}]\":\"{4}\"", i, Key, Docs, Hits, string.Join(",", Used));
        }
    }

    public class KeywordList
    {
        private List<Keyword> _keywords;

        public KeywordList(List<Keyword> keywords)
        {
            _keywords = keywords;
        }

        public List<string> ToList()
        {
            List<string> ret = new List<string>();
            int i = 0;
            foreach (var key in _keywords)
            {
                ret.Add(key.ToString(i));
                i++;
            }
            return ret;
        }
    }
}