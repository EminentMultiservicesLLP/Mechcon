using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.DashBoards.Models
{
    public class ChartModel
    {
        public double NoOfRequest { get; set; }
        public string BranchName { get; set; }
        public double SumOfAmount { get; set; }
    }

    public class ChartModelLastFewMonthTotalParent
    {
        public string EntityName { get; set; }
        public List<ChartModelLastFewMonthTotalChild> MonthNameValues { get; set; }
    }
    public class ChartModelLastFewMonthTotalChild
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class BarChartPopulateParent
    {
        public List<string> Legends;
        public List<string> Labels;
        public List<double[]> LabelValues;

    }

    public class DashboardRequestCounts
    {
        private string totalAmount = "0";
        public string RequestType { get; set; }
        public string RequestCount { get; set; }
        public string Icon { get; set; }
        public string MenuId { get; set; }
        public string TotalAmount
        {

            //string.Format("Fixed formatted: {0:n0}", Convert.ToInt32(totalAmount)); 
            get
            {
                return string.Format("Rs.{0:n0}", Convert.ToInt32(totalAmount));
                //string.Format("{0:C}", Convert.ToInt32(totalAmount), CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            set { totalAmount = value; }
        }
    }
}