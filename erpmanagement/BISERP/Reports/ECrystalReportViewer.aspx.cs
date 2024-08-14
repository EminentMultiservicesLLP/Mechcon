//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using BISERPCommon;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;

//namespace BISERP.Reports
//{

//    public partial class ECrystalReportViewer : System.Web.UI.Page
//    {

//        static readonly BISERPCommon.ILogger Logger = BISERPCommon.Logger.Register(typeof(ECrystalReportViewer));
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                if (Request.QueryString["ReportName"] != null)
//                {

//                    if (Request.QueryString["ReportName"].ToString() == "PurchaseOrder")
//                    {
//                        int poId = Convert.ToInt32(Request.QueryString["id"]);
//                        ExportPo(poId);
//                    }
//                }
//            }
//        }

//        private void ExportPo(int poid)
//        {
//            try
//            {
//                Logger.LogInfo("Export PO started ");
//                SqlConnection con =
//                    new SqlConnection(ConfigurationManager.ConnectionStrings["BISConnection"].ConnectionString);
//                SqlCommand cmd = new SqlCommand("sp_PurchaseOrderRPT", con);
//                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = poid;
//                cmd.CommandType = CommandType.StoredProcedure;

//                var conDataSource = con.DataSource;
//                var conDataBase = con.Database;

//                Logger.LogInfo("Export PO conDataSource: " + conDataSource + " conDataBase:" + conDataBase);
//                SqlDataAdapter sda = new SqlDataAdapter(cmd);
//                DataTable dt = new DataTable();
//                sda.Fill(dt);

//                ReportDocument objdoc = new ReportDocument();
//                CrystalReportViewer1.DisplayGroupTree = false;
//                objdoc.Load(Path.Combine(Server.MapPath("~/Reports/Reports"), "PurchaseOrder.rpt")); //(Request.MapPath(@"REPORTS/rptPrlSalaryRegisterEmpFrmt1512Report.rpt"));
//                objdoc.SetParameterValue("@ID", poid);//objdoc.SetParameterValue("@PstrCXmlDataDtl", "<payslip><brlocid>22</brlocid><attyear>2010</attyear><attmonth>6</attmonth><reportid>3</reportid><hdrfrmtid>6</hdrfrmtid><dtlfrmtid>5</dtlfrmtid></payslip>");
//                objdoc.SetDataSource(dt);

//                SetCrystalLogin(conDataSource, conDataBase, objdoc);

//                CrystalReportViewer1.Zoom(100);
//                CrystalReportViewer1.Visible = true;
//                CrystalReportViewer1.DisplayToolbar = true;
//                CrystalReportViewer1.Height = 500;
//                CrystalReportViewer1.Width = 500;
//                CrystalReportViewer1.EnableParameterPrompt = false;
//                CrystalReportViewer1.ReportSource = objdoc;
//                CrystalReportViewer1.DataBind();
//               // CrystalReportViewer1.RefreshReport();

//                objdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "CallPendingReport" + DateTime.Now + ".pdf");
                
//            }
//        catch (Exception ex)
//        {
//            Logger.LogError("Export PO Failed" + ex.StackTrace);
//            return ;
//        }
//        }

//        private void SetCrystalLogin(string sServer, string sCompanyDb, CrystalDecisions.CrystalReports.Engine.ReportDocument oRpt)
//        {
//            CrystalDecisions.Shared.ConnectionInfo oConnectInfo = new CrystalDecisions.Shared.ConnectionInfo();

//            oConnectInfo.DatabaseName = sCompanyDb;
//            oConnectInfo.ServerName = sServer;
//            oConnectInfo.IntegratedSecurity = true;
            
//            // Set the logon credentials for all tables
//            SetCrystalTablesLogin(oConnectInfo, oRpt.Database.Tables);

//            // Check for subreports
//            foreach (CrystalDecisions.CrystalReports.Engine.Section oSection in oRpt.ReportDefinition.Sections)
//            {
//                foreach (CrystalDecisions.CrystalReports.Engine.ReportObject oRptObj in oSection.ReportObjects)
//                {
//                    if (oRptObj.Kind == CrystalDecisions.Shared.ReportObjectKind.SubreportObject)
//                    {
//                        // This is a subreport so set the logon credentials for this report's tables
//                        CrystalDecisions.CrystalReports.Engine.SubreportObject oSubRptObj = oRptObj as CrystalDecisions.CrystalReports.Engine.SubreportObject;

//                        // Open the subreport
//                        CrystalDecisions.CrystalReports.Engine.ReportDocument oSubRpt = oSubRptObj.OpenSubreport(oSubRptObj.SubreportName);

//                        SetCrystalTablesLogin(oConnectInfo, oSubRpt.Database.Tables);
//                    }
//                }
//            }

//            oRpt.Refresh();

//            oRpt.SetDatabaseLogon("", "", sServer, sCompanyDb, false);
//            oRpt.VerifyDatabase();

//            oRpt.Refresh();

//        }

//        private void SetCrystalTablesLogin(CrystalDecisions.Shared.ConnectionInfo oConnectInfo, Tables oTables)
//        {
//            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in oTables)
//            {
//                CrystalDecisions.Shared.TableLogOnInfo oLogonInfo = oTable.LogOnInfo;
//                oLogonInfo.ConnectionInfo = oConnectInfo;

//                oTable.ApplyLogOnInfo(oLogonInfo);
//            }
//        }


//    }
//}