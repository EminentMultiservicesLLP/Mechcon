using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class RequestForQuoteEntities
    {
        public int IndentId { get; set; }
        public string IndentNumber { get; set; }
        public DateTime IndentDate { get; set; }
        public string strIndentDate { get; set; }
        public DateTime? QuotationDeadLine { get; set; }
        public string strQuotationDeadLine { get; set; }
        public int? DepartmentId { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public int? BudgetId { get; set; }
        public string Remarks { get; set; }
        public string IndentNature { get; set; }
        public string strIndentNature { get; set; }
        public DateTime? DeliveryStartDate { get; set; }

        public DateTime? DeliveryEndDate { get; set; }
        public int? ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public string Status { get; set; }
        public string AuthorisedRemarks { get; set; }
        public int? AuthorisedBy { get; set; }
        public DateTime? AuthorisedOn { get; set; }
        public string strAuthorisedOn { get; set; }
        public bool Authorised { get; set; }
        public int? ProcurementTypeID { get; set; }
        public int? PurchaseIndentId { get; set; }
        public int? Storeid { get; set; }
        public string StoreName { get; set; }
        public int? BranchID { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string DescriptiveName { get; set; }
        public string ClientName { get; set; }
        public List<RequestForQuoteDetailEntities> IndentDetails { get; set; }
        public List<RFQDeliveryTermEntities> RFQDeliveryTerms { get; set; }
        public List<RFQPaymentTermEntities> RFQPaymenterms { get; set; }
        public string IndentRemark { get; set; }
        public string CreatedBy { get; set; }
        public int IndentIdTemplateId { get; set; }
        public string IndentTemplateName { get; set; }
        public int AuthorizationStatusId { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
        public string FinancialYear { get; set; }
        public string RequiredDate { get; set; }
        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }

    }
    public class RFQDeliveryTermEntities
    {
        public int RFQID { get; set; }
        public int DelTermID { get; set; }
        public DateTime EnteredOn { get; set; }
        public int EnteredBy { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

        public string DeliveryTermDesc { get; set; }
        public string DeliveryTermCode { get; set; }
    }
    public class RFQPaymentTermEntities
    {
        public int POID { get; set; }
        public int PayTermID { get; set; }
        public string PaymentTermDesc { get; set; }
        public string PaymentTermCode { get; set; }
        public DateTime EnteredOn { get; set; }
        public int EnteredBy { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}
