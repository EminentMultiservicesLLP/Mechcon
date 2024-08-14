using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IFloorRespository
    {
        IEnumerable<FloorEntity> GetAllFloor();
        int CreateFloor(FloorEntity Entity);
        bool UpdateFloor(FloorEntity Entity);
        IEnumerable<FloorEntity> GetBranchFloor();
        IEnumerable<FloorEntity> GetActiveFloor();
        IEnumerable<FloorEntity> GetActiveLocationWiseFloor(int LocationId);
        IEnumerable<FloorEntity> GetBranchLocationFloor(int BranchId);
    }
}
