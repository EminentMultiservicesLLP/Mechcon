using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class DeactivationDetail
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int AssetId { get; set; }
        public int RequestId { get; set; }
        public int DeactiveReason { get; set; }
        public int SupplierId { get; set; }
        public string Receiver { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public double Amount { get; set; }
        public Nullable<DateTime> LogDate { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
        public int IsApporved { get; set; }
        public int APPROVE_AUTH_ID { get; set; }
        public string  APPROVAL_REMARK { get; set; }
        public int ISCOMPLETED { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string REMARK { get; set; }
        public bool Deactive { get; set; }
        public string ApprovedBy { get; set; }
        public string NewSupplier { get; set; }
        public string AssetCode { get; set; }
        public string ItemName { get; set; }
        internal object ListToModel(List<DeactivationDetail> DeactivationDetail)
        {
            var result = from m in DeactivationDetail
                      
                         select new
                         {
                             m.Id,
                             m.AssetId,
                             m.AssetCode,
                             m.ItemName,
                             m.DeactiveReason,
                             m.ApprovedBy,
                             m.Receiver,
                             m.Amount,
                             m.Address,
                             m.ContactPerson,
                             m.NewSupplier,
                           
                         };
            return result;
        }
    }
}