using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Billing
{
   public class ClientBillingEntity
    {
        public int ClientBillId { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string StrBillDate { get; set; }
        public int BranchId { get; set; }
        public double GrossAmt { get; set; }
        public double StanderdDis { get; set; }
        public double TotalRecieved { get; set; }
        public double Sgst { get; set; }
        public double Cgst { get; set; }
        public double Igst { get; set; }
        public double Ugst { get; set; }
        public double RoundOffAmt { get; set; }
        public double NetAmt { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public string Message { get; set; }
        public string ClientPONo { get; set; }
        public int ConsigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public string StrDueDate { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public List<PaymentTermsMasterEntities> PaymentTerm { get; set; }
        public List<ClientBillingDtEntity> ClientBillingDt { get; set; }
        public string ConsigneeName { get; set; }
        public string ConAddress { get; set; }
        public string ConGSTIN { get; set; }
        public string ECode { get; set; }
        public string EName { get; set; }
        public string EAddress { get; set; }
        public string ESociety { get; set; }
        public string ECity { get; set; }
        public string EState { get; set; }
        public string ECountry { get; set; }
        public string EPin { get; set; }
        public string EGSTIN { get; set; }
        public string EPhone1 { get; set; }
        public string EPhone2 { get; set; }
        public string EEMail { get; set; }
        public string EWeb { get; set; }

        public double TaxAmt { get; set; }

        public string StoreName { get; set; }
        public string ClientCode { get; set; }
        public string PANNo { get; set; }
        public string ClientEmail { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string CellPhone { get; set; }
        public string ProjectCode { get; set; }
        public string ClientName { get; set; }
        public string GSTIN { get; set; }
        public string Remark { get; set; }
        public string ProjectName { get; set; }
        public double ClientTotal { get; set; }
        public double SupplierTotal { get; set; }
        public double VendorTotal { get; set; }
        public string ReportName { get; set; }
        public string InvoiceType { get; set; }
    }

}
