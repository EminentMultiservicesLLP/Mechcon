using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class TrainingDaillyUpdatesRepository : ITrainingDaillyUpdatesRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(TrainingDaillyUpdatesRepository));
        public bool CreateTrainingDaillyUpdates(TrainingDaillyUpdatesHdrEntity entity)
        {
            bool isSuccess = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var daillyupdateResult = ((entity.DaillyUpdId <= 0) ? InsUpdTrainingDaillyUpdatesHdr(entity, dbHelper).DaillyUpdId : entity.DaillyUpdId);
                    foreach (var detail in entity.DailyUpdateDtlModel)
                    {
                        detail.DaillyUpdId = daillyupdateResult;
                        detail.TrainingDay = entity.TrainingDay;
                        detail.InsertedBy = entity.InsertedBy;
                        detail.InsertedON = entity.InsertedON;
                        detail.InsertedIPAddress = entity.InsertedIPAddress;
                        detail.InsertedMacName = entity.InsertedMacName;
                        detail.InsertedMacID = entity.InsertedMacID;

                        var daillyUpdatesDtlId = InsUpdTrainingDaillyUpdatesDtl(detail, dbHelper);
                        if (daillyUpdatesDtlId > 0)
                        {
                            foreach (var rating in detail.DailyUpdatesRatingModel)
                            {
                                if (rating.RatingId != null)
                                {
                                    rating.DaillyUpdDtlId = daillyUpdatesDtlId;
                                    InsUpdTrainingDailyUpdatesRating(rating, dbHelper);
                                }
                            }
                        }
                    }

                    dbHelper.CommitTransaction(transaction);
                    isSuccess = true;
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateTrainingDaillyUpdates method of Repository : parameter :" +
                        Environment.NewLine + ex.StackTrace);

                });
            }

            return isSuccess;
        }



        public TrainingDaillyUpdatesHdrEntity InsUpdTrainingDaillyUpdatesHdr(TrainingDaillyUpdatesHdrEntity entity, DBHelper dbHelper)
        {
            var iResult = 0;
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DaillyUpdId", entity.DaillyUpdId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("TrainingTemplateID", entity.TrainingTemplateID, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingDay", entity.TrainingDay, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdTrainingDaillyUpdatesHdr, paramCollection, CommandType.StoredProcedure, "DaillyUpdId");

                entity.DaillyUpdId = iResult;

            }).IfNotNull(ex => { throw (ex); });
            return entity;
        }


        public int InsUpdTrainingDaillyUpdatesDtl(TrainingDailyUpdatesDtlEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            TryCatch.Run(() =>
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DaillyUpdDtlId", entity.DaillyUpdDtlId, DbType.Int32, ParameterDirection.InputOutput));
                //paramCollection.Add(new DBParameter("DaillyUpdDtlId", entity.DaillyUpdDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("DaillyUpdId", entity.DaillyUpdId, DbType.Int32));
                paramCollection.Add(new DBParameter("TraineeName", entity.TraineeName, DbType.String));
                paramCollection.Add(new DBParameter("Performance", entity.Performance, DbType.String));
                paramCollection.Add(new DBParameter("TrainingDay", entity.TrainingDay, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdTrainingDaillyUpdatesDtl,
                paramCollection, CommandType.StoredProcedure, "DaillyUpdDtlId");
                //iResult = Convert.ToInt32(dbHelper.ExecuteScalar(TrainingQueries.InsUpdTrainingDaillyUpdatesDtl, paramCollection,
                //    CommandType.StoredProcedure));

            }).IfNotNull(ex => { throw (ex); });

            return iResult;
        }

        public void InsUpdTrainingDailyUpdatesRating(TrainingDailyUpdatesRatingEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DaillyUpdDtlId", entity.DaillyUpdDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("RatingId", (entity.RatingId ?? String.Empty), DbType.String));
                paramCollection.Add(new DBParameter("TraingTemplateDtlId", entity.TraingTemplateDtlId, DbType.Int32));
                dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdTrainingDailyUpdatesRating, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });
        }

        public IEnumerable<TraineeEntity> GetGuardTrainee()
        {
            List<TraineeEntity> trainee;
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetGuardTrainee, CommandType.StoredProcedure);
                trainee = dtUnits.AsEnumerable()
                            .Select(row => new TraineeEntity()
                            {
                                TraineeName = row.Field<string>("EmpFName"),
                                //TraineeId = row.Field<int>("GuardId")
                            }).ToList();
            }
            return trainee;
        }

        public IEnumerable<TrainingDaysEntity> GetTraniningDay(int id)
        {
            List<TrainingDaysEntity> trainingday;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Id", id, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTraniningDay, param, CommandType.StoredProcedure);
                trainingday = dtUnits.AsEnumerable()
                            .Select(row => new TrainingDaysEntity()
                            {
                                TrainingDay = row.Field<int>("TrainingDay"),
                                TrainingDayId = row.Field<int>("TrainingDay")
                            }).ToList();
            }
            return trainingday;
        }

        public IEnumerable<TrainingEntity> GetTrainingTemplate(int trainingTypeId)
        {
            List<TrainingEntity> trainingName;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("trainingTypeId", trainingTypeId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTrainingTemplate, param, CommandType.StoredProcedure);
                trainingName = dtUnits.AsEnumerable()
                            .Select(row => new TrainingEntity()
                            {
                                TrainingName = row.Field<string>("TrainingName"),
                                TrainingId = row.Field<int>("TrainingTemplateID")
                            }).ToList();
            }
            return trainingName;
        }


        public IEnumerable<TrainingTemplatePeriodDetails> GetTraniningTemplatePeriod(int trainingtempdtId, int trainingDay)
        {
            {
                List<TrainingTemplatePeriodDetails> trainingday;
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection param = new DBParameterCollection();
                    DBParameter param1 = new DBParameter("trainingtempdtId", trainingtempdtId, DbType.Int32);
                    DBParameter param2 = new DBParameter("trainingDay", trainingDay, DbType.Int32);
                    param.Add(param1);
                    param.Add(param2);
                    DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTraniningTemplatePeriod, param, CommandType.StoredProcedure);

                    trainingday = dtUnits.AsEnumerable()
                                .Select(row => new TrainingTemplatePeriodDetails()
                                {
                                    PeriodName = row.Field<string>("PeriodName"),
                                    PeriodId = row.Field<int>("PeriodId"),

                                }).ToList();
                }
                return trainingday;
            }
        }


        public IEnumerable<TraniningTemplatePeriodRecordEntity> GetTraniningTemplatePeriodRecord(int trainingtempdtId, int trainingDay)
        {
            {
                List<TraniningTemplatePeriodRecordEntity> trainingday;
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection param = new DBParameterCollection();
                    DBParameter param1 = new DBParameter("trainingtempdtId", trainingtempdtId, DbType.Int32);
                    DBParameter param2 = new DBParameter("trainingDay", trainingDay, DbType.Int32);
                    param.Add(param1);
                    param.Add(param2);
                    DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTraniningTemplatePeriodRecord, param, CommandType.StoredProcedure);

                    trainingday = dtUnits.AsEnumerable()
                                .Select(row => new TraniningTemplatePeriodRecordEntity()
                                {
                                    TrainingTemplateId = row.Field<int>("TrainingTemplateID"),
                                    DaillyUpdId = row.Field<int>("DaillyUpdId"),
                                    DaillyUpdDtlId = row.Field<int>("DaillyUpdDtlId"),
                                    TraineeName = row.Field<string>("TraineeName"),
                                    Performance = row.Field<string>("Performance"),
                                    PeriodId = row.Field<int>("PeriodId"),
                                    RatingId = row.Field<string>("RatingId")
                                }).ToList();
                }
                return trainingday;
            }



        }



    }
}
