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
    public class AdjustmentVoucherRepository : IAdjustmentVoucherRepository
    {
        public AdjustmentVoucherEntity CreateNewEntry(AdjustmentVoucherEntity entity, DBHelper dbhelper)
        {
            if (entity.Authorized == true)
            {
           
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("VoucherId", entity.VoucherId, DbType.Int32,
                ParameterDirection.Output));
            paramCollection.Add(new DBParameter("VoucherNo", entity.VoucherNo, DbType.String, 50,
                ParameterDirection.Output));
            paramCollection.Add(new DBParameter("DiscrepancyId", entity.DiscrepancyId, DbType.Int32));
            paramCollection.Add(new DBParameter("VoucherDate", entity.VoucherDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("Authorised", entity.Authorized, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            var parameterList = dbhelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertAdjustmentVoucher,
                paramCollection,  CommandType.StoredProcedure);
            entity.VoucherId = Convert.ToInt32(parameterList["VoucherId"].ToString());
            entity.VoucherNo = "Generated Voucher No : "+parameterList["VoucherNo"].ToString();
        }
            else
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VoucherId", entity.VoucherId, DbType.Int32,
                    ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VoucherNo", entity.VoucherNo, DbType.String, 50,
                    ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DiscrepancyId", entity.DiscrepancyId, DbType.Int32));
                paramCollection.Add(new DBParameter("VoucherDate", entity.VoucherDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("Authorised", entity.Authorized, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                var parameterList = dbhelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertAdjustmentVoucher,
                    paramCollection,  CommandType.StoredProcedure);
                entity.VoucherNo = "Adjustment Voucher Cancelled successfully";
            }
        return entity;
        }
    }
}
