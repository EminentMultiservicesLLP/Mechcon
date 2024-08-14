using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Accounts.Models
{
    public class ClientRecieptModel
    {
        public int ClientBillId { get; set; }
        public int RecieptId { get; set; }
        public string BillNo { get; set; }
        public string RecieptNo { get; set; }
        public double NetAmt { get; set; }
        public double RecieptAmount { get; set; }
        public DateTime RecieptDate { get; set; }
        public string StrRecieptDate { get; set; }
        public string Notes { get; set; }
        public double TotalRecieved { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public List<ClientRecieptDtModel> ClientRecieptDt { get; set; }
    }
    public class ClientRecieptDtModel
    {
        public int RecieptId { get; set; }
        public int ReceiptDtlId { get; set; }
        public int PaymentModeId { get; set; }
        public string ChequeNo { get; set; }
        public string RecieptNo { get; set; }
        public double? CCCharges { get; set; }
        public double Amount { get; set; }
        public string BankName { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? ChequeExpDate { get; set; }
    }
}