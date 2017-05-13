using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;
using ClosedXML.Excel;
using Skrin.Models.QIVSearch;
using SKRIN;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using System.Globalization;

namespace Skrin.BLL.Root
{
    public enum ParamType {Main = 0, Big = 1, Small = 2, SONO = 3, All = 4} 

    public class QIVParamRepository
    {
        public static async Task<int> GetTypeCount(int year)
        {
            int res = 0;
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query query = await sql.OpenQueryAsync("select count(*) as cnt from gks..gks_quart_indics_types A where @year>=A.begin_yyyyq and @year<=A.end_yyyyq",new SQLParamInt("year",year));
                if (await query.ReadAsync())
                {
                    res = (int)query.GetFieldAsInt("cnt")+1;
                }
                query.Close();
            }
            return res;
        }

        public static async Task<int> GetDefaultPeriod()
        {
            int res = 2014;
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query query = await sql.OpenQueryAsync("select max(year) as y from gks.[dbo].[GKS_QIV_Years] ");
                if (await query.ReadAsync())
                {
                    res = (int)query.GetFieldAsInt("y");
                }
                query.Close();
            }
            return res;
        }

        public static async Task<string> GetNauforPeriod()
        {
            string res = "";
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
//                using (Query query = await sql.OpenQueryAsync("SELECT top 1 (cast(quarter as varchar(2)) + 'кв. ' + cast(year as varchar(4))) as quartyear FROM  (select year,quarter from naufor..quart_indic_values where year>1997 group by year,quarter) a  ORDER BY year desc,quarter desc "))
                using (Query query = await sql.OpenQueryAsync("SELECT top 1 (cast(quarter as varchar(2)) + 'кв. ' + cast(year as varchar(4))) as quartyear from naufor..quart_indic_values where year>1997 ORDER BY year desc,quarter desc "))
                {
                    if (await query.ReadAsync())
                    {
                        res = (string)query.GetFieldAsString("quartyear");
                    }
                }   
            }
            return res;
        }

        public static async Task<int> GetMsfoPeriod()
        {
            int res = 2014;
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                using (Query query = await sql.OpenQueryAsync("select max(year) as y from naufor..msfo_quart_indic_values"))
                {
                    if (await query.ReadAsync())
                    {
                        res = (int)query.GetFieldAsInt("y");
                    }
                    query.Close();
                }
            }
            return res;
        }

        public static async Task<Tuple<string, string>> GetIndicNameAsync(int id, int type_id)
        {
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                string name = "";
                string line_code = "";

                using (Query query = await sql.OpenQueryAsync("select A.id, L.line_code, L.name from gks..gks_quart_indics_new A INNER JOIN gks..GKS_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.section_no=L.section_no and A.line_code=L.line_code where @type_id=A.type_id and @id=A.id", new SQLParamInt("type_id", type_id), new SQLParamInt("id", id)))
                {
                    if (await query.ReadAsync())
                    {
                        name = query.GetFieldAsString("name");
                        line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                    }
                }

                return new Tuple<string, string>(line_code, name);
            }
        }


        public static async Task<Tuple<string, string>> GetCodeNameAsync(int id, int type_id, string tab_name)
        {
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                string name = "";
                string line_code = "";

                string sel = "";
                switch (tab_name)
                {
                    case "gks":
                        sel = @"select cast(B.line_code as varchar(100)) as line_code, C.name as name
                                    from gks..gks_quart_indics_new B 
                                    inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                                    inner join gks..gks_quart_indic_cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.section_no=D.section_no and B.col_code=D.col_code and D.delta_year=0
                                    where B.id=@param_id and B.type_id=@param_type";
                        //sel = "select A.id, L.line_code, L.name from gks..gks_quart_indics_new A INNER JOIN gks..GKS_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.section_no=L.section_no and A.line_code=L.line_code where @type_id=A.type_id and @id=A.id";
                        break;
                    case "naufor":
                        sel = @"select cast(L.line_code as varchar(100)) as line_code, L.name as name from naufor..Quart_Indics A inner join naufor..Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@param_type and A.id=@param_id"; 
                        //sel = "select A.id, L.line_code, L.name from naufor..Quart_Indics A inner join naufor..Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@type_id and A.id=@id";
                        break;
                    case "msfo":
                        sel = @"select '' as line_code, C.name_eng + ' (' + D.name_eng + ')'  as name
                                from naufor..MSFO_Quart_Indics B
                                    inner join naufor..MSFO_Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code
                                    inner join naufor..MSFO_Quart_Indic_Cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.col_code=D.col_code and D.col_code=3
                                where B.id=@param_id";
                        //sel = "select A.id, L.line_code, L.name_eng as name from naufor..MSFO_Quart_Indics A inner join naufor..MSFO_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@type_id and A.id=@id";
                        break;
                }

                using (Query query = await sql.OpenQueryAsync(sel, new SQLParamInt("param_type", type_id), new SQLParamInt("param_id", id)))
                {
                    if (await query.ReadAsync())
                    {
                        name = query.GetFieldAsString("name");
                        line_code = query.GetFieldAsString("line_code");
                    }
                }

                return new Tuple<string, string>(line_code, name);
            }
        }

        public static async Task<QIVParams> GetDefaultParam(int period)
        {
            int type_id = 6;
            int id = 50;
            string name = "";
            string line_code = "";
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query query;
                query = await sql.OpenQueryAsync("select id as type_id from gks..gks_quart_indics_types A where @year>=A.begin_yyyyq and @year<=A.end_yyyyq and A.iss_type=2", new SQLParamInt("year", period));
                if (await query.ReadAsync())
                {
                    type_id = (int)query.GetFieldAsInt("type_id");
                }
                query.Close();

                query = await sql.OpenQueryAsync("select A.id, L.line_code, L.name from gks..gks_quart_indics_new A INNER JOIN gks..GKS_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.section_no=L.section_no and A.line_code=L.line_code where @type_id=A.type_id and @id=A.id", new SQLParamInt("type_id", type_id), new SQLParamInt("id", id));
                //Если не будет записи для данного type_id и indic_id
                if (!(await query.ReadAsync()))
                {
                    query.Close();
                    query = await sql.OpenQueryAsync("select top 1 A.id, L.line_code, L.name from gks..gks_quart_indics_new A inner join gks..GKS_Quart_Indic_Cols B on A.type_id=B.type_id and A.form_no=B.form_no and A.section_no=B.section_no and B.[delta_year]=0  INNER JOIN gks..GKS_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.section_no=L.section_no and A.line_code=L.line_code where A.type_id=@type_id ORDER BY A.id", new SQLParamInt("type_id", type_id));
                    if (await query.ReadAsync())
                    {
                        id = (int)query.GetFieldAsInt("id");
                        name = query.GetFieldAsString("name");
                        line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                    }
                }
                else 
                {
                    name = query.GetFieldAsString("name");
                    line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                }
                query.Close();
            }

            return new QIVParams { id = id, line_code = line_code, name = name,type_id=type_id };
        }

        public static async Task<QIVParams> GetNauforParam(int period)
        {
            int type_id = 5;
            int id = 50;
            string name = "";
            string line_code = "";
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query query;
                query = await sql.OpenQueryAsync("select id as type_id from naufor..Quart_Indics_Types A where @year>=A.begin_yyyyq and @year<=isnull(A.end_yyyyq,100000)", new SQLParamInt("year", period));
                if (await query.ReadAsync())
                {
                    type_id = (int)query.GetFieldAsInt("type_id");
                }
                query.Close();

                query = await sql.OpenQueryAsync("select A.id, L.line_code, L.name from naufor..Quart_Indics A inner join naufor..Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@type_id and A.id=@id", new SQLParamInt("type_id", type_id), new SQLParamInt("id", id));
                //Если не будет записи для данного type_id и indic_id
                if (!(await query.ReadAsync()))
                {
                    query.Close();
                    query = await sql.OpenQueryAsync("select top 1 A.id, L.line_code, L.name from naufor..Quart_Indics A inner join naufor..Quart_Indic_Cols B on A.type_id=B.type_id and A.form_no=B.form_no inner join naufor..Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@type_id ORDER BY A.id", new SQLParamInt("type_id", type_id));
                    if (await query.ReadAsync())
                    {
                        id = (int)query.GetFieldAsInt("id");
                        name = query.GetFieldAsString("name");
                        line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                    }
                }
                else
                {
                    name = query.GetFieldAsString("name");
                    line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                }
                query.Close();
            }

            return new QIVParams { id = id, line_code = line_code, name = name, type_id = type_id };
        }

        public static async Task<QIVParams> GetMSFOParam(int period)
        {
            int id = 15;
            string name = "";
            string line_code = "";
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                Query query;
                query = await sql.OpenQueryAsync("select A.id, L.line_code, L.name_eng as name from naufor..MSFO_Quart_Indics A inner join naufor..MSFO_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.id=15");
                if (!(await query.ReadAsync()))
                {
                    query.Close();
                    query = await sql.OpenQueryAsync("select top 1 A.id, L.line_code, L.name_eng as name from naufor..MSFO_Quart_Indics A inner join naufor..MSFO_Quart_Indic_Cols B on A.type_id=B.type_id and A.form_no=B.form_no inner join naufor..MSFO_Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code ORDER BY A.id");
                    if (await query.ReadAsync())
                    {
                        id = (int)query.GetFieldAsInt("id");
                        name = query.GetFieldAsString("name");
                        line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                    }
                }
                else
                {
                    name = query.GetFieldAsString("name");
                    line_code = ((int)query.GetFieldAsInt("line_code")).ToString();
                }
                query.Close();
            }
            return new QIVParams { id = id, line_code = line_code, name = name };
        }

        public static async Task<List<QIVParams>> GetParams(int period, ParamType type)
        {
            List<QIVParams> ret = new List<QIVParams>();
            string querytext = "";
            switch (type)
            {
                case ParamType.Main:
                    //+' (Форма №'+cast(C.form_no as varchar(100))+')'
                    querytext = @"
                        select null as group_id, null as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name as name, B.type_id
                        from gks..gks_quart_indics_new B 
                            inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                        where B.id in (6,14,30,31,34,50,51,52,54,55) and B.type_id=(select max(T.id) from gks..GKS_Quart_Indics_Types T where T.iss_type=2 and @year>=T.begin_yyyyq and @year<=T.end_yyyyq)
                        order by B.form_no, C.so, B.col_code
                    ";
                    break;
                case ParamType.Big:
                    querytext = @"
                        select B.form_no as group_id, 'Форма №'+cast(B.form_no as varchar(100)) as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name + case when B.form_no=3 then ' ('+D.name+')' else '' end  as name, B.type_id
                        from gks..gks_quart_indics_new B 
                            inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                            inner join gks..gks_quart_indic_cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.section_no=D.section_no and B.col_code=D.col_code and D.delta_year=0
                        where B.type_id=(select max(T.id) from gks..GKS_Quart_Indics_Types T where T.iss_type=2 and @year>=T.begin_yyyyq and @year<=T.end_yyyyq)
                        order by B.form_no, C.so, d.col_code
                    ";
                    break;
                case ParamType.Small:
                    querytext = @"
                        select B.form_no as group_id, 'Форма №'+cast(B.form_no as varchar(100)) as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name + case when B.form_no=3 then ' ('+D.name+')' else '' end  as name, B.type_id
                        from gks..gks_quart_indics_new B 
                            inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                            inner join gks..gks_quart_indic_cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.section_no=D.section_no and B.col_code=D.col_code and D.delta_year=0
                        where B.type_id=(select max(T.id) from gks..GKS_Quart_Indics_Types T where T.iss_type=1 and @year>=T.begin_yyyyq and @year<=T.end_yyyyq)
                        order by B.form_no, C.so, d.col_code
                    ";
                    break;
                case ParamType.SONO:
                    querytext = @"
                        select B.form_no as group_id, 'Форма №'+cast(B.form_no as varchar(100)) as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name + case when B.form_no=3 then ' ('+D.name+')' else '' end  as name, B.type_id
                        from gks..gks_quart_indics_new B 
                            inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                            inner join gks..gks_quart_indic_cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.section_no=D.section_no and B.col_code=D.col_code and D.delta_year=0
                        where B.type_id=(select max(T.id) from gks..GKS_Quart_Indics_Types T where T.iss_type=0 and @year>=T.begin_yyyyq and @year<=T.end_yyyyq)
                        order by B.form_no, C.so, d.col_code
                    ";
                    break;
            }
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                byte group_id = 0;
                using (Query query = await sql.OpenQueryAsync(querytext, new SQLParamInt("year", period)))
                {
                    while (await query.ReadAsync())
                    {
                        if ((query.GetFieldAsByte("group_id") != null) && ((byte)query.GetFieldAsByte("group_id") != group_id))
                        {
                            ret.Add(new QIVParams() { id = (int)query.GetFieldAsByte("group_id"), name = query.GetFieldAsString("group_name"), isFolder = true });
                            group_id = (byte)query.GetFieldAsByte("group_id");
                        }
                        ret.Add(new QIVParams() { id = (int)query.GetFieldAsInt("id"), line_code = query.GetFieldAsString("line_code"), name = query.GetFieldAsString("name"), isFolder = false, type_id = (int)query.GetFieldAsInt("type_id") });
                    }
                }
            }
            return ret;
        }

        public static async Task<List<QIVParams>> GetNauforParams(int period, ParamType type)
        {
            List<QIVParams> ret = new List<QIVParams>();
            string querytext = "";
            switch (type)
            {
                case ParamType.Main:
                    querytext = @"select null as group_id, null as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name as name, B.type_id
                    from naufor..Quart_Indics B 
                        inner join naufor..Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code
                    where B.id in (6,14,30,31,34,50,51,52,54,55) and B.type_id=(select T.id from naufor..Quart_Indics_Types T where @yyyyq>=T.begin_yyyyq and @yyyyq<=isnull(T.end_yyyyq,100000)) 
                    order by B.form_no, C.so, B.col_code
                    ";
                    break;
                case ParamType.All:
                    querytext = @"select B.form_no as group_id, 'Форма №'+cast(B.form_no as varchar(100)) as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name, B.type_id
                    from naufor..Quart_Indics B
                        inner join naufor..Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code
                        inner join naufor..Quart_Indic_Cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.col_code=D.col_code and D.delta_year=0
                    where B.type_id=(select T.id from naufor..Quart_Indics_Types T where @yyyyq>=T.begin_yyyyq and @yyyyq<=isnull(T.end_yyyyq,100000))
                    order by B.form_no, C.so, d.col_code";
                    break;
            }
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                byte group_id = 0;
                using (Query query = await sql.OpenQueryAsync(querytext, new SQLParamInt("yyyyq", period)))
                {
                    while (await query.ReadAsync())
                    {
                        if ((query.GetFieldAsByte("group_id") != null) && ((byte)query.GetFieldAsByte("group_id") != group_id))
                        {
                            ret.Add(new QIVParams() { id = (int)query.GetFieldAsByte("group_id"), name = query.GetFieldAsString("group_name"), isFolder = true });
                            group_id = (byte)query.GetFieldAsByte("group_id");
                        }
                        ret.Add(new QIVParams() { id = (int)query.GetFieldAsInt("id"), line_code = query.GetFieldAsString("line_code"), name = query.GetFieldAsString("name"), isFolder = false, type_id = (int)query.GetFieldAsInt("type_id") });
                    }
                }
            }
            return ret;
        }

        public static async Task<List<QIVParams>> GetMsfoParams(ParamType type)
        {
            List<QIVParams> ret = new List<QIVParams>();
            string querytext = "";
            switch (type)
            {
                case ParamType.Main:
                    querytext = @"select B.form_no as group_id, F.name_eng as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name_eng + ' (' + D.name_eng + ')' as name, B.type_id
                    from naufor..MSFO_Quart_Indics B
                        inner join naufor..MSFO_Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code and C.is_main=1
                        inner join naufor..MSFO_Quart_Indic_Cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.col_code=D.col_code and D.col_code=3
                        inner join naufor..MSFO_Quart_Forms F on B.type_id=F.type_id and B.form_no=F.form_no
                    order by B.form_no, C.line_code, D.col_code";
                    break;
                case ParamType.All:
                    querytext = @"select B.form_no as group_id, F.name_eng as group_name, B.id, cast(B.line_code as varchar(100)) as line_code, C.name_eng + ' (' + D.name_eng + ')' as name, B.type_id
                    from naufor..MSFO_Quart_Indics B
                        inner join naufor..MSFO_Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code
                        inner join naufor..MSFO_Quart_Indic_Cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.col_code=D.col_code and D.col_code=3
                        inner join naufor..MSFO_Quart_Forms F on B.type_id=F.type_id and B.form_no=F.form_no
                    order by B.form_no, C.line_code, D.col_code";
                    break;
            }
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                byte group_id = 0;
                Query query = await sql.OpenQueryAsync(querytext);
                while (await query.ReadAsync())
                {
                    if ((query.GetFieldAsByte("group_id") != null) && ((byte)query.GetFieldAsByte("group_id") != group_id))
                    {
                        ret.Add(new QIVParams() { id = (int)query.GetFieldAsByte("group_id"), name = query.GetFieldAsString("group_name"), isFolder = true });
                        group_id = (byte)query.GetFieldAsByte("group_id");
                    }
                    ret.Add(new QIVParams() { id = (int)query.GetFieldAsInt("id"), line_code = query.GetFieldAsString("line_code"), name = query.GetFieldAsString("name"), isFolder = false, type_id = (int)query.GetFieldAsInt("type_id") });
                }
                query.Close();
            }

            return ret;
        }
        
        public static string GetParamName(QIVSearchType qiv_type, int param_id, int? param_type)
        {
            string ret = "";
            string querytext = "";
            switch (qiv_type)
                {
                    //В зависимости от закладки
                    case QIVSearchType.GKS:
                        querytext = @"
                                    select cast(B.line_code as varchar(100)) +  ' ' + C.name  + isnull(' ('+D.name+')','') + isnull(' (Форма №'+cast(B.form_no as varchar(100))+')','') as param_name
                                    from gks..gks_quart_indics_new B 
                                    inner join gks..gks_quart_indic_lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.section_no=C.section_no and B.line_code=C.line_code
                                    inner join gks..gks_quart_indic_cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.section_no=D.section_no and B.col_code=D.col_code and D.delta_year=0
                                    where B.id=@param_id and B.type_id=@param_type
                                ";
                        break;
                    case QIVSearchType.NAUFOR:
                        querytext = @"
                                    select cast(L.line_code as varchar(100)) + ' ' + L.name  as param_name from naufor..Quart_Indics A inner join naufor..Quart_Indic_Lines L on A.type_id=L.type_id and A.form_no=L.form_no and A.line_code=L.line_code where A.type_id=@param_type and A.id=@param_id
                                ";
                        break;
                    case QIVSearchType.MSFO:
                        querytext = @"
                                select C.name_eng + ' (' + D.name_eng + ')'  as param_name
                                from naufor..MSFO_Quart_Indics B
                                    inner join naufor..MSFO_Quart_Indic_Lines C on B.type_id=C.type_id and B.form_no=C.form_no and B.line_code=C.line_code
                                    inner join naufor..MSFO_Quart_Indic_Cols D on B.type_id=D.type_id and B.form_no=D.form_no and B.col_code=D.col_code and D.col_code=3
                                    inner join naufor..MSFO_Quart_Forms F on B.type_id=F.type_id and B.form_no=F.form_no
                                where B.id=@param_id
					            order by B.form_no, C.line_code, D.col_code
                                ";
                        break;
                }
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                using (Query query = sql.OpenQuery(querytext, new SQLParamInt("param_id", param_id), new SQLParamInt("param_type", param_type)))
                {
                    while (query.Read())
                    {
                        if ((query.GetFieldAsString("param_name") != null))
                        {
                            ret = query.GetFieldAsString("param_name");
                        }
                    }
                }
            }
            return ret;

        }

        public static async Task<QIVQuery> GetSearchQuery(QIVSearchParams param, QIVResultType result_type)
        {
            QIVQuery res = new QIVQuery();
            res.sqlparams = new List<SQLParam>();
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                //Собираем select 
                switch (result_type)
                {
                    case QIVResultType.Web:
                        res.select_text = @"
                            SELECT A.inn, A.ogrn, isnull(A.short_name,A.name) as name, A.ticker, A.issuer_id, CAST(A.type_id as VARCHAR(2)) as type_id
                        ";
                        break;
                    case QIVResultType.Group:
                        res.select_text = @"
                            SELECT A.issuer_id, CAST(A.type_id as VARCHAR(2)) as type_id
                        ";
                        break;
                    case QIVResultType.Excel:
                        res.select_text = @"
                            SELECT A.inn, A.ogrn, isnull(A.short_name,A.name) as name, A.ticker, A.issuer_id, CAST(A.type_id as VARCHAR(2)) as type_id, R.name as region_name
                                , A.name as full_name, Opf.name as opf_name, Okfs.name as Okfs_name, A.okpo, A.ogrn, convert(VARCHAR(20), A.reg_date, 104) as ogrn_date, A.reg_org_name, isnull(A.okved3_name,'-') as okved_name, A.okved3_code as okved_code, A.legal_address
                                , A.ruler, A.legal_phone as phone, A.legal_fax as fax, A.legal_email as email, A.www
                        ";
                        break;
                }

                //Собираем from 
                res.from_text = @"
                    FROM searchdb2..union_search A
                ";
                if (result_type == QIVResultType.Excel) {
                    res.from_text += @" 
                        LEFT JOIN naufor..Regions R on R.id=A.region_id2
                        LEFT JOIN naufor..okopf Opf on A.opf=Opf.id
                        LEFT JOIN naufor..Okfs Okfs on A.Okfs=Okfs.id
                    ";
                }

                //Собираем ограничения where
                res.where_text = @"
                    WHERE 1=1
                ";

                //ОКОПФ
                if (param.okopf != null && param.okopf != "")
                {
                    using (SQL sql2 = new SQL(Configs.ConnectionString))
                    {
                        using (Query q = await sql2.OpenQueryAsync(@"
                            ;with tree(id, ParentID, name, level)
                            as
                            (
                            select A.id, A.Parent_ID, A.name, 0
                            from  naufor.dbo.okopf A
	                            inner join searchdb2.dbo.kodesplitter(0,@okopf) b on a.id=b.kod 
                            union all
                            select C.id, C.Parent_ID, C.name, level+1
                            from naufor.dbo.okopf c
	                            inner join tree t on t.id=c.Parent_ID
                            )
                            select STUFF((select ','+cast(id as varchar(100)) from tree for xml path('')),1,1,'') as okopf
                        ", new SQLParamVarchar("okopf", param.okopf))
                        )
                        {
                            if (q.Read())
                                res.where_text += " and A.opf " + ((param.okopf_excl == 1) ? " not " : "") + " in (" + q.GetFieldAsString("okopf") + ")";
                        }
                    }
                }

                //ОКФС
                if (param.okfs != null && param.okfs != "")
                {
                    using (SQL sql2 = new SQL(Configs.ConnectionString))
                    {
                        using (Query q = await sql2.OpenQueryAsync(@"
                            ;with tree(id, ParentID, name, level)
                            as
                            (
                            select A.id, A.Parent_ID, A.name, 0
                            from  naufor.dbo.okfs A
	                            inner join searchdb2.dbo.kodesplitter(0,@okfs) b on a.id=b.kod 
                            union all
                            select C.id, C.Parent_ID, C.name, level+1
                            from naufor.dbo.okfs c
	                            inner join tree t on t.id=c.Parent_ID
                            )
                            select STUFF((select ','+cast(id as varchar(100)) from tree for xml path('')),1,1,'') as okfs
                        ", new SQLParamVarchar("okfs", param.okfs))
                        )
                        {
                            if (q.Read()) 
                                res.where_text += " and A.okfs " + ((param.okfs_excl == 1) ? " not " : "") + " in (" + q.GetFieldAsString("okfs") + ")";
                        }
                        
                    }
                }

                //Дата регистрации с по
                if (param.dbeg != null && param.dbeg != "")
                {
                    try
                    {
                        res.sqlparams.Add(new SQLParamDateTime("@dbeg", DateTime.Parse(param.dbeg)));
                        res.where_text += " and A.reg_date>=@dbeg";
                    }
                    catch { };
                }

                if (param.dend != null && param.dend != "")
                {
                    try
                    {
                        res.sqlparams.Add(new SQLParamDateTime("@dend", DateTime.Parse(param.dend)));
                        res.where_text += " and A.reg_date<=@dend";
                    }
                    catch { };
                }

                if (param.regions != null && param.regions != "")
                {
                    if (param.is_okato == 0) 
                    {
                        //Регионы по окато
                        using (SQL sql2 = new SQL(Configs.ConnectionString))
                        {
                            var or_text = "";
                            Query q2 = await sql2.OpenQueryAsync(@"
                                Select a.okato 
                                from naufor..okato a 
	                                inner join searchdb2.dbo.kodesplitter(0,@ids) b on a.parentid=b.kod 
	                                inner join naufor..okato c on c.id=b.kod and c.parentid=0
                                union all 	
                                Select a.okato 
                                from naufor..okato a 
	                                inner join searchdb2.dbo.kodesplitter(0,@ids) b  on b.kod=a.id and A.parentid!=0
                            ", new SQLParamVarchar("@ids", param.regions));
                            while (await q2.ReadAsync())
                            {
                                or_text += " or A.okato " + ((param.reg_excl == 1) ? " not " : "") + " like " + SQL.StoBC(Utilites.TrimOKATO(q2.GetFieldAsString("okato")) + "%");
                            }
                            if (or_text != "")
                            { 
                                res.where_text += " and (" + or_text.Substring(3) + ")";
                            }
                            q2.Close();
                        }
                    }
                    else
                    {
                        //Регионы по налоговой
                        using (SQL sql2 = new SQL(Configs.ConnectionString))
                        {
                            Query q2 = await sql2.OpenQueryAsync(@"SELECT STUFF((SELECT ','+right('00' + cast(kod as varchar(16)),2) FROM  ( 
                         Select a.kod from searchdb2..regions a inner join (Select *  from searchdb2.dbo.kodesplitter(0,@ids)) b 
                         on (a.parent_id=b.kod and exists(select 1 from searchdb2..regions c where c.id=b.kod)) or (b.kod=a.id and parent_id!=0) 
                         ) o ORDER BY kod FOR XML PATH('')),1,1,'') as regions", new SQLParamVarchar("@ids", param.regions));
                            while (await q2.ReadAsync())
                            {
                                res.where_text += " and A.region_id2 " + ((param.reg_excl == 1) ? " not " : "") + "in (" + SQL.StrEscape(q2.GetFieldAsString("regions")) + ")";
                            }
                            q2.Close();
                        }

                        //param.regions = await SearchRepository.GetRegsForSqlSearchAsync(param.regions, param.is_okato);
                        //res.where_text += " and A.region_id2 " + ((param.reg_excl == 1) ? " not " : "") + "in (" + SQL.StrEscape(param.regions) + ")";
                    }
                }

                //Отрасли по ОКВЭД
                if (param.industry != null && param.industry != "")
                {
                    if (param.ind_main == 1)
                    {
                        res.where_text += " and " + ((param.ind_excl == 1) ? " not " : "") + " exists (select 1 from naufor..okveds_search B where a.okved3_code=B.okved and a.okved3_year=B.year and B.okved3_id in (" + SQL.StrEscape(param.industry) + "))";
                    }
                    else
                    {
                        res.where_text += " and " + ((param.ind_excl == 1) ? " not " : "") + " exists (SELECT 1 from searchdb2..union_okved3 uo join naufor..okveds_search B on uo.okved3_code=B.okved and uo.okved3_year=B.year and B.okved3_id in (" + SQL.StrEscape(param.industry) + ") where uo.us_id=a.id)";
                    }
                }

                //Группы
                if (param.group_id > 0)
                {
                    res.from_text += " inner join security..secUserListItems_join li on li.issuerid=a.issuer_id  and ListID=@group_id";
                    res.sqlparams.Add(new SQLParamInt("@group_id", param.group_id));
                }

                //Наличие в реестре Росстата (1 - есть)
                if(param.rgstr == 1)
                {
                    res.where_text += " and a.gks_delete_date IS NULL";
                }

                //Показатели
                switch (param.qiv_type)
                {
                    //В зависимости от закладки
                    case QIVSearchType.GKS:
                        if (param.andor == "or")
                        {
                            //or
                            string or_text = "";
                            //string or_value = "";
                            List<string> or_value = new List<string>();
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];

                                res.select_text += " , Q{i}.value as val{i}".Replace("{i}", istr);
                                res.from_text += " LEFT JOIN GKS..GKS_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.gks_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year}".Replace("{i}", istr).Replace("{year}", line.period.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                or_value.Add("Q{i}.issuer_id".Replace("{i}", istr));

                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if ((from != null) || (to != null))
                                {
                                    or_text += " or (";
                                    if (from != null)
                                    {
                                        or_text += "Q{i}.value>=@from{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                    }
                                    if (to != null)
                                    {
                                        if (from != null) or_text += " and ";
                                        or_text += "Q{i}.value<=@to{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                    }
                                    or_text += ")";
                                }
                            }
                            if (or_text != "")
                            {
                                res.where_text += " and (" + or_text.Substring(3) + ")";
                            }
                            else
                            {
                                if (or_value.Count == 1)
                                {
                                    res.where_text += " and " + or_value[0] + " is not null";
                                }
                                else
                                {
                                    res.where_text += " and coalesce(" + String.Join(",", or_value) + ") is not null";
                                }
                            }
                        }
                        else
                        {
                            //and
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];
                                res.select_text += " , Q{i}.value as val{i}".Replace("{i}", istr);
                                res.from_text += " INNER JOIN GKS..GKS_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.gks_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year}".Replace("{i}", istr).Replace("{year}", line.period.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if (from != null)
                                {
                                    res.where_text += " and Q{i}.value>=@from{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                }
                                if (to != null)
                                {
                                    res.where_text += " and Q{i}.value<=@to{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                }
                            }
                        }
                        break;
                    //-----------------------------
                    case QIVSearchType.NAUFOR:
                        if (param.andor == "or")
                        {
                            //or
                            string or_text = "";
                            List<string> or_value = new List<string>();
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];
                                int year = line.period / 10;
                                int quarter = line.period % 10;

                                res.select_text += " , cast(Q{i}.value as decimal) as val{i}".Replace("{i}", istr);
                                res.from_text += " LEFT JOIN NAUFOR..Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year} and Q{i}.quarter = {quarter}".Replace("{i}", istr).Replace("{year}", year.ToString()).Replace("{quarter}", quarter.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                or_value.Add("Q{i}.issuer_id".Replace("{i}", istr));


                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if ((from != null) || (to != null))
                                {
                                    or_text += " or (";
                                    if (from != null)
                                    {
                                        or_text += "cast(Q{i}.value as decimal)>=@from{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                    }
                                    if (to != null)
                                    {
                                        if (from != null) or_text += " and ";
                                        or_text += "cast(Q{i}.value as decimal)<=@to{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                    }
                                    or_text += ")";
                                }
                            }
                            if (or_text != "")
                            {
                                res.where_text += " and (" + or_text.Substring(3) + ")";
                            }
                            else
                            {
                                if (or_value.Count == 1)
                                {
                                    res.where_text += " and " + or_value[0] + " is not null";
                                }
                                else
                                {
                                    res.where_text += " and coalesce(" + String.Join(",", or_value) + ") is not null";
                                }
                            }
                        }
                        else
                        {
                            //and
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];
                                int year = line.period / 10;
                                int quarter = line.period % 10;

                                res.select_text += " , cast(Q{i}.value as decimal) as val{i}".Replace("{i}", istr);
                                res.from_text += " INNER JOIN NAUFOR..Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year} and Q{i}.quarter = {quarter}".Replace("{i}", istr).Replace("{year}", year.ToString()).Replace("{quarter}", quarter.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if (from != null)
                                {
                                    res.where_text += " and cast(Q{i}.value as decimal)>=@from{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                }
                                if (to != null)
                                {
                                    res.where_text += " and cast(Q{i}.value as decimal)<=@to{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                }
                            }
                        }
                        break;
                    //-----------------------------
                    case QIVSearchType.MSFO:
                        if (param.andor == "or")
                        {
                            //or
                            string or_text = "";
                            List<string> or_value = new List<string>();
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];
                                //int year = line.period / 10;
                                //int quarter = line.period % 10;

                                res.select_text += " , cast(Q{i}.value as decimal) as val{i}".Replace("{i}", istr);
                                //from_text += " LEFT JOIN NAUFOR..MSFO_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year} and Q{i}.quarter = {quarter}".Replace("{i}", istr).Replace("{year}", line.period.ToString()).Replace("{quarter}", quarter.ToString());
                                res.from_text += " LEFT JOIN NAUFOR..MSFO_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year}".Replace("{i}", istr).Replace("{year}", line.period.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                or_value.Add("Q{i}.issuer_id".Replace("{i}", istr));

                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if ((from != null) || (to != null))
                                {
                                    or_text += " or (";
                                    if (from != null)
                                    {
                                        or_text += "cast(Q{i}.value as decimal)>=@from{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                    }
                                    if (to != null)
                                    {
                                        if (from != null) or_text += " and ";
                                        or_text += "cast(Q{i}.value as decimal)<=@to{i}".Replace("{i}", istr);
                                        res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                    }
                                    or_text += ")";
                                }
                            }
                            if (or_text != "")
                            {
                                res.where_text += " and (" + or_text.Substring(3) + ")";
                            }
                            else
                            {
                                if (or_value.Count == 1)
                                {
                                    res.where_text += " and " + or_value[0] + " is not null";
                                }
                                else
                                {
                                    res.where_text += " and coalesce(" + String.Join(",", or_value) + ") is not null";
                                }
                            }
                        }
                        else
                        {
                            //and
                            for (int i = 0; i < param.param_lines.Count; i++)
                            {
                                string istr = i.ToString();
                                QIVSearchLines line = param.param_lines[i];
                                //int year = line.period / 10;
                                //int quarter = line.period % 10;

                                res.select_text += " , cast(Q{i}.value as decimal) as val{i}".Replace("{i}", istr);
                                //from_text += " INNER JOIN NAUFOR..MSFO_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year} and Q{i}.quarter = {quarter}".Replace("{i}", istr).Replace("{year}", year.ToString()).Replace("{quarter}", quarter.ToString());
                                res.from_text += " INNER JOIN NAUFOR..MSFO_Quart_Indic_Values Q{i} on Q{i}.issuer_id=A.issuer_id and Q{i}.indic_id = @indic{i} and Q{i}.year = {year}".Replace("{i}", istr).Replace("{year}", line.period.ToString());
                                res.sqlparams.Add(new SQLParamInt("@indic" + istr, line.param_id));
                                line.from = (line.from != "") ? line.from : null;
                                decimal? from = Helper.GetDecimalFromText(line.from);
                                line.to = (line.to != "") ? line.to : null;
                                decimal? to = Helper.GetDecimalFromText(line.to);
                                if (from != null)
                                {
                                    res.where_text += " and cast(Q{i}.value as decimal)>=@from{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@from" + istr, from));
                                }
                                if (to != null)
                                {
                                    res.where_text += " and cast(Q{i}.value as decimal)<=@to{i}".Replace("{i}", istr);
                                    res.sqlparams.Add(new SQLParamNumeric("@to" + istr, to));
                                }
                            }
                        }
                        break;
                }
            }
            return res;
        }

        public static async Task<QIVSearchResult>  DoSearch(QIVSearchParams param)
        {
            var ret = new QIVSearchResult();
            ret.results = new List<QIVSearchResultLine>();
            QIVQuery qtext = await GetSearchQuery(param,QIVResultType.Web);
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                //Запрос по количеству
                using (Query query = await sql.OpenQueryAsync("select count(*) as cnt " + qtext.from_text + qtext.where_text, qtext.sqlparams.ToArray<SQLParam>()))
                {
                    if (await query.ReadAsync())
                    {
                        ret.total = (int)query.GetFieldAsInt("cnt");
                    }
                }

                //Сортировка и постраничный вывод 
                //string bottom_text = " ORDER BY A.group_id, A.id OFFSET "+((param.page_no-1)*param.rcount).ToString()+" ROWS FETCH NEXT "+param.rcount.ToString()+" ROWS ONLY";
                param.sort_col = param.sort_col + 7;

                string bottom_text = " ORDER BY " + param.sort_col + " " + ((param.sort_direct == 0)?"asc":"desc") + " OFFSET "  + ((param.page_no - 1) * param.rcount).ToString() + " ROWS FETCH NEXT " + param.rcount.ToString() + " ROWS ONLY";
                
                //Запрос
                string q = qtext.select_text + qtext.from_text + qtext.where_text + bottom_text;
                using (Query query = await sql.OpenQueryAsync(qtext.select_text + qtext.from_text + qtext.where_text + bottom_text, qtext.sqlparams.ToArray<SQLParam>()))
                {
                    while (await query.ReadAsync())
                    {
                        var pvalues = new List<decimal?>();
                        for (int i = 0; i < param.param_lines.Count; i++)
                        {
                            pvalues.Add(query.GetFieldAsDecimal("val" + i.ToString()));
                        }
                        ret.results.Add(new QIVSearchResultLine() { inn = query.GetFieldAsString("inn"), ogrn = query.GetFieldAsString("ogrn"), name = query.GetFieldAsString("name"), ticker = query.GetFieldAsString("ticker"), issuer_id = query.GetFieldAsString("issuer_id"), type_id = query.GetFieldAsString("type_id"), param_values = pvalues });
                    }
                }
            }
            return ret;
        }

        public static async Task<int> DoSearchToGroup(QIVSearchParams param, int user_id, int group_limit)
        {
            QIVQuery qtext = await GetSearchQuery(param, QIVResultType.Group);
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                //Сортировка и постраничный вывод
                string  bottom_text = " ORDER BY A.group_id, A.id OFFSET " + ((param.page_no - 1) * param.rcount).ToString() + " ROWS FETCH NEXT " + param.rcount.ToString() + " ROWS ONLY";

                if (param.page_no < 0)
                {
                    bottom_text = " ORDER BY A.group_id, A.id OFFSET 0 ROWS FETCH NEXT "+group_limit+" ROWS ONLY"; //10000
                }
                
                
                //Запрос
                using (Query query = await sql.OpenQueryAsync(qtext.select_text + qtext.from_text + qtext.where_text + bottom_text, qtext.sqlparams.ToArray<SQLParam>()))
                {
                    var sb = new StringBuilder();
                    while (await query.ReadAsync())
                    {
                        //sb.AppendLine(query.GetFieldAsString("issuer_id") + "_" + query.GetFieldAsString("type_id"));
                        sb.Append(query.GetFieldAsString("issuer_id") + "_" + query.GetFieldAsString("type_id") + ",");
                    }
                    int count;
                    int? id = param.group_id;
                    string newname = param.group_name;
                    AuthenticateSqlUtilites.SaveGroup(user_id, ref id, ref newname, sb.ToString(), out count, true);
                    return count;
                }
            }
        }

        public static async Task<ExcelResult> DoSearchToExcel(QIVSearchParams param)
        {
            var ret = new QIVSearchResult();
            ret.results = new List<QIVSearchResultLine>();
            QIVQuery qtext = await GetSearchQuery(param, QIVResultType.Excel);
            ExcelResult res = new ExcelResult();

            var pname = new List<string>();

            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                //Сортировка
                param.sort_col = param.sort_col + 23;
                string bottom_text = " ORDER BY " + param.sort_col + " " + ((param.sort_direct == 0) ? "asc" : "desc") + " OFFSET 0 ROWS FETCH NEXT 10000 ROWS ONLY";

/*
                string bottom_text = " ORDER BY 23 desc";
                if (param.page_no < 0)
                {
                    bottom_text = " ORDER BY 23 desc OFFSET 0 ROWS FETCH NEXT 10000 ROWS ONLY"; //10000
                }
 */ 
                //Экспорт списка компаний
                if (!String.IsNullOrEmpty(param.issuers))
                {
                    param.issuers = "'" + param.issuers.Replace(",", "','") + "'";
                    qtext.where_text = @"
                        WHERE 1=1 and A.issuer_id in (" + param.issuers + ")";
                }

                res.AddHeader(0, "Наименование", "name", "s");
                res.AddHeader(1, "Регион", "region_name", "s");
                res.AddHeader(2, "Полное наименование", "full_name", "s");
                res.AddHeader(3, "Код СКРИН", "ticker", "s");
                res.AddHeader(4, "ОПФ", "opf_name", "s");
                res.AddHeader(5, "ОКФС", "Okfs_name", "s");
                res.AddHeader(6, "ИНН", "inn", "s");
                res.AddHeader(7, "ОКПО", "okpo", "s");
                res.AddHeader(8, "ОГРН", "ogrn", "s");
                res.AddHeader(9, "Дата присвоения ОГРН", "ogrn_date", "s");
                res.AddHeader(10, "Наименование регистрирующего органа, внесшего запись", "reg_org_name", "s");
                res.AddHeader(11, "Отрасль", "okved_name", "s");
                res.AddHeader(12, "ОКВЭД", "okved_code", "s");
                res.AddHeader(13, "Адрес", "legal_address", "s");
                res.AddHeader(14, "Руководитель", "ruler", "s");
                res.AddHeader(15, "Телефон", "phone", "s");
                res.AddHeader(16, "Факс", "fax", "s");
                res.AddHeader(17, "E-mail", "email", "s");
                res.AddHeader(18, "WWW", "www", "s");
                for (int i = 0; i < param.param_lines.Count; i++)
                {
                    QIVSearchLines line = param.param_lines[i];
                    var period = "";
                    if(line.period.ToString().Length >4)
                    {
                        period = (line.period % 10).ToString() + "кв. " + (line.period / 10).ToString() + "г." ;
                    }
                    else
                    {
                        period = line.period.ToString() + "г.";
                    }
                    res.AddHeader(19 + i, period + " " + GetParamName(param.qiv_type, line.param_id, line.param_type), "val" + i.ToString(), "n");
                }

                //Запрос
                using (Query query = await sql.OpenQueryAsync(qtext.select_text + qtext.from_text + qtext.where_text + bottom_text, qtext.sqlparams.ToArray<SQLParam>()))
                {
                    while (await query.ReadAsync())
                    {
                        res.AddValue(0, query.GetFieldAsString("name"));
                        res.AddValue(1, query.GetFieldAsString("region_name"));
                        res.AddValue(2, query.GetFieldAsString("full_name"));
                        res.AddValue(3, query.GetFieldAsString("ticker"));
                        res.AddValue(4, query.GetFieldAsString("opf_name"));
                        res.AddValue(5, query.GetFieldAsString("Okfs_name"));
                        res.AddValue(6, query.GetFieldAsString("inn"));
                        res.AddValue(7, query.GetFieldAsString("okpo"));
                        res.AddValue(8, query.GetFieldAsString("ogrn"));
                        res.AddValue(9, query.GetFieldAsString("ogrn_date"));
                        res.AddValue(10, query.GetFieldAsString("reg_org_name"));
                        res.AddValue(11, query.GetFieldAsString("okved_name"));
                        res.AddValue(12, query.GetFieldAsString("okved_code"));
                        res.AddValue(13, query.GetFieldAsString("legal_address"));
                        res.AddValue(14, query.GetFieldAsString("ruler"));
                        res.AddValue(15, query.GetFieldAsString("phone"));
                        res.AddValue(16, query.GetFieldAsString("fax"));
                        res.AddValue(17, query.GetFieldAsString("email"));
                        res.AddValue(18, query.GetFieldAsString("www"));
                        for (int i = 0; i < param.param_lines.Count; i++)
                        {
                            decimal? v = query.GetFieldAsDecimal("val" + i.ToString());
                            if (v == null)
                                res.AddValue(19 + i, "");
                            else
                                res.AddValue(19 + i, ((decimal)v).ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
            }


                return res;
        }
/*
        public static XLWorkbook GetQIVSearchExcel(ExcelResult result)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Выборка");

            int i_count = result.GetFieldCount();

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            for (int i = 0; i < i_count; i++)
            {
                ws.Column(i + 1).Width = i == 0 ? 100 : 50;
                ws.Cell(1, i + 1).Value = result.GetHeaderName(i);
            }


            for (int i = 0, i_max = result.GetRowCount(); i < i_max; i++)
            {
                ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                for (int j = 0; j < i_count; j++)
                {
                    ws.Cell(i + 2, j + 1).Value = result.GetVal(j, i);
                    if (ws.Cell(i + 2, j + 1).DataType == XLCellValues.Number)
                    {
                        ws.Cell(i + 2, j + 1).Style.NumberFormat.SetFormat("#,##0.00");
                        ws.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                }
            }

            return wb;

        }
 */ 

        public static async Task<List<TpriceTemplate>> GetTemplates(int user_id)
        {
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                using (Query q = await sql.OpenQueryAsync(@"Select id,name,tcontent from skrin_net..qivsearch_template where user_id=@user_id order by id",new SQLParamInt("user_id",user_id)))
                {
                    var ret = new List<TpriceTemplate>();
                    ret.Add(new TpriceTemplate
                    {
                        id = 0,
                        name = "Нет"
                    });
                    while (await q.ReadAsync())
                    {
                        ret.Add(new TpriceTemplate
                        {
                            id = (int)q.GetFieldAsInt("id"),
                            name = q.GetFieldAsString("name")
                        });
                    }
                    return ret;
                }
            }
        }

        public static async Task<string> GetTemplate(int user_id, int id)
        {
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                using (Query q = await sql.OpenQueryAsync(@"Select tcontent from skrin_net..qivsearch_template where user_id=@user_id and id=@id", new SQLParamInt("user_id", user_id), new SQLParamInt("id", id)))
                {
                    if (await q.ReadAsync())
                    {
                        return q.GetFieldAsString("tcontent");
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static async Task SaveTemplate(int user_id, string name, string template)
        {
            using (SQL sql = new SQL(Configs.ConnectionString))
            {
                await sql.ExecQueryAsync(@"insert into skrin_net..qivsearch_template (user_id,name,tcontent) VALUES (@user_id,@name,@tcontent)", new SQLParamInt("user_id", user_id), new SQLParamVarchar("name", name), new SQLParamVarchar("tcontent", template));

            }
        }
    }
}