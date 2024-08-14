using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IDiscrepancyDtRepository
    {
        int CreateDiscrepancyDt(DiscrepancyEntity mientity, DiscrepancyDtEntity entity,DBHelper dbHelper );
        IEnumerable<DiscrepancyDtEntity> GetDetailByDiscrepancyId(int DiscrepancyId);
        bool UpdateDiscrepancyAuthQty(DiscrepancyEntity mientity, DiscrepancyDtEntity entity, DBHelper dbHelper);
    }
}
