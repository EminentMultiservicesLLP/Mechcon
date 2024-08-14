using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Accounts.Models
{
    public class SupplierBillModel
    {
        public int BillId { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string strBillDate { get; set; }
        public int GrnId { get; set; }
        public int SuppilerId { get; set; }
        public string SupplierName { get; set; }
        public Nullable<double> BillAmount { get; set; }
        public Nullable<double> PartyPayable { get; set; }
        public Nullable<double> UpdatedMacName { get; set; }
        public Nullable<double> UpdatedMacID { get; set; }
        public Nullable<double> UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> state { get; set; }
    }
}