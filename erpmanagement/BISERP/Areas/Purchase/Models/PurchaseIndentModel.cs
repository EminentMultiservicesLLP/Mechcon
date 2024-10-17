using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BISERP.Areas.Masters.Models;

namespace BISERP.Area.Purchase.Models
{
    public class PurchaseIndentModel
    {
        [JsonProperty("IndentId")]
        public int IndentId { get; set; }

        [Display(Name = "Indent No")]
        [JsonProperty("IndentNumber")]
        public string IndentNumber { get; set; }

        [Display(Name = "PR Date")]
        [JsonProperty("IndentDate")]
        public DateTime IndentDate { get; set; }

        public string strIndentDate { get; set; }

        [Display(Name = "Indent No")]
        [JsonProperty("DepartmentId")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Budget")]
        [JsonProperty("BudgetId")]
        public int? BudgetId { get; set; }

        [Display(Name = "Remarks")]
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "PropIndication")]
        [JsonProperty("PropIndication")]
        public bool PropIndication { get; set; }

        [Display(Name = "Nature")]
        [JsonProperty("IndentNature")]
        public int IndentNature { get; set; }
        public string strIndentNature { get; set; }

        [Display(Name = "Delivery Start Date")]
        [JsonProperty("DeliveryStartDate")]
        public DateTime? DeliveryStartDate { get; set; }
        [Display(Name = "Delivery End Date")]
        [JsonProperty("DeliveryEndDate")]
        public DateTime? DeliveryEndDate { get; set; }
        [Display(Name = "Item Type")]
        [JsonProperty("ItemCategoryId")]
        public int? ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        [Display(Name = "Status")]
        [JsonProperty("Status")]
        public string Status { get; set; }
        [Display(Name = "Cancellation Remark")]
        [JsonProperty("AuthorisedRemarks")]
        public string AuthorisedRemarks { get; set; }
        public int? AuthorisedBy { get; set; }
        public DateTime? AuthorisedOn { get; set; }
        public string strAuthorisedOn { get; set; }
        public bool Authorised { get; set; }
        [JsonProperty("CancellationRemark")]
        public string CancellationRemark { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? CancelledOn { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public int? ProcurementTypeID { get; set; }

        [Display(Name = "Project")]
        [JsonProperty("Storeid")]
        public int? Storeid { get; set; }
        public string StoreName { get; set; }

        [Display(Name = "Product")]
        [JsonProperty("ProductID")]
        public int? ProductID { get; set; }
        public string ProductName { get; set; }

        [Display(Name = "Location")]
        [JsonProperty("LocationID")]
        public int? LocationID { get; set; }
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
        public List<int> IndentDetailIds { get; set; }
        [JsonProperty("Deactive")]
        public Nullable<bool> Deactive { get; set; }
        public List<PurchaseIndentDetailModel> IndentDetails { get; set; }
        public string Message { get; set; }
        public string Supplier { get; set; }//DescriptiveName
        public string pono { get; set; }
        public string DescriptiveName { get; set; }
        public string IndentNo { get; set; }
        public int Indent_Id { get; set; }
        public string ClientName { get; set; }
        public IEnumerable<ProjectBudgetDtlModel> Budgetdt { get; set; }
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

    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public bool Deactive { get; set; }
    }
    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
    public class ProductItemModel
    {
        public string ProductName { get; set; }
        public string ProjectName { get; set; }
        public string ItemName { get; set; }
        public double ItemQty { get; set; }
    }
}