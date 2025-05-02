using BISERPBusinessLayer.Entities.DashBoard;
using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IDashBoardRepository
    {
        StoreDashBoardEntity GetItemSummary();
		IEnumerable<StoreDSBDStockSummaryEntity> GetStoreDSBDStockSummary(int byValue = 1);
        IEnumerable<StoreDSBDGuardIssueSummaryEntity> GetStoreDSBDGuardIssueSummary();
        IEnumerable<DashBoardCountSummuryModel> GetDashBoardCountSummury(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySale(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlyPurchase(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySaleVsExpense(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySaleVsTarget(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_MultiChartBarTrendModel> DashboardGetMonthlyResourcewiseTarget(int? FinancialYearID, int? ProjectID);
        IEnumerable<Dashboard_ColumnChartModel> DashboardGetProjectStatusDataYearly(int? FinancialYearID, int? ProjectID, int? MaxId);
    }
}
