using Skrin.BLL.Infrastructure;
using Skrin.Models.Tree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Root
{
    public static class TreeRepository
    {
        public static async Task<List<CodeLine>> GetCodeLinesAsync(int src, int node_id)
        {
            List<CodeLine> ret = new List<CodeLine>();
            string sql = "";
            switch (src)
            {
                case 0:
                    sql = "Select id, subject as name,(select top 1 1 from okato where parentid=a.id) as hc,parentid, null extra_info from okato a where parentid =" + node_id + ((node_id == 0) ? " or parentid in (select id from okato where parentid=0)" : "") + " order by parentid,okato";
                    break;
                case 1:
                    //sql = "select id,kod+'. ' + Replace(name,'\"','&quot;') as name, (select top 1 1 from searchdb2..OKVEDs where parentid=a.id) as hc,parentid, null extra_info from searchdb2..OKVEDs  a where parentid=" + node_id + " order by parentid, kod";
                    sql = "select id, short_name name, (select top 1 1 from naufor..okveds3 where parent_id=a.id) as hc,parent_id, a.name_json extra_info from naufor..okveds3  a where parent_id=" + node_id + " order by parent_id, ord";
                    break;
                case 2:
                    sql = "select id,kod+'. ' + Replace(name,'\"','&quot;') as name, (select top 1 1 from searchdb2..okonh where parent_id=a.id) as hc,parent_id, null extra_info from searchdb2..okonh  a where parent_id=" + node_id + " order by parent_id, kod";
                    break;
                case 3:
                    sql = "Select id,name,(select top 1 1 from searchdb2..okfs where parent_id=a.id) as hc,parent_id, null extra_info from searchdb2..okfs a where parent_id=" + node_id + ((node_id == 0) ? " or parent_id in (select id from searchdb2..okfs  where parent_id=0)" : "") + " order by parent_id,id";
                    break;
                case 4:
                    sql = "Select id,cast(id as varchar(5)) + '.' + name,(select top 1 1 from searchdb2..okopf where parent_id=a.id) as hc,parent_id, null extra_info from searchdb2..okopf a where parent_id=" + node_id + ((node_id == 0) ? " or parent_id in (select id from searchdb2..okopf  where parent_id=0)" : "") + " order by parent_id,id";
                    break;
                case 5:
                    sql = "Select id,name,(select top 1 1 from searchdb2..regions where parent_id=a.id) as hc,parent_id, null extra_info from searchdb2..regions a where parent_id=" + node_id + ((node_id == 0) ? " or parent_id in (select id from searchdb2..regions  where parent_id=0)" : "") + " order by parent_id,id";
                    //sql = "select id, name, null as hc, null as parent_id, null as extra_info, case when id = 77 then 2  when id = 78 then 1  else 0  end as ordered from naufor..Regions a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where is_deleted is null and id > 0 order by ordered desc, id asc";
                    break;
                case 6:
                    sql = "Select id,name, 0 as hc,0 as parent_id, null extra_info from naufor..issuer_statuses a order by id";
                    break;
                case 8:
                    sql = "Select id, Replace(Replace(Replace(name,'\"','&quot;'),char(10),' '),char(13),' ') as name,(select top 1 1 from skrin_content_output..Bargains_types where parent_id=a.id) as hc,parent_id, null extra_info from skrin_content_output..Bargains_types a where parent_id ='" + node_id + "'" + ((node_id == 0) ? " or parent_id in (select id from skrin_content_output..Bargains_types where parent_id='0')" : "") + " order by parent_id,so,name";
                    break;
                case 9:
                    sql = "select id,case id when 146 then ' ' else '' end +name as name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..fas_regions_tree order by parent_id,2";
                    break;
                case 10:
                    sql = "Select id,name, case when parent_id=0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..columns2show a where is_short=0 order by id";
                    break;
                case 11:
                    sql = "select id,code+'. ' + Replace(name,'\"','&quot;') as name, (select top 1 1 from Zakupki..Dic_Okdp where parent_id=a.id) as hc,parent_id, null extra_info from Zakupki..Dic_Okdp  a where parent_id=" + node_id + " order by parent_id, code";
                    break;
                case 12:
                    sql = "select id,name, (select top 1 1 from naufor..Vestnik_Types where parent_id=a.id) as hc,parent_id, null extra_info from naufor..Vestnik_Types  a where parent_id= " + node_id + ((node_id == 0) ? " or parent_id in (select id from naufor..Vestnik_Types where parent_id=0)" : "") + " order by parent_id,so";
                    break;
                case 13:
                    sql = "select id,name, (select top 1 1 from skrin_content_output..vFedresurs_messages_types2 where parent_id=a.id) as hc,parent_id, null extra_info from skrin_content_output..vFedresurs_messages_types2 a where parent_id= " + node_id + ((node_id == 0) ? " or parent_id in (select id from skrin_content_output..vFedresurs_messages_types2 where parent_id=0)" : "") + " order by abs(parent_id),name";
                    break;
                case 14:
                    sql = "select id,name, (select top 1 1 from skrin_content_output..disclosure_mt where parent_id=a.id) as hc,parent_id, null extra_info from skrin_content_output..disclosure_mt a where parent_id= " + node_id + ((node_id == 0) ? " or parent_id in (select id from skrin_content_output..disclosure_mt where parent_id=0)" : "") + " order by abs(parent_id),name";
                    break;
                case 15:
                    sql = "select id,name, (select top 1 1 from skrin_content_output..analytic_menu where parent_id=a.id) as hc,parent_id, null extra_info from skrin_content_output..analytic_menu a where parent_id= " + node_id + ((node_id == 0) ? " or parent_id in (select id from skrin_content_output..analytic_menu where parent_id=0)" : "") + " order by abs(parent_id),id";
                    break;
                case 17:
                    sql = "select id, case isNull(code, '') when '' then '' else '. ' end + Replace(name,'\"','&quot;') as name, (select top 1 1 from Zakupki..Dic_Product_Codes where parent_id=a.id) as hc,parent_id, null extra_info from Zakupki..vw_Dic_Product_Codes  a where parent_id=" + node_id + " order by parent_id, code";
                    break;
                case 18:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..event_types_tree order by parent_id,id";
                    break;
                case 19:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..bankrot_types_tree order by id";
                    break;
                case 20:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..ac_types_tree order by parent_id, name";
                    break;
                case 21:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..disput_types_tree order by parent_id,id";
                    break;
                case 22:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..side_types_tree order by parent_id,id";
                    break;
                case 23:
                    sql = "select id, name, case parent_id when 0 then 1 else 0 end as hc,parent_id, null extra_info from skrin_content_output..fedresurs_types_tree2 order by parent_id,id";
                    break;
                case 24:
                    sql = "select id, short_name name, (select top 1 1 from naufor..okveds3 where parent_id=a.id) as hc,parent_id, a.name_json extra_info from naufor..okveds3  a where parent_id=" + node_id + " order by parent_id, ord";
                    break;
                case 25:
                    sql = "select id,name, (select top 1 1 from skrin_content_output..analytic_menu where parent_id=a.id) as hc,parent_id, null extra_info from skrin_content_output..analytic_menu a where parent_id= " + node_id + ((node_id == 0) ? " or parent_id in (select id from skrin_content_output..analytic_menu where parent_id=0)" : "") + " order by abs(parent_id),id";
                     break;
                case 26:
                     sql = "Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 999 parent_id, null  extra_info, [Ordr] from kz.dbo.ut_ClssEntState a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 'Все' name,1 hc,0 parent_id, null extra_info, 0 order by Ordr";
                     break;
                case 27:
                     sql = "Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 999 parent_id, null extra_info from kz.dbo.ut_ClssOwnership a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 'Все' name,1 hc,0 parent_id, null extra_info order by Dscr";
                     break;
                case 28:
                     sql = "SELECT 999 id, 'Все' name,1 hc,0 parent_id, null extra_info union all Select Id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc,999 parent_id, null extra_info from kz.dbo.ut_ClssEconomyType a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where Id<>0 ";
                     break;
                case 29:
                     sql = "SELECT 999 id, 'Все' name,1 hc,0 parent_id, null extra_info union all Select id, Quantity + ' человек' as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 999 parent_id, null extra_info from kz.dbo.ut_ScaleEntQuantity a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where Id<>0 ";
                     break;
                case 30:
                     sql = "SELECT 999 id, 'Все' name,1 hc,0 parent_id, null extra_info union all Select Id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 999 parent_id, null extra_info from kz.dbo.ut_ClssEntSize a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id where Id<>0";
                     break;
                case 31:
                     sql = "Select Id, RuName,(select top 1 1 from kz..Dic_Kato where ParentId=a.Id) as hc,ParentId,null extra_info from kz..Dic_Kato a where parentid =" + node_id + ((node_id == 0) ? " OR parentid in (select id from kz..Dic_Kato WHERE parentid = 0) " : "") + " order by ParentId,Code";
                     break;
                case 32:
                     sql = " select Id,Code+'. ' + Name  as Name, (select top 1 1 from kz..Dic_OKED where ParentId=a.Id) as hc, parentId,null extra_info  from kz..Dic_OKED  a where parentId=" + node_id + ((node_id == 0 ? " or parentid in (select id from kz..Dic_OKED WHERE parentid = 0) " : "")) + " order by parentId, Code";
                     break;
                case 41: //Украина
                     sql = "Select id, cast(Id as varchar(5)) + '.' + DscrRU as name, (select top 1 1 from ua3..Dic_OPF where parentId=a.Id) as hc, parentId as parentId, null extra_info from ua3..Dic_OPF a where parentId=" + node_id + ((node_id == 0) ? " or parentId in (select id from ua3..Dic_OPF where parentId=0)" : "") + " order by parentId, Id";
                     break;
                case 42:
                     sql = "Select id, name, (select top 1 1 from UA3..Dic_Koatuu where ParentId=a.Id) as hc, ParentId as parent_id, null extra_info from UA3..Dic_Koatuu a where parentid =" + node_id + ((node_id == 0) ? " or parentId in (select id from UA3..Dic_Koatuu where parentId=0)" : "") + " order by ParentId, Koatuu";
                     break;
                case 43:
//                     sql = "Select Id, RuName as name, case  when kod is null  or len(kod)=0 then 0 else 1 end as sel from ua2.dbo.Dic_Area a left join (Select kod from searchdb2.dbo.KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id  order by Name";
                     sql = "Select id, RuName as name, hc=0, parent_id=0, null extra_info from ua3..Dic_Area a left join (Select kod from searchdb2..KodeSplitter(0,'" + node_id + "')) b on b.kod=a.id  order by Name";
                     break;
                case 44:
                case 45:
                     sql = "select id, Code+'. ' + RUName collate database_default as name, (select top 1 1 from UA3..Dic_Kved where ParentId=a.Id) as hc, parentId as parent_id, null extra_info from UA3..Dic_Kved a where parentId=" + node_id + " and isOld=0 " + (node_id == 0 ? " or parentId in (select id from UA3..Dic_Kved  where parentId=0 and isOld=0 )" : "") + "  order by parentId, Code";
                     break;
            }
            if (sql != "")
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int? hc = (int?)reader.ReadNullIfDbNull(2);

                            ret.Add(new CodeLine
                            {
                                id = (int)reader[0],
                                title = (string)reader.ReadNullIfDbNull(1),
                                isFolder = hc.HasValue && hc == 1,
                                parent = (int?)reader.ReadNullIfDbNull(3) ?? 0,
                                extra_info = (string)reader.ReadNullIfDbNull(4),
                                src = src
                            });
                        }
                    }
                }
            }
            return ret;
        }


        public static async Task<List<ExpandedCodeLine>> GetExpandedCodeLinesAsync(int src, string node_ids)
        {
            List<ExpandedCodeLine> ret = new List<ExpandedCodeLine>();
            string sql = "";
            switch (src)
            {
                case 0:
                    sql = ";WITH C ([ID], ParentID, [Subject],[level]) AS " +
                        "( " +
                        "SELECT B.ID, B.ParentID, " +
                        "B.[Subject],0 as [level] " +
                        "FROM okato AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.ParentID, " +
                        "D.[Subject],([Level] + 1) as [Level] " +
                        "FROM Okato AS D " +
                        "INNER JOIN C " +
                        "ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, Replace(d.Subject,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  then 1 else 0 end as selected, " +
                        "case  when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.ParentID else 0 end as expanded,isnull((select top 1 1 from okato where parentid=d.id),0) as hc " +
                        "FROM C inner join okato d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc";
                    break;
                case 1:
                    /*sql = ";WITH C ([ID], ParentID, [name],[level]) AS " +
                        "(SELECT B.ID, B.ParentID, B.name,0 as [level] " +
                        "FROM searchdb2..okveds AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.ParentID, " +
                        "D.name,([Level] + 1) as [Level] " +
                        "FROM searchdb2..okveds AS D " +
                        "INNER JOIN C ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, kod+'. ' + Replace(d.name,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parentid else 0 end as expanded, isnull((select top 1 1 from searchdb2..okveds where parentid=d.id),0) as hc " +
                        "FROM C  inner join searchdb2..okveds d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc";*/

                    sql = string.Format(@";WITH C ([ID], parent_id, [name],[level]) AS 
                        (SELECT B.ID, B.parent_id, B.short_name,0 as [level] 
                        FROM naufor..okveds3 AS B WHERE B.[ID] in ({0}) 
                        UNION ALL 
                        SELECT D.[ID], D.parent_id, 
                        D.short_name,([Level] + 1) as [Level] 
                        FROM naufor..okveds3 AS D 
                        INNER JOIN C ON C.parent_id = D.ID) 
                        SELECT [level],d.ID, d.Parent_ID, short_name as name,case when d.id in ({0}) then 1 else 0 end as selected, 
                        case when c.id=d.id and not c.id in ({0}) then d.parent_id else 0 end as expanded, isnull((select top 1 1 from naufor..okveds3 where parent_id=d.id),0) as hc, d.name_json extra_info 
                        FROM C  inner join naufor..okveds3 d on c.parent_id=d.parent_id 
                        where d.parent_id>=0 order by [level] desc", string.Join(",", node_ids.ParamVals().Keys));

                    break;
                case 2:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                        "(SELECT B.ID, B.Parent_ID, 0 as [level] " +
                        "FROM searchdb2..okonh AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.Parent_ID, " +
                        "([Level] + 1) as [Level] " +
                        "FROM searchdb2..okonh AS D " +
                        "INNER JOIN C ON C.Parent_ID = D.ID) " +
                        "SELECT [level],d.ID, d.Parent_ID, kod+'. ' + Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from searchdb2..okonh where parent_id=d.id),0) as hc " +
                        "FROM C  inner join searchdb2..okonh d on c.parent_id=d.parent_id " +
                        "where d.parent_id>=0 order by [level] desc";
                    break;
                case 3:
                    sql = ";WITH C ([ID], parent_id, [level]) AS  " +
                        "(SELECT B.ID, B.parent_id, 0 as [level]  " +
                        "FROM searchdb2..okfs AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  " +
                        "UNION ALL  " +
                        "SELECT D.[ID], D.parent_id,  " +
                        "([Level] + 1) as [Level]  " +
                        "FROM searchdb2..okfs AS D  " +
                        "INNER JOIN C ON c.parent_ID = d.ID)  " +
                        "SELECT distinct [level],d.ID, d.parent_id, cast(d.id as varchar(5))+'. ' + Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected,  " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from searchdb2..okfs where parent_id=d.id),0) as hc  " +
                        "FROM (Select distinct * from C) C   inner join searchdb2..okfs d on c.parent_id=d.parent_id  " +
                        "where d.parent_id>=0 order by [level] desc";
                    break;
                case 4:
                    sql = ";WITH C ([ID], parent_id, [level]) AS  " +
                        "(SELECT B.ID, B.parent_id, 0 as [level]  " +
                        "FROM searchdb2..okopf AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  " +
                        "UNION ALL  " +
                        "SELECT D.[ID], D.parent_id,  " +
                        "([Level] + 1) as [Level]  " +
                        "FROM searchdb2..okopf AS D  " +
                        "INNER JOIN C ON c.parent_ID = d.ID)  " +
                        "SELECT distinct [level],d.ID, d.parent_id, cast(d.id as varchar(5))+'. ' + Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected,  " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from searchdb2..okopf where parent_id=d.id),0) as hc  " +
                        "FROM (Select distinct * from C) C   inner join searchdb2..okopf d on c.parent_id=d.parent_id  " +
                        "where d.parent_id>=0 order by [level] desc";
                    break;
                case 5:
                    sql = "Select id,name,(select top 1 1 from searchdb2..regions where parent_id=a.id) as hc,kod as code,0,0 from searchdb2..regions a where parent_id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") order by id";
                    break;
                case 8:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                    "    (SELECT B.ID, parent_id, 0 as [level]  " +
                    "	FROM skrin_content_output..Bargains_types AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                    "   UNION ALL " +
                    "    SELECT D.[ID], D.Parent_ID, " +
                    "    ([Level] + 1) as [Level] " +
                    "    FROM skrin_content_output..Bargains_types AS D " +
                    "    INNER JOIN C ON C.ID = D.parent_id) " +
                    "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..Bargains_types where parent_id=d.id),0) as hc " +
                        "FROM C  inner join skrin_content_output..Bargains_types D on c.id=d.id order by [level] desc";
                    break;
                case 9:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                    "    (SELECT B.ID, parent_id, 0 as [level]  " +
                    "	FROM skrin_content_output..fas_regions_tree AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                    "   UNION ALL " +
                    "    SELECT D.[ID], D.Parent_ID, " +
                    "    ([Level] + 1) as [Level] " +
                    "    FROM skrin_content_output..fas_regions_tree AS D " +
                    "    INNER JOIN C ON C.ID = D.parent_id) " +
                    "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..fas_regions_tree where parent_id=d.id),0) as hc " +
                        "FROM C  inner join skrin_content_output..fas_regions_tree D on c.id=d.id order by [level] desc";
                    break;
                case 10:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                        "(SELECT B.ID, parent_id, 0 as [level]  " +
                        "FROM gks..columns2show AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  and is_short=0 " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.Parent_ID, " +
                        "([Level] + 1) as [Level] " +
                        "FROM skrin_content_output..columns2show AS D " +
                        "INNER JOIN C ON C.ID = D.parent_id) " +
                        "SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..columns2show where parent_id=d.id),0) as hc " +
                        "FROM C  inner join skrin_content_output..columns2show D on c.id=d.id order by [level] desc";
                    break;
                case 11:
                    sql = ";WITH C ([ID], Parent_ID, [name],[level]) AS " +
                        "(SELECT B.ID, B.Parent_ID, B.name,0 as [level] " +
                        "FROM Zakupki..Dic_Okdp AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.Parent_ID, " +
                        "D.name,([Level] + 1) as [Level] " +
                        "FROM Zakupki..Dic_Okdp AS D " +
                        "INNER JOIN C ON C.Parent_ID = D.ID) " +
                        "SELECT [level],d.ID, d.Parent_ID, code+'. ' + Replace(d.name,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from Zakupki..Dic_Okdb where parent_id=d.id),0) as hc " +
                        "FROM C  inner join Zakupki..Dic_Okdp d on c.parent_id=d.parent_id " +
                        "where d.parent_id>=0 order by [level] desc ";
                    break;
                case 12:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                    "    (SELECT B.ID, parent_id, 0 as [level]  " +
                    "	FROM naufor..Vestnik_Types AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                    "   UNION ALL " +
                    "    SELECT D.[ID], D.Parent_ID, " +
                    "    ([Level] + 1) as [Level] " +
                    "    FROM naufor..Vestnik_Types AS D " +
                    "    INNER JOIN C ON C.ID = D.parent_id) " +
                    "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from naufor..Vestnik_Types where parent_id=d.id),0) as hc " +
                        "FROM C  inner join naufor..Vestnik_Types D on c.id=d.id order by [level] desc";
                    break;
                case 13:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                    "    (SELECT B.ID, parent_id, 0 as [level]  " +
                    "	FROM skrin_content_output..vFedresurs_messages_types2 AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                    "   UNION ALL " +
                    "    SELECT D.[ID], D.Parent_ID, " +
                    "    ([Level] + 1) as [Level] " +
                    "    FROM skrin_content_output..vFedresurs_messages_types2 AS D " +
                    "    INNER JOIN C ON C.ID = D.parent_id) " +
                    "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..vFedresurs_messages_types2 where parent_id=d.id),0) as hc " +
                        "FROM C  inner join skrin_content_output..vFedresurs_messages_types2 D on c.id=d.id order by [level] desc";
                    break;
                case 14:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                 "    (SELECT B.ID, parent_id, 0 as [level]  " +
                 "	FROM skrin_content_output..disclosure_mt AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                 "   UNION ALL " +
                 "    SELECT D.[ID], D.Parent_ID, " +
                 "    ([Level] + 1) as [Level] " +
                 "    FROM skrin_content_output..disclosure_mt AS D " +
                 "    INNER JOIN C ON C.ID = D.parent_id) " +
                 "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                     "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..disclosure_mt where parent_id=d.id),0) as hc " +
                     "FROM C  inner join skrin_content_output..disclosure_mt D on c.id=d.id order by [level] desc";
                    break;
                case 15:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS " +
                 "    (SELECT B.ID, parent_id, 0 as [level]  " +
                 "	FROM skrin_content_output..analytic_menu AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                 "   UNION ALL " +
                 "    SELECT D.[ID], D.Parent_ID, " +
                 "    ([Level] + 1) as [Level] " +
                 "    FROM skrin_content_output..analytic_menu AS D " +
                 "    INNER JOIN C ON C.ID = D.parent_id) " +
                 "    SELECT [level],d.ID, d.Parent_ID, Replace(d.name,'\"','&quot;') as name,case when d.id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                     "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from skrin_content_output..analytic_menu where parent_id=d.id),0) as hc " +
                     "FROM C  inner join skrin_content_output..analytic_menu D on c.id=d.id order by [level] desc";
                    break;
                case 17:
                    sql = ";WITH C ([ID], Parent_ID, [name],[level]) AS " +
                         "(SELECT B.ID, B.Parent_ID, B.name,0 as [level] " +
                         "FROM Zakupki..Dic_Product_Codes AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                         "UNION ALL " +
                         "SELECT D.[ID], D.Parent_ID, " +
                         "D.name,([Level] + 1) as [Level] " +
                         "FROM Zakupki..Dic_Product_Codes AS D " +
                         "INNER JOIN C ON C.Parent_ID = D.ID) " +
                         "SELECT [level],d.ID, d.Parent_ID, case isNull(code, '') when '' then '' else '. ' end + Replace(d.name,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                         "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parent_id else 0 end as expanded, isnull((select top 1 1 from Zakupki..Dic_Product_Codes where parent_id=d.id),0) as hc " +
                         "FROM C  inner join Zakupki..Dic_Product_Codes d on c.parent_id=d.parent_id " +
                         "where d.parent_id>=0 order by [level] desc ";
                    break;
                case 24:
                    sql = string.Format(@";WITH C ([ID], parent_id, [name],[level]) AS 
                        (SELECT B.ID, B.parent_id, B.short_name,0 as [level] 
                        FROM naufor..okveds3 AS B WHERE B.[ID] in ({0}) 
                        UNION ALL 
                        SELECT D.[ID], D.parent_id, 
                        D.short_name,([Level] + 1) as [Level] 
                        FROM naufor..okveds3 AS D 
                        INNER JOIN C ON C.parent_id = D.ID) 
                        SELECT [level],d.ID, d.Parent_ID, short_name as name,case when d.id in ({0}) then 1 else 0 end as selected, 
                        case when c.id=d.id and not c.id in ({0}) then d.parent_id else 0 end as expanded, isnull((select top 1 1 from naufor..okveds3 where parent_id=d.id),0) as hc, d.name_json extra_info 
                        FROM C  inner join naufor..okveds3 d on c.parent_id=d.parent_id 
                        where d.parent_id>=0 order by [level] desc", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 26:
                    sql = string.Format(@";WITH C ([id],name,hc,parent_id,ordr) AS 
	                     (	
	                     Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 
	                     999 parent_id, [Ordr] from kz.dbo.ut_ClssEntState a left join 
	                     (Select kod from searchdb2.dbo.KodeSplitter(0,'{0}')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 
	                     'Все' name,1 hc,0 parent_id, 0 
	                     ) SELECT hc [level],id,parent_id,name,1 selected, 0 exapnded, hc from C WHERE id IN ({0})", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 27:
                    sql = string.Format(@"	 ;WITH C ([id],name,hc,parent_id,ordr) AS 
	                         (	
	                         Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 
	                         999 parent_id, 0 [Ordr] from kz.dbo.ut_ClssOwnership a left join 
	                         (Select kod from searchdb2.dbo.KodeSplitter(0,'{0}')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 
	                         'Все' name,1 hc,0 parent_id, 0 
	                         ) SELECT hc [level],id,parent_id,name,1 selected, 0 exapnded, hc from C WHERE id IN ({0})", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 28:
                    sql = string.Format(@"	 ;WITH C ([id],name,hc,parent_id,ordr) AS 
	                         (	
	                         Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 
	                         999 parent_id, 0 [Ordr] from kz.dbo.ut_ClssEconomyType a left join 
	                         (Select kod from searchdb2.dbo.KodeSplitter(0,'{0}')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 
	                         'Все' name,1 hc,0 parent_id, 0 
	                         ) SELECT hc [level],id,parent_id,name,1 selected, 0 exapnded, hc from C WHERE id IN ({0})", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 29:
                    sql = string.Format(@"	 ;WITH C ([id],name,hc,parent_id,ordr) AS 
	                         (	
	                         Select CAST(id as int) id, [Quantity] as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 
	                         999 parent_id, 0 [Ordr] from kz.dbo.ut_ScaleEntQuantity a left join 
	                         (Select kod from searchdb2.dbo.KodeSplitter(0,'{0}')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 
	                         'Все' name,1 hc,0 parent_id, 0 
	                         ) SELECT hc [level],id,parent_id,name,1 selected, 0 exapnded, hc from C WHERE id IN ({0})", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 30:
                    sql = string.Format(@"	 ;WITH C ([id],name,hc,parent_id,ordr) AS 
	                         (	
	                         Select CAST(id as int) id, Dscr as name,case  when kod is null  or len(kod)=0 then 0 else 1 end as hc, 
	                         999 parent_id, 0 [Ordr] from kz.dbo.ut_ClssEntSize a left join 
	                         (Select kod from searchdb2.dbo.KodeSplitter(0,'{0}')) b on b.kod=a.id where Id<>0 union all SELECT 999 id, 
	                         'Все' name,1 hc,0 parent_id, 0 
	                         ) SELECT hc [level],id,parent_id,name,1 selected, 0 exapnded, hc from C WHERE id IN ({0})", string.Join(",", node_ids.ParamVals().Keys));
                    break;
                case 31:
                    sql = @";WITH C (Id, ParentID, RuName,[level]) AS " +
                        "( " +
                        "SELECT B.Id, B.ParentID, " +
                        "B.RuName,0 as [level] " +
                        "FROM kz..Dic_Kato AS B WHERE B.Id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.Id, D.ParentID, " +
                        "D.RuName,([Level] + 1) as [Level] " +
                        "FROM kz..Dic_Kato AS D " +
                        "INNER JOIN C " +
                        "ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, Replace(d.RuName,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  then 1 else 0 end as selected, " +
                        "case  when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.ParentID else 0 end as expanded,isnull((select top 1 1 from kz..Dic_Kato where parentid=d.id),0) as hc " +
                        "FROM C inner join kz..Dic_Kato d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc";
                    break;
                case 32:
                    sql = @";WITH C ([ID], ParentID, [name],[level]) AS " +
                        "(SELECT B.ID, B.ParentID, B.Name,0 as [level] " +
                    	"FROM kz.dbo.Dic_OKED AS B WHERE B.[ID] in (" +string.Join(",", node_ids.ParamVals().Keys) + ") " +
	                    "UNION ALL " +
	                    "SELECT D.[ID], D.ParentID, " +
	                    "D.Name,([Level] + 1) as [Level] " +
	                    "FROM kz.dbo.Dic_OKED AS D " +
	                    "INNER JOIN C ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, Code+'. ' + d.Name as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parentid else 0 end as expanded, isnull((select top 1 1 from kz.dbo.Dic_OKED where parentid=d.id),0) as hc " +
                        "FROM C  inner join kz.dbo.Dic_OKED d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc ";
                    break;
		case 41:
                    sql = ";WITH C ([ID], Parent_ID, [level]) AS  " +
                        "(SELECT B.Id, B.parentId, 0 as [level]  " +
                        "FROM UA3..Dic_OPF AS B WHERE B.Id in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  " +
                        "UNION ALL  " +
                        "SELECT D.Id, D.parentId,  " +
                        "([Level] + 1) as [Level]  " +
                        "FROM UA3..Dic_OPF AS D  " +
                        "INNER JOIN C ON c.parentId = d.ID)  " +
                        "SELECT distinct [level],d.Id, d.parentId, cast(d.Id as varchar(5))+'. ' + Replace(d.DscrRU,'\"','&quot;') as name,case when d.Id  in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected,  " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parentId else 0 end as expanded, isnull((select top 1 1 from UA3..Dic_OPF where parentId=d.Id),0) as hc  " +
                        "FROM (Select distinct * from C) C   inner join UA3..Dic_OPF d on c.parentId=d.parentId  " +
                        "where d.parentId>=0 order by [level] desc";
                    break;
                case 42:
                    sql = ";WITH C (Id, ParentID, Name,[level]) AS " +
                        "( " +
                        "SELECT B.Id, B.ParentID, " +
                        "B.Name,0 as [level] " +
                        "FROM UA3..Dic_Koatuu AS B WHERE B.Id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.Id, D.ParentID, " +
                        "D.Name,([Level] + 1) as [Level] " +
                        "FROM UA3..Dic_Koatuu AS D " +
                        "INNER JOIN C " +
                        "ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, Replace(d.Name,'\"','&quot;') as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ")  then 1 else 0 end as selected, " +
                        "case  when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.ParentID else 0 end as expanded,isnull((select top 1 1 from UA3..Dic_Koatuu where parentid=d.id),0) as hc " +
                        "FROM C inner join UA3..Dic_Koatuu d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc";
                    break;
                case 43:
                    break;
                case 44:
                case 45:
                    sql = ";WITH C ([ID], ParentID, [name],[level]) AS " +
                        "(SELECT B.ID, B.ParentID, B.RUname,0 as [level] " +
                        "FROM UA3..Dic_Kved AS B WHERE B.[ID] in (" + string.Join(",", node_ids.ParamVals().Keys) + ") " +
                        "UNION ALL " +
                        "SELECT D.[ID], D.ParentID, " +
                        "D.RUname,([Level] + 1) as [Level] " +
                        "FROM UA3..Dic_Kved AS D " +
                        "INNER JOIN C ON C.ParentID = D.ID) " +
                        "SELECT [level],d.ID, d.ParentID, Code+'. ' + d.RUName collate database_default as name,case when d.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then 1 else 0 end as selected, " +
                        "case when c.id=d.id and not c.id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") then d.parentid else 0 end as expanded, isnull((select top 1 1 from UA3..Dic_Kved where parentid=d.id),0) as hc " +
                        "FROM C  inner join UA3..Dic_Kved d on c.parentid=d.parentid " +
                        "where d.parentid>=0 order by [level] desc ";
                    break;

            }
            if (sql != "")
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    foreach (var p in node_ids.ParamVals())
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    con.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int? hc = (int?)reader.ReadNullIfDbNull(6);

                            var cl = new ExpandedCodeLine
                            {
                                id = (int)reader[1],
                                title = (string)reader.ReadNullIfDbNull(3),
                                isFolder = hc.HasValue && hc == 1,
                                expanded = (int)reader[5],
                                parent = (int?)reader.ReadNullIfDbNull(2) ?? 0,
                                selected = (int)reader[4],
                                src=src
                            };

                            if (src == 24 || src == 1)
                            {
                                cl.extra_info = (string)reader.ReadNullIfDbNull("extra_info");
                            }

                            ret.Add(cl);
                        }
                    }
                }
            }
            return ret;
        }


        public static async Task<List<ShortCodeLine>> GetShortCodeLinesAsync(int src, string st)
        {
            List<ShortCodeLine> ret = new List<ShortCodeLine>();

            if (!string.IsNullOrWhiteSpace(st))
            {
                st = Regex.Replace(st, "';", " ").Trim();
                if (!string.IsNullOrWhiteSpace(st))
                {
                    string sql = "";
                    switch (src)
                    {
                        case 0:
                            //sql = "Select id,okato,Replace(subject,'\"','&quot;')  from okato where okato like @st1 or subject like @st2 order by subject";
                            sql = "Select top 1000 id,okato,Replace(fullpath,'\"','&quot;')  from naufor..okato where okato like @st1 or subject like @st2 order by subject";
                            break;
                        //case 1:
                        //    sql = "Select id, kod,Replace(name,'\"','&quot;') as name from searchdb2..okveds where kod like @st1 or name like @st2 order by kod, name";
                        //    break;
                        case 2:
                            sql = "Select top 1000 id, kod as code,Replace(name,'\"','&quot;') as name from searchdb2..okonh where kod like @st1 or name like @st2 order by kod, name";
                            break;
                        case 5:
                            //sql = "Select active_id as id, active_id as code,Replace(name,'\"','&quot;') as name from naufor..regions where active_id like @st1 or name like @st2 order by active_id, name";
                            sql = "Select top 1000 id, cast(active_id as varchar(1000)) as code,Replace(name,'\"','&quot;') as name from naufor..regions where active_id like @st1 or name like @st2 order by active_id, name";
                            break;
                        case 9:
                            sql = "Select top 1000 id, cast(id as varchar(1000)) as code, name,(select top 1 1 from skrin_content_output..fas_regions_tree where parent_id=a.id) as hc,id as code from skrin_content_output..fas_regions_tree a where name like @st1 or name like @st2 order by name";
                            break;
                        case 11:
                            sql = "Select top 1000 id, code,Replace(name,'\"','&quot;') as name from Zakupki..Dic_Okdp where code like @st1 or name like @st2 order by code, name";
                            break;
                        case 17:
                            sql = "Select top 1000 id, code, Replace(name,'\"','&quot;') as name from Zakupki..Dic_Product_Codes where code like @st1 or name like @st2 order by code, name";
                            break;
                    }
                    if (sql != "")
                    {
                        using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.Parameters.Add("@st1", SqlDbType.VarChar).Value = st + "%";
                            cmd.Parameters.Add("@st2", SqlDbType.VarChar).Value = "%" + st + "%";
                            con.Open();
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {

                                    ret.Add(new ShortCodeLine
                                    {
                                        id = (int)reader[0],
                                        name = reader.ReadEmptyIfDbNull(2),
                                        kod = reader.ReadEmptyIfDbNull(1),
                                        type = ShortCodeLineType.Text
                                    });
                                }
                            }
                        }
                    }                    
                    //Для нового ОКВЕД
                    if(src==24 || src==1)
                    {
                        sql = " SELECT top 1000 id, name_json from  naufor..okveds3 where name_json like @st ";
                        using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.Parameters.Add("@st", SqlDbType.VarChar).Value = "%" + st + "%";
                            con.Open();
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    ret.Add(new ShortCodeLine
                                    {
                                        id = (int)reader[0],
                                        name = (string)reader[1],
                                        type = ShortCodeLineType.Json
                                    });
                                }
                            }
                        }
                    }                    
                }
            }

            return ret;

        }


        public static async Task<string> GetResultStringAsync(int src, string node_ids)
        {
            if (!string.IsNullOrWhiteSpace(node_ids))
            {
                string sql = "";
                switch (src)
                {
                    case 0:
                        sql = "SELECT STUFF((SELECT ', '+subject FROM  okato where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 1:
                        //sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..okveds where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        sql = "SELECT STUFF((SELECT ', '+short_name FROM  naufor..okveds3 where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 2:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..okonh where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 3:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..okfs where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 4:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..okopf where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 5:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..regions where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        //sql = "SELECT STUFF((SELECT ', '+name FROM  naufor..regions where active_id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 6:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  naufor..issuer_statuses where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 7:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  searchdb2..rfi_states where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 8:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..bargains_types where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 9:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..fas_regions_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 10:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..columns2show where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 12:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  naufor..Vestnik_Types where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 13:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..vFedresurs_messages_types2 where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parent_id,id FOR XML PATH('')),1,1,'')";
                        break;
                    case 14:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..disclosure_mt where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parent_id,id FOR XML PATH('')),1,1,'')";
                        break;
                    case 18:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..event_types_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parent_id,id FOR XML PATH('')),1,1,'')";
                        break;
                    case 19:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..bankrot_types_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parent_id,id FOR XML PATH('')),1,1,'')";
                        break;
                    case 20:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..ac_types_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") order by parent_id desc, name FOR XML PATH('')),1,1,'')";
                        break;
                    case 21:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..disput_types_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") order by parent_id desc, id FOR XML PATH('')),1,1,'')";
                        break;
                    case 22:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..side_types_tree where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") order by parent_id desc, id FOR XML PATH('')),1,1,'')";
                        break;
                    case 23:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..vFedresurs_messages_types2 where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") order by parent_id desc, id FOR XML PATH('')),1,1,'')";
                        break;
                    case 25:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  skrin_content_output..analytic_menu where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parent_id,id FOR XML PATH('')),1,1,'')";
                        break;

                    case 41: //Украина
                        sql = "SELECT STUFF((SELECT ', '+DscrRU FROM UA3..Dic_OPF  where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 42:
                        sql = "SELECT STUFF((SELECT ', '+Name FROM  UA3..Dic_Koatuu where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 43:
                        sql = "SELECT STUFF((SELECT ', '+name FROM  UA3..Dic_Area where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 44:
                    case 45:
                        sql = "SELECT STUFF((SELECT ', '+RUName FROM  UA3..Dic_Kved where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;
                    case 24:
                        sql = "SELECT STUFF((SELECT ', '+short_name FROM  naufor..okveds3 where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')";
                        break;      
                    case 26:
                        if (node_ids == "999") { sql = "select 'Все' name"; } else { sql = "SELECT STUFF((SELECT ', '+Dscr FROM kz..ut_ClssEntState where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')"; }
                        break;
                    case 27:
                        if (node_ids == "999") { sql = "select 'Все' name"; } else { sql = "SELECT STUFF((SELECT ', '+Dscr FROM kz..ut_ClssOwnership where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')"; }
                        break;
                    case 28:
                        if (node_ids == "999") { sql = "select 'Все' name"; } else { sql = "SELECT STUFF((SELECT ', '+Dscr FROM kz..ut_ClssEconomyType where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')"; }
                        break;
                    case 29:
                        if (node_ids == "999") { sql = "select 'Все' name"; } else { sql = "SELECT STUFF((SELECT ', '+Quantity FROM kz..ut_ScaleEntQuantity where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')"; }
                        break;
                    case 30:
                        if (node_ids == "999") { sql = "select 'Все' name"; } else { sql = "SELECT STUFF((SELECT ', '+Dscr FROM kz..ut_ClssEntSize where id in (" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY id FOR XML PATH('')),1,1,'')"; }
                        break;
                    case 31:
                        sql = "SELECT STUFF((SELECT ', '+Runame FROM  kz..Dic_Kato where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parentid,id FOR XML PATH('')),1,1,'')";
                        break;
                    case 32:
                        sql = "SELECT STUFF((SELECT ', '+Name FROM  kz..Dic_OKED where id in(" + string.Join(",", node_ids.ParamVals().Keys) + ") ORDER BY parentid,id FOR XML PATH('')),1,1,'')";
                        break;
                }
                if (sql != "")
                {
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, con);
                        foreach (var p in node_ids.ParamVals())
                        {
                            cmd.Parameters.AddWithValue(p.Key, p.Value);
                        }
                        con.Open();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (await reader.ReadAsync())
                            {
                                return (string)reader.ReadNullIfDbNull(0);
                            }
                        }
                    }
                }
            }
            return null;
        }


    }
}