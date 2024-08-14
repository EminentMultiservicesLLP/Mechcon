using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.QueryCollection.Purchase;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class PurchaseIndentCommonRepository : IPurchaseIndentCommonRepository
    {
        IPurchaseIndentRepository _ipIndent;
        IPurchaseIndentDetailRepository _ipIndentDetails;
        IRequestStatusRepository _requestStatus;
        IRequestForQuoteCommonRepository _rfqCommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseIndentCommonRepository));

        public PurchaseIndentCommonRepository(IPurchaseIndentRepository ipIndent, IPurchaseIndentDetailRepository ipIndentDetails,
                                            IRequestStatusRepository requestStatus, IRequestForQuoteCommonRepository rfqCommon)
        {
            _ipIndent = ipIndent;
            _ipIndentDetails = ipIndentDetails;
            _requestStatus = requestStatus;
            _rfqCommon = rfqCommon;
        }

        public PurchaseIndentEntities CreatePurchaseIndent(PurchaseIndentEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        var newEntity = _ipIndent.CreatePurchaseIndent(entity, dbhelper);
                        entity.IndentId = newEntity.IndentId;
                        entity.IndentNumber = newEntity.IndentNumber;
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _ipIndentDetails.CreatePurchaseIndentDetails(entity.IndentId, entity.Storeid, IndentDetail, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in CreatePurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return entity;
        }

        public bool UpdatePurchaseIndent(PurchaseIndentEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _ipIndent.UpdatePurchaseIndent(entity, dbhelper);
                        foreach (var IndentDetail in entity.IndentDetails)
                        {
                            IndentDetail.IndentDetailId = _ipIndentDetails.CreatePurchaseIndentDetails(entity.IndentId, entity.Storeid, IndentDetail, dbhelper);
                            if (IndentDetail.IndentDetailId <= 0)
                            {
                                isSucecss = false;
                                //dbhelper.RollbackTransaction(transaction);
                                break;
                            }
                        }

                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in UpdatePurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });

                    if (isSucecss)
                        dbhelper.CommitTransaction(transaction);
                    else
                        dbhelper.RollbackTransaction(transaction);
                }
            }
            return isSucecss;
        }

        public bool AuthCancelPurchaseIndent(PurchaseIndentEntities entity)
        {
            bool isSuccess = false;

            using (DBHelper dbhelper = new DBHelper())
            using (IDbTransaction transaction = dbhelper.BeginTransaction())
            {
                try
                {
                    isSuccess = _ipIndent.AuthCancelPurchaseIndent(entity, dbhelper);

                    if (isSuccess)
                    {
                        foreach (var indentDetail in entity.IndentDetails)
                        {
                            isSuccess = _ipIndentDetails.UpdatePurchaseIndentAuthQty(entity, indentDetail, dbhelper);
                            if (!isSuccess)
                            {
                                break;
                            }
                        }
                    }

                    if (isSuccess && entity.AuthorizationStatusId == 2)
                    {
                        isSuccess = _rfqCommon.AutoRFQGeneration(entity.IndentId, dbhelper);
                    }

                    if (isSuccess)
                    {
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }
                catch (Exception ex)
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelPurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    throw; // Optionally rethrow the exception to handle it further up the call stack
                }
            }

            return isSuccess;
        }

        public PurchaseIndentEntities SaveIndentTemplate(PurchaseIndentEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        var newEntity = SaveIndentTemplate(entity, dbhelper);
                        if (newEntity.IndentIdTemplateId > 0)
                        {
                            foreach (var IndentDetail in entity.IndentDetails)
                            {
                                IndentDetail.IndentIdTemplateId = entity.IndentIdTemplateId;
                                IndentDetail.IndentDetailId = SaveIndentTemplateDetails(IndentDetail, dbhelper);
                            }
                        }
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in SaveIndentTemplate method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return entity;
        }

        public PurchaseIndentEntities SaveIndentTemplate(PurchaseIndentEntities entity, DBHelper dbhelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentIdTemplateId", entity.IndentIdTemplateId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentTemplateName", entity.IndentTemplateName, DbType.String));
            paramCollection.Add(new DBParameter("StoreId", entity.Storeid, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemCategoryId", entity.ItemCategoryId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            var parameterList = dbhelper.ExecuteNonQueryForOutParameter(PurchaseQueries.SaveIndentTemplate, paramCollection, CommandType.StoredProcedure);
            entity.IndentIdTemplateId = Convert.ToInt32(parameterList["IndentIdTemplateId"].ToString());
            return entity;
        }

        public int SaveIndentTemplateDetails(PurchaseIndentDetailEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetailId", entity.IndentDetailId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentIdTemplateId", entity.IndentIdTemplateId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemQty", entity.ItemQty, DbType.Double));
            paramCollection.Add(new DBParameter("ItemRate", entity.ItemRate, DbType.Double));
            paramCollection.Add(new DBParameter("EstimatedCost", entity.EstimatedCost, DbType.Double));
            paramCollection.Add(new DBParameter("LandingRate", entity.LandingRate, DbType.Double));
            paramCollection.Add(new DBParameter("packsizeid", entity.packsizeid, DbType.Int32));
            paramCollection.Add(new DBParameter("freeqty", entity.freeqty, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("IssueQty", entity.IssueQty, DbType.Int32));
            paramCollection.Add(new DBParameter("Consumeqty", entity.Consumeqty, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("IndentRemark", entity.IndentRemark, DbType.String));
            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.SaveIndentTemplateDetails, paramCollection, CommandType.StoredProcedure, "IndentDetailId");
            return iResult;
        }
    }
}
