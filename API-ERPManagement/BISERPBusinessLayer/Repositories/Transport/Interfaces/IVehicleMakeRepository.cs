using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleMakeRepository
    {
        IEnumerable<VehicleMakeEntity> GetAllVehicleMake();
        int CreateVehicleMake(VehicleMakeEntity Entity);
        bool UpdateVehicleMake(VehicleMakeEntity Entity);
        IEnumerable<VehicleMakeEntity> GetActiveVehicleMake();

    }
}
