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
    public class TechnicianRepository : ITechnicianRepository
    {
        public IEnumerable<TechnicianEntity> GetAllTechnician()
        {
            List<TechnicianEntity> units;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllTechnician, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new TechnicianEntity
                            {
                                TechnicianId = row.Field<int>("TechnicianId"),
                                TechnicianCode = row.Field<string>("TechnicianCode"),
                                TechnicianName = row.Field<string>("TechnicianName"),
                                Designation = row.Field<string>("Designation"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateTechnician(TechnicianEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TechnicianId", entity.TechnicianId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("TechnicianCode", entity.TechnicianCode, DbType.String));
                paramCollection.Add(new DBParameter("TechnicianName", entity.TechnicianName, DbType.String));
                paramCollection.Add(new DBParameter("Designation", entity.Designation, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstTechnician, paramCollection, CommandType.StoredProcedure, "TechnicianId");
            }
            return iResult;
        }

        public bool UpdateTechnician(TechnicianEntity entity)
        {
            int iResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TechnicianId", entity.TechnicianId, DbType.Int32));
                paramCollection.Add(new DBParameter("TechnicianCode", entity.TechnicianCode, DbType.String));
                paramCollection.Add(new DBParameter("TechnicianName", entity.TechnicianName, DbType.String));
                paramCollection.Add(new DBParameter("Designation", entity.Designation, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
               iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstTechnician, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<TechnicianEntity> GetActiveTechnician()
        {
            List<TechnicianEntity> technician;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetAllTechnician, paramCollection, CommandType.StoredProcedure);
                technician = dtManufacturer.AsEnumerable()
                            .Select(row => new TechnicianEntity
                            {
                                TechnicianId = row.Field<int>("TechnicianId"),
                                TechnicianCode = row.Field<string>("TechnicianCode"),
                                TechnicianName = row.Field<string>("TechnicianName"),
                                Designation = row.Field<string>("Designation"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return technician;
        }

        public bool CheckDuplicateItem(string code)
        {
            var bResult = false;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Technician", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int technicianId)
        {
            var bResult = false;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Technician", DbType.String));
                paramCollection.Add(new DBParameter("ID", technicianId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
