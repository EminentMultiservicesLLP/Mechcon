using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class WorkOrderEntities
    {
        public int IndentId { get; set; }
        public string IndentNumber { get; set; }
        public DateTime IndentDate { get; set; }
        public string strIndentDate { get; set; }
        public DateTime? QuotationDeadLine { get; set; }
        public string strQuotationDeadLine { get; set; }
        public int? DepartmentId { get; set; }
        public int? VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string ShippingAddress { get; set; }
        public int? BudgetId { get; set; }
        public string Remarks { get; set; }
        public string IndentNature { get; set; }
        public string strIndentNature { get; set; }
        public DateTime? DeliveryStartDate { get; set; }

        public DateTime? DeliveryEndDate { get; set; }
        public int? ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public double? Tax { get; set; }
        public double? Amount { get; set; }
        public double? GrandTotal { get; set; }
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
        public List<WorkOrderDetailEntities> IndentDetails { get; set; }
        public List<WODeliveryTermEntities> WODeliveryTerms { get; set; }
        public List<WOPaymentTermEntities> WOPaymenterms { get; set; }
        public List<WOOtherTermEntities> WOOtherTerms { get; set; }
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
        public string Preparedby { get; set; }
    }

    public class WorkOrderDetailEntities
    {
        public int IndentDetailId { get; set; }
        public int ItemId { get; set; }
        public double? ItemRate { get; set; }
        public double? ItemQty { get; set; }
        public double? EstimatedCost { get; set; }
        public double? SalesTax { get; set; }
        public double? ExciseTax { get; set; }
        public double? Escalated { get; set; }
        public double? LandingRate { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryEnddate { get; set; }
        public double? AuthorisedQty { get; set; }
        public int packsizeid { get; set; }
        public double? freeqty { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }
        public string VATOn { get; set; }
        public string VAT { get; set; }
        public double? MRP { get; set; }
        public double? IssueQty { get; set; }
        public double? Consumeqty { get; set; }
        public int BrandID { get; set; }
        public string POStatus { get; set; }
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
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double? TaxRate { get; set; }
        public string UnitName { get; set; }
        public double? CurrentQty { get; set; }
        public string PackSize { get; set; }
        public decimal? PackSizeQuantity { get; set; }
        public string DescriptiveName { get; set; }
        public string HSNCode { get; set; }
        public string IndentRemark { get; set; }
        public int IndentIdTemplateId { get; set; }
        public string ItemsRequiredDate { get; set; }
        public double? PendingQty { get; set; }
        public double? IGST { get; set; }
        public double? CGST { get; set; }
        public double? UGST { get; set; }
        public double? SGST { get; set; }
        public double? Amount { get; set; }
        public double? TaxAmount { get; set; }
        public double? NetAmount { get; set; }
    }
    public class WODeliveryTermEntities
    {
        public int WOID { get; set; }
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
    public class WOPaymentTermEntities
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
    public class WOOtherTermEntities
    {
        public int POID { get; set; }
        public int OtherTermID { get; set; }
        public string OthersTermDesc { get; set; }
        public string OthersTermCode { get; set; }
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
