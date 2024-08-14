using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class InsuranceRepository : IInsuranceRepository
    {
        public IEnumerable<InsuranceEntity> GetAllInsurance()
        {
            List<InsuranceEntity> insurance = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtInsurance = dbHelper.ExecuteDataTable(TransportQuery.GetAllGreenTax, CommandType.StoredProcedure);
                insurance = dtInsurance.AsEnumerable()
                            .Select(row => new InsuranceEntity
                            {
                                InsuranceId = row.Field<int>("InsuranceId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PolicyNo = row.Field<string>("PolicyNo"),
                                CompanyId = row.Field<int>("CompanyId"),
                                CompanyName = row.Field<string>("CompanyName"),
                                IssueDate = row.Field<DateTime?>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                Amount = row.Field<double>("Amount"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Agent = row.Field<string>("Agent"),
                                CovertNote = row.Field<string>("CovertNote"),
                                InsuranceType = row.Field<int>("InsuranceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return insurance;
        }

        public IEnumerable<InsuranceEntity> InsuranceNotification(int DueDays)
        {
            List<InsuranceEntity> insurance = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(TransportQuery.GetInsuranceNotification, param, CommandType.StoredProcedure);
                insurance = dtInsurance.AsEnumerable()
                            .Select(row => new InsuranceEntity
                            {
                                InsuranceId = row.Field<int>("InsuranceId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PolicyNo = row.Field<string>("PolicyNo"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                CompanyId = row.Field<int>("CompanyId"),
                                IssueDate = row.Field<DateTime?>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                Amount = row.Field<double>("Amount"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Agent = row.Field<string>("Agent"),
                                CovertNote = row.Field<string>("CovertNote"),
                                InsuranceType = row.Field<int?>("InsuranceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return insurance;
        }

        public IEnumerable<InsuranceEntity> GetAllInsurance(int VehicleId)
        {
            List<InsuranceEntity> insurance = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("VehicleId", VehicleId, DbType.Int32);
                DataTable dtInsurance = dbHelper.ExecuteDataTable(TransportQuery.GetVehicleInsurance, param, CommandType.StoredProcedure);
                insurance = dtInsurance.AsEnumerable()
                            .Select(row => new InsuranceEntity
                            {
                                InsuranceId = row.Field<int>("InsuranceId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PolicyNo = row.Field<string>("PolicyNo"),
                                CompanyId = row.Field<int>("CompanyId"),
                                //CompanyName = row.Field<string>("CompanyName"),
                                IssueDate = row.Field<DateTime?>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                Amount = row.Field<double>("Amount"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Agent = row.Field<string>("Agent"),
                                CovertNote = row.Field<string>("CovertNote"),
                                InsuranceType = row.Field<int?>("InsuranceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return insurance;
        }

        public int CreateInsurance(InsuranceEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("InsuranceId", Entity.InsuranceId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("PolicyNo", Entity.PolicyNo, DbType.String));
                paramCollection.Add(new DBParameter("CompanyId", Entity.CompanyId, DbType.Int32));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Agent", Entity.Agent, DbType.String));
                paramCollection.Add(new DBParameter("CovertNote", Entity.CovertNote, DbType.String));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("InsuranceType", Entity.InsuranceType, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertInsurance, paramCollection, CommandType.StoredProcedure, "InsuranceId");
            }
            return iResult;
        }

        public bool UpdateInsurance(InsuranceEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("InsuranceId", Entity.InsuranceId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("PolicyNo", Entity.PolicyNo, DbType.String));
                paramCollection.Add(new DBParameter("CompanyId", Entity.CompanyId, DbType.Int32));
                paramCollection.Add(new DBParameter("CompanyName", Entity.CompanyName, DbType.String));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Agent", Entity.Agent, DbType.String));
                paramCollection.Add(new DBParameter("CovertNote", Entity.CovertNote, DbType.String));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("InsuranceType", Entity.InsuranceType, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateInsurance, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<InsuranceEntity> ActiveInsurance()
        {
            List<InsuranceEntity> insurance = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtinsurance = dbHelper.ExecuteDataTable(TransportQuery.GetAllGreenTax, param, CommandType.StoredProcedure);
                insurance = dtinsurance.AsEnumerable()
                            .Select(row => new InsuranceEntity
                            {
                                InsuranceId = row.Field<int>("InsuranceId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PolicyNo = row.Field<string>("PolicyNo"),
                                CompanyId = row.Field<int>("CompanyId"),
                                CompanyName = row.Field<string>("CompanyName"),
                                IssueDate = row.Field<DateTime?>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                Amount = row.Field<double>("Amount"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Agent = row.Field<string>("Agent"),
                                CovertNote = row.Field<string>("CovertNote"),
                                InsuranceType = row.Field<int>("InsuranceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return insurance;
        }

        public bool CheckDuplicateInsurance(int VehicleId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Insurance", DbType.String));
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TransportQuery.CheckDpVehicleTax, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
