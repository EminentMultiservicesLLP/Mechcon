using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.VendorProcess.Models
{
    public class VendorMaterialIssueDtModel
    {
        public int IssueDetailsId { get; set; }
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> MRP { get; set; }
        public Nullable<double> TotalAmt { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }
        public string UnitName { get; set; }
        public Nullable<double> IssuedQuantity { get; set; }
        public Nullable<double> AuthorisedQuantity { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedON { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> Authorised { get; set; }
        public int AuthorisedBy { get; set; }
        public DateTime AuthorisedOn { get; set; }
        public int CancelledBy { get; set; }
        public DateTime CancelledOn { get; set; }
        public int CancelReason { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<bool> state { get; set; }
        public Nullable<double> Item_Stock { get; set; }
        public string HSNCode { get; set; }
    }
}