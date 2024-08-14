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
    public class TrainingCategoryRepository : ITrainingCategoryRepository
    {
        public IEnumerable<TrainingCategoryEntity> GetAllTrainingCategory() 
        {
            List<TrainingCategoryEntity> category;
            using (var dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllTrainingCategory, CommandType.StoredProcedure);
                category = dtUnits.AsEnumerable()
                            .Select(row => new TrainingCategoryEntity
                            {
                                CategoryId = row.Field<int>("CategoryId"),
                                Code = row.Field<string>("Code"),
                                Category = row.Field<string>("Category"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return category;
        }

        public int CreateTrainingCategory(TrainingCategoryEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CategoryId", entity.CategoryId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Category", entity.Category, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdTrainingCategory, paramCollection, CommandType.StoredProcedure, "CategoryId");
            }
            return iResult;
        }

        public bool UpdateTrainingCategory(TrainingCategoryEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CategoryId", entity.CategoryId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Category", entity.Category, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdTrainingCategory, paramCollection, CommandType.StoredProcedure);
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
                paramCollection.Add(new DBParameter("Type", "TrainingCategory", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateupdate(string code, int id)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "TrainingCategory", DbType.String));
                paramCollection.Add(new DBParameter("ID", id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
