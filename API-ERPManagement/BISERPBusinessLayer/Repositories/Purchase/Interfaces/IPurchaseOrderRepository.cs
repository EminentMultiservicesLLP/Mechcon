using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        PurchaseOrderEntities GetByID(int Id);
        IEnumerable<PurchaseOrderEntities> GetAllList();
        IEnumerable<PurchaseOrderEntities> GetPOForAuthorization(int StoreId, int? AgainstId);
        IEnumerable<PurchaseOrderEntities> GetAuthorizedList(int StoreId);
        PurchaseOrderEntities CreateNewEntry(PurchaseOrderEntities entity, DBHelper dbhelper);
        bool UpdateEntry(PurchaseOrderEntities entity, DBHelper dbhelper);
        bool BeforePOAuth(PurchaseOrderEntities entity, DBHelper dbhelper);
        bool AuthCancelPurchaseOrder(PurchaseOrderEntities entity, DBHelper dbhelper);
        //bool DeleteEntry(PurchaseOrderEntities entity);
        IEnumerable<PurchaseOrderEntities> GetUnAuthorizationPo(int? StoreId, int? AgainstId);
        IEnumerable<PurchaseOrderEntities> PurchaseOrderForGrn(int storeId);
        bool PurchaseOrderAmendment(List<PurchaseOrderDetailsEntities> entity);
        bool SavePoAmendmentMaster(PurchaseOrderEntities entity, DBHelper dbhelper);
        IEnumerable<PurchaseOrderEntities> GetPoForAmmendmet(int PoId);
        bool PurchaseOrderClose(PurchaseOrderEntities entity);
        IEnumerable<PurchaseOrderEntities> GetPurchaseOrderForReport();
        IEnumerable<POStateDetailsEntities> GetPOStateDetails(int? IndentId);
        IEnumerable<SupplierDeliveryReportEntities> GetSupplierDeliveryReport(int? supplierID, string FromDate, string ToDate);
    }
}
