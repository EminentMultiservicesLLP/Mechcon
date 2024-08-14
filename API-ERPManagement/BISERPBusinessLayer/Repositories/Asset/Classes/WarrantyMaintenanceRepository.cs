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
    public class WarrantyMaintenanceRepository : IWarrantyMaintenanceRepository
    {
        public WarrantyMaintenanceEntity CreateWarrantyMaintenance(WarrantyMaintenanceEntity Entity, DBHelper dbhelper)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.WarrantyId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RequisitionId", Entity.RequisitionId, DbType.Int32));
                paramCollection.Add(new DBParameter("MaintenanceDate", Entity.MaintenanceDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MaintenanceTime", Entity.MaintenanceTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("DownTime", Entity.DownTime, DbType.String));
                paramCollection.Add(new DBParameter("Days", Entity.Days, DbType.String));
                paramCollection.Add(new DBParameter("MaintenanceCost", Entity.MaintenanceCost, DbType.String));
                paramCollection.Add(new DBParameter("ActualFault", Entity.ActualFault, DbType.String));
                paramCollection.Add(new DBParameter("DoneBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("MaintenanceType", Entity.MaintenanceType, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(AssetQueries.InsUpdWarrantyMaintenance, paramCollection,  CommandType.StoredProcedure);
                Entity.WarrantyId = Convert.ToInt32(parameterList["Id"].ToString());
                Entity.Code = parameterList["Code"].ToString();
            }
            return Entity;
        }
    }
}
