using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class VehicleTransferModel
    {
        public int TransferId { get; set; }

        [Display(Name = "Vehicle No")]
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public bool TransferSold { get; set; }

        [Display(Name="Transfer Date")]
        public DateTime TransferDate { get; set; }

        [Display(Name = "Sold Date")]
        public DateTime SoldDate { get; set; }
        public string strTransferDate { get; set; }
        public string strSoldDate { get; set; }

        [Display(Name = "Sold Amount")]
        public Nullable<double> SoldAmount { get; set; }

        [Display(Name = "Existing Branch")]
        public int OldBranchId { get; set; }
        public string OldBranch { get; set; }

        [Display(Name = "Transfer Branch")]
        public int NewBranchId { get; set; }
        public string NewBranch { get; set; }

        [Display(Name = "EMI Completed")]
        public int EMICompleted { get; set; }

        [Display(Name = "EMI Pending")]
        public int EMIPending { get; set; }

        [Display(Name = "Transfer Reason")]
        public string TransferReason { get; set; }

        [Display(Name = "Sold Reason")]
        public string SoldReason { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
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

        public Nullable<bool> Authorised { get; set; }
        public Nullable<int> AuthorisedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        internal object ListToModel(List<VehicleTransferModel> Transfer)
        {
            var result = from m in Transfer
                         select new
                         {
                             TransferId = m.TransferId,
                             m.VehicleId,
                             m.VehicleNo,
                             m.TransferSold,
                             m.TransferReason,
                             m.TransferDate,
                             m.SoldDate,
                             m.OldBranchId,
                             m.OldBranch,
                             m.NewBranchId,
                             m.NewBranch,
                             m.EMIPending,
                             m.EMICompleted,
                             m.CancelledDate,
                             m.Cancelled,
                             m.Authorised
                         };
            return result;
        }
    }
}