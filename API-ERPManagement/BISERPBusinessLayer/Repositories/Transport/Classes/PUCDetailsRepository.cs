﻿using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class PUCDetailsRepository : IPUCDetailsRepository
    {

        public IEnumerable<PUCDetailsEntity> GetAllPUCDetails()
        {
            List<PUCDetailsEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllPUCDetails, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new PUCDetailsEntity
                            {
                                PUCId = row.Field<int>("PUCId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PUCNo = row.Field<string>("PUCNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public IEnumerable<PUCDetailsEntity> PUCDetailsNotification(int DueDays)
        {
            List<PUCDetailsEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetPUCDetailNotification, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new PUCDetailsEntity
                            {
                                PUCId = row.Field<int>("PUCId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                PUCNo = row.Field<string>("PUCNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public IEnumerable<PUCDetailsEntity> GetAllPUCDetails(int VehicleId)
        {
            List<PUCDetailsEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("VehicleId", VehicleId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetVehiclePUCDetails, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new PUCDetailsEntity
                            {
                                PUCId = row.Field<int>("PUCId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PUCNo = row.Field<string>("PUCNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public int CreatePUCDetails(PUCDetailsEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PUCId", Entity.PUCId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("PUCNo", Entity.PUCNo, DbType.String));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertPUCDetails, paramCollection, CommandType.StoredProcedure, "PUCId");
            }
            return iResult;
        }

        public bool UpdatePUCDetails(PUCDetailsEntity Entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PUCDetailsEntity> ActivePUCDetails()
        {
            throw new NotImplementedException();
        }

        public bool CheckDuplicatePUCDetails(int VehicleId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "PUC", DbType.String));
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TransportQuery.CheckDpVehicleTax, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
