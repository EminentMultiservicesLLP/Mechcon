using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IPurchaseReturnRepository
    {
        IEnumerable<PurchaseReturnEntity> GetDetailByID(int? StoreId);
        IEnumerable<PurchaseReturnEntity> GetAllPurchaseReturn(int? StoreId);
        IEnumerable<PurchaseReturnDetailsEntities> GetGrnDetailByID(int GrnId);
        PurchaseReturnEntity CreatePurchaseReturn(PurchaseReturnEntity entity,DBHelper dbHelper);
        bool PurchaseReturnAuth(PurchaseReturnEntity entity, PurchaseReturnDetailsEntities entity1);
        IEnumerable<PurchaseReturnEntity> GetPurchaseReturnForReport();
        PurchaseReturnRptEntity GetPurchaseReturnById(int Id);
    }
}
