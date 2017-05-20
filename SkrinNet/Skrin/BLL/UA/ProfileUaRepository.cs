using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Skrin.Models.UA;

namespace Skrin.BLL.UA
{
    public class ProfileUaRepository
    {
        private readonly string _edrpou;
        private static readonly string _constring = Configs.ConnectionString;
        
        private static readonly bool _need_watching = Configs.NeedWatchingTimout;
        private readonly string REPLACER = Configs.Replacer;
        private readonly string DATEREPLACER = Configs.DateReplacer;

        private UAProfile _profile = new UAProfile();

//        private static readonly List<string> _logos = new List<string> { "AFLT.png", "AVAZ.png", "sanve.png", "kzos.jpg", "KZMS.jpg", "NORTK.png", "SZTT.png", "TAYME.jpg", "tatn.png", "cppsk.png" };

        public ProfileUaRepository(string edrpou)
        {
            _edrpou = edrpou;
        }

        #region open

        public UAProfile GetProfile()
        {
            
            //_ActionWrapper(_GetMainInfo);
            Action[] actions = new Action[]{
                _GetMainInfo,
                _GetOkveds
            };

            List<Action> wrapped_actions = new List<Action>();

            foreach (var action in actions)
            {
                wrapped_actions.Add(() =>
                {
                    _ActionWrapper(action);
                });
            }

            Parallel.Invoke(wrapped_actions.ToArray());
            return _profile;
        }


        private void _GetMainInfo()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("ua3..content_get_uaprofile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 32).Value = _edrpou;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.edrpou = _edrpou;
                        _profile.source_id = (int)rd.ReadNullIfDbNull("sourceid");
                        _profile.name = rd.ReadEmptyIfDbNull("name");
                        _profile.short_name = rd.ReadEmptyIfDbNull("shortname");
                        _profile.opf = (int?)rd.ReadNullIfDbNull("opf") ;
	                    _profile.opf_descr = rd.ReadEmptyIfDbNull("opf_descr");
                        _profile.address = rd.ReadEmptyIfDbNull("address");
                        _profile.area_id = (int?)rd.ReadNullIfDbNull("areaid");
                        _profile.area_name = rd.ReadEmptyIfDbNull("area_name");
                        _profile.post_index = rd.ReadEmptyIfDbNull("PostIndex");
                        _profile.region = rd.ReadEmptyIfDbNull("region");
                        _profile.city = rd.ReadEmptyIfDbNull("city");
                        _profile.street = rd.ReadEmptyIfDbNull("street");
                        _profile.building = rd.ReadEmptyIfDbNull("building");
                        _profile.corpus = rd.ReadEmptyIfDbNull("corpus");
                        _profile.flat = rd.ReadEmptyIfDbNull("flat");
                        _profile.phone = rd.ReadEmptyIfDbNull("phone");
                        _profile.fax = rd.ReadEmptyIfDbNull("fax");
                        _profile.web = rd.ReadEmptyIfDbNull("web");
                        _profile.email = rd.ReadEmptyIfDbNull("email");
                        _profile.ruler_name = rd.ReadEmptyIfDbNull("rulername");
                        _profile.ruler_title = rd.ReadEmptyIfDbNull("rulertitle");
                        _profile.regno = rd.ReadEmptyIfDbNull("regno");
                        _profile.regdate = rd.ReadEmptyIfDbNull("regdate");
                        _profile.regorg = rd.ReadEmptyIfDbNull("regorg");
                        _profile.koatuu = rd.ReadEmptyIfDbNull("koatuu");
                        _profile.koatuu_name = rd.ReadEmptyIfDbNull("koatuu_name") ;
                        _profile.kfv = rd.ReadEmptyIfDbNull("kfv");
	                    _profile.kfv_descr = rd.ReadEmptyIfDbNull("kfv_descr");
                        _profile.spodu = rd.ReadEmptyIfDbNull("spodu");
	                    _profile.spodu_descr = rd.ReadEmptyIfDbNull("spodu_descr");
                        _profile.codu = rd.ReadEmptyIfDbNull("codu");
	                    _profile.codu_descr = rd.ReadEmptyIfDbNull("codu_descr");
                        _profile.main_kved = rd.ReadEmptyIfDbNull("mainkved");
                        _profile.main_kved_descr = rd.ReadEmptyIfDbNull("mainkveddescr");
                    }
                }
            }
        }

        private void _ActionWrapper(Action action)
        {
            if (!_need_watching)
            {
                action();
            }
            else
            {
                string action_name = action.Method.Name;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                action();
                stopwatch.Stop();
//                _profile.timeouts.Add(action_name, stopwatch.ElapsedMilliseconds);
            }
        }

        private void _GetOkveds()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("ua3..content_get_uaprofile_kveds", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@edrpou", _edrpou);
                con.Open();
                _profile.all_kveds = new List<Kved>();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _profile.all_kveds.Add(new Kved
                        {
                            kod = rd.ReadEmptyIfDbNull("KvedCode"),
                            name = rd.ReadEmptyIfDbNull("CodeDescr"),
                            is_main = (int)rd.ReadNullIfDbNull("IsMain"),
                        });
                    }
                }
            }
        }
        #endregion open
        #region close

        public UAProfile GetClosedProfile()
        {

            _GetCloseMainInfo();
            //_GetOkveds()
            _profile.all_kveds = new List<Kved>{
                new Kved{kod=REPLACER,name=REPLACER},new Kved{kod=REPLACER,name=REPLACER}
            };
            return _profile;
        }

        private void _GetCloseMainInfo()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("ua3..content_get_uaprofile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@edrpou", SqlDbType.VarChar, 32).Value = _edrpou;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        _profile.edrpou = _edrpou;
                        _profile.source_id = (int)rd.ReadNullIfDbNull("sourceid");
                        _profile.name = rd.ReadEmptyIfDbNull("name");
                        _profile.short_name = rd.ReadEmptyIfDbNull("shortname");
                        _profile.opf = (int?)rd.ReadNullIfDbNull("opf") ;
	                    _profile.opf_descr = rd.ReadEmptyIfDbNull("opf_descr");
                        _profile.address = rd.ReadEmptyIfDbNull("address");
                        //--
                        _profile.area_id = (int?)rd.ReadNullIfDbNull("areaid");
                        _profile.area_name = rd.ReadEmptyIfDbNull("area_name");
                        _profile.post_index = rd.ReadEmptyIfDbNull("PostIndex");
                        _profile.region = rd.ReadEmptyIfDbNull("region");
                        _profile.city = rd.ReadEmptyIfDbNull("city");
                        _profile.street = rd.ReadEmptyIfDbNull("street");
                        _profile.building = rd.ReadEmptyIfDbNull("building");
                        _profile.corpus = rd.ReadEmptyIfDbNull("corpus");
                        _profile.flat = rd.ReadEmptyIfDbNull("flat");
                        //--
                        _profile.phone = rd.ReadEmptyIfDbNull("phone");
                        _profile.fax = rd.ReadEmptyIfDbNull("fax");
                        _profile.web = REPLACER;
                        _profile.email = REPLACER;

                        _profile.ruler_name = REPLACER;
                        _profile.ruler_title = REPLACER;
                        _profile.regno = REPLACER;
                        _profile.regdate = REPLACER;
                        _profile.regorg = REPLACER;
                        _profile.koatuu = REPLACER;
                        _profile.koatuu_name = REPLACER ;
                        _profile.kfv = REPLACER;
	                    _profile.kfv_descr = REPLACER;
                        _profile.spodu = REPLACER;
	                    _profile.spodu_descr = REPLACER;
                        _profile.codu = REPLACER;
	                    _profile.codu_descr = REPLACER;
                        _profile.main_kved = REPLACER;
                        _profile.main_kved_descr = REPLACER;

/*

                            ticker = _ticker,
                            name = rd.ReadEmptyIfDbNull("NAME"),
                            short_name = rd.ReadEmptyIfDbNull("short_name"),
                            legal_address = rd.ReadEmptyIfDbNull("legal_address"),
                            legal_phone = rd.ReadEmptyIfDbNull("legal_phone"),
                            legal_fax = rd.ReadEmptyIfDbNull("legal_fax"),
                            legal_email = REPLACER,
                            www = REPLACER,
                            inn = rd.ReadEmptyIfDbNull("inn"),
                            ogrn = rd.ReadEmptyIfDbNull("ogrn"),
                            ogrn_date = DATEREPLACER,
                            reg_org = REPLACER,
                            fcsm_code = REPLACER,
                            first_okved = REPLACER,
                            region = REPLACER,
                            gks_id = (string)rd.ReadNullIfDbNull("gks_id"),
                            id = (int)rd["id"],
                            okato = REPLACER,
                            opf = (int?)rd.ReadNullIfDbNull("opf"),
                            issuer_id = rd.ReadEmptyIfDbNull("issuer_id"),
                            uniq = ((byte?)rd.ReadNullIfDbNull("uniq") == 1),
                            group_id = (int)rd["group_id"],
                            okpo = rd.ReadEmptyIfDbNull("okpo"),
                            kpp = rd.ReadEmptyIfDbNull("kpp"),
                            type_id = (int)rd["type_id"],
                            //logo_path = (_logos.Contains(_ticker.ToUpper()) && (Int16?)rd.ReadNullIfDbNull("information") == 4) ? string.Format("<img border=\"0\" src=\"/logos/{0}.jpg\" alt=\"logo\"/>", _ticker) : null,
                            ul2_ogrn = (string)rd.ReadNullIfDbNull("ul2_ogrn"),
                            src = rd.ReadEmptyIfDbNull("src")

                        }; */
                    }
                }
            }
        }
        #endregion
    }
}