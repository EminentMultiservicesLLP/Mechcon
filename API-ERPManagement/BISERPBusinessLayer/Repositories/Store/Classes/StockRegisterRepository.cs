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
    public class StockRegisterRepository:IStockRegisterRepository
    {
        public IEnumerable<StockRegisterEntity> GetStorewiseStock(int storedId, int itemTypeId, DateTime FromDate, DateTime ToDate, int IssueTo)
        {
            
             IEnumerable<StockRegisterEntity> stockRegiter = null;
             using (DBHelper dbHelper = new DBHelper())
             {
                 DBParameterCollection paramCollection = new DBParameterCollection();

                 paramCollection.Add(new DBParameter("storeid", storedId, DbType.Int32));
                 paramCollection.Add(new DBParameter("ItemTypeId", itemTypeId, DbType.Int32));
                 paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                 paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                 paramCollection.Add(new DBParameter("IssueTo", IssueTo, DbType.Int32));
                 DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.GetStoreWisestockDataByID, paramCollection, CommandType.StoredProcedure);

                 stockRegiter = dtstockRegiter.AsEnumerable()
                             .Select(row => new StockRegisterEntity
                             {
                                 ItemID = row.Field<int>("ItemId"),
                                 ItemCode = row.Field<string>("Code"),
                                 ItemName = row.Field<string>("Name"),
                                 QtyReceived = row.Field<double>("QtyReceived"),
                                 QtyIssued = row.Field<double>("QtyIssued"),
                                 BalanceQty = row.Field<double>("BatchBalQty"),
                                 OpeningBalance = row.Field<double>("PreviousBalanceQty"),

                                 DocDate = row.Field<DateTime>("DocDate"),
                                 strDocDate = Convert.ToDateTime(row.Field<DateTime>("DocDate")).ToString("dd-MMM-yyyy"),
                                 DocId = row.Field<int>("docid"),
                                 Description = row.Field<string>("Description"),
                                 ReceiptNo = row.Field<string>("ReceiptNo"),

                                 IssueNo = row.Field<string>("IssueNo"),
                                 UnitName = row.Field<string>("UnitName"),
                                 IssueToStore = row.Field<string>("Issue To Store"),
                                 BatchName = row.Field<string>("BatchName"),

                                 ExpiryDate = row.Field<DateTime>("Expirydate"),
                                 MRP = row.Field<double>("Mrp"),
                                 ItemTypeID = row.Field<int>("ItemTypeID"),
                                 ItemType = row.Field<string>("ItemTypeName")
                             }).ToList();
             }
             return stockRegiter;
        }
   
        public IEnumerable<StockRegisterEntity> GetStoreItemwiseStock(int storedId, int itemTypeId, DateTime FromDate, DateTime ToDate)
        {
            IEnumerable<StockRegisterEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("storeid", storedId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", itemTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));              
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.GetStoreItemWisestockDataByID, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockRegisterEntity
                            {
                                ItemID = row.Field<int>("ItemId"),
                                ItemCode = row.Field<string>("Code"),
                                ItemName = row.Field<string>("Name"),
                                QtyReceived = row.Field<double>("QtyReceived"),
                                QtyIssued = row.Field<double>("QtyIssued"),
                                BalanceQty = row.Field<double>("BalanceQty"),
                                OpeningBalance = row.Field<double>("PreviousBalanceQty"),

                                DocDate = row.Field<DateTime?>("DocDate"),
                                strDocDate = Convert.ToDateTime(row.Field<DateTime?>("DocDate")).ToString("dd-MMM-yyyy"),

                                DocId = row.Field<int>("docid"),
                                Description = row.Field<string>("Description"),
                                ReceiptNo = row.Field<string>("ReceiptNo"),

                                IssueNo = row.Field<string>("IssueNo"), 
                                UnitName = row.Field<string>("UnitName"),
                                IssueToStore = row.Field<string>("Issue To Store"),
                                BatchName = row.Field<string>("BatchName"),

                                ExpiryDate = row.Field<DateTime?>("Expirydate"),
                                //StrExpirydate = row.Field<string>("Expirydate"),

                                MRP = row.Field<double>("Mrp"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                ItemType = row.Field<string>("ItemTypeName")
                            }).ToList();
            }
            return stockRegiter;
        }

        public IEnumerable<StockRegisterEntity> StoreStockRegisterReport(DateTime fromdate, DateTime todate, int StoredId, int ItemTypeId, int itemid)
        {
            IEnumerable<StockRegisterEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoredId", StoredId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", itemid, DbType.Int32));
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.StorestockReport, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockRegisterEntity
                            {
                                ItemID = row.Field<int>("ItemId"),
                                ItemCode = row.Field<string>("Code"),
                                ItemName = row.Field<string>("Name"),
                                QtyReceived = row.Field<double>("QtyReceived"),
                                QtyIssued = row.Field<double>("QtyIssued"),
                                BalanceQty = row.Field<double>("BalanceQty"),
                                OpeningBalance = row.Field<double>("OpeningBalance"),
                                DocDate = row.Field<DateTime>("DocDate"),
                                DocId = row.Field<int>("docid"),
                                Description = row.Field<string>("Description"),
                                ReceiptNo = row.Field<string>("ReceiptNo"),
                                StoreName = row.Field<string>("StoreName"),
                                IssueNo = row.Field<string>("IssueNo"),
                                UnitName = row.Field<string>("UnitName"),
                                IssueToStore = row.Field<string>("ToStore"),
                                BatchName = row.Field<string>("BatchName"),

                                ExpiryDate = row.Field<DateTime>("Expirydate"),
                                MRP = row.Field<double>("Mrp"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                ItemType = row.Field<string>("ItemType")
                            }).ToList();
            }
            return stockRegiter;
        }

        public IEnumerable<StockRegisterEntity> StockEvaluationReport(DateTime todate, int StoredId, int ItemTypeId)
        {
            IEnumerable<StockRegisterEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AsOfDate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Storeid", StoredId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.StockEvaluationReport, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockRegisterEntity
                            {
                                ItemCode = row.Field<string>("ItemCode"),
                                ItemName = row.Field<string>("ItemName"),
                                BalanceQty = row.Field<double>("Stock"),
                                MRP = row.Field<double>("MRP"),
                                Amount = row.Field<double>("Amount"),
                                StoreName = row.Field<string>("StoreName"),
                                ItemType = row.Field<string>("ItemTypeName"),
                            }).ToList();
            }
            return stockRegiter;
        }

        public IEnumerable<StockRegisterEntity> StockConsumptionReport(DateTime fromdate, DateTime todate, int StoredId, int ItemTypeId)
        {
            IEnumerable<StockRegisterEntity> stockRegiter = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", StoredId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtstockRegiter = dbHelper.ExecuteDataTable(StoreQuery.StockConsumptionReport, paramCollection, CommandType.StoredProcedure);

                stockRegiter = dtstockRegiter.AsEnumerable()
                            .Select(row => new StockRegisterEntity
                            {
                                ItemID = row.Field<int>("ItemId"),
                                ItemCode = row.Field<string>("Code"),
                                ItemName = row.Field<string>("Name"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = row.Field<DateTime>("Expirydate"),
                                MRP = row.Field<double>("Mrp"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                ItemType = row.Field<string>("ItemType"),
                                StoreName = row.Field<string>("StoreName"),
                                DocDate = row.Field<DateTime>("DocDate"),
                                DocId = row.Field<int>("DocId"),
                                DocType = row.Field<string>("DocType"),
                                BalanceQty = row.Field<double>("Qty")                                
                            }).ToList();
            }
            return stockRegiter;
        }

        public IEnumerable<BatchEntity> ExpiryRegisterReport(DateTime fromdate, int MaxDays)
        {
            IEnumerable<BatchEntity> batchRegister = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MaxDays", MaxDays, DbType.Int32));
                DataTable dtbatch = dbHelper.ExecuteDataTable(StoreQuery.ExpiryRegisterReport, paramCollection, CommandType.StoredProcedure);

                batchRegister = dtbatch.AsEnumerable()
                            .Select(row => new BatchEntity
                            {
                                ID = row.Field<int>("BatchId"),
                                Name = row.Field<string>("BatchName"),
                                ItemName = row.Field<string>("ItemName"),
                                SupplierName = row.Field<string>("SupplierName"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                GRNNo = row.Field<string>("GRNNo"),
                                CurrentBal = row.Field<double>("CurrentBal"),
                                Rate = row.Field<double>("Rate"),
                                MRP = row.Field<double>("MRP"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return batchRegister;
        }



    }

}
