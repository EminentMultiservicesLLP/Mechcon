using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Mapper.Master;
using AutoMapper;
using System.Data.SqlClient;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class UnitMasterRepository : IUnitMasterRepository
   {
        public UnitMasterEntities GetUnitById(int unitId)
        {
            UnitMasterEntities unit = null;
            using(DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UnitId", unitId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(MasterQueries.GetUnitMasterById,param, CommandType.StoredProcedure);

                unit = dtUnits.AsEnumerable()
                            .Select(row => new UnitMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }            
            return unit;
        }

        public IEnumerable<UnitMasterEntities> GetAllUnits()
        {
            List<UnitMasterEntities> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(MasterQueries.GetAllUnitMaster, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new UnitMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (units == null || units.Count == 0)
                    units.Add(new UnitMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = ""
                    });
            }           
            return units;
        }

        public int CreateUnit(UnitMasterEntities unitEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", unitEntity.ID, DbType.Int32,ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", unitEntity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", unitEntity.Name, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", unitEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", unitEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", unitEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", unitEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", unitEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", unitEntity.Deactive, DbType.Boolean));

               
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertUnitMaster, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Unit", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool UpdateUnit(UnitMasterEntities unitEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", unitEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", unitEntity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", unitEntity.Name, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", unitEntity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", unitEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", unitEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", unitEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", unitEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", unitEntity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteUnit(UnitMasterEntities unitEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UnitID", unitEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", unitEntity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", unitEntity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;            
        }
        public IEnumerable<UnitMasterEntities> GetActiveUnit()
        {
            List<UnitMasterEntities> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllUnitMaster, param, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new UnitMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                              
                            }).ToList();
            }
            return unit;
        }
        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Unit", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
   }
}
