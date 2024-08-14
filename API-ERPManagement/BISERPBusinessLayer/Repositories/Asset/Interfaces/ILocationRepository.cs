using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<LocationEntity> GetAllLocation();
        int CreateLocation(LocationEntity entity);
        bool UpdateLocation(LocationEntity entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int LocationId);
        IEnumerable<LocationEntity> GetActiveLocation();
        IEnumerable<LocationEntity> GetBranchLocation(int BranchId);
    }
}
