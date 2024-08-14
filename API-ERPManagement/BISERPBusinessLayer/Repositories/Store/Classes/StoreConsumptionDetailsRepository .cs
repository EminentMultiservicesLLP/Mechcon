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
    public class StoreConsumptionDetailsRepository : IStoreConsumptionDetailsRepository
    {
        public int StockConsumptionDetails(StoreConsumptionEntities mainentity, StoreConsumptionDetailsEntities entity,DBHelper dbHelper)
        {
            int iResult = 0;
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Consumptiondtlid", entity.ConsumptionDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("Consumptionid", mainentity.ConsumptionId, DbType.Int32));
                paramCollection.Add(new DBParameter("Itemid", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", mainentity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Batchid", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Consumeqty", entity.ConsumeQty, DbType.Double));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", mainentity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedON", mainentity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", mainentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName ", mainentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", mainentity.InsertedMacID, DbType.String));

                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndentDtl, paramCollection, CommandType.StoredProcedure, "IndentDetails_Id");
                dbHelper.ExecuteNonQuery(StoreQuery.InsINV_StoreConsumptionDetail, paramCollection, CommandType.StoredProcedure);
                //iResult = (int)paramCollection.Get("IndentId").Value;
          
            return iResult;
        }
        public IEnumerable<StoreConsumptionCancelEntities> GetConsumptionDT(int Id)
        {
            List<StoreConsumptionCancelEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ConsumptionId", Id, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(StoreQuery.GetAllConsumptiondt, param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreConsumptionCancelEntities
                            {
                                Itemid = row.Field<int?>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                Batchid = row.Field<int?>("BatchId"),
                                Batch = row.Field<string>("Batch"),
                                Unit = row.Field<string>("UnitName"),
                                StockQty = row.Field<double>("StockQty"),
                                ConsumeQty = row.Field<double>("ConsumeQty"),
                                Remark = row.Field<string>("Remark"),
                                ConsumptionDtlid = row.Field<int?>("ConsumptionDtlId"),
                                Consumptionid = row.Field<int?>("ConsumptionId"),
                                Storeid = row.Field<int>("StoreId"),
                                code = row.Field<string>("ConsumptionCode"),
                            }).ToList();
            }
            return item;
        }
        public List<StoreConsumptionCancelEntities> CreateStockcancel(List<StoreConsumptionCancelEntities> entity)
        {    
          foreach (var details in entity)
            {
               using (DBHelper dbHelper = new DBHelper())
               {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ConCancelid", details.ConCancelid, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("Consumptionid", details.Consumptionid, DbType.Int32));
                    paramCollection.Add(new DBParameter("ConsumptionDtlid", details.ConsumptionDtlid, DbType.Int32));
                    paramCollection.Add(new DBParameter("code", details.code, DbType.String));
                    paramCollection.Add(new DBParameter("Itemid", details.Itemid, DbType.Int32));
                    paramCollection.Add(new DBParameter("Batchid ", details.Batchid, DbType.Int32));
                    paramCollection.Add(new DBParameter("Storeid", details.Storeid, DbType.Int32));
                    paramCollection.Add(new DBParameter("Qty", details.Qty, DbType.Double));
                    paramCollection.Add(new DBParameter("Cancelledby", details.Cancelledby, DbType.Int32));
                    paramCollection.Add(new DBParameter("Remark", details.Remark, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedBy", details.InsertedBy, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", details.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", details.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacID", details.InsertedMacID, DbType.String));

                    dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsINV_StoreConsumptioncancel, paramCollection, CommandType.StoredProcedure, "ConCancelid");
               }
            }
          return entity;
        }
    }
}
