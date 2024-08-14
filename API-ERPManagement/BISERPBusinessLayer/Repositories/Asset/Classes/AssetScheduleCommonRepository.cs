using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Asset;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetScheduleCommonRepository : IAssetScheduleCommonRepository
    {
        IAssetScheduleRepository _assetSchedule;
        IAssetScheduleDetailRepository _assetScheduleDt;
        private static readonly ILogger _loggger = Logger.Register(typeof(AssetScheduleCommonRepository));

        public AssetScheduleCommonRepository(IAssetScheduleRepository assetSchedule, IAssetScheduleDetailRepository assetScheduleDt)
        {
            _assetSchedule = assetSchedule;
            _assetScheduleDt = assetScheduleDt;
        }
        public AssetScheduleEntity SaveAssetSchedule(AssetScheduleEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _assetSchedule.SaveAssetSchedule(entity, dbhelper);
                    entity.ScheduleId = newEntity.ScheduleId;
                    if (entity.ScheduleId > 0)
                    {
                        foreach (var dt in entity.schDetails)
                        {
                            dt.ScheduleDetailId = _assetScheduleDt.SaveAssetScheduleDetails(entity, dt, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }                        
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveAssetSchedule method of AssetScheduleCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }



        public IEnumerable<AssetScheduleEntity> AssetScheduleReport(DateTime fromdate, DateTime todate, int MaintenanceTypeId)
        {
            List<AssetScheduleEntity> Schedule = new List<AssetScheduleEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MaintenanceTypeId", MaintenanceTypeId, DbType.Int32));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(AssetQueries.AssetScheduleReport, paramCollection, CommandType.StoredProcedure);

                Schedule = dtMaterialIssue.AsEnumerable()
                            .Select(row => new AssetScheduleEntity
                            {
                                ScheduleId = row.Field<int>("ScheduleId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                IsFrequency = row.Field<bool>("IsFrequency"),
                                MaintenanceType = row.Field<string>("MaintainanceType"),
                                BranchName = row.Field<string>("BranchName"),
                            }).GroupBy(test => test.ScheduleId).Select(grp => grp.First()).ToList();

                foreach (var M in Schedule)
                {
                    M.schDetails = dtMaterialIssue.AsEnumerable().Select(dtrow => new AssetScheduleDetailsEntity
                    {
                        ScheduleId = dtrow.Field<int>("ScheduleId"),
                        ScheduleDetailId = dtrow.Field<int>("ScheduleDetailId"),
                        Remark = dtrow.Field<string>("Remark"),
                        TODO = dtrow.Field<string>("TODO"),
                        ScheduleDate = dtrow.Field<DateTime>("ScheduleDate")
                    }).Where(mo => mo.ScheduleId == M.ScheduleId).ToList();
                }
            }
            return Schedule;
        }
    }
}
