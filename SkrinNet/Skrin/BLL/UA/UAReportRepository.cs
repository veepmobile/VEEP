using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.Models.UA;
using System.Threading.Tasks;
using System.IO;
using SKRIN;

namespace Skrin.BLL.UA
{
    public enum UAReportType { AnnualReports = 0, QuartReports = 1};
    public class UAReportRepository
    {
        private static readonly string _constring = Configs.ConnectionString;
        private readonly string _edrpou;
        private readonly UAReportType _report_type;

        public UAReportRepository()
        {
        }

        public UAReportRepository(string edrpou, UAReportType report_type)
        {
            _edrpou = edrpou;
            _report_type = report_type;
        }

        public async Task<UAReportModel> GetReportListAsync()
        {
            UAReportModel rm = new UAReportModel() { edrpou = _edrpou};

            using (SQL sql = new SQL(_constring))
            {
                Query q = await sql.OpenQueryAsync("exec ua3..issuer_uareports_bytype @edrpou, @rep_type", new SQLParamVarchar("edrpou", _edrpou), new SQLParamInt("rep_type", (int)_report_type));
                bool f = true;
                UAReportGroup rg = null;
                UAReportItem ri = null;
                UAReportFile rf = null;
                while (await q.ReadAsync())
                {
                    bool c = false;
                    if (f)
                    {
                        rm.edrpou = q.GetFieldAsString("edrpou");
                        rm.report_view = (UAReportView)q.GetFieldAsInt("report_view");
                    }
                    if (f || (rg.group_id != q.GetFieldAsInt("group_id")))
                    {
                        rg = new UAReportGroup
                        {
                            group_id = q.GetFieldAsInt("group_id"),
                            group_name = q.GetFieldAsString("group_name")
                        };
                        c = true;
                        rm.Items.Add(rg);
                    }
                    if (c || (ri.item_id != q.GetFieldAsInt("item_id")))
                    {
                        ri = new UAReportItem
                        {
                            item_id = q.GetFieldAsInt("item_id"),
                            item_name = q.GetFieldAsString("item_name")
                        };
                        c = true;
                        rg.Items.Add(ri);
                    }
                    if (c || (rf.doc_id != q.GetFieldAsString("doc_id")))
                    {
                        rf = new UAReportFile
                        {
                            doc_id = q.GetFieldAsString("doc_id"),
                            doc_name = q.GetFieldAsString("doc_name"),
                            file_name = q.GetFieldAsString("file_name"),
                            pages = (short)q.GetFieldAsShort("pages"),
                            export_type = (int)q.GetFieldAsInt("export_type")
                        };

                        string file_path = string.Format(@"{0}{1}\{2}\{3}", Configs.UADocPath, rm.edrpou, rf.doc_id, rf.file_name);
                        rf.file_size = (File.Exists(file_path) ? Utilites.GetFileSize(file_path) : "0b");
                        c = true;
                        ri.Items.Add(rf);
                    }
                    f = false;
                }
            }
            return rm;
        }

        public async Task<UADocument> getDocument(string doc_id)
        {
            UADocument _doc = null;
            string sSql = "SELECT [id], [edrpou], [year], [quart], [is_year], [source_id], [file_name], [file_id] FROM ua3..html_docs where id=@doc_id";
            using (SQL sql = new SQL(_constring))
            {
                Query q = await sql.OpenQueryAsync(sSql, new SQLParamVarchar("doc_id", doc_id));
                if (await q.ReadAsync())
                {
                    _doc = new UADocument() { id= q.GetFieldAsString("id")
                                            , edrpou = q.GetFieldAsString("edrpou")
                                            , year = (int)q.GetFieldAsInt("year")
                                            , quart = (int)q.GetFieldAsInt("quart")
                                            , is_year= (bool)q.GetFieldAsBool("is_year")
                                            , file_name= q.GetFieldAsString("file_name")
                                            , file_id= (int)q.GetFieldAsInt("file_id")
                    };
                }
            }
            return _doc;
        }
    }
}
