using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class OpeningBalanceRepository : IOpeningBalanceRepository
    {
        public int CreateOpeningBalance(OpeningBalanceEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {                 
                if (entity.ExpiryDate == null) {
                    entity.ExpiryDate = new DateTime(2099, 01, 01);
                }
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Storeid", entity.Storeid, DbType.Int32));
                paramCollection.Add(new DBParameter("Itemid", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("Name", (string.IsNullOrWhiteSpace(entity.BatchName) ? Convert.ToString(entity.ItemName) : entity.BatchName), DbType.String));
                paramCollection.Add(new DBParameter("OpeningBal", entity.OpeningBal, DbType.Double));
                paramCollection.Add(new DBParameter("Expirydate",entity.ExpiryDate,DbType.DateTime));
                paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
                paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));

                //paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateOpeningBalance, paramCollection, CommandType.StoredProcedure);
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndent, paramCollection, CommandType.StoredProcedure, "Indent_Id");
            }
            return iResult;
        }
    }
}
