using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class POPaymentTerm : IPOPaymentTerm
    {

        public List<POPaymentTermEntities> GetDetailByPOID(int Id)
        {
            List<POPaymentTermEntities> popayment = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPOPayment = dbHelper.ExecuteDataTable(PurchaseQueries.GetPOPaymentTermByPOID, param, CommandType.StoredProcedure);

                popayment = dtPOPayment.AsEnumerable()
                            .Select(row => new POPaymentTermEntities
                            {
                                PayTermID = row.Field<int>("PayTermID"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc"),
                                PaymentTermCode = row.Field<string>("PaymentTermCode")
                            }).ToList();
            }
            return popayment;
        }

        public int CreateNewEntry(PurchaseOrderEntities poEntity, POPaymentTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("PayTermID", entity.PayTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOPaymentTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPOPaymentTerm, paramCollection, CommandType.StoredProcedure, "POID");
            return iResult;
        }
    }
}
