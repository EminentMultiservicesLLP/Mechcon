using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.SM_DashBoard.Models
{
    public class BarChartModel
    {
        public int MonthNo { get; set; }
        public string MonthName { get; set; }
        public int TotalCount { get; set; }
        public decimal? TotalValue { get; set; }
        //Location Wise
        public int DomesticCount { get; set; }
        public int ExportCount { get; set; }
        public int SEZCount { get; set; }
        public decimal? DomesticValue { get; set; }
        public decimal? ExportValue { get; set; }
        public decimal? SEZValue { get; set; }
        //Product Wise
        public int SystemsCount { get; set; }
        public int ProductsCount { get; set; }
        public int SparesCount { get; set; }
        public int ServicesCount { get; set; }
        public decimal? SystemsValue { get; set; }
        public decimal? ProductsValue { get; set; }
        public decimal? SparesValue { get; set; }
        public decimal? ServicesValue { get; set; }
    }
    public class PieChartModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public decimal? TotalValue { get; set; }
        //Location Wise
        public int DomesticCount { get; set; }
        public int ExportCount { get; set; }
        public int SEZCount { get; set; }
        public decimal? DomesticValue { get; set; }
        public decimal? ExportValue { get; set; }
        public decimal? SEZValue { get; set; }
        //Product Wise
        public int SystemsCount { get; set; }
        public int ProductsCount { get; set; }
        public int SparesCount { get; set; }
        public int ServicesCount { get; set; }
        public decimal? SystemsValue { get; set; }
        public decimal? ProductsValue { get; set; }
        public decimal? SparesValue { get; set; }
        public decimal? ServicesValue { get; set; }
    }
}