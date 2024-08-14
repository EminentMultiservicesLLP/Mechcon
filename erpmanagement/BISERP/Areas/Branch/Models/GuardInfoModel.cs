using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Branch.Models
{
    public class GuardInfoModel
    {
        public int GuardId { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Fisrt Name")]
        public string EmpFName { get; set; }

        [Display(Name = "Middle Name")]
        public string EmpMName { get; set; }

        [Display(Name = "Last Name")]
        public string EmpLName { get; set; }

        [Display(Name = "Age")]
        public double age { get; set; }

        [Display(Name = "Gender")]
        public int Gender { get; set; }
        public string Address { get; set; }

        [Display(Name = "ContactNo")]
        public string ContactNo { get; set; }

        [Display(Name = "BloodGroup")]
        public string Bloodgroup { get; set; }
        [Display(Name = "DateOfBirth")]
        public Nullable<DateTime> DateOfBirth { get; set; }
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
        public GuardDetailsModel GuardDt { get; set; }
        public string TicketCode { get; set; }
        [Display(Name = "Gender")]
        public string strGender { get; set; }
    }
}