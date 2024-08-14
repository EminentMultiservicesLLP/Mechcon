using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Billing
{
   public class VendorBillingEntity
    {
        public int VendorbillId { get; set; }
        public string VendorbillNo { get; set; }
        public int VendorId { get; set; }
        public DateTime VendorBillDate { get; set; }
        public double GrossAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double NetAmt { get; set; }
        public int GRNId { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public List<VendorBillingDtlEntity> VendorBillingdt { get; set; }
    }
    public class VendorBillingDtlEntity
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int VendorbillId { get; set; }
        public int VendorbilldtlId { get; set; }
        public string VendorbillNo { get; set; }
        public DateTime VendorBillDate { get; set; }
        public string StrVendorBillDate { get; set; }
        public int ID { get; set; }
        public string GRNNo { get; set; }
        public string strGRNDate { get; set; }
        public double TotalAmount { get; set; }
        public double PayingAmount { get; set; }
        public double PaidAmount { get; set; }
        public double DiscountAmount { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string DiscountReason { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
    }
}
