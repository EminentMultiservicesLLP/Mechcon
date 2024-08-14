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
    public class StockDisposeDetailsRepository : IStockDisposeDetailsRepository
    {
        public int StockDisposeDetails(StockDisposeEntity mainentity, StockDisposeDtEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DisposeDetailId", entity.DisposeDetailId, DbType.Int32));
                paramCollection.Add(new DBParameter("DisposeId", mainentity.DisposeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", mainentity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchID", entity.BatchID, DbType.Int32));
                paramCollection.Add(new DBParameter("DisposedQuantity", entity.DisposedQuantity, DbType.Double));
                paramCollection.Add(new DBParameter("Reason", entity.Reason, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName ", mainentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));

                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndentDtl, paramCollection, CommandType.StoredProcedure, "IndentDetails_Id");
                dbHelper.ExecuteNonQuery(StoreQuery.InsINV_StockDisposeDetail, paramCollection, CommandType.StoredProcedure);
                //iResult = (int)paramCollection.Get("IndentId").Value;
          
            return iResult;
        }
      
    }
}
