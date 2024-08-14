using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
  public class PurchaseReturnCommanRepository :IPurchaseReturnCommonRepository
    {
        IPurchaseReturnRepository _purchaseReturn;
        IPurchaseReturnDetailsRepository _purchasedtReturn;
       
        //IMaterialReturnDetailsRepository _purchasedetailReturn;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseReturnCommanRepository));

        public PurchaseReturnCommanRepository(IPurchaseReturnRepository purchaseReturn, IPurchaseReturnDetailsRepository purchasedtReturn)
        {
            _purchaseReturn = purchaseReturn;
            _purchasedtReturn = purchasedtReturn;
        }
        public PurchaseReturnEntity SavePREDetails(PurchaseReturnEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _purchaseReturn.CreatePurchaseReturn(entity, dbhelper);
                    entity.ID = newEntity.ID;
                    entity.PRNo = newEntity.PRNo;

                    if (entity.ID > 0)
                    {
                        foreach (var ReturnDetail in entity.PurchaseReturnDt)
                        {
                            ReturnDetail.PrdtID = _purchasedtReturn.CreatePurchaseReturntDetails(entity, ReturnDetail, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
    }
}
