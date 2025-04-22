using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
  public  interface IPurchaseReturnDetailsRepository
  {
      int CreatePurchaseReturntDetails(PurchaseReturnEntity entity1, PurchaseReturnDetailsEntities entity, DBHelper dbHelper);
      List<PurchaseReturnDetailsEntities> PurchaseReturnDetailsById(int ReturnID);
      List<PurchaseReturnDetailsRptEntities> PurchaseReturnDetailsRptById(int ReturnID);
        //  bool UpdatePurchaseReturnAuth(PurchaseReturnEntity entity, PurchaseReturnDetailsEntities entity1);
    }
}
