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
    public class RequestForQuoteCommonRepository : IRequestForQuoteCommonRepository
    {
        IRequestForQuoteRepository _rfq;
        IRequestForQuoteDetailRepository _rfqDetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseIndentCommonRepository));

        public RequestForQuoteCommonRepository(IRequestForQuoteRepository rfq, IRequestForQuoteDetailRepository rfqDetails)
        {
            _rfq = rfq;
            _rfqDetails = rfqDetails;
        }

        public RequestForQuoteEntities CreateRequestForQuote(RequestForQuoteEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        var newEntity = _rfq.CreateRequestForQuote(entity, dbhelper);
                        entity.IndentId = newEntity.IndentId;
                        entity.IndentNumber = newEntity.IndentNumber;
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _rfqDetails.CreateRequestForQuoteDetails(entity.IndentId, IndentDetail, dbhelper);
                        }
                        if (entity.RFQDeliveryTerms != null)
                        {
                            foreach (var DeliveryTerm in entity.RFQDeliveryTerms)
                            {
                                DeliveryTerm.DelTermID = CreateRFQDeliveryTerms(entity, DeliveryTerm, dbhelper);
                            }
                        }
                        if (entity.RFQPaymenterms != null)
                        {
                            foreach (var PaymentTerm in entity.RFQPaymenterms)
                            {
                                PaymentTerm.PayTermID = CreateRFQPaymentTerms(entity, PaymentTerm, dbhelper);
                            }
                        }
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in CreateRequestForQuote method of RequestForQuoteCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return entity;
        }

        public bool UpdateRequestForQuote(RequestForQuoteEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _rfq.UpdateRequestForQuote(entity, dbhelper);
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _rfqDetails.CreateRequestForQuoteDetails(entity.IndentId, IndentDetail, dbhelper);
                            if (IndentDetail.IndentDetailId <= 0)
                            {
                                isSucecss = false;
                                break;
                            }
                        }
                        if (entity.RFQDeliveryTerms != null)
                        {
                            foreach (var DeliveryTerm in entity.RFQDeliveryTerms)
                            {
                                DeliveryTerm.DelTermID = CreateRFQDeliveryTerms(entity, DeliveryTerm, dbhelper);
                            }
                        }
                        if (entity.RFQPaymenterms != null)
                        {
                            foreach (var PaymentTerm in entity.RFQPaymenterms)
                            {
                                PaymentTerm.PayTermID = CreateRFQPaymentTerms(entity, PaymentTerm, dbhelper);
                            }
                        }

                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in UpdateRequestForQuote method of RequestForQuoteCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });

                    if (isSucecss)
                        dbhelper.CommitTransaction(transaction);
                    else
                        dbhelper.RollbackTransaction(transaction);
                }
            }
            return isSucecss;
        }

        public int CreateRFQDeliveryTerms(RequestForQuoteEntities RFQEntity, RFQDeliveryTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("RFQID", RFQEntity.IndentId, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("DelTermID", entity.DelTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", RFQEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", RFQEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", RFQEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", RFQEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", RFQEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", RFQEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", RFQEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPODeliveryTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertRFQDeliveryTerms, paramCollection, CommandType.StoredProcedure, "RFQID");
            return iResult;
        }

        public int CreateRFQPaymentTerms(RequestForQuoteEntities RFQEntity, RFQPaymentTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("RFQID", RFQEntity.IndentId, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("PayTermID", entity.PayTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", RFQEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", RFQEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", RFQEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", RFQEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", RFQEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", RFQEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", RFQEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOPaymentTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertRFQPaymentTerms, paramCollection, CommandType.StoredProcedure, "RFQID");
            return iResult;
        }

        public List<RFQDeliveryTermEntities> GetRFQDeliveryTerms(int Id)
        {
            List<RFQDeliveryTermEntities> RFQelivery = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("RFQID", Id, DbType.Int32);
                DataTable dtPODelivery = dbHelper.ExecuteDataTable(PurchaseQueries.GetRFQDeliveryTermByID, param, CommandType.StoredProcedure);

                RFQelivery = dtPODelivery.AsEnumerable()
                            .Select(row => new RFQDeliveryTermEntities
                            {
                                DelTermID = row.Field<int>("DelTermID"),
                                DeliveryTermDesc = row.Field<string>("DeliveryTermDesc"),
                                DeliveryTermCode = row.Field<string>("DeliveryTermCode")
                            }).ToList();
            }
            return RFQelivery;
        }

        public List<RFQPaymentTermEntities> GetRFQPaymenterms(int Id)
        {
            List<RFQPaymentTermEntities> PFQpayment = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("RFQID", Id, DbType.Int32);
                DataTable dtPOPayment = dbHelper.ExecuteDataTable(PurchaseQueries.GetRFQPaymentTermByID, param, CommandType.StoredProcedure);

                PFQpayment = dtPOPayment.AsEnumerable()
                            .Select(row => new RFQPaymentTermEntities
                            {
                                PayTermID = row.Field<int>("PayTermID"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc"),
                                PaymentTermCode = row.Field<string>("PaymentTermCode")
                            }).ToList();
            }
            return PFQpayment;
        }

        public bool AuthCancelRequestForQuote(RequestForQuoteEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _rfq.AuthCancelRequestForQuote(entity, dbhelper);
                        if (isSucecss)
                        {
                            foreach (var IndentDetail in entity.IndentDetails)
                            {
                                isSucecss = _rfqDetails.UpdateRequestForQuoteAuthQty(entity, IndentDetail, dbhelper);
                                if (!isSucecss)
                                {
                                    break;
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

        public bool AutoRFQGeneration(int PRId, DBHelper dbhelper)
        {
            bool success = false;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Success", success, DbType.Boolean, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("PRId", PRId, DbType.Int32));
            success = dbhelper.ExecuteNonQueryForOutParameter<bool>(PurchaseQueries.AutoRFQGeneration,paramCollection,CommandType.StoredProcedure,"Success");
            return success;
        }
    }
}
