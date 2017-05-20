using Skrin.BLL.Infrastructure;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Search
{
    public class CompanySearchKz
    {

        private CompaniesKzSearchObject _so;
        private static string _constring = Configs.ConnectionString;

        public CompanySearchKz(CompaniesKzSearchObject so)
        {
            _so = so;
        }

        public static async Task<string> GetRegionsAsync(string ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                string sSql = "select STUFF((SELECT ','+Code from (Select Code from  kz.dbo.Dic_Kato where Id in (" + ids + ")) o  ORDER BY Code  FOR XML PATH('')),1,1,'')";
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand(sSql, con);

                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader[0] != DBNull.Value ? (string)reader[0] : "";
                        }
                    }
                }
            }
            return "";
        }

        public static async Task<string> GetIndustryAsync(string ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                string sSql = "SELECT STUFF((SELECT ','+Code FROM  (  " +
                 "Select a.Code from kz.dbo.Dic_OKED a inner join (Select *  from searchdb2.dbo.kodesplitter(0,'" + ids + "')) b " +
                 "on (a.parentid=b.kod and exists(select 1 from kz.dbo.Dic_OKED c where c.id=b.kod and c.parentid=0)) " +
                 "or (b.kod=a.id and parentid!=0) ) o ORDER BY o.Code FOR XML PATH('')),1,1,'')";
                using (SqlConnection con = new SqlConnection(_constring))
                {
                    SqlCommand cmd = new SqlCommand(sSql, con);

                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader[0] != DBNull.Value ? (string)reader[0] : "";
                        }
                    }
                }
            }
            return "";
        }

        public static async Task<List<KZSearchResult>> DoSearch(CompaniesKzSearchObject so)
        {
            string sSql = "KZ..[MainSearchRows2]";
            List<KZSearchResult> ret = new List<KZSearchResult>();

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(sSql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@company", SqlDbType.VarChar, 512).Value = !String.IsNullOrEmpty(so.company) ? so.company : "";
                //cmd.Parameters.Add("@kod", SqlDbType.VarChar, 512).Value = !String.IsNullOrEmpty(so.company) ? so.company : "";
                cmd.Parameters.Add("@ruler", SqlDbType.VarChar, 512).Value = !String.IsNullOrEmpty(so.ruler) ? so.ruler : "";
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.status) ? so.status : "";
                cmd.Parameters.Add("@stat_excl", SqlDbType.Int).Value = so.stat_excl;
                cmd.Parameters.Add("@regions", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
                cmd.Parameters.Add("@reg_excl", SqlDbType.Int).Value = so.reg_excl;
                cmd.Parameters.Add("@industry", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.industry) ? so.industry : "";
                cmd.Parameters.Add("@ind_excl", SqlDbType.Int).Value = so.ind_excl;
                cmd.Parameters.Add("@ind_main", SqlDbType.Int).Value = so.ind_main;
                cmd.Parameters.Add("@econ", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.econ) ? so.econ : "";
                cmd.Parameters.Add("@econ_excl", SqlDbType.Int).Value = so.econ_excl;
                cmd.Parameters.Add("@own", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.own) ? so.own : "";
                cmd.Parameters.Add("@own_excl", SqlDbType.Int).Value = so.own_excl;
                cmd.Parameters.Add("@siz", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.siz) ? so.siz : "";
                cmd.Parameters.Add("@siz_excl", SqlDbType.Int).Value = so.siz_excl;
                cmd.Parameters.Add("@pcount", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.pcount) ? so.pcount : "";
                cmd.Parameters.Add("@pcount_excl", SqlDbType.Int).Value = so.pcount_excl;
                cmd.Parameters.Add("@page_no", SqlDbType.Int).Value = so.page_no;
                cmd.Parameters.Add("@rcount", SqlDbType.Int).Value = so.rcount;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = so.user_id;
                cmd.Parameters.Add("@top1000", SqlDbType.Int).Value = so.top1000;         
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        ret.Add(new KZSearchResult() { 
                        code = (string)rd[0],
                        name = rd[1] != DBNull.Value ? (string)rd[1] : "-",
                        region = rd[2] != DBNull.Value ? (string)rd[2] : "-",
                        cnt = (int)rd[3],
                        FullAddress = rd[4] != DBNull.Value ? (string)rd[4] : "",
                        Manager = rd[5] != DBNull.Value ? (string)rd[5] : "",
                        MainDeal = rd[6] != DBNull.Value ? (string)rd[6] : "",
                        access = (int)rd[7]             
                        });
                    }                   
                }
            }
            return ret;
        }

        public static async Task<List<KZDetails>> DoSearchTop1000(CompaniesKzSearchObject so)
        {
            string sSql = "KZ..[MainSearchRows]";
            List<KZDetails> ret = new List<KZDetails>();

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand(sSql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@company", SqlDbType.VarChar, 512).Value = !String.IsNullOrEmpty(so.company) ? so.company : "";
                cmd.Parameters.Add("@ruler", SqlDbType.VarChar, 512).Value = !String.IsNullOrEmpty(so.ruler) ? so.ruler : "";
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.status) ? so.status : "";
                cmd.Parameters.Add("@stat_excl", SqlDbType.Int).Value = so.stat_excl;
                cmd.Parameters.Add("@regions", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.regions) ? so.regions : "";
                cmd.Parameters.Add("@reg_excl", SqlDbType.Int).Value = so.reg_excl;
                cmd.Parameters.Add("@industry", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.industry) ? so.industry : "";
                cmd.Parameters.Add("@ind_excl", SqlDbType.Int).Value = so.ind_excl;
                cmd.Parameters.Add("@ind_main", SqlDbType.Int).Value = so.ind_main;
                cmd.Parameters.Add("@econ", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.econ) ? so.econ : "";
                cmd.Parameters.Add("@econ_excl", SqlDbType.Int).Value = so.econ_excl;
                cmd.Parameters.Add("@own", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.own) ? so.own : "";
                cmd.Parameters.Add("@own_excl", SqlDbType.Int).Value = so.own_excl;
                cmd.Parameters.Add("@siz", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.siz) ? so.siz : "";
                cmd.Parameters.Add("@siz_excl", SqlDbType.Int).Value = so.siz_excl;
                cmd.Parameters.Add("@pcount", SqlDbType.VarChar).Value = !String.IsNullOrEmpty(so.pcount) ? so.pcount : "";
                cmd.Parameters.Add("@pcount_excl", SqlDbType.Int).Value = so.pcount_excl;
                cmd.Parameters.Add("@page_no", SqlDbType.Int).Value = so.page_no;
                cmd.Parameters.Add("@rcount", SqlDbType.Int).Value = so.rcount;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = so.user_id;
                cmd.Parameters.Add("@top1000", SqlDbType.Int).Value = so.top1000;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        ret.Add(new KZDetails()
                        {
                            Name = rd.ReadEmptyIfDbNull("Name"),
                            Code = rd.ReadEmptyIfDbNull("Code"),
                            RegionName = rd.ReadEmptyIfDbNull("RegionName"),
                            FullAddress = rd.ReadEmptyIfDbNull("FullAddress"),
                            MainDeal = rd.ReadEmptyIfDbNull("MainDeal"),
                            CodeTax = rd.ReadEmptyIfDbNull("CodeTax"),
                            DateReg = rd.ReadEmptyIfDbNull("DateReg"),
                            Phone = rd.ReadEmptyIfDbNull("Phone"),
                            Fax = rd.ReadEmptyIfDbNull("Fax"),
                            Email = rd.ReadEmptyIfDbNull("Email")
                        });
                    }
                }
            }
            return ret;
        }

    }
}