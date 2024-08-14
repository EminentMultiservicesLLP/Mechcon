using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface ICancelPendingMaterialIndentRepository
    {
        IEnumerable<CancelPendingMaterialIndentEntities> GetCancelMaterialIndent(int storedId);
        int CreateCancelPendingMaterialIndent(CancelPendingMaterialIndentEntities entity);

    }
}
