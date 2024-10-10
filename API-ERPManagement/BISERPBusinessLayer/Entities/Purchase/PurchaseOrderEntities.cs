using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class PurchaseOrderEntities
    {
        public int ID { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int TenderID { get; set; }
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public string strPODate { get; set; }
        public string DeliveryTerms { get; set; }
        public string PaymentsTerms { get; set; }
        public string OtherTerms { get; set; }
        public int TaxID { get; set; }
        public double? Tax { get; set; }
        public double? Discount { get; set; }
        public double? Amount { get; set; }
        public double? OtherCharges { get; set; }
        public double? GrandTotal { get; set; }
        public int CreditDays { get; set; }
        public bool POComplete { get; set; }
        public string Notes { get; set; }
        public string Preparedby { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string SignAuthorityPerson { get; set; }
        public string SignAuthorityPDesig { get; set; }
        public string RefNo { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Authorised { get; set; }
        public int RFQId { get; set; }
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
        public int? StoreId { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public string StoreName { get; set; }
        public int BranchID { get; set; }
        public int VendorId { get; set; }
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
        public List<PurchaseOrderDetailsEntities> PODetails { get; set; }
        public List<PODeliveryTermEntities> PODeliveryTerms { get; set; }
        public List<POPaymentTermEntities> POPaymenterms { get; set; }
        public List<POOtherTermEntities> POOtherTerms { get; set; }
        public List<POTaxModel> POTax { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public List<SupplierMasterEntities> Supplier { get; set; }
        public List<UnitMasterEntities> Unit { get; set; }
        public List<ItemPackSizeMasterEntities> PackingSize { get; set; }
        public List<POPriceBasisEntity> POBasis { get; set; }
        public List<POInspectionModel> POInspectio { get; set; }
        public string AuthorizisePerson { get; set; }
        public string Preparedbyname { get; set; }
        public string Transport { get; set; }
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
        public int PoVendorId { get; set; }
        public string PoVendorName { get; set; }
        public string CreatedBy { get; set; }
        public int ItemTypeId { get; set; }
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
}
