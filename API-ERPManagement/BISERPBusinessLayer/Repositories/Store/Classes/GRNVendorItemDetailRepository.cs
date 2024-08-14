using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class GRNVendorItemDetailRepository : IGRNVendorItemDetailRepository
    {

        public int CreateNewEntry(GRNVendorEntity grnentity, GRNVendorItemDetailEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("GRNId", grnentity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32));
            paramCollection.Add(new DBParameter("AcceptedQuantity", entity.AcceptedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("InsertedBy", grnentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", grnentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", grnentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", grnentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", grnentity.InsertedMacID, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertGRNVendorItemDetails, paramCollection, CommandType.StoredProcedure, "Id");
            
            return iResult;
        }
    }
}
