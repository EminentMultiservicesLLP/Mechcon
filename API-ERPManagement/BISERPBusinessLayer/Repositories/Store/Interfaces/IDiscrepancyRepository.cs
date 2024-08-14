using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IDiscrepancyRepository
    {
        DiscrepancyEntity CreateNewEntry(DiscrepancyEntity entity, DBHelper dbHelper);
        //bool AuthCancelDiscrepancy(DiscrepancyEntity entity, DBHelper dbhelper);
        IEnumerable<DiscrepancyEntity> GetAllDiscrepancy(int UserId);
    }
}
