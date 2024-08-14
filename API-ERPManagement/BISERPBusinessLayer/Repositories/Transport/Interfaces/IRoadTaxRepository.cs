using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IRoadTaxRepository
    {        
        IEnumerable<RoadTaxEntity> GetAllRoadTax();
        IEnumerable<RoadTaxEntity> RoadTaxNotification(int DueDays);
        IEnumerable<RoadTaxEntity> GetAllRoadTax(int VehicleId);
        int CreateRoadTax(RoadTaxEntity Entity);
        bool UpdateRoadTax(RoadTaxEntity Entity);
        IEnumerable<RoadTaxEntity> ActiveRoadTax();
        bool CheckDuplicateRoadTax(int VehicleId);
    }
}
