using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Debt
{
    public class DebtItem
    {
        public Int64 Id { get; set; }
        public string DebtorName { get; set; }
        public string DebtorAddress { get; set; }
        public string NumProizv { get; set; }
        public string DateProizv { get; set; }
        public string NumSvodPr { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNum { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentReq { get; set; }
        public string Predmet { get; set; }
        public decimal? Sum { get; set; }
        public string PristavName { get; set; }
        public string Region { get; set; }
        public string PristavAddress { get; set; }
        public string UpdateDate { get; set; }
        public string CloseDate { get; set; }
        public string CloseCause { get; set; }
        public Int16 Status { get; set; }
    }

    public enum DebtorSearchType
    {
        None = 0,
        AdressAndName = 1,
        Name = 2,
        Adress = 3
    }

    public class SearchObject
    {
        public string iss { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string numProizv { get; set; }
        public string region { get; set; }
        public string predmet { get; set; }
        //public string address { get; set; }
        //public int debtors { get; set; }
        public int page { get; set; }
        public string step { get; set; }
        //public string debtorstr { get; set; }
        public short version { get; set; }
        public List<DebtorSearchType> searchtypes { get; set; }
        public TypeOfQuery queryoption { get; set; }
    }


    public class DebtorAdress
    {
        public string zip { get; set; }
        public string region { get; set; }
        public string region_name { get; set; }
        public string district_name { get; set; }
        public string city_name { get; set; }
        public string locality_name { get; set; }
        public string street_name { get; set; }
        public string house { get; set; }
    }
    public class DebtorName
    {
        public string shortname { get; set; }
        public string longname { get; set; }
    }
    public enum TypeOfQuery
    {
        AdressOrName = 0,
        AdressAndName = 1,
        AdressXorName = 6,
        Adress = 5,
        AdressNoInter = 4,
        Name = 3,
        NameNoInter = 2
    }
}
