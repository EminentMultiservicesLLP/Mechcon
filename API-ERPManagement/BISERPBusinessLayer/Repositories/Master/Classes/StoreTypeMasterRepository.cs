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
    public class StoreTypeMasterRepository : IStoreTypeMasterRepository
    {
        public StoreTypeMasterEntities GetStoreTypeById(int Id)
        {
            StoreTypeMasterEntities storetype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtStoretypes = dbHelper.ExecuteDataTable(MasterQueries.GetStoreTypeById, param, CommandType.StoredProcedure);

                storetype = dtStoretypes.AsEnumerable()
                            .Select(row => new StoreTypeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Description = row.Field<string>("Description"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return storetype;
        }

        public IEnumerable<StoreTypeMasterEntities> GetAllStoreTypes()
        {
            List<StoreTypeMasterEntities> storetypes = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStoretypes = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreTypeMaster, CommandType.StoredProcedure);
                storetypes = dtStoretypes.AsEnumerable()
                            .Select(row => new StoreTypeMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Description = row.Field<string>("Description"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (storetypes == null || storetypes.Count == 0)
                    storetypes.Add(new StoreTypeMasterEntities
                    {
                        ID = 0,
                        Description = "",
                        Deactive = false
                    });
            }
            return storetypes;
        }

        public int CreateStoreType(StoreTypeMasterEntities StoreTypeentity)
        {
            int iResult = 0;
            using(DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Description", StoreTypeentity.Description, DbType.String));
                paramCollection.Add(new DBParameter("CreatedBy", StoreTypeentity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("CreatedOn", StoreTypeentity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("CreatedMacName", StoreTypeentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("CreatedMacID", StoreTypeentity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("CreatedIPAddress", StoreTypeentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", StoreTypeentity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertStoreTypeMaster, paramCollection, CommandType.StoredProcedure);
                iResult = (int)paramCollection.Get("StoreTypeId").Value;

            }
            return iResult;
        }

        public bool UpdateStoreType(StoreTypeMasterEntities StoreTypeentity)
        {
             int iResult = 0;
             using (DBHelper dbHelper = new DBHelper())
             {
                  DBParameterCollection paramCollection = new DBParameterCollection();
                  paramCollection.Add(new DBParameter("ItemID", StoreTypeentity.ID, DbType.Int32));
                  paramCollection.Add(new DBParameter("Description", StoreTypeentity.Description, DbType.String));
                  paramCollection.Add(new DBParameter("UpdatedBy", StoreTypeentity.UpdatedBy, DbType.String));
                  paramCollection.Add(new DBParameter("UpdatedIP", StoreTypeentity.UpdatedIPAddress, DbType.String));

                  iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateStoreMasterById, paramCollection, CommandType.StoredProcedure);

             }
             if (iResult > 0)
                 return true;
             else
                 return false;
             
        }

        public bool DeleteStoreType(StoreTypeMasterEntities entity)
        {
            throw new NotImplementedException();
        }
    }
}
