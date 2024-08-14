using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using BISERPCommon;
using BISERPCommon.Extensions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Table = System.Web.UI.WebControls.Table;

namespace BISERP.Reports
{
    public partial class report : System.Web.UI.Page
    {
        private static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(report));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PurchaseOrderReport();
            }
        }

        private void PurchaseOrderReport()
        {
            Logger.LogInfo("******** Starting PurchaseOrderReport Generation. *********");
            var connectionString = ConfigurationManager.ConnectionStrings["BISConnection"].ConnectionString;
            using (var cn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    try
                    {
                        cn.Open();
                        var poResult = new DataSet();

                        DataSet callSet = new DataSet();
                        adapter.SelectCommand = new SqlCommand("EXEC dbsp_rpt_PurchaseOrder 1", cn);
                        adapter.Fill(callSet);
                        DataTable dt = callSet.Tables[0].Copy();
                        dt.TableName = "dbsp_rpt_PurchaseOrder";
                        poResult.Tables.Add(dt);

                        callSet = new DataSet();
                        adapter.SelectCommand = new SqlCommand("EXEC dbsp_rpt_IndentDtl_PO 1,6", cn);
                        adapter.Fill(callSet);
                        dt = callSet.Tables[0].Copy();
                        dt.TableName = "dbsp_rpt_IndentDtl_PO";
                        poResult.Tables.Add(dt);

                        callSet = new DataSet();
                        adapter.SelectCommand = new SqlCommand("EXEC dbsp_rpt_DeliveryTerms_PO 1", cn);
                        adapter.Fill(callSet);
                        dt = callSet.Tables[0].Copy();
                        dt.TableName = "dbsp_rpt_DeliveryTerms_PO";
                        poResult.Tables.Add(dt);

                        callSet = new DataSet();
                        adapter.SelectCommand = new SqlCommand("EXEC dbsp_rpt_PaymentTerms_PO 1", cn);
                        adapter.Fill(callSet);
                        dt = callSet.Tables[0].Copy();
                        dt.TableName = "dbsp_rpt_PaymentTerms_PO";
                        poResult.Tables.Add(dt);

                        callSet = new DataSet();
                        adapter.SelectCommand = new SqlCommand("EXEC dbsp_rpt_OtherTerms_PO 1", cn);
                        adapter.Fill(callSet);
                        dt = callSet.Tables[0].Copy();
                        dt.TableName = "dbsp_rpt_OtherTerms_PO";
                        poResult.Tables.Add(dt);


                        ReportDocument rd = new ReportDocument();
                        rd.Load(Server.MapPath("~/Reports/Reports/PurchaseOrder.rpt"));
                        rd.SetDataSource(poResult);

                        SetDbLogonForReport(ref rd);

                        //rd.Subreports["IndentDtlSubRptForPO.rpt"].SetDataSource(POResult.Tables[1]);
                        //rd.Subreports["Delivery_Terms.rpt"].SetDataSource(POResult.Tables[2]);
                        //rd.Subreports["IVPoOtherTerms.rpt"].SetDataSource(POResult.Tables[4]);
                        //rd.Subreports["POPaymentTerms.rpt"].SetDataSource(POResult.Tables[3]);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();

                    }
                    catch (Exception ex)
                    {
                        Logger.LogInfo("******** Error in PurchaseOrderReport method: - " + ex.StackTrace + " *********");
                        throw;
                    }
                }

            }
            Logger.LogInfo("******** Finished PurchaseOrderReport Generation. *********");
        }

        private void SetDbLogonForReport(ref ReportDocument rptDoc)
        {
            SqlConnectionStringBuilder csb = null;
            try
            {
                if (ConfigurationManager.ConnectionStrings["BISConnection"] != null)
                {
                    var conn = ConfigurationManager.ConnectionStrings["BISConnection"].ConnectionString;
                    csb = new SqlConnectionStringBuilder(conn);
                }

                ConnectionInfo connectionInfo = new ConnectionInfo
                {
                    ServerName = (csb != null ? csb.DataSource : "(local)"),
                    DatabaseName = (csb != null ? csb.InitialCatalog : "BISERP"),
                    IntegratedSecurity = true,
                    Type = ConnectionInfoType.SQL
                };

                foreach (Table oTable in rptDoc.Database.Tables)
                {
                    TableLogOnInfo oTableLogOnInfo = new TableLogOnInfo();
                    oTableLogOnInfo.ConnectionInfo = connectionInfo;
                    //oTable..ApplyLogOnInfo(oTableLogOnInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInfo("******** Error in SetDbLogonForReport method: - " + ex.StackTrace + " *********");
                throw;
            }
            finally
            {
                if (csb.IsNotNull())
                    csb = null;
            }
        }
    }
}