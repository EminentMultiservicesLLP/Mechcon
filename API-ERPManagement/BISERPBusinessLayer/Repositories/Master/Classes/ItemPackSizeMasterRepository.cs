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
    public class ItemPackSizeMasterRepository : IItemPackSizeMasterRepository
    {

        public ItemPackSizeMasterEntities GetItemPackSizeById(int Id)
        {
            ItemPackSizeMasterEntities itemPackSize = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ItemPackSizeId", Id, DbType.Int32);
                DataTable dtItemPackSize = dbHelper.ExecuteDataTable(MasterQueries.GetItemPackSizeMasterById, param, CommandType.StoredProcedure);

                itemPackSize = dtItemPackSize.AsEnumerable()
                            .Select(row => new ItemPackSizeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Quantity = row.Field<decimal>("Quantity"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return itemPackSize;
        }

        public IEnumerable<ItemPackSizeMasterEntities> GetAllItemPackSize()
        {
            List<ItemPackSizeMasterEntities> itemPackSize = new List<ItemPackSizeMasterEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtItemPackSize = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemPackSizeMaster, CommandType.StoredProcedure);
                itemPackSize = dtItemPackSize.AsEnumerable()
                            .Select(row => new ItemPackSizeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Quantity = row.Field<decimal>("Quantity"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (itemPackSize == null || itemPackSize.Count == 0)
                    itemPackSize.Add(new ItemPackSizeMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        Deactive = false
                    });
            }
            return itemPackSize;
        }

        public int CreateItemPackSize(ItemPackSizeMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                dbHelper.ExecuteNonQuery(MasterQueries.InsertItemPackSizeMaster, paramCollection, CommandType.StoredProcedure);
                iResult = (int)paramCollection.Get("ID").Value;
            }
            return iResult;
        }

        public bool UpdateItemPackSize(ItemPackSizeMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Double));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateItemPackSizeMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteItemPackSize(ItemPackSizeMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", entity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteItemPackSizeMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<ItemPackSizeMasterEntities> GetActivePackSize()
        {
            List<ItemPackSizeMasterEntities> itemPackSize = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemPackSizeMaster, param, CommandType.StoredProcedure);
                itemPackSize = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemPackSizeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Quantity = row.Field<decimal>("Quantity"),

                            }).ToList();
            }
            return itemPackSize;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Packsize", DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Packsize", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
