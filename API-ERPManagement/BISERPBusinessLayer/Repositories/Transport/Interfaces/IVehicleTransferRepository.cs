using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IVehicleTransferRepository
    {
        IEnumerable<VehicleTransferEntity> GetAllVehicleTransfer();
        IEnumerable<VehicleTransferEntity> GetAllVehicleTransferRPT(DateTime fromdate, DateTime todate);
        IEnumerable<VehicleTransferEntity> GetAllVehicleSoldRPT(DateTime fromdate, DateTime todate);
        int CreateVehicleTransfer(VehicleTransferEntity Entity);
        bool AuthorizeCancel(VehicleTransferEntity Entity);
    }
}
