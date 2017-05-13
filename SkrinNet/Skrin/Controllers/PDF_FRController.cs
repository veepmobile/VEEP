using serch_contra_bll.StopLightFreeEgrul;
using serch_contra_bll.ActionStoplight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using System.Data;
using System.Data.SqlClient;
using FastReport;
using FastReport.Export.Pdf;
using System.IO;
using Skrin.BLL;
using Skrin.Models;
using Skrin.Models.Iss.StopLight;
using Skrin.BLL.Authorization;

namespace Skrin.Controllers
{
    public class PDF_FRController : BaseController
    {   private enum Key { CanShow, CanShowStopLight };
        private static Dictionary<string, AccessType> roles = new Dictionary<string, AccessType>();

        static PDF_FRController()
        {
            roles.Add(Key.CanShowStopLight.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.KaPoln);
            roles.Add(Key.CanShow.ToString(), AccessType.Pred | AccessType.Bloom | AccessType.Deal | AccessType.Emitent | AccessType.KaPlus | AccessType.Ka | AccessType.KaPoln);
        }
        // GET: PDF_FR
        public ActionResult Index(string iss, int us_id, string choice, int source, string url)
        {
            bool open_company = open_companies.Contains(iss.ToLower());
            UserSession us = HttpContext.GetUserSession();
            bool CanShowStopLight = open_company || us.HasRole(roles, Key.CanShowStopLight.ToString());
                 
            bool open_profile = us.HasRole(roles, Key.CanShow.ToString()) || open_company;

            SqlCommand cmd = new SqlCommand("skrin_net..getPathURep");
                    cmd.Parameters.Add("@iss", SqlDbType.VarChar, 32).Value = iss;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = us_id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = new SqlConnection(Configs.ConnectionString);
                    cmd.Connection.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    String File_id = r.GetValue(0).ToString();
                    String issuer_id = r.GetValue(1).ToString();
                    String ogrn = r.GetValue(2).ToString();
                    String inn = r.GetValue(3).ToString();
                    String canASL = r.GetValue(4).ToString();

                    Report report = new Report();

                    PDFExport export = new PDFExport();
                    report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Templates//report3.frx"));
                    StopLightCreator creator = new StopLightCreator(iss);
                    StopLightData data = new StopLightData(ogrn, Configs.ConnectionString);
                    ShapeObject ShapeSL = report.FindObject("ShapeSL") as ShapeObject;
                    TextObject TextSL = report.FindObject("TextSL") as TextObject;
                    if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.NoRating)
                        ShapeSL.Visible = false;
                    int factors_count = 0;
                    if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Red)
                    {
                        factors_count = data.Factors.Where(p => p.Value.IsUnconditional).Count(p => p.Value.IsStoped);
                        ShapeSL.Border.Color = System.Drawing.Color.Red;
                        TextSL.TextColor = System.Drawing.Color.Red;
                        
                    }
                    if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Yellow)
                    {
                        factors_count = data.Factors.Where(p => p.Value.IsUnconditional == false).Count(p => p.Value.IsStoped);
                        ShapeSL.Border.Color = System.Drawing.Color.Gold;
                        TextSL.TextColor = System.Drawing.Color.Gold;
                    }
                    TextSL.Text = factors_count>0? factors_count.ToString() : "";
                    StopLight sl = creator.GetStopLight();
                    if (sl != null)
                    {
                        report.RegisterData(sl.headerList, "StopLightHeader");
                        if (CanShowStopLight)
                        {
                            if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Red)
                                report.RegisterData(data.Factors.Values.Where((p => p.IsUnconditional && p.IsStoped)).ToList(), "RedStops");
                            if (data.Rating == serch_contra_bll.StopLightFreeEgrul.ColorRate.Yellow)
                                report.RegisterData(data.Factors.Values.Where(p => !p.IsUnconditional && p.IsStoped).ToList(), "YellowStops");
                        }
                        else
                        {
                            DataBand chb = report.FindObject("RedStopsDATA") as DataBand;
                            chb.Visible = false;
                            DataBand ch6 = report.FindObject("YellowStopsDATA") as DataBand;
                            ch6.Visible = false;
                        }
                    }

                    var stoplight = new ActionStopLightCreator(ogrn, inn, Configs.ConnectionString).Create();
                    var asl = ASL.Create(stoplight);

                    if (asl!=null)
                    {
                                              
                        var ASLList = new List<ASLData>
                        {
                            new ASLData { id = 1, val = asl.total_count, color = ( (asl.total_count>50)? "ForestGreen":(asl.total_count>0)?"Gold":"Red")}, 
                            new ASLData { id = 1, val = 100-asl.total_count, color = asl.total_count==0? "Red" : "LightGray"} 
                        };
                            report.RegisterData(ASLList, "ASLTablet");
                            ChildBand ch10 = report.FindObject("Child10") as ChildBand;
                            if (CanShowStopLight)
                            {
                                report.RegisterData(asl.items, "ASLInfo");
                                TextObject tASL = report.FindObject("TextASL") as TextObject;
                                tASL.TextColor = ((asl.total_count > 50) ? System.Drawing.Color.ForestGreen : (asl.total_count > 0) ? System.Drawing.Color.Gold : System.Drawing.Color.Red);
                                tASL.Text = asl.total_count.ToString();
                                ch10.Visible = true;
                            }
                            else
                            {
                               
                                ch10.Visible = false;
                            }
                    }
                    else
                    {
                        DataBand DASL = report.FindObject("DataASL") as DataBand;
                        DASL.Visible = false;
                    }
                    
                    
            
            
            
            
            
            
            
                    report.SetParameterValue("@iss", iss);
                    report.SetParameterValue("@choice", choice);
                    report.SetParameterValue("@file_id", File_id);
                    report.SetParameterValue("@user_id", us_id);
                   
                    report.Report.Prepare();

                    Stream stream = new MemoryStream();
                    // export the report
                    report.Report.Export(export, stream);
                    stream.Position = 0;

                    // free resources used by report
                    report.Dispose();


                    //string destPath = System.Web.HttpContext.Current.Server.MapPath(Configs.Cloud + us_id.ToString() + "//" + issuer_id + "//");
                    string destPath = Configs.Cloud + us_id.ToString() + "\\" + issuer_id + "\\";
                    //string destPath = "E:\\" + us_id.ToString() + "\\" + issuer_id + "\\";
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    string fname = iss + ".pdf";
                    using (var fileStream = new FileStream(destPath + File_id + ".pdf", FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Close();
                    }

                    stream.Position = 0;
                    //return File(stream, "application/pdf", fname);
                    return Content(File_id);
                    
                    

                }
               

        }
    }
