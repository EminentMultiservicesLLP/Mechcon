using System;
using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class TrainingTemplateRepository : ITrainingTemplateRepository
    {

        private static readonly ILogger Loggger = Logger.Register(typeof(TrainingTemplateRepository));
        public TrainingTempHeaderModel CreateTrainingTemplate(TrainingTempHeaderModel entity)
        {
            bool isSuccess = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {

                    var tempateResult = ((entity.TrainingTemplateId <= 0) ? InsUpdTrainingTemplateHdr(entity, dbHelper).TrainingTemplateId: entity.TrainingTemplateId);
                        
                    if (tempateResult > 0)
                    {
                        foreach (var detail in entity.TrainingTempDaywiseModel)
                        {
                            detail.TrainingTemplateId = tempateResult;
                            string dateTime = "1/1/0001 12:00:00 AM";
                            DateTime dt = Convert.ToDateTime(dateTime);
                            if (detail.FromTime == dt || detail.ToTime ==dt)
                            {
                                detail.FromTime = DateTime.Now;
                                detail.ToTime = DateTime.Now;
                            }
                          
                            if(detail.Id > 0)
                                UpdateTrainingTempDaywise(detail,entity, dbHelper);
                            else
                            
                                detail.Id=InsUpdTrainingTemplateDtl(detail,entity, dbHelper);
                        }
                        dbHelper.CommitTransaction(transaction);
                        isSuccess = true;
                    }
                    else
                    {
                        dbHelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateTrainingTemplate method of TrainingTemplateRepository : parameter :" +
                        Environment.NewLine + ex.StackTrace);
                    throw ex;

                });
            }
            return entity;
        }

        public TrainingTempHeaderModel InsUpdTrainingTemplateHdr(TrainingTempHeaderModel entity, DBHelper dbHelper)
        {
           
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TrainingTemplateId", entity.TrainingTemplateId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingName", entity.TrainingName, DbType.String));
                paramCollection.Add(new DBParameter("TrainingCode", entity.TrainingCode, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

               var iResult = dbHelper.ExecuteNonQueryForOutParameter(TrainingQueries.InsUpdTrainingTemplateHdr,paramCollection, CommandType.StoredProcedure);
               entity.TrainingTemplateId = Convert.ToInt32(iResult["TrainingTemplateId"].ToString());
            }).IfNotNull(ex => { throw (ex); });
            return entity;
        }

        public int InsUpdTrainingTemplateDtl(TrainingTempDaywiseModel entity,TrainingTempHeaderModel entity1, DBHelper dbHelper)
        {
            int iResult = 0;
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("TrainingTemplateId", entity.TrainingTemplateId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingDay", entity.TrainingDay, DbType.Int16));
                paramCollection.Add(new DBParameter("CategoryId", entity.CategoryId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainerId", entity.TrainerId, DbType.Int32));
               
                paramCollection.Add(new DBParameter("FromTime", entity.FromTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToTime", entity.ToTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedBy", entity1.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity1.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity1.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity1.InsertedMacID, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdTrainingTemplateDtl, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });
            return iResult;
        }



        public bool UpdateTrainingTempDaywise(TrainingTempDaywiseModel entity, TrainingTempHeaderModel entity1, DBHelper dbHelper)
        {
            int iResult;
            //using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingTemplateId", entity.TrainingTemplateId, DbType.Int32));
                paramCollection.Add(new DBParameter("CategoryId", entity.CategoryId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainerId", entity.TrainerId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingDay", entity.TrainingDay, DbType.Int32));
                paramCollection.Add(new DBParameter("FromTime", entity.FromTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToTime", entity.ToTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedBy", entity1.InsertedBy, DbType.Int32));
               // paramCollection.Add(new DBParameter("UpdatedOn", entity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity1.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity1.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity1.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdTrainingTemplateDtl, paramCollection,CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<TrainingTempHeaderModel> GetTrainingTemplateHdr()
        {
            List<TrainingTempHeaderModel> template;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(TrainingQueries.GetTrainingTemplateHdr, CommandType.StoredProcedure);
                  template = dtMaterialIndent.AsEnumerable()
                        .Select(row => new TrainingTempHeaderModel
                        {
                            TrainingTemplateId = row.Field<int>("TrainingTemplateId"),
                            TrainingCode = row.Field<string>("TrainingCode"),
                            TrainingName = row.Field<string>("TrainingName"),
                            BatchId = row.Field<int>("BatchId"),
                            BatchName = row.Field<string>("BatchName"),
                            TrainingTypeCode = row.Field<string>("TrainingTypeCode"),
                            TrainingTypeId = row.Field<int>("TrainingTypeId"),
                        }).ToList();
              
            }
            return template;
        }


        public IEnumerable<TrainingTempDaywiseModel> GetTrainingTempDaywise(int trainingTemplateId,string day )
        {
            List<TrainingTempDaywiseModel> daywise;
            using (DBHelper dbHelper = new DBHelper())
            {
              
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TrainingTemplateId", trainingTemplateId, DbType.Int32));
                paramCollection.Add(new DBParameter("day", day, DbType.String));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(TrainingQueries.GetTrainingTemplateDtl, paramCollection, CommandType.StoredProcedure);
                daywise = dtMaterialIndent.AsEnumerable()
                            .Select(row => new TrainingTempDaywiseModel
                            {
                                Id = row.Field<int>("Id"),
                                TrainingTemplateId = row.Field<int>("TrainingTemplateId"),
                                TrainingDay = row.Field<int>("TrainingDay"),
                                CategoryId = row.Field<int>("CategoryId"),
                                TrainerId = row.Field<int>("TrainerId"),
                                strFromTime = string.Format("{0:hh:mm tt}", row.Field<DateTime?>("FromTime")),
                                strToTime = string.Format("{0:hh:mm tt}", row.Field<DateTime?>("ToTime"))
                            }).ToList();
            }
            return daywise;
        }
    }
}
