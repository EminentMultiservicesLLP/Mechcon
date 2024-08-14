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
    public class AdjustmentVoucherDtRepository : IAdjustmentVoucherDtRepository
    {

        public int CreateAdjustmentVoucherDt(AdjustmentVoucherEntity entity, AdjustmentVoucherDtEntity dtentity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("VoucherDetailId", dtentity.VoucherDetailId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("VoucherId", entity.VoucherId, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("DiscrepancyDetailId", dtentity.DiscrepancyDetailId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", dtentity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchID", dtentity.BatchID, DbType.Int32));
            paramCollection.Add(new DBParameter("Quantity", dtentity.Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("PhysicalQty", dtentity.PhysicalQty, DbType.Double));
            paramCollection.Add(new DBParameter("ShortQty", dtentity.ShortQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("SurplusQty", dtentity.SurPlusQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("Reason", dtentity.Reason, DbType.String));
            paramCollection.Add(new DBParameter("Authorized", true, DbType.Boolean));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertAdjustmentVoucherDt, paramCollection, CommandType.StoredProcedure, "VoucherDetailId");
            return iResult;
        }
    }
}
