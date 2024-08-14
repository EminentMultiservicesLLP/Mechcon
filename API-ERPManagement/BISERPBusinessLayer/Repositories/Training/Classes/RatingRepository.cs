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
    public class RatingRepository : IRatingRepository
    {
        public IEnumerable<RatingEntity> GetAllRating()
        {
            List<RatingEntity> rating;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllRating, CommandType.StoredProcedure);
                rating = dtUnits.AsEnumerable()
                            .Select(row => new RatingEntity()
                            {
                                RatingId = row.Field<int>("RatingId"),
                                RatingCode = row.Field<string>("RatingCode"),
                                Rating = row.Field<string>("Rating"),
                                Marks = row.Field<string>("Marks"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return rating;
        }
        public int CreateRating(RatingEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RatingId", entity.RatingId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RatingCode", entity.RatingCode, DbType.String));
                paramCollection.Add(new DBParameter("Rating", entity.Rating, DbType.String));
                paramCollection.Add(new DBParameter("Marks", entity.Marks, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TrainingQueries.InsUpdRating, paramCollection, CommandType.StoredProcedure, "RatingId");
            }
            return iResult;
        }
        public bool UpdateRating(RatingEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RatingId", entity.RatingId, DbType.Int32));
                paramCollection.Add(new DBParameter("RatingCode", entity.RatingCode, DbType.String));
                paramCollection.Add(new DBParameter("Rating", entity.Rating, DbType.String));
                paramCollection.Add(new DBParameter("Marks", entity.Marks, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.UpdatedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.UpdatedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.UpdatedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdRating, paramCollection, CommandType.StoredProcedure);
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
                paramCollection.Add(new DBParameter("Type", "Rating", DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Rating", DbType.String));
                paramCollection.Add(new DBParameter("ID", id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
