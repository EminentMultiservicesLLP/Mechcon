using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class InsuranceModel
    {
        public int InsuranceId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }

        [Display(Name = "Policy No")]
        public string PolicyNo { get; set; }

        [Display(Name = "Company Name")]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        [Display(Name = "Issue Date")]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        public string strIssueDate { get; set; }
        public string strExpiryDate { get; set; }
        public double Amount { get; set; }

        [Display(Name = "Reminder Days")]
        public int ReminderDays { get; set; }
        public string Agent { get; set; }
        public string CovertNote { get; set; }
        public int? InsuranceType { get; set; }
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
        public bool Deactive { get; set; }
    }
}