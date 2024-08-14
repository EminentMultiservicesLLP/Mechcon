using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IGRNDetailRepository
    {
        GRNDetailEntity GetByID(int Id);
        IEnumerable<GRNDetailEntity> GetDetailByGRNId(int GRNId);
        IEnumerable<GRNDetailEntity> GetAllList();
        int CreateNewEntry(GRNEntity grnentity, GRNDetailEntity entity, DBHelper dbHelper);

        bool UpdateGRNDetailEntry(GRNEntity grnentity, GRNDetailEntity entity, DBHelper dbHelper);
        bool UpdateEntry(GRNDetailEntity entity, DBHelper dbHelper);
        bool AuthCancelGRNDetail(GRNEntity grnentity, GRNDetailEntity entity, DBHelper dbHelper);
    }
}
