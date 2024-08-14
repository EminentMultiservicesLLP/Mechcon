using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IOutsideMaintenanceRepository
    {
        OutsideMaintenanceEntity CreateOutsideMaintenance(OutsideMaintenanceEntity Entity,DBHelper dbhelper);
        OutsideMaintenanceEntity GetOutsideMaintenanceNodt( int MaintenanceId);
        IEnumerable<SparePartsOuthouseUtilEntity> GetOutsideMaintenanceNodtItem(int OUTHOUSEID);
       
    }
}
