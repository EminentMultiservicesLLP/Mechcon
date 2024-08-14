using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IUnitMasterRepository
    {
            UnitMasterEntities GetUnitById(int unitId);
            IEnumerable<UnitMasterEntities> GetAllUnits();
            int CreateUnit(UnitMasterEntities unitEntity);
            bool UpdateUnit(UnitMasterEntities unitEntity);
            bool DeleteUnit(UnitMasterEntities unitEntity);
            IEnumerable<UnitMasterEntities> GetActiveUnit();
            bool CheckDuplicateItem(string code);
            bool CheckDuplicateupdate(string code, int ID);
    }
}
