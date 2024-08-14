using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Branch.Models
{
    public class GuardDetailsModel
    {
        public int Id { get; set; }
        public int GuardId { get; set; }
        public int BranchId { get; set; }
        public int EmpId { get; set; }

        [Display(Name = "Reference Name1")]
        public string Reference1 { get; set; }

        [Display(Name = "Reference Name2")]
        public string Reference2 { get; set; }

        [Display(Name = "Reference Name3")]
        public string Reference3 { get; set; }

        [Display(Name = "Reference Name4")]
        public string Reference4 { get; set; }

        [Display(Name = "PoliceVerification Completed")]
        public bool PoliceVerification { get; set; }
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