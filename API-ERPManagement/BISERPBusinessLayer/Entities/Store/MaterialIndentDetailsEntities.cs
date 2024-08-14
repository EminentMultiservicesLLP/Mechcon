using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class MaterialIndentDetailsEntities
    {
        public int IndentDetails_Id { get; set; }
        public int Indent_Id { get; set; }
        public Nullable<int> Item_Id { get; set; }
        public Nullable<double> User_Quantity { get; set; }
        public Nullable<double> Authorised_Curr_Quantity { get; set; }
        public Nullable<double> Authorised_Quantity { get; set; }
        public Nullable<double> Item_Stock { get; set; }
        public string ItemAddedBy { get; set; }
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
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int? BatchId { get; set; }
        public string BatchName { get; set; }
        public string DispensingUnit { get; set; }
        public string ExpiryDate { get; set; }
        public string PackSizeName { get; set; }
        public Nullable<bool> state { get; set; }
        public Nullable<bool> MICancelled { get; set; }
        public Nullable<int> MICancelledby { get; set; }
        public string MICancelledbyUser { get; set; }
        public Nullable<System.DateTime> MICancelledOn { get; set; }
        public string MICancelReason { get; set; }

        public double? MRP { get; set; }
        public string DescriptiveName { get; set; }
        public int PackSizeID { get; set; }
        public string HSNCode { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
