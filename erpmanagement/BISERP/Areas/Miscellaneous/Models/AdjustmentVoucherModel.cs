using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class AdjustmentVoucherModel
    {
        public int VoucherId { get; set; }
        public int DiscrepancyId { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<DateTime> VoucherDate { get; set; }
        public string strVoucherDate { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string StoreName { get; set; }
        public string Remarks { get; set; }
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
        public List<AdjustmentVoucherDtModel> Voucherdetails { get; set; }
        internal object ListToModel(List<AdjustmentVoucherModel> AdjustmentVoucher)
        {
            var result = from m in AdjustmentVoucher
                         from mdt in m.Voucherdetails
                         select new
                         {
                             m.VoucherId,
                             m.VoucherNo,
                             m.VoucherDate,
                             m.StoreId,
                             m.StoreName,
                             m.Remarks,
                             mdt.VoucherDetailId,
                             mdt.ItemName,
                             mdt.ItemID,
                             mdt.BatchName,
                             mdt.PhysicalQty,
                             mdt.ShortQuantity,
                             mdt.SurPlusQuantity,
                             mdt.Reason
                         };
            return result;
        }
    }
}