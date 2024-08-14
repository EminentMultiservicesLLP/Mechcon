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
    public class StoreItemTypeMappingRepository : IStoreItemTypeMappingRepository
    {


        public IEnumerable<StoreItemTypeMappingMasterEntities> GetStoreItemMappingByStoreId(int Id)  
        {
            List<StoreItemTypeMappingMasterEntities> storeItemMapping = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreID", Id, DbType.Int32);
                DataTable dtStoreItemMapping = dbHelper.ExecuteDataTable(MasterQueries.GetStoreItemTypeMappingByStoreId, param, CommandType.StoredProcedure);
                storeItemMapping = dtStoreItemMapping.AsEnumerable()
                            .Select(row => new StoreItemTypeMappingMasterEntities
                            {

                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreID = row.Field<int?>("StoreID"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                Select = row.Field<bool>("SELECTED")
                            }).ToList();
            }
            return storeItemMapping;
        }

        public IEnumerable<StoreItemTypeMappingMasterEntities> GetAllStoreItemMappings()
        {
            List<StoreItemTypeMappingMasterEntities> storeItemMapping = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreItemMapping, CommandType.StoredProcedure);
                storeItemMapping = dtUnits.AsEnumerable()
                            .Select(row => new StoreItemTypeMappingMasterEntities
                            {
                                StoreID = row.Field<int>("StoreID"),
                                ItemTypeID = row.Field<int>("ItemTypeID")
                            }).ToList();
                if (storeItemMapping == null || storeItemMapping.Count == 0)
                    storeItemMapping.Add(new StoreItemTypeMappingMasterEntities
                    {
                        StoreID = 0,
                        ItemTypeID = 0
                    });
            }
            return storeItemMapping;
        }

        public int CreateStoreItemTypeMapping(string Itemtype, StoreItemTypeMappingMasterEntities mainentity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreID",mainentity.StoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeID", Itemtype, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", mainentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertStoreItemTypeMapping, paramCollection, CommandType.StoredProcedure);
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertStoreItemTypeMapping, paramCollection, CommandType.StoredProcedure, "StoreID");
            }

            return iResult;
        }

        public bool UpdateItemTypeMapping(StoreItemTypeMappingMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
               
                paramCollection.Add(new DBParameter("ItemTypeID", entity.ItemTypeID, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreID", entity.StoreID, DbType.Int32));               
                paramCollection.Add(new DBParameter("Select", entity.Select, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.AddDeleteStoreItemTypeMapping, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteItemTypeMapping(StoreItemTypeMappingMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreID", entity.StoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeID", entity.ItemTypeID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", entity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
