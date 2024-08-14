using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class SparePartsOuthouseUtilRepository : ISparePartsOuthouseUtilRepository
    {
        public int CreateSparePartsOuthouseUtilRepository(OutsideMaintenanceEntity MainEntity, SparePartsOuthouseUtilEntity Entity, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", Entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("OutHouseId", MainEntity.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemName", Entity.ItemName, DbType.String));
                paramCollection.Add(new DBParameter("Qty", Entity.Qty, DbType.Double));
                paramCollection.Add(new DBParameter("Cost", Entity.Cost, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", MainEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", MainEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", MainEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", MainEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", MainEntity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdSpareOuthouse, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }
    }
}
