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
using BISERPCommon;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class SupplierBillPayment : ISupplierBillPayment
    {
        public List<SupplierBillPaymentEntity> CreateBillPayment(List<SupplierBillPaymentEntity> entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                foreach (var bill in entity)
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("PaymentId", bill.PaymentId, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("PaymentNo", bill.PaymentNo, DbType.String));
                    paramCollection.Add(new DBParameter("BillId", bill.BillId, DbType.Int32));
                    paramCollection.Add(new DBParameter("PaymentDate", bill.PaymentDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("SupplierId", bill.SupplierId, DbType.Int32));
                    paramCollection.Add(new DBParameter("PaidAmount", bill.PaidAmount, DbType.Int32));                    
                    paramCollection.Add(new DBParameter("InsertedBy", bill.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", bill.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedMacName", bill.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacID", bill.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", bill.InsertedIPAddress, DbType.String));
                    bill.PaymentId = dbhelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertSupplierBillPayment, paramCollection, CommandType.StoredProcedure, "PaymentId");                    
                }
                dbhelper.CommitTransaction(transaction);
            }
            return entity;
        }
    }
}
