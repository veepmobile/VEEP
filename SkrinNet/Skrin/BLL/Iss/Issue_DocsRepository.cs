using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;
using SKRIN;

namespace Skrin.BLL.Iss
{
    public class Issue_DocsRepository
    {
        private static readonly string _constring = Configs.ConnectionString;
        private readonly string _ticker;
        public Issue_DocsRepository(string ticker)
        {
            _ticker = ticker;
        }


        public async Task<Issue_DocsModel> GetIssue_DocsAsync()
        {
            Issue_DocsModel im = new Issue_DocsModel() { ticker = _ticker };

            using (SQL sql = new SQL(_constring))
            {
                Query q = await sql.OpenQueryAsync(@"
                        SELECT 
                            A.issuer_id
                            ,A.id as issue_id
                            ,A.reg_no as issue_no
			                ,isnull(convert(VARCHAR(10), A.reg_date, 104), '-') AS issue_date
                            ,s.name as sec_name
			                ,tp.Name AS doc_name
			                ,dp.file_name
			                ,docs.id AS doc_id
			                ,docs.pages
			                ,id.reg_date
                            ,-2 as export_type
                        FROM naufor..Issues A
                            INNER JOIN naufor..Issue_Docs id on A.id=id.issue_id
                            INNER JOIN naufor..Issue_Doc_Types tp on id.doc_type_id=tp.id
                            INNER JOIN naufor..Sec_Types S on A.sec_type_id=S.id
			                INNER JOIN naufor..docs_new docs ON docs.id = id.doc_id
			                INNER JOIN naufor..doc_pages_new dp ON dp.doc_id = docs.id
                            INNER JOIN searchdb2..union_search us on A.issuer_id=us.issuer_id and us.type_id=0
                        WHERE us.ticker = @ticker
                        ORDER BY A.reg_date DESC, A.id ASC, tp.orderno
                    ", new SQLParamVarchar("ticker", _ticker)
                );
                bool f = true;
                Issue_DocItem idi = null;
                ReportFile rf = null;
                while (await q.ReadAsync())
                {
                    bool c = false;
                    if (f)
                    {
                        im.issuer_id = q.GetFieldAsString("issuer_id");
                    }
                    if (f || (idi.issue_id != q.GetFieldAsString("issue_id")))
                    {
                        idi = new Issue_DocItem
                        {
                            issue_id = q.GetFieldAsString("issue_id"),
                            issue_no = q.GetFieldAsString("issue_no"),
                            issue_date = q.GetFieldAsString("issue_date"),
                            sec_name = q.GetFieldAsString("sec_name"),
                        };
                        c = true;
                        im.Items.Add(idi);
                    }
                    if (c || (rf.doc_id != q.GetFieldAsString("doc_id")))
                    {
                        rf = new ReportFile
                        {
                            doc_id = q.GetFieldAsString("doc_id"),
                            doc_name = q.GetFieldAsString("doc_name"),
                            file_name = q.GetFieldAsString("file_name"),
                            pages = (short)q.GetFieldAsShort("pages"),
                            export_type = (int)q.GetFieldAsInt("export_type")
                        };
                        rf.file_size = Utilites.GetFileSize(string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, im.issuer_id, rf.doc_id, rf.file_name));
                        c = true;
                        idi.Items.Add(rf);
                    }
                    f = false;
                }
            }
            return im;
        }
    }
}