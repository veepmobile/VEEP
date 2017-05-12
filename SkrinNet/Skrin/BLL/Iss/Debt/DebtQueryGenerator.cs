using Skrin.Models.Iss.Debt;
using Skrin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.DebtBLL
{
    public class DebtQueryGenerator
    {
        private Skrin.Models.Iss.Debt.SearchObject _so;
        private CompanyData _company;
        private List<DebtorAdress> _adress;
        private List<DebtorName> _names;

        public DebtQueryGenerator(Skrin.Models.Iss.Debt.SearchObject so, CompanyData company, List<DebtorAdress> adress, List<DebtorName> names)
        {
            //so.address = (!string.IsNullOrEmpty(so.address) ? System.Uri.UnescapeDataString(so.address) : so.address);
            //so.debtorName = (!string.IsNullOrEmpty(so.debtorName) ? System.Uri.UnescapeDataString(so.debtorName) : so.debtorName);
            //so.fullName = (!string.IsNullOrEmpty(so.fullName) ? System.Uri.UnescapeDataString(so.fullName) : so.fullName);
            //so.debtorstr = (!string.IsNullOrEmpty(so.debtorstr) ? System.Uri.UnescapeDataString(so.debtorstr) : so.debtorstr);
            so.predmet = (!string.IsNullOrEmpty(so.predmet) ? System.Uri.UnescapeDataString(so.predmet) : so.predmet);
            _so = so;
            _company = company;
            _adress = adress;
            _names = names;
        }
        public DebtQueryGenerator(CompanyData company, List<DebtorAdress> adress, List<DebtorName> names)
        {
            _so = new Skrin.Models.Iss.Debt.SearchObject();
            _so.predmet = "-1";
            //_so.address = "-1";
            _so.region = "0";
            _so.version = -1;
            _so.queryoption = TypeOfQuery.AdressOrName;
            _company = company;
            _adress = adress;
            _names = names;
        }

        public string GetQuery()
        {
            int count;
            if (!Int32.TryParse(_so.step, out count))
            {
                count = 20;
            }
            int from = (_so.page - 1) * count;
            string orderby = "date1_ts";
            string ordercharacter = "desc";
            string query_pattern = "{{\"sort\":{{\"{0}\":{{\"order\":\"{1}\"}}}},\"from\":{2},\"size\":{3},\"query\":{{\"bool\":{{\"must\":[{4}]}}}}}}";
            string cond = _GetCondition();
            return string.Format(query_pattern, orderby, ordercharacter, from, count, cond);
        }

        public string GetExport()
        {
            string json = "{\"sort\":{\"date1_ts\":\"desc\"},\"_source\":[\"id\",\"version\"],\"size\":10000,\"query\":{\"bool\":{\"must\":[" + _GetCondition() + "]}}}";
            return json;
        }
        public string GetRegions()
        {
            string json = "{\"size\":0,\"query\" : {\"bool\":{\"must\":[" + _GetCondition() + 
                "]}},\"aggs\":{\"regions\":{\"terms\":{\"field\":\"region_id\",\"size\":0,\"order\":{\"_term\":\"asc\"}},\"aggs\":{\"regionsnames\":{\"terms\":{\"field\":\"name\"}}}}}}";
            return json;
        }
        public string GetPredmets()
        {
            string json = "{\"size\":0,\"query\" : {\"bool\":{\"must\":[" + _GetCondition() +
                "]}},\"aggs\":{\"predmets\":{\"terms\":{\"field\":\"predmet\",\"size\":0}}}}";
            return json;
        }
        public string GetAdressess()
        {
            string json = "{\"size\":0,\"query\" : {\"bool\":{\"must\":[" + _GetCondition() +
                "]}},\"aggs\":{\"addresses\":{\"terms\":{\"field\":\"adress\",\"size\":0}}}}";
            return json;
        }
        public string GetDebtors()
        {
            string json = "{\"size\":0,\"query\" : {\"bool\":{\"must\":[" + _GetCondition() +
                "]}},\"aggs\":{\"debtors\":{\"terms\":{\"field\":\"debtor\",\"size\":0}}}}";
            return json;
        }
        public string GetSummaryTable()
        {
            string json = "{\"size\":0,\"query\" : {\"bool\":{\"must\":[" + _GetCondition() +
                "]}},\"aggs\":{\"summarytable\":{\"date_histogram\":{\"field\":\"date1_ts\"," + 
                "\"interval\":\"year\",\"order\":{\"_key\":\"desc\"}},\"aggs\":{\"versions\"" + 
                ":{\"terms\":{\"field\":\"version\"},\"aggs\":{\"summofdebts\":{\"sum\":{\"field\":\"summ\"}}}}}}}}";
            return json;
        }
        public string GetListOfIdAndVersion()
        {
            string json = "{\"sort\":{\"id\":\"asc\"},\"_source\":[\"id\",\"version\"],\"size\":10000,\"query\":{\"bool\":{\"must\":[" + _GetCondition() + "]}}}";
            return json;
        }

        private string _GetCondition()
        {
            if ((_names == null && (_so.queryoption == TypeOfQuery.AdressAndName || _so.queryoption == TypeOfQuery.AdressOrName || 
                _so.queryoption == TypeOfQuery.AdressXorName || _so.queryoption == TypeOfQuery.Name || _so.queryoption == TypeOfQuery.NameNoInter)) ||
                (_adress == null && (_so.queryoption == TypeOfQuery.AdressAndName || _so.queryoption == TypeOfQuery.AdressOrName ||
                _so.queryoption == TypeOfQuery.AdressXorName || _so.queryoption == TypeOfQuery.Adress || _so.queryoption == TypeOfQuery.AdressNoInter)))
            {
                return "{\"term\":{\"returnnothing\":\"true\"}}";
            }
            string ret = ""; // полный запрос
            string temp = ""; // временное хранилище
            string nameq = ""; // запрос по имени
            string adressfullq = ""; // запрос по всем адресам
            string adressq = ""; // запрос по одному адресу
            string adresstopq = ""; // регион, регион id, район, город
            string adressbottomq = ""; // поселение, улица, дом
            string adresszipq = ""; // индекс
            string boolshould = "{{\"bool\":{{\"should\":[{0}]}}}}"; // логическое ИЛИ
            string boolmust = "{{\"bool\":{{\"must\":[{0}]}}}}"; // логическое И
            string boolmustnot = "{{\"bool\":{{\"must_not\":[{0}]}}}}"; // логическое НЕ

            // + Название
            if (_names != null)
            {
                foreach (var name in _names)
                {
                    if (!(string.IsNullOrEmpty(name.shortname) && string.IsNullOrEmpty(name.longname)))
                    {
                        if (!string.IsNullOrEmpty(name.shortname))
                        {
                            temp += "{\"match_phrase\":{\"debtoranalyzed\":\"" + name.shortname + "\"}},";
                        }
                        if (!string.IsNullOrEmpty(name.longname))
                        {
                            temp += "{\"match_phrase\":{\"debtoranalyzed\":\"" + name.longname + "\"}},";
                        }
                    }
                }
                nameq = string.Format(boolshould, temp.Trim(','));
            }
            // - Название

            // + Адрес
            if (_adress != null)
            {
                foreach (var addr in _adress)
                {
                    temp = "";
                    adresstopq = "";
                    adressbottomq = "";
                    adresszipq = "";
                    // + Верхний уровень
                    if (!String.IsNullOrEmpty(addr.region_name) || !String.IsNullOrEmpty(addr.region))
                    {
                        if (!string.IsNullOrEmpty(addr.region))
                        {
                            temp = "{\"match\":{\"a_regid\":\"" + addr.region + "\"}},";
                        }
                        if (!string.IsNullOrEmpty(addr.region_name))
                        {
                            temp += "{\"match_phrase\":{\"a_regname\":\"" + addr.region_name + "\"}},";
                        }
                        temp += string.Format(boolmust, "{\"match\":{\"a_regid\":\"NULL\"}},{\"match\":{\"a_regname\":\"NULL\"}}");
                        adresstopq += string.Format(boolshould, temp) + ",";
                    }
                    if (!String.IsNullOrEmpty(addr.district_name))
                    {
                        temp = "{\"match_phrase\":{\"a_distrname\":\"" + addr.district_name + "\"}},{\"match\":{\"a_distrname\":\"NULL\"}}";
                        adresstopq += string.Format(boolshould, temp) + ",";
                    }
                    if (!String.IsNullOrEmpty(addr.city_name))
                    {
                        temp = "{\"match_phrase\":{\"a_cityname\":\"" + addr.city_name + "\"}},{\"match\":{\"a_cityname\":\"NULL\"}}";
                        adresstopq += string.Format(boolshould, temp);
                    }
                    adresstopq = adresstopq.TrimEnd(',');
                    // - Верхний уровень
                    // + Нижний уровень
                    if (!string.IsNullOrEmpty(addr.locality_name))
                    {
                        temp = "{\"match_phrase\":{\"a_localityname\":\"" + addr.locality_name + "\"}}";
                        adressbottomq += temp + ",";
                    }
                    if (!string.IsNullOrEmpty(addr.street_name))
                    {
                        temp = "{\"match_phrase\":{\"a_streetname\":\"" + addr.street_name + "\"}}";
                        adressbottomq += temp + ",";
                    }
                    if (!string.IsNullOrEmpty(addr.house))
                    {
                        temp = "{\"match_phrase\":{\"a_dopinfo\":\"" + addr.house + "\"}},{\"match_phrase\":{\"a_housenum\":\"" +
                            addr.house + "\"}}";
                        adressbottomq += string.Format(boolshould, temp);
                    }
                    if (!string.IsNullOrEmpty(adressbottomq))
                    {
                        adressbottomq = String.Format(boolmust, adressbottomq.TrimEnd(','));
                    }
                    // - Нижний уровень
                    // + Индекс
                    if (!string.IsNullOrEmpty(addr.zip))
                    {
                        temp = "";
                        temp += "{\"match\":{\"a_streetname\":\"NULL\"}},";
                        temp += "{\"match\":{\"a_localityname\":\"NULL\"}},";
                        temp += "{\"match\":{\"a_housenum\":\"NULL\"}},";
                        temp += "{\"match\":{\"a_zip\":\"" + addr.zip + "\"}}";
                        adresszipq += string.Format(boolmust, temp.TrimEnd(','));
                    }
                    // - Индекс
                    adressq += String.Format(boolmust, (adresstopq + "," + String.Format(boolshould, (adressbottomq + "," + adresszipq).Trim(','))).Trim(',')) + ",";
                }
            adressfullq = String.Format(boolshould, adressq.TrimEnd(','));
            }
            // - Адрес
            // + Тип запроса
            switch (_so.queryoption)
            {
                case TypeOfQuery.AdressAndName:
                    ret += String.Format(boolmust, (nameq + "," + adressfullq).Trim(',')) + ",";
                    break;
                case TypeOfQuery.AdressOrName:
                    ret += String.Format(boolshould, (nameq + "," + adressfullq).Trim(',')) + ",";
                    break;
                case TypeOfQuery.AdressXorName:
                    ret += String.Format(boolmust, (String.Format(boolshould, (nameq + "," + adressfullq).Trim(',')) + "," +
                    String.Format(boolmustnot, String.Format(boolmust, (nameq + "," + adressfullq).Trim(',')))).Trim(',')) + ",";
                    break;
                case TypeOfQuery.Adress:
                    if (!String.IsNullOrEmpty(adressfullq))
                        ret += adressfullq  + ",";
                    break;
                case TypeOfQuery.AdressNoInter:
                    ret += String.Format(boolmust, (adressfullq + "," +
                    String.Format(boolmustnot, String.Format(boolmust, (nameq + "," + adressfullq).Trim(',')))).Trim(',')) + ",";
                    break;
                case TypeOfQuery.Name:
                    if (!String.IsNullOrEmpty(nameq))
                        ret += nameq  + ",";
                    break;
                case TypeOfQuery.NameNoInter:
                    ret += String.Format(boolmust, (nameq + "," +
                   String.Format(boolmustnot, String.Format(boolmust, (nameq + "," + adressfullq).Trim(',')))).Trim(',')) + ",";
                    break;
            }
            // - Тип запроса
            // + Пользовательские фильтры
            if (!string.IsNullOrEmpty(_so.numProizv))
            {
                ret += "{\"match\":{\"nproizv\":\"" + _so.numProizv + "\"}},";
            }
            if (_so.predmet != "-1")
            {
                ret += "{\"match\":{\"predmet\":\"" + _so.predmet + "\"}},";
            }
            //if (_so.address != "-1")
            //{
            //    ret += "{\"match\":{\"adress\":\"" + _so.address + "\"}},";
            //}
            if (_so.region != "0")
            {
                ret += "{\"term\":{\"region_id\":\"" + _so.region + "\"}},";
            }
            if (!(string.IsNullOrEmpty(_so.dateFrom) && string.IsNullOrEmpty(_so.dateTo)))
            {
                string datequery = "{{\"range\":{{\"date1_ts\":{{{0}}}}}}},";
                string datequeryfilling = "";
                if (!string.IsNullOrEmpty(_so.dateFrom))
                {
                    datequeryfilling += "\"gte\":\""+ _so.dateFrom + "\",";
                }
                if (!string.IsNullOrEmpty(_so.dateTo))
                {
                    datequeryfilling += "\"lte\":\"" + _so.dateTo + "\"";
                }
                ret += string.Format(datequery, datequeryfilling.TrimEnd(','));
            }
            //if (!String.IsNullOrEmpty(_so.debtorstr))
            //{
            //    ret += "{\"terms\":{\"debtor\":[" + _so.debtorstr.TrimEnd(',') + "]}},";
            //}
            if (_so.version != -1)
            {
                ret += "{\"term\":{\"version\":\"" + _so.version + "\"}},";
            }
            // - Пользовательские фильтры
            // + ИП не удалено!!!
            ret += "{\"term\":{\"deletedate\":\"01.01.1900\"}}";
            // - ИП не удалено!!!

            return ret;
        }
    }
}