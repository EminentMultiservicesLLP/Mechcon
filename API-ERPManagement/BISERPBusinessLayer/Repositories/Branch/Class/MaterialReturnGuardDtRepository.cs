using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class MaterialReturnGuardDtRepository : IMaterialReturnGuardDtRepository
    {
        public int CreateEntity(MaterialReturnGuardEntity mientity, MaterialReturnGuardDtEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnDtlId", entity.ReturnDtlId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ReturnId", mientity.ReturnId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Int32));
                paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
                paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("StoreId", mientity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsertMaterialReturnDetails, paramCollection, CommandType.StoredProcedure, "ReturnDtlId");
            }
            return iResult;
        }
    }
}
