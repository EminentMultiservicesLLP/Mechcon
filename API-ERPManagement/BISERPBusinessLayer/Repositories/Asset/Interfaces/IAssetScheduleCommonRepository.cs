using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetScheduleCommonRepository
    {
        AssetScheduleEntity SaveAssetSchedule(AssetScheduleEntity entity);

        IEnumerable<AssetScheduleEntity> AssetScheduleReport(DateTime fromdate,DateTime todate,int MaintenanceTypeId);
    }
}
