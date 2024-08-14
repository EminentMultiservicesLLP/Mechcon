using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IGRNVendorRepository
    {
        GRNVendorEntity GetByID(int Id);
        IEnumerable<GRNVendorEntity> GetAllList();
        IEnumerable<GRNVendorEntity> GRNForAuthorization(int StoreId);
        GRNVendorEntity CreateNewEntry(GRNVendorEntity entity, DBHelper dbHelper);
        bool UpdateEntry(GRNVendorEntity entity, DBHelper dbHelper);
        bool AuthCancelGRN(GRNVendorEntity entity, DBHelper dbHelper); 
    }
}
