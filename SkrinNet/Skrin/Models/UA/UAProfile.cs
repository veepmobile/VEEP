using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.UA
{
    public class UAProfile
    {
        public string edrpou {get; set;}
        public int source_id {get; set;}
        public string name  {get; set;}
        public string short_name {get;set;}
        public int? opf {get;set;}
        public string opf_descr { get; set; }
        public string address { get; set; }
        public int? area_id {get;set;}
        public string area_name { get; set; }
        public string post_index { get; set; }
        public string region {get;set;}
        public string city {get;set;}
        public string street {get;set;}
        public string building {get;set;}
        public string corpus {get;set;}
        public string flat {get;set;}
        public string phone {get;set;}
        public string fax {get;set;}
        public string web {get;set;}
        public string email {get;set;}
        public string ruler_name {get;set;}
        public string ruler_title {get;set;}
        public string regno {get;set;}
        public string regdate {get;set;}
        public string regorg {get;set;}
        public string koatuu {get; set;}
        public string koatuu_name { get; set; }
        public string kfv {get;set;}
        public string kfv_descr { get; set; }
        public string spodu { get; set; }
        public string spodu_descr { get; set; }
        public string codu { get; set; }
        public string codu_descr { get; set; }

        public string main_kved { get; set; }
        public string main_kved_descr { get; set; } 

        public List<Kved> all_kveds { get; set; }
    }

    public class Kved
    {
        public string kod { get; set; } //="34.10.2" 
        public string name { get; set; } //="Производство легковых автомобилей" 
        public int is_main { get; set; }
    }
}