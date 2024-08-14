using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IMaterialConsumptionRepository
    {
        IEnumerable<MaterialConsumptionEntity> GetMaterialConsumption(int RequisitionId);
        int CreateMaterialConsumption(InHouseEntity MainEntity, MaterialConsumptionEntity Entity, DBHelper dbhelper);
    }
}
