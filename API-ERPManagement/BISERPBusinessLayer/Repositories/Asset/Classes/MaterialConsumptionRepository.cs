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
    public class MaterialConsumptionRepository : IMaterialConsumptionRepository
    {
        public int CreateMaterialConsumption(InHouseEntity MainEntity, MaterialConsumptionEntity Entity, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ConsumptionId", Entity.ConsumptionId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("InHouseId", MainEntity.InHouseId, DbType.String));
                paramCollection.Add(new DBParameter("ItemName", Entity.ItemName, DbType.String));
                paramCollection.Add(new DBParameter("Qty", Entity.Qty, DbType.Double));
                paramCollection.Add(new DBParameter("Cost", Entity.Cost, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", MainEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", MainEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", MainEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", MainEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", MainEntity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdMaterialConsumption, paramCollection, CommandType.StoredProcedure, "ConsumptionId");
            }
            return iResult;
        }

        public IEnumerable<MaterialConsumptionEntity> GetMaterialConsumption(int InHouseId)
        {
            List<MaterialConsumptionEntity> mconsume = new List<MaterialConsumptionEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("InHouseId", InHouseId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetMaterialConsumption, param, CommandType.StoredProcedure);
                mconsume = dtType.AsEnumerable()
                            .Select(row => new MaterialConsumptionEntity
                            {
                                ConsumptionId = row.Field<int>("ConsumptionId"),
                                //ItemId = row.Field<int>("ItemId"),                               
                                InHouseId = row.Field<int>("InHouseId"),
                                ItemName = row.Field<string>("ItemName"),
                                Cost = row.Field<double>("Cost"),
                                Qty = row.Field<double>("Qty")
                            }).ToList();
            }
            return mconsume;
        }
    }
}
