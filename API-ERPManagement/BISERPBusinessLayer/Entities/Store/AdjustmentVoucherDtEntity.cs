using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class AdjustmentVoucherDtEntity
    {
        public int VoucherDetailId { get; set; }
        public int DiscrepancyDetailId { get; set; }
        public Nullable<int> VoucherId { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> PhysicalQty { get; set; }
        public Nullable<double> ShortQuantity { get; set; }
        public Nullable<double> SurPlusQuantity { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<int> cancelled { get; set; }
        public Nullable<int> cancelledBy { get; set; }
        public Nullable<System.DateTime> cancelledOn { get; set; }
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
    }
}
