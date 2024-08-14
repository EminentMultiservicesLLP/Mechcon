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
    public class RoomRepository : IRoomRepository
    {
        public IEnumerable<RoomEntity> GetAllRoom()
        {
            List<RoomEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllRoom, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new RoomEntity
                            {
                                RoomId = row.Field<int>("RoomId"),
                                RoomCode = row.Field<string>("RoomCode"),
                                RoomName = row.Field<string>("RoomName"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (units == null || units.Count == 0)
                    units.Add(new RoomEntity
                    {
                        RoomId = 0,
                        RoomCode = "",
                        RoomName = "",
                        FloorId = 0,
                        BranchId = 0
                    });
            }
            return units;
        }
        public IEnumerable<RoomEntity> GetActiveRoom(int UserId)
        {
            List<RoomEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetActiveRoom, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new RoomEntity
                            {
                                RoomId = row.Field<int>("RoomId"),
                                RoomCode = row.Field<string>("RoomCode"),
                                RoomName = row.Field<string>("RoomName"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (division == null || division.Count == 0)
                    division.Add(new RoomEntity
                    {
                        RoomId = 0,
                        RoomCode = "",
                        RoomName = "",
                        FloorId = 0,
                        BranchId = 0
                    });
            }
            return division;
        }

        public IEnumerable<RoomEntity> GetFloorRoom(int FloorId)
        {
            List<RoomEntity> room = new List<RoomEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FloorId", FloorId, DbType.Int32));
                DataTable dtRoom = dbHelper.ExecuteDataTable(AssetQueries.GetFloorRoom, paramCollection, CommandType.StoredProcedure);
                room = dtRoom.AsEnumerable()
                            .Select(row => new RoomEntity
                            {
                                RoomId = row.Field<int>("RoomId"),
                                RoomCode = row.Field<string>("RoomCode"),
                                RoomName = row.Field<string>("RoomName"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return room;
        }
        public int CreateRoom(RoomEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RoomId", Entity.RoomId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RoomCode", Entity.RoomCode, DbType.String));
                paramCollection.Add(new DBParameter("RoomName", Entity.RoomName, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstRoom, paramCollection, CommandType.StoredProcedure, "RoomId");
            }
            return iResult;
        }
        public bool UpdateRoom(RoomEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RoomId", Entity.RoomId, DbType.Int32));
                paramCollection.Add(new DBParameter("RoomCode", Entity.RoomCode, DbType.String));
                paramCollection.Add(new DBParameter("RoomName", Entity.RoomName, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstRoom, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
