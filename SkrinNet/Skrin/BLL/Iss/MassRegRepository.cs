using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using SkrinService.Domain.AddressSearch.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Skrin.BLL.Iss
{
    public class MassRegRepository
    {
        private string _connectionString;
        private string ogrn;
        private string inn;


        public MassRegRepository(string ticker)
        {
            _connectionString = Configs.ConnectionString;
            GetCodes(ticker);
        }

        public QueueStatus GetQueueStatus()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2..getQueueStatusAll_Address", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 10).Value = inn;
                con.Open();
                QueueStatus status = (QueueStatus)cmd.ExecuteScalar();
                return status;
            }
        }

        public string GetAddress()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2..massreg_getAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 10).Value = inn;
                con.Open();
                return (string)cmd.ExecuteScalar();
            }
        }

        private void GetCodes(string ticker)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"select isnull(ogrn,'') ogrn, isnull(inn,'')inn from searchdb2.dbo.union_search
                            where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ogrn = (string)reader["ogrn"];
                    inn = (string)reader["inn"];
                }
                if (ogrn == "0")
                    ogrn = "";
            }
        }

        private string SerializeJson(HomeListEdit hl)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string json = ser.Serialize(hl);
            return json;
        }

        private HomeListEdit DeserializeJson(string json)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            HomeListEdit hl = ser.Deserialize<HomeListEdit>(json);
            return hl;
        }

        public HomeListEdit GetReady()
        {
            HomeListEdit hl = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.massreg_getready", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 10).Value = inn;
                con.Open();
                string json = (string)cmd.ExecuteScalar();
                if (!string.IsNullOrEmpty(json))
                    hl = DeserializeJson(json);
            }
            return hl;
        }

        public string GetExeption()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.massreg_getexeption", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar, 10).Value = inn;
                con.Open();
                return (string)cmd.ExecuteScalar();
            }
        }

        public void SetReady(HomeListEdit hl)
        {
            if (hl != null)
            {
                string json = SerializeJson(hl);
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("fsns2.dbo.massreg_setready", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                    cmd.Parameters.Add("@inn", SqlDbType.VarChar, 10).Value = inn;
                    cmd.Parameters.Add("@search_result", SqlDbType.VarChar).Value = json;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Добавление в очередь поиска массовой ренистрации по найденному адресу
        /// </summary>
        public void InsertInMassRegQueue()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"if not exists (select 1 from fsns2.dbo.massreg_queue where ogrn=@ogrn)
                    insert into fsns2.dbo.massreg_queue (ogrn) values (@ogrn)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar, 13).Value = ogrn;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}