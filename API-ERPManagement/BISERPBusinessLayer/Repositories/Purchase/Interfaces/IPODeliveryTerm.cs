using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPODeliveryTerm
    {
        List<PODeliveryTermEntities> GetDetailByPOID(int Id);
        int CreateNewEntry(PurchaseOrderEntities poEntity, PODeliveryTermEntities entity, DBHelper dbhelper);
    }
}
