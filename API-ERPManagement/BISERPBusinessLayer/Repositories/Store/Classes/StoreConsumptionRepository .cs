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
    public class StoreConsumptionRepository : IStoreConsumptionRepository
    {
        public IEnumerable<StoreConsumptionDetailsEntities> GetItemforConsume(int StoreId)
        {
            List<StoreConsumptionDetailsEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("pStoreId", StoreId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(StoreQuery.GetINV_ItemforConsume, param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreConsumptionDetailsEntities
                            {
                                ItemID = row.Field<int>("ItemID"),
                                BatchId = row.Field<int>("BatchId"),
                                Unit = row.Field<string>("Unit"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
                                strExpiryDate = Convert.ToDateTime(row.Field<DateTime?>("ExpiryDate")).ToString("dd-MMM-yyyy"),
                                ItemName = row.Field<string>("ItemName"),
                                Batch = row.Field<string>("Batch"),
                                StockQty = row.Field<double>("StockQty")

                            }).ToList();
            }
            return item;
        }
        public StoreConsumptionEntities CreateStockConsumption(StoreConsumptionEntities entity,DBHelper dbHelper)
        {
           
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Consumptionid", entity.ConsumptionId, DbType.Int32,ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Consumptioncode", entity.ConsumptionCode, DbType.String, 100, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Consumptiondate", entity.ConsumptionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Storeid ", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Insertedbyuserid", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Insertedon", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("Insertedipaddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacname", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacid", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Updatedby", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Updatedon", entity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("Updatedipaddress", entity.UpdatedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Updatedmacname ", entity.UpdatedMacName, DbType.String));
                paramCollection.Add(new DBParameter("Updatedmacid", entity.UpdatedMacID, DbType.String));
          
              
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsINVStoreConsumptionMaster, paramCollection,  CommandType.StoredProcedure);
                entity.ConsumptionId = Convert.ToInt32(parameterList["Consumptionid"].ToString());
                entity.ConsumptionCode = parameterList["Consumptioncode"].ToString();

               return entity;
        }
        public IEnumerable<StoreConsumptionEntities> GetALLConsumptionNo()
        {
            List<StoreConsumptionEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtCon = dbHelper.ExecuteDataTable(StoreQuery.GetAllConsumptionNO, CommandType.StoredProcedure);
                item = dtCon.AsEnumerable()
                            .Select(row => new StoreConsumptionEntities
                            {
                                ConsumptionId = row.Field<int>("ConsumptionId"),
                                ConsumptionCode = row.Field<string>("ConsumptionCode"),
                                ConsumptionDate = row.Field<DateTime?>("ConsumptionDate"),
                                strConsumptionDate = Convert.ToDateTime(row.Field<DateTime?>("ConsumptionDate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                store = row.Field<string>("Store"),
                            }).ToList();
            }
            return item;
        }


    }
}
