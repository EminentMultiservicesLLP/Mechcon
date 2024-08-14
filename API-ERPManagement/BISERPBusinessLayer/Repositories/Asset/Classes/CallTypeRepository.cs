using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class CallTypeRepository : ICallTypeRepository
    {
        public IEnumerable<CallTypeEntity> GetAllCallType()
        {
            List<CallTypeEntity> units;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllCallType, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new CallTypeEntity
                            {
                                CallTypeId = row.Field<int>("CallTypeId"),
                                CallTypeCode = row.Field<string>("CallTypeCode"),
                                CallType = row.Field<string>("CallType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateCallType(CallTypeEntity entity)
        {
            int iResult;
            using (var dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CallTypeId", entity.CallTypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("CallTypeCode", entity.CallTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("CallType", entity.CallType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstCallType, paramCollection, CommandType.StoredProcedure, "CallTypeId");
            }
            return iResult;
        }

        public bool UpdateCallType(CallTypeEntity entity)
        {
            int iResult;
            using (var dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CallTypeId", entity.CallTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("CallTypeCode", entity.CallTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("CallType", entity.CallType, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstCallType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<CallTypeEntity> GetActiveCallType()
        {
             List<CallTypeEntity> division;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetAllCallType, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new CallTypeEntity
                            {
                                CallTypeId = row.Field<int>("CallTypeId"),
                                CallTypeCode = row.Field<string>("CallTypeCode"),
                                CallType = row.Field<string>("CallType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }

        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "CallType", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateupdate(string code, int id)
        {
            bool bResult = false;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "CallType", DbType.String));
                paramCollection.Add(new DBParameter("ID", id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
