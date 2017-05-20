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
    public class ProfileKzRepository
    {
        private string _code;
        const string REPLACER = "*****";
        const string DATEREPLACER = "**.**.****";
        private ProfileKZ Prof = new ProfileKZ();
        private static readonly string _constring = Configs.ConnectionString;

        public ProfileKzRepository(string Code)
        {
            _code = Code;
        }

        #region close

        public ProfileKZ GetClosedProfile()
        {
            Prof.Code = _code;
            GetFakeProfile();

            return Prof;
        }

        private void GetFakeProfile()
        {
            Prof.address = REPLACER;
            Prof.area = REPLACER;
            Prof.fulladdress = REPLACER;
            Prof.fullname = REPLACER;
            Prof.main_deal = REPLACER;
            Prof.name = REPLACER;
            Prof.name_short = REPLACER;
            Prof.phone = REPLACER;
            Prof.reg_number = REPLACER;
            Prof.stat = REPLACER;
            Prof.updatedate = DATEREPLACER;
        }

        #endregion

        #region open

        public ProfileKZ GetProfile()
        {
            Prof.Code = _code;
            GetProfileMainData();

            return Prof;
        }

        public void GetProfileMainData()
        {
            string sSql = @"select re.NameFull fullname, re.NameShort name_short, KZ.dbo.getKatoMainRegion(re.SearchKATO) as area,	
                            fulladdress=case  when re.Code_KATO is null then Post+', '+ KZ.dbo.getfullPlace(Id_Place)+' '+ Address else KZ.dbo.getKatoPath(re.Code_KATO) end,
                            phone,
	                        main_deal=case
		                        when re.Code_OKED is not null
			                        then do.Name
			                        else (select top 1 Dscr from KZ.dbo.ut_HistEnterpriseAct ha
					                        join KZ.dbo.ut_ClssActivity ca
					                        on ca.Id=ha.Id_Activity
					                        where Code_Main=@Code 
					                        order by DateBegin desc) collate database_default end,
	                        es.Dscr  stat,
	                        CodeBIN reg_number,
	                        (SELECT isnull(Post+' ','') +
	                        isnull((select rp.Name collate database_default from KZ.dbo.ut_RfPlace  rp where rp.Id=re.Id_Place)+', ','')+
	                         +Address) AS address,
	                        Convert(varchar(20),UpdatedDate,104)  AS updatedate
		                        from kz.dbo.ut_RfEnterprise re
		                        left join kz.dbo.Dic_OKED do on re.Code_OKED=do.Code
		                        left join kz.dbo.ut_ClssEntState es on re.Id_State=es.Id
	                        where re.Code=@Code";


            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand(sSql, con))
            {
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Code", SqlDbType.VarChar, 15).Value = Prof.Code;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            Prof.fullname = rd.ReadEmptyIfDbNull("fullname");
                            Prof.name_short = rd.ReadEmptyIfDbNull("name_short");
                            Prof.area = rd.ReadEmptyIfDbNull("area");
                            Prof.fulladdress = rd.ReadEmptyIfDbNull("fulladdress");
                            Prof.phone = rd.ReadEmptyIfDbNull("Phone");
                            if (!String.IsNullOrEmpty(Prof.phone)) Prof.phone = (Prof.phone.IndexOf(",") == Prof.phone.Length - 1) ? Prof.phone.Substring(0,Prof.phone.Length - 1) : Prof.phone;
                            Prof.main_deal = rd.ReadEmptyIfDbNull("main_deal");
                            Prof.stat = rd.ReadEmptyIfDbNull("stat");
                            Prof.reg_number = rd.ReadEmptyIfDbNull("reg_number");
                            Prof.address = rd.ReadEmptyIfDbNull("address");
                            Prof.updatedate = rd.ReadEmptyIfDbNull("updatedate");
                        }
                        rd.Close();
                    }
                }
                finally
                {
                    con.Close();

                }
            }

        }

        #endregion

    }
}