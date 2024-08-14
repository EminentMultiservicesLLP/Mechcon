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
    public class VehicleusageRepository : IVehicleusageRepository
    {
        public IEnumerable<VehicleUsageEntity> GetAllVehicleUsage()
        {
            List<VehicleUsageEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleUsage, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VehicleUsageEntity
                            {
                                Id = row.Field<int>("Id"),
                                Code = row.Field<string>("Code"),
                                UsedFor = row.Field<string>("UsedFor"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return type;
        }
        public int CreateVehicleUsage(VehicleUsageEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("UsedFor", Entity.UsedFor, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_VehicleUsage, paramCollection, CommandType.StoredProcedure, "Id");
            }
            return iResult;
        }
        public bool UpdateVehicleUsage(VehicleUsageEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("UsedFor", Entity.UsedFor, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_VehicleUsage, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<VehicleUsageEntity> GetActiveVehicleUsage(int UserId)
        {
            List<VehicleUsageEntity> VehicleModel = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.GetActiveVehicleUsage, paramCollection, CommandType.StoredProcedure);
                VehicleModel = dtManufacturer.AsEnumerable()
                            .Select(row => new VehicleUsageEntity
                            {
                                Id = row.Field<int>("Id"),
                                Code = row.Field<string>("Code"),
                                UsedFor = row.Field<string>("UsedFor"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (VehicleModel == null || VehicleModel.Count == 0)
                    VehicleModel.Add(new VehicleUsageEntity
                    {
                        Id = 0,
                        Code = "",
                        UsedFor = "",
                        Deactive = false
                    });
            }
            return VehicleModel;
        }

    }
}
