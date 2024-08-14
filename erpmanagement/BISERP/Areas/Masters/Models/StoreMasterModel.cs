using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class StoreMasterModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        public string MasterCode { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Project Code")]
        [JsonProperty("Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Project Name ")]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Display(Name = "Branch ID")]
        [JsonProperty("BranchID")]
        public int? BranchID { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        [Display(Name = "Branch Name")]
        [JsonProperty("BranchName")]
        public string BranchName { get; set; }




        [Display(Name = "Store Type")]
        [JsonProperty("StoreTypeID")]
        public int StoreTypeID { get; set; }


        [Display(Name = "Store Type Name")]
        [JsonProperty("StoreTypeName")]
        public string StoreTypeName { get; set; }

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

        [Display(Name = "Deactive")]
        [JsonProperty("Deactive")]
        public Nullable<bool> Deactive { get; set; }

        public IEnumerable<StoreTypeMasterModel> storetypes { get; set; }
        public string Message { get; set; }

        public string Description { get; set; }
        [Display(Name = "Budget")]
        public double ProjectBudget { get; set; }
        public double Utlized { get; set; }
        public double Balance { get; set; }
        public IEnumerable<StoreMasterDtlModel> Storedt { get; set; }
        public IEnumerable<ProjectBudgetDtlModel> Budgetdt { get; set; }
        [Display(Name = "PO No")]
        public string ClientPoNo { get; set; }
        public IEnumerable<ProjectTCMasterModel> ProjectTCdt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string strStartDate { get; set; }
        public string strDueDate { get; set; }


        [Display(Name = "StartDate")]
        public DateTime? DesignStartDate { get; set; }
        [Display(Name = "RevisionDate")]
        public DateTime? DesignRevisionDate { get; set; }
        [Display(Name = "ApprovalDate")]
        public DateTime? DesignApprovalDate { get; set; }

        [JsonProperty("DesignApproved")]
        [Display(Name = "Design")]
        public bool DesignApproved { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? ElectricalDesignStartDate { get; set; }
        [Display(Name = "RevisionDate")]
        public DateTime? ElectricalDesignRevisionDate { get; set; }
        [Display(Name = "ApprovalDate")]
        public DateTime? ElectricalDesignApprovalDate { get; set; }

        [JsonProperty("ElectricalDesignApproved")]
        [Display(Name = "ElectricalDesign")]
        public bool ElectricalDesignApproved { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? MarketingStartDate { get; set; }
        [Display(Name = "RevisionDate")]
        public DateTime? MarketingRevisionDate { get; set; }
        [Display(Name = "ApprovalDate")]
        public DateTime? MarketingApprovalDate { get; set; }

        [JsonProperty("MarketingApproved")]
        [Display(Name = "Marketing")]
        public bool MarketingApproved { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? ProjectsStartDate { get; set; }
        [Display(Name = "RevisionDate")]
        public DateTime? ProjectsRevisionDate { get; set; }
        [Display(Name = "ApprovalDate")]
        public DateTime? ProjectsApprovalDate { get; set; }

        [JsonProperty("ProjectsApproved")]
        [Display(Name = "Projects")]
        public bool ProjectsApproved { get; set; }

        public string strDesignStartDate { get; set; }
        public string strDesignRevisionDate { get; set; }
        public string strDesignApprovalDate { get; set; }

        public string strElectricalDesignStartDate { get; set; }
        public string strElectricalDesignRevisionDate { get; set; }
        public string strElectricalDesignApprovalDate { get; set; }

        public string strMarketingStartDate { get; set; }
        public string strMarketingRevisionDate { get; set; }
        public string strMarketingApprovalDate { get; set; }

        public string strProjectsStartDate { get; set; }
        public string strProjectsRevisionDate { get; set; }
        public string strProjectsApprovalDate { get; set; }
        public string Department { get; set; }
        public DateTime? NewRevisionDate { get; set; }
        public string strNewRevisionDate { get; set; }
        public string NewComment { get; set; }
    }

    public class StoreMasterDtlModel
    {
        public int StoreId { get; set; }
        public int StoreDtlId { get; set; }
        public int ItemId { get; set; }
        public double ItemQty { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string ItemDescription { get; set; }
    }
    public class ProjectBudgetDtlModel
    {
        public int StoreId { get; set; }
        public double BudgetCost { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public double UtilizedBudget { get; set; }
        public double BalanceDue { get; set; }
    }
    public class ProjectTransactionRecordModel
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
    }
    public class DeliverablesDetailModel
    {
        public string Type { get; set; }
        public string Status { get; set; }
    }
    public class BudgetStatusModel
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public double Budget { get; set; }
        public double Utilized { get; set; }
        public double Balance { get; set; }
        public string Delivered { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
    }
}