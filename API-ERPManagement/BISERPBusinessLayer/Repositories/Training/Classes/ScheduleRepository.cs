using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class ScheduleRepository : IScheduleRepository
    {
        public IEnumerable<ScheduleEntity> GetAllSchedules()
        {
            List<ScheduleEntity> units;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", "0", DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllSchedule, param, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new ScheduleEntity
                            {
                                ScheduleId = row.Field<int>("ScheduleId"),
                                ScheduleDate = row.Field<DateTime>("ScheduleDate"),
                                strScheduleDate = row.Field<DateTime?>("ScheduleDate").DateTimeFormat1(),
                                strStartTime = row.Field<DateTime?>("StartTime").TimeFormat(),
                                strEndTime = row.Field<DateTime?>("EndTime").TimeFormat(),
                                BatchId = row.Field<int>("BatchId"),
                                BatchName = row.Field<string>("BatchName"),
                                SubjectId = row.Field<int>("SubjectId"),
                                Subject = row.Field<string>("SubjectName"),
                                SubjectTopicId = row.Field<int>("SubjectTopicId"),
                                TopicName = row.Field<string>("TopicName"),
                                TrainerId = row.Field<int>("TrainerId"),
                                Trainer = row.Field<string>("TrainerName"),
                                Remark = row.Field<string>("Remark"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateSchedule(ScheduleEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScheduleId", entity.ScheduleId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("ScheduleDate", entity.ScheduleDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StartTime", entity.StartTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("EndTime", entity.EndTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("SubjectId", entity.SubjectId, DbType.Int32));
                paramCollection.Add(new DBParameter("SubjectTopicId", entity.SubjectTopicId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainerId", entity.TrainerId, DbType.Int32));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdSchedule, paramCollection, CommandType.StoredProcedure, "ScheduleId");
            }
            return iResult;
        }

        public bool UpdateSchedule(ScheduleEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScheduleId", entity.ScheduleId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("ScheduleDate", entity.ScheduleDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StartTime", entity.StartTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("EndTime", entity.EndTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("SubjectId", entity.SubjectId, DbType.Int32));
                paramCollection.Add(new DBParameter("SubjectTopicId", entity.SubjectTopicId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainerId", entity.TrainerId, DbType.Int32));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdSchedule, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
