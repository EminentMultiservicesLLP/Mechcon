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
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class RequestRegisterRepository : IRequestRegisterRepository
    {
        public RequestRegisterEntity CreateRequestRegister(RequestRegisterEntity Entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", Entity.RequestId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RequestCode", Entity.RequestCode, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("AssetId", Entity.AssetId, DbType.String));                
                paramCollection.Add(new DBParameter("RequestedDate", Entity.RequestedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Priority", Entity.Priority, DbType.String));
                paramCollection.Add(new DBParameter("Complaint", Entity.Complaint, DbType.String));
                paramCollection.Add(new DBParameter("RequiredDate", Entity.RequiredDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("RequestedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("ProblemNature", Entity.ProblemNature, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(AssetQueries.InsUpdRequestRegister, paramCollection,  CommandType.StoredProcedure);
                Entity.RequestId = Convert.ToInt32(parameterList["RequestId"].ToString());
                Entity.RequestCode = parameterList["RequestCode"].ToString();                
            }
            return Entity;
        }

        public int UpdateRequestRegister(RequestRegisterEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", Entity.RequestId, DbType.Int32));                
                paramCollection.Add(new DBParameter("AssetId", Entity.AssetId, DbType.String));
                paramCollection.Add(new DBParameter("RequestedDate", Entity.RequestedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Priority", Entity.Priority, DbType.String));
                paramCollection.Add(new DBParameter("Complaint", Entity.Complaint, DbType.String));
                paramCollection.Add(new DBParameter("RequiredDate", Entity.RequiredDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("RequestedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("ProblemNature", Entity.ProblemNature, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.UpdateRequestRegister, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
        public IEnumerable<RequestRegisterEntity> GetRequestno(int BranchId)
        {
            List<RequestRegisterEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", BranchId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetRequistionNo,param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                RequestCode = row.Field<string>("RequestCode"),
                               RequestedDate = row.Field<DateTime>("RequestedDate"),
                                StrRequestedDate = Convert.ToDateTime(row.Field<DateTime>("RequestedDate")).ToString("dd-MMM-yyyy"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                Description = row.Field<string>("Description"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Complaint = row.Field<string>("Complaint"),
                                RequiredDate = row.Field<DateTime?>("RequiredDate"),
                                StrRequiredDate = Convert.ToDateTime(row.Field<DateTime?>("RequiredDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName"),
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                                //RequisitionStatus = row.Field<int?>("RequisitionStatus"),
                                //MaintenanceStatus = row.Field<int?>("MaintenanceStatus"),
                               // Assignment = row.Field<string>("Assignment"),
                            }).ToList();
            }
            return type;
        }

        public RequestRegisterEntity GetRequestDetail(int RequisitionId)
        {
            RequestRegisterEntity type = new RequestRegisterEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("RequisitionId", RequisitionId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetRequistionDetail, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                BranchId = row.Field<int>("BranchId"),
                                RequestCode = row.Field<string>("RequestCode"),
                                RequestedDate = row.Field<DateTime>("RequestedDate"),
                                StrRequestedDate = Convert.ToDateTime(row.Field<DateTime>("RequestedDate")).ToString("dd-MMM-yyyy"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                Description = row.Field<string>("Description"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Complaint = row.Field<string>("Complaint"),
                                RequiredDate = row.Field<DateTime>("RequiredDate"),
                                StrRequiredDate = Convert.ToDateTime(row.Field<DateTime>("RequiredDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName")
                            }).FirstOrDefault();
            }
            return type;
        }
        public bool UpdRequestAcceptance(RequestRegisterEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", Entity.RequestId, DbType.Int32));
                paramCollection.Add(new DBParameter("REQUISTATION_STATUS", Entity.RequisitionStatus, DbType.Int32));
                paramCollection.Add(new DBParameter("MAINTENANCE_STATUS", Entity.MaintenanceStatus, DbType.Int32));
                paramCollection.Add(new DBParameter("ASSIGMENT", Entity.Assignment, DbType.String));
                paramCollection.Add(new DBParameter("REJECTION_REASON ", Entity.RejectionReason, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.UpdRequestAcceptance, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<RequestRegisterEntity> GetRequestNoDeletion(int BranchId)
        {
            List<RequestRegisterEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", BranchId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetRequestNoDeletion, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                RequestCode = row.Field<string>("RequestCode"),
                                RequestedDate = row.Field<DateTime>("RequestedDate"),
                                StrRequestedDate = Convert.ToDateTime(row.Field<DateTime>("RequestedDate")).ToString("dd-MMM-yyyy"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                Description = row.Field<string>("Description"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Complaint = row.Field<string>("Complaint"),
                                RequiredDate = row.Field<DateTime?>("RequiredDate"),
                                StrRequiredDate = Convert.ToDateTime(row.Field<DateTime?>("RequiredDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName")
                            }).ToList();
            }
            return type;
        }

        public IEnumerable<RequestRegisterEntity> GetRequestRegister(DateTime FromDate, DateTime ToDate, int BranchId, int Status)
        {
            List<RequestRegisterEntity> register = new List<RequestRegisterEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Status", Status, DbType.Int32));
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetRequestRegister, paramCollection, CommandType.StoredProcedure);
                register = dtUnits.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                BranchId = row.Field<int>("BranchId"),
                                RequestCode = row.Field<string>("RequestCode"),
                                RequestedDate = row.Field<DateTime>("RequestedDate"),
                                StrRequestedDate = Convert.ToDateTime(row.Field<DateTime>("RequestedDate")).ToString("dd-MMM-yyyy"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                Description = row.Field<string>("Description"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Complaint = row.Field<string>("Complaint"),
                                RequiredDate = row.Field<DateTime>("RequiredDate"),
                                StrRequiredDate = Convert.ToDateTime(row.Field<DateTime?>("RequiredDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName"),
                                BranchName = row.Field<string>("BranchName"),
                                Priority = row.Field<string>("Priority"),
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                            }).ToList();
            }
            return register;
        }

        public IEnumerable<RequestRegisterEntity> GetAllRequestRegister()
        {
            List<RequestRegisterEntity> register = new List<RequestRegisterEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllRequestRegister, CommandType.StoredProcedure);
                register = dtUnits.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                BranchId = row.Field<int>("BranchId"),
                                RequestCode = row.Field<string>("RequestCode"),
                               RequestedDate = row.Field<DateTime>("RequestedDate"),
                              StrRequestedDate = Convert.ToDateTime(row.Field<DateTime>("RequestedDate")).ToString("dd-MMM-yyyy"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                Description = row.Field<string>("Description"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Complaint = row.Field<string>("Complaint"),
                               // RequiredDate = row.Field<DateTime>("RequiredDate"),
                                //StrRequiredDate = Convert.ToDateTime(row.Field<DateTime>("RequiredDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName"),
                                BranchName = row.Field<string>("BranchName"),
                                Priority = row.Field<string>("Priority"),
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                            }).ToList();
            }
            return register;
        }


        public IEnumerable<RequestRegisterEntity> RequestRegisterReport(DateTime FromDate, DateTime ToDate, int BranchId)
        {
            List<RequestRegisterEntity> register = new List<RequestRegisterEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.RequestRegisterReport, paramCollection, CommandType.StoredProcedure);
                register = dtUnits.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                RequestId = row.Field<int>("RequestId"),
                                BranchId = row.Field<int>("BranchId"),
                                RequestCode = row.Field<string>("RequestCode"),
                                RequestedDate = row.Field<DateTime?>("RequestedDate"),
                                Priority = row.Field<string>("Priority"),
                                Complaint = row.Field<string>("Complaint"),
                                RequiredDate = row.Field<DateTime?>("RequiredDate"),
                                StrRequiredDate = row.Field<DateTime?>("RequiredDate").DateTimeFormat1(),
                                AssetCode = row.Field<string>("AssetCode"),
                                ProblemNature = row.Field<string>("ProblemNature"),
                                Remark = row.Field<string>("Remark"),
                                RequestedBy = row.Field<int>("RequestedBy"),
                                ItemName = row.Field<string>("ItemName"),
                                ModelNo = row.Field<string>("ModelNo"),
                                UserName = row.Field<string>("UserName"),
                                BranchName = row.Field<string>("BranchName"),
                                RequisitionStatus = row.Field<int>("RequisitionStatus"),
                                MaintenanceStatus = row.Field<int>("MaintenanceStatus"),
                                RejectionReason = row.Field<string>("RejectionReason")
                            }).ToList();
            }
            return register;
        }

        public IEnumerable<RequestRegisterEntity> RequestNotification(int DueDays)
        {
            List<RequestRegisterEntity> AMC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(AssetQueries.AlertRequestNotification, param, CommandType.StoredProcedure);
                AMC = dtInsurance.AsEnumerable()
                            .Select(row => new RequestRegisterEntity
                            {
                                RequestId = row.Field<int>("RequestId"),
                                RequestCode = row.Field<string>("RequestCode"),
                                StrMaintenanceStatus = row.Field<string>("MaintenanceStatus"),
                                StrRequisitionStatus = row.Field<string>("RequisitionStatus"),
                                RequiredDate = row.Field<DateTime>("RequiredDate"),
                                StrRequiredDate = Convert.ToDateTime(row.Field<DateTime>("RequiredDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return AMC;
        }
    }
}
