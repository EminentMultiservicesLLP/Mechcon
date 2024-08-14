using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class StockDetailsRepository:IStockDetailsRepository
    {
        public IEnumerable<StockDetailsEntity> GetStoreWisestockData(int storedId, int itemTypeId, int BranchID)
        {

            IEnumerable<StockDetailsEntity> stockRegiter = null;
             using (DBHelper dbHelper = new DBHelper())
             {
                 DBParameterCollection paramCollection = new DBParameterCollection();
                 paramCollection.Add(new DBParameter("storeid", storedId, DbType.Int32));
                 paramCollection.Add(new DBParameter("ItemTypeId", itemTypeId, DbType.Int32));
                 paramCollection.Add(new DBParameter("BranchID",BranchID, DbType.Int32));
                 DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.GetStockDetails, paramCollection, CommandType.StoredProcedure);

                 stockRegiter = dtstockRegiter.AsEnumerable()
                             .Select(row => new StockDetailsEntity
                             {   
                                 ID = row.Field<int>("ItemId"),
                                 ItemName = row.Field<string>("Item Name"),
                                 Itemcode = row.Field<string>("Item Code"),
                                 Batch = row.Field<string>("Batch"),
                                 Manufacture = row.Field<string>("Manufacture"),
                                 Supplier = row.Field<string>("Supplier"),
                                 StoreName = row.Field<string>("Store"),
                                 ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                 Qty = row.Field<double>("Qty"),
                                 strExpiryDate = Convert.ToDateTime(row.Field<DateTime>("ExpiryDate")).ToString("dd-MMMM-yyyy"),
                               
                             }).ToList();
             }
             return stockRegiter;
        }

        public IEnumerable<StockDetailsEntity> StoreStockSummary(int StoreId, int ItemTypeId)
        {

            IEnumerable<StockDetailsEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.StoreStockSummary, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockDetailsEntity
                            {
                                ItemID = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemType = row.Field<string>("ItemType"),
                                Itemcode = row.Field<string>("ItemCode"),
                                StoreName = row.Field<string>("StoreName"),
                                Qty = row.Field<double>("Qty"),
                                MRP = row.Field<double>("MRP")
                            }).ToList();
            }
            return stockRegiter;
        }

        public IEnumerable<StockDetailsEntity> StoreStockDetails(int StoreId, int ItemTypeId)
        {

            IEnumerable<StockDetailsEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.StoreStockDetails, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockDetailsEntity
                            {
                                ItemID = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemType = row.Field<string>("ItemType"),
                                Itemcode = row.Field<string>("ItemCode"),
                                Batch = row.Field<string>("Batch"),
                                StoreName = row.Field<string>("StoreName"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                Qty = row.Field<double>("Qty"),
                                MRP = row.Field<double>("MRP"),
                                strExpiryDate = Convert.ToDateTime(row.Field<DateTime>("ExpiryDate")).ToString("dd-MMMM-yyyy"),

                            }).ToList();
            }
            return stockRegiter;
        }
    }

}
