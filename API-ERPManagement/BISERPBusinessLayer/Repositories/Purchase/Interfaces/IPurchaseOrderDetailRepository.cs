using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPurchaseOrderDetailRepository
    {
        List<PurchaseOrderDetailsEntities> GetDetailByPOID(int Id);

        int CreateNewEntry(PurchaseOrderEntities poEntity, PurchaseOrderDetailsEntities entity, DBHelper dbhelper);
        bool UpdatePurchaseOrderQty(int POID, PurchaseOrderDetailsEntities entity);
        int AmmendPoDetail(PurchaseOrderEntities poEntity, PurchaseOrderDetailsEntities entity, DBHelper dbhelper);
    }
}
