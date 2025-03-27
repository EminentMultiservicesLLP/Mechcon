using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IScopeOfSupplyMasterRepository
    {
        IEnumerable<ScopeOfSupplyMasterEntities> GetAllScopeOfSupply();
        int CreateScopeOfSupply(ScopeOfSupplyMasterEntities entity);
        bool UpdateScopeOfSupply(ScopeOfSupplyMasterEntities entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
