using System;
using System.Configuration;
using System.Data;

using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using BISERPCommon;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportAppServer.ClientDoc;

public partial class ECrystalReportViewer1 : System.Web.UI.Page
{
    public string ReportCaption = string.Empty;
    public string ReportName = string.Empty;
    public string ReportPath = string.Empty;
    public string ReportServerUri = string.Empty;
    public string ReportStoreProcedure = string.Empty;
    static BISERPCommon.ILogger _logger = Logger.Register(typeof(ECrystalReportViewer1));
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ReportDescription"] != null)
            {
                if (Request.QueryString["ReportDescription"].ToString() == "PurchaseOrder")
                {
                    int poId = Convert.ToInt32(Request.QueryString["POid"]);
                    ExportPO(poId);
                }
            }
        }
    }    

    public void ExportPO(int poid)
    {
        try
        {
            _logger.LogInfo("Export PO started ");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BISConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_PurchaseOrderRPT", con);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = poid;
            cmd.CommandType = CommandType.StoredProcedure;

            var conDataSource = con.DataSource;
            var conDataBase = con.Database;

            _logger.LogInfo("Export PO conDataSource: " + conDataSource + " conDataBase:" + conDataBase);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

           /* _logger.LogInfo("Export PO record count: " + dt.Rows.Count);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/Reports"), "PurchaseOrder.rpt"));

            SetCrystalLogin(conDataSource, conDataBase, rd);
            rd.SetDataSource(dt);
            rd.SetParameterValue("@ID", poid);

            _logger.LogInfo("Export PO Crystal Report parameter: " + poid);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            _logger.LogInfo("Export PO Response: " + poid);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            _logger.LogInfo("Export PO Completed");
            return File(stream, "application/pdf", "PurchaseOrder.pdf");*/

            ReportDocument rd = new ReportDocument();
            CrystalReportViewer2.DisplayGroupTree = false;
            ECrystalReportViewer.cr
            //rd.Load(Path.Combine(Server.MapPath("~/Reports/Reports"), "PurchaseOrder.rpt"));

            //SetCrystalLogin(conDataSource, conDataBase, rd);
            //rd.SetDataSource(dt);
            //rd.SetParameterValue("@ID", poid);

            //CrystalReportViewer1.Zoom(100);
            //CrystalReportViewer1.EnableParameterPrompt = false;
            //CrystalReportViewer1.ReportSource = rd;
            //CrystalReportViewer1.RefreshReport();   
        }
        catch (Exception ex)
        {
            _logger.LogError("Export PO Failed" + ex.StackTrace);
            return ;
        }

    }

    private void GenerateReport()  // To Generate Report
    {
        try
        {
            this.Title = ReportCaption;
            string connectionString = Convert.ToString(ConfigurationManager.AppSettings["ConnString"]);
            String strPwd = Convert.ToString(ConfigurationManager.AppSettings["ConnPwd"]); 
            
            Dictionary<string, string> dicParameters = new Dictionary<string, string>();
            dicParameters = (Dictionary<string, string>)Session["dicParameters"];
           
            SqlParameter[] parameters = new SqlParameter[dicParameters.Count];           

            //SqlParameter parameter = new SqlParameter("PstrCXmlDataDtl", SqlDbType.Text);
            //param.Value = "<payslip><brlocid>22</brlocid><attyear>2010</attyear><attmonth>7</attmonth><empid></empid><reportid>3</reportid><hdrfrmtid>6</hdrfrmtid><dtlfrmtid>5</dtlfrmtid></payslip>";

            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand commd = new SqlCommand();

            commd.CommandType = CommandType.StoredProcedure;
            commd.CommandText = ReportStoreProcedure; // "SMSPRPaySlipFormatReportDtl";
            commd.Connection = myConnection;

            if (dicParameters.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> KVP in dicParameters)
                {
                    parameters[i] = new SqlParameter(Convert.ToString(KVP.Key), Convert.ToString(KVP.Value));
                    commd.Parameters.Add(parameters[i++]);                    
                }
            }
            
            SqlDataAdapter ad = new SqlDataAdapter(commd);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            //CrystalReportViewer1.DisplayGroupTree = false;
            //objdoc.Load(Request.MapPath(this.ReportPath)); //(Request.MapPath(@"REPORTS/rptPrlSalaryRegisterEmpFrmt1512Report.rpt"));
            //objdoc.SetParameterValue(parameters[0].ParameterName, parameters[0].Value);//objdoc.SetParameterValue("@PstrCXmlDataDtl", "<payslip><brlocid>22</brlocid><attyear>2010</attyear><attmonth>6</attmonth><reportid>3</reportid><hdrfrmtid>6</hdrfrmtid><dtlfrmtid>5</dtlfrmtid></payslip>");
            //objdoc.SetDataSource(ds.Tables[0]);
            //objdoc.SetDatabaseLogon("sa", strPwd);

            //CrystalReportViewer1.Zoom(100);            
            //CrystalReportViewer1.EnableParameterPrompt = false;
            //CrystalReportViewer1.ReportSource = objdoc;            
            //CrystalReportViewer1.RefreshReport();             
        }
        catch (Exception ex) {
            Response.Write(ex.Message);
        }
    }

    
}
