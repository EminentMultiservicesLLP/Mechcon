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
    public class StockDiposeRepository : IStockDisposeRepository
    {

        public StockDisposeEntity CreateStockdispose(StockDisposeEntity entity, DBHelper dbHelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("DisposeId", entity.DisposeId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("DisposeNo", entity.DisposeNo, DbType.String, 100, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("DisposeDate", entity.DisposeDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("StoreId ", entity.StoreId, DbType.Int32));
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

            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsINVStockDisposeMaster, paramCollection,  CommandType.StoredProcedure);
            entity.DisposeId = Convert.ToInt32(parameterList["DisposeId"].ToString());
            entity.DisposeNo = parameterList["DisposeNo"].ToString();
            return entity;
        }
      
    }
}
