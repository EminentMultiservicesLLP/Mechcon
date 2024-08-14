using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPurchaseIndentDetailRepository
    {
        PurchaseIndentDetailEntities GetPurchaseIndentDetilsById(int Id);
        List<PurchaseIndentDetailEntities> GetAllPurchaseIndentDetailByIndentId(int IndentId);
        List<PurchaseIndentDetailEntities> GetAuthPIDetailByIndentId(int IndentId);
        int CreatePurchaseIndentDetails(int IndentId, int? StoreId, PurchaseIndentDetailEntities entity, DBHelper dbhelper);
        bool UpdatePurchaseIndentDetails(PurchaseIndentEntities mainentity, PurchaseIndentDetailEntities entity, DBHelper dbhelper);
        bool UpdatePurchaseIndentAuthQty(PurchaseIndentEntities mainentity, PurchaseIndentDetailEntities entity, DBHelper dbhelper);
        bool DeletePurchaseIndentDetails(PurchaseIndentDetailEntities entity);

        bool DeleteIndentItem(int IndentDetailId);
    }
}
