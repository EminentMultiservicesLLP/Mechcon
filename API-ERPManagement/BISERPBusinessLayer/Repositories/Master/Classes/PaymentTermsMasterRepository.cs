using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Masters;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
   public  class PaymentTermsMasterRepository:IPaymentTermsMasterRepository
    {
       public IEnumerable<PaymentTermsMasterEntities> GetAllPayment()
       {
           List<PaymentTermsMasterEntities> payments = null;
           using (DBHelper dbHelper = new DBHelper())
           {
               DataTable dtpayments = dbHelper.ExecuteDataTable(MasterQueries.GetAllPayment, CommandType.StoredProcedure);
               payments = dtpayments.AsEnumerable().Select(row => new PaymentTermsMasterEntities
               {
                   TermID = row.Field<int>("TermID"),
                   PaymentTermDesc = row.Field<string>("PaymentTermDesc").NullToString(),
                   PaymentTermCode = row.Field<string>("PaymentTermCode"),
                   Deactive = row.Field<Boolean>("Deactive")
               }).ToList();
               if (payments == null || payments.Count == 0)
                   payments.Add(new PaymentTermsMasterEntities
                   {
                       TermID = 0,
                       PaymentTermDesc = "",
                       PaymentTermCode = "",
                       Deactive = false
                   });               
           }
           return payments;
       }

        public IEnumerable<PaymentTermsMasterEntities> GetActiveTerms()
        {
            List<PaymentTermsMasterEntities> payments = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtpayments = dbHelper.ExecuteDataTable(MasterQueries.GetAllPayment, param, CommandType.StoredProcedure);
                payments = dtpayments.AsEnumerable().Select(row => new PaymentTermsMasterEntities
                {
                    TermID = row.Field<int>("TermID"),
                    PaymentTermDesc = row.Field<string>("PaymentTermDesc").NullToString(),
                    PaymentTermCode = row.Field<string>("PaymentTermCode"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                return payments;
            }
        }

        public PaymentTermsMasterEntities GetPaymentById(int termid)
        {
            PaymentTermsMasterEntities payments = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Termid", termid, DbType.Int32);
                DataTable dtpayments = dbHelper.ExecuteDataTable(MasterQueries.GetPaymentById, param, CommandType.StoredProcedure);

                payments = dtpayments.AsEnumerable()
                            .Select(row => new PaymentTermsMasterEntities
                            {
                                TermID = row.Field<int>("TermID"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc").NullToString(),
                                PaymentTermCode = row.Field<string>("PaymentTermCode"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }

            return payments; 
        }

        public int CreatePayment(PaymentTermsMasterEntities paymentEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TermID", paymentEntity.TermID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("PaymentTermCode", paymentEntity.PaymentTermCode, DbType.String));
                paramCollection.Add(new DBParameter("PaymentTermDesc", paymentEntity.PaymentTermDesc, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", paymentEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", paymentEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", paymentEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", paymentEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", paymentEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", paymentEntity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertPaymentMaster, paramCollection, CommandType.StoredProcedure, "TermID");
            }
            return iResult;
        }

        public bool UpdatePaymentTerm(PaymentTermsMasterEntities paymentEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TermID", paymentEntity.TermID, DbType.Int32));
                paramCollection.Add(new DBParameter("PaymentTermCode", paymentEntity.PaymentTermCode, DbType.String));
                paramCollection.Add(new DBParameter("PaymentTermDesc", paymentEntity.PaymentTermDesc, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", paymentEntity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", paymentEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", paymentEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", paymentEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", paymentEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", paymentEntity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdatePaymentMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string PaymentTermCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Payment", DbType.String));
                paramCollection.Add(new DBParameter("Code", PaymentTermCode, DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Payment", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
