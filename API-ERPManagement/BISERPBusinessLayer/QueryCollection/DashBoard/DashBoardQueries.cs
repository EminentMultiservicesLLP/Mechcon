using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.DashBoard
{
    public class DashBoardQueries
    {
        public const string GetSupplierCostLastSixMonth = "dbsp_SupplierCostChart_LastSixMonth";
        public const string GetPurchaseDashboardRequestCountsQuaterly = "dbsp_DSBD_GetPurchaseRequestCounts_Quaterly";
        public const string GetDashboardPurchaseIndentRequestCount = "dbsp_DSBD_GetPurchaseIndentRequestCount";
        public const string GetDashboardPurchaseOrderRequestCount = "dbsp_DSBD_GetPurchaseOrderRequestCount";

        public const string GetDashBoardCountSummury = "dbsp_DSBD_GetDashBoardCountSummury";


        public const string GetMonthlySale = "dbsp_dashboard_GetMonthlySale";
        public const string GetMonthlyPurchase = "dbsp_dashboard_GetMonthlyPurchase";
        public const string GetMonthlySaleVsTarget = "dbsp_dashboard_GetMonthlySaleVsTarget";
        public const string GetMonthlySaleVsExpense = "dbsp_dashboard_GetMonthlySaleVsExpense";
        public const string GetMonthlyResourcewiseTarget = "dbsp_dashboard_GetMonthlyResourcewiseTarget";
        public const string GetProjectStatusDataYearly = "dbsp_dashboard_GetProjectStatusDataYearly";
    }
}
