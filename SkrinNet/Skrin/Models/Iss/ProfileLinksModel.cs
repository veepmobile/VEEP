using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.ProfileLinks
{
    public class ProfileLinksModel
    {
        public ProfileLinksModel()
        {
        }
        public string iss { get; set; }
        public int user_id { get; set; }
        public MainData profile { get; set; }
        public List<FounderToData> founder_to { get; set; }
        public List<ManagerToData> manager_to { get; set; }
        public List<FounderFromFLData> founder_from_fl { get; set; }
        public List<FounderFromULData> founder_from_ul { get; set; }
        public List<FounderFromToFLData> founder_from_to_fl { get; set; }
        public List<FounderFromToULData> founder_from_to_ul { get; set; }
        public List<FounderFromManagerToData> founder_from_manager_to { get; set; }
        public List<SuccessorData> successor_from { get; set; }
        public List<SuccessorData> successor_to { get; set; }
        public List<ManagerFromData> manager_from { get; set; }
        public List<ManagerFromFounderToData> manager_from_founder_to { get; set; }
        public int manager_from_founder_to_inn_count{ get; set; }
        public int manager_from_founder_to_fio_count{ get; set; }
        public List<ManagerFromToData> manager_from_to { get; set; }
        public int manager_from_to_inn_count { get; set; }
        public int manager_from_to_fio_count { get; set; }
        public List<ManagerFromIPData> manager_from_ip { get; set; }
        public int manager_from_ip_inn_count { get; set; }
        public int manager_from_ip_fio_count { get; set; }
    }
    public class MainData
    {
        public string name { get; set; }
        public string short_name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string address { get; set; }
    }

    public class FounderToData
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
        public string share { get; set; }
        public string share_percent { get; set; }
        public string ogrn_to { get; set; }
        public string name_to { get; set; }
        public string status_to { get; set; }
        public string ticker_to { get; set; }
        public string gd { get; set; }
        public string remark { get; set; }
    }
    public class ManagerToData
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string ogrn_to { get; set; }
        public string name_to { get; set; }
        public string status_to { get; set; }
        public string ticker_to { get; set; }
        public string gd { get; set; }
        public string remark { get; set; }
    }
    public class FounderFromFLData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string share { get; set; }
        public string share_percent { get; set; }
        public string gd { get; set; }
    }
    public class FounderFromULData
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
        public string share { get; set; }
        public string share_percent { get; set; }
        public string ticker { get; set; }
        public string status { get; set; }
        public string gd { get; set; }
    }
    public class FounderRecFLData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string share { get; set; }
        public string share_percent { get; set; }
        public string ogrn_to { get; set; }
        public string name_to { get; set; }
        public string status_to { get; set; }
        public string ticker_to { get; set; }
        public string gd { get; set; }
        public string remark { get; set; }
    }
    public class FounderRecULData
    {
        public string name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string share { get; set; }
        public string share_percent { get; set; }
        public string ogrn_to { get; set; }
        public string name_to { get; set; }
        public string status_to { get; set; }
        public string ticker_to { get; set; }
        public string gd { get; set; }
        public string remark { get; set; }
    }
    public class ManagerRecData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public string name { get; set; }
        public string ogrn { get; set; }
        public string ticker { get; set; }
        public string status { get; set; }
        public string gd { get; set; }
        public string remark { get; set; }
    }
    public class FounderFromToFLData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public List<FounderRecFLData> founder_inn { get; set; }
        public List<FounderRecFLData> founder_fio { get; set; }
        public FounderFromToFLData()
        {
            founder_inn = new List<FounderRecFLData>();
            founder_fio = new List<FounderRecFLData>();
        }
    }
    public class FounderFromToULData
    {
        public string name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public List<FounderRecULData> founder { get; set; }
        public FounderFromToULData()
        {
            founder = new List<FounderRecULData>();
        }
    }
    public class FounderFromManagerToData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public List<ManagerRecData> manager_inn { get; set; }
        public List<ManagerRecData> manager_fio { get; set; }
        public FounderFromManagerToData()
        {
            manager_inn = new List<ManagerRecData>();
            manager_fio = new List<ManagerRecData>();
        }
    }
    public class SuccessorData
    {
        public string name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string ticker { get; set; }
        public string status { get; set; }
        public string gd { get; set; }
    }
    public class ManagerFromData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public string gd { get; set; }
    }
    public class ManagerFromFounderToData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public List<FounderRecFLData> founder_inn { get; set; }
        public List<FounderRecFLData> founder_fio { get; set; }
        public ManagerFromFounderToData()
        {
            founder_inn = new List<FounderRecFLData>();
            founder_fio = new List<FounderRecFLData>();
        }
    }
    public class ManagerFromToData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public List<ManagerRecData> manager_inn { get; set; }
        public List<ManagerRecData> manager_fio { get; set; }
        public ManagerFromToData()
        {
            manager_inn = new List<ManagerRecData>();
            manager_fio = new List<ManagerRecData>();
        }
    }
    public class IPRecData
    {
        public string fio { get; set; }
        public string subrf { get; set; }
        public string inn { get; set; }
        public string ogrnip { get; set; }
        public string id { get; set; }
        public string gd { get; set; }
        public string sd { get; set; }
    }
    public class ManagerFromIPData
    {
        public string fio { get; set; }
        public string inn { get; set; }
        public string position { get; set; }
        public List<IPRecData> ip_inn { get; set; }
        public List<IPRecData> ip_fio { get; set; }
        public ManagerFromIPData()
        {
            ip_inn = new List<IPRecData>();
            ip_fio = new List<IPRecData>();
        }
    }
}