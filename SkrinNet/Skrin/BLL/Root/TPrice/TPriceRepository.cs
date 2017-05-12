using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Skrin.BLL.Infrastructure;
using Skrin.Models.QIVSearch;

namespace Skrin.BLL.Root.TPrice
{
    public class TPriceRepository
    {
        static readonly string _constring = Configs.ConnectionString;

        public static async Task<string> GetNewId(string old_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..getTPriceID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = old_id;
                con.Open();
                return (string)await cmd.ExecuteScalarAsync();
            }
        }

        public static async Task<List<TPriceULDetails>> SearchAsync(TPriceSO so, int user_id)
        {
            if (so.template_params == null || so.template_params.tparams == null)
                return null;

            List<string> xml = new List<string>() { "<root>" };
            foreach (var par in so.template_params.tparams)
            {
                xml.Add(string.Format("<param id=\"{0}\" year=\"{1}\" quarter=\"4\" maxval=\"{2}\" minval=\"{3}\"/>", par.param_id, par.year, par.to, par.from));
            }
            xml.Add("</root>");

            #region log

            List<string> proc_creator = new List<string>();
            proc_creator.Add(so.template_params.pure_actives.ToString());
            proc_creator.Add(so.template_params.loss.ToString());
            proc_creator.Add(so.template_params.constitutors.ToString());
            proc_creator.Add(so.template_params.ncons.ToString());
            proc_creator.Add(so.template_params.subs.ToString());
            proc_creator.Add(so.template_params.nsubs.ToString());
            proc_creator.Add(so.template_params.nperiods.ToString());
            proc_creator.Add(so.template_params.only_suitable.ToString());
            proc_creator.Add(so.template_params.group_id.ToString());
            proc_creator.Add(string.Format("'{0}'",so.template_params.regions));
            proc_creator.Add(so.template_params.reg_excl.ToString());
            proc_creator.Add(string.Format("'{0}'", so.template_params.industry.EmptyNull()));
            proc_creator.Add(so.template_params.ind_excl.ToString());
            proc_creator.Add(so.template_params.ind_main.ToString());
            proc_creator.Add(string.Format("'{0}'", so.template_params.okfs));
            proc_creator.Add(so.template_params.okfs_excl.ToString());
            proc_creator.Add(so.extra_params.page_no.ToString());
            proc_creator.Add(so.template_params.rcount.ToString());
            proc_creator.Add(user_id.ToString());
            proc_creator.Add("0");
            proc_creator.Add("''");
            proc_creator.Add(string.Format("'{0}'", so.template_params.andor));
            proc_creator.Add(string.Format("'{0}'", string.Join("", xml)));
            proc_creator.Add(string.Format("'{0}'",so.extra_params.sel));
            proc_creator.Add("''");

            string proc_log = "skrin_content_output..TPrices_Groups_Temp_SkrinNet " + string.Join(",",proc_creator);
            string err_log = null;

            #endregion


            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..TPrices_Groups_Temp_SkrinNet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@pure_actives", so.template_params.pure_actives);
                cmd.Parameters.AddWithValue("@loss", so.template_params.loss);
                cmd.Parameters.AddWithValue("@constitutors", so.template_params.constitutors);
                cmd.Parameters.AddWithValue("@ncons", so.template_params.ncons);
                cmd.Parameters.AddWithValue("@subs", so.template_params.subs);
                cmd.Parameters.AddWithValue("@nsubs", so.template_params.nsubs);
                cmd.Parameters.AddWithValue("@nperiods", so.template_params.nperiods);
                cmd.Parameters.AddWithValue("@only_suitable", so.template_params.only_suitable);
                cmd.Parameters.AddWithValue("@group_id", so.template_params.group_id);
                cmd.Parameters.AddWithValue("@regions", so.template_params.regions);
                cmd.Parameters.AddWithValue("@reg_excl", so.template_params.reg_excl);
                cmd.Parameters.AddWithValue("@industry", so.template_params.industry.EmptyNull());
                cmd.Parameters.AddWithValue("@ind_excl", so.template_params.ind_excl);
                cmd.Parameters.AddWithValue("@ind_main", so.template_params.ind_main);
                cmd.Parameters.AddWithValue("@okfs", so.template_params.okfs);
                cmd.Parameters.AddWithValue("@okfs_excl", so.template_params.okfs_excl);
                cmd.Parameters.AddWithValue("@page_no", so.extra_params.page_no);
                cmd.Parameters.AddWithValue("@rcount", so.template_params.rcount);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@top1000", 0);
                cmd.Parameters.AddWithValue("@group_name", "");
                cmd.Parameters.AddWithValue("@andor", so.template_params.andor);
                cmd.Parameters.AddWithValue("@params", string.Join("", xml));
                cmd.Parameters.AddWithValue("@sess_id", so.extra_params.sel);
                cmd.Parameters.AddWithValue("@ids", "");
                con.Open();
                var ret = new List<TPriceULDetails>();
                try
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add(new TPriceULDetails
                            {
                                issuer_id = reader.ReadEmptyIfDbNull("issuer_id"),
                                ticker = reader.ReadEmptyIfDbNull("ticker"),
                                ogrn = reader.ReadEmptyIfDbNull("ogrn"),
                                name = reader.ReadEmptyIfDbNull("name"),
                                inn = reader.ReadEmptyIfDbNull("inn"),
                                okpo = reader.ReadEmptyIfDbNull("okpo"),
                                ruler = reader.ReadEmptyIfDbNull("ruler"),
                                legal_address = reader.ReadEmptyIfDbNull("legal_address"),
                                del = reader.ReadEmptyIfDbNull("del"),
                                type_id = reader.ReadEmptyIfDbNull("type_id"),
                                gks_id = reader.ReadEmptyIfDbNull("gks_id"),
                                okved = reader.ReadEmptyIfDbNull("okved"),
                                suit = (int)reader["suit"],
                                search_count = (int)reader["search_count"],
                                session_id = (string)reader["sess_id"]
                            });
                        }
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    err_log = ex.Message;
                    throw;
                }
                finally
                {
                    SqlUtiltes.ProcLog(proc_log, err_log);
                }
            }
        }


        public static async Task<TPriceMessage> GroupAsync(TPriceSO so, int user_id)
        {
            if (so.template_params == null || so.template_params.tparams == null)
                return null;

            List<string> xml = new List<string>() { "<root>" };
            foreach (var par in so.template_params.tparams)
            {
                xml.Add(string.Format("<param id=\"{0}\" year=\"{1}\" quarter=\"4\" maxval=\"{2}\" minval=\"{3}\"/>", par.param_id, par.year, par.to, par.from));
            }
            xml.Add("</root>");


            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..TPrices_Groups_Temp_SkrinNet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@pure_actives", so.template_params.pure_actives);
                cmd.Parameters.AddWithValue("@loss", so.template_params.loss);
                cmd.Parameters.AddWithValue("@constitutors", so.template_params.constitutors);
                cmd.Parameters.AddWithValue("@ncons", so.template_params.ncons);
                cmd.Parameters.AddWithValue("@subs", so.template_params.subs);
                cmd.Parameters.AddWithValue("@nsubs", so.template_params.nsubs);
                cmd.Parameters.AddWithValue("@nperiods", so.template_params.nperiods);
                cmd.Parameters.AddWithValue("@only_suitable", so.template_params.only_suitable);
                cmd.Parameters.AddWithValue("@group_id", so.template_params.group_id);
                cmd.Parameters.AddWithValue("@regions", so.template_params.regions);
                cmd.Parameters.AddWithValue("@reg_excl", so.template_params.reg_excl);
                cmd.Parameters.AddWithValue("@industry", so.template_params.industry.EmptyNull());
                cmd.Parameters.AddWithValue("@ind_excl", so.template_params.ind_excl);
                cmd.Parameters.AddWithValue("@ind_main", so.template_params.ind_main);
                cmd.Parameters.AddWithValue("@okfs", so.template_params.okfs);
                cmd.Parameters.AddWithValue("@okfs_excl", so.template_params.okfs_excl);
                cmd.Parameters.AddWithValue("@page_no", so.extra_params.page_no);
                cmd.Parameters.AddWithValue("@rcount", so.template_params.rcount);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@top1000", 1);
                cmd.Parameters.AddWithValue("@group_name", so.extra_params.group_name);
                cmd.Parameters.AddWithValue("@andor", so.template_params.andor);
                cmd.Parameters.AddWithValue("@params", string.Join("", xml));
                cmd.Parameters.AddWithValue("@sess_id", so.extra_params.sel);
                cmd.Parameters.AddWithValue("@ids", "");
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        return new TPriceMessage
                        {
                            code = (int)reader[0],
                            msg = (string)reader[1]
                        };
                    }
                }
                throw new Exception("Этого не должно быть");
            }
        }

        public static async Task<ExcelResult> SearchExcelAsync(TPriceSO so, int user_id)
        {
            if (so.template_params == null || so.template_params.tparams == null)
                return null;

            List<string> xml = new List<string>() { "<root>" };
            foreach (var par in so.template_params.tparams)
            {
                xml.Add(string.Format("<param id=\"{0}\" year=\"{1}\" quarter=\"4\" maxval=\"{2}\" minval=\"{3}\"/>", par.param_id, par.year, par.to, par.from));
            }
            xml.Add("</root>");
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..TPrices_Groups_Temp_SkrinNet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@pure_actives", so.template_params.pure_actives);
                cmd.Parameters.AddWithValue("@loss", so.template_params.loss);
                cmd.Parameters.AddWithValue("@constitutors", so.template_params.constitutors);
                cmd.Parameters.AddWithValue("@ncons", so.template_params.ncons);
                cmd.Parameters.AddWithValue("@subs", so.template_params.subs);
                cmd.Parameters.AddWithValue("@nsubs", so.template_params.nsubs);
                cmd.Parameters.AddWithValue("@nperiods", so.template_params.nperiods);
                cmd.Parameters.AddWithValue("@only_suitable", so.template_params.only_suitable);
                cmd.Parameters.AddWithValue("@group_id", so.template_params.group_id);
                cmd.Parameters.AddWithValue("@regions", so.template_params.regions);
                cmd.Parameters.AddWithValue("@reg_excl", so.template_params.reg_excl);
                cmd.Parameters.AddWithValue("@industry", so.template_params.industry.EmptyNull());
                cmd.Parameters.AddWithValue("@ind_excl", so.template_params.ind_excl);
                cmd.Parameters.AddWithValue("@ind_main", so.template_params.ind_main);
                cmd.Parameters.AddWithValue("@okfs", so.template_params.okfs);
                cmd.Parameters.AddWithValue("@okfs_excl", so.template_params.okfs_excl);
                cmd.Parameters.AddWithValue("@page_no", so.extra_params.page_no);
                cmd.Parameters.AddWithValue("@rcount", so.template_params.rcount);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@top1000", 2);
                cmd.Parameters.AddWithValue("@group_name", so.extra_params.group_name);
                cmd.Parameters.AddWithValue("@andor", so.template_params.andor);
                cmd.Parameters.AddWithValue("@params", string.Join("", xml));
                cmd.Parameters.AddWithValue("@sess_id", so.extra_params.sel);
                cmd.Parameters.AddWithValue("@ids", so.extra_params.iss.EmptyNull());
                con.Open();
                var ret = new ExcelResult();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ret.AddHeader((int)(long)reader["rn"], (string)reader["name"], (string)reader["fname"], (string)reader["fmt"]);
                    }
                    if(await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            for (int i = 0,imax=ret.GetFieldCount(); i < imax; i++)
                            {
                                string fieldname = ret.GetFieldName(i);
                                ret.AddValue(i, (string)reader.ReadNullIfDbNull(fieldname));
                            }
                        }
                    }
                }
                return ret;
            }
        }


        public static async Task<TPriceResult> CalculateAsync(string id, string ids)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..CalculateTPrice_sel_SkrinNet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
                cmd.Parameters.Add("@ids", SqlDbType.VarChar).Value = ids;
                cmd.CommandTimeout = 0;
                con.Open();
                var result = new TPriceResult();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.values.Add(new TPriceVal
                        {
                            id = (int)reader["id"],
                            name = (string)reader["name"],
                            v1 = (decimal)reader["v1"],
                            v2 = (decimal)reader["v2"],
                            v3 = (decimal)reader["v3"],
                            v4 = (decimal)reader["v4"],
                            v5 = (decimal)reader["v5"],
                            v6 = (decimal)reader["v6"]
                        });
                    }
                }

                return result;
            }
        }

        public static async Task<List<TpriceTemplate>> GetTemplates(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("Select id,name,tcontent from skrin_net..transprice_template where user_id=@user_id  order by id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value=user_id;
                con.Open();
                var ret = new List<TpriceTemplate>();
                using (var reader=await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ret.Add(new TpriceTemplate
                        {
                            id = (int)reader["id"],
                            name = (string)reader["name"],
                            tcontent = (string)reader["tcontent"]
                        });
                    }
                    return ret;
                }
            }
        }

        public static async Task<string> GetTemplate(int user_id, int id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("Select tcontent from skrin_net..transprice_template where user_id=@user_id and id=@id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                return (string)await cmd.ExecuteScalarAsync();
            }
        }

        public static async Task SaveTemplate(int user_id,string name ,string template)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"insert into skrin_net..transprice_template
                            (user_id,name,tcontent)
                            VALUES
                            (@user_id,@name,@tcontent)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 512).Value = name;
                cmd.Parameters.Add("@tcontent", SqlDbType.VarChar).Value = template;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}