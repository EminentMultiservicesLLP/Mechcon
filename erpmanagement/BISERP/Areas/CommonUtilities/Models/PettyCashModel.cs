using System;

namespace BISERP.Areas.CommonUtilities.Models
{
    public class PettyCashModel
    {
        public int TransactionId { get; set; }
        
        public int BranchId { get; set; }
        
        public string TransactionDate { get; set; }

        public string PayeeName { get; set; }

        public bool IsCashTransaction { get; set; }

        public bool IsDeposite { get; set; }

        public string ChequeNumber { get; set; }

        public string ChequeBank { get; set; }

        public Double? TransactionAmount { get; set; }

        public string Remarks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int UpadtedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string strTransactionDate { get; set; } 
    }
}