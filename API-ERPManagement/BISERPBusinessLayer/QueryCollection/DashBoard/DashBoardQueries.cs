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
    }
}
