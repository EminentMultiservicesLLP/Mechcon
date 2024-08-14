using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public  interface IStockDetailsRepository
    {
        IEnumerable<StockDetailsEntity> GetStoreWisestockData(int storedId, int itemTypeId,int BranchID);
        IEnumerable<StockDetailsEntity> StoreStockSummary(int StoreId, int ItemTypeId);
        IEnumerable<StockDetailsEntity> StoreStockDetails(int StoreId, int ItemTypeId);
    }
}
