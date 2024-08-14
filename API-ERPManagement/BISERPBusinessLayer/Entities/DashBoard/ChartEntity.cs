using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ChartEntity
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

    public class DashboardRequestCounts
    {
        public string RequestType { get; set; }
        public string RequestCount { get; set; }
        public string Icon { get; set; }
        public string MenuId { get; set; }
        public string TotalAmount { get; set; }
    }
}
