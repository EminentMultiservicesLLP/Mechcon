using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetScheduleDetailRepository
    {
        int SaveAssetScheduleDetails(AssetScheduleEntity mainentity, AssetScheduleDetailsEntity entity, DBHelper dbhelper);

        IEnumerable<AssetScheduleDetailsEntity> AMCNotification(int DueDays);
        IEnumerable<AssetScheduleDetailsEntity> CMCNotification(int DueDays);
        IEnumerable<AssetScheduleDetailsEntity> OtherNotification(int DueDays);
     
       // IEnumerable<AssetScheduleDetailsEntity> GetAssetcodeScheduledt( int ScheduleId);
    }
}
