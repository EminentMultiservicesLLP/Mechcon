using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IStoreQuantityLimitsRepository
    {
        IEnumerable<StoreQuantityLimitsEntity> GetAllItemLimits(int storeId, int ItemTypeId);
        int CreateQuantityLimits(StoreQuantityLimitsEntity entity);
        IEnumerable<StoreQuantityLimitsEntity> GetNotificationQuantity(int UserId);
        IEnumerable<MasterReportEntity> GetMasterReport(int StoreId, DateTime? FromDate, DateTime? ToDate);
        IEnumerable<ProjectBudgetConclusion> GetProjectBudgetConclusion(int StoreId, DateTime? FromDate, DateTime? ToDate);
    }
}
