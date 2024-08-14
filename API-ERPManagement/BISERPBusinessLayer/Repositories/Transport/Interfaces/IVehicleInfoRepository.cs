using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleInfoRepository
    {
        IEnumerable<VehicleInfoEntity> GetAllVehicle(int UserId);
        IEnumerable<VehicleInfoEntity> GetAllVehicleNo(int branchId);
        IEnumerable<VehicleInfoEntity> GetAllVehicleSchedule(int BranchId);
        int SaveVehicleInfo(VehicleInfoEntity Entity);
        bool UpdateVehicleInfo(VehicleInfoEntity Entity);
        bool CheckDuplicateVehicle(VehicleInfoEntity Entity);
        IEnumerable<VehicleInfoEntity> GetAllVehicleListRpt(int BranchId);
        IEnumerable<VehicleInfoEntity> VehicleInfoReport(int BranchId, int VehicleId);
    }
}
