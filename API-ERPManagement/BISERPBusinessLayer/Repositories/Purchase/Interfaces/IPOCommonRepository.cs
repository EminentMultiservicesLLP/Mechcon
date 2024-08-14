using BISERPBusinessLayer.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPOCommonRepository
    {
        PurchaseOrderEntities SavePurchaseOrder(PurchaseOrderEntities entity);
        bool AuthCancelPO(PurchaseOrderEntities entity);
        bool UpdatePurchaseOrder(PurchaseOrderEntities entity);
        List<PurchaseOrderEntities> GetrptPurchaseOrder(int PoId);
        bool SavePoAmendment(PurchaseOrderEntities entity);
    }
}
