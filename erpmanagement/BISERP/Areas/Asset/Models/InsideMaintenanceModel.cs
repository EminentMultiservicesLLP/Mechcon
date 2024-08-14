using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class InsideMaintenanceModel
    {
        public int InHouseId { get; set; }
        public string Code { get; set; }
        public int RequisitionId { get; set; }
        public string TimeSpent { get; set; }

        [Display(Name = "Completed Date")]
        public Nullable<DateTime> CompletedDate { get; set; }

        [Display(Name = "Transfer Date")]
        public Nullable<DateTime> TransferDate { get; set; }

        [Display(Name = "Time")]
        public Nullable<DateTime> CompletedTime { get; set; }
        public string Feedback { get; set; }
        public string TransferReason { get; set; }
        public Nullable<DateTime> LogDate { get; set; }

        [Display(Name="Completed")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Transfer")]
        public bool IsTransfer { get; set; }
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

        public TechnicianEntryModel techentry { get; set; }
        public List<MaterialConsumptionModel> consumption { get; set; }

        public string RequisitionNo { get; set; }
        public string strRequestDate { get; set; }
        public string RequestedBy { get; set; }
        public string Priority { get; set; }
        public string strTransferDate { get; set; }
        public string strCompletedDate { get; set; }
        public string strCompletedTime { get; set; }
        public int VendorId { get; set; }
    }
}