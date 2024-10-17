using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class PurchaseIndentEntities
    {
        public int IndentId { get; set; }
        public string IndentNumber { get; set; }
        public DateTime IndentDate { get; set; }
        public string strIndentDate { get; set; }
        public int? DepartmentId { get; set; }
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
        public int? Storeid { get; set; }
        public string StoreName { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
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
        public List<PurchaseIndentDetailEntities> IndentDetails { get; set; }
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
        public int? MaterialIndentId { get; set; }
        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string VerifiedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }       
        public DateTime? VerifiedOn { get; set; }
        public string strInsertedOn { get; set; }
        public string strVerifiedOn { get; set; }
    }
    public class IndentTepmlateClass
    {
        public int IndentIdTemplateId { get; set; }
        public string IndentTemplateName { get; set; }
    }
    public class PIRemarkLibrary
    {
        public int IndentRemarkId { get; set; }
        public string IndentRemark { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }
    public class ProductEntities
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public bool Deactive { get; set; }
    }
    public class ProjectEntities
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
    public class ProductItemEntities
    {
        public string ProductName { get; set; }
        public string ProjectName { get; set; }
        public string ItemName { get; set; }
        public double ItemQty { get; set; }
    }
}
