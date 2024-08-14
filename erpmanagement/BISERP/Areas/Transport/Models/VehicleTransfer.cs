using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class VehicleTransfer
    {
        [Display(Name = "Vehicle No")]
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public bool TransferSold { get; set; }

        [Display(Name="Transfer Date")]
        public DateTime TransferDate { get; set; }

        [Display(Name = "Sold Date")]
        public DateTime SoldDate { get; set; }

        [Display(Name = "Existing Branch")]
        public int OldBranchId { get; set; }
        public string OldBranch { get; set; }

        [Display(Name = "Transfer Branch")]
        public int NewBranchId { get; set; }
        public int NewBranch { get; set; }

        [Display(Name = "EMI Completed")]
        public int EMICompleted { get; set; }

        [Display(Name = "EMI Pending")]
        public int EMIPending { get; set; }

        [Display(Name = "Transfer Reason")]
        public string TransferReason { get; set; }
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