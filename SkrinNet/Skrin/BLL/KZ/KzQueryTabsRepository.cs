using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;
using Skrin.Models;
using Skrin.Models.ProfileKZ;
using Skrin.BLL.Infrastructure;
using Skrin.BLL;
using System.Threading.Tasks;

namespace Skrin.BLL.KZ
{
    public class KzQueryTabsRepository
    {
        private string _code;
        private static readonly string _constring = Configs.ConnectionString;
        private ProfileKZCodes Codes;

        public KzQueryTabsRepository(string Code)
        {
            _code = Code;
        }
        

        public async Task<ProfileKZCodes> GetCodes()
        {
            Codes = await GetMainCodes();
            Codes.OKEDS = await GetOKEDCodes();
            Codes.kato_name = !String.IsNullOrEmpty(Codes.kato) ? await GetKATOCodes(Codes.kato) : "";
            return Codes;
        }

        public async Task<ProfileKZControls> GetConstrols(string date)
        {
            ProfileKZControls deal = new ProfileKZControls();
            deal.dates = await GetControlDates(date);
            deal.Manager = await GetDealManager(deal.dates.Count > 1 ? deal.dates.FirstOrDefault(i => i.cur == 1).daterep : deal.dates.FirstOrDefault().daterep);
            return deal;
        }

        private async Task<ProfileKZCodes> GetMainCodes()
        {
            ProfileKZCodes codes = new ProfileKZCodes();

            string sSql = @"select CodeTax r_number, Convert(varchar(20),DateReg,104) r_date,Convert(varchar(20),DateReg2,104) r_date2,
	                        Code code,Code_KATO kato,CodeBIN bin,ow.Dscr ownershp, et.Dscr ectype, es.Dscr size, Convert(varchar(20), rf.UpdatedDate,104) updatedate
	                        from KZ.dbo.ut_RfEnterprise rf 
	                        left join KZ.dbo.ut_ClssOwnership ow on rf.Id_Own=ow.Id
			                        left join KZ.dbo.ut_ClssEconomyType et on rf.Id_Economy=et.Id
			                        left join KZ.dbo.ut_ClssEntSize es on rf.Id_Size=es.Id
	                        where Code=@Code";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rd.ReadAsync())
                        {
                            codes.r_number = rd.ReadEmptyIfDbNull("r_number");
                            codes.r_date = rd.ReadEmptyIfDbNull("r_date");
                            codes.r_date2 = rd.ReadEmptyIfDbNull("r_date2");
                            codes.code = rd.ReadEmptyIfDbNull("code");
                            codes.kato = rd.ReadEmptyIfDbNull("kato");
                            codes.bin = rd.ReadEmptyIfDbNull("bin");
                            codes.ownership = rd.ReadEmptyIfDbNull("ownershp");
                            codes.ectype = rd.ReadEmptyIfDbNull("ectype");
                            codes.size = rd.ReadEmptyIfDbNull("size");
                            codes.updatedate = rd.ReadEmptyIfDbNull("updatedate");
                        }                     
                    }
                }
                catch(Exception ee)
                {
                    string pp = ee.Message;
                }
                finally
                {
                    con.Close();                 
                
                }
            }

            return codes;
        }

        private async Task<Dictionary<string,string>> GetOKEDCodes()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            string sSql = @"select  he.Code_OKED Val, do.Name str_Name 
	                        from KZ.dbo.ut_HoldEntOKED he 
	                        left join KZ.dbo.Dic_OKED do
	                        on do.Code=he.Code_OKED
	                        where Code_Main=@Code";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            ret.Add((rd[0] != DBNull.Value ? (string)rd[0] :""),rd.ReadEmptyIfDbNull(1));
                        }
                    }
                }
                finally
                {
                    con.Close();

                }
            }

            return ret;
        }

        private async Task<string> GetKATOCodes(string Kato)    
        {        
            string sSql = @"SELECT KZ.dbo.getKatoPath(@Kato)";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Kato", SqlDbType.VarChar).Value = Kato;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rd.ReadAsync())
                        {
                            return rd[0] != DBNull.Value ? (string)rd[0] : "";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }        
        }

        public async Task<List<ProfileKZEmployments>> GetPeople()
        {
            List<ProfileKZEmployments> People = new List<ProfileKZEmployments>();
            string sSql = @"select convert(varchar(20),DateBegin,104) datestat, Quantity from
	                        (select h.DateBegin, q.Quantity
		                        from KZ.dbo.ut_HistEnterpriseSize h
		                        join KZ.dbo.ut_ScaleEntQuantity q on h.Id_Quantity=q.Id
		                        where Code_Main=@Code)prof1
		                        order by DateBegin";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        while (await rd.ReadAsync())
                        {
                            People.Add(new ProfileKZEmployments() { 
                                date = rd.ReadEmptyIfDbNull(0),
                                pcount = rd.ReadEmptyIfDbNull(1)
                            });
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            return People;
        }

        public async Task<List<string>> GetDeals()
        {
            List<string> deals = new List<string>();
            string sSql = @"SELECT STUFF((select ', ' + rc.Name from KZ.dbo.ut_HoldEntCountry ec  
				join KZ.dbo.ut_RfCountry rc on rc.Id=ec.Id_Country
				where ec.Code_Main=@Code ),1,1,'')";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        while (await rd.ReadAsync())
                        {
                            deals.Add(rd.ReadEmptyIfDbNull(0));
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            return deals;
        }

        private async Task<List<DateReps>> GetControlDates(string date)
        {
            List<DateReps> dates = new List<DateReps>();
            string sSql = @"	select distinct Convert(varchar(20),DateBegin,104) as daterep,DateBegin,
	                    case Convert(varchar(20),DateBegin,104) when @Date then 1 else 0 end as cur
	                    from kz.dbo.ut_HistEnterpriseMgr periods where Code_Main=@Code order by DateBegin desc";
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = date;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        while (await rd.ReadAsync())
                        {
                            dates.Add(new DateReps() { daterep = rd.ReadEmptyIfDbNull(0),cur = (int)rd.GetSqlInt32(2)});
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            return dates;
        }

        private async Task<string> GetDealManager(string date)
        {
            string sSql = @"select Manager from kz.dbo.ut_HistEnterpriseMgr part1 
	                    where Code_Main=@Code and Convert(varchar(20),DateBegin,104)=@Date ";

            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = _code;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = date;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rd.ReadAsync())
                        {
                            return rd[0] != DBNull.Value ? (string)rd[0] : "";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}