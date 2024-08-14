using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleTypeRepository
    {
        IEnumerable<VehicleTypeEntity> GetAllVehicleType();
        int CreateVehicleType(VehicleTypeEntity Entity);
        bool UpdateVehicleType(VehicleTypeEntity Entity);
        IEnumerable<VehicleTypeEntity> GetActiveVehicleType(int UserId);

    }
}
