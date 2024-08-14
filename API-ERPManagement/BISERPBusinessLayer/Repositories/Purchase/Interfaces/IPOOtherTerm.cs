using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPOOtherTerm
    {
        List<POOtherTermEntities> GetDetailByPOID(int Id);
        int CreateNewEntry(PurchaseOrderEntities poEntity, POOtherTermEntities entity, DBHelper dbhelper);
        int CreateNewBasisEntry(PurchaseOrderEntities poEntity, POPriceBasisEntity entity, DBHelper dbhelper);
        int CreateNewInspectionEntry(PurchaseOrderEntities poEntity, POInspectionModel entity, DBHelper dbhelper);
        List<POPriceBasisEntity> GetBasisByPOID(int Id);
        List<POInspectionModel> GetInspectionPOID(int Id);
        List<POTaxModel> GetTaxByPOID(int Id);
    }
}
