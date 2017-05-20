using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.StopLight
{
    public class TableRowBlock
    {
        public int Id { get; set; }
        public string BlockHeader { get; set; }
        public List<TableRow> Rows { get; set; }

        public TableRowBlock()
        {
            Rows = new List<TableRow>();
        }

        public TableRowBlock GetNotEmptyTableRowBlock()
        {
            List<TableRow> rows = Rows.Where(p => string.IsNullOrEmpty(p.RowValue) == false).ToList();
            if (rows.Count > 0)
                return new TableRowBlock { Id = this.Id, BlockHeader = this.BlockHeader, Rows = rows };
            return null;
        }
    }

    public class TableRow
    {
        public string RowHeader { get; set; }
        public string RowValue { get; set; }
    }
}