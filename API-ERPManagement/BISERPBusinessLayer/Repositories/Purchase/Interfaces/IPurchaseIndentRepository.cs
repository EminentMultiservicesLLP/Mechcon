using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPurchaseIndentRepository
    {
        PurchaseIndentEntities GetPurchaseIndentById(int Id);
        IEnumerable<PurchaseIndentEntities> GetAllPurchaseIndent();
        IEnumerable<PurchaseIndentEntities> Getforverification(int StoreId);
        IEnumerable<PurchaseIndentEntities> GetAllPurchaseIndent(int StoreId);
        IEnumerable<PurchaseIndentEntities> GetAuthorizedPurchaseIndent(int StoreId);
        PurchaseIndentEntities CreatePurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper);
        bool UpdatePurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper);
        bool AuthCancelPurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper);
        bool DeletePurchaseIndent(PurchaseIndentEntities entity);
        IEnumerable<PurchaseIndentEntities> GetPendintMaterialRequest(int clientId);
        IEnumerable<IndentTepmlateClass> GetAllIndentTemplate(int StoreId, int ItemCategoryId);
        List<PurchaseIndentDetailEntities> GetIndentTemplateforId(int templateId);
        IEnumerable<PurchaseIndentEntities> GetPurchaseIndentForReport();
        List<PIRemarkLibrary> GetPIRemarkLibrary(int StoreId, int ItemId);
        IEnumerable<ProductEntities> GetProduct();
        IEnumerable<ProjectEntities> GetProject(int ProductID);
        IEnumerable<ProductItemEntities> GetProductItem(int ProductID, int? ProjectID);
        IEnumerable<PRStateDetailsEntities> GetPRStateDetails(int? IndentId);
    }
}
