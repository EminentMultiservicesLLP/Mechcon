using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class BatchTypeRepository : IBatchTypeRepository
    {

        public IEnumerable<BatchTypeEntity> GetAllBatchType()
        {
            List<BatchTypeEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllBatchType, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new BatchTypeEntity
                            {
                                BatchTypeId = row.Field<int>("BatchTypeId"),
                                BatchTypeCode = row.Field<string>("BatchTypeCode"),
                                BatchType = row.Field<string>("BatchType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateBatchType(BatchTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BatchTypeId", Entity.BatchTypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BatchTypeCode", Entity.BatchTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("BatchType", Entity.BatchType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdBatchType, paramCollection, CommandType.StoredProcedure, "BatchTypeId");
            }
            return iResult;
        }

        public bool UpdateBatchType(BatchTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BatchTypeId", Entity.BatchTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchTypeCode", Entity.BatchTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("BatchType", Entity.BatchType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdBatchType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "BatchType", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateupdate(string code, int Id)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "BatchType", DbType.String));
                paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
