using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class CashWithdrawalEntity
    {
        public int BranchId { get; set; }
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
