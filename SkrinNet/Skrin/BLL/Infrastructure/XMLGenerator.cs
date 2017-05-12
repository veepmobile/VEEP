using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Skrin.BLL.Infrastructure
{
    /// <summary>
    /// Класс - обертка XmlSerializer
    /// </summary>
    public class XMLGenerator<T>
    {
        private XmlSerializer _serializer;
        private T _obj;

        public XMLGenerator(T obj)
        {
            _serializer = new XmlSerializer(typeof(T));
            _obj = obj;
        }

        public XMLGenerator(T obj, string schema)
        {
            _serializer = new XmlSerializer(typeof(T), schema);
            _obj = obj;
        }

        public string GetStringXML()
        {
            string res = "";
            using (var ms = new MemoryStream())
            {
                _serializer.Serialize(ms, _obj);
                ms.Position = 0;
                StreamReader reader = new StreamReader(ms);
                res = reader.ReadToEnd();
                reader.Close();
            }
            return res;
        }

        public static string GetStringXML(T obj)
        {
            return new XMLGenerator<T>(obj).GetStringXML();
        }

    }
}