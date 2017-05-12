using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.Models.Iss.Content
{
   
    public class ZakupkiSearchObject
    {
        

        public string inn { get; set; }
        public string dfrom { get; set; }
        public string dto { get; set; }
        public bool Is44 { get; set; }
        public bool Is94 { get; set; }
        public bool Is223 { get; set; }
        public bool IsSup { get; set; }
        public bool IsCus { get; set; }
        public bool IsPar { get; set; }
        public string reg_num { get; set; }
        public string status { get; set; }
        public string sfrom { get; set; }
        public string sto { get; set; }
        public bool is_product { get; set; }
        public string product { get; set; }
        public int product_excl { get; set; }
        public bool is_contrname { get; set; }
        public string contragent { get; set; }
        public int contragent_excl { get; set; }
        public int page { get; set; }
        public int isAll { get; set; }
    }

    public class ZakupkiStageStatus
    {
        public byte? id { get; set; }
        public string name { get; set; }
    }

    public class ZakupkiStageGroup
    {
        public string name { get; set; }
        public List<ZakupkiStageStatus> statuses { get; set; }
    }

    public class NotificationData
    {
        public string pur_num { get; set; }
        public int lot_num { get; set; }
        public string not_href { get; set; }
        public string not_type { get; set; }
        public string etp_name { get; set; }
        public string etp_url { get; set; }
        public string purchaseObjectInfo { get; set; }
        public string status_name { get; set; }
        public NotCustomer customer { get; set; }
        public Stage1 stage1 { get; set; }
        public Stage2 stage2 { get; set; }
        public decimal? maxPrice { get; set; }
        public decimal? totalSum { get; set; }
        public string financeSource { get; set; }
        public string currency { get; set; }
        public string preference { get; set; }
        public string requirement { get; set; }
        public string restrictInfo { get; set; }
        public List<CustRequirement> custRequirements { get; set; }
        public List<Purchase> purchases { get; set; }
        public PurchaseDocumentation purchaseDocumentation { get; set; }
        public List<ShortContractInfo> contracts { get; set; }
        public List<ShortLortInfo> other_lots { get; set; }
    }

    public class ShortContractInfo
    {
        public long id { get; set; }
        public string pub_date { get; set; }
        public string reg_num { get; set; }
        public string price { get; set; }
        public string stage { get; set; }
        public string cust_name { get; set; }
        public string cust_ticker { get; set; }
        public string placing { get; set; }
        public string supliers { get; set; }
        public int contract_source { get; set; }
    }

    public class ShortLortInfo
    {
        public int lot_number { get; set; }
        public decimal? max_price { get; set; }
        public string lot_info { get; set; }
    }

    public class NotCustomer
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public string post_address { get; set; }
        public string legal_address { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string add_info { get; set; }
    }

    public class Stage1
    {
        public string collecting_startDate { get; set; }
        public string collecting_place { get; set; }
        public string collecting_order { get; set; }
        public string collecting_endDate { get; set; }
        public string collecting_addInfo { get; set; }
        public string collecting_form { get; set; }

        public string opening_date { get; set; }
        public string opening_place { get; set; }
        public string opening_addInfo { get; set; }

        public string scoring_date { get; set; }
        public string scoring_place { get; set; }
        public string scoring_addInfo { get; set; }

        public string bidding_date { get; set; }
        public string bidding_place { get; set; }
        public string bidding_addInfo { get; set; }

        public string prequalification_date { get; set; }
        public string prequalification_place { get; set; }

        public string selecting_date { get; set; }
        public string selecting_place { get; set; }

        public string contracting_contractingTerm { get; set; }
        public string contracting_evadeConditions { get; set; }

        public string finalOpening_date { get; set; }
        public string finalOpening_place { get; set; }
        public string finalOpening_addInfo { get; set; }


        public bool IsEmpty
        {
            get
            {
                return collecting_startDate.IsNull() && collecting_place.IsNull() && collecting_order.IsNull() && collecting_endDate.IsNull() && collecting_addInfo.IsNull() &&
                collecting_form.IsNull() && opening_date.IsNull() && opening_place.IsNull() && opening_addInfo.IsNull() && scoring_date.IsNull() && scoring_place.IsNull() && scoring_addInfo.IsNull() &&
                bidding_date.IsNull() && bidding_place.IsNull() && bidding_addInfo.IsNull() && prequalification_date.IsNull() && prequalification_place.IsNull() && selecting_date.IsNull() &&
                selecting_place.IsNull() && contracting_contractingTerm.IsNull() && contracting_evadeConditions.IsNull() && finalOpening_date.IsNull() &&
                finalOpening_place.IsNull() && finalOpening_addInfo.IsNull();
            }
        }
    }

    public class Stage2
    {
        public string collecting_startDate { get; set; }
        public string collecting_place { get; set; }
        public string collecting_order { get; set; }
        public string collecting_endDate { get; set; }

        public string opening_date { get; set; }
        public string opening_place { get; set; }
        public string opening_addInfo { get; set; }

        public string scoring_date { get; set; }
        public string scoring_place { get; set; }
        public string scoring_addInfo { get; set; }

        public bool IsEmpty
        {
            get
            {
                return collecting_startDate.IsNull() && collecting_place.IsNull() && collecting_order.IsNull() && collecting_endDate.IsNull() && opening_date.IsNull() && opening_place.IsNull() &&
                opening_addInfo.IsNull() && scoring_date.IsNull() && scoring_place.IsNull() && scoring_addInfo.IsNull();
            }
        }
    }

    public class CustRequirement
    {
        public string cust_reg_num { get; set; }
        public string cust_name { get; set; }

        public List<Requirement> requirements { get; set; }

    }

    public class Requirement
    {
        public decimal? maxPrice { get; set; }
        public string deliveryPlace { get; set; }
        public string deliveryTerm { get; set; }
        public string onesideRejection { get; set; }
        public string addInfo { get; set; }

        public decimal? budgetFinancings_totalSum { get; set; }
        public decimal? nonbudgetFinancings_totalSum { get; set; }

        public string fz94_quantity { get; set; }
        public string fz94_financeSource { get; set; }
        public string fz94_paymentCondition { get; set; }

        public CustApplicationGuarantee applicationGuarantee { get; set; }
        public CustContractGuarantee contractGuarantee { get; set; }
    }

    public class CustApplicationGuarantee
    {
        public decimal? amount { get; set; }
        public string procedureInfo { get; set; }
        public string account_info { get; set; }

        public static CustApplicationGuarantee GetCustApplicationGuarantee(decimal? _amount, string _procedureInfo, string _account_info)
        {
            if (_amount == null && _procedureInfo.IsNull())
                return null;
            return new CustApplicationGuarantee
            {
                amount = _amount,
                procedureInfo = _procedureInfo,
                account_info = _account_info
            };
        }
    }

    public class CustContractGuarantee
    {
        public decimal? amount { get; set; }
        public string procedureInfo { get; set; }
        public string account_info { get; set; }

        public static CustContractGuarantee GetCustContractGuarantee(decimal? _amount, string _procedureInfo, string _account_info)
        {
            if (_amount == null && _procedureInfo.IsNull())
                return null;
            return new CustContractGuarantee
            {
                amount = _amount,
                procedureInfo = _procedureInfo,
                account_info = _account_info
            };
        }
    }

    public class Purchase
    {
        public string name { get; set; }
        public decimal? price { get; set; }
        public decimal? sum { get; set; }
        public string okdp { get; set; }
        public string okei { get; set; }
        public decimal? quantity { get; set; }
    }

    public class PurchaseDocumentation
    {
        public string grantStartDate { get; set; }
        public string grantPlace { get; set; }
        public string grantOrder { get; set; }
        public string languages { get; set; }
        public string grantMeans { get; set; }
        public string grantEndDate { get; set; }
        public PurchasePayInfo payInfo { get; set; }

        public static PurchaseDocumentation GetPurchaseDocumentation(string _grantStartDate, string _grantPlace, string _grantOrder, string _languages,
            string _grantMeans, string _grantEndDate)
        {
            if (_grantStartDate.IsNull() && _grantPlace.IsNull() && _grantOrder.IsNull() && _languages.IsNull() && _grantMeans.IsNull() && _grantEndDate.IsNull())
                return null;
            return new PurchaseDocumentation
            {
                grantStartDate = _grantStartDate,
                grantPlace = _grantPlace,
                grantOrder = _grantOrder,
                languages = _languages,
                grantMeans = _grantMeans,
                grantEndDate = _grantEndDate
            };
        }
    }

    public class PurchasePayInfo
    {
        public Decimal? amount { get; set; }
        public string currency { get; set; }
        public Decimal? part { get; set; }
        public string procedureInfo { get; set; }
        public string account_info { get; set; }
    }

    public class Contract44Data
    {
        public long id { get; set; }
        public int source_id { get; set; }
        public Contract44GeneralInfo gi { get; set; }
        public Contract44CustomerInfo ci { get; set; }
        public Contract44GeneralData gd { get; set; }
        public List<Contract44Budgetaties> bud { get; set; }
        public List<ExtraContract44Budgetaties> ebud { get; set; }
        public List<Contract44Product> products { get; set; }
        public List<Contract44Suplier> supliers { get; set; }
    }

    public class Contract44GeneralInfo
    {
        public string reg_num { get; set; }
        public string status { get; set; }
        public string not_num { get; set; }
        public int? lot_num { get; set; }
        public bool notification_exists { get; set; }
        public string place { get; set; }
        public string prot_date { get; set; }
        public string publish_date { get; set; }
        public string doc { get; set; }
    }

    public class Contract44CustomerInfo
    {
        public string fullname { get; set; }
        public string shortname { get; set; }
        public string ticker { get; set; }
        public string reg_date { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public string cust_code { get; set; }
        public string finace_source { get; set; }
        public string bud_level { get; set; }
        public string budget { get; set; }
        public string exta_budget { get; set; }
        public string budget_oktmo { get; set; }
    }

    public class Contract44GeneralData
    {
        public string sign_date { get; set; }
        public string contract_num { get; set; }
        public decimal? price { get; set; }
        public string currency { get; set; }
        public string exec_start_date { get; set; }
        public string exec_end_date { get; set; }
        public decimal? enforcement { get; set; }
    }

    public class Contract44BudgetatiesItem
    {
        public string kbk { get; set; }
        public string comments { get; set; }
        public decimal? price { get; set; }
    }

    public class Contract44Budgetaties
    {
        public string period { get; set; }
        public List<Contract44BudgetatiesItem> items { get; set; }
        public decimal? sum { get; set; }
    }

    public class ExtraContract44BudgetatiesItem
    {
        public string kosgu { get; set; }
        public decimal? price { get; set; }
    }

    public class ExtraContract44Budgetaties
    {
        public string period { get; set; }
        public List<ExtraContract44BudgetatiesItem> items { get; set; }
        public decimal? sum { get; set; }
    }

    public class Contract44Product
    {
        public string name { get; set; }
        public string code { get; set; }
        public string okei { get; set; }
        public decimal? price { get; set; }
        public decimal? quantity { get; set; }
        public decimal? sum { get; set; }
    }

    public class Contract44Suplier
    {
        public string name { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string address { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string ticker { get; set; }
        public string s_type { get; set; }
    }

    public class Contract223Data
    {
        public Guid id { get; set; }
        public Contract223GeneralInfo gi { get; set; }
        public Contract223CustomerInfo ci { get; set; }
        public Contract223PlacerInfo pi { get; set; }
        public Contract223SuplierInfo si { get; set; }
        public Contract223GeneralData gd { get; set; }
        public List<Contract223Product> products { get; set; }
    }

    public class Contract223GeneralInfo
    {
        public string reg_num { get; set; }
        public string status { get; set; }
        public string place { get; set; }
        public string purchase_name { get; set; }
        public string edition { get; set; }
        public string publish_date { get; set; }
    }

    public class Contract223CustomerInfo
    {
        public string fullname { get; set; }
        public string shortname { get; set; }
        public string ticker { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public string ogrn { get; set; }
        public string post_address { get; set; }
        public string legal_address { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Contract223PlacerInfo
    {
        public string fullname { get; set; }
        public string shortname { get; set; }
        public string ticker { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public string ogrn { get; set; }
        public string post_address { get; set; }
        public string legal_address { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Contract223SuplierInfo
    {
        public string name { get; set; }
        public string ticker { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public string ogrn { get; set; }
        public string type { get; set; }
    }

    public class Contract223GeneralData
    {
        public string crDate { get; set; }
        public string summa { get; set; }
        public string currency { get; set; }
        public string exe_date { get; set; }
        public string deliveryPlace { get; set; }
    }

    public class Contract223Product
    {
        public int? num { get; set; }
        public string okdp { get; set; }
        public string okved { get; set; }
        public string ei { get; set; }
        public decimal? qty { get; set; }
        public string additionalInfo { get; set; }
    }

    public class ZakupkiDetail
    {
        public string notification_id { get; set; } //id записи
        public string not_publish_date { get; set; }    //дата публикации (закупка)
        public string not_product { get; set; }     //Предмет закупки
        public string sourse_fz { get; set; }       //ФЗ
        public string pur_num { get; set; }         //Номер закупки
        public string lot_num { get; set; }         //Номер лота
        public string st_not_sum { get; set; }      //Начальная цена закупки
        public string dif_sum { get; set; }         //Изменение начальной цены закупки в рублях
        public string dif_per { get; set; }         //Изменение начальной цены закупки в процентах
        public string not_type { get; set; }      //Способ размещения закупки
        public string not_status_name { get; set; }      //Статус закупки
        public string not_cust { get; set; }      //Заказчик закупки
        public string not_part { get; set; }        //Участники закупки
        public string contr_pub_date { get; set; }      //дата публикации (контракт)
        public string contr_reg_num { get; set; }         //Номер контракта
        public string contr_sum { get; set; }         //Цена контракта в рублях
        public string contr_stage { get; set; }         //Статус контракта
        public string contr_placing { get; set; }         //Способ размещения контракта
        public string contr_product_list { get; set; }    //Предмет контракта
        public string contr_customer { get; set; }    //Заказчик по контракту
        public string contr_supliers { get; set; }    //Поставщик по контракту
    }


}