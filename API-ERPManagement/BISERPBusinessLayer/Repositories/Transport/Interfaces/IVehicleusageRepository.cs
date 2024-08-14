using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleusageRepository
    {
        IEnumerable<VehicleUsageEntity> GetAllVehicleUsage();
        int CreateVehicleUsage(VehicleUsageEntity Entity);
        bool UpdateVehicleUsage(VehicleUsageEntity Entity);
        IEnumerable<VehicleUsageEntity> GetActiveVehicleUsage(int UserId);

    }
}
