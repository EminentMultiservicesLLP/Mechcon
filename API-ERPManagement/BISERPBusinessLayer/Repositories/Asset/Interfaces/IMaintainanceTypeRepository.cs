using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IMaintainanceTypeRepository
    {
        IEnumerable<MaintainanceTypeEntity> GetAllMaintainanceType();
        int CreateMaintainanceType(MaintainanceTypeEntity Entity);
        bool UpdateMaintainanceType(MaintainanceTypeEntity Entity);
        IEnumerable<MaintainanceTypeEntity> GetActiveMaintainanceType();
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
