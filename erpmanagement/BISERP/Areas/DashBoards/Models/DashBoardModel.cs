using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.DashBoards.Models
{
    public class DashBoardModel
    {
        public int TotalItem { get; set; }
        public int ActiveItem { get; set; }
        public int LowStockItem { get; set; }
        public double TransitItemQuantity { get; set; }
    }
	
	public class StoreDSBDStockSummaryEntity
    {
        public string Branch { get; set; }
        public double Accesorries { get; set; }
        public double HousseKeeping { get; set; }
        public double Linen { get; set; }
        public double Stationary { get; set; }
        public double Uniform { get; set; }
        public double Others { get; set; }
        public double TotalCost { get; set; }
    }

    public class StoreDSBDGuardIssueSummaryEntity
    {
        public string Branch { get; set; }
        public int Renewal { get; set; }
        public int FreshIssue { get; set; }
        public double NetAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double DiscountGiven { get; set; }
        public double GrossAmount { get; set; }
    }
    public class DashBoardCountSummuryModel
    {
        public string Type { get; set; }
        public int? Count { get; set; }
        public decimal? Value { get; set; }
    }
    public class Dashboard_BarTrendModel
    {
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public decimal? TotalSale { get; set; }
        public decimal? TotalPurchase { get; set; }
        public decimal? TotalTarget { get; set; }
        public decimal? TotalExpense { get; set; }
    }
    public class Dashboard_MultiChartBarTrendModel
    {
        public string MonthName { get; set; }
        public string ResourceName { get; set; }
        public decimal? TargetValue { get; set; }
        public decimal? WonValue { get; set; }
    }

    public class Dashboard_ColumnChartModel
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string PorjectName { get; set; }
        public double? ProjectBudget { get; set; }
        public double? UtilizedCost { get; set; }
        public double? Balance { get; set; }
        public int RowCount { get; set; }
    }
}