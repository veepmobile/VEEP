using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.BLL.Root;
using System.Data.SqlClient;
using Skrin.BLL.Infrastructure;
using System.Data;

namespace Skrin.BLL.Iss.Zakupki
{
    public static class ZakupkiRepository
    {
        

        public static async Task<List<ZakupkiStageGroup>> GetStageGroupsAsync()
        {
            Dictionary<byte, string> stage_group_ids = await GetStageGroupIdsAsync();
            List<ZakupkiStageGroup> groups = new List<ZakupkiStageGroup>();
            foreach (byte key in stage_group_ids.Keys)
            {
                ZakupkiStageGroup group = new ZakupkiStageGroup { name = stage_group_ids[key], statuses = new List<ZakupkiStageStatus>() };
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select id,name from ZakupkiZ.dbo.union_status where parent_id=@id", con);
                    cmd.Parameters.Add("@id", SqlDbType.TinyInt).Value = key;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        group.statuses.Add(new ZakupkiStageStatus
                        {
                            id = (byte)reader["id"],
                            name = (string)reader["name"]
                        });
                    }
                }
                groups.Add(group);
            }
            return groups;
        }

        private static async Task<Dictionary<byte, string>> GetStageGroupIdsAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                Dictionary<byte, string> ret = new Dictionary<byte, string>();
                SqlCommand cmd = new SqlCommand("select id,name from ZakupkiZ.dbo.union_status where parent_id=0", con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    ret.Add((byte)reader["id"], (string)reader["name"]);
                }
                return ret;
            }
        }

        public static async Task<string> GetCodeAsync(string ids, ZakupkiCodeType type, bool IsNot)
        {
            string sign = IsNot ? "!" : "";
            string sql = "";
            switch (type)
            {
                case ZakupkiCodeType.Okved:
                    sql = @"SELECT STUFF((SELECT ', " + sign + "'+replace(kod,'.','D') FROM  ( " +
                 "Select a.kod from searchdb2..okveds a inner join (Select *  from searchdb2.dbo.kodesplitter(0,'" + ids + "')) b " +
                 "on (a.parentid=b.kod and exists(select 1 from searchdb2..okveds c where c.id=b.kod and c.parentid=0)) or (b.kod=a.id and parentid!=0) " +
                 ") o ORDER BY o.kod FOR XML PATH('')),1,1,'')";
                    break;
                case ZakupkiCodeType.Okdp:
                    sql = @"SELECT STUFF((SELECT '| " + sign + "'+replace(code,'.','D') FROM  ( " +
                 "Select a.code from zakupki.dbo.Dic_Product_Codes a inner join (Select *  from searchdb2.dbo.kodesplitter(0,'" + ids + "')) b " +
                 "on (a.parent_id=b.kod and exists(select 1 from zakupki.dbo.Dic_Product_Codes c where c.id=b.kod and c.parent_id=0)) or (b.kod=a.id and parent_id!=0)  " +
                 ") o ORDER BY o.code FOR XML PATH('')),1,1,'')";
                    break;
                case ZakupkiCodeType.Region:
                    sql = @"SELECT STUFF((SELECT ','+right('00' + cast(kod as varchar(16)),2) FROM  ( " +
                         "Select a.kod from zakupki..vw_regions a inner join (Select *  from searchdb2.dbo.kodesplitter(0,'" + ids + "')) b " +
                         "on (a.parent_id=b.kod and exists(select 1 from zakupki..vw_regions c where c.id=b.kod and c.parent_id=0)) or (b.kod=a.id and parent_id!=0) " +
                         ") o ORDER BY kod FOR XML PATH('')),1,1,'')";
                    break;
            }
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                return (string)await cmd.ExecuteScalarAsync();
            }
        }

        #region notification

        public static async Task<NotificationData> GetShortNotDataAsync(string pur_num, int lot_num)
        {
            NotificationData not = null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                if (lot_num == 0)
                    lot_num = 1;
                string sql = @"select
                                cn.lot_id,
                                cn.notification_id not_id,
                                n.href not_href,
                                nt.ShortDesc not_type,
                                etp.name etp_name,
                                etp.url etp_url,
                                isnull(cn.shortName,cn.fullName) customer_name,
                                us.ticker customer_ticker,
                                cn.purchaseObjectInfo,
                                zcs.name status_name,
                                l.maxPrice,
                                l.financeSource,
                                cur.name currency,
                                l.purchaseObjects_totalSum
                                from ZakupkiZ.dbo.cons_notification cn
                                left join ZakupkiZ.dbo.notification n on cn.notification_id = n.id
                                left join ZakupkiZ.dbo.notificationTypes nt on nt.notificationType=cn.notificationType
                                left join ZakupkiZ.dbo.ETP etp on etp.code=cn.ETP_code
                                left join searchdb2..union_search us on us.inn=cn.inn and us.uniq_inn=1
                                left join ZakupkiZ.dbo.status zcs on zcs.id=cn.status_id
                                left join ZakupkiZ.dbo.lot l on cn.lot_id=l.id
                                left join ZakupkiZ.dbo.nsiCurrency cur on cur.code=l.currency_code
                                where cn.purchaseNumber=@pur_num and cn.lotNumber=@lot_num";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 600;
                cmd.Parameters.Add("@pur_num", SqlDbType.VarChar, 19).Value = pur_num;
                cmd.Parameters.Add("@lot_num", SqlDbType.Int).Value = lot_num;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        long lot_id = (long)reader["lot_id"];
                        long not_id = (long)reader["not_id"];
                        not = new NotificationData
                        {
                            lot_num = lot_num,
                            pur_num = pur_num,
                            not_href = (string)reader.ReadNullIfDbNull("not_href"),
                            not_type = (string)reader.ReadNullIfDbNull("not_type"),
                            etp_name = (string)reader.ReadNullIfDbNull("etp_name"),
                            etp_url = (string)reader.ReadNullIfDbNull("etp_url"),
                            purchaseObjectInfo = (string)reader.ReadNullIfDbNull("purchaseObjectInfo"),
                            status_name = (string)reader.ReadNullIfDbNull("status_name"),
                            customer = new NotCustomer
                            {
                                name = (string)reader.ReadNullIfDbNull("customer_name"),
                                ticker = (string)reader.ReadNullIfDbNull("customer_ticker")
                            },
                            maxPrice = (decimal?)reader.ReadNullIfDbNull("maxPrice"),
                            financeSource = (string)reader.ReadNullIfDbNull("financeSource"),
                            currency = (string)reader.ReadNullIfDbNull("currency"),
                            totalSum = (decimal?)reader.ReadNullIfDbNull("purchaseObjects_totalSum"),
                            purchases = await _GetPurchase(lot_id),
                            contracts = await _GetContracts(pur_num, lot_num),
                            other_lots = await _GetOtherLots(pur_num, lot_num)
                        };

                    }
                }
            }
            return not;
        }


        public static async Task<NotificationData> GetNotDataAsync(string pur_num, int lot_num)
        {
            NotificationData not = null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                if (lot_num == 0)
                    lot_num = 1;
                string sql = @"select
                            cn.lot_id,
                            cn.notification_id not_id,
                            n.href not_href,
                            nt.ShortDesc not_type,
                            etp.name etp_name,
                            etp.url etp_url,
                            isnull(cn.shortName,cn.fullName) customer_name,
                            us.ticker customer_ticker,
                            cn.purchaseObjectInfo,
                            zcs.name status_name,
                            cn.responsibleInfo_orgPostAddress cust_post_address,
                            cn.responsibleInfo_orgFactAddress cust_legal_address,
                            isnull(cn.contactPerson_lastName+' ','')+isnull(cn.contactPerson_firstName+' ','')+isnull(cn.contactPerson_middleName,'') cust_contact,
                            cn.responsibleInfo_contactEMail cust_email,
                            cn.responsibleInfo_contactPhone cust_phone,
                            cn.responsibleInfo_contactFax cust_fax,
                            cn.responsibleInfo_addInfo cust_add_info,
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_collecting_startDate) stageOne_collecting_startDate, 
	                            pri.stageOne_collecting_place, pri.stageOne_collecting_order, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_collecting_endDate) stageOne_collecting_endDate, 
	                            pri.stageOne_collecting_addInfo, pri.stageOne_collecting_form, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_opening_date) stageOne_opening_date, 
	                            pri.stageOne_opening_place, pri.stageOne_opening_addInfo, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_scoring_date) stageOne_scoring_date, 
	                            pri.stageOne_scoring_place, pri.stageOne_scoring_addInfo, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_bidding_date)stageOne_bidding_date, 
	                            pri.stageOne_bidding_place, pri.stageOne_bidding_addInfo, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_prequalification_date)stageOne_prequalification_date, 
	                            pri.stageOne_prequalification_place, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_selecting_date)stageOne_selecting_date, 
	                            pri.stageOne_selecting_place, pri.stageOne_contracting_contractingTerm, pri.stageOne_contracting_evadeConditions, 
	                            ZakupkiZ.dbo.date_convert(pri.stageOne_finalOpening_date)stageOne_finalOpening_date, 
	                            pri.stageOne_finalOpening_place, pri.stageOne_finalOpening_addInfo, 
	                            ZakupkiZ.dbo.date_convert(pri.stageTwo_collecting_startDate) stageTwo_collecting_startDate,
	                            pri.stageTwo_collecting_place, pri.stageTwo_collecting_order, 
	                            ZakupkiZ.dbo.date_convert(pri.stageTwo_collecting_endDate) stageTwo_collecting_endDate, 
	                            ZakupkiZ.dbo.date_convert(pri.stageTwo_opening_date) stageTwo_opening_date, 
	                            pri.stageTwo_opening_place, pri.stageTwo_opening_addInfo, 
	                            ZakupkiZ.dbo.date_convert(pri.stageTwo_scoring_date) stageTwo_scoring_date, 
	                            pri.stageTwo_scoring_place, pri.stageTwo_scoring_addInfo,
                            l.maxPrice,
                            l.financeSource,
                            cur.name currency,
                            l.purchaseObjects_totalSum,
                            STUFF((select distinct '|'+isnull(p.name,'')from  ZakupkiZ.dbo.preferense p
                            where p.lot_id=cn.lot_id for xml path('')), 1, 1, '') preference,
                            STUFF((select distinct '|'+isnull(p.name,'')+ isnull('|'+p.content,'')from  ZakupkiZ.dbo.requirement p
                            where p.lot_id=cn.lot_id for xml path('')), 1, 1, '') requirement,
                            l.restrictInfo 
                            from ZakupkiZ.dbo.cons_notification cn
                            left join ZakupkiZ.dbo.notification n on cn.notification_id = n.id
                            left join ZakupkiZ.dbo.notificationTypes nt on nt.notificationType=cn.notificationType
                            left join ZakupkiZ.dbo.ETP etp on etp.code=cn.ETP_code
                            left join searchdb2..union_search us on us.inn=cn.inn and us.uniq_inn=1
                            left join ZakupkiZ.dbo.status zcs on zcs.id=cn.status_id
                            left join ZakupkiZ.dbo.procedureInfo pri on pri.notification_id=cn.notification_id 
                            left join ZakupkiZ.dbo.lot l on cn.lot_id=l.id
                            left join ZakupkiZ.dbo.nsiCurrency cur on cur.code=l.currency_code
                            where cn.purchaseNumber=@pur_num and isnull(cn.lotNumber,1)=@lot_num";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 600;
                cmd.Parameters.Add("@pur_num", SqlDbType.VarChar, 19).Value = pur_num;
                cmd.Parameters.Add("@lot_num", SqlDbType.Int).Value = lot_num;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        long lot_id = (long)reader["lot_id"];
                        long not_id = (long)reader["not_id"];
                        not = new NotificationData
                        {
                            lot_num = lot_num,
                            pur_num = pur_num,
                            not_href = (string)reader.ReadNullIfDbNull("not_href"),
                            not_type = (string)reader.ReadNullIfDbNull("not_type"),
                            etp_name = (string)reader.ReadNullIfDbNull("etp_name"),
                            etp_url = (string)reader.ReadNullIfDbNull("etp_url"),
                            purchaseObjectInfo = (string)reader.ReadNullIfDbNull("purchaseObjectInfo"),
                            status_name = (string)reader.ReadNullIfDbNull("status_name"),
                            customer = new NotCustomer
                            {
                                name = (string)reader.ReadNullIfDbNull("customer_name"),
                                ticker = (string)reader.ReadNullIfDbNull("customer_ticker"),
                                post_address = (string)reader.ReadNullIfDbNull("cust_post_address"),
                                legal_address = (string)reader.ReadNullIfDbNull("cust_legal_address"),
                                contact = (string)reader.ReadNullIfDbNull("cust_contact"),
                                email = (string)reader.ReadNullIfDbNull("cust_email"),
                                phone = (string)reader.ReadNullIfDbNull("cust_phone"),
                                fax = (string)reader.ReadNullIfDbNull("cust_fax"),
                                add_info = (string)reader.ReadNullIfDbNull("cust_add_info")
                            },
                            maxPrice = (decimal?)reader.ReadNullIfDbNull("maxPrice"),
                            financeSource = (string)reader.ReadNullIfDbNull("financeSource"),
                            currency = (string)reader.ReadNullIfDbNull("currency"),
                            totalSum = (decimal?)reader.ReadNullIfDbNull("purchaseObjects_totalSum"),
                            preference = (string)reader.ReadNullIfDbNull("preference"),
                            requirement = (string)reader.ReadNullIfDbNull("requirement"),
                            restrictInfo = (string)reader.ReadNullIfDbNull("restrictInfo"),
                            stage1 = _GetStage1(reader),
                            stage2 = _GetStage2(reader),
                            custRequirements =await _GetRequirement(lot_id),
                            purchases = await _GetPurchase(lot_id),
                            purchaseDocumentation = await _GetDocumentation(not_id),
                            contracts = await _GetContracts(pur_num, lot_num),
                            other_lots = await _GetOtherLots(pur_num, lot_num)
                        };

                    }
                }
            }
            return not;
        }


        private static Stage1 _GetStage1(IDataReader reader)
        {
            Stage1 stage1 = new Stage1
            {
                collecting_startDate = (string)reader.ReadNullIfDbNull("stageOne_collecting_startDate"),
                collecting_place = (string)reader.ReadNullIfDbNull("stageOne_collecting_place"),
                collecting_order = (string)reader.ReadNullIfDbNull("stageOne_collecting_order"),
                collecting_endDate = (string)reader.ReadNullIfDbNull("stageOne_collecting_endDate"),
                collecting_addInfo = (string)reader.ReadNullIfDbNull("stageOne_collecting_addInfo"),
                collecting_form = (string)reader.ReadNullIfDbNull("stageOne_collecting_form"),
                opening_date = (string)reader.ReadNullIfDbNull("stageOne_opening_date"),
                opening_addInfo = (string)reader.ReadNullIfDbNull("stageOne_opening_addInfo"),
                opening_place = (string)reader.ReadNullIfDbNull("stageOne_opening_place"),
                scoring_date = (string)reader.ReadNullIfDbNull("stageOne_scoring_date"),
                scoring_place = (string)reader.ReadNullIfDbNull("stageOne_scoring_place"),
                scoring_addInfo = (string)reader.ReadNullIfDbNull("stageOne_scoring_addInfo"),
                bidding_date = (string)reader.ReadNullIfDbNull("stageOne_bidding_date"),
                bidding_place = (string)reader.ReadNullIfDbNull("stageOne_bidding_place"),
                bidding_addInfo = (string)reader.ReadNullIfDbNull("stageOne_bidding_addInfo"),
                prequalification_date = (string)reader.ReadNullIfDbNull("stageOne_prequalification_date"),
                prequalification_place = (string)reader.ReadNullIfDbNull("stageOne_prequalification_place"),
                selecting_date = (string)reader.ReadNullIfDbNull("stageOne_selecting_date"),
                selecting_place = (string)reader.ReadNullIfDbNull("stageOne_selecting_place"),
                contracting_contractingTerm = (string)reader.ReadNullIfDbNull("stageOne_contracting_contractingTerm"),
                contracting_evadeConditions = (string)reader.ReadNullIfDbNull("stageOne_contracting_evadeConditions"),
                finalOpening_date = (string)reader.ReadNullIfDbNull("stageOne_finalOpening_date"),
                finalOpening_place = (string)reader.ReadNullIfDbNull("stageOne_finalOpening_place"),
                finalOpening_addInfo = (string)reader.ReadNullIfDbNull("stageOne_finalOpening_addInfo")
            };
            return stage1.IsEmpty ? null : stage1;
        }

        private static Stage2 _GetStage2(IDataReader reader)
        {
            Stage2 stage2 = new Stage2
            {
                collecting_startDate = (string)reader.ReadNullIfDbNull("stageTwo_collecting_startDate"),
                collecting_place = (string)reader.ReadNullIfDbNull("stageTwo_collecting_place"),
                collecting_order = (string)reader.ReadNullIfDbNull("stageTwo_collecting_order"),
                collecting_endDate = (string)reader.ReadNullIfDbNull("stageTwo_collecting_endDate"),
                opening_date = (string)reader.ReadNullIfDbNull("stageTwo_opening_date"),
                opening_addInfo = (string)reader.ReadNullIfDbNull("stageTwo_opening_addInfo"),
                opening_place = (string)reader.ReadNullIfDbNull("stageTwo_opening_place"),
                scoring_date = (string)reader.ReadNullIfDbNull("stageTwo_scoring_date"),
                scoring_place = (string)reader.ReadNullIfDbNull("stageTwo_scoring_place"),
                scoring_addInfo = (string)reader.ReadNullIfDbNull("stageTwo_scoring_addInfo")
            };
            return stage2.IsEmpty ? null : stage2;
        }

        private static async Task<List<CustRequirement>> _GetRequirement(long lot_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                List<CustRequirement> reqs = new List<CustRequirement>();
                string sql = @"select distinct cr.customer_regNum reg_num,isnull(org.shortName,org.fullName) name
                                from ZakupkiZ.dbo.customerRequirement cr 
                                join ZakupkiZ.dbo.nsiOrganization org on org.regNumber=cr.customer_regNum
                                where cr.lot_id=@lot_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@lot_id", SqlDbType.BigInt).Value = lot_id;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string reg_num = (string)reader.ReadNullIfDbNull("reg_num");
                    if (!reg_num.IsNull())
                    {
                        reqs.Add(new CustRequirement
                        {
                            cust_reg_num = reg_num,
                            cust_name = (string)reader.ReadNullIfDbNull("name"),
                            requirements = await _GetExtraFields(reg_num, lot_id)
                        });
                    }
                }
                return reqs;
            }
        }

        private static async Task<List<Requirement>> _GetExtraFields(string reg_num, long lot_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 
                            maxPrice,
                            isnull(deliveryPlace,
                            STUFF((select distinct '|'+ISNULL(kl.fullName+', ','')+ isnull(k.deliveryPlace, ' ') from  ZakupkiZ.dbo.kladrPlace k
                            left JOIN ZakupkiZ..KLADR kl on kl.kladrCode=k.kladr_kladrCode
                            where k.customerRequirement_id=cr.id for xml path('')), 1, 1, '')) deliveryPlace,
                            deliveryTerm,
                            onesideRejection,
                            addInfo,
                            budgetFinancings_totalSum,
                            nonbudgetFinancings_totalSum,
                            applicationGuarantee_amount,
                            applicationGuarantee_procedureInfo,
                            isnull('Р/с: '+applicationGuarantee_settlementAccount+', ','') +
                            isnull('Л/с: '+applicationGuarantee_personalAccount+', ','') +
                            isnull('БИК: '+applicationGuarantee_bik,'') applicationGuarantee_account_info,
                            contractGuarantee_amount,
                            contractGuarantee_procedureInfo,
                            isnull('Р/с: '+contractGuarantee_settlementAccount+', ','') +
                            isnull('Л/с: '+contractGuarantee_personalAccount+', ','') +
                            isnull('БИК: '+contractGuarantee_bik,'') contractGuarantee_account_info,
                            fz94_quantity, 
                            fz94_financeSource, 
                            fz94_paymentCondition
                            from ZakupkiZ.dbo.customerRequirement cr
                            where lot_id=@lot_id and customer_regNum=@reg_num";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@reg_num", SqlDbType.VarChar, 19).Value = reg_num;
                cmd.Parameters.Add("@lot_id", SqlDbType.BigInt).Value = lot_id;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                List<Requirement> reqs = new List<Requirement>();
                while (await reader.ReadAsync())
                {
                    reqs.Add(new Requirement
                    {
                        maxPrice = (decimal?)reader.ReadNullIfDbNull("maxPrice"),
                        deliveryPlace = (string)reader.ReadNullIfDbNull("deliveryPlace"),
                        deliveryTerm = (string)reader.ReadNullIfDbNull("deliveryTerm"),
                        onesideRejection = (string)reader.ReadNullIfDbNull("onesideRejection"),
                        addInfo = (string)reader.ReadNullIfDbNull("addInfo"),
                        budgetFinancings_totalSum = (decimal?)reader.ReadNullIfDbNull("budgetFinancings_totalSum"),
                        nonbudgetFinancings_totalSum = (decimal?)reader.ReadNullIfDbNull("nonbudgetFinancings_totalSum"),
                        fz94_quantity = (string)reader.ReadNullIfDbNull("fz94_quantity"),
                        fz94_financeSource = (string)reader.ReadNullIfDbNull("fz94_financeSource"),
                        fz94_paymentCondition = (string)reader.ReadNullIfDbNull("fz94_paymentCondition"),
                        applicationGuarantee = CustApplicationGuarantee.GetCustApplicationGuarantee((decimal?)reader.ReadNullIfDbNull("applicationGuarantee_amount"),
                        (string)reader.ReadNullIfDbNull("applicationGuarantee_procedureInfo"), (string)reader.ReadNullIfDbNull("applicationGuarantee_account_info")),
                        contractGuarantee = CustContractGuarantee.GetCustContractGuarantee((decimal?)reader.ReadNullIfDbNull("contractGuarantee_amount"),
                        (string)reader.ReadNullIfDbNull("contractGuarantee_procedureInfo"), (string)reader.ReadNullIfDbNull("contractGuarantee_account_info"))
                    });
                }
                return reqs;
            }
        }

        private static async Task<PurchaseDocumentation> _GetDocumentation(long not_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 
                            ZakupkiZ.dbo.date_convert(n.purchaseDocumentation_grantStartDate) purchaseDocumentation_grantStartDate, 
                            n.purchaseDocumentation_grantPlace, 
                            n.purchaseDocumentation_grantOrder, 
                            n.purchaseDocumentation_languages, 
                            n.purchaseDocumentation_grantMeans, 
                            ZakupkiZ.dbo.date_convert(n.purchaseDocumentation_grantEndDate) purchaseDocumentation_grantEndDate, 
                            n.payInfo_amount, 
                            c.name currency, 
                            n.payInfo_part, 
                            n.payInfo_procedureInfo, 
                            isnull('Р/с: '+n.payInfo_settlementAccount+', ','') +
                            isnull('Л/с: '+n.payInfo_personalAccount+', ','') +
                            isnull('БИК: '+n.payInfo_bik,'') account_info
                            from ZakupkiZ.dbo.notification n
                            left join ZakupkiZ.dbo.nsiCurrency c on n.payInfo_payCurrency_code=c.code
                            where id=@not_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@not_id", SqlDbType.BigInt).Value = not_id;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                PurchaseDocumentation doc = null;
                if (await reader.ReadAsync())
                {
                    doc = PurchaseDocumentation.GetPurchaseDocumentation((string)reader.ReadNullIfDbNull("purchaseDocumentation_grantStartDate"), (string)reader.ReadNullIfDbNull("purchaseDocumentation_grantPlace"),
                        (string)reader.ReadNullIfDbNull("purchaseDocumentation_grantOrder"), (string)reader.ReadNullIfDbNull("purchaseDocumentation_languages"),
                        (string)reader.ReadNullIfDbNull("purchaseDocumentation_grantMeans"), (string)reader.ReadNullIfDbNull("purchaseDocumentation_grantEndDate"));
                    if (doc != null)
                    {
                        Decimal? payInfo_amount = (Decimal?)reader.ReadNullIfDbNull("payInfo_amount");
                        if (payInfo_amount != null)
                        {
                            doc.payInfo = new PurchasePayInfo
                            {
                                amount = payInfo_amount,
                                currency = (string)reader.ReadNullIfDbNull("currency"),
                                part = (Decimal?)reader.ReadNullIfDbNull("payInfo_part"),
                                procedureInfo = (string)reader.ReadNullIfDbNull("payInfo_procedureInfo"),
                                account_info = (string)reader.ReadNullIfDbNull("account_info")
                            };
                        }
                    }
                }
                return doc;
            }
        }

        private static async Task<List<Purchase>> _GetPurchase(long lot_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 
                                p.name,
                                p.price,
                                p.sum,
                                p.OKPD_code,
                                o.localName,
                                isnull(quantity_value,1) quantity
                                from ZakupkiZ.dbo.purchaseObject p
                                left join ZakupkiZ.dbo.nsiOKEI o on p.OKEI_code=o.code
                                where lot_id=@lot_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@lot_id", SqlDbType.BigInt).Value = lot_id;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                List<Purchase> pur = new List<Purchase>();
                while (await reader.ReadAsync())
                {
                    pur.Add(new Purchase
                    {
                        name = (string)reader.ReadNullIfDbNull("name"),
                        price = (decimal?)reader.ReadNullIfDbNull("price"),
                        sum = (decimal?)reader.ReadNullIfDbNull("sum"),
                        okdp = (string)reader.ReadNullIfDbNull("OKPD_code"),
                        okei = (string)reader.ReadNullIfDbNull("localName"),
                        quantity = (decimal?)reader.ReadNullIfDbNull("quantity")
                    });
                }
                return pur;
            }
        }

        private static async Task<List<ShortContractInfo>> _GetContracts(string pur_num, int lot_num)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Zakupki..get_contracts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_num", SqlDbType.VarChar).Value = pur_num;
                cmd.Parameters.Add("@l_num", SqlDbType.Int).Value = lot_num;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                List<ShortContractInfo> contracts = new List<ShortContractInfo>();
                while (await reader.ReadAsync())
                {
                    contracts.Add(new ShortContractInfo
                    {
                        id = (long)reader["id"],
                        pub_date = (string)reader.ReadNullIfDbNull("pub_date"),
                        reg_num = (string)reader.ReadNullIfDbNull("regNum"),
                        price = (string)reader.ReadNullIfDbNull("price"),
                        stage = (string)reader.ReadNullIfDbNull("contr_stage"),
                        cust_name = (string)reader.ReadNullIfDbNull("customer_name"),
                        cust_ticker = (string)reader.ReadNullIfDbNull("customer_ticker"),
                        placing = (string)reader.ReadNullIfDbNull("placing"),
                        supliers = (string)reader.ReadNullIfDbNull("suppliers"),
                        contract_source = (int)reader.ReadNullIfDbNull("contract_source")
                    });
                }
                return contracts;
            }
        }

        private static async Task<List<ShortLortInfo>> _GetOtherLots(string pur_num, int lot_num)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 
                                cn.lotNumber,
                                l.maxPrice,
                                zakupkiZ.dbo.text_cut(STUFF((select ', '+isnull(p.name,'') from ZakupkiZ.dbo.purchaseObject p where p.lot_id=l.id for xml path('')), 1, 1, ''),150,
                                '...,Всего: '+cast((select count(1) from ZakupkiZ.dbo.purchaseObject p where p.lot_id=l.id)as varchar(50))) lotInfo
                                from ZakupkiZ.dbo.cons_notification cn
                                join ZakupkiZ.dbo.lot l on cn.lot_id=l.id 
                                where cn.purchaseNumber=@p_num and cn.lotNumber!=@l_num
                                order by cn.lotNumber";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@p_num", SqlDbType.VarChar).Value = pur_num;
                cmd.Parameters.Add("@l_num", SqlDbType.Int).Value = lot_num;
                cmd.CommandTimeout = 600;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                List<ShortLortInfo> lots = new List<ShortLortInfo>();
                while (await reader.ReadAsync())
                {
                    lots.Add(new ShortLortInfo
                    {
                        lot_number = (int)reader["lotNumber"],
                        max_price = (decimal?)reader.ReadNullIfDbNull("maxPrice"),
                        lot_info = (string)reader.ReadNullIfDbNull("lotInfo")
                    });
                }
                return lots;
            }
        }

        #endregion

        #region contract_44

        public static async Task<Contract44Data> GetCont44DataAsync(long id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select 
                                        contract_source= 
                                        case when c.source_id=1 then 1
	                                        else 
		                                        case when c.c44_placing_type in(1,3) then 3 --Контракт по результатам процедуры определения поставщика по закупке (44ФЗ)
		                                        else 1 end
                                        end,
                                        c.regNum,
                                        st.name status,
                                        c.notificationNumber not_num,
                                        c.lotNumber lot_num,
                                        not_ex=case when exists (select 1 from ZakupkiZ.dbo.cons_notification where purchaseNumber=c.notificationNumber and lotNumber=c.lotNumber) then 1 else 0 end,
                                        'placing'=case when isnull(cp.name,p.name) is not null then isnull(cp.name,p.name) when c.singleCustomer=1 then 'Единственный поставщик' else '' end,
                                        convert(varchar(20),c.protocolDate,104) prot_date,
                                        convert(varchar(20),c.publishDate,104) publ_date,
                                        c.documentBase doc,
                                        cus.fullName,
                                        cus.shortName,
                                        us1.ticker,
                                        convert(varchar(20),cus.regDate,104) cus_reg_date,
                                        cus.inn,
                                        cus.kpp,
                                        c.customer_code,
                                        c.financeSource,
                                        c.budget_oktmo+' - '+ok.fullName budget_oktmo,
                                        bud.name budget,
                                        ebud.name extrabudget,
                                        bl.name budgetLevel,
                                        convert(varchar(20),c.signDate,104) sign_date,
                                        c.number contr_num,
                                        c.price,
                                        cur.name+' ('+c.currency+')' currency,
                                        convert(varchar(20),c.exec_start,104) exec_start,
                                        convert(varchar(20),c.exec_end,104) exec_end,
                                        ISNULL(ce.amountRur,ce.amount) enforcement 

                                        from Zakupki.dbo.contracts c
                                        left join Zakupki.dbo.currentContractStages st on c.currentContractStage=st.code
                                        left join Zakupki.dbo.c44_placings cp on cp.id=c.c44_placing and c.source_id=3
                                        left join Zakupki.dbo.placings p on p.id=c.placing and c.source_id=1
                                        left join Zakupki.dbo.customers cus on c.customer=cus.regNum 
                                        left join  searchdb2.dbo.union_search us1 on cus.inn=us1.inn  and uniq_inn=1
                                        left join ZakupkiZ.dbo.nsiOKTMO ok on ok.code=c.budget_oktmo
                                        left join Zakupki.dbo.budgets bud on c.budget=bud.code
                                        left join Zakupki.dbo.extrabudgets ebud on c.extrabudget=ebud.code
                                        left join Zakupki.dbo.currencies cur on cur.code=c.currency
                                        left join Zakupki.dbo.budgetLevels bl on bl.code=c.budgetLevel
                                        left JOIN Zakupki.dbo.contract_enforcement ce on ce.contract_id=c.id
                                        where c.id=@contract_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 600;
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                Contract44Data cn = null;
                if (await reader.ReadAsync())
                {
                    cn = new Contract44Data
                    {
                        id = id,
                        source_id = (int)reader.ReadNullIfDbNull("contract_source"),
                        gi = new Contract44GeneralInfo
                        {
                            reg_num = (string)reader.ReadNullIfDbNull("regNum"),
                            status = (string)reader.ReadNullIfDbNull("status"),
                            not_num = (string)reader.ReadNullIfDbNull("not_num"),
                            lot_num = (int?)reader.ReadNullIfDbNull("lot_num"),
                            notification_exists = (int)reader["not_ex"] == 1,
                            place = (string)reader.ReadNullIfDbNull("placing"),
                            prot_date = (string)reader.ReadNullIfDbNull("prot_date"),
                            publish_date = (string)reader.ReadNullIfDbNull("publ_date"),
                            doc = (string)reader.ReadNullIfDbNull("doc")
                        },
                        ci = new Contract44CustomerInfo
                        {
                            fullname = (string)reader.ReadNullIfDbNull("fullName"),
                            shortname = (string)reader.ReadNullIfDbNull("shortName"),
                            ticker = (string)reader.ReadNullIfDbNull("ticker"),
                            reg_date = (string)reader.ReadNullIfDbNull("cus_reg_date"),
                            inn = (string)reader.ReadNullIfDbNull("inn"),
                            kpp = (string)reader.ReadNullIfDbNull("kpp"),
                            cust_code = (string)reader.ReadNullIfDbNull("customer_code"),
                            finace_source = (string)reader.ReadNullIfDbNull("financeSource"),
                            budget_oktmo = (string)reader.ReadNullIfDbNull("budget_oktmo"),
                            budget = (string)reader.ReadNullIfDbNull("budget"),
                            exta_budget = (string)reader.ReadNullIfDbNull("extrabudget"),
                            bud_level = (string)reader.ReadNullIfDbNull("budgetLevel")
                        },
                        gd = new Contract44GeneralData
                        {
                            sign_date = (string)reader.ReadNullIfDbNull("sign_date"),
                            contract_num = (string)reader.ReadNullIfDbNull("contr_num"),
                            price = (decimal?)reader.ReadNullIfDbNull("price"),
                            currency = (string)reader.ReadNullIfDbNull("currency"),
                            exec_start_date = (string)reader.ReadNullIfDbNull("exec_start"),
                            exec_end_date = (string)reader.ReadNullIfDbNull("exec_end"),
                            enforcement = (decimal?)reader.ReadNullIfDbNull("enforcement")
                        },
                        bud = await _GetCont44Buds(id),
                        ebud = await _GetExtCont44Buds(id),
                        supliers = await _GetContract44Supliers(id),
                        products = await _GetContract44Products(id)
                    };
                }
                return cn;
            }
        }


        private static async Task<List<Contract44Budgetaties>> _GetCont44Buds(long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year)) per,sum(price) sum from Zakupki.dbo.budgetaries
                                   where contract_id=@contract_id
                                  group by isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))
                                  order by isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<Contract44Budgetaties> buds = new List<Contract44Budgetaties>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    DateTime? per = (DateTime?)reader.ReadNullIfDbNull("per");
                    if (per != null)
                    {
                        buds.Add(new Contract44Budgetaties
                        {
                            period = per.Value.ToShortDateString(),
                            sum = (decimal?)reader.ReadNullIfDbNull("sum"),
                            items = await _GetCont44BudsItem(per.Value, contract_id)
                        });
                    }
                }
                return buds;
            }
        }

        private static async Task<List<Contract44BudgetatiesItem>> _GetCont44BudsItem(DateTime per, long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select KBK,comment, price from Zakupki.dbo.budgetaries
                      where contract_id=@contract_id and isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))=@per
                      order by KBK";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@per", SqlDbType.DateTime).Value = per;
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<Contract44BudgetatiesItem> items = new List<Contract44BudgetatiesItem>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    items.Add(new Contract44BudgetatiesItem
                    {
                        kbk = (string)reader.ReadNullIfDbNull("KBK"),
                        comments = (string)reader.ReadNullIfDbNull("comment"),
                        price = (decimal?)reader.ReadNullIfDbNull("price")
                    });
                }
                return items;
            }
        }


        private static async Task<List<ExtraContract44Budgetaties>> _GetExtCont44Buds(long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @" select isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year)) per,sum(price) sum from Zakupki.dbo.extrabudgetaries
                                   where contract_id=@contract_id
                                  group by isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))
                                  order by isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<ExtraContract44Budgetaties> ebuds = new List<ExtraContract44Budgetaties>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    DateTime? per = (DateTime?)reader.ReadNullIfDbNull("per");
                    if (per != null)
                    {
                        ebuds.Add(new ExtraContract44Budgetaties
                        {
                            period = per.Value.ToShortDateString(),
                            sum = (decimal?)reader.ReadNullIfDbNull("sum"),
                            items = await _GetExtCont44BudsItem(per.Value, contract_id)
                        });
                    }
                }
                return ebuds;
            }
        }

        private static async Task<List<ExtraContract44BudgetatiesItem>> _GetExtCont44BudsItem(DateTime per, long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select KOSGU, price from Zakupki.dbo.extrabudgetaries
                      where contract_id=@contract_id and isnull(end_date,Zakupki.dbo.get_last_day_of_month(month,year))=@per
                      order by KOSGU";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@per", SqlDbType.DateTime).Value = per;
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<ExtraContract44BudgetatiesItem> items = new List<ExtraContract44BudgetatiesItem>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    items.Add(new ExtraContract44BudgetatiesItem
                    {
                        kosgu = (string)reader.ReadNullIfDbNull("KOSGU"),
                        price = (decimal?)reader.ReadNullIfDbNull("price")
                    });
                }
                return items;
            }
        }

        private static async Task<List<Contract44Suplier>> _GetContract44Supliers(long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select s.organizationName name,s.country_name, s.country,s.factualAddress postAddress, s.contactPhone,
	                         s.inn,s.kpp,s.status_name,s.contactEmail,ISNULL(us2.ticker,g1.okpo) ticker,  
	                         s_type=case when s.participantType in('U','UF') then 'U' ELSE 'P' END
	                         from zakupki.dbo.vw_suppliers s
	                         left join  searchdb2.dbo.union_search us2 on s.inn=us2.inn  and uniq_inn=1 and s.participantType in('U','UF')
	                         left join gks_ip.dbo.GKS_IPS g1 on g1.inn=s.inn and s.participantType in('P','PF')
	                         where s.contract_id=@contract_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<Contract44Suplier> supl = new List<Contract44Suplier>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    supl.Add(new Contract44Suplier
                    {
                        name = (string)reader.ReadNullIfDbNull("name"),
                        country_code = (string)reader.ReadNullIfDbNull("country"),
                        country_name = (string)reader.ReadNullIfDbNull("country_name"),
                        address = (string)reader.ReadNullIfDbNull("postAddress"),
                        phone = (string)reader.ReadNullIfDbNull("contactPhone"),
                        email = (string)reader.ReadNullIfDbNull("contactEmail"),
                        inn = (string)reader.ReadNullIfDbNull("inn"),
                        kpp = (string)reader.ReadNullIfDbNull("kpp"),
                        status = (string)reader.ReadNullIfDbNull("status_name"),
                        ticker = (string)reader.ReadNullIfDbNull("ticker"),
                        s_type = (string)reader.ReadNullIfDbNull("s_type")
                    });
                }
                return supl;
            }
        }

        private static async Task<List<Contract44Product>> _GetContract44Products(long contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"select name, OKDP okdp,okei,price,quantity,sum from zakupki.dbo.vw_products prod
	                        where prod.contract_id=@contract_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@contract_id", SqlDbType.BigInt).Value = contract_id;
                con.Open();
                List<Contract44Product> prods = new List<Contract44Product>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    prods.Add(new Contract44Product
                    {
                        name = (string)reader.ReadNullIfDbNull("name"),
                        code = (string)reader.ReadNullIfDbNull("okdp"),
                        okei = (string)reader.ReadNullIfDbNull("okei"),
                        price = (decimal?)reader.ReadNullIfDbNull("price"),
                        quantity = (decimal?)reader.ReadNullIfDbNull("quantity"),
                        sum = (decimal?)reader.ReadNullIfDbNull("sum")
                    });
                }
                return prods;
            }
        }

        #endregion

        #region contract_223

        public static async Task<Contract223Data> GetCont223DataAsync(Guid id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"SELECT
                                pc.purchaseNoticeNumber reg_num,
                                case pc.status when 'P' then 'Опубликовано' else null end status,
                                pt.purchaseCodeName place,
                                pc.purchaseInfo_name purchase_name,
                                pc.type + ' ' + cast(pc.version as varchar(12)) edition,
                                convert(varchar(10),pc.publicationDateTime,104)+ ' ' + convert(varchar(5),pc.publicationDateTime,108) publ_date,
                                pc.placer,pc.customer,pc.supplier,
                                cus.fullName cus_fullName,
                                cus.shortName cus_shortName,
                                us1.ticker cus_ticker,
                                cus.inn cus_inn,
                                cus.kpp cus_kpp,
                                cus.ogrn cus_ogrn,
                                cus.postalAddress cus_postalAddress,
                                cus.legalAddress cus_legalAddress,
                                cus.phone cus_phone,
                                cus.fax cus_fax,
                                cus.email cus_email,
                                pl.fullName pl_fullName,
                                pl.shortName pl_shortName,
                                us2.ticker pl_ticker,
                                pl.inn pl_inn,
                                pl.kpp pl_kpp,
                                pl.ogrn pl_ogrn,
                                pl.postalAddress pl_postalAddress,
                                pl.legalAddress pl_legalAddress,
                                pl.phone pl_phone,
                                pl.fax pl_fax,
                                pl.email pl_email,
                                sup.name sup_name,
                                ISNULL(us3.ticker,g1.okpo) sup_ticker,
                                sup.inn sup_inn,
                                sup.kpp sup_kpp,
                                sup.ogrn sup_ogrn,
                                sup.type sup_type,
                                convert(varchar(10), pc.createDateTime,104) as crDate,
                                isnull(case isnumeric(pc.[sum]) when 1 then cast(cast(pc.[sum] as money) as varchar(32)) else cast(pc.[sum] as varchar(32)) end,cast(pc.sumInfo as varchar(32))) as  summa,
                                pc.currency, 
                                pc.fulfillmentDate exe_date,
                                pc.deliveryPlace
                                from Zakupki..purchaseContracts pc
                                left join zakupki.dbo.purchaseTypes pt on pc.purchaseMethodCode=pt.purchaseMethodCode
                                left JOIN zakupki.dbo.pchcustomers cus on cus.id=pc.customer
                                LEFT JOIN zakupki.dbo.pchplacers pl on pl.id=pc.placer
                                LEFT JOIN zakupki.dbo.pchsuppliers sup on sup.id=pc.supplier
                                left join  searchdb2.dbo.union_search us1 on cus.inn=us1.inn  and us1.uniq_inn=1
                                left join  searchdb2.dbo.union_search us2 on pl.inn=us2.inn  and us2.uniq_inn=1
                                left join  searchdb2.dbo.union_search us3 on sup.inn=us3.inn  and us3.uniq_inn=1 and sup.type='L'
                                left join gks_ip.dbo.GKS_IPS g1 on g1.inn=sup.inn and sup.type='P'
                                where pc.guid=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                con.Open();
                Contract223Data cn = null;
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (await reader.ReadAsync())
                {
                    cn = new Contract223Data
                    {
                        id = id,
                        gi = new Contract223GeneralInfo
                        {
                            reg_num = (string)reader.ReadNullIfDbNull("reg_num"),
                            status = (string)reader.ReadNullIfDbNull("status"),
                            place = (string)reader.ReadNullIfDbNull("place"),
                            purchase_name = (string)reader.ReadNullIfDbNull("purchase_name"),
                            edition = (string)reader.ReadNullIfDbNull("edition"),
                            publish_date = (string)reader.ReadNullIfDbNull("publ_date")
                        },
                        gd = new Contract223GeneralData
                        {
                            crDate = (string)reader.ReadNullIfDbNull("crDate"),
                            summa = (string)reader.ReadNullIfDbNull("summa"),
                            currency = (string)reader.ReadNullIfDbNull("currency"),
                            exe_date = (string)reader.ReadNullIfDbNull("exe_date"),
                            deliveryPlace = (string)reader.ReadNullIfDbNull("deliveryPlace")
                        },
                        ci = (Guid?)reader.ReadNullIfDbNull("customer") == null ? null : new Contract223CustomerInfo
                        {
                            fullname = (string)reader.ReadNullIfDbNull("cus_fullName"),
                            shortname = (string)reader.ReadNullIfDbNull("cus_shortName"),
                            ticker = (string)reader.ReadNullIfDbNull("cus_ticker"),
                            inn = (string)reader.ReadNullIfDbNull("cus_inn"),
                            kpp = (string)reader.ReadNullIfDbNull("cus_kpp"),
                            ogrn = (string)reader.ReadNullIfDbNull("cus_ogrn"),
                            post_address = (string)reader.ReadNullIfDbNull("cus_postalAddress"),
                            legal_address = (string)reader.ReadNullIfDbNull("cus_legalAddress"),
                            email = (string)reader.ReadNullIfDbNull("cus_email"),
                            phone = (string)reader.ReadNullIfDbNull("cus_phone"),
                            fax = (string)reader.ReadNullIfDbNull("cus_fax")
                        },
                        pi = (Guid?)reader.ReadNullIfDbNull("placer") == null ? null : new Contract223PlacerInfo
                        {
                            fullname = (string)reader.ReadNullIfDbNull("pl_fullName"),
                            shortname = (string)reader.ReadNullIfDbNull("pl_shortName"),
                            ticker = (string)reader.ReadNullIfDbNull("pl_ticker"),
                            inn = (string)reader.ReadNullIfDbNull("pl_inn"),
                            kpp = (string)reader.ReadNullIfDbNull("pl_kpp"),
                            ogrn = (string)reader.ReadNullIfDbNull("pl_ogrn"),
                            post_address = (string)reader.ReadNullIfDbNull("pl_postalAddress"),
                            legal_address = (string)reader.ReadNullIfDbNull("pl_legalAddress"),
                            email = (string)reader.ReadNullIfDbNull("pl_email"),
                            phone = (string)reader.ReadNullIfDbNull("pl_phone"),
                            fax = (string)reader.ReadNullIfDbNull("pl_fax")
                        },
                        si = (Guid?)reader.ReadNullIfDbNull("supplier") == null ? null : new Contract223SuplierInfo
                        {
                            name = (string)reader.ReadNullIfDbNull("sup_name"),
                            ticker = (string)reader.ReadNullIfDbNull("sup_ticker"),
                            inn = (string)reader.ReadNullIfDbNull("sup_inn"),
                            kpp = (string)reader.ReadNullIfDbNull("sup_kpp"),
                            ogrn = (string)reader.ReadNullIfDbNull("sup_ogrn"),
                            type = (string)reader.ReadNullIfDbNull("sup_type")
                        },
                        products = await _GetConrtact223Products(id)
                    };
                }
                return cn;
            }
        }


        private static async Task<List<Contract223Product>> _GetConrtact223Products(Guid contract_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"Select ordinalNumber num, okdp + isnull(' - ' + o.name,'') as okdp,okved + ' - ' + ok.name as okved ,ei.name as ei,qty,additionalInfo 
                            from zakupki.dbo.contractItems ci left join zakupki..dic_Okdp o on o.code=ci.okdp 
                            left join zakupki..Okveds ok on  ok.code=ci.okved 
                            left join zakupki..OKEIs ei on ei.code=ci.okei 
                            where  purchaseContract_giud=@id order by ordinalNumber	";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = contract_id;
                con.Open();
                List<Contract223Product> prods = new List<Contract223Product>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    prods.Add(new Contract223Product
                    {
                        num = (int?)reader.ReadNullIfDbNull("num"),
                        okdp = (string)reader.ReadNullIfDbNull("okdp"),
                        okved = (string)reader.ReadNullIfDbNull("okved"),
                        ei = (string)reader.ReadNullIfDbNull("ei"),
                        qty = (decimal?)reader.ReadNullIfDbNull("qty"),
                        additionalInfo = (string)reader.ReadNullIfDbNull("additionalInfo")
                    });
                }
                return prods;
            }
        }
        #endregion
    }

    public enum ZakupkiCodeType
    {
        Okved = 0, Okdp = 1, Region = 2
    }
}