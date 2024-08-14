using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface ISupplierMasterRepository
    {
        SupplierMasterEntities GetSupplierById(int SupplierId);
        IEnumerable<SupplierMasterEntities> GetAllSupplier();
        IEnumerable<SupplierMasterEntities> GetActiveSupplier();
        int CreateSupplier(SupplierMasterEntities SupplierEntity);
        bool UpdateSupplier(SupplierMasterEntities SupplierEntity);
        bool DeleteSupplier(SupplierMasterEntities SupplierEntity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
        bool AuthCanSupplier(SupplierMasterEntities SupplierEntity);
    }
}
