using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class MomentOrderRepository : IMomentOrderRepository
    {

        public int CreateMomentOrder(MomentOrderEntity entity)
        {
            const int iResult = 0;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("OrderId", entity.OrderId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("TrainingCentreId", entity.TrainingCentreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("EmployeeType", entity.EmployeeType, DbType.Int32));
                paramCollection.Add(new DBParameter("EmpId", entity.EmpId, DbType.String));
                paramCollection.Add(new DBParameter("TempEmpId", entity.TempEmpId, DbType.String));
                paramCollection.Add(new DBParameter("Medical", entity.Medical, DbType.Boolean));
                paramCollection.Add(new DBParameter("Details", entity.Details, DbType.String));
                paramCollection.Add(new DBParameter("TrainingDate", entity.TrainingDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BranchQueries.InsMomentOrder, paramCollection,  CommandType.StoredProcedure);
                entity.OrderId = Convert.ToInt32(parameterList["OrderId"].ToString());
                entity.Code = parameterList["Code"].ToString();
            }
            return iResult;
        }


        public IEnumerable<MomentOrderEntity> GetAllMomentOrderList(int userId)
        {
            List<MomentOrderEntity> list;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", userId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(BranchQueries.GetAllMomentOrder, paramCollection, CommandType.StoredProcedure);
                list = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MomentOrderEntity
                            {
                                OrderId = row.Field<int>("OrderId"),
                                Code = row.Field<string>("Code"),
                                EmployeeType = row.Field<int>("EmployeeType"),
                                TrainingCentreId = row.Field<int>("TrainingCentreId"),
                                TrainingCentre = row.Field<string>("TrainingCentre"),
                                BranchId = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                EmpId = row.Field<int>("EmpId"),
                                TempEmpId = row.Field<int>("TempEmpId"),
                                EmployeeName = row.Field<string>("EmployeeName"),
                                EmpName = row.Field<string>("EmpName"),
                                Temporaryname = row.Field<string>("Temporaryname"),
                                TicketCode = row.Field<string>("TicketCode"),
                                TrainingDate = row.Field<DateTime>("TrainingDate"),
                                StrTrainingDate = Convert.ToDateTime(row.Field<DateTime>("TrainingDate")).ToString("dd-MMM-yyyy"),
                                BatchId = row.Field<int>("BatchId"),
                                BatchName = row.Field<string>("BatchName"),
                                Medical = row.Field<bool>("Medical"),
                                Details = row.Field<string>("Details"),
                                BatchQty = row.Field<double>("BatchQty"),
                                StrEmployeeType = row.Field<string>("StrEmployeeType")
                            }).ToList();
            }
            return list;
        }

        public MomentOrderEntity UpdateMomentOrder(MomentOrderEntity entity)
        {
            int iResult=0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("OrderId", entity.OrderId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Accepted", entity.Accepted, DbType.Boolean));
                //paramCollection.Add(new DBParameter("Height", entity.Height, DbType.String));
                //paramCollection.Add(new DBParameter("Weight", entity.Weight, DbType.String));
                //paramCollection.Add(new DBParameter("Waist", entity.Waist, DbType.String));
                //paramCollection.Add(new DBParameter("Fitness", entity.Fitness, DbType.String));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Status", true, DbType.Boolean, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("StatusCode", entity.StatupCode, DbType.String, 500, ParameterDirection.Output));
               // iResult = dbHelper.ExecuteNonQuery(BranchQueries.UpdMomentOrder, paramCollection, CommandType.StoredProcedure);
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(BranchQueries.UpdMomentOrder, paramCollection, CommandType.StoredProcedure, "Status");
                 var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BranchQueries.UpdMomentOrder, paramCollection, CommandType.StoredProcedure);
                 entity.Status = Convert.ToBoolean(parameterList["Status"].ToString());
                 entity.StatupCode = parameterList["StatusCode"].ToString();
            }
            return entity;
          
        }

        public MomentOrderEntity ClearBatchFullOrderacceptance(MomentOrderEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("OrderId", entity.OrderId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Accepted", entity.Accepted, DbType.Boolean));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Status", true, DbType.Boolean, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("StatusCode", entity.StatupCode, DbType.String, 500, ParameterDirection.Output));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BranchQueries.ClearBatchFullOrderacceptance, paramCollection,  CommandType.StoredProcedure);
                entity.Status = Convert.ToBoolean(parameterList["Status"].ToString());
                entity.StatupCode = parameterList["StatusCode"].ToString();
            }
            return entity;

        }
        
    }
}
