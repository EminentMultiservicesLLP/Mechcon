using BISERPBusinessLayer.Entities.TimingPlan;
using BISERPBusinessLayer.QueryCollection.TimingPlan;
using BISERPBusinessLayer.Repositories.TimingPlan.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BISERPBusinessLayer.Repositories.TimingPlan.Classes
{
    public class TimingPlanRepository : ITimingPlanRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(TimingPlanRepository));

        #region Schedule
        public IEnumerable<TP_TaskMaster> GetTaskMaster()
        {
            List<TP_TaskMaster> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetTaskMaster, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_TaskMaster
                            {
                                TaskID = row.Field<int?>("TaskID"),
                                TaskName = row.Field<string>("TaskName"),
                                TaskDescription = row.Field<string>("TaskDescription"),
                                IsActive = row.Field<bool>("IsActive")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<TP_ProjectTaskSchedule> GetProjectTaskSchedule(int ProjectID)
        {
            List<TP_ProjectTaskSchedule> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ProjectID", ProjectID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetProjectTaskSchedule, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_ProjectTaskSchedule
                            {
                                ScheduleID = row.Field<int?>("ScheduleID"),
                                ProjectID = row.Field<int?>("ProjectID"),
                                TaskID = row.Field<int?>("TaskID"),
                                TaskName = row.Field<string>("TaskName"),
                                TaskDescription = row.Field<string>("TaskDescription"),
                                AssignedTo = row.Field<int?>("AssignedTo"),
                                AssignedToName = row.Field<string>("AssignedToName"),
                                State = row.Field<bool>("State")
                            }).ToList();
            }
            return data;
        }
        public TP_PTSchedule SaveProjectTaskSchedule(TP_PTSchedule entity)
        {
            using (var dbhelper = new DBHelper())
            using (var transaction = dbhelper.BeginTransaction())
            {
                try
                {
                    SaveProjectTaskRole(entity, dbhelper);

                    foreach (var schedule in entity.TP_PTScheduleList)
                    {
                        schedule.CreatedBy = entity.LoginId;
                        var savedSchedule = SavePTSchedule(schedule, dbhelper);

                        foreach (var detail in schedule.PTDetails)
                        {
                            detail.UpdatedBy = entity.LoginId;
                            detail.DetailID = SavePTScheduleDetails(savedSchedule.ScheduleID, detail, dbhelper);
                        }
                    }

                    entity.isDeleted = FinalizeTaskScheduleAfterSave(entity.TP_PTScheduleList.FirstOrDefault()?.ProjectID, dbhelper);

                    dbhelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbhelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveProjectTaskSchedule_v2: " + Environment.NewLine + ex);
                    throw;
                }
            }

            return entity;
        }
        public int SaveProjectTaskRole(TP_PTSchedule entity, DBHelper dbhelper)
        {
            var paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("isSaved", 0, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("ProjectID", entity.ProjectID, DbType.Int32));
            paramCollection.Add(new DBParameter("ProjectOwnerID", entity.ProjectOwnerID, DbType.Int32));
            paramCollection.Add(new DBParameter("ProjectHeadID", entity.ProjectHeadID, DbType.Int32));
            paramCollection.Add(new DBParameter("ProjectEngineerID", entity.ProjectEngineerID, DbType.Int32));
            paramCollection.Add(new DBParameter("LoginId", entity.LoginId, DbType.Int32));

            return dbhelper.ExecuteNonQueryForOutParameter<int>(
                TimingPlanQueries.SaveProjectTaskRole,
                paramCollection,
                CommandType.StoredProcedure,
                "isSaved"
            );
        }
        public TP_ProjectTaskSchedule SavePTSchedule(TP_ProjectTaskSchedule entity, DBHelper dbhelper)
        {
            var paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("ScheduleID", entity.ScheduleID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("ProjectID", entity.ProjectID, DbType.Int32));
            paramCollection.Add(new DBParameter("TaskID", entity.TaskID, DbType.Int32));
            paramCollection.Add(new DBParameter("AssignedToName", entity.AssignedToName, DbType.String));
            paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.String));

            var outputParams = dbhelper.ExecuteNonQueryForOutParameter(
                TimingPlanQueries.SavePTSchedule,
                paramCollection,
                CommandType.StoredProcedure
            );

            entity.ScheduleID = Convert.ToInt32(outputParams["ScheduleID"]);
            return entity;
        }
        public int SavePTScheduleDetails(int? scheduleID, TP_ProjectTaskScheduleDetails entity, DBHelper dbhelper)
        {
            var paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("DetailID", entity.DetailID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("ScheduleID", scheduleID, DbType.Int32));
            paramCollection.Add(new DBParameter("DateKey", entity.DateKey, DbType.String));
            paramCollection.Add(new DBParameter("Date", entity.Date, DbType.String));
            paramCollection.Add(new DBParameter("IsSchedule", entity.IsSchedule, DbType.Boolean));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.String));

            return dbhelper.ExecuteNonQueryForOutParameter<int>(
                TimingPlanQueries.SavePTScheduleDetails,
                paramCollection,
                CommandType.StoredProcedure,
                "DetailID"
            );
        }
        public int FinalizeTaskScheduleAfterSave(int? projectID, DBHelper dbhelper)
        {
            var paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("isDeleted", 0, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("ProjectID", projectID, DbType.Int32));

            return dbhelper.ExecuteNonQueryForOutParameter<int>(
                TimingPlanQueries.FinalizeTaskScheduleAfterSave,
                paramCollection,
                CommandType.StoredProcedure,
                "isDeleted"
            );
        }
        public IEnumerable<TP_PTSchedule> GetProjectTaskRole(int ProjectID)
        {
            List<TP_PTSchedule> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ProjectID", ProjectID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetProjectTaskRole, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_PTSchedule
                            {
                                ProjectOwnerID = row.Field<int?>("ProjectOwnerID"),
                                ProjectHeadID = row.Field<int?>("ProjectHeadID"),
                                ProjectEngineerID = row.Field<int?>("ProjectEngineerID")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<TP_ProjectTaskScheduleDetails> GetProjectTaskScheduleDetails(int ProjectID)
        {
            List<TP_ProjectTaskScheduleDetails> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ProjectID", ProjectID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetProjectTaskScheduleDetails, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_ProjectTaskScheduleDetails
                            {
                                ScheduleID = row.Field<int?>("ScheduleID"),
                                DetailID = row.Field<int?>("DetailID"),
                                DateKey = row.Field<string>("DateKey"),
                                Date = row.Field<string>("Date"),
                                IsSchedule = row.Field<bool>("IsSchedule")
                            }).ToList();
            }
            return data;
        }
        #endregion Schedule

        #region TaskProgress
        public IEnumerable<TP_ProjectTaskSchedule> GetTaskProgress(int IsCompleted, int LoginId)
        {
            List<TP_ProjectTaskSchedule> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("IsCompleted", IsCompleted, DbType.String));
                param.Add(new DBParameter("LoginId", LoginId, DbType.Int32));

                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetTaskProgress, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_ProjectTaskSchedule
                            {
                                ScheduleID = row.Field<int?>("ScheduleID"),
                                ProjectID = row.Field<int?>("ProjectID"),
                                ProjectName = row.Field<string>("ProjectName"),
                                TaskID = row.Field<int?>("TaskID"),
                                TaskName = row.Field<string>("TaskName"),
                                TaskDescription = row.Field<string>("TaskDescription"),
                                strStartDate = row.Field<DateTime?>("StartDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("StartDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                strEndDate = row.Field<DateTime?>("EndDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EndDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                ProgressPercent = row.Field<decimal?>("ProgressPercent"),
                                Status = row.Field<string>("Status"),
                                Reason = row.Field<string>("Reason"),
                                Action = row.Field<string>("Action")
                            }).ToList();
            }
            return data;
        }
        public TP_ProjectTaskSchedule SaveTaskProgress(TP_ProjectTaskSchedule entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleID", entity.ScheduleID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProgressPercent", entity.ProgressPercent, DbType.Decimal));
                    paramCollection.Add(new DBParameter("Status", entity.Status, DbType.String));
                    paramCollection.Add(new DBParameter("Reason", entity.Reason, DbType.String));
                    paramCollection.Add(new DBParameter("Action", entity.Action, DbType.String));
                    paramCollection.Add(new DBParameter("LastActionBy", entity.LastActionBy, DbType.Int32));
                    var parameterList = dbHelper.ExecuteScalar(TimingPlanQueries.SaveTaskProgress, paramCollection, CommandType.StoredProcedure);
                    entity.ScheduleID = Convert.ToInt32(parameterList.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveTaskProgress method of TimingPlanRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        #endregion TaskProgress

        #region TaskMonitor
        public IEnumerable<TP_ProjectTaskSchedule> GetTaskMonitor(int ProjectID)
        {
            List<TP_ProjectTaskSchedule> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ProjectID", ProjectID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetTaskMonitor, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_ProjectTaskSchedule
                            {
                                ScheduleID = row.Field<int?>("ScheduleID"),
                                ProjectID = row.Field<int?>("ProjectID"),
                                ProjectName = row.Field<string>("ProjectName"),
                                TaskID = row.Field<int?>("TaskID"),
                                TaskName = row.Field<string>("TaskName"),
                                TaskDescription = row.Field<string>("TaskDescription"),
                                AssignedTo = row.Field<int?>("AssignedTo"),
                                AssignedToName = row.Field<string>("AssignedToName"),
                                strStartDate = row.Field<DateTime?>("StartDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("StartDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                strEndDate = row.Field<DateTime?>("EndDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EndDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                ProgressPercent = row.Field<decimal?>("ProgressPercent"),
                                Status = row.Field<string>("Status"),
                                DeliveryStatus = row.Field<string>("DeliveryStatus"),
                                Reason = row.Field<string>("Reason"),
                                Action = row.Field<string>("Action"),
                                LastActionByName = row.Field<string>("LastActionByName"),
                                strLastActionDate = row.Field<DateTime?>("LastActionDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("LastActionDate")).ToString("dd-MMM-yyyy") : string.Empty

                            }).ToList();
            }
            return data;
        }
        #endregion TaskMonitor

        #region TaskTracking
        public IEnumerable<TP_ProjectTaskSchedule> GetTaskTracking(int ProjectID)
        {
            List<TP_ProjectTaskSchedule> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ProjectID", ProjectID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TimingPlanQueries.GetTaskTracking, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TP_ProjectTaskSchedule
                            {
                                TaskID = row.Field<int?>("TaskID"),
                                TaskName = row.Field<string>("TaskName"),
                                TaskDescription = row.Field<string>("TaskDescription"),
                                AssignedToName = row.Field<string>("AssignedToName"),
                                strStartDate = row.Field<DateTime?>("StartDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("StartDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                strEndDate = row.Field<DateTime?>("EndDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EndDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                strLastActionDate = row.Field<DateTime?>("LastActionDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("LastActionDate")).ToString("dd-MMM-yyyy") : string.Empty,
                                ScheduledWeeks = row.Field<int?>("ScheduledWeeks"),
                                TakenWeeks = row.Field<int?>("TakenWeeks"),
                                Status = row.Field<string>("Status"),
                                DeliveryStatus = row.Field<string>("DeliveryStatus")
                            }).ToList();
            }
            return data;
        }
        #endregion TaskTracking
    }
}
