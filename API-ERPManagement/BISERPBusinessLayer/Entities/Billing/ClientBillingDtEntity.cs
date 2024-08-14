using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Entities.Billing
{
    public class ClientBillingDtEntity
    {
        public int ClientBillId { get; set; }
        public string BillNo { get; set; }
        public string ClientName { get; set; }
        public int ClientBillDtlId { get; set; }
        public int ItemId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime BillDate { get; set; }
        public string StrBillDate { get; set; }
        public string ItemName { get; set; }
        public double Rate { get; set; }
        public double Qty { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public double TaxAmt { get; set; }
        public double Amount { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public double UGST { get; set; }
        public double CGST { get; set; }
        public double SGSTPer { get; set; }
        public double IGSTPer { get; set; }
        public double UGSTPer { get; set; }
        public double CGSTPer { get; set; }
        public double TaxRates { get; set; }
        public string Taxes { get; set; }
        public double RoundOffAmt { get; set; }
        public List<PaymentTermsMasterEntities> PaymentTerm { get; set; }
        public string UnitName { get; set; }
        public string HSNCode { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public string InvoiceType { get; set; }

    }
    public class PayModeEntity
    {
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }

    }

}
