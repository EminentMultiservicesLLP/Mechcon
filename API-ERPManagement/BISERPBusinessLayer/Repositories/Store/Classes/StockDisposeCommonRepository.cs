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
using BISERPBusinessLayer.QueryCollection.Store;


namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class StockDiposeCommonRepository : IStockdisposeCommonRepository
    {
        IStockDisposeRepository _stockDispose;
        IStockDisposeDetailsRepository _stockDisposeDetails;
          private static readonly ILogger _loggger = Logger.Register(typeof(StockDiposeCommonRepository));

          public StockDiposeCommonRepository(IStockDisposeRepository stockDispose, IStockDisposeDetailsRepository stockDisposeDetails)
        {
            _stockDispose = stockDispose;
            _stockDisposeDetails = stockDisposeDetails;
        }
          public StockDisposeEntity SaveDetails(StockDisposeEntity entity)
          {
              using (DBHelper dbhelper = new DBHelper())
              {
                  IDbTransaction transaction = dbhelper.BeginTransaction();
                  TryCatch.Run(() =>
                  {
                      var newEntity = _stockDispose.CreateStockdispose(entity, dbhelper);
                      entity.DisposeId = newEntity.DisposeId;
                      entity.DisposeNo = newEntity.DisposeNo;

                      if (entity.DisposeId > 0)
                      {
                          foreach (var Detail in entity.StockDisposedt)
                          {
                              Detail.DisposeDetailId = _stockDisposeDetails.StockDisposeDetails(entity, Detail, dbhelper);
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

          public IEnumerable<StockDisposeEntity> StockdisposeReport(DateTime fromdate, DateTime todate, int StoreId)
          {
              List<StockDisposeEntity> grn = new List<StockDisposeEntity>();
              using (DBHelper dbHelper = new DBHelper())
              {
                  DBParameterCollection paramCollection = new DBParameterCollection();
                  paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                  paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                  paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                  DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetAllStockDisposeRpt, paramCollection, CommandType.StoredProcedure);
                  grn = dtgrn.AsEnumerable()
                              .Select(row => new StockDisposeEntity
                              {
                                  DisposeId = row.Field<int>("DisposeId"),
                                  DisposeNo = row.Field<string>("DisposeNo"),
                                  DisposeDate = row.Field<DateTime?>("DisposeDate"),
                                  StoreName = row.Field<string>("StoreName"),
                                  StoreId = row.Field<int>("StoreId"),
                              }).GroupBy(test => test.DisposeId).Select(grp => grp.First()).ToList();
                  foreach (var M in grn)
                  {
                      M.StockDisposedt = dtgrn.AsEnumerable().Select(dtrow => new StockDisposeDtEntity
                      {
                          DisposeDetailId = dtrow.Field<int>("DisposeDetailId"),
                          DisposeId = dtrow.Field<int>("DisposeId"),
                          ItemID = dtrow.Field<int>("ItemId"),
                          ItemName = dtrow.Field<string>("ItemName"),
                          DisposedQuantity = dtrow.Field<double?>("DisposedQuantity"),
                          Reason = dtrow.Field<string>("Reason"),
                      }).Where(mo => mo.DisposeId == M.DisposeId).ToList();
                  }
              }
              return grn;
          }
    }
}
