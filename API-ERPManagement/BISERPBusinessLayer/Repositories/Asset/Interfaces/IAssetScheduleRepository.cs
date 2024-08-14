using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetScheduleRepository
    {
        AssetScheduleEntity SaveAssetSchedule(AssetScheduleEntity entity, DBHelper dbhelper);
        IEnumerable<AssetScheduleDetailsEntity> GetAssetSchedule(DateTime fromdate, DateTime todate, int branchId);
        int CreateAssetScheduleCompletion(AssetScheduleCompletionEntity entity);
        IEnumerable<AssetEntity> GetBranchAssetsforschedule(int BranchId);

    }
}
