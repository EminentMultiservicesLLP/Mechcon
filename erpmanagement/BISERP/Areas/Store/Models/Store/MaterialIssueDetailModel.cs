using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class MaterialIssueDetailModel
    {
        public int IssueDetailsId { get; set; }
        public int IndentDetailId { get; set; }
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string UnitName { get; set; }
        public int? Batchid { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }
        public Nullable<double> IssuedQuantity
        {
            get;
            set;
        }

        public Nullable<double> AuthorisedQuantity { get; set; }
        public Nullable<double> Item_Stock { get; set; }
        public Nullable<double> Rate { get; set; }
        public string Status { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> Authorised { get; set; }
        public Nullable<bool> state { get; set; }
        public Nullable<double> MRP { get; set; }

        public Nullable<float> BalIndentQtyForIssue{ get; set; }

        public string Strauthorized { get; set; }
    }
}