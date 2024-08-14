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
    public  interface IStoreConsumptionDetailsRepository
    {
        int StockConsumptionDetails(StoreConsumptionEntities mainentity, StoreConsumptionDetailsEntities entity,DBHelper dbHelper);

        IEnumerable<StoreConsumptionCancelEntities> GetConsumptionDT(int Id);

        List<StoreConsumptionCancelEntities> CreateStockcancel(List<StoreConsumptionCancelEntities> entity);

    }
}
