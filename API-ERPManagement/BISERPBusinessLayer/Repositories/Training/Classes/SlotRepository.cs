using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class SlotRepository : ISlotRepository
    {
        public IEnumerable<SlotEntity> GetAllSlot()
        {
            List<SlotEntity> slot;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllSlot, CommandType.StoredProcedure);
                slot = dtUnits.AsEnumerable()
                            .Select(row => new SlotEntity()
                            {
                                SlotId = row.Field<int>("SlotId"),
                                SlotCode = row.Field<string>("SlotCode"),
                                SlotName = row.Field<string>("SlotName"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                ToDate = row.Field<DateTime>("ToDate"),
                                NoOfDays = row.Field<string>("NoOfDays"),
                                NoOfSlot = row.Field<string>("NoOfSlot"),
                                Remarks = row.Field<string>("Remarks"),
                                TrainingTypeId = row.Field<int>("TrainingTypeId"),
                                strFromDate = Convert.ToDateTime(row.Field<DateTime?>("FromDate")).ToString("dd-MMMM-yyyy"),
                                strToDate = Convert.ToDateTime(row.Field<DateTime?>("ToDate")).ToString("dd-MMMM-yyyy"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                SlotDate = row.Field<string>("SlotDate"),
                            }).ToList();
            }
            return slot;
        }
        public int CreateSlot(SlotEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SlotId", entity.SlotId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("SlotCode", entity.SlotCode, DbType.String));
                paramCollection.Add(new DBParameter("SlotName", entity.SlotName, DbType.String));
                paramCollection.Add(new DBParameter("FromDate", entity.FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("NoOfDays", entity.NoOfDays, DbType.String));
                paramCollection.Add(new DBParameter("NoOfSlot", entity.NoOfSlot, DbType.String));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("TrainingTypeId", entity.TrainingTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdSlot, paramCollection, CommandType.StoredProcedure, "SlotId");
            }
            return iResult;
        }
        public bool UpdateSlot(SlotEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SlotId", entity.SlotId, DbType.Int32));
                paramCollection.Add(new DBParameter("SlotCode", entity.SlotCode, DbType.String));
                paramCollection.Add(new DBParameter("SlotName", entity.SlotName, DbType.String));
                paramCollection.Add(new DBParameter("FromDate", entity.FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("NoOfDays", entity.NoOfDays, DbType.String));
                paramCollection.Add(new DBParameter("NoOfSlot", entity.NoOfSlot, DbType.String));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("TrainingTypeId", entity.TrainingTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdSlot, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Slot", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int id)
        {
            bool bResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Slot", DbType.String));
                paramCollection.Add(new DBParameter("ID", id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public IEnumerable<SlotEntity> GetTraniningWiseSlot(int TrainingTypeId)
        {
            List<SlotEntity> slot;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("TrainingTypeId", TrainingTypeId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetTraniningWiseSlot, param, CommandType.StoredProcedure);
                slot = dtUnits.AsEnumerable()
                            .Select(row => new SlotEntity()
                            {
                                SlotId = row.Field<int>("SlotId"),
                                SlotCode = row.Field<string>("SlotCode"),
                                SlotName = row.Field<string>("SlotName"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                ToDate = row.Field<DateTime>("ToDate"),
                                NoOfDays = row.Field<string>("NoOfDays"),
                                NoOfSlot = row.Field<string>("NoOfSlot"),
                                TrainingTypeId = row.Field<int>("TrainingTypeId"),
                                Deactive = row.Field<Boolean>("Deactive"),
                            }).ToList();
            }
            return slot;
        }
        public IEnumerable<SlotEntity> GetDateWiseSlot()
        {
            List<SlotEntity> slot;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllDayWiseSlot, CommandType.StoredProcedure);
                slot = dtUnits.AsEnumerable()
                            .Select(row => new SlotEntity()
                            {
                                SlotId = row.Field<int>("SlotId"),
                                SlotCode = row.Field<string>("SlotCode"),
                                SlotName = row.Field<string>("SlotName"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                ToDate = row.Field<DateTime>("ToDate"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                SlotDate = row.Field<string>("SlotDate"),
                            }).ToList();
            }
            return slot;
        }
    }
}
