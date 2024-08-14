using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class StoreUnitLinkingMasterRepository : IStoreUnitLinkingMasterRepository
    {
        public IEnumerable<StoreUnitLinkingEntities> GetAllUnitStore()
        {
            List<StoreUnitLinkingEntities> storeunit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreunit, param, CommandType.StoredProcedure);
                storeunit = dtStores.AsEnumerable()
                            .Select(row => new StoreUnitLinkingEntities
                            {
                                ID = row.Field<int>("ID"),
                                UnitStore = row.Field<string>("UnitStore"),
                                StoreName = row.Field<string>("StoreName"),
                                ParentId = row.Field<int>("ParentId"),
                                StoreId = row.Field<int>("StoreId"),                               
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (storeunit == null || storeunit.Count == 0)
                    storeunit.Add(new StoreUnitLinkingEntities
                    {
                        ID = 0,
                        UnitStore = "",
                        StoreName = "",
                        Deactive = false
                    });
            }
            return storeunit;
        }
        public int CreateUnitStore(StoreUnitLinkingEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ParentId", entity.ParentId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));              
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertStoreunit, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }
        public bool CheckDuplicateUnitStore(StoreUnitLinkingEntities entity)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "UnitStore", DbType.String));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(StoreUnitLinkingEntities entity)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "UnitStore", DbType.String));
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.String));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool UpdateStoreUnit(StoreUnitLinkingEntities entity)
        {
            int bResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("ParentId", entity.ParentId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                bResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertStoreunit, paramCollection, CommandType.StoredProcedure);
            }
            if (bResult > 0)
                return true;
            else
                return false;
        }
    }
}
