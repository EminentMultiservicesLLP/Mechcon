using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Area.Branch.Models
{
    public class MaterialReturnGuardModel
    {
        public int ReturnId { get; set; }
        public int IssueId { get; set; }

        [Display(Name = "Issue No")]
        public string IssueNo { get; set; }

        [Display(Name = "Return No")]
        public string ReturnNo { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Employee")]
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        [Display(Name = "Tot. Ret. Amount")]
        public double NetAmount { get; set; }
        public double BalanceAmount { get; set; }
        public double ReceivedAmount { get; set; }
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
        public List<MaterialReturnGuardDtModel> MaterialReturnGuardDt { get; set; }
    }
}