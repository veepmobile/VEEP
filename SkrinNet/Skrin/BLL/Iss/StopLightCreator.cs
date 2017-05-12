using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using serch_contra_bll.StopLightFreeEgrul;
using Skrin.Controllers;
using Skrin.Models.Iss.StopLight;

namespace Skrin.BLL
{
    /// <summary>
    /// Фабрика создания светофора для Pdf отчета
    /// </summary>
    public class StopLightCreator
    {
        private static string _constring = Configs.ConnectionString;
        private string _ogrn;


        public StopLightCreator(string ticker)
        {
            _GetOgrn(ticker);
        }

        public StopLight GetStopLight()
        {
            if (string.IsNullOrEmpty(_ogrn))
                return null;
            StopLightData data = new StopLightData(_ogrn, _constring);
            if (data == null)
                return null;
            StopLight sl = new StopLight();

            //sl.RedStops=

            sl.RedStops = data.Factors.Where(p => p.Value.IsUnconditional).Select(p => new StopLightRowData()
            {
                cstop = p.Value.IsStoped ? "1" : "0",
                phead = p.Value.Name,
                data = p.Value.Info
            }).ToList();
            sl.YellowStops = data.Factors.Where(p => p.Value.IsUnconditional == false).Select(p => new StopLightRowData()
            {
                cstop = p.Value.IsStoped ? "1" : "0",
                phead = p.Value.Name,
                data = p.Value.Info
            }).ToList();
            sl.header = new StopLightHeaderData();
            switch (data.Rating)
            {
                case ColorRate.Green:
                    sl.header.green = "111";
                    sl.header.txt = "Контрагент надежный";
                    break;
                case ColorRate.Red:
                    sl.header.red = "111";
                    sl.header.txt = "Внимание, безусловный стоп-сигнал.\nКоличество найденных факторов: " + sl.RedStops.Count(p => p.cstop == "1");
                    break;
                case ColorRate.Yellow:
                    sl.header.yellow = "111";
                    sl.header.txt = "Внимание, условный стоп-сигнал.\nКоличество найденных факторов: " + sl.YellowStops.Count(p => p.cstop == "1");
                    break;
            }
            sl.header.rating_date = DateTime.Now.AddDays(-1).ToRusString();
            return sl;
        }

        private void _GetOgrn(string ticker)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT ogrn from searchdb2..union_search where ticker=@ticker and uniq=1", con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = ticker;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    _ogrn = (string)reader.ReadNullIfDbNull(0);
                }

            }
        }


    }
}
