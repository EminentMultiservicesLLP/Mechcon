using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Accounts.Models
{
    public class SupplierBillPaymentModel
    {
        public int PaymentId { get; set; }
        public int BillId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public int SupplierId { get; set; }
        public double BillAmount { get; set; }
        public Nullable<double> PaidAmount { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}