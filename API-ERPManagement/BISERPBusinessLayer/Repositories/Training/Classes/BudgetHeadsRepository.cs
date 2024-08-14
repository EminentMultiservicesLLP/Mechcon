using System;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.QueryCollection.Masters;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class BudgetHeadsRepository : IBudgetHeadsRepository
    {
        public IEnumerable<BudgetHeadsEntity> GetAllBudgetHeads()
        {
            List<BudgetHeadsEntity> heads;
            using (var dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllBudgetHeads, CommandType.StoredProcedure);
                heads = dtUnits.AsEnumerable()
                            .Select(row => new BudgetHeadsEntity
                            {
                                BudgetId = row.Field<int>("BudgetId"),
                                BudgetCode = row.Field<string>("BudgetCode"),
                                BudgetHeads = row.Field<string>("BudgetHeads"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return heads;
        }

        public int CreateBudgetHeads(BudgetHeadsEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BudgetCode", entity.BudgetCode, DbType.String));
                paramCollection.Add(new DBParameter("BudgetHeads", entity.BudgetHeads, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdBudgetHeads, paramCollection, CommandType.StoredProcedure, "BudgetId");
            }
            return iResult;
        }

        public bool UpdateBudgetHeads(BudgetHeadsEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32));
                paramCollection.Add(new DBParameter("BudgetCode", entity.BudgetCode, DbType.String));
                paramCollection.Add(new DBParameter("BudgetHeads", entity.BudgetHeads, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdBudgetHeads, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string code, string Name)
        {
            bool bResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "BudgetHeads", DbType.String));
                paramCollection.Add(new DBParameter("Name", Name, DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "BudgetHeads", DbType.String));
                paramCollection.Add(new DBParameter("ID", id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }


    }
}
