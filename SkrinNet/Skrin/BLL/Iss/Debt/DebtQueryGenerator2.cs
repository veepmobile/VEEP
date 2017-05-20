using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Debt;
using SKRIN;
using Nest;

namespace Skrin.BLL.Iss.Debt
{
    using NestFuncQuery = Func<QueryContainerDescriptor<DebtorListItem>, QueryContainer>;
    using NestFuncSearch = Func<SearchDescriptor<DebtorListItem>, ISearchRequest>;

    public class DebtorListItem
    {
        public int id;
        public string a_cityname;
        public string a_distrname;
        public string a_dopinfo;
        public string a_localityname;
        public string a_housenum;
        public int a_regid;
        public string a_regname;
        public string a_streetname;
        public string a_zip;
        public string debtor;
        public string adress;
        public string nproizv;
        public DateTime date1_ts;
        public string predmet;
        public double summ;
        public double summ1;
        public string region_id;
        public string name;
        public string cause;
        public string date3;
        public string debtoranalyzed;
        public string adressanalyzed;
        public string deletedate;
        public byte version;
        public DebtorSearchType searchtype;
    }

    

    public class DebtorSearchResult
    {
        public int total_found;
        public DebtorListItem[] items;
    }

    public class DebtorId
    {
        public long id;
        public byte ver;
    }

    public class DebtElasticRegion
    {
        public string region_id;
        public string name;
    }

    public class DebtElasticPredmet
    {
        public string predmet;
    }

    public class DebtElasticAddress
    {
        public string address;
    }

    public class DebtElasticDebtor
    {
        public string debtor;
    }

    public class DebtElasticSummary
    {
        public int year;
        public long? nowcnt;
        public double?  nowsum;
        public long? oldcnt;
        public double? oldsum;
    }

    public class DebtQueryGenerator2
    {
        private Skrin.Models.Iss.Debt.SearchObject _so;
        private CompanyData _company;
        private List<DebtorAdress> _adress;
        private List<DebtorName> _names;

        public DebtQueryGenerator2(Skrin.Models.Iss.Debt.SearchObject so, CompanyData company, List<DebtorAdress> adress, List<DebtorName> names)
        {
            //so.address = (!string.IsNullOrEmpty(so.address) ? System.Uri.UnescapeDataString(so.address) : so.address);
            //so.debtorName = (!string.IsNullOrEmpty(so.debtorName) ? System.Uri.UnescapeDataString(so.debtorName) : so.debtorName);
            //so.fullName = (!string.IsNullOrEmpty(so.fullName) ? System.Uri.UnescapeDataString(so.fullName) : so.fullName);
            //so.debtorstr = (!string.IsNullOrEmpty(so.debtorstr) ? System.Uri.UnescapeDataString(so.debtorstr) : so.debtorstr);
            so.predmet = (!string.IsNullOrEmpty(so.predmet) ? System.Uri.UnescapeDataString(so.predmet) : so.predmet);
            _so = so;
            if (_so.searchtypes == null) {
                _so.searchtypes = new List<DebtorSearchType> { DebtorSearchType.AdressAndName, DebtorSearchType.Adress, DebtorSearchType.Name};
            }
            _company = company;
            _adress = adress;
            _names = names;
        }

        public DebtQueryGenerator2(CompanyData company, List<DebtorAdress> adress, List<DebtorName> names)
        {
            _so = new Skrin.Models.Iss.Debt.SearchObject();
            _so.predmet = "-1";
            //_so.address = "-1";
            _so.region = "0";
            _so.version = -1;
            _so.searchtypes = new List<DebtorSearchType> { DebtorSearchType.AdressAndName, DebtorSearchType.Adress, DebtorSearchType.Name };

            _company = company;
            _adress = adress;
            _names = names;
        }

        public static DebtorListItem SetSearchTypeDelegate(DebtorListItem obj, IReadOnlyCollection<string> matched_queries)
        {
            var matchres = obj;
            matchres.searchtype = DebtorSearchType.None;
            if (matched_queries.Count > 0)
            {
                if (matched_queries.First() == "AdressAndName") matchres.searchtype = DebtorSearchType.AdressAndName;
                if (matched_queries.First() == "Adress") matchres.searchtype = DebtorSearchType.Adress;
                if (matched_queries.First() == "Name") matchres.searchtype = DebtorSearchType.Name;
            }
            return matchres;
        }

        public async static Task<List<DebtItem>> GetDetails(IEnumerable<DebtorId> list)
        {
            List<DebtItem> result = new List<DebtItem>();
            foreach (var d in list)
            {
                string query = @"
                    select a.*, 
                        b.name,
                        (select top 1 insert_date from OpenData..FSSP_FilesTable where IsExport=1 and type=0 order by insert_date desc) as uptodate 
                    from OpenData..FSSP{type} a 
                        LEFT OUTER JOIN naufor..Regions b on try_cast(a.region_id as int)=b.id 
                    where a.delete_date is null and a.Id=@id
                ".Replace("{type}", d.ver==1 ? "_Archive" : "");

                DebtItem debtor = new DebtItem();
                using (SQL sql = new SQL(Configs.ConnectionString))
                {
                    using (Query q = await sql.OpenQueryAsync(query, new SQLParamInt64("id", d.id)))
                    {
                        while (await q.ReadAsync())
                        {
                            result.Add(new DebtItem { 
                                Id = d.id,
                                Status = d.ver,
                                DebtorName = q.GetFieldAsString("debtor"),
                                DebtorAddress = q.GetFieldAsString("adress"),
                                NumProizv = q.GetFieldAsString("nProizv"),
                                DateProizv = q.GetFieldAsDateTimeNull("date1")!=null ? ((DateTime)q.GetFieldAsDateTimeNull("date1")).ToShortDateString() : "-",
                                NumSvodPr = q.GetFieldAsString("nSvodPr"),
                                DocumentType = q.GetFieldAsString("type") ?? "-",
                                DocumentNum = q.GetFieldAsString("nDoc") ?? "-",
                                DocumentDate = q.GetFieldAsDateTimeNull("date2")!=null ? ((DateTime)q.GetFieldAsDateTimeNull("date2")).ToShortDateString() : "-",
                                DocumentReq = q.GetFieldAsString("req") ?? "-",
                                Predmet = q.GetFieldAsString("predmet") ?? "-",
                                PristavName = q.GetFieldAsString("dep") ?? "-",
                                Region = q.GetFieldAsString("name") ?? "-",
                                PristavAddress = q.GetFieldAsString("adressPr") ?? "-",
                                UpdateDate = q.GetFieldAsDateTimeNull("date2")!=null ? ((DateTime)q.GetFieldAsDateTimeNull("uptodate")).ToShortDateString() : "-",
                                Sum = (d.ver == 0 ? q.GetFieldAsDecimal("summ")  : null),
                                CloseCause = (d.ver == 1 ? (q.GetFieldAsString("cause") ?? "-") : ""),
                                CloseDate = (d.ver == 1 ? (q.GetFieldAsDateTimeNull("date2")!=null ? ((DateTime)q.GetFieldAsDateTimeNull("uptodate")).ToShortDateString() : "-") : ""),
                            });
                        }
                    }
                }
            }
            return result;
        }

        public NestFuncSearch GetSearch()
        {
            int count;
            if (!Int32.TryParse(_so.step, out count))
            {
                count = 20;
            }
            int f = (_so.page - 1) * count;

            return (s => s
                .Type("debt")
                .Query(q => q
                    .Bool(b => b
                        .Must(_GetCondition())
                    )
                )
                .From(f)
                .Size(count)
                .Sort(ss => ss
                    .Descending(p => p.date1_ts)
                )
            );
        }


        public NestFuncSearch GetExport()
        {
            return (s => s
                .Type("debt")
                .Sort(ss => ss
                    .Descending(p => p.date1_ts)
                )
                .Source(src => src
                    .Includes(inc => inc
                        .Fields(p1 => p1.id, p2 => p2.version)
                    )
                )
                .Size(10000)
                .Query( q => q
                    .Bool( b => b
                        .Must(
                            _GetCondition()
                        )
                    )

                )
            );
        }

        public NestFuncSearch GetRegions()
        {
            return (s => s
                .Type("debt")
                .Size(0)
                .Query( q => q
                    .Bool( b => b
                        .Must(
                            _GetCondition()   
                        )
                    )
                )
                .Aggregations( a => a
                    .Terms("regions", t => t
                        .Field(p => p.region_id)
                        .Size(0)
                        .OrderAscending("_term")
                        .Aggregations( a2 => a2
                            .Terms("regionsnames", t2 => t2
                                .Field(p2 => p2.name)
                            )
                        )
                    )

                )
            );
        }

        public NestFuncSearch GetPredmets()
        {
            return (s => s
                .Type("debt")
                .Size(0)
                .Query( q => q
                    .Bool( b => b
                        .Must(
                            _GetCondition()
                        )
                    )
                )
                .Aggregations( a => a
                    .Terms("predmets", t => t
                        .Field(p => p.predmet)
                        .Size(0)
                    )
                )
            );
        }

        public NestFuncSearch GetAdressess()
        {
            return (s => s
                .Type("debt")
                .Size(0)
                .Query( q => q
                    .Bool( b => b
                        .Must(
                            _GetCondition()
                        )
                    )
                )
                .Aggregations(a => a
                    .Terms("addresses", t => t
                        .Field(p => p.adress)
                        .Size(0)
                    )
                )
            );
        }

        public NestFuncSearch GetDebtors()
        {
            return (s => s
                .Type("debt")
                .Size(0)
                .Query(q => q
                    .Bool( b => b
                        .Must(
                            _GetCondition()
                        )
                    )
                )
                .Aggregations( a => a
                    .Terms("debtors", t => t
                        .Field(p => p.debtor)
                        .Size(0)
                    )
                )
            );
        }

        public NestFuncSearch GetSummaryTable()
        {
            return (s => s
                .Type("debt")
                .Size(0)
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            _GetCondition()
                        )
                    )
                )
                .Aggregations(a => a
                    .DateHistogram("summarytable", d => d
                        .Field(p => p.date1_ts)
                        .Interval(DateInterval.Year)
                        .OrderDescending("_key")
                        .Aggregations(a2 => a2
                            .Terms("versions", t2 => t2
                                .Field(p2 => p2.version)
                                .Aggregations(a3 => a3
                                    .Sum("summofdebts", s3 => s3
                                        .Field(p3 => p3.summ)
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        
        public NestFuncSearch GetListOfIdAndVersion()
        {
            return (s => s
                .Type("debt")
                .Size(10000)
                .Source(ss => ss
                    .Includes(inc => inc
                        .Fields(p1 => p1.id, p2 => p2.version)
                    )
                )
                .Sort(ss => ss
                    .Ascending(p => p.id)
                )
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            _GetCondition()
                        )
                    )
                )
            );
        }

        private IEnumerable<NestFuncQuery> _GetCondition()
        {
            NestFuncQuery nothing = (s => s
                    .Term(c => c
                        .Field(p => p.version)
                        .Value(-1)
                    )
                );

/*
            if (_names == null || _adress == null )
            {
                return new NestFuncQuery[]{(s => s
                    .Term(c => c
                        .Name("returnnothing")
                        .Value("true")
                    )
                )};
            }
 */ 
/*
            if ((_names == null && (_so.queryoption == TypeOfQuery.AdressAndName || _so.queryoption == TypeOfQuery.AdressOrName ||
                _so.queryoption == TypeOfQuery.AdressXorName || _so.queryoption == TypeOfQuery.Name || _so.queryoption == TypeOfQuery.NameNoInter)) ||
                (_adress == null && (_so.queryoption == TypeOfQuery.AdressAndName || _so.queryoption == TypeOfQuery.AdressOrName ||
                _so.queryoption == TypeOfQuery.AdressXorName || _so.queryoption == TypeOfQuery.Adress || _so.queryoption == TypeOfQuery.AdressNoInter)))
            {
                return new NestFuncQuery[]{(s => s
                    .Term(c => c
                        .Name("returnnothing")
                        .Value("true")
                    )
                )};
            }
*/

            // Название
            NestFuncQuery nameq = null; // запрос по имени
            if (_names != null)
            {
                var list = new List<NestFuncQuery>(); // запрос по имени
                foreach (var name in _names)
                {
                    if (!(string.IsNullOrEmpty(name.shortname) && string.IsNullOrEmpty(name.longname)))
                    {
                        if (!string.IsNullOrEmpty(name.shortname))
                        {
                            list.Add((s => s
                                .MatchPhrase(mp => mp
                                    .Field(p => p.debtoranalyzed)
                                    .Query(name.shortname)
                                )
                            ));
                        }
                        if (!string.IsNullOrEmpty(name.longname))
                        {
                            list.Add((s => s
                                .MatchPhrase(mp => mp
                                    .Field(p => p.debtoranalyzed)
                                    .Query(name.longname)
                                )
                            ));
                        }
                    }
                }
                nameq = ( s => s
                    .Bool(b => b
                        .Should(list.ToArray())
                    )
                );
            }
            // Адрес
            NestFuncQuery adressfullq = null; // запрос по всем адресам

            if (_adress != null)
            {
                var adressq_list = new List<NestFuncQuery>();
                foreach (var addr in _adress)
                {
                    // Верхний уровень
                    var adresstopq_list = new List<NestFuncQuery>();
                    if (!String.IsNullOrEmpty(addr.region_name) || !String.IsNullOrEmpty(addr.region))
                    {
                        var list = new List<NestFuncQuery>();
                        if (!string.IsNullOrEmpty(addr.region))
                        {
                            list.Add(s => s
                                .Match(m => m
                                    .Field(p => p.a_regid)
                                    .Query(addr.region)
                                )
                            );
                        }
                        if (!string.IsNullOrEmpty(addr.region_name))
                        {
                            list.Add(s => s
                                .MatchPhrase(m => m
                                    .Field(p => p.a_regname)
                                    .Query(addr.region_name)
                                )
                            );
                        }

                        list.Add( s => s
                            .Bool( b => b
                                .Must(
                                    mst1 => mst1
                                        .Match( m1 => m1
                                            .Field(p => p.a_regid)
                                            .Query("NULL")
                                        )
                                    ,mst2 => mst2
                                        .Match( m2 => m2
                                            .Field(p => p.a_regname)
                                            .Query("NULL")
                                        )
                                )
                            )
                        );

                        adresstopq_list.Add(s => s
                            .Bool(b => b
                                .Should(list.ToArray())
                            )
                        );
                    }

                    if (!String.IsNullOrEmpty(addr.district_name))
                    {
                        adresstopq_list.Add(s => s
                            .Bool(b => b
                                .Should(
                                    c1 => c1
                                        .MatchPhrase( m => m
                                            .Field(p => p.a_distrname)
                                            .Query(addr.district_name)
                                        )
                                    ,c2 => c2
                                        .MatchPhrase( m => m
                                            .Field(p => p.a_distrname)
                                            .Query("NULL")
                                        )
                                )
                            )
                        );
                    }
                    if (!String.IsNullOrEmpty(addr.city_name))
                    {
                        adresstopq_list.Add(s => s
                            .Bool(b => b
                                .Should(
                                    c1 => c1
                                        .MatchPhrase( m => m
                                            .Field(p => p.a_cityname)
                                            .Query(addr.city_name)
                                        )
                                    ,c2 => c2
                                        .MatchPhrase( m => m
                                            .Field(p => p.a_cityname)
                                            .Query("NULL")
                                        )
                                )
                            )
                        );
                    }

                    // + Нижний уровень
                    var adressbottomq_list = new List<NestFuncQuery>();
                    NestFuncQuery adressbottomq;
                    if (!string.IsNullOrEmpty(addr.locality_name))
                    {
                        adressbottomq_list.Add( s => s
                            .MatchPhrase( m => m
                                .Field(p => p.a_localityname)
                                .Query(addr.locality_name)
                            )
                        );
                    }
                    if (!string.IsNullOrEmpty(addr.street_name))
                    {
                        adressbottomq_list.Add(s => s
                            .MatchPhrase(m => m
                                .Field(p => p.a_streetname)
                                .Query(addr.street_name)
                            )
                        );
                    }
                    if (!string.IsNullOrEmpty(addr.house))
                    {
                        adressbottomq_list.Add(s => s
                            .Bool(b => b
                                .Should(
                                    s1 => s1
                                        .MatchPhrase(m1 => m1
                                            .Field(p => p.a_dopinfo)
                                            .Query(addr.house)
                                        )
                                    ,s2 => s2
                                        .MatchPhrase( m2 => m2
                                            .Field(p => p.a_housenum)
                                            .Query(addr.house)
                                        )
                                )
                            )
                        );
                    }

                    adressbottomq = (s => s
                        .Bool(b => b
                            .Must(adressbottomq_list.ToArray())
                        )
                    );

                    // + Индекс
                    NestFuncQuery adresszipq = null;
                    if (!string.IsNullOrEmpty(addr.zip))
                    {
                        adresszipq = ( s => s
                            .Bool( b => b
                                .Must(
                                    mst1 => mst1
                                        .Match( m => m
                                            .Field(p => p.a_streetname)
                                            .Query("NULL")
                                        )
                                    ,mst2 => mst2
                                        .Match( m => m
                                            .Field(p => p.a_localityname)
                                            .Query("NULL")
                                        )
                                    ,mst3 => mst3
                                        .Match( m => m
                                            .Field(p => p.a_housenum)
                                            .Query("NULL")
                                        )
                                    ,mst4 => mst4
                                        .Match( m => m
                                            .Field(p => p.a_zip)
                                            .Query(addr.zip)
                                        )
                                )
                            )
                        );
                    }
                    adresstopq_list.Add(
                        mst1 => mst1
                            .Bool( b => b
                                .Should(adressbottomq,adresszipq)
                            )
                    );
                    adressq_list.Add( s => s
                        .Bool( b => b
                            .Must(adresstopq_list.ToArray())
                        )
                    );
                }
                adressfullq = ( s => s
                    .Bool( b => b
                        .Should(adressq_list.ToArray())
                    )
                );
            }
            // + Тип запроса
            var type_list = new List<Func<QueryContainerDescriptor<DebtorListItem>, QueryContainer>>();
            foreach (var st in _so.searchtypes)
            {
                switch (st)
                {
                    case DebtorSearchType.AdressAndName:
                        List<NestFuncQuery> mstan = new List<NestFuncQuery>();
                        if (_adress != null && _names != null)
                        {
                            mstan.AddRange(new List<NestFuncQuery>() { nameq, adressfullq });
                        }
                        else
                        {
                            mstan.Add(nothing);
                        }
                        type_list.Add((s => s
                            .Bool(b => b
                                .Must(mstan)
                                .Name("AdressAndName")
                            )
                        ));
                        break;
                    case DebtorSearchType.Adress:
                        List<NestFuncQuery> msta = new List<NestFuncQuery>() { adressfullq ?? nothing };
                        if (_names != null)
                        {
                            msta.Add((mst1 => mst1
                                        .Bool(b1 => b1
                                            .MustNot(mn => mn
                                                .Bool(b2 => b2
                                                    .Must(nameq, adressfullq)
                                                )
                                            )
                                        )
                                    ));
                        }
                        type_list.Add(s => s
                            .Bool(b => b
                                .Must(msta)
                                .Name("Adress")
                            )
                        );
                        break;
                    case DebtorSearchType.Name:
                        List<NestFuncQuery> mstn = new List<NestFuncQuery>() {nameq ?? nothing};
                        if (_adress != null)
                        {
                            mstn.Add((mst1 => mst1
                                            .Bool(b1 => b1
                                                .MustNot(mnt => mnt
                                                    .Bool(b2 => b2
                                                        .Must(nameq, adressfullq)
                                                    )
                                                )
                                            )
                                        ));
                        }
                        type_list.Add(s => s
                            .Bool( b => b
                                .Must(mstn)
                                .Name("Name")
                            )
                        );
                        break;
                }
            }
            var res_list = new List<Func<QueryContainerDescriptor<DebtorListItem>, QueryContainer>>();

            res_list.Add((s => s
                .Bool(b => b
                    .Should(type_list.ToArray())
                )
            ));

            // + Пользовательские фильтры
            if (!string.IsNullOrEmpty(_so.numProizv))
            {
                res_list.Add(s => s
                    .Match(m => m
                        .Field(p => p.nproizv)
                        .Query(_so.numProizv)
                    )
                );
            }
            if (_so.predmet != "-1")
            {
                res_list.Add(s => s
                    .Match(m => m
                        .Field(p => p.predmet)
                        .Query(_so.predmet)
                    )
                );
            }
            if (_so.region != "0")
            {
                res_list.Add(s => s
                    .Term(m => m
                        .Field(p => p.region_id)
                        .Value(_so.region)
                    )
                );
            }
            if (!(string.IsNullOrEmpty(_so.dateFrom) && string.IsNullOrEmpty(_so.dateTo)))
            {
                res_list.Add(s => s
                    .DateRange( r => r
                        .Field(p => p.date1_ts)
                        .GreaterThanOrEquals(_so.dateFrom)
                        .LessThanOrEquals(_so.dateTo)
                    )
                );    
            }
            if (_so.version != -1)
            {
                res_list.Add(s => s
                    .Term(t => t
                        .Field(p => p.version)
                        .Value(_so.version)
                    )
                );
            }

            // + ИП не удалено!!!
            res_list.Add(s => s
                .Term(t => t
                    .Field(p => p.deletedate)
                    .Value("01.01.1900")
                )
            );
            return res_list.ToArray<NestFuncQuery>();
        }
    }
}