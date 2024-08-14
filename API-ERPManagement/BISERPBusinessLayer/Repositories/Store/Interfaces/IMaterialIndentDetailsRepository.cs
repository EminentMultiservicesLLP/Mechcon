using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
  public  interface IMaterialIndentDetailsRepository
    {
        MaterialIndentDetailsEntities GetMaterialIndentDetailsById(int Id);
        List<MaterialIndentDetailsEntities> GetAllMaterialIndentDetailsByIndentId(int Indent_Id);
        List<MaterialIndentDetailsEntities> GetAuthMIDetailsByIndentId(int Indent_Id);
        List<MaterialIndentDetailsEntities> GetVerifiedMIDetailsByIndentId(int Indent_Id);
        List<MaterialIndentDetailsEntities> GetAuthMIItemDetails(int IndentDetailId);
        List<MaterialIndentDetailsEntities> GetpendingAuthMIItemDetails(int IndentId);
        int CreateMaterialIndentDetails(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper);
        bool UpdateMaterialIndentAuthQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper);
        bool UpdateMaterialIndentVerifyQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper);
        bool UpdateMaterialIndentDtQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity);
        bool UpdatePendingMaterialIndent(List<MaterialIndentDetailsEntities> entity);
        bool DeleteIndentItem(MaterialIndentDetailsEntities entity);
    }
}
