using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class WorkOrderCommonRepository : IWorkOrderCommonRepository
    {
        IWorkOrderRepository _wo;
        IWorkOrderDetailRepository _woDetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseIndentCommonRepository));

        public WorkOrderCommonRepository(IWorkOrderRepository wo, IWorkOrderDetailRepository woDetails)
        {
            _wo = wo;
            _woDetails = woDetails;
        }

        public WorkOrderEntities CreateWorkOrder(WorkOrderEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        var newEntity = _wo.CreateWorkOrder(entity, dbhelper);
                        entity.IndentId = newEntity.IndentId;
                        entity.IndentNumber = newEntity.IndentNumber;
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _woDetails.CreateWorkOrderDetails(entity.IndentId, IndentDetail, dbhelper);
                        }
                        if (entity.WODeliveryTerms != null)
                        {
                            foreach (var DeliveryTerm in entity.WODeliveryTerms)
                            {
                                DeliveryTerm.DelTermID = CreateWODeliveryTerms(entity, DeliveryTerm, dbhelper);
                            }
                        }
                        if (entity.WOPaymenterms != null)
                        {
                            foreach (var PaymentTerm in entity.WOPaymenterms)
                            {
                                PaymentTerm.PayTermID = CreateWOPaymentTerms(entity, PaymentTerm, dbhelper);
                            }
                        }
                        if (entity.WOOtherTerms != null)
                        {
                            foreach (var OtherTerms in entity.WOOtherTerms)
                            {
                                OtherTerms.OtherTermID = CreateWOOtherTerms(entity, OtherTerms, dbhelper);
                            }
                        }
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in CreateWorkOrder method of WorkOrderCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return entity;
        }

        public bool UpdateWorkOrder(WorkOrderEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _wo.UpdateWorkOrder(entity, dbhelper);
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _woDetails.CreateWorkOrderDetails(entity.IndentId, IndentDetail, dbhelper);
                            if (IndentDetail.IndentDetailId <= 0)
                            {
                                isSucecss = false;
                                break;
                            }
                        }
                        if (entity.WODeliveryTerms != null)
                        {
                            foreach (var DeliveryTerm in entity.WODeliveryTerms)
                            {
                                DeliveryTerm.DelTermID = CreateWODeliveryTerms(entity, DeliveryTerm, dbhelper);
                            }
                        }
                        if (entity.WOPaymenterms != null)
                        {
                            foreach (var PaymentTerm in entity.WOPaymenterms)
                            {
                                PaymentTerm.PayTermID = CreateWOPaymentTerms(entity, PaymentTerm, dbhelper);
                            }
                        }
                        if (entity.WOOtherTerms != null)
                        {
                            foreach (var OtherTerms in entity.WOOtherTerms)
                            {
                                OtherTerms.OtherTermID = CreateWOOtherTerms(entity, OtherTerms, dbhelper);
                            }
                        }

                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in UpdateWorkOrder method of WorkOrderCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });

                    if (isSucecss)
                        dbhelper.CommitTransaction(transaction);
                    else
                        dbhelper.RollbackTransaction(transaction);
                }
            }
            return isSucecss;
        }

        public int CreateWODeliveryTerms(WorkOrderEntities WOEntity, WODeliveryTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("WOID", WOEntity.IndentId, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("DelTermID", entity.DelTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", WOEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", WOEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", WOEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPODeliveryTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertWODeliveryTerms, paramCollection, CommandType.StoredProcedure, "WOID");
            return iResult;
        }
        public int CreateWOPaymentTerms(WorkOrderEntities WOEntity, WOPaymentTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("WOID", WOEntity.IndentId, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("PayTermID", entity.PayTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", WOEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", WOEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", WOEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOPaymentTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertWOPaymentTerms, paramCollection, CommandType.StoredProcedure, "WOID");
            return iResult;
        }
        public int CreateWOOtherTerms(WorkOrderEntities WOEntity, WOOtherTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("WOID", WOEntity.IndentId, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("OtherTermID", entity.OtherTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", WOEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", WOEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", WOEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", WOEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", WOEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOOtherTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertWOOtherTerms, paramCollection, CommandType.StoredProcedure, "WOID");
            return iResult;
        }

        public List<WODeliveryTermEntities> GetWODeliveryTerms(int Id)
        {
            List<WODeliveryTermEntities> WOelivery = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("WOID", Id, DbType.Int32);
                DataTable dtPODelivery = dbHelper.ExecuteDataTable(PurchaseQueries.GetWODeliveryTermByID, param, CommandType.StoredProcedure);

                WOelivery = dtPODelivery.AsEnumerable()
                            .Select(row => new WODeliveryTermEntities
                            {
                                DelTermID = row.Field<int>("DelTermID"),
                                DeliveryTermDesc = row.Field<string>("DeliveryTermDesc"),
                                DeliveryTermCode = row.Field<string>("DeliveryTermCode")
                            }).ToList();
            }
            return WOelivery;
        }
        public List<WOPaymentTermEntities> GetWOPaymenterms(int Id)
        {
            List<WOPaymentTermEntities> PFQpayment = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("WOID", Id, DbType.Int32);
                DataTable dtPOPayment = dbHelper.ExecuteDataTable(PurchaseQueries.GetWOPaymentTermByID, param, CommandType.StoredProcedure);

                PFQpayment = dtPOPayment.AsEnumerable()
                            .Select(row => new WOPaymentTermEntities
                            {
                                PayTermID = row.Field<int>("PayTermID"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc"),
                                PaymentTermCode = row.Field<string>("PaymentTermCode")
                            }).ToList();
            }
            return PFQpayment;
        }
        public List<WOOtherTermEntities> GetWOOtherterms(int Id)
        {
            List<WOOtherTermEntities> PFQpayment = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("WOID", Id, DbType.Int32);
                DataTable dtPOOther = dbHelper.ExecuteDataTable(PurchaseQueries.GetWOOtherTermByID, param, CommandType.StoredProcedure);

                PFQpayment = dtPOOther.AsEnumerable()
                            .Select(row => new WOOtherTermEntities
                            {
                                OtherTermID = row.Field<int>("OtherTermID"),
                                OthersTermDesc = row.Field<string>("OthersTermDesc"),
                                OthersTermCode = row.Field<string>("OthersTermCode")
                            }).ToList();
            }
            return PFQpayment;
        }

        public bool AuthCancelWorkOrder(WorkOrderEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _wo.AuthCancelWorkOrder(entity, dbhelper);
                        if (isSucecss)
                        {
                            foreach (var IndentDetail in entity.IndentDetails)
                            {
                                isSucecss = _woDetails.UpdateWorkOrderAuthQty(entity, IndentDetail, dbhelper);
                                if (!isSucecss)
                                {
                                    break;
                                }
                            }
                            if (entity.WODeliveryTerms != null)
                            {
                                foreach (var DeliveryTerm in entity.WODeliveryTerms)
                                {
                                    DeliveryTerm.DelTermID = CreateWODeliveryTerms(entity, DeliveryTerm, dbhelper);
                                }
                            }
                            if (entity.WOPaymenterms != null)
                            {
                                foreach (var PaymentTerm in entity.WOPaymenterms)
                                {
                                    PaymentTerm.PayTermID = CreateWOPaymentTerms(entity, PaymentTerm, dbhelper);
                                }
                            }
                            if (entity.WOOtherTerms != null)
                            {
                                foreach (var OtherTerms in entity.WOOtherTerms)
                                {
                                    OtherTerms.OtherTermID = CreateWOOtherTerms(entity, OtherTerms, dbhelper);
                                }
                            }
                        }
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in AuthCancelPurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });

                    if (isSucecss)
                        dbhelper.CommitTransaction(transaction);
                    else
                        dbhelper.RollbackTransaction(transaction);
                }
            }
            return isSucecss;
        }

    }
}
