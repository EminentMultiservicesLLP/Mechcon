using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.DashBoard
{
   
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
