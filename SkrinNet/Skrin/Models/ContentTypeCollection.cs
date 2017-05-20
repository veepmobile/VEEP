using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models
{
    public static class ContentTypeCollection
    {

        static Dictionary<string, ContectTypeItem> _items = new Dictionary<string, ContectTypeItem>{
            {"jpg",new ContectTypeItem{ContentType="image/jpeg",Ftype=FileType.Img,Style="icon-file-image image"}},
            {"bmp",new ContectTypeItem{ContentType="image/bmp",Ftype=FileType.Img, Style="icon-file-image image"}},
            {"tif",new ContectTypeItem{ContentType="image/tiff",Ftype=FileType.Img, Style="icon-file-image image" }},
            {"txt",new ContectTypeItem{ContentType="text/html",Ftype=FileType.Text, Style="icon-file-doc doc"}},
            {"htm",new ContectTypeItem{ContentType="text/html",Ftype=FileType.Binary,Style="icon-file-doc doc"}},
            {"xml",new ContectTypeItem{ContentType="text/xml",Ftype=FileType.Xml,Style="icon-file-code code"}},
            {"html",new ContectTypeItem{ContentType="text/html",Ftype=FileType.Binary,Style="icon-file-doc doc"}},
            {"doc",new ContectTypeItem{ContentType="application/msword",Ftype=FileType.Binary,Style="icon-file-word word"}},
            {"docx",new ContectTypeItem{ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document",Ftype=FileType.Binary,Style="icon-file-word word"}},
            {"rtf",new ContectTypeItem{ContentType="application/rtf",Ftype=FileType.Binary, Style="icon-file-word word"}},
            {"pdf",new ContectTypeItem{ContentType="application/pdf",Ftype=FileType.Binary, Style="icon-file-pdf pdf"}},
            {"xls",new ContectTypeItem{ContentType="application/vnd.ms-excel",Ftype=FileType.Binary,Style="icon-file-excel excel"}},
            {"xlsx",new ContectTypeItem{ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Ftype=FileType.Binary, Style="icon-file-excel excel"}},
            {"zip",new ContectTypeItem{ContentType="application/x-zip-compressed",Ftype=FileType.Binary, Style="icon-file-archive archive"}},
            {"rar",new ContectTypeItem{ContentType="application/rar",Ftype=FileType.Binary, Style="icon-file-archive archive"}}
        };



        private class ContectTypeItem
        {
            public string ContentType { get; set; }
            public string Style { get; set; }
            public FileType Ftype { get; set; }
        }

        public static string GetContentType(string extention)
        {
            ContectTypeItem item=null;
            _items.TryGetValue(extention, out item);
            if (item != null)
                return item.ContentType;
            return "application/octet-stream";
        }

        public static string GetStyle(string extention)
        {
            ContectTypeItem item = null;
            _items.TryGetValue(extention, out item);
            if (item != null)
                return item.Style;
            return "icon-doc doc";
        }

        public static FileType GetFileType(string extention)
        {
            ContectTypeItem item = null;
            _items.TryGetValue(extention, out item);
            if (item != null)
                return item.Ftype;
            return FileType.Binary;
        }

    }

    

    public enum FileType
    {
        Binary,Text,Xml,Img
    }
}