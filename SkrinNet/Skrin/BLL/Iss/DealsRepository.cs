using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Skrin.Models;

namespace Skrin.BLL.Iss
{
    public class DealsRepository
    {
        private  string _issuer_id;
        private readonly string _ticker;
        private  string _issuer_name;
        private static readonly string _constring = Configs.ConnectionString;

        public DealsRepository(string ticker)
        {
            _ticker = ticker;
            _Init();
        }

        public async Task<DealInfo> GetDealInfoAsync(DealInfoTypes type,string period=null)
        {
            if (_issuer_id == null)
                return null;

            int doc_type_id,doc_id;
            string header;
            switch (type)
            {
                case DealInfoTypes.Main:
                    doc_type_id = 4;
                    header = "Основная хозяйственная деятельность";
                    doc_id = -17;
                    break;
                case DealInfoTypes.Plans:
                    doc_type_id = 48;
                    header = "Планы будущей деятельности";
                    doc_id = -18;
                    break;
                default:
                    throw new ArgumentException();
            }

            List<Period> periods = await _GetPeriodsAsync(doc_type_id);

            if (periods.Count == 0)
                return null;

            if(period=="0" || period==null)
            {
                period = periods.Select(p => p.ValueDate).First();
            }

            

            FileInfo fi = await _GetFilePathAsync(doc_type_id, period);

            string file_path = fi.ToString();
            
            string extention=file_path.Substring(file_path.Length-3,3);

            DealInfo di = new DealInfo(type,_ticker)
            {
                Header = header,
                Periods = periods,
                SelectedPeriod = period
            };

            switch (extention)
            {
                case "txt":
                    di.Body = new HtmlString(await _GetBodyFromTextFile(file_path,true));
                    break;
                case "xml":
                    if(type==DealInfoTypes.Plans)
                    {
                        string xml_source = await _GetBodyFromTextFile(file_path,false);
                        HTMLCreator creator = new HTMLCreator("plans");
                        di.Body = new HtmlString(creator.GetHtml(xml_source));
                    }
                    else
                    {
                        di.Body = new HtmlString(_GetFileLink(fi, header, extention,doc_id));
                    }
                    break;
                default:
                    di.Body = new HtmlString(_GetFileLink(fi, header, extention,doc_id));
                    break;
            }

            return di;
        }

        private void _Init()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql="Select issuer_id,isnull(short_name,name)name from searchdb2..union_search where ticker=@ticker";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ticker", SqlDbType.VarChar, 32).Value = _ticker;
                con.Open();
                using (SqlDataReader reader=cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        _issuer_id = (string)reader["issuer_id"];
                        _issuer_name = (string)reader.ReadNullIfDbNull("name");
                    }
                }
            }
        }

        private async Task<List<Period>> _GetPeriodsAsync(int doc_type_id)
        {
            List<Period> ret=new List<Period>();
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"Select convert(varchar(8),coalesce(reg_date,update_date,insert_date),112) as od, 
                    convert(varchar(10),coalesce(reg_date,update_date,insert_date),104) as sd from issuer_docs where issuer_id=@issuer_id and doc_type_id=@doc_type_id order by reg_date desc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _issuer_id;
                cmd.Parameters.Add("@doc_type_id", SqlDbType.Int).Value = doc_type_id;
                con.Open();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        ret.Add(new Period
                        {
                            ValueDate = (string)reader["od"],
                            ShowDate = (string)reader["sd"]
                        });
                    }
                }
            }
            return ret;
        }

        private async Task<FileInfo> _GetFilePathAsync(int doc_type_id, string period)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"select a.doc_id, file_name from Issuer_Docs a inner join Doc_Pages_New b on a.doc_id=b.doc_id 
                                where issuer_id=@issuer_id and doc_type_id=@doc_type_id and convert(varchar(10),coalesce(reg_date,update_date,insert_date),112)=@period";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = _issuer_id;
                cmd.Parameters.Add("@doc_type_id", SqlDbType.Int).Value = doc_type_id;
                cmd.Parameters.Add("@period", SqlDbType.VarChar).Value = period;
                con.Open();
                using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    FileInfo fi = null;
                    if(await reader.ReadAsync())
                    {
                        fi = new FileInfo
                        {
                            doc_id = (string)reader["doc_id"],
                            file_name = (string)reader["file_name"],
                            issuer_id = _issuer_id
                        };
                    }
                    return fi;
                }
            }
        }


        private async Task<string> _GetBodyFromTextFile(string filepath,bool formating)
        {
            using(StreamReader sr=new StreamReader(filepath,Encoding.GetEncoding("Windows-1251")))
            {
                string text = await sr.ReadToEndAsync();
                return formating ? string.Format("<p>{0}</p>", Regex.Replace(text, @"\n", "<br/>")) : text;
            }
        }

        private string _GetFileLink(FileInfo fi,string fake_file_name,string extention, int doc_id)
        {
            return string.Format("<p>Скачать файл: <a href='/Documents/Index?iss={3}&id={4}&fn={6}&doc_id={7}'><span class=\"{5} d_icon\"></span>{0}.{1}({2})</a></p>",
                fake_file_name, extention, Utilites.GetFileSize(fi.ToString()), _ticker, fi.doc_id, ContentTypeCollection.GetStyle(extention),fi.file_name.UrlEncode(),doc_id);
        }


        

        

        private class FileInfo
        {
            public string doc_id { get; set; }
            public string file_name { get; set; }
            public string issuer_id { get; set; }

            public override string ToString()
            {
                return string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, issuer_id, doc_id, file_name);
            }
        }

    }
}