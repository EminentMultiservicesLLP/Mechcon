using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BISERPCommon;
using BISERP.Areas.Asset.Models;

namespace BISERP.Reports
{
    public partial class AssetReport : System.Web.UI.Page
    {
        ReportParameter[] rparams;
        ReportDataSource rds;

        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(AssetReport));
        public AssetReport()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["reportid"] != null)
                {
                    if (Request.QueryString["reportid"].ToString() == "201")
                    {
                        int BranchId = 0, AssetId = 0;
                        if (Request.QueryString["BranchId"] != null)
                        {
                            if (Request.QueryString["BranchId"].ToString() != "")
                                BranchId = Convert.ToInt32(Request.QueryString["BranchId"].ToString());
                            else
                                BranchId = 0;
                        }
                        else
                            BranchId = 0;

                        if (Request.QueryString["AssetId"] != null)
                        {
                            if (Request.QueryString["AssetId"].ToString() != "")
                                AssetId = Convert.ToInt32(Request.QueryString["AssetId"].ToString());
                            else
                                AssetId = 0;
                        }
                        else
                            AssetId = 0;
                        AssetDetailReport(BranchId, AssetId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "202")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());
                        int MTypeId = 0;
                        if (Request.QueryString["MTypeId"] != null)
                        {
                            if (Request.QueryString["MTypeId"].ToString() != "")
                                MTypeId = Convert.ToInt32(Request.QueryString["MTypeId"].ToString());
                            else
                                MTypeId = 0;
                        }
                        else
                            MTypeId = 0;

                        AssetScheduleReport(fromdate, todate, MTypeId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "203")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());
                        int BranchId = 0;
                        if (Request.QueryString["BranchId"] != null)
                        {
                            if (Request.QueryString["BranchId"].ToString() != "")
                                BranchId = Convert.ToInt32(Request.QueryString["BranchId"].ToString());
                            else
                                BranchId = 0;
                        }
                        else
                            BranchId = 0;

                        RequestRegisterReport(fromdate, todate, BranchId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "204")
                    {
                        int BranchId = 0;
                        if (Request.QueryString["BranchId"] != null)
                        {
                            if (Request.QueryString["BranchId"].ToString() != "")
                                BranchId = Convert.ToInt32(Request.QueryString["BranchId"].ToString());
                            else
                                BranchId = 0;
                        }
                        else
                            BranchId = 0;

                        GetAssetdeactivation(BranchId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "205")
                    {
                        int locationId = 0;
                        if (Request.QueryString["locationId"] != null)
                        {
                            if (Request.QueryString["locationId"].ToString() != "")
                                locationId = Convert.ToInt32(Request.QueryString["locationId"].ToString());
                            else
                                locationId = 0;
                        }
                        else
                            locationId = 0;
                        AssetLocationReport(locationId);
                    }
                }
            }
        }

        private void RequestRegisterReport(DateTime fromdate, DateTime todate, int BranchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Asset/Requestregister.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<RequestRegisterModel> schedules = new List<RequestRegisterModel>();
            RequestRegisterModel model = new RequestRegisterModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/requestreg/regrequestreport/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + BranchId + Common.Constants.JsonTypeResult;
            var response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                schedules = response.Content.ReadAsAsync<List<RequestRegisterModel>>().Result;
            }
            //var result = model.ListToModel(schedules);
            rds = new ReportDataSource();
            rds.Name = "dsRequestRegister";
            rds.Value = schedules;
            ReportViewer1.LocalReport.DataSources.Add(rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void AssetScheduleReport(DateTime fromdate, DateTime todate, int MTypeId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Asset/AssetSchedule.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<AssetScheduleModel> schedules = new List<AssetScheduleModel>();
            AssetScheduleModel model = new AssetScheduleModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/assetschedule/AssetScheduleReport/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + MTypeId + Common.Constants.JsonTypeResult;
            var response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                schedules = response.Content.ReadAsAsync<List<AssetScheduleModel>>().Result;
            }
            var result = model.ListToModel(schedules);
            rds = new ReportDataSource();
            rds.Name = "dsAssetSchedule";
            rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(rds);

            ReportViewer1.LocalReport.Refresh();
        }

        protected void AddReportParameter(int intParamIndex, string strParamName, string strParamValue)
        {
            rparams[intParamIndex] = new ReportParameter(strParamName, strParamValue);
        }

        private void AssetDetailReport(int BranchId, int AssetId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Asset/AssetDetails.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<AssetLocationModel> locations = new List<AssetLocationModel>();
            AssetLocationModel model = new AssetLocationModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/assetlocation/assetdetailreport/" + BranchId + "/" + AssetId + Common.Constants.JsonTypeResult;
            var response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                locations = response.Content.ReadAsAsync<List<AssetLocationModel>>().Result;
            }
            
            rds = new ReportDataSource();
            rds.Name = "dsAssetDetails";
            rds.Value = locations;
            ReportViewer1.LocalReport.DataSources.Add(rds);

            //rparams = new ReportParameter[2];
            //AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            //AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            //ReportViewer1.LocalReport.SetParameters(rparams);
            ReportViewer1.LocalReport.Refresh();
        }
        private void GetAssetdeactivation(int BranchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Asset/AssetDeactivation.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<DeactivationDetail> Detail = new List<DeactivationDetail>();
            DeactivationDetail model = new DeactivationDetail();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/assetdel/GetAssetdeactivation/" + BranchId + Common.Constants.JsonTypeResult;
            var response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                Detail = response.Content.ReadAsAsync<List<DeactivationDetail>>().Result;
            }
            var result = model.ListToModel(Detail);
            rds = new ReportDataSource();
            rds.Name = "dsAssetDeactivation";
            rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(rds);

            ReportViewer1.LocalReport.Refresh();
        }


        private void AssetLocationReport(int locationId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Asset/AssetLocation.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<AssetLocationModel> locations = new List<AssetLocationModel>();
            AssetLocationModel model = new AssetLocationModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/assetlocation/assetlocationreport/" + locationId + Common.Constants.JsonTypeResult;
            
            
            
            var response = client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                locations = response.Content.ReadAsAsync<List<AssetLocationModel>>().Result;
            }
            rds = new ReportDataSource();
            rds.Name = "dsAssetDetails";
            rds.Value = locations;
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}