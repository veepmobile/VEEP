using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Skrin.BLL.ISS.Content
{
    public static class SkrinContentRepository
    {
        private static string _constring = Configs.ConnectionString;
        /*
        public static List<EgrulULAddressModel> GetEgrulBranches(object ogrn_obj)
        {
            string ogrn = (string)ogrn_obj;
            List<EgrulULAddressModel> ret = new List<EgrulULAddressModel>();
            if (ogrn != null && ogrn.Length == 13)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_content_output.dbo.egrul_get_branches", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new EgrulULAddressModel
                        {
                            name = (string)reader.ReadNullIfDbNull("name"),
                            address = (string)reader.ReadNullIfDbNull("address"),
                            grn_date = (DateTime?)reader.ReadNullIfDbNull("grn_date"),
                            grn = (string)reader.ReadNullIfDbNull("grn")
                        });
                    }
                }
            }
            return ret;
        }
        */
        /*
        public static List<EgrulULAddressModel> GetEgrulOutputs(object ogrn_obj)
        {
            string ogrn = (string)ogrn_obj;
            List<EgrulULAddressModel> ret = new List<EgrulULAddressModel>();
            if (ogrn != null && ogrn.Length == 13)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand("skrin_content_output.dbo.egrul_get_outputs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new EgrulULAddressModel
                        {
                            name = (string)reader.ReadNullIfDbNull("name"),
                            address = (string)reader.ReadNullIfDbNull("address"),
                            grn_date = (DateTime?)reader.ReadNullIfDbNull("grn_date"),
                            grn = (string)reader.ReadNullIfDbNull("grn")
                        });
                    }
                }
            }
            return ret;
        }
        */
        public static HorisonrtalTable GetEgrulBranches(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_branches1");
        }

        public static HorisonrtalTable GetEgrulOutputs(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_outputs1");
        }

        public static HorisonrtalTable GetLicense(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_license");
        }

        public static HorisonrtalTable GetCapital(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_capital");
        }

        public static HorisonrtalTable GetReduce(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_authorized_reduce");
        }

        public static HorisonrtalTable GetRegister(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_shared_holders_register");
        }

        public static HorisonrtalTable GetReorg(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_reorg");
        }

        public static HorisonrtalTable GetPredecessors(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_predecessors");
        }

        public static HorisonrtalTable GetSuccessors(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_successors");
        }

        //---->
        public static HorisonrtalTable GetManagers(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_managers");
        }
        public static HorisonrtalTable GetManagersHistory(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_managers_history");
        }
        public static HorisonrtalTable GetManagementCompanies(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_management_companies");
        }
        public static HorisonrtalTable GetManagementForeignCompanies(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.[egrul_get_management_foreigncompanies]");
        }
        public static HorisonrtalTable GetManagementForeignCompanyFilials(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.[egrul_get_management_foreigncompany_filials]");
        }
        public static HorisonrtalTable GetManagementForeignCompanyFilialsManager(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.[egrul_get_management_foreigncompany_filials_manager]");
        }

        //----> Сведения об учредителе (участнике)
        public static HorisonrtalTable GetConstitutors(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_constitutors");
        }
        //        public static HorisonrtalTable GetConstitutors(object ogrn_obj)
        //public static HorisonrtalTablePlus GetConstitutorsPlus(object ogrn_obj)
        //{
        //    return GetUniversalTablePlus((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_constitutors_1");
        //}
        public static HorisonrtalTable GetConstitutorsHistory(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_constitutors_history");
        }

        //---->
        public static HorisonrtalTable GetGegDataMaincodes(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_maincodes1");
        }
        public static HorisonrtalTable GetRegDataNames(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_names");
        }
        public static HorisonrtalTable GetGetRegDataNameHistory(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_name_history");
        }
        public static HorisonrtalTable GetRegDataAddress(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_address");
        }
        public static HorisonrtalTable GetGetRegDataAddressHistory(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_address_history");
        }
        public static HorisonrtalTable GetGetRegDataStatus(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_status");
        }
        public static HorisonrtalTable GetGetRegDataStoping(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_stoping");
        }
        public static HorisonrtalTable GetGetRegDataReginfo(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_reginfo");
        }
        public static HorisonrtalTable GetGetRegDataRegoldinfo(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_regoldinfo");
        }
        public static HorisonrtalTable GetGetRegDataRegorginfo(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_regorginfo");
        }
        public static HorisonrtalTable GetGetRegDataRecord(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_record");
        }
        public static HorisonrtalTable GetGetRegDataPFRegistration(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_pfregistration");
        }
        public static HorisonrtalTable GetGetRegDataFSSRegistration(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_fssregistration");
        }
        public static HorisonrtalTable GetGetRegDataOKVEDs(object ogrn_obj)
        {
            return GetUniversalTable((string)ogrn_obj, "skrin_content_output.dbo.egrul_get_okveds");
        }


/*
        private void getConstitutorsHistory(object ogrn_obj)
        {
            Prof.egrul_founder_hist = new List<EgrulFounderHistoryData>();

            if (!Prof.egrul_exists)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileEgrulFounderHistory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", Prof.profile.ogrn);
                try
                {
                    con.Open();

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Prof.egrul_founder_hist.Add(new EgrulFounderHistoryData
                            {
                                dc = rd.ReadEmptyIfDbNull("dc"),
                                df = rd.ReadEmptyIfDbNull("df"),
                                ds = rd.ReadEmptyIfDbNull("ds"),
                                name = rd.ReadEmptyIfDbNull("name"),
                                summa = (decimal?)rd.ReadNullIfDbNull("summa"),
                                uid = (Guid)rd["uid"],
                                mode = (int)rd["mode"],
                                inn = rd.ReadEmptyIfDbNull("inn")
                            });
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileEgrulFounderHistory: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                    stopwatch.Stop();
                    Prof.timeouts.Add("getProfileEgrulFounderHistory", stopwatch.ElapsedMilliseconds);
                }
            }
        }
*/


        public static HorisonrtalTable GetUniversalTable(string ogrn, string query)
        {
            HorisonrtalTable ret = new HorisonrtalTable();
            string _s;
            //ColumnData _cd;
            if (!string.IsNullOrWhiteSpace(ogrn) && ogrn.Length == 13)
            {
                using (SqlConnection con = new SqlConnection(_constring))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            ret.Header = rd.getStrValue(0, "");
                            _s = rd.getStrValue(1, "");
                            if (_s != "") ret.TableAttributes = _s;
                            _s = rd.getStrValue(2, "");
                            if (_s != "") ret.Format = _s;
                        }
                        rd.NextResult();
                        ret.Columns = new Dictionary<string, ColumnData>();
                        switch (rd.FieldCount)
                        {
                            case 12:
                                {
                                    while (rd.Read())
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

                            case 9:
                                {
                                    while (rd.Read())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(rd.getStrValue(2, ""), rd.getStrValue(3, ""), rd.getStrValue(4, ""), rd.getIntValue(6, 0), rd.getStrValue(7, ""), rd.getStrValue(8, "")));
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    while (rd.Read())
                                    {
                                        ret.Columns.Add(rd.getStrValue(1, ""), new ColumnData(rd.getStrValue(2, ""), rd.getStrValue(3, ""), rd.getStrValue(4, "")));
                                    }
                                    break;
                                }
                        }
                        rd.NextResult();
                        while (rd.Read())
                        {
                            /*
                            for (int c = 0; c < rd.FieldCount; c++)
                            {
                                _cd = ret.Columns[rd.GetName(c)];
                                _cd.ColumnValues.Add(new ColumnValue(rd.GetValue(c), _cd.ValueFormat));
                            }
                             */
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


        //public static HorisonrtalTablePlus GetUniversalTablePlus(string ogrn, string query)
        //{
        //    HorisonrtalTablePlus ret = new HorisonrtalTablePlus();
        //    if (!string.IsNullOrWhiteSpace(ogrn) && ogrn.Length == 13)
        //    {
        //        using (SqlConnection con = new SqlConnection(_constring))
        //        using (SqlCommand cmd = con.CreateCommand())
        //        {
        //            cmd.CommandText = query;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
        //            con.Open();
        //            using (SqlDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    ret.Title = rd.getStrValue(0, "");
        //                    ret.Attributes = rd.getStrValue(1, ""); ;
        //                    ret.Format = rd.getStrValue(2, ""); 
        //                }
        //                rd.NextResult();
        //                int _row_no = 0;
        //                List<HeaderItem> _heads = new List<HeaderItem>();
        //                ret.HeaderRows.Add(_row_no, _heads);
        //                while (rd.Read())
        //                {
        //                    if (_row_no != rd.getIntValue(0, 0))
        //                    {
        //                        _row_no = rd.getIntValue(0, 0);
        //                        _heads = new List<HeaderItem>();
        //                        ret.HeaderRows.Add(_row_no, _heads);
        //                    }
        //                    _heads.Add(new HeaderItem(rd.getIntValue(1, 0),
        //                                              rd.getIntValue(2, 0),
        //                                              rd.getStrValue(3, ""), 
        //                                              rd.getStrValue(4, ""), 
        //                                              rd.getStrValue(5, "")));
        //                }
        //                rd.NextResult();
        //                while (rd.Read())
        //                {
        //                    ret.Columns.Add(rd.getStrValue(1, ""), new CellData(
        //                          rd.getStrValue(2, "") //Attributes
        //                        , rd.getStrValue(3, "") //Format
        //                        ));
        //                }
        //                rd.NextResult();
        //                while (rd.Read())
        //                {
        //                    foreach (var _cd in ret.Columns)
        //                    {
        //                        _cd.Value.Values.Add(new CellValue(rd.GetValue(rd.GetOrdinal(_cd.Key)), _cd.Value.Format));
        //                    }
        //                }
        //                rd.Close();
        //            }
        //            con.Close();
        //        }
        //    }
        //    return ret;
        //}
    }
}