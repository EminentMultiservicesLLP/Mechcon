using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using BISERP.Areas.Purchase.Models;

namespace BISERP.Area.Purchase.Models
{
    public class PurchaseOrderModel
    {
        public int ID { get; set; }
        public int? SupplierID { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        public int TenderID { get; set; }

        [Display(Name = "PO No")]
        public string PONo { get; set; }

        [Display(Name = "PO Date")]
        public DateTime PODate { get; set; }
        public string strPODate { get; set; }
        public string DeliveryTerms { get; set; }
        public string PaymentsTerms { get; set; }
        public string OtherTerms { get; set; }
        public int TaxID { get; set; }
        public int VendorId { get; set; }
        public double? Tax { get; set; }
        public double? Discount { get; set; }

        [Display(Name = "Gross Amount")]
        public double? Amount { get; set; }

        [Display(Name = "Other Charges")]
        public double? OtherCharges { get; set; }

        [Display(Name = "Grand Total")]
        public double? GrandTotal { get; set; }
        public int CreditDays { get; set; }
        public bool POComplete { get; set; }
        public string Notes { get; set; }
        public string Preparedby { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string SignAuthorityPerson { get; set; }
        public string SignAuthorityPDesig { get; set; }

        [Display(Name = "Reference No")]
        public string RefNo { get; set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
        public bool Authorised { get; set; }
        public int RFQId { get; set; }

        [Display(Name = "Indent No")]
        public string RFQNo { get; set; }
        public bool PostedToFA { get; set; }
        public string Against { get; set; }
        public int QuotationId { get; set; }
        public double? TotalFORe { get; set; }
        public double? TotalExciseAmt { get; set; }
        public int Againstid { get; set; }
        public string Status { get; set; }
        public int? AuthorisedBy { get; set; }
        public string AuthorisedByName { get; set; }
        public DateTime AuthorisedDate { get; set; }
        public string Details { get; set; }
        public bool Cancellation { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public int CanceledBy { get; set; }

        [Display(Name = "Store Name")]
        public int? StoreId { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public string StoreName { get; set; }
        public int BranchID { get; set; }
        public int ISPoModify { get; set; }
        public int POARC { get; set; }
        public string AuthoNotes { get; set; }
        public bool ISPOCLOSED { get; set; }
        public DateTime POCloseDate { get; set; }
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
        public List<PurchaseOrderDetailModel> PODetails { get; set; }
        public List<PODeliveryTermModel> PODeliveryTerms { get; set; }
        public List<POPaymentTermModel> POPaymenterms { get; set; }
        public List<POOtherTermModel> POOtherTerms { get; set; }
        public List<POTaxModel> POTax { get; set; }
        public List<POPriceBasisModel> POBasis { get; set; }
        public List<POInspectionModel> POInspectio { get; set; }
        public string Message { get; set; }
        public string Transport { get; set; }
        public string PurchaseTerm { get; set; }
        public string GSTIN { get; set; }
        public string SupEmail { get; set; }
        public string SupContactPerson { get; set; }
        public string SupSociety { get; set; }
        public string SupStreet { get; set; }
        public string SupState { get; set; }
        public string SupPin { get; set; }
        public string SupPhone { get; set; }
        public double? BED { get; set; }
        public double? Edu { get; set; }
        public double? SHECess { get; set; }
        public string SupAdd { get; set; }
        public string VendorAdd { get; set; }
        public string VendorGSTN { get; set; }
        public string VendorPhone { get; set; }
        public int Qty { get; set; }
        public int Amendment { get; set; }
        [Display(Name = "Vendor")]
        public int PoVendorId { get; set; }
        public string PoVendorName { get; set; }
        public int ItemTypeId { get; set; }
        public string CreatedBy { get; set; }
        public int AuthorizationStatusId { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string strDeliveryDate { get; set; }
        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string VerifiedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string strPreparedOn { get; set; }
        public string strVerifiedOn { get; set; }
        public string strAuthorisedOn { get; set; }

    }
    public class POStateDetailsModel
    {
        public string ItemName { get; set; }
        public int POQty { get; set; }
        public int GRNQty { get; set; }
        public string POStatus { get; set; }
    }
    public class SupplierDeliveryReportModel
    {
        public int POID { get; set; }
        public string PONo { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int POQty { get; set; }
        public DateTime RequiredDate { get; set; }
        public string strRequiredDate { get; set; }
        public int GRNQty { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string strDeliveryDate { get; set; }
        public int DaysLate { get; set; }
    }

}