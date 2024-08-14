using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IGRNVendorDetailRepository
    {
        GRNVendorDetailEntity GetByID(int Id);
        IEnumerable<GRNVendorDetailEntity> GetDetailByGRNId(int GRNId);
        IEnumerable<GRNVendorDetailEntity> GetAllList();
        int CreateNewEntry(GRNVendorEntity grnentity, GRNVendorDetailEntity entity, DBHelper dbHelper);
        bool UpdateEntry(GRNVendorDetailEntity entity, DBHelper dbHelper);
        bool AuthCancelGRNDetail(GRNVendorEntity grnentity, GRNVendorDetailEntity entity, DBHelper dbHelper);
    }
}
