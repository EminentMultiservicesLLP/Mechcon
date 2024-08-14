using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public partial class MaterialIssueDetailsEntity
    {
        public int IssueDetailsId { get; set; }
        public int IssueId { get; set; }
        public int IndentDetailId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string UnitName { get; set; }

        public Nullable<double> IssuedQuantity { get; set; }
        public Nullable<double> Authorised_Curr_Quantity { get; set; }
        public Nullable<double> AuthorisedQuantity { get; set; }
        public Nullable<double> Item_Stock { get; set; }
        public string ItemAddedBy { get; set; }
        public Nullable<double> Rate { get; set; }
        public int? BatchId { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Authorised { get; set; }
        public Nullable<double> PendingQty { get; set; }
        public Nullable<int> indCancelledby { get; set; }
        public Nullable<System.DateTime> indCancelledOn { get; set; }
        public Nullable<int> indCancelReason { get; set; }
        public Nullable<bool> indCancelled { get; set; }
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
        public Nullable<double> MRP { get; set; }
        public Nullable<float> BalIndentQtyForIssue { get; set; }
        public double Amount { get; set; }
    }
}
