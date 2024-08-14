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
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        public IEnumerable<VehicleTypeEntity> GetAllVehicleType()
        {
            List<VehicleTypeEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleType, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VehicleTypeEntity
                            {
                                TypeId = row.Field<int>("TypeId"),
                                TypeCode = row.Field<string>("TypeCode"),
                                TypeName = row.Field<string>("TypeName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return type;
        }
        public int CreateVehicleType(VehicleTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("TypeId", Entity.TypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("TypeCode", Entity.TypeCode, DbType.String));
                paramCollection.Add(new DBParameter("TypeName", Entity.TypeName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_VehicleType, paramCollection, CommandType.StoredProcedure, "TypeId");
            }
            return iResult;
        }
        public bool UpdateVehicleType(VehicleTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TypeId", Entity.TypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("TypeCode", Entity.TypeCode, DbType.String));
                paramCollection.Add(new DBParameter("TypeName", Entity.TypeName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_VehicleType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<VehicleTypeEntity> GetActiveVehicleType(int UserId)
        {
            List<VehicleTypeEntity> VehicleModel = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.GetActiveVehicleType, paramCollection, CommandType.StoredProcedure);
                VehicleModel = dtManufacturer.AsEnumerable()
                            .Select(row => new VehicleTypeEntity
                            {
                                TypeId = row.Field<int>("TypeId"),
                                TypeCode = row.Field<string>("TypeCode"),
                                TypeName = row.Field<string>("TypeName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (VehicleModel == null || VehicleModel.Count == 0)
                    VehicleModel.Add(new VehicleTypeEntity
                    {
                        TypeId = 0,
                        TypeCode = "",
                        TypeName = "",
                        Deactive = false
                    });
            }
            return VehicleModel;
        }

    }
}
