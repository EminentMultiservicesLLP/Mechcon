using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterilaIndentCommonRepository
    {
        MaterialIndentEntities SaveMaterialIndent(MaterialIndentEntities entity);
        bool UpdateMaterialIndent(MaterialIndentEntities entity);
        bool AuthCancelMaterialIndent(MaterialIndentEntities entity);
        bool VerifyCancelMaterialIndent(MaterialIndentEntities entity);
        IEnumerable<MaterialIndentEntities> GetMaterialRpt(DateTime fromdate, DateTime todate, int StoreId);
        IEnumerable<MaterialIndentEntities> MaterialReturnRpt(DateTime fromdate, DateTime todate, int StoreId);
        IEnumerable<MaterialIndentEntities> GetAllPendingMatreialIndent( int StoreId);
    }
}
