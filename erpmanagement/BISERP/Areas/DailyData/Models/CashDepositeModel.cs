using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.DailyData.Models
{
    public class CashDepositeModel
    {
        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }
        public int DepositeID { get; set; }

        public DateTime? Date { get; set; }

        public bool ByCash { get; set; }

        public string ChequeNumber { get; set; }

        public string ChequeBank { get; set; }

        public double? Amount { get; set; }

        public string Remarks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}