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
    public class BatchRepository : IBatchRepository 
    {
        public IEnumerable<BatchEntity> GetAllBatch()
        {
            List<BatchEntity> batch;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtbatch = dbHelper.ExecuteDataTable(TrainingQueries.GetAllBatch, CommandType.StoredProcedure);
                batch = dtbatch.AsEnumerable()
                            .Select(row => new BatchEntity
                            {
                                BatchId = row.Field<int>("BatchId"),
                                CentreId = row.Field<int>("CentreId"),                                
                                BatchTypeId = row.Field<int>("BatchTypeId"),
                                StartDate = row.Field<DateTime>("StartDate"),
                                EndDate = row.Field<DateTime>("EndDate"),
                                strStartDate = row.Field<DateTime?>("StartDate").DateTimeFormat1(),
                                strEndDate = row.Field<DateTime?>("EndDate").DateTimeFormat1(),
                                BatchQty = row.Field<double>("BatchQty"),
                                TrainingTypeId = row.Field<int>("TrainingTypeId"),
                                GradeId = row.Field<int>("GradeId"),
                                NoOfSlot = row.Field<int>("NoOfSlot"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                Batchname = row.Field<string>("Batchname"),
                                Centre = row.Field<string>("TrainingCentre"),
                                BatchType = row.Field<string>("BatchType"),
                                TrainingType = row.Field<string>("TrainingType"),
                                Grade = row.Field<string>("Name"),
                            }).ToList();
            }
            return batch;
        }

        public int CreateBatch(BatchEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("CentreId", entity.CentreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchTypeId", entity.BatchTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("EndDate", entity.EndDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("BatchQty", entity.BatchQty, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingTypeId", entity.TrainingTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("GradeId", entity.GradeId, DbType.Int32));
                paramCollection.Add(new DBParameter("NoOfSlot", entity.NoOfSlot, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdBatch, paramCollection, CommandType.StoredProcedure, "BatchId");
            }
            return iResult;
        }

        public bool UpdateBatch(BatchEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32, ParameterDirection.InputOutput));
                paramCollection.Add(new DBParameter("CentreId", entity.CentreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchTypeId", entity.BatchTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("EndDate", entity.EndDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("BatchQty", entity.BatchQty, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingTypeId", entity.TrainingTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("GradeId", entity.GradeId, DbType.Int32));
                paramCollection.Add(new DBParameter("NoOfSlot", entity.NoOfSlot, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdBatch, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<BatchEntity> GetTraniningWiseSlot(int trainingTypeId)
        {
            List<BatchEntity> batches;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("TrainingTypeId", trainingTypeId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTraniningWiseSlot, param, CommandType.StoredProcedure);
                batches = dtUnits.AsEnumerable()
                            .Select(row => new BatchEntity()
                            {
                                BatchId = row.Field<int>("BatchId"),
                                CentreId = row.Field<int>("CentreId"),
                                Batchname = row.Field<string>("BatchName"),
                                StartDate = row.Field<DateTime>("FromDate"),
                                EndDate = row.Field<DateTime>("ToDate"),
                                NoOfDays = row.Field<int>("NoOfDays"),
                                NoOfSlot = row.Field<int>("NoOfSlot"),
                                TrainingTypeId = row.Field<int>("TrainingTypeId"),
                               // strFromDate = Convert.ToDateTime(row.Field<DateTime?>("FromDate")).ToString("dd-MMM-yyyy"),
                               // strToDate = Convert.ToDateTime(row.Field<DateTime?>("ToDate")).ToString("dd-MMM-yyyy"),
                                Deactive = row.Field<Boolean>("Deactive")
                                //TraniningSlotCode = row.Field<string>("Code"),
                            }).ToList();
            }
            return batches;
        }

        public IEnumerable<BatchEntity> GetDateWiseSlot(int trainingCentreId)
        {
            List<BatchEntity> batch;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("TrainingCentreId", trainingCentreId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllDayWiseBatchSlot,param, CommandType.StoredProcedure);
                batch = dtUnits.AsEnumerable()
                            .Select(row => new BatchEntity()
                            {
                                BatchId = row.Field<int>("BatchId"),
                                CentreId = row.Field<int>("CentreId"),
                                Batchname = row.Field<string>("BatchName"),
                                StartDate = row.Field<DateTime>("FromDate"),
                                EndDate = row.Field<DateTime>("ToDate"),
                                NoOfDays = row.Field<int>("NoOfDays"),
                                NoOfSlot = row.Field<int>("NoOfSlot"),
                                
                                Deactive = row.Field<Boolean>("Deactive")
                              
                            }).ToList();
            }
            return batch;
        }
        public bool CheckDuplicateBatch(int centreId, int batchTypeId, int trainingTypeId, DateTime? startDate, DateTime? endDate)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CentreId", centreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchTypeId", batchTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingTypeId", trainingTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("StartDate", startDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("EndDate", endDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TrainingQueries.CheckDuplicateBatch, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
