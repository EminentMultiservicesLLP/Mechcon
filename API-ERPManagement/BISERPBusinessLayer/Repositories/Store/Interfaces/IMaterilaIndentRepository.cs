using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterilaIndentRepository
    {
        MaterialIndentEntities GetMaterialIndentById(int Id);
        IEnumerable<MaterialIndentEntities> GetAllMaterialIndents(int StatusId, int UserId);

        IEnumerable<MaterialIndentEntities> GetAllMaterialIndents(int StoreId, DateTime Fromdate, DateTime todate , string ReportType);
        IEnumerable<MaterialIndentEntities> GetActiveMaterialIndents();
        IEnumerable<MaterialIndentEntities> GetAllAuthMaterialIndents(int Id, int UserId);
        IEnumerable<MaterialIndentEntities> GetAuthMaterialIndents(int UserId);
        IEnumerable<MaterialIndentEntities> GetAuthMaterialIndentsForIssue(int UserId);
        IEnumerable<MaterialIndentEntities> GetAuthUnitMaterialIndents(int UnitStoreId);
        MaterialIndentEntities CreateMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper);
        bool UpdateMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper);
        bool AuthCancelMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper);
        bool VerifyCancelMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper);
        bool DeleteUnit(MaterialIndentEntities entity);
        string CheckForValidation(MaterialIndentEntities entity);
        IEnumerable<ItemMasterEntities> GetItemListForMI(int? StoreId, int? CategoryId);

    }
}
