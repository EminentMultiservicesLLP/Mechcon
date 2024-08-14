using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Areas.DashBoards.Models
{
    public class StockRegisterModel
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double OpeningBalance { get; set; }
        public double QtyReceived { get; set; }
        public double QtyIssued { get; set; }
        public double BalanceQty { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public string DocType { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public string ReceiptNo { get; set; }
        public string IssueNo { get; set; }        
        public string UnitName { get; set; }       
        public string IssueToStore { get; set; }        
        public string BatchName { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<double> MRP { get; set; }
        public Nullable<int> ItemTypeID { get; set; }
        public string ItemType { get; set; }

        
    }
}