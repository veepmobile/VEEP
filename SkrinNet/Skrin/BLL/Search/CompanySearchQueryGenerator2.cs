using Skrin.BLL.Infrastructure;
using Skrin.Models;
using Skrin.Models.Iss.Content;
using Skrin.Models.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Nest;

namespace Skrin.BLL.Search
{

    using NestFuncQuery = Func<QueryContainerDescriptor<SearchListItem>, QueryContainer>;

    public class CompanySearchQueryGenerator2
    {
        private CompaniesSearchObject _so;
        private string connection_string = Configs.ConnectionString;
        //private int _group_limit;

        public CompanySearchQueryGenerator2(CompaniesSearchObject so,int group_limit)
        {
            _so = so;
            //_group_limit = group_limit;
        }

        public Func<SearchDescriptor<SearchListItem>, ISearchRequest> GetQuery()
        {
            int count = 100;
            if (_so.rcount == 0)
            {
                count = 30;
            }
            int f = (_so.page_no - 1) * count;


            List<NestFuncQuery> queries = new List<Func<QueryContainerDescriptor<SearchListItem>, QueryContainer>>();
            List<NestFuncQuery> mustnot = new List<Func<QueryContainerDescriptor<SearchListItem>, QueryContainer>>();
            List<NestFuncQuery> filter = new List<Func<QueryContainerDescriptor<SearchListItem>, QueryContainer>>();

            //company
            NestFuncQuery _company;
            if (!String.IsNullOrWhiteSpace(_so.company))
            {
                string name = "";
                string opf = "";
                SplitOpf(_so.company, out name, out opf);
                var list = new List<NestFuncQuery>();
                if (!String.IsNullOrWhiteSpace(name))
                {
                    list.Add(s => s
                        .Match(mp => mp
                            .Field(p => p.key_list.Suffix("key_value"))
                            .Query(name.Replace("\\", "\\\\").Replace("\"", ""))));

                }

                if (!String.IsNullOrWhiteSpace(opf))
                {
                    list.Add(s => s 
                        .Match(mp => mp
                            .Field(p => p.key_list.Suffix("key_value"))
                            .Query(opf)));
                }

                if (_so.archive == 1)
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("key_list.is_old")
                            .Value("false")));
                }

                _company = (s => s
                    .Nested(n => n
                        .Path("key_list")
                        .Query(q => q
                            .Bool(b => b
                                .Must(list.ToArray())))
                        .InnerHits(ih => ih
                            .Sort(ss => ss
                                .Ascending(p => p.key_list.Suffix("is_old"))))));

                queries.Add(_company);
            }

            //ruler
            if (!String.IsNullOrWhiteSpace(_so.ruler))
            {
                NestFuncQuery _ruler;
                List<Field> fields = new List<Field> {"manager_list.fio","manager_list.inn","manager_list.position"};
                var list = new List<NestFuncQuery>();
                list.Add(s => s
                    .MultiMatch(mm => mm
                        .Fields(fields.ToArray())
                        .Query(_so.ruler)
                        .Type(TextQueryType.CrossFields)
                        .Operator(Operator.And)));

                if (_so.archive == 1)
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("manager_list.is_old")
                            .Value("false")));
                }

                _ruler = (s => s
                    .Nested(n => n
                        .Path("manager_list")
                        .Query(q => q
                            .Bool(b => b
                                .Must(list.ToArray())))
                        .InnerHits(ih => ih
                            .Sort(ss => ss
                                .Ascending(p => p.manager_list.Suffix("is_old"))))));

                queries.Add(_ruler);
            }

            //constitutor
            if (!String.IsNullOrWhiteSpace(_so.constitutor))
            {
                NestFuncQuery _constitutor;
                List<Field> fields = new List<Field> { "constitutor_list.name","constitutor_list.inn","constitutor_list.ogrn" };
                var list = new List<NestFuncQuery>();
                list.Add(s => s
                    .MultiMatch(mm => mm
                        .Fields(fields.ToArray())
                        .Query(_so.constitutor)
                        .Type(TextQueryType.CrossFields)
                        .Operator(Operator.And)));

                if (_so.archive == 1)
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("constitutor_list.is_old")
                            .Value("false")));
                }

                _constitutor = (s => s
                    .Nested(n => n
                        .Path("constitutor_list")
                        .Query(q => q
                            .Bool(b => b
                                .Must(list.ToArray())))
                        .InnerHits(ih => ih
                            .Sort(ss => ss
                                .Ascending(p => p.manager_list.Suffix("is_old"))))));

                queries.Add(_constitutor);
            }

            //address
            if (!String.IsNullOrWhiteSpace(_so.address))
            {
                NestFuncQuery _address;
                var list = new List<NestFuncQuery>();
                list.Add(s => s
                    .Match(mp => mp
                        .Field(p => p.address_list.Suffix("address"))
                        .Query(_so.address)
                        .Operator(Operator.And)));

                if (_so.archive == 1)
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("address_list.is_old")
                            .Value("false")));
                }

                _address = (s => s
                    .Nested(n => n
                        .Path("address_list")
                        .Query(q => q
                            .Bool(b => b
                                .Must(list.ToArray())))
                        .InnerHits(ih => ih
                            .Sort(ss => ss
                                .Ascending(p => p.address_list.Suffix("is_old"))))));

                queries.Add(_address);
            }

            //phone
            if (!String.IsNullOrWhiteSpace(_so.phone))
            {
                string phone = _so.phone;
                phone = phone.Replace("+7", "8");
                if (phone.Length > 10)
                    if (phone.Substring(0, 1) == "7" || phone.Substring(0, 1) == "8") phone = phone.Substring(1);
                NestFuncQuery _phone;
                var list = new List<NestFuncQuery>();
                list.Add(s => s
                    .Wildcard(w =>w
                        .Field(new Field("phone_list.phone"))
                        .Value(phone)));

                if (_so.archive == 1)
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("phone_list.is_old")
                            .Value("false")));
                }

                _phone = (s => s
                    .Nested(n => n
                        .Path("phone_list")
                        .Query(q => q
                            .Bool(b => b
                                .Must(list.ToArray())))
                        .InnerHits(ih => ih
                            .Sort(ss => ss
                                .Ascending(p => p.phone_list.Suffix("is_old"))))));

                queries.Add(_phone);
            }

            //opf
            if (!String.IsNullOrWhiteSpace(_so.okopf))
            {
                List<int> opf = new List<int>();
                var okopf = _so.okopf.Split('|');
                foreach(var item in okopf)
                {
                    opf.Add(Int32.Parse(item));
                }

                if (_so.okopf_excl == 0)
                {
                    filter.Add(s => s
                        .Terms(p => p
                            .Name("opf")
                            .Terms(opf.ToArray())));
                }
                else
                {
                    mustnot.Add(s => s
                        .Terms(p => p
                            .Name("opf")
                            .Terms(opf.ToArray())));
                }
            }

            //okfs
            if (!String.IsNullOrWhiteSpace(_so.okfs))
            {
                List<int> okfs_list = new List<int>();
                var okfs = _so.okfs.Split('|');
                foreach (var item in okfs)
                {
                    okfs_list.Add(Int32.Parse(item));
                }

                if (_so.okfs_excl == 0)
                {
                    filter.Add(s => s
                        .Terms(p => p
                            .Name("okfs")
                            .Terms(okfs_list.ToArray())));
                }
                else
                {
                    mustnot.Add(s => s
                        .Terms(p => p
                            .Name("okfs")
                            .Terms(okfs_list.ToArray())));
                }
            }

            //industry
            if (!String.IsNullOrWhiteSpace(_so.industry))
            {
                List<int> okved_list = new List<int>();
                var okved = _so.industry.Split('|');
                foreach (var item in okved)
                {
                    okved_list.Add(Int32.Parse(item));
                }
                if (_so.ind_main == 1)
                {
                    if (_so.ind_excl == 0)
                    {
                        filter.Add(s => s
                            .Terms(p => p
                                .Name("okved_id_list")
                                .Terms(okved_list.ToArray())));
                    }
                    else
                    {
                        mustnot.Add(s => s
                            .Terms(p => p
                                .Name("okved_id_list")
                                .Terms(okved_list.ToArray())));
                    }
                }
                else
                {
                    NestFuncQuery _okved;
                        _okved = (s => s
                            .Nested(n => n
                                .Path("okved_list")
                                .Query(q => q
                                    .Bool(b => b
                                        .Must(m => m
                                            .Terms(p => p
                                                .Name("okved_id_list")
                                                .Terms(okved_list.ToArray())))))
                                .InnerHits()));
                    if (_so.ind_excl == 0)
                    {
                        queries.Add(_okved);
                    }
                    else
                    {
                        mustnot.Add(_okved);
                    }

                }
            }


            //regions
            if (!String.IsNullOrWhiteSpace(_so.regions))
            {
                if (_so.is_okato == 0)
                {
                    List<int> regions_list = new List<int>();
                    var regions = _so.regions.Split('|');
                    foreach (var item in regions)
                    {
                        regions_list.Add(Int32.Parse(item));
                    }
                    if (_so.reg_excl == 0)
                    {
                        filter.Add(s => s
                            .Terms(p => p
                                .Name("region_id")
                                .Terms(regions_list.ToArray())));
                    }
                    else
                    {
                        mustnot.Add(s => s
                            .Terms(p => p
                                .Name("region_id")
                                .Terms(regions_list.ToArray())));
                    }
                }
                else
                {
                    List<string> okato_list = new List<string>();
                    var regions = _so.regions.Split('|');
                    foreach (var item in regions)
                    {
                        okato_list.Add(item.ToString());
                    }
                    if (_so.reg_excl == 0)
                    {
                        filter.Add(s => s
                            .Terms(p => p
                                .Name("okato_list")
                                .Terms(okato_list.ToArray())));
                    }
                    else
                    {
                        mustnot.Add(s => s
                            .Terms(p => p
                                .Name("okato_list")
                                .Terms(okato_list.ToArray())));
                    }
                }
            }

            //trades
            if (!String.IsNullOrWhiteSpace(_so.trades))
            {
                var list = new List<NestFuncQuery>();

                if (_so.trades.Substring(1, 1) == "1")
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("is_mmvb")
                            .Value("true")));
                }
                if (_so.trades.Substring(2, 1) == "1")
                {
                    list.Add(s => s
                        .Term(p => p
                            .Name("is_rtsboard")
                            .Value("true")));
                }
                if(list.Count > 0)
                {
                    NestFuncQuery _trades;
                    _trades = (s => s
                        .Bool(b => b
                            .Should(list.ToArray())
                            .MinimumShouldMatch(1)));

                    queries.Add(_trades);
                }
            }

            //gaap
            if (_so.gaap == 1)
            {
                filter.Add(s => s
                        .Term(p => p
                            .Name("is_gaap")
                            .Value("true")));
            }

            //bankrupt
            if (_so.bankrupt == 1)
            {
                filter.Add(s => s
                        .Term(p => p
                            .Name("is_nedobr")
                            .Value("true")));
            }

            //rgstr
            if (_so.rgstr == 1)
            {
                filter.Add(s => s
                        .Term(p => p
                            .Name("is_gks")
                            .Value("true")));
            }

            //msp
            if (_so.msp == 1)
            {
                List<int> msp = new List<int>{1,2,3};
                filter.Add(s => s
                            .Terms(p => p
                                .Name("msp_type")
                                .Terms(msp.ToArray())));
            }

            //rsbu
            if (_so.rsbu == 1)
            {
                filter.Add(s => s
                        .Term(p => p
                            .Name("rsbu_year")
                            .Value(2015)));
            }

            //status
            if (_so.status == 1)
            {
                filter.Add(s => s
                        .Term(p => p
                            .Name("status")
                            .Value(0)));
                filter.Add(s => s
                    .Exists(ex => ex
                        .Field(new Field("ogrn"))));
            }

            //filials
            if (_so.filials > 0)
            {
                mustnot.Add(s => s
                        .Term(p => p
                            .Name("opf")
                            .Value(90)));
            }

            //fas
            if (!String.IsNullOrWhiteSpace(_so.fas))
            {
                List<int> fas_list = new List<int>();
                var fas = _so.fas.Split('|');
                foreach (var item in fas)
                {
                    fas_list.Add(Int32.Parse(item));
                }
                if (_so.fas_excl == 0)
                {
                    filter.Add(s => s
                        .Terms(p => p
                            .Name("fas_list")
                            .Terms(fas_list.ToArray())));
                }
                else
                {
                    mustnot.Add(s => s
                        .Terms(p => p
                            .Name("fas_list")
                            .Terms(fas_list.ToArray())));
                }
            }

            //group
            if (_so.group_id != 0)
            {
                List<string> gl = GetGroupList(_so.group_id, connection_string);
                if (gl.Count > 0)
                {
                    filter.Add(s => s
                        .Terms(p => p
                            .Name("ticker")
                            .Terms(gl.ToArray())));
                }
            }



            return (s => s
                .Type("_search")
                .Query(q => q
                    .Bool(b => b
                        .Must(queries.ToArray())
                        .MustNot(mustnot.ToArray())
                        .Filter(filter.ToArray())
                    )
                )
                .From(f)
                .Size(count)
                .Sort(ss => ss
                    .Ascending(p => p.status)
                    .Descending(p => p.is_gks)
                    .Descending(p => p.uniq)
                    .Script( c => c
                        .Type("number")
                        .Script( sc => sc
                            .Inline("def sc = 0; sc=doc['group_id'].value; if (sc>1) {sc=2} sc=2-sc; sc=sc*2+_score; return sc")
                            .Lang("painless")
                            )
                        .Descending()
                    )
                    .Ascending(p => p.bones)
                )
            );

        }







        private static void SplitOpf(string company, out string name, out string opf)
        {
            string[] opf_list = { "АО", "ООО", "ЗАО", "ПАО", "АООТ", "АОЗТ", "СОАО", "ОАО", "ТОО", "ГУП", "ФГУП" };
            name = company;
            opf = "";
            int n = company.IndexOf(" ");
            if (n > 0)
            {
                string s = company.Substring(0, n).ToUpper();
                for (int i = 0; i < opf_list.Count(); i++)
                {
                    if (opf_list[i] == s)
                    {
                        opf = s;
                        name = company.Substring(n + 1).Trim();
                        break;
                    }
                }
            }
        }

        private List<string> GetGroupList(int group_id, string connection_string)
        {
            if (group_id == 0)
                return null;

            List<string> res = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    string sql = "SELECT u.ticker from searchdb2..union_search u inner join security..secUserListItems_Join s ON u.issuer_id = s.IssuerID where s.ListID=@group_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@group_id", SqlDbType.BigInt).Value = group_id;
                    cmd.CommandTimeout = 300;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string ticker = (reader["ticker"]!=DBNull.Value)?(string)reader["ticker"]:"";
                        res.Add(ticker.ToUpper());
                    }
                }
            }
            catch
            {
                return null;
            }
            return res;
        }

        #region old
        /*
        public object GetQuery()
        {
            int count = 100;
            if (_so.rcount != null)
            {
                count = 30;
            }
            int f = (_so.page_no - 1) * count;

            return new
            {
                query = new
                {
                    @bool = _GetCondition()
                },
                size = count,
                from = f,
                sort = new object[]
                {
                    new { status = new { order = "asc" }},
                    new {is_gks = new { order = "desc" }},
                    new {uniq = new { order = "desc" }},
                    new {_script = new
                    {
                        type = "number",
                        script = new
                        {
                            lang = "painless",
                            inline = "def sc = 0; sc=doc['group_id'].value; if (sc>1) {sc=2} sc=2-sc; sc=sc*2+_score; return sc"
                        },
                        order = "desc"
                    }},
                    new {bones = new { order = "desc" }}
                },
                track_scores = "true"
            };
        }

        public object _GetCondition()
        {
            List<object> must = new List<object>();
            List<object> filter = new List<object>();
            List<object> must_not = new List<object>();

            //must

            if (!String.IsNullOrWhiteSpace(_so.company))
            {
                string name = "";
                string opf = "";
                SplitOpf(_so.company, out name, out opf);
                if (!String.IsNullOrWhiteSpace(name))
                {
                    List<object> nameObj = new List<object>();
                    //nameObj.Add(new { path = "key_list", inner_hits = new { sort = new { key_list.is_old = new { order = "asc"} } } });

                }





            }



            //filter
            DateTime? reg_DBeg = null;
            DateTime? reg_DEnd = null;
            try { reg_DBeg = DateTime.ParseExact(_so.dbeg, "dd.MM.yyyy", null); }
            catch { }
            try { reg_DEnd = DateTime.ParseExact(_so.dend, "dd.MM.yyyy", null); }
            catch { }

            if (reg_DBeg != null || reg_DEnd != null)
            {
                if (reg_DEnd == null) reg_DEnd = DateTime.ParseExact("01.01.2100", "dd.MM.yyyy", null);
                List<object> reg_date = new List<object>();
                if (reg_DBeg != null) { reg_date.Add(new { gte = String.Format("{0:dd.MM.yyyy}", reg_DBeg) }); }
                if (reg_DEnd != null) { reg_date.Add(new { lte = String.Format("{0:dd.MM.yyyy}", reg_DEnd) }); }
                if (reg_date.Count > 0) { filter.Add(new { range = new { reg_date } }); }
            }

            if (!String.IsNullOrWhiteSpace(_so.okopf))
            {
                var opf = (_so.okopf).Split('|').Distinct();
                List<int> opf_list = new List<int>();
                foreach (var item in opf)
                {
                    opf_list.Add(Convert.ToInt32(item));
                }
                if (_so.okopf_excl == 0)
                {
                    filter.Add(new { terms = new { opf = opf_list.ToArray() } });
                }
                else
                {
                    must_not.Add(new { terms = new { opf = opf_list.ToArray() } });
                }
            }







            //must_not





            //object[] mustObj = must.ToArray();
            //object[] filterObj = filter.ToArray();
            //object[] must_notObj = must_not.ToArray();

            return new
            {
                must = must.ToArray(),
                filter = filter.ToArray(),
                must_not = must_not.ToArray()
            };
        }
        */
        #endregion


    }
}