using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Branch.Models
{
    public class MomentOrderModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Batch")]
        public int BatchId { get; set; }

        [Display(Name = "Training Centre")]
        public int TrainingCentreId { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Employee Type")]
        public int EmployeeType { get; set; }
        public long EmpId { get; set; }

        [Display(Name = "Temporary Name")]
        public int TempEmpId { get; set; }

        [Display(Name = "Medical Checkup")]
        public bool Medical { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }
        [Display(Name = "Name")]
        public string EmpName { get; set; }

        [Display(Name = "Ticket Code")]
        public string TicketCode { get; set; }

        [Display(Name = "Order Date")]
        public Nullable<DateTime> TrainingDate { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }
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
        public string Message { get; set; }
        public string Remarks { get; set; }

        public bool Accepted { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Waist { get; set; }
        public string Fitness { get; set; }
        public string TrainingCentre { get; set; }
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
        public string Temporaryname { get; set; }
        public string StrTrainingDate { get; set; }
        public string BatchName { get; set; }
        public string StrEmployeeType { get; set; }
        public bool Status { get; set; }
        public string StatupCode { get; set; }
        public double BatchQty { get; set; }
    }
}