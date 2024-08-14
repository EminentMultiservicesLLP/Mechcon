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
using BISERPCommon.Extensions;
using BISERPCommon;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ItemTypeMasterRepository : IItemTypeMasterRepository
    {

        public ItemTypeMasterEntities GetItemTypeById(int itemtypeId)
        {
            ItemTypeMasterEntities itemtype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ItemTypeId", itemtypeId, DbType.Int32);
                DataTable dtitemtype = dbHelper.ExecuteDataTable(MasterQueries.GetItemTypeMasterById, param, CommandType.StoredProcedure);

                itemtype = dtitemtype.AsEnumerable()
                            .Select(row => new ItemTypeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                IsManufactRequired = row.Field<bool>("IsManufactRequired"),
                                POException = row.Field<float>("POException"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }
            return itemtype;
        }

        public IEnumerable<ItemTypeMasterEntities> GetItemTypeByStoreId(int StoreId)
        {
            List<ItemTypeMasterEntities> itemtype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtitemtype = dbHelper.ExecuteDataTable(MasterQueries.GetItemTypeByStoreId, param, CommandType.StoredProcedure);
                itemtype = dtitemtype.AsEnumerable()
                            .Select(row => new ItemTypeMasterEntities
                            {
                                ID = row.Field<int>("ItemTypeID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreId = row.Field<int>("StoreID")
                             }).ToList();
            }
            return itemtype;

        }

        public IEnumerable<ItemTypeMasterEntities> GetAllItemType()
        {
            List<ItemTypeMasterEntities> itemtype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtitemtype = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemTypeMaster, CommandType.StoredProcedure);
                itemtype = dtitemtype.AsEnumerable()
                            .Select(row => new ItemTypeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                IsManufactRequired = row.Field<bool?>("IsManufactRequired"),
                                POException = row.Field<double?>("POException"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();

                if (itemtype == null || itemtype.Count == 0)
                    itemtype.Add(new ItemTypeMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = ""
                    });
            }
            return itemtype;
        }
      
        public IEnumerable<ItemTypeMasterEntities> GetActiveItemType()
        {
            List<ItemTypeMasterEntities> itemtype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemTypeMaster, param, CommandType.StoredProcedure);
                itemtype = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemTypeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                IsManufactRequired = row.Field<bool?>("IsManufactRequired"),
                                POException = row.Field<double?>("POException"),

                            }).ToList();
            }
            return itemtype;
        }
        public int CreateItemType(ItemTypeMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("POException", entity.POException, DbType.Double));
                paramCollection.Add(new DBParameter("IsManufactRequired", entity.IsManufactRequired, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertItemTypeMaster, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public bool UpdateItemType(ItemTypeMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("POException", entity.POException, DbType.Double));
                paramCollection.Add(new DBParameter("IsManufactRequired", entity.IsManufactRequired, DbType.Boolean));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateItemTypeMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteItemType(ItemTypeMasterEntities itemtypeEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemTypeId", itemtypeEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", itemtypeEntity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", itemtypeEntity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
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
                paramCollection.Add(new DBParameter("Type", "ItemType", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "ItemType", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
       
    }
}
