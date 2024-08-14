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
    public class FloorRespository : IFloorRespository
    {
        public IEnumerable<FloorEntity> GetAllFloor()
        {
            List<FloorEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllFloor, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new FloorEntity
                            {
                                FloorId = row.Field<int>("FloorId"),
                                FloorCode = row.Field<string>("FloorCode"),
                                FloorName = row.Field<string>("FloorName"),
                                LocationId = row.Field<int>("LocationId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (units == null || units.Count == 0)
                    units.Add(new FloorEntity
                    {
                        FloorId = 0,
                        FloorCode = "",
                        FloorName = "",
                        BranchId =0,
                        LocationId = 0
                    });
            }
            return units;
        }
        public IEnumerable<FloorEntity> GetActiveFloor()
        {
            List<FloorEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
              DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.AstGetActiveFloor, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new FloorEntity
                            {
                                FloorId = row.Field<int>("FloorId"),
                                FloorCode = row.Field<string>("FloorCode"),
                                FloorName = row.Field<string>("FloorName"),
                                LocationId = row.Field<int>("LocationId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }
        public IEnumerable<FloorEntity> GetBranchFloor()
        {
            List<FloorEntity> division = new List<FloorEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.AstGetBranchFloor, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new FloorEntity
                            {
                                FloorId = row.Field<int>("FloorId"),
                                FloorCode = row.Field<string>("FloorCode"),
                                FloorName = row.Field<string>("FloorName"),
                                LocationId = row.Field<int>("LocationId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }

        public IEnumerable<FloorEntity> GetActiveLocationWiseFloor(int LocationId)
        {
            List<FloorEntity> division = new List<FloorEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("LocationId", LocationId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetActiveLocationWiseFloor, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new FloorEntity
                            {
                                FloorId = row.Field<int>("FloorId"),
                                FloorCode = row.Field<string>("FloorCode"),
                                FloorName = row.Field<string>("FloorName"),
                                LocationId = row.Field<int>("LocationId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }

        public int CreateFloor(FloorEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("FloorCode", Entity.FloorCode, DbType.String));
                paramCollection.Add(new DBParameter("FloorName", Entity.FloorName, DbType.String));
                paramCollection.Add(new DBParameter("LocationId", Entity.LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstFloor, paramCollection, CommandType.StoredProcedure, "FloorId");
            }
            return iResult;
        }
        public bool UpdateFloor(FloorEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorCode", Entity.FloorCode, DbType.String));
                paramCollection.Add(new DBParameter("FloorName", Entity.FloorName, DbType.String));
                paramCollection.Add(new DBParameter("LocationId", Entity.LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstFloor, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<FloorEntity> GetBranchLocationFloor(int BranchId)
        {
            List<FloorEntity> division = new List<FloorEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetActiveLocationWiseFloor, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new FloorEntity
                            {
                                FloorId = row.Field<int>("FloorId"),
                                FloorCode = row.Field<string>("FloorCode"),
                                FloorName = row.Field<string>("FloorName"),
                                LocationId = row.Field<int>("LocationId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }
    }
}
