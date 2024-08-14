using BISERPBusinessLayer.Entities.Transport;
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
    public class GreenTaxRepository : IGreenTaxRepository
    {

        public IEnumerable<GreenTaxEntity> GetAllGreenTax()
        {
            List<GreenTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllGreenTax, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new GreenTaxEntity
                            {
                                GreenTaxId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                GreenTaxNo = row.Field<string>("GreenTaxNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public IEnumerable<GreenTaxEntity> GreenTaxNotification(int DueDays)
        {
            List<GreenTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DueDays", DueDays, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetGreenTaxNotification, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new GreenTaxEntity
                            {
                                GreenTaxId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                GreenTaxNo = row.Field<string>("GreenTaxNo"),
                                VehicleNo = row.Field<string>("VehicleNo"),
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

        public IEnumerable<GreenTaxEntity> GetAllGreenTax(int VehicleId)
        {
            List<GreenTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("VehicleId", VehicleId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetVehicleGreenTax, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new GreenTaxEntity
                            {
                                GreenTaxId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                GreenTaxNo = row.Field<string>("GreenTaxNo"),
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

        public int CreateGreenTax(GreenTaxEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("GreenTaxId", Entity.GreenTaxId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("GreenTaxNo", Entity.GreenTaxNo, DbType.String));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("IssueDate", Entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ExpiryDate", Entity.ExpiryDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReminderDays", Entity.ReminderDays, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertGreenTax, paramCollection, CommandType.StoredProcedure, "GreenTaxId");
            }
            return iResult;
        }

        public bool UpdateGreenTax(GreenTaxEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("GreenTaxId", Entity.GreenTaxId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("GreenTaxNo", Entity.GreenTaxNo, DbType.String));
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
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateGreenTax, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<GreenTaxEntity> ActiveGreenTax()
        {
            List<GreenTaxEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllGreenTax, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new GreenTaxEntity
                            {
                                GreenTaxId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                GreenTaxNo = row.Field<string>("GreenTaxNo"),
                                Amount = row.Field<double>("Amount"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                ReminderDays = row.Field<int>("ReminderDays"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }

        public bool CheckDuplicateGreenTax(int VehicleId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Green", DbType.String));
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TransportQuery.CheckDpVehicleTax, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
