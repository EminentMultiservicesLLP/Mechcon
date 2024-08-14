using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Accounts.Models
{
    public class SupplierBillingModel
    {
        public int SupplierbillId { get; set; }
        public string SupplierbillNo { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime SupplierBillDate { get; set; }
        public string StrSupplierBillDate { get; set; }
        public double TotalRecieved { get; set; }
        public double GrossAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double NetAmt { get; set; }
        public int POId { get; set; }
        public int GRNId { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public double PoAmount { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public bool State { get; set; }

        public List<SupplierBillingdtModel> SupplierBillingdt { get; set; }
    }
    public class SupplierBillingdtModel
    {
        public string SupplierName { get; set; }
        public int SupplierbillId { get; set; }
        public int SupplierbilldtlId { get; set; }
        public string SupplierbillNo { get; set; }
        public int GRNId { get; set; }
        public int ID { get; set; }
        public int PoID { get; set; }
        public string GRNNo { get; set; }
        public string strGRNDate { get; set; }
        public double TotalAmount { get; set; }
        public double PayingAmount { get; set; }
        public double PaidAmount { get; set; }
        public double DiscountAmount { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string DiscountReason { get; set; }
        public string ChequeNo { get; set; }
        public string StrSupplierBillDate { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public bool State { get; set; }
        public string BillStatus { get; set; }
        public string SupGSTIN { get; set; }
        public string InvoiceNo { get; set; }
    }
}