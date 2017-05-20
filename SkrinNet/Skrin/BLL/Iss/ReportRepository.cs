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
    public enum ReportType { AnnualReports = 0, QuartReports = 1, QuartReportsFSFR = 2, BuhReports = 3, MSFO_Reports = 4 };
    public class ReportRepository
    {
        private static readonly string _constring = Configs.ConnectionString;
        private readonly string _ticker;
        private readonly ReportType _report_type;

        public ReportRepository(string ticker, ReportType report_type)
        {
            _ticker = ticker;
            _report_type = report_type;
        }


        public async Task<ReportModel> GetReportListAsync()
        {
            ReportModel rm = new ReportModel() { ticker = _ticker};

            using (SQL sql = new SQL(_constring))
            {
                Query q = await sql.OpenQueryAsync("exec skrin_content_output..issuer_reports_bytype @ticker, @rep_type", new SQLParamVarchar("ticker", _ticker), new SQLParamInt("rep_type", (int)_report_type));
                bool f = true;
                ReportGroup rg = null;
                ReportItem ri = null;
                ReportFile rf = null;
                while (await q.ReadAsync())
                {
                    bool c = false;
                    if (f)
                    {
                        rm.issuer_id = q.GetFieldAsString("issuer_id");
                        rm.report_view = (ReportView)q.GetFieldAsInt("report_view");
                    }
                    if (f || (rg.group_id != q.GetFieldAsInt("group_id")))
                    {
                        rg = new ReportGroup
                        {
                            group_id = q.GetFieldAsInt("group_id"),
                            group_name = q.GetFieldAsString("group_name")
                        };
                        c = true;
                        rm.Items.Add(rg);
                    }
                    if (c || (ri.item_id != q.GetFieldAsInt("item_id")))
                    {
                        ri = new ReportItem
                        {
                            item_id = q.GetFieldAsInt("item_id"),
                            item_name = q.GetFieldAsString("item_name")
                        };
                        c = true;
                        rg.Items.Add(ri);
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
                        rf.file_size = Utilites.GetFileSize(string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, rm.issuer_id, rf.doc_id, rf.file_name));
                        c = true;
                        ri.Items.Add(rf);
                    }
                    f = false;
                }
            }
            return rm;
        }

    }
}
