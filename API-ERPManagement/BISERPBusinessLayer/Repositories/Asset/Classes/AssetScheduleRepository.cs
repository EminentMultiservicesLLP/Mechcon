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
    public class AssetScheduleRepository : IAssetScheduleRepository
    {

        public AssetScheduleEntity SaveAssetSchedule(AssetScheduleEntity entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ScheduleId", entity.ScheduleId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("AssetId", entity.AssetId, DbType.Int32));
            paramCollection.Add(new DBParameter("IsFrequency", entity.IsFrequency, DbType.Boolean));
            paramCollection.Add(new DBParameter("MaintenanceTypeId", entity.MaintenanceTypeId, DbType.Int32));
            paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("ToDate", entity.ToDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("NoOfTimes", entity.NoOfTimes, DbType.Int32));
            paramCollection.Add(new DBParameter("ScheduleType", entity.ScheduleType, DbType.Int32));
            paramCollection.Add(new DBParameter("Interval", entity.Interval, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAssetSchedule, paramCollection, CommandType.StoredProcedure, "ScheduleId");
            entity.ScheduleId = iResult;
            return entity;
        }
        public IEnumerable<AssetScheduleDetailsEntity> GetAssetSchedule(DateTime fromdate, DateTime todate, int branchId)
        {
            List<AssetScheduleDetailsEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("branchId", branchId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetAssetSchedule, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new AssetScheduleDetailsEntity
                            {
                                ScheduleId = row.Field<int>("ScheduleId"),
                                ScheduleDetailId = row.Field<int>("ScheduleDetailId"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                ScheduleDate = row.Field<DateTime>("ScheduleDate"),
                                StrScheduleDate = Convert.ToDateTime(row.Field<DateTime>("ScheduleDate")).ToString("dd-MMMM-yyyy"),
                                MaintenanceTypeId = row.Field<int>("MaintenanceTypeId"),
                                MaintenanceType = row.Field<string>("MaintainanceType"),
                                TODO = row.Field<string>("TODO"),
                                Remark = row.Field<string>("Remark"),
                            }).ToList();
            }
            return type;
        }
        public int CreateAssetScheduleCompletion(AssetScheduleCompletionEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ScheduleId", entity.ScheduleId, DbType.Int32));
                paramCollection.Add(new DBParameter("ScheduleDetailId", entity.ScheduleDetailId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetId", entity.AssetId, DbType.Int32));
                paramCollection.Add(new DBParameter("MaintenanceTypeId", entity.MaintenanceTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ScheduleDate", Convert.ToDateTime(entity.StrScheduleDate), DbType.DateTime));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("TODO", entity.TODO, DbType.String));
                paramCollection.Add(new DBParameter("CompletedDate", entity.CompletedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("DoneBy", entity.DoneBy, DbType.String));
                paramCollection.Add(new DBParameter("CompletionRemark", entity.CompletionRemark, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsAssetScheduleCompletion, paramCollection, CommandType.StoredProcedure, "Id");
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndent, paramCollection, CommandType.StoredProcedure, "Indent_Id");
            }
            return iResult;
        }
        public IEnumerable<AssetEntity> GetBranchAssetsforschedule(int BranchId)
        {
            List<AssetEntity> room = new List<AssetEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtRoom = dbHelper.ExecuteDataTable(AssetQueries.GetBranchAssetsforschedule, paramCollection, CommandType.StoredProcedure);
                room = dtRoom.AsEnumerable()
                            .Select(row => new AssetEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                BranchId = row.Field<int>("BranchId"),
                                ItemName = row.Field<string>("ItemName"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                ModelNo = row.Field<string>("ModelNo"),
                                ItemId = row.Field<int>("ItemId"),
                                SupplierId = row.Field<int>("SupplierId"),
                                SupplierName = row.Field<string>("SupplierName"),
                            }).ToList();
            }
            return room;
        }
    }
}
