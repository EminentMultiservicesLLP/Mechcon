using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public  interface IStockRegisterRepository
    {
        IEnumerable<StockRegisterEntity> GetStorewiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate, int IssueTo);
        IEnumerable<StockRegisterEntity> StoreStockRegisterReport(DateTime fromdate, DateTime todate, int StoredId, int ItemTypeId, int itemid);
        IEnumerable<StockRegisterEntity> StockConsumptionReport(DateTime fromdate, DateTime todate, int StoredId, int ItemTypeId);
        IEnumerable<StockRegisterEntity> StockEvaluationReport(DateTime todate, int StoredId, int ItemTypeId);

        IEnumerable<BatchEntity> ExpiryRegisterReport(DateTime fromdate, int MaxDays);
        IEnumerable<StockRegisterEntity> GetStoreItemwiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate);


    }
}
