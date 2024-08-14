using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleModelRepository
    {
        IEnumerable<VehicleModelEntity> GetAllVehicleModel();
        int CreateVehicleModel(VehicleModelEntity Entity);
        bool UpdateVehicleModel(VehicleModelEntity Entity);
        IEnumerable<VehicleModelEntity> GetActiveVehicleModel();
    }
}
