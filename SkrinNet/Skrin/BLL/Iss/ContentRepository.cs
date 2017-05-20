using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using Skrin.BLL.Root;

namespace Skrin.BLL.Iss
{
    public static class ContentRepository
    {

        /* Регистрационные данные (ЕГРЮЛ) */
        public static async Task<HorisonrtalTable> GetMaincodes(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_maincodes1");
        }
        public static async Task<HorisonrtalTable> GetNames(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_names");
        }
        public static async Task<HorisonrtalTable> GetNameHistory(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_name_history");
        }
        public static async Task<HorisonrtalTable> GetAddress(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_address");
        }
        public static async Task<HorisonrtalTable> GetAddressHistory(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_address_history");
        }
        public static async Task<HorisonrtalTable> GetStatus(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_status");
        }
        public static async Task<HorisonrtalTable> GetStoping(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_stoping");
        }
        public static async Task<HorisonrtalTable> GetRegInfo(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_reginfo");
        }
        public static async Task<HorisonrtalTable> GetRegOldInfo(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_regoldinfo");
        }
        public static async Task<HorisonrtalTable> GetRegOrgInfo(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_regorginfo");
        }
        public static async Task<HorisonrtalTable> GetRecord(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_record");
        }
        public static async Task<HorisonrtalTable> GetPFRegistration(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_pfregistration");
        }
        public static async Task<HorisonrtalTable> GetFSSRegistration(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_fssregistration");
        }
        public static async Task<HorisonrtalTable> GetOKVEDs(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_okveds2");
        }




        /* Размер уставного капитала */
        public static async Task<HorisonrtalTable> GetCapital(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_capital");
        }
        public static async Task<HorisonrtalTable> GetReduce(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_authorized_reduce");
        }
        public static async Task<HorisonrtalTable> GetCBCapital(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.cb_get_capital");
        }


        /* Реорганизация */
        public static async Task<HorisonrtalTable> GetReorg(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_reorg");
        }
        public static async Task<HorisonrtalTable> GetPredecessors(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_predecessors");
        }
        public static async Task<HorisonrtalTable> GetSuccessors(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_successors");
        }


        /* Лицензии */
        public static async Task<HorisonrtalTable> GetLicense(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_license");
        }


        /* Лицо, имеющее право действовать без доверенности */
        public static async Task<HorisonrtalTable> GetManagers(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_managers");
        }
        public static async Task<HorisonrtalTable> GetManagersHistory(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_managers_history");
        }
        public static async Task<HorisonrtalTable> GetManagementCompanies(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_management_companies");
        }
        public static async Task<HorisonrtalTable> GetManagementForeignCompanies(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.[egrul_get_management_foreigncompanies]");
        }
        public static async Task<HorisonrtalTable> GetManagementForeignCompanyFilials(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.[egrul_get_management_foreigncompany_filials]");
        }
        public static async Task<HorisonrtalTable> GetManagementForeignCompanyFilialsManager(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.[egrul_get_management_foreigncompany_filials_manager]");
        }


        //Учредители(участники)
        public static async Task<HorisonrtalTable> GetConstitutors(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_constitutors");
        }
        public static async Task<HorisonrtalTable> GetConstitutorsHistory(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_constitutors_history");
        }


        //Филиалы и представительства
        public static async Task<HorisonrtalTable> GetEgrulBranches(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_branches1");
        }
        public static async Task<HorisonrtalTable> GetEgrulOutputs(string ogrn)
        {
            return await GetUniversalTable(ogrn, "skrin_content_output.dbo.egrul_get_outputs1");
        }


        //Сообщения о банкротстве
        public static async Task<CompanyData> GetBancruptcy(string ticker)
        {
            return await SqlUtiltes.GetCompanyAsync(ticker);
        }
        //Сообщения о банкротстве ИП
        public static async Task<CompanyData> GetBancruptcyIp(string ticker)
        {
            return await SqlUtiltes.GetIPAsync(ticker);
        }

        public static async Task<BancryptcyMessage> GetBancruptcyMessage(string ids, string ticker)
        {
            return await SqlUtiltes.GetBancruptcyMessageAsync(ids, ticker);
        }
        public static async Task<string> GetMessages(string ids)
        {
            StringBuilder sb = new StringBuilder("");
            BancryptcyMessage msg = await SqlUtiltes.GetBancruptcyMessageAsync(ids, "");
            for (int i = 0, i_max = msg.MessagesList.Count; i < i_max; i++)
            {
                sb.Append("<br/><br/><i>Источник данных: " + msg.MessagesList[i].SourceName + "</i><br /><br /><i>Дата публикации сообщения: " + msg.MessagesList[i].RegDate + "</i><br /><br />" + msg.MessagesList[i].Contents + "<br/><hr />");
            }
            return sb.ToString();
        }

        //Сообщения Вестника госрегистрации
        public static async Task<CompanyData> GetVestnik(string ticker)
        {
            return await SqlUtiltes.GetCompanyAsync(ticker);
        }

        //Сведения о фактах деятельности
        public static async Task<CompanyData> GetFedresurs(string ticker)
        {
            {
                return await SqlUtiltes.GetCompanyAsync(ticker);
            }
        }

        //Картотека арбитражных дел
        public static async Task<CompanyData> GetPravo(string ticker)
        {
            return await SqlUtiltes.GetCompanyAsync(ticker);
        }

        //Картотека арбитражных дел
        public static async Task<CompanyData> GetPravoIp(string ticker)
        {
            return await SqlUtiltes.GetIPAsync(ticker);
        }

        private static async Task<HorisonrtalTable> GetUniversalTable(string ogrn, string query)
        {
            HorisonrtalTable ret = new HorisonrtalTable();
            string _s;
            //ColumnData _cd;
            if (!string.IsNullOrWhiteSpace(ogrn) && ogrn.Length == 13)
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            ret.Header = rd.getStrValue(0, "");
                            _s = rd.getStrValue(1, "");
                            if (_s != "") ret.TableAttributes = _s;
                            _s = rd.getStrValue(2, "");
                            if (_s != "") ret.Format = _s;
                        }
                        await rd.NextResultAsync();
                        ret.Columns = new Dictionary<string, ColumnData>();
                        switch (rd.FieldCount)
                        {
                            case 12:
                                {
                                    while (await rd.ReadAsync())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(
                                              rd.getStrValue(2, "") //Header
                                            , rd.getStrValue(3, "") //HeaderAttributes
                                            , rd.getStrValue(4, "") //HeaderFormat
                                            , rd.getStrValue(5, "") //ColumnAttributes 
                                            , rd.getStrValue(6, "") //ValueFormat
                                            , rd.getIntValue(8, 0)  //Column_Colspan
                                            , rd.getStrValue(9, "") //Column_Grp_Header
                                            , rd.getStrValue(10, "") //Column_Grp_Header_Attributes
                                            , rd.getStrValue(11, "") //Column_Grp_Header_Format
                                            ));
                                    }
                                    break;
                                }
                            case 10:
                                {
                                    while (await rd.ReadAsync())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(rd.getStrValue(2, ""), rd.getStrValue(3, ""), rd.getStrValue(4, ""), rd.getIntValue(6, 0), rd.getStrValue(7, ""), rd.getStrValue(8, ""), rd.getStrValue(9, "")));
                                    }
                                    break;
                                }

                            case 9:
                                {
                                    while (await rd.ReadAsync())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(rd.getStrValue(2, ""), rd.getStrValue(3, ""), rd.getStrValue(4, ""), rd.getIntValue(6, 0), rd.getStrValue(7, ""), rd.getStrValue(8, "")));
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    while (await rd.ReadAsync())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(rd.getStrValue(2, ""), rd.getStrValue(3, ""), rd.getStrValue(4, "")));
                                    }
                                    break;
                                }
                        }
                        await rd.NextResultAsync();
                        while (await rd.ReadAsync())
                        {

                            foreach (var _cd in ret.Columns)
                            {
                                _cd.Value.ColumnValues.Add(new ColumnValue(rd.GetValue(rd.GetOrdinal(_cd.Key)), _cd.Value.ValueFormat));
                            }
                        }
                        rd.Close();
                    }
                    con.Close();
                }
            }
            return ret;
        }
    }


}