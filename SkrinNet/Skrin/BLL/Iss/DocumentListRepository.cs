using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;

namespace Skrin.BLL.Iss
{
    public class DocumentListRepository
    {
        private static readonly string _constring = Configs.ConnectionString;
        private readonly string _ticker;
        private readonly int _doc_group_id;

        public DocumentListRepository(string ticker,int doc_group_id)
        {
            _ticker = ticker;
            _doc_group_id = doc_group_id;
        }


        public async Task<DocumentList> GetDocumentListAsync()
        {
            DocumentList dl = new DocumentList() { ticker=_ticker};

            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_content_output..issuer_docs_bytype", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = _ticker;
                cmd.Parameters.Add("@doc_group", SqlDbType.Int).Value = _doc_group_id;
                con.Open();
                
                int? cur_group_id = null;
                int? cur_type_id = null;
                DocumentGroup dg=null;
                DocumentType dt = null;
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        if (dl.issuer_id == null)
                        {
                            dl.issuer_id = (string)reader["issuer_id"];
                        }
                        int group_id = (int)reader["group_id"];
                        if ((cur_group_id==null) || (cur_group_id != group_id))
                        {
                            cur_group_id = group_id;
                            dg = new DocumentGroup
                            {
                                group_id = group_id,
                                group_name = (string)reader["group_name"]                                
                            };
                            dl.Items.Add(dg);                            
                        }
                        int type_id = (int)reader["type_id"];
                        if ((cur_type_id==null) || (cur_type_id != type_id))
                        {
                            cur_type_id = type_id;
                            dt = new DocumentType
                            {
                                type_id = type_id,
                                type_name = (string)reader["type_name"]
                            };
                            dg.Items.Add(dt);
                        }
                        DocumentItem di = new DocumentItem
                        {
                            doc_id = (string)reader["doc_id"],
                            doc_name = reader.ReadEmptyIfDbNull("doc_name"),
                            dt = (string)reader.ReadNullIfDbNull("dt"),
                            file_name = (string)reader["file_name"],
                            pages = (int)(Int16)reader["pages"],
                            reg_date = (DateTime?)reader.ReadNullIfDbNull("reg_date")
                        };

                        dt.Items.Add(di);
                    }
                }

                
            }

            foreach(var group in dl.Items)
            {
                foreach(var type in group.Items)
                {
                    foreach(var item in type.Items)
                    {
                        if(item.pages==0)
                        {
                            item.file_size = Utilites.GetFileSize(string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, dl.issuer_id, item.doc_id, item.file_name));
                        }
                    }
                }
            }

            return dl;
        }

    }
}