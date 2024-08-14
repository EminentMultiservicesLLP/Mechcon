using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class RoadTaxModel
    {
        public int RoadTaxId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }

        [Display(Name="Tax No")]
        public string TaxNo { get; set; }
        public double Amount { get; set; }

        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        public string strIssueDate { get; set; }
        public string strExpiryDate { get; set; }

        [Display(Name = "Reminder Days")]
        public int ReminderDays { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
    }
}