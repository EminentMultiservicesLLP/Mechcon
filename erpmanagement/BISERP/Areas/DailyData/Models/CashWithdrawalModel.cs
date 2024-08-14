using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.DailyData.Models
{
    public class CashWithdrawalModel
    {
        public int WithdrawalID { get; set; }

        public DateTime? Date { get; set; }

        public string PayeeName { get; set; }

        public bool ByCash { get; set; }

        public string ChequeNumber { get; set; }

        public string ChequeBank { get; set; }

        public Double? Amount { get; set; }

        public string Remarks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int UpadtedBy { get; set; }

        public DateTime? UpdatedDate { get; set; } 
    }
}