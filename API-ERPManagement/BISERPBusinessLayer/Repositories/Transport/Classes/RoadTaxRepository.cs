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
    public class RoadTaxRepository : IRoadTaxRepository
    {

        public IEnumerable<RoadTaxEntity> GetAllRoadTax()
        {
            List<RoadTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllRoadTax, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new RoadTaxEntity
                            {
                                RoadTaxId = row.Field<int>("RoadTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                TaxNo = row.Field<string>("TaxNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public IEnumerable<RoadTaxEntity> RoadTaxNotification(int DueDays)
        {
            List<RoadTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetRoadTaxNotification, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new RoadTaxEntity
                            {
                                RoadTaxId = row.Field<int>("RoadTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                TaxNo = row.Field<string>("TaxNo"),
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

        public IEnumerable<RoadTaxEntity> GetAllRoadTax(int VehicleId)
        {
            List<RoadTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("VehicleId", VehicleId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetVehicleRoadTax, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new RoadTaxEntity
                            {
                                RoadTaxId = row.Field<int>("RoadTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                TaxNo = row.Field<string>("TaxNo"),
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

        public int CreateRoadTax(RoadTaxEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RoadTaxId", Entity.RoadTaxId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("TaxNo", Entity.TaxNo, DbType.String));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertRoadTax, paramCollection, CommandType.StoredProcedure, "RoadTaxId");
            }
            return iResult;
        }

        public bool UpdateRoadTax(RoadTaxEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RoadTaxId", Entity.RoadTaxId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("TaxNo", Entity.TaxNo, DbType.String));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateRoadTax, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<RoadTaxEntity> ActiveRoadTax()
        {
            List<RoadTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllRoadTax, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new RoadTaxEntity
                            {
                                RoadTaxId = row.Field<int>("RoadTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                TaxNo = row.Field<string>("TaxNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public bool CheckDuplicateRoadTax(int VehicleId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Road", DbType.String));
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TransportQuery.CheckDpVehicleTax, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}