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
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        public IEnumerable<VehicleMakeEntity> GetAllVehicleMake()
        {
            List<VehicleMakeEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleMake, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VehicleMakeEntity
                            {
                                MakeId = row.Field<int>("MakeId"),
                                MakeName = row.Field<string>("MakeName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return type;
        }
        public int CreateVehicleMake(VehicleMakeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("MakeId", Entity.MakeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("MakeName", Entity.MakeName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_VehicleMake, paramCollection, CommandType.StoredProcedure, "MakeId");
            }
            return iResult;
        }
        public bool UpdateVehicleMake(VehicleMakeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("MakeId", Entity.MakeId, DbType.Int32));
                paramCollection.Add(new DBParameter("MakeName", Entity.MakeName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_VehicleMake, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<VehicleMakeEntity> GetActiveVehicleMake()
        {
            List<VehicleMakeEntity> VehicleModel = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.GetActiveVehicleMake, CommandType.StoredProcedure);
                VehicleModel = dtManufacturer.AsEnumerable()
                            .Select(row => new VehicleMakeEntity
                            {
                                MakeId = row.Field<int>("MakeId"),
                                MakeName = row.Field<string>("MakeName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (VehicleModel == null || VehicleModel.Count == 0)
                    VehicleModel.Add(new VehicleMakeEntity
                    {
                        MakeId = 0,
                        MakeName = "",
                        Deactive = false
                    });
            }
            return VehicleModel;
        }

    }
}
