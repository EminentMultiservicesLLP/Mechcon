using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class WarrantyMaintenanceModel
    {
        public int WarrantyId { get; set; }

        [Display(Name="War. Order No.")]
        public string Code { get; set; }
        public int RequisitionId { get; set; }

        [Display(Name = "Maintenance Date")]
        public DateTime MaintenanceDate { get; set; }

        [Display(Name = "Maintenance Time")]
        public DateTime MaintenanceTime { get; set; }

        [Display(Name = "Down Period")]
        public string DownTime { get; set; }
        public string Days { get; set; }

        [Display(Name = "Maintenance Cost")]
        public string MaintenanceCost { get; set; }

        [Display(Name = "Actual Fault")]
        public string ActualFault { get; set; }

        [Display(Name = "Done By")]
        public int DoneBy { get; set; }

        [Display(Name = "Maintenance Category")]
        public int MaintenanceType { get; set; }
        public Nullable<DateTime> LogDate { get; set; }
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
        public bool Deactive { get; set; }

        public List<WarrantySparePartsModel> spareparts { get; set; }
    }
}