using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Data;
using BISERP.Areas.Store.Models.Store;
using System.Net.Http;
using System.Threading;
using BISERPCommon;
using BISERP.Area.Branch.Models;
using BISERP.Areas.Accounts.Models;
using BISERP.Areas.DashBoards.Models;
using BISERP.Areas.Miscellaneous.Models;
using BISERP.Areas.Store.Models.Store.Reports;
using BISERP.Areas.Transport.Models;
using BISERPCommon.Extensions;
using BISERP.Areas.Purchase.Models;
using BISERP.Area.Purchase.Models;
using BISERP.Areas.Masters.Models;
using BISERP.Areas.SM_Reports.Models;

namespace BISERP.Views.Shared
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        ReportParameter[] _rparams;
        private ReportDataSource _rds;

        readonly HttpClient _client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ReportViewer));

        public ReportViewer()
        {
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["reportid"] != null)
                {
                    if (Request.QueryString["reportid"].ToString() == "101")
                    {
                        int indentId = Convert.ToInt32(Request.QueryString["indentid"]);
                        MaterialIndentReport(indentId);
                    }
                    if (Request.QueryString["reportid"] == "102")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());
                        int storeid = 0;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                        {
                            storeid = 0;
                        }
                        MaterialIndentDetailReport(fromdate, todate, storeid);
                    }
                    if (Request.QueryString["reportid"] == "103")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                        {
                            storeid = 0;
                        }
                        MaterialIndentReturnReport(fromdate, todate, storeid);
                    }
                    if (Request.QueryString["reportid"] == "111")
                    {
                        int issuetId = Convert.ToInt32(Request.QueryString["issueid"].ToString());
                        MaterialIssueReport(issuetId);
                    }
                    if (Request.QueryString["reportid"] == "112")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int fstoreid, tstoreid;
                        if (Request.QueryString["fstoreid"] != null)
                        {
                            fstoreid = Request.QueryString["fstoreid"] != ""
                                ? Convert.ToInt32(Request.QueryString["fstoreid"])
                                : 0;
                        }
                        else
                            fstoreid = 0;

                        if (Request.QueryString["tstoreid"] != null)
                        {
                            tstoreid = Request.QueryString["tstoreid"] != ""
                                ? Convert.ToInt32(Request.QueryString["tstoreid"])
                                : 0;
                        }
                        else
                            tstoreid = 0;

                        MaterialIssueDetailReport(fromdate, todate, fstoreid, tstoreid);
                    }
                    if (Request.QueryString["reportid"] == "114")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int itemid;
                        if (Request.QueryString["itemid"] != null)
                        {
                            itemid = Request.QueryString["itemid"] != ""
                                ? Convert.ToInt32(Request.QueryString["itemid"])
                                : 0;
                        }
                        else
                            itemid = 0;

                        MaterialIssueItemwiseReport(fromdate, todate, itemid);
                    }

                    if (Request.QueryString["reportid"] == "121")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        MaterialIndentReturnReport(fromdate, todate, storeid);
                    }
                    if (Request.QueryString["reportid"] == "141")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["tdate"]);
                        int storeid;
                        int suppid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["suppid"] != null)
                        {
                            suppid = Request.QueryString["suppid"] != ""
                                ? Convert.ToInt32(Request.QueryString["suppid"])
                                : 0;
                        }
                        else
                            suppid = 0;

                        GRNSummaryReport(fromdate, todate, storeid, suppid);
                    }
                    if (Request.QueryString["reportid"] == "142")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["tdate"]);
                        string ReportType = Request.QueryString["ReportType"];
                        int storeid, suppid, grnid;
                        string storeName = Request.QueryString["sname"];
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["suppid"] != null)
                        {
                            suppid = Request.QueryString["suppid"] != ""
                                ? Convert.ToInt32(Request.QueryString["suppid"])
                                : 0;
                        }
                        else
                            suppid = 0;

                        if (Request.QueryString["GrnId"] != null)
                        {
                            grnid = Request.QueryString["GrnId"] != ""
                                ? Convert.ToInt32(Request.QueryString["GrnId"])
                                : 0;
                        }

                        else
                            grnid = 0;

                        GRNDetailsReport(fromdate, todate, storeid, suppid, grnid, storeName, ReportType);
                    }
                    if (Request.QueryString["reportid"] == "113")
                    {
                        PendingMaterialReport();
                    }
                    if (Request.QueryString["reportid"] == "161")
                    {
                        int storeid, typeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["typeid"] != null)
                        {
                            typeid = Request.QueryString["typeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["typeid"])
                                : 0;
                        }
                        else
                            typeid = 0;

                        StoreStockSummaryReport(storeid, typeid);
                    }
                    if (Request.QueryString["reportid"] == "162")
                    {
                        int storeid, typeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["typeid"] != null)
                        {
                            typeid = Request.QueryString["typeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["typeid"])
                                : 0;
                        }
                        else
                            typeid = 0;

                        StoreStockDetailReport(storeid, typeid);
                    }
                    if (Request.QueryString["reportid"] == "163")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid, typeid, itemid = 0;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["typeid"] != null)
                        {
                            typeid = Request.QueryString["typeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["typeid"])
                                : 0;
                        }
                        else
                            typeid = 0;

                        if (Request.QueryString["itemid"] != null)
                        {
                            itemid = Request.QueryString["itemid"] != ""
                                ? Convert.ToInt32(Request.QueryString["itemid"])
                                : 0;
                        }
                        else
                            typeid = 0;

                        StoreStockRegisterReport(fromdate, todate, storeid, typeid, itemid);
                    }
                    if (Request.QueryString["reportid"] == "164")
                    {
                        int storeid, typeid;
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["typeid"] != null)
                        {
                            typeid = Request.QueryString["typeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["typeid"])
                                : 0;
                        }
                        else
                            typeid = 0;

                        StockEvaluationReport(storeid, typeid, todate);
                    }
                    if (Request.QueryString["reportid"] == "165")
                    {
                        int storeid, typeid;
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        if (Request.QueryString["typeid"] != null)
                        {
                            if (Request.QueryString["typeid"] != "")
                                typeid = Convert.ToInt32(Request.QueryString["typeid"]);
                            else
                                typeid = 0;
                        }
                        else
                            typeid = 0;

                        StockConsumtionReport(storeid, typeid, fromdate, todate);
                    }
                    if (Request.QueryString["reportid"] == "166")
                    {
                        int maxDays = 0;
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        if (Request.QueryString["maxdays"] != null)
                        {
                            maxDays = Request.QueryString["maxdays"] != ""
                                ? Convert.ToInt32(Request.QueryString["maxdays"])
                                : 0;
                        }
                        else
                            maxDays = 0;

                        ExpiryRegisterReport(fromdate, maxDays);
                    }
                    if (Request.QueryString["reportid"] == "143")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["tdate"]);
                        GRNCancelledReport(fromdate, todate);
                    }
                    if (Request.QueryString["reportid"] == "144")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["tdate"]);
                        int itemid;
                        if (Request.QueryString["itemid"] != null)
                        {
                            if (Request.QueryString["itemid"] != "")
                                itemid = Convert.ToInt32(Request.QueryString["itemid"]);
                            else
                                itemid = 0;
                        }
                        else
                            itemid = 0;
                        GrnItemwiseReport(fromdate, todate, itemid);
                    }
                    if (Request.QueryString["reportid"] == "115")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;
                        CancelMaterialAcceptReport(fromdate, todate, storeid);
                    }

                    if (Request.QueryString["reportid"] == "171")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        IssueReceipt(fromdate, todate, branchId);
                        //int branchId;
                        //if (Request.QueryString["branchId"] != null)
                        //{
                        //    branchId = Request.QueryString["branchId"] != "" ? Convert.ToInt32(Request.QueryString["branchId"]) : 0;
                        //}
                        //else
                        //    branchId = 0;

                        //int issueId = 0;
                        //if (Request.QueryString["IssueId"] != null)
                        //{
                        //    issueId = Request.QueryString["IssueId"] != "" ? Convert.ToInt32(Request.QueryString["IssueId"]) : 0;
                        //}
                        //else
                        //    issueId = 0;
                    }
                    if (Request.QueryString["reportid"].ToString() == "172")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        IssueReceiptDt(fromdate, todate, branchId);
                    }
                    if (Request.QueryString["reportid"] == "210")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        IssueReceiptRenewal(fromdate, todate, branchId);
                    }

                    if (Request.QueryString["reportid"].ToString() == "181")
                    {

                        int branchId = 0, VehicleId = 0;
                        if (Request.QueryString["Branchid"] != null)
                        {
                            if (Request.QueryString["Branchid"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        if (Request.QueryString["VehicleId"] != null)
                        {
                            if (Request.QueryString["VehicleId"].ToString() != "")
                                VehicleId = Convert.ToInt32(Request.QueryString["VehicleId"].ToString());
                            else
                                VehicleId = 0;
                        }
                        else
                            VehicleId = 0;
                        VehicleInfo(branchId, VehicleId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "182")
                    {

                        int branchId = 0;
                        if (Request.QueryString["Branchid"] != null)
                        {
                            if (Request.QueryString["Branchid"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;

                        VehicleList(branchId);
                    }
                    if (Request.QueryString["reportid"].ToString() == "183")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0, VehicleId = 0, VehicleType = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        if (Request.QueryString["VehicleId"] != null)
                        {
                            if (Request.QueryString["VehicleId"].ToString() != "")
                                VehicleId = Convert.ToInt32(Request.QueryString["VehicleId"].ToString());
                            else
                                VehicleId = 0;
                        }
                        else
                            VehicleId = 0;
                        if (Request.QueryString["VehicleType"] != null)
                        {
                            if (Request.QueryString["VehicleType"].ToString() != "")
                                VehicleType = Convert.ToInt32(Request.QueryString["VehicleType"].ToString());
                            else
                                VehicleType = 0;
                        }
                        else
                            VehicleType = 0;

                        VehicleSchedule(branchId, VehicleId, fromdate, todate, VehicleType);
                    }

                    if (Request.QueryString["reportid"].ToString() == "184")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0, VehicleId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        if (Request.QueryString["VehicleId"] != null)
                        {
                            if (Request.QueryString["VehicleId"].ToString() != "")
                                VehicleId = Convert.ToInt32(Request.QueryString["VehicleId"].ToString());
                            else
                                VehicleId = 0;
                        }
                        else
                            VehicleId = 0;
                        VehicleTransfer(branchId, VehicleId, fromdate, todate);
                    }

                    if (Request.QueryString["reportid"].ToString() == "225")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0, VehicleId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        if (Request.QueryString["VehicleId"] != null)
                        {
                            if (Request.QueryString["VehicleId"].ToString() != "")
                                VehicleId = Convert.ToInt32(Request.QueryString["VehicleId"].ToString());
                            else
                                VehicleId = 0;
                        }
                        else
                            VehicleId = 0;
                        VehicleSold(branchId, VehicleId, fromdate, todate);
                    }

                    if (Request.QueryString["reportid"].ToString() == "185")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int supplierid = 0;
                        if (Request.QueryString["supplierid"] != null)
                        {
                            if (Request.QueryString["supplierid"].ToString() != "")
                                supplierid = Convert.ToInt32(Request.QueryString["supplierid"].ToString());
                            else
                                supplierid = 0;
                        }
                        else
                            supplierid = 0;

                        PendingInvoiceReport(fromdate, todate, supplierid);
                    }
                    if (Request.QueryString["reportid"].ToString() == "152")
                    {


                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int storeid = 0;
                        if (Request.QueryString["storeid"] != null)
                        {
                            if (Request.QueryString["storeid"].ToString() != "")
                                storeid = Convert.ToInt32(Request.QueryString["storeid"].ToString());
                            else
                                storeid = 0;
                        }
                        else
                            storeid = 0;

                        StockDispose(fromdate, todate, storeid);
                    }
                    if (Request.QueryString["reportid"].ToString() == "151")
                    {


                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int storeid = 0;
                        if (Request.QueryString["storeid"] != null)
                        {
                            if (Request.QueryString["storeid"].ToString() != "")
                                storeid = Convert.ToInt32(Request.QueryString["storeid"].ToString());
                            else
                                storeid = 0;
                        }
                        else
                            storeid = 0;

                        AdjustmentVoucherReport(fromdate, todate, storeid);
                    }

                    if (Request.QueryString["reportid"] == "206")
                    {
                        if (Request.QueryString["FromDate"] != null && Request.QueryString["FromDate"] != "" &&
                            Request.QueryString["ToDate"] != null && Request.QueryString["ToDate"] != "")
                        {
                            DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                            DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                            MaterialIssueDetailAllBranchReport(fromdate, todate);
                        }
                        else
                            MaterialIssueDetailAllBranchReport(null, null);
                    }
                    if (Request.QueryString["reportid"] == "207")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid;
                        int suppid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                            storeid = 0;

                        BranchSummaryIssueReport(fromdate, todate, storeid);
                    }
                    if (Request.QueryString["reportid"].ToString() == "208")
                    {

                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"].ToString());
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"].ToString());

                        int branchId = 0, VehicleType = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;
                        if (Request.QueryString["VehicleType"] != null)
                        {
                            if (Request.QueryString["VehicleType"].ToString() != "")
                                VehicleType = Convert.ToInt32(Request.QueryString["VehicleType"].ToString());
                            else
                                VehicleType = 0;
                        }
                        else
                            VehicleType = 0;
                        Fuelconsumptionrpt(branchId, fromdate, todate, VehicleType);
                    }


                    if (Request.QueryString["reportid"] == "226")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                        DateTime todate = Convert.ToDateTime(Request.QueryString["todate"]);
                        int storeid;
                        if (Request.QueryString["storeid"] != null)
                        {
                            storeid = Request.QueryString["storeid"] != ""
                                ? Convert.ToInt32(Request.QueryString["storeid"])
                                : 0;
                        }
                        else
                        {
                            storeid = 0;
                        }
                        GetStoreStatusReportBranchWise(storeid, fromdate, todate);
                    }
                    if (Request.QueryString["reportid"].ToString() == "227")
                    {
                        DateTime fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);

                        int branchId = 0;
                        if (Request.QueryString["branchId"] != null)
                        {
                            if (Request.QueryString["branchId"].ToString() != "")
                                branchId = Convert.ToInt32(Request.QueryString["branchId"].ToString());
                            else
                                branchId = 0;
                        }
                        else
                            branchId = 0;

                        VehicleInforpt(branchId, fromdate);
                    }

                    if (Request.QueryString["reportid"].ToString() == "228")
                    {

                        int poId = 0;
                        string rpttype = "";
                        string poType = "";
                        if (Request.QueryString["poId"] != null)
                        {
                            if (Request.QueryString["poId"].ToString() != "")
                                poId = Convert.ToInt32(Request.QueryString["poId"].ToString());
                            else
                                poId = 0;
                        }
                        else
                            poId = 0;
                        if (Request.QueryString["poType"] != "")
                        {
                            if (Request.QueryString["poType"] != "")
                                poType = Request.QueryString["poType"];
                            else
                                poType = "";
                        }
                        else
                            poType = "";
                        if (Request.QueryString["rpttype"] != null)
                        {
                            if (Request.QueryString["rpttype"] != "")
                                rpttype = Request.QueryString["rpttype"];
                            else
                                rpttype = "";
                        }
                        else
                            rpttype = "";

                        PurchaseRpt(poId, poType, rpttype);
                    }

                    if (Request.QueryString["reportid"].ToString() == "242")
                    {
                        int purchaseReturnId = 0;
                        string rpttype = "";
                        if (Request.QueryString["purchaseReturnId"] != null)
                        {
                            if (Request.QueryString["purchaseReturnId"].ToString() != "")
                                purchaseReturnId = Convert.ToInt32(Request.QueryString["purchaseReturnId"].ToString());
                            else
                                purchaseReturnId = 0;
                        }
                        else
                            purchaseReturnId = 0;

                        PurchaseReturnRpt(purchaseReturnId, rpttype);
                    }

                    if (Request.QueryString["reportid"].ToString() == "231")
                    {

                        int clientBillid = 0, ReportFor = 0;

                        if (Request.QueryString["clientBillid"] != null)
                        {
                            if (Request.QueryString["clientBillid"].ToString() != "")
                                clientBillid = Convert.ToInt32(Request.QueryString["clientBillid"].ToString());
                            else
                                clientBillid = 0;
                        }
                        else
                            ReportFor = 0;
                        if (Request.QueryString["ReportFor"] != null)
                        {
                            if (Request.QueryString["ReportFor"].ToString() != "")
                                ReportFor = Convert.ToInt32(Request.QueryString["ReportFor"].ToString());
                            else
                                ReportFor = 0;
                        }
                        else
                            ReportFor = 0;

                        ClientBillid(clientBillid, ReportFor);
                    }
                    if (Request.QueryString["reportid"].ToString() == "232")
                    {

                        int storeid = 0;

                        if (Request.QueryString["storeid"] != null)
                        {
                            if (Request.QueryString["storeid"].ToString() != "")
                                storeid = Convert.ToInt32(Request.QueryString["storeid"].ToString());
                            else
                                storeid = 0;
                        }
                        else
                            storeid = 0;

                        PendingGrnItemWise(storeid);
                    }
                    if (Request.QueryString["reportid"].ToString() == "233")
                    {

                        int clientBillid = 0, ReportFor = 0;

                        if (Request.QueryString["clientBillid"] != null)
                        {
                            if (Request.QueryString["clientBillid"].ToString() != "")
                                clientBillid = Convert.ToInt32(Request.QueryString["clientBillid"].ToString());
                            else
                                clientBillid = 0;
                        }
                        else
                            ReportFor = 0;
                        if (Request.QueryString["ReportFor"] != null)
                        {
                            if (Request.QueryString["ReportFor"].ToString() != "")
                                ReportFor = Convert.ToInt32(Request.QueryString["ReportFor"].ToString());
                            else
                                ReportFor = 0;
                        }
                        else
                            ReportFor = 0;

                        ClientBillid(clientBillid, ReportFor);
                    }

                    if (Request.QueryString["reportid"].ToString() == "234")
                    {

                        int clientBillid = 0, ReportFor = 0;

                        if (Request.QueryString["clientBillid"] != null)
                        {
                            if (Request.QueryString["clientBillid"].ToString() != "")
                                clientBillid = Convert.ToInt32(Request.QueryString["clientBillid"].ToString());
                            else
                                clientBillid = 0;
                        }
                        else
                            ReportFor = 0;
                        if (Request.QueryString["ReportFor"] != null)
                        {
                            if (Request.QueryString["ReportFor"].ToString() != "")
                                ReportFor = Convert.ToInt32(Request.QueryString["ReportFor"].ToString());
                            else
                                ReportFor = 0;
                        }
                        else
                            ReportFor = 0;

                        ClientBillid(clientBillid, ReportFor);
                    }
                    if (Request.QueryString["reportid"].ToString() == "235")
                    {

                        int clientBillid = 0, ReportFor = 0;

                        if (Request.QueryString["clientBillid"] != null)
                        {
                            if (Request.QueryString["clientBillid"].ToString() != "")
                                clientBillid = Convert.ToInt32(Request.QueryString["clientBillid"].ToString());
                            else
                                clientBillid = 0;
                        }
                        else
                            ReportFor = 0;
                        if (Request.QueryString["ReportFor"] != null)
                        {
                            if (Request.QueryString["ReportFor"].ToString() != "")
                                ReportFor = Convert.ToInt32(Request.QueryString["ReportFor"].ToString());
                            else
                                ReportFor = 0;
                        }
                        else
                            ReportFor = 0;

                        ClientBillid(clientBillid, ReportFor);
                    }
                    if (Request.QueryString["reportid"].ToString() == "236")
                    {

                        int clientBillid = 0, ReportFor = 0;

                        if (Request.QueryString["clientBillid"] != null)
                        {
                            if (Request.QueryString["clientBillid"].ToString() != "")
                                clientBillid = Convert.ToInt32(Request.QueryString["clientBillid"].ToString());
                            else
                                clientBillid = 0;
                        }
                        else
                            ReportFor = 0;
                        if (Request.QueryString["ReportFor"] != null)
                        {
                            if (Request.QueryString["ReportFor"].ToString() != "")
                                ReportFor = Convert.ToInt32(Request.QueryString["ReportFor"].ToString());
                            else
                                ReportFor = 0;
                        }
                        else
                            ReportFor = 0;

                        ClientBillid(clientBillid, ReportFor);
                    }
                    if (Request.QueryString["reportid"].ToString() == "237")
                    {

                        int IndentId = 0;
                        string pIType = "";
                        if (Request.QueryString["IndentId"] != null)
                        {
                            if (Request.QueryString["IndentId"].ToString() != "")
                                IndentId = Convert.ToInt32(Request.QueryString["IndentId"].ToString());
                            else
                                IndentId = 0;
                        }
                        else
                            IndentId = 0;

                        if (Request.QueryString["pIType"] != "")
                        {
                            if (Request.QueryString["pIType"] != "")
                                pIType = Request.QueryString["pIType"];
                            else
                                pIType = "";
                        }
                        else
                            pIType = "";
                        PurchaseIndentById(IndentId, pIType);
                    }
                    if (Request.QueryString["reportid"].ToString() == "238")
                    {

                        int IndentId = 0;
                        string RFQType = "";
                        if (Request.QueryString["IndentId"] != null)
                        {
                            if (Request.QueryString["IndentId"].ToString() != "")
                                IndentId = Convert.ToInt32(Request.QueryString["IndentId"].ToString());
                            else
                                IndentId = 0;
                        }
                        else
                            IndentId = 0;

                        if (Request.QueryString["RFQType"] != "")
                        {
                            if (Request.QueryString["RFQType"] != "")
                                RFQType = Request.QueryString["RFQType"];
                            else
                                RFQType = "";
                        }
                        else
                            RFQType = "";

                        RFQById(IndentId, RFQType);
                    }
                    if (Request.QueryString["reportid"].ToString() == "239")
                    {

                        int IndentId = 0;
                        string WOType = "";
                        if (Request.QueryString["IndentId"] != null)
                        {
                            if (Request.QueryString["IndentId"].ToString() != "")
                                IndentId = Convert.ToInt32(Request.QueryString["IndentId"].ToString());
                            else
                                IndentId = 0;
                        }
                        else
                            IndentId = 0;

                        if (Request.QueryString["WOType"] != "")
                        {
                            if (Request.QueryString["WOType"] != "")
                                WOType = Request.QueryString["WOType"];
                            else
                                WOType = "";
                        }
                        else
                            WOType = "";

                        WorkOrderById(IndentId, WOType);
                    }

                    if (Request.QueryString["reportid"].ToString() == "240")
                    {

                        int PLId = 0;
                        if (Request.QueryString["packingListId"] != null)
                        {
                            if (Request.QueryString["packingListId"].ToString() != "")
                                PLId = Convert.ToInt32(Request.QueryString["packingListId"].ToString());
                            else
                                PLId = 0;
                        }
                        else
                            PLId = 0;
                        PackingListById(PLId);
                    }

                    if (Request.QueryString["reportid"].ToString() == "241")
                    {
                        int orderBookId = 0;
                        if (Request.QueryString["orderBookId"] != null)
                        {
                            if (Request.QueryString["orderBookId"].ToString() != "")
                                orderBookId = Convert.ToInt32(Request.QueryString["orderBookId"].ToString());
                            else
                                orderBookId = 0;
                        }
                        else
                            orderBookId = 0;
                        WorkOrderRptById(orderBookId);
                    }
                }
            }
        }

        private void MaterialIndentDetailReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIndentDetail.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIndentModel> indent = new List<MaterialIndentModel>();
            MaterialIndentModel model = new MaterialIndentModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materilaindents/getallmtidsrpt/" + strfromdate + "/" +
                          strtodate + "/" + StoreId + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                indent = response.Content.ReadAsAsync<List<MaterialIndentModel>>().Result;
            }
            var result = model.ListToModel(indent);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIndent";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void MaterialIndentReturnReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIndentReturn.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIndentModel> indent = new List<MaterialIndentModel>();
            MaterialIndentModel model = new MaterialIndentModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materilaindents/mireturnrpt/" + strfromdate + "/" +
                          strtodate + "/" + StoreId + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                indent = response.Content.ReadAsAsync<List<MaterialIndentModel>>().Result;
            }
            var result = model.ListToModel(indent);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIndent";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void GetStoreStatusReportBranchWise(int StoreId, DateTime fromdate, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StoreStatusReport.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;

            List<StockRegisterModel> stock = new List<StockRegisterModel>();
            StockRegisterModel model = new StockRegisterModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockregister/getStorewiseStock/" + StoreId + "/" +
                          strfromdate + "/" + strtodate + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockRegisterModel>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "stockstatus";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "fromdate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "toDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }


        private void MaterialIndentReport(int IndentId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIndent.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            MaterialIndentModel indent = new MaterialIndentModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materilaindents/getmaterilaIndentbyid/" +
                          IndentId.ToString() + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                indent = response.Content.ReadAsAsync<MaterialIndentModel>().Result;
            }
            DataTable dt = indent.ModelToData(indent);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIndent";
            _rds.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        protected void AddReportParameter(int intParamIndex, string strParamName, string strParamValue)
        {
            _rparams[intParamIndex] = new ReportParameter(strParamName, strParamValue);
        }

        private void MaterialIssueReport(int IssueId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIssue.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            MaterialIssueModel issue = new MaterialIssueModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/mireport/" + IssueId.ToString() + "/" +
                          Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<MaterialIssueModel>().Result;
            }
            DataTable dt = issue.ModelToData(issue);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssue";
            _rds.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void MaterialIssueDetailReport(DateTime fromdate, DateTime todate, int fStoreId, int tStoreId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIssueDetail.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueModel> issue = new List<MaterialIssueModel>();
            MaterialIssueModel model = new MaterialIssueModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/getallmidsrpt/" + strfromdate + "/" +
                          strtodate + "/" + fStoreId + "/" + tStoreId + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueModel>>().Result;
            }
            var result = model.ListToModel(issue);
            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssue";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void BranchSummaryIssueReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/BranchSummaryIssue.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueModel> issue = new List<MaterialIssueModel>();
            MaterialIssueModel model = new MaterialIssueModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/getallmidsrpt/" + strfromdate + "/" +
                          strtodate + "/" + StoreId + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueModel>>().Result;
            }
            var result = model.ListToModel(issue);
            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssue";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void GRNSummaryReport(DateTime fromdate, DateTime todate, int storeid, int suppid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/GRNSummary.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<GRNModel> grn = new List<GRNModel>();
            GRNModel model = new GRNModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/grn/grnsummary/" + strfromdate + "/" + strtodate +
                          "/" + storeid + "/" + suppid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                grn = response.Content.ReadAsAsync<List<GRNModel>>().Result;
            }
            //var result = model.ListToModel(grn);

            _rds = new ReportDataSource();
            _rds.Name = "dsGRNSummary";
            _rds.Value = grn;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }



        private void PendingMaterialReport()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/PendingMaterialIssue.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueModel> issue = new List<MaterialIssueModel>();
            MaterialIssueModel model = new MaterialIssueModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/getallpmidsrpt/" +
                          Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueModel>>().Result;
            }
            var result = model.ListToModel(issue);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssue";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.Refresh();
        }

        private void GRNCancelledReport(DateTime fromdate, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/GRNCancelled.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<GRNModel> grn = new List<GRNModel>();
            GRNModel model = new GRNModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/grn/grncancel/" + strfromdate + "/" + strtodate +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                grn = response.Content.ReadAsAsync<List<GRNModel>>().Result;
            }
            var result = model.ListToModel(grn);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);

            _rds = new ReportDataSource();
            _rds.Name = "dsGRNDetails";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.Refresh();
        }

        private void MaterialIssueItemwiseReport(DateTime fromdate, DateTime todate, int itemid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIssueItemwise.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueModel> issue = new List<MaterialIssueModel>();
            MaterialIssueModel model = new MaterialIssueModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/miitemwiserpt/" + strfromdate + "/" +
                          strtodate + "/" + itemid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueModel>>().Result;
            }
            var result = model.ListToModel(issue);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssueItemwise";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StoreStockSummaryReport(int storeid, int typeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StoreStockSummary.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockDetailsEntity> stock = new List<StockDetailsEntity>();
            StockDetailsEntity model = new StockDetailsEntity();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockdetails/storestocksummary/" + storeid + "/" +
                          typeid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockDetailsEntity>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsStoreStockSummary";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[1];
            AddReportParameter(0, "UptoDate", System.DateTime.Now.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StoreStockDetailReport(int storeid, int typeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StoreStockDetails.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockDetailsEntity> stock = new List<StockDetailsEntity>();
            StockDetailsEntity model = new StockDetailsEntity();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockdetails/storestockdetails/" + storeid + "/" +
                          typeid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockDetailsEntity>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsStoreStockDetails";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[1];
            AddReportParameter(0, "UptoDate", System.DateTime.Now.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StoreStockRegisterReport(DateTime fromdate, DateTime todate, int storeid, int typeid, int itemid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StoreStockRegister.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockRegisterModel> stock = new List<StockRegisterModel>();
            StockRegisterModel model = new StockRegisterModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockregister/storestockregister/" + strfromdate +
                          "/" + strtodate + "/" + storeid + "/" + typeid + "/" + itemid +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockRegisterModel>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsStoreStockRegister";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "fromdate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "toDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void GrnItemwiseReport(DateTime fromdate, DateTime todate, int itemid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/GRNItemwise.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<GRNModel> grn = new List<GRNModel>();
            GRNModel model = new GRNModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/grn/grnitemwise/" + strfromdate + "/" + strtodate +
                          "/" + itemid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                grn = response.Content.ReadAsAsync<List<GRNModel>>().Result;
            }
            var result = model.ListToModel(grn);

            _rds = new ReportDataSource();
            _rds.Name = "dsGRNDetails";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void CancelMaterialAcceptReport(DateTime fromdate, DateTime todate, int storeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/CancelledMaterialIssueAcceptance.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueModel> issue = new List<MaterialIssueModel>();
            MaterialIssueModel model = new MaterialIssueModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/getallcmidsrpt/" + strfromdate + "/" +
                          strtodate + "/" + storeid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueModel>>().Result;
            }
            var result = model.ListToModel(issue);

            _rds = new ReportDataSource();
            _rds.Name = "dsMaterialIssue";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();

        }

        private void IssueReceipt(DateTime fromdate, DateTime todate, int branchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/IssueReceipt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueGuardModel> issue = new List<MaterialIssueGuardModel>();
            MaterialIssueGuardModel model = new MaterialIssueGuardModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/missueguard/getissueguarddtReceipt/" + strfromdate +
                          "/" + strtodate + "/" + branchId + "/" + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueGuardModel>>().Result;
            }
            var result = model.ListToModel(issue);
            _rds = new ReportDataSource();
            _rds.Name = "dsbranch";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
            //ReportViewer1.LocalReport.ReportPath = ReportPath;
            //List<MaterialIssueGuardModel> issue = new List<MaterialIssueGuardModel>();
            //MaterialIssueGuardModel model = new MaterialIssueGuardModel();
            //string _url = BISERP.Common.Constants.WebAPIAddress + "/missueguard/getissueguardReceipt/" + branchId + "/" + IssueId + Common.Constants.JsonTypeResult;
            //var response = _client.GetAsync(_url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    issue = response.Content.ReadAsAsync<List<MaterialIssueGuardModel>>().Result;
            //}
            //var result = model.ListToModel(issue);

            //_rds = new ReportDataSource();
            //_rds.Name = "dsbranch";
            //_rds.Value = result;
            //ReportViewer1.LocalReport.DataSources.Add(_rds);
            //ReportViewer1.LocalReport.Refresh();
        }

        private void IssueReceiptDt(DateTime fromdate, DateTime todate, int branchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/IssueReceiptDetail.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueGuardModel> issue = new List<MaterialIssueGuardModel>();
            MaterialIssueGuardModel model = new MaterialIssueGuardModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/missueguard/getissueguarddtReceipt/" + strfromdate +
                          "/" + strtodate + "/" + branchId + "/" + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueGuardModel>>().Result;
            }
            var result = model.ListToModel(issue);
            _rds = new ReportDataSource();
            _rds.Name = "dsbranch";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void IssueReceiptRenewal(DateTime fromdate, DateTime todate, int branchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/Guarduniformrenewal.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueGuardModel> issue = new List<MaterialIssueGuardModel>();
            MaterialIssueGuardModel model = new MaterialIssueGuardModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/missueguard/getissueguardrenewaldtReceipt/" +
                          strfromdate + "/" + strtodate + "/" + branchId + "/" + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueGuardModel>>().Result;
            }
            var result = model.ListToModel(issue);
            _rds = new ReportDataSource();
            _rds.Name = "dsbranch";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StockEvaluationReport(int storeid, int typeid, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StoreStockEvaluation.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockRegisterModel> stock = new List<StockRegisterModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockregister/stockevaluation/" +
                          todate.ToString("MM-dd-yy") + "/" + storeid + "/" + typeid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockRegisterModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsStockEvaluation";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[1];
            AddReportParameter(0, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StockConsumtionReport(int storeid, int typeid, DateTime fromdate, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StockConsumption.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockRegisterModel> stock = new List<StockRegisterModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockregister/stockconsumption/" +
                          fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + storeid + "/" +
                          typeid + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<StockRegisterModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsStockConsumption";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void ExpiryRegisterReport(DateTime fromdate, int MaxDays)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/ExpiryRegister.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<BatchModel> stock = new List<BatchModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/stockregister/expiryregister/" +
                          fromdate.ToString("MM-dd-yyyy") + "/" + MaxDays + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<BatchModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsExpiryRegister";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[1];
            AddReportParameter(0, "fromdate", fromdate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleInfo(int branchId, int VehicleId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/VehicleInformation.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<VehicleInfoModel> vehicle = new List<VehicleInfoModel>();
            VehicleInfoModel model = new VehicleInfoModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/vehicle/getvehicleinfo/" + branchId + "/" +
                          VehicleId + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                vehicle = response.Content.ReadAsAsync<List<VehicleInfoModel>>().Result;
            }
            var result = model.ListToModel(vehicle);
            _rds = new ReportDataSource();
            _rds.Name = "dsTransport";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleList(int branchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/VehicleList.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<VehicleInfoModel> vehicle = new List<VehicleInfoModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/vehicle/getvehicleList/" + branchId +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                vehicle = response.Content.ReadAsAsync<List<VehicleInfoModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsVehicleInFormation";
            _rds.Value = vehicle;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            //rparams = new ReportParameter[1];
            //AddReportParameter(0, "fromdate", fromdate.ToString("dd/MM/yyyy"));
            //  ReportViewer1.LocalReport.SetParameters(rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleTransfer(int branchId, int VehicleId, DateTime fromdate, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/VehicleTransfer.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<VehicleTransferModel> stock = new List<VehicleTransferModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/transfer/gettransfervehiclerpt/" +
                          fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<VehicleTransferModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsVehicleTransfer";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleSold(int branchId, int VehicleId, DateTime fromdate, DateTime todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/VehicleSold.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<VehicleTransferModel> stock = new List<VehicleTransferModel>();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/transfer/getsoldvehiclerpt/" +
                          fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                stock = response.Content.ReadAsAsync<List<VehicleTransferModel>>().Result;
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsVehicleTransfer";
            _rds.Value = stock;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleSchedule(int branchId, int VehicleId, DateTime fromdate, DateTime todate, int VehicleType)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/ScheduleTrip.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<DriverScheduleModel> driver = new List<DriverScheduleModel>();
            DriverScheduleModel model = new DriverScheduleModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/driverschedule/getdriverschedulerpt/" + VehicleId +
                          "/" + branchId + "/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" +
                          VehicleType + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                driver = response.Content.ReadAsAsync<List<DriverScheduleModel>>().Result;
            }
            var result = model.ListToModel(driver);
            _rds = new ReportDataSource();
            _rds.Name = "dsDriverSchedule";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleSchedule(int branchId)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/DriverReport.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<DriverScheduleModel> driver = new List<DriverScheduleModel>();
            DriverScheduleModel model = new DriverScheduleModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/driverschedule/getdriverschedulerpt/" + branchId +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                driver = response.Content.ReadAsAsync<List<DriverScheduleModel>>().Result;
            }
            var result = model.ListToModel(driver);
            _rds = new ReportDataSource();
            _rds.Name = "dsDriverSchedule";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void PendingInvoiceReport(DateTime fromdate, DateTime todate, int supplierid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/PendingInvoices.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<SupplierBillModel> bill = new List<SupplierBillModel>();
            MaterialIndentModel model = new MaterialIndentModel();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = BISERP.Common.Constants.WebAPIAddress + "/billcreation/getsupplierbill/" + supplierid + "/" +
                          strfromdate + "/" + strtodate + "/" + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                bill = response.Content.ReadAsAsync<List<SupplierBillModel>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsPendingInvoices";
            _rds.Value = bill;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void StockDispose(DateTime fromdate, DateTime todate, int storeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StockDispose.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<StockDisposeModel> Stock = new List<StockDisposeModel>();
            StockDisposeModel model = new StockDisposeModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/Stockdispose/stockdisposerpt/" +
                          fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + storeid +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                Stock = response.Content.ReadAsAsync<List<StockDisposeModel>>().Result;
            }
            var result = model.ListToModel(Stock);
            _rds = new ReportDataSource();
            _rds.Name = "StockDispose";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void AdjustmentVoucherReport(DateTime fromdate, DateTime todate, int storeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/StockAdjustmentVoucher.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<AdjustmentVoucherModel> Voucher = new List<AdjustmentVoucherModel>();
            AdjustmentVoucherModel model = new AdjustmentVoucherModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/voucher/adjustmentvoucherRpt/" +
                          fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + storeid +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                Voucher = response.Content.ReadAsAsync<List<AdjustmentVoucherModel>>().Result;
            }
            var result = model.ListToModel(Voucher);
            _rds = new ReportDataSource();
            _rds.Name = "StockAdjustmentVoucher";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", fromdate.ToString("dd/MM/yyyy"));
            AddReportParameter(1, "ToDate", todate.ToString("dd/MM/yyyy"));
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }

        private void MaterialIssueDetailAllBranchReport(DateTime? fromdate, DateTime? todate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Store/MaterialIssueAllBranch.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<MaterialIssueAllBranchEntity> issue = new List<MaterialIssueAllBranchEntity>();
            MaterialIssueAllBranchEntity model = new MaterialIssueAllBranchEntity();
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            string _url = string.Empty;
            if (fromdate.IsNotNull() || todate.IsNotNull())
            {

                _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/MaterialIssueAllBranch/" +
                       strfromdate + "/" + strtodate + Common.Constants.JsonTypeResult;
            }
            else
            {
                _url = BISERP.Common.Constants.WebAPIAddress + "/materialissue/MaterialIssueAllBranch/" +
                       Common.Constants.JsonTypeResult;
            }
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                issue = response.Content.ReadAsAsync<List<MaterialIssueAllBranchEntity>>().Result;
            }
            var result = issue;
            _rds = new ReportDataSource();
            _rds.Name = "MaterialIssueAllBranchReport";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            _rparams = new ReportParameter[2];
            AddReportParameter(0, "FromDate", strfromdate);
            AddReportParameter(1, "ToDate", strtodate);
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.Refresh();
        }


        private void Fuelconsumptionrpt(int branchId, DateTime fromdate, DateTime todate, int VehicleType)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/Fuelconsumption.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            List<DriverScheduleModel> driver = new List<DriverScheduleModel>();
            DriverScheduleModel model = new DriverScheduleModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/driverschedule/getFuelconsumptionrpt/" + branchId +
                          "/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + VehicleType +
                          Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                driver = response.Content.ReadAsAsync<List<DriverScheduleModel>>().Result;
            }
            var result = model.ListToModel(driver);
            _rds = new ReportDataSource();
            _rds.Name = "dsDriverSchedule";
            _rds.Value = result;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private void VehicleInforpt(int branchId, DateTime fromdate)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string ReportPath = Server.MapPath("../Reports/Transport/DriverReport.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;

            List<DriverScheduleModel> driver = new List<DriverScheduleModel>();
            DriverScheduleModel model = new DriverScheduleModel();
            string _url = BISERP.Common.Constants.WebAPIAddress + "/driverschedule/getdriverschedulerpt/" + branchId +
                          "/" + fromdate.ToString("MM-dd-yy") + "/" + Common.Constants.JsonTypeResult;
            var response = _client.GetAsync(_url).Result;
            if (response.IsSuccessStatusCode)
            {
                driver = response.Content.ReadAsAsync<List<DriverScheduleModel>>().Result;
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsDriverSchedule";
            _rds.Value = driver;
            ReportViewer1.LocalReport.DataSources.Add(_rds);

            ReportViewer1.LocalReport.Refresh();
        }

        private List<GRNModel> grn = null;

        public async void GRNDetailsReport(DateTime fromdate, DateTime todate, int storeid, int suppid, int grnid, string StoreName, string ReportType, string strOption = "Preview")
        {

            GetReportReady();

            string reportPath = Server.MapPath("../Reports/Store/GRNDetails.rdlc");
            ReportViewer1.LocalReport.ReportPath = reportPath;

            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");

            string _url = url + "/grn/grndetails/" + strfromdate + "/" + strtodate + "/" + storeid + "/" + suppid + "/" + grnid + "/" + ReportType + Common.Constants.JsonTypeResult;
            var response = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                grn = new List<GRNModel>();
                grn.AddRange(response.ToList());
                //string _url = BISERP.Common.Constants.WebAPIAddress + "/grn/grndetails/" + strfromdate + "/" + strtodate + "/" + storeid + "/" + suppid + "/" + grnid + Common.Constants.JsonTypeResult;
                //var response = await _client.GetAsync(_url);

                //if (response != null)
                //{
                //    grn = response.Content.ReadAsAsync<List<GRNModel>>().Result;
                //}
                var result = from m in grn
                             from mdt in m.GRNDetails
                             select new
                             {
                                 Id = m.ID,
                                 m.GrnTypeID,
                                 m.StoreId,
                                 m.SupplierID,
                                 m.DCNo,
                                 m.DCDate,
                                 m.GRNNo,
                                 m.GRNDate,
                                 m.InvoiceNo,
                                 m.InvoiceDate,
                                 m.TotalAmount,
                                 m.AuthorisedAmt,
                                 m.Transporter,
                                 m.VehicleNo,
                                 m.TotalTaxamt,
                                 m.TotalFORE,
                                 m.TotalExciseAmt,
                                 m.TotalDisc,
                                 m.InwardNo,
                                 m.InwardDate,
                                 m.GRNType,
                                 m.StoreName,
                                 m.SupplierName,
                                 m.GrnPaymentType,
                                 m.PONo,
                                 m.PODate,
                                 m.Authorized,
                                 m.Service,
                                 m.Warranty,
                                 m.Preparedby,
                                 m.AuthorizedByName,
                                 m.companyName,
                                 m.companyAddress,
                                 m.companyEmail,
                                 m.companyCIN,
                                 m.companyGST,
                                 m.SupplierCode,
                                 mdt.ItemName,
                                 mdt.BatchName,
                                 mdt.Qty,
                                 mdt.FreeQty,
                                 mdt.Rate,
                                 mdt.Discount,
                                 mdt.MRP,
                                 mdt.TaxAmount,
                                 mdt.Amount,
                                 mdt.DescriptiveName,
                                 mdt.RejectedQty,
                                 mdt.RecieveQty,
                                 mdt.RejectionReason,
                                 mdt.PopendingQty,
                                 mdt.UnitName,
                                 mdt.CQty,
                                 mdt.ItemCode,
                                 mdt.Make,
                                 mdt.MaterialOfConstruct,
                                 mdt.IndentRemark,
                                 mdt.POIndentRemark,
                             };
                //var result = model.ListToModel(grn);

                _rparams = new ReportParameter[3];
                AddReportParameter(0, "StoreName", StoreName);
                AddReportParameter(1, "FromDate", fromdate.ToString("dd/MM/yyyy"));
                AddReportParameter(2, "ToDate", todate.ToString("dd/MM/yyyy"));
                ReportViewer1.LocalReport.SetParameters(_rparams);

                ReportDataSource rds = new ReportDataSource
                {
                    Name = "dsGRNDetails",
                    Value = result.ToList()
                };

                prepareReport(ReportViewer1.LocalReport, reportPath, rds);
                ReportViewer1.LocalReport.Refresh();
            }

            switch (strOption)
            {
                case "Preview":
                    ReportViewer1.LocalReport.Refresh();
                    break;
                case "PDF":
                    GenerateFile(strOption, ReportViewer1.LocalReport, "GRNReport");
                    break;
                case "Excel":
                    GenerateFile(strOption, ReportViewer1.LocalReport, "GRNReport");
                    break;
            }

        }


        private List<ClientBillingModel> billList;

        public async void ClientBillid(int clientBillid, int ReportFor)
        {
            string ReportPath = Server.MapPath("../Reports/Billing/ClientBillingRpt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/clientbilling/getClienBillRptdetailById/" + clientBillid.ToString() + "/" + ReportFor +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<ClientBillingModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                billList = new List<ClientBillingModel>();
                billList.Add(response);
            }
            billList[0].InvoiceType = billList[0].ClientBillingDt.Select(m => m.InvoiceType).FirstOrDefault();
            _rds = new ReportDataSource();
            _rds.Name = "dsClientBilling";
            _rds.Value = billList;
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ClientBillDtl);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ClientPaymentTerm);
            ReportViewer1.LocalReport.Refresh();
        }

        void ClientBillDtl(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsClientBillingDtl", billList[0].ClientBillingDt));
        }

        void ClientPaymentTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetClientPaymentTerm", billList[0].PaymentTerm));
        }

        private List<PurchaseOrderModel> podetailList;
        public async void PurchaseRpt(int poId, string poType, string rptype, string strOption = "Preview")
        {
            GetReportReady();
            string ReportPath = "";
            if (rptype == "Horizontal")
            {
                ReportPath = Server.MapPath("../Reports/Purchase/PurchaseHorizontal/PurchaseReport.rdlc");
            }
            else
            {
                ReportPath = Server.MapPath("../Reports/Purchase/PurchaseVerticle/PurchaseReportVert.rdlc");
            }

            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/purchaseorder/getbypoid/" + poId.ToString() + Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<PurchaseOrderModel>(_client, _url, CancellationToken.None);

            if (response != null)
            {
                podetailList = new List<PurchaseOrderModel>();
                podetailList.Add(response);

                ReportDataSource _rds = new ReportDataSource();
                _rds.Name = "DataSetPurchaseOrder";
                _rds.Value = podetailList;
                _rparams = new ReportParameter[1];
                AddReportParameter(0, "poType", poType);
                ReportViewer1.LocalReport.SetParameters(_rparams);
                prepareReport(ReportViewer1.LocalReport, ReportPath, _rds);

                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(POdetail);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(Deliveryterm);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PaymentTerm);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PoTax);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(Basis);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(Inspection);

            }

            switch (strOption)
            {
                case "Preview":
                    ReportViewer1.LocalReport.Refresh();
                    break;
                case "PDF":
                    GenerateFile(strOption, ReportViewer1.LocalReport, "Reports/report.pdf");
                    break;
                case "Excel":
                    GenerateFile(strOption, ReportViewer1.LocalReport, "Reports/report.xls");
                    break;
            }
        }

        void POdetail(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetPODetail", podetailList[0].PODetails));
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetOtherterm", podetailList[0].POOtherTerms));
        }

        void Deliveryterm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetDeliveryTerm", podetailList[0].PODeliveryTerms));
        }

        void PaymentTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetPaymentTerm", podetailList[0].POPaymenterms));
        }

        void PoTax(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetTax", podetailList[0].POTax));
        }

        void Basis(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetBasis", podetailList[0].POBasis));
        }

        void Inspection(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetIspection", podetailList[0].POInspectio));
        }

        private List<PurchaseReturnRptModel> pr;
        public async void PurchaseReturnRpt(int purchaseReturnId, string prType)
        {
            try
            {
                string reportPath = Server.MapPath("../Reports/Purchase/PurchaseReturn/PurchaseReturnReport.rdlc");
                ReportViewer1.LocalReport.ReportPath = reportPath;

                string requestUrl = $"{url}/purchasereturn/getpurchasereturnbyid/{purchaseReturnId}{Common.Constants.JsonTypeResult}";
                var response = await Common.AsyncWebCalls.GetAsync<PurchaseReturnRptModel>(_client, requestUrl, CancellationToken.None);

                if (response != null)
                {
                    pr = new List<PurchaseReturnRptModel> { response };

                    _rds = new ReportDataSource
                    {
                        Name = "DataSetPurchaseReturn",
                        Value = pr
                    };

                    var report = ReportViewer1.LocalReport;
                    report.DataSources.Clear();
                    report.DataSources.Add(_rds);
                    report.SubreportProcessing += new SubreportProcessingEventHandler(PurchaseReturndetail);
                    report.Refresh();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void PurchaseReturndetail(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetPurchaseReturnDetail", pr[0].PurchaseReturnDt));
        }

        private void GetReportReady()
        {
            if (ReportViewer1 == null)
            {
                ReportViewer1 = new Microsoft.Reporting.WebForms.ReportViewer();
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
            }

            ReportViewer1.LocalReport.DataSources.Clear();
        }

        private void prepareReport(LocalReport report, string reportFilePath, ReportDataSource data)
        {
            report.ReportPath = reportFilePath;
            report.DataSources.Add(data);
        }

        private void GenerateFile(string fileType, LocalReport report, string fileName)
        {
            Byte[] bytesPdf = report.Render(fileType);
            if (bytesPdf != null)
            {
                switch (fileType)
                {
                    case "PDF":
                        System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                            "attachment;filename=" + fileName + ".pdf");
                        System.Web.HttpContext.Current.Response.ContentType = "application/octectstream";
                        break;
                    case "Excel":
                        System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                            "attachment;filename=" + fileName + ".xlsx");
                        System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                        break;
                    default:
                        System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                            "attachment;filename=" + fileName + ".doc");
                        System.Web.HttpContext.Current.Response.ContentType =
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                }
            }

            System.Web.HttpContext.Current.Response.BinaryWrite(bytesPdf);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }

        private async void PendingGrnItemWise(int storeid)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string reportPath = Server.MapPath("../Reports/Store/PendingItemGRNWise.rdlc");
            ReportViewer1.LocalReport.ReportPath = reportPath;
            List<GRNModel> grn = null;
            string _url = BISERP.Common.Constants.WebAPIAddress + "/grn/PendingGrnItemWise/" + storeid +
                          Common.Constants.JsonTypeResult;
            // var response = _client.GetAsync(_url).Result;
            var response = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(_client, _url, CancellationToken.None);

            //string _url = url + "/grn/grndetails/" + strfromdate + "/" + strtodate + "/" + storeid + "/" + suppid + "/" + grnid + Common.Constants.JsonTypeResult;
            //var response = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(_client, _url, CancellationToken.None);

            //if (response.IsSuccessStatusCode)
            //{
            //    driver = response.Content.ReadAsAsync<List<GRNModel>>().Result;
            if (response != null)
            {
                grn = new List<GRNModel>();
                grn.AddRange(response.ToList());

                var abc = from m in grn
                          from mdt in m.GRNDetails
                          select new
                          {
                              Id = m.ID,
                              m.StoreId,
                              m.PoID,
                              m.SupplierID,
                              m.GRNNo,
                              m.StoreName,
                              m.SupplierName,
                              m.PONo,
                              m.strPODate,
                              mdt.ItemName,
                              mdt.ItemID,
                              mdt.ItemCode,
                              mdt.Qty,
                              mdt.Rate,
                              mdt.DescriptiveName,
                              mdt.PopendingQty
                          };
                //var result = model.ListToModel(driver);
                _rds = new ReportDataSource();
                _rds.Name = "dsGRNDetails";
                _rds.Value = abc.ToList();
                ReportViewer1.LocalReport.DataSources.Add(_rds);

                ReportViewer1.LocalReport.Refresh();
            }
        }
        private List<PurchaseIndentModel> _purchaseIndentList;
        public async void PurchaseIndentById(int indentId, string pIType)
        {
            string ReportPath = Server.MapPath("../Reports/Purchase/PurchaseIndentRpt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/pindent/getpurchaseindentbyid/" + indentId.ToString() +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<PurchaseIndentModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                _purchaseIndentList = new List<PurchaseIndentModel>();
                _purchaseIndentList.Add(response);
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsPurchaseIndent";
            _rds.Value = _purchaseIndentList;
            _rparams = new ReportParameter[1];
            AddReportParameter(0, "pIType", pIType);
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(IndentDetails);
            ReportViewer1.LocalReport.Refresh();

        }

        void IndentDetails(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsPurchaseIndentDt", _purchaseIndentList[0].IndentDetails));
        }

        private List<RequestForQuoteModel> _RFQList;
        public async void RFQById(int indentId, string RFQType)
        {
            string ReportPath = Server.MapPath("../Reports/Purchase/RFQRpt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/RequestForQuote/GetRequestForQuoteById/" + indentId.ToString() +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<RequestForQuoteModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                _RFQList = new List<RequestForQuoteModel>();
                _RFQList.Add(response);
            }

            _rds = new ReportDataSource();
            _rds.Name = "dsRequestForQuote";
            _rds.Value = _RFQList;
            _rparams = new ReportParameter[1];
            AddReportParameter(0, "RFQType", RFQType);
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(RFQDetail);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(RFQDeliveryterm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(RFQPaymentterm);
            ReportViewer1.LocalReport.Refresh();
        }

        void RFQDetail(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsRFQDt", _RFQList[0].IndentDetails));
        }

        void RFQDeliveryterm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsRFQDeliveryTerm", _RFQList[0].RFQDeliveryTerms));
        }

        void RFQPaymentterm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsRFQPaymentTerm", _RFQList[0].RFQPaymenterms));
        }

        private List<WorkOrderModel> _WOList;
        public async void WorkOrderById(int indentId, string WOType)
        {
            string ReportPath = Server.MapPath("../Reports/Purchase/WorkOrderRpt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/WorkOrder/GetWorkOrderById/" + indentId.ToString() +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<WorkOrderModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                _WOList = new List<WorkOrderModel>();
                _WOList.Add(response);
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsWorkOrder";
            _rds.Value = _WOList;
            _rparams = new ReportParameter[1];
            AddReportParameter(0, "WOType", WOType);
            ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WorkOrderDetail);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WODeliveryTerm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WOPaymentTerm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WOOthersTerm);
            ReportViewer1.LocalReport.Refresh();
        }

        void WorkOrderDetail(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsWorkOrderDt", _WOList[0].IndentDetails));
        }

        void WODeliveryTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsWODeliveryTerm", _WOList[0].WODeliveryTerms));
        }

        void WOPaymentTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsWOPaymentTerm", _WOList[0].WOPaymenterms));
        }
        void WOOthersTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsWOOtherTerm", _WOList[0].WOOtherTerms));
        }


        private List<PackingListModel> _PKList;
        public async void PackingListById(int Id)
        {
            string ReportPath = Server.MapPath("../Reports/PackingList/PackingListReport.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/PackingList/getPackingListById/" + Id.ToString() +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<PackingListModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                _PKList = new List<PackingListModel>();
                _PKList.Add(response);
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsPackingList";
            _rds.Value = _PKList;
            //_rparams = new ReportParameter[1];
            //AddReportParameter(0, "PLType", PLType);
            //ReportViewer1.LocalReport.SetParameters(_rparams);
            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PackingListDetails);
            ReportViewer1.LocalReport.Refresh();
        }

        void PackingListDetails(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsPackingListDetails", _PKList[0].PackingListDetails));
        }

        private List<WorkOrderRptModel> _workOrderRpt;
        public async void WorkOrderRptById(int orderBookId)
        {
            string ReportPath = Server.MapPath("../Reports/WorkOrder/workOrderRpt.rdlc");
            ReportViewer1.LocalReport.ReportPath = ReportPath;
            string _url = url + "/sm_Reports/getWorkOrderRpt/" + orderBookId.ToString() +
                          Common.Constants.JsonTypeResult;
            var response =
                await Common.AsyncWebCalls.GetAsync<WorkOrderRptModel>(_client, _url, CancellationToken.None);
            if (response != null)
            {
                _workOrderRpt = new List<WorkOrderRptModel>();
                _workOrderRpt.Add(response);
            }
            _rds = new ReportDataSource();
            _rds.Name = "dsWorkOrderRpt";
            _rds.Value = _workOrderRpt;

            ReportViewer1.LocalReport.DataSources.Add(_rds);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WORptPaymentTerm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WORptDeliveryTerm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WORptOtherTerm);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(WORptBasisTerm);
            ReportViewer1.LocalReport.Refresh();
        }

        void WORptPaymentTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsPaymentTerm", _workOrderRpt[0].PaymentTermList));
        }
        void WORptDeliveryTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsDeliveryTerm", _workOrderRpt[0].DeliveryTermList));
        }
        void WORptOtherTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsOtherTerm", _workOrderRpt[0].OtherTermList));
        }
        void WORptBasisTerm(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsBasisTerm", _workOrderRpt[0].BasisTermList));
        }
    }
}