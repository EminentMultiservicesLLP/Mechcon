using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ScopeOfSupplyMasterRepository : IScopeOfSupplyMasterRepository
    {
        public IEnumerable<ScopeOfSupplyMasterEntities> GetAllScopeOfSupply()
        {
            List<ScopeOfSupplyMasterEntities> projectTC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtprojectTC = dbHelper.ExecuteDataTable(MasterQueries.GetAllScopeOfSupply, CommandType.StoredProcedure);
                projectTC = dtprojectTC.AsEnumerable().Select(row => new ScopeOfSupplyMasterEntities
                {
                    ScopeOfSupplyID = row.Field<int>("ScopeOfSupplyID"),
                    ScopeOfSupplyDesc = row.Field<string>("ScopeOfSupplyDesc").NullToString(),
                    ScopeOfSupplyName = row.Field<string>("ScopeOfSupplyName"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                if (projectTC == null || projectTC.Count == 0)
                    projectTC.Add(new ScopeOfSupplyMasterEntities
                    {
                        ScopeOfSupplyID = 0,
                        ScopeOfSupplyDesc = "",
                        ScopeOfSupplyName = "",
                        Deactive = false
                    });
            }
            return projectTC;
        }

        public int CreateScopeOfSupply(ScopeOfSupplyMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScopeOfSupplyID", entity.ScopeOfSupplyID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ScopeOfSupplyName", entity.ScopeOfSupplyName, DbType.String));
                paramCollection.Add(new DBParameter("ScopeOfSupplyDesc", entity.ScopeOfSupplyDesc, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertScopeOfSupplyMaster, paramCollection, CommandType.StoredProcedure, "ScopeOfSupplyID");
            }
            return iResult;
        }

        public bool UpdateScopeOfSupply(ScopeOfSupplyMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScopeOfSupplyID", entity.ScopeOfSupplyID, DbType.Int32));
                paramCollection.Add(new DBParameter("ScopeOfSupplyName", entity.ScopeOfSupplyName, DbType.String));
                paramCollection.Add(new DBParameter("ScopeOfSupplyDesc", entity.ScopeOfSupplyDesc, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateScopeOfSupplyMaster, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateItem(string ScopeOfSupplyName)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "ScopeOfSupply", DbType.String));
                paramCollection.Add(new DBParameter("Code", ScopeOfSupplyName, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "ScopeOfSupply", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}