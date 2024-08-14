using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class CashDepositeEntity
    {
        public int BranchId { get; set; }
        public int DepositeID { get; set; }

        public DateTime? Date { get; set; }

        public bool ByCash { get; set; }

        public string ChequeNumber { get; set; }

        public string ChequeBank { get; set; }

        public double? Amount { get; set; }

        public string Remarks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; } 

     
    }
}
