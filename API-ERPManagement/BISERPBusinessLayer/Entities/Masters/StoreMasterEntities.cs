using BISERPBusinessLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class StoreMasterEntities : ActionResponseStatus
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        public int StoreTypeID { get; set; }
        public string MasterCode { get; set; }
        public string Code { get; set; }
        public Nullable<int> BranchID { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
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
        public Nullable<bool> Deactive { get; set; }

        public string StoreTypeName { get; set; }
        public string Description { get; set; }
        public double ProjectBudget { get; set; }
        public double Utlized { get; set; }
        public double Balance { get; set; }
        public IEnumerable<StoreMasterDtlEntity> Storedt { get; set; }
        public IEnumerable<ProjectBudgetDtlEntity> Budgetdt { get; set; }
        public string ClientPoNo { get; set; }
        public IEnumerable<ProjectTCMasterEntities> ProjectTCdt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string strStartDate { get; set; }
        public string strDueDate { get; set; }


        public DateTime? DesignStartDate { get; set; }
        public DateTime? DesignRevisionDate { get; set; }
        public DateTime? DesignApprovalDate { get; set; }
        public bool DesignApproved { get; set; }

        public DateTime? ElectricalDesignStartDate { get; set; }
        public DateTime? ElectricalDesignRevisionDate { get; set; }
        public DateTime? ElectricalDesignApprovalDate { get; set; }
        public bool ElectricalDesignApproved { get; set; }

        public DateTime? MarketingStartDate { get; set; }
        public DateTime? MarketingRevisionDate { get; set; }
        public DateTime? MarketingApprovalDate { get; set; }
        public bool MarketingApproved { get; set; }

        public DateTime? ProjectsStartDate { get; set; }
        public DateTime? ProjectsRevisionDate { get; set; }
        public DateTime? ProjectsApprovalDate { get; set; }
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

    public class StoreMasterDtlEntity
    {
        public int StoreId { get; set; }
        public int StoreDtlId { get; set; }
        public int ItemId { get; set; }
        public double ItemQty { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string ItemDescription { get; set; }
    }
    public class ProjectBudgetDtlEntity
    {
        public int StoreId { get; set; }
        public int StoreDtlId { get; set; }
        public int ItemId { get; set; }
        public double ItemQty { get; set; }
        public string ItemName { get; set; }
        public double BudgetCost { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public double UtilizedBudget { get; set; }
        public double BalanceDue { get; set; }

    }

    public class ProjectTransactionRecordEntities
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
    }
    public class DeliverablesDetailEntities
    {
        public string Type { get; set; }
        public string Status { get; set; }
    }
    public class BudgetStatusEntities
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
    public class DashboardCount
    {
        public int AuthorizationStatusID { get; set; }
        public string AuthorizationStatus { get; set; }
        public int RequestCount { get; set; }
    }
}
