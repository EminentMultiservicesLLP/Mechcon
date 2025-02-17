using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IStoreMasterRepository
    {
        StoreMasterEntities GetStoreById(int Id);
        IEnumerable<StoreMasterEntities> GetAllStores();
        IEnumerable<StoreMasterEntities> GetAllMainStores();
        IEnumerable<StoreMasterEntities> GetStoreFinancialYearWise(string FinancialYear);
        IEnumerable<StoreMasterEntities> GetUnitStores();
        IEnumerable<StoreMasterEntities> GetSubStores();
        IEnumerable<StoreMasterEntities> GetIndentToStores(int StoreId, int UserId);
        StoreMasterEntities CreateStore(StoreMasterEntities entity);
        StoreMasterEntities UpdateStore(StoreMasterEntities entity);
        bool DeleteStore(StoreMasterEntities entity);
        IEnumerable<StoreMasterEntities> GetActiveStore(int UserId);
        IEnumerable<StoreMasterEntities> BranchStoreMasters(int BranchId, int UserId);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicatestore(int StoreTypeID);
        bool CheckDuplicateupdate(string Code, int Id);
        IEnumerable<StoreMasterDtlEntity> GetStoredtl(int storeId);
        IEnumerable<ProjectBudgetDtlEntity> GetStoreBudgetdtl(int storeId);
        IEnumerable<ProjectBudgetDtlEntity> GetProjectBudget(int storeId, int ID);
        IEnumerable<StoreMasterEntities> GetProjectBudgetStatus(int storeId);
        IEnumerable<StoreMasterEntities> GetBudgetUtilzedDetails(int ProjectID, int ItemTypeId);
        List<StoreMasterEntities> GetMasterCode(int masterId);
        IEnumerable<ProjectTCMasterEntities> GetStorewiseProjectTC(int Id);
        int SaveStoreMasterApproval(StoreMasterEntities entity);
        IEnumerable<StoreMasterEntities> GetRevisionDetails(int StoreID, string Department);
        List<ProjectTransactionRecordEntities> GetProjectTransactionRecord(int StoreId);
        List<DeliverablesDetailEntities> GetDeliverablesDetail(int StoreId);
        List<BudgetStatusEntities> GetBudgetStatus();
        IEnumerable<WorkOrderEntities> GetEnqForProjectMaster(int UserID);
        IEnumerable<DashboardCount> GetPRDashboardCount(string financialYear , int? StoreID);
        IEnumerable<DashboardCount> GetPODashboardCount(string financialYear , int? StoreID);
        IEnumerable<DashboardCount> GetGRNDashboardCount(string financialYear , int? StoreID);
        IEnumerable<DashboardCount> GetMRDashboardCount(string financialYear , int? StoreID);

    }
}
