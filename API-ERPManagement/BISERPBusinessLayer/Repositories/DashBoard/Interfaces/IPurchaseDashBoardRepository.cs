using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.DashBoard.Interfaces
{
    public interface IPurchaseDashBoardRepository
    {
        List<ChartModelLastFewMonthTotalParent> SupplierCostLastSixMonth();
        List<DashboardRequestCounts> GetPurchaseDashBoardRequestCounts_Quaterly(DateTime? fromdate, DateTime? todate);
        List<DashboardRequestCounts> GetDashBoardPurchaseIndentRequestCount(DateTime? fromDt, DateTime? toDt);
        List<DashboardRequestCounts> GetDashBoardPurchaseOrderRequestCount(DateTime? fromDt, DateTime? toDt);
    }
}
