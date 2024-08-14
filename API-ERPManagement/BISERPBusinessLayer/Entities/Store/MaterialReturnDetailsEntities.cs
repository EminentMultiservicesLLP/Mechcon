using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class MaterialReturnDetailsEntities
    {
        public int ReturnDtlID { get; set; }
        public Nullable<int> ReturnID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Reason { get; set; }
        public Nullable<double> StockQty { get; set; }
        public Nullable<int> totalReturnQty { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
      
        public string Batch { get; set; }
        public string strExpiryDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<double> AccptanceQuantity { get; set; }
        public Nullable<double> IssueQty { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> Deactive { get; set; }
    }
}
