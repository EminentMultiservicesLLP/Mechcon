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
}