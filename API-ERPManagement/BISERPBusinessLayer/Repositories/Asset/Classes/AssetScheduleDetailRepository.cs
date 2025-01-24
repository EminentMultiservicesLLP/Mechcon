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
    public class AssetScheduleDetailRepository : IAssetScheduleDetailRepository
    {

        public int SaveAssetScheduleDetails(AssetScheduleEntity mainentity, AssetScheduleDetailsEntity entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ScheduleDetailId", entity.ScheduleDetailId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IsFrequency", entity.IsFrequency, DbType.Boolean));
            paramCollection.Add(new DBParameter("MaintenanceTypeId", entity.MaintenanceTypeId, DbType.Int32));
            paramCollection.Add(new DBParameter("ScheduleDate", entity.ScheduleDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
            paramCollection.Add(new DBParameter("TODO", entity.TODO, DbType.String));
            paramCollection.Add(new DBParameter("ScheduleId", mainentity.ScheduleId, DbType.Int32));
            paramCollection.Add(new DBParameter("ScheduleTypeId", entity.ScheduleTypeId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", mainentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAssetScheduleDetails, paramCollection, CommandType.StoredProcedure, "ScheduleDetailId");
            return iResult;
        }
        //public IEnumerable<AssetScheduleDetailsEntity> GetAssetcodeScheduledt(int ScheduleId)
        //{
        //    List<AssetScheduleDetailsEntity> type = null;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DBParameterCollection paramCollection = new DBParameterCollection();

        //        paramCollection.Add(new DBParameter("ScheduleId", ScheduleId, DbType.Int32));
        //        DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetAssetScheduledt, paramCollection, CommandType.StoredProcedure);
        //        type = dtType.AsEnumerable()
        //                    .Select(row => new AssetScheduleDetailsEntity
        //                    {
        //                        ScheduleId = row.Field<int>("ScheduleId"),
        //                        ScheduleDate = row.Field<DateTime>("ScheduleDate"),
        //                        StrScheduleDate = Convert.ToDateTime(row.Field<DateTime>("ScheduleDate")).ToString("dd-MMM-yyyy"),
        //                        MaintenanceTypeId = row.Field<int>("MaintenanceTypeId"),
        //                        MaintenanceType = row.Field<string>("MaintainanceType"),
        //                        TODO = row.Field<string>("TODO"),
        //                        Remark = row.Field<string>("Remark"),
        //                    }).ToList();
        //    }
        //    return type;
        //}
        public IEnumerable<AssetScheduleDetailsEntity> AMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> AMC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(AssetQueries.AlertAmc, param, CommandType.StoredProcedure);
                AMC = dtInsurance.AsEnumerable()
                            .Select(row => new AssetScheduleDetailsEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                ScheduleDate = row.Field<DateTime>("ScheduleDate"),
                                StrScheduleDate = Convert.ToDateTime(row.Field<DateTime>("ScheduleDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return AMC;
        }
        public IEnumerable<AssetScheduleDetailsEntity> CMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> AMC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(AssetQueries.AlertCmc, param, CommandType.StoredProcedure);
                AMC = dtInsurance.AsEnumerable()
                            .Select(row => new AssetScheduleDetailsEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                ScheduleDate = row.Field<DateTime>("ScheduleDate"),
                                StrScheduleDate = Convert.ToDateTime(row.Field<DateTime>("ScheduleDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return AMC;
        }
      
        public IEnumerable<AssetScheduleDetailsEntity> OtherNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> AMC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(AssetQueries.AlertOtherNotification, param, CommandType.StoredProcedure);
                AMC = dtInsurance.AsEnumerable()
                            .Select(row => new AssetScheduleDetailsEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                ScheduleDate = row.Field<DateTime>("ScheduleDate"),
                                MaintenanceType = row.Field<string>("MaintenanceType"), 
                                StrScheduleDate = Convert.ToDateTime(row.Field<DateTime>("ScheduleDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return AMC;
        }
    

    }
}
