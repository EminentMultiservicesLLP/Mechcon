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
    public class StoreConsumptionCommonRepository : IStoreConsumptionCommonRepository
    {
           IStoreConsumptionRepository _storeConsumption;
          IStoreConsumptionDetailsRepository _storeConsumptionDetails;
          private static readonly ILogger _loggger = Logger.Register(typeof(StoreConsumptionCommonRepository));

          public StoreConsumptionCommonRepository(IStoreConsumptionRepository stockConsumption, IStoreConsumptionDetailsRepository stockConsumptionDetails)
        {
            _storeConsumption = stockConsumption;
            _storeConsumptionDetails = stockConsumptionDetails;
        }
          public StoreConsumptionEntities SaveDetails(StoreConsumptionEntities entity)
          {
              using (DBHelper dbhelper = new DBHelper())
              {
                  IDbTransaction transaction = dbhelper.BeginTransaction();
                  TryCatch.Run(() =>
                  {
                      var newEntity = _storeConsumption.CreateStockConsumption(entity, dbhelper);
                      entity.ConsumptionId = newEntity.ConsumptionId;
                      entity.ConsumptionCode = newEntity.ConsumptionCode;

                      if (entity.ConsumptionId > 0)
                      {
                          foreach (var Detail in entity.StockConsumptiondt)
                          {
                              Detail.ConsumptionDtlId = _storeConsumptionDetails.StockConsumptionDetails(entity, Detail, dbhelper);
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
