using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IStoreUnitLinkingMasterRepository
    {
        int CreateUnitStore(StoreUnitLinkingEntities entity);
      bool UpdateStoreUnit(StoreUnitLinkingEntities entity);
      IEnumerable<StoreUnitLinkingEntities> GetAllUnitStore();
      bool CheckDuplicateUnitStore(StoreUnitLinkingEntities entity);
      bool CheckDuplicateupdate(StoreUnitLinkingEntities entity);
    }
}
