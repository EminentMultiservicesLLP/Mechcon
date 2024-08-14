using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPOPaymentTerm
    {
        List<POPaymentTermEntities> GetDetailByPOID(int Id);
        int CreateNewEntry(PurchaseOrderEntities poEntity, POPaymentTermEntities entity, DBHelper dbhelper);
    }
}
