using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        public IEnumerable<VehicleModelEntity> GetAllVehicleModel()
        {
            List<VehicleModelEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleModel, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VehicleModelEntity
                            {
                                ModelId = row.Field<int>("ModelId"),
                                ModelCode = row.Field<string>("ModelCode"),
                                ModelName = row.Field<string>("ModelName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return type;
        }
        public int CreateVehicleModel(VehicleModelEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("ModelId", Entity.ModelId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ModelCode", Entity.ModelCode, DbType.String));
                paramCollection.Add(new DBParameter("ModelName", Entity.ModelName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_VehicleModel, paramCollection, CommandType.StoredProcedure, "ModelId");
            }
            return iResult;
        }
        public bool UpdateVehicleModel(VehicleModelEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ModelId", Entity.ModelId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelCode", Entity.ModelCode, DbType.String));
                paramCollection.Add(new DBParameter("ModelName", Entity.ModelName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_VehicleModel, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<VehicleModelEntity> GetActiveVehicleModel()
        {
            List<VehicleModelEntity> VehicleModel = null;
            using (DBHelper dbHelper = new DBHelper())
            {                
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.GetActiveVehicleModel, CommandType.StoredProcedure);
                VehicleModel = dtManufacturer.AsEnumerable()
                            .Select(row => new VehicleModelEntity
                            {
                                ModelId = row.Field<int>("ModelId"),
                                ModelCode = row.Field<string>("ModelCode"),
                                ModelName = row.Field<string>("ModelName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (VehicleModel == null || VehicleModel.Count == 0)
                    VehicleModel.Add(new VehicleModelEntity
                    {
                        ModelId = 0,
                        ModelCode = "",
                        ModelName = "",
                        Deactive = false
                    });
            }
            return VehicleModel;
        }

    }
  
}
