using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BISERPService.Controllers
{
     [RoutePrefix("api/Stockdispose")]
    public class StockdisposeController : ApiController
    {
          IStockDisposeRepository _stockDispose;
          IStockDisposeDetailsRepository __stockDisposeDetails;
          IStockdisposeCommonRepository _stockcommom;
          private static readonly ILogger _loggger = Logger.Register(typeof(StockdisposeController));

          public StockdisposeController(IStockDisposeRepository stockdispose, IStockDisposeDetailsRepository stockdisposeDetails, IStockdisposeCommonRepository storecommom)
        {
            _stockDispose = stockdispose;
            __stockDisposeDetails = stockdisposeDetails;
            _stockcommom = storecommom;
        }

         

          [Route("createStockdispose")]
          [AcceptVerbs("POST")]
          public IHttpActionResult CreateStockConsumption(StockDisposeEntity entity)
          {
              bool isSucecss = true;
              TryCatch.Run(() =>
              {
                  entity = _stockcommom.SaveDetails(entity);
                  if (entity.DisposeId == 0)
                  {
                      isSucecss = false;
                  }
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in StockdisposeReport method of StockdisposeController : parameter :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });

              if (isSucecss)
                  return Created<StockDisposeEntity>(Request.RequestUri + entity.DisposeId.ToString(), entity);
              else
                  return BadRequest();
          }

          [Route("stockdisposerpt/{fromdate}/{todate}/{StoreId}")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult StockdisposeReport(DateTime fromdate, DateTime todate, int StoreId)
          {
              List<StockDisposeEntity> grn = new List<StockDisposeEntity>();
              TryCatch.Run(() =>
              {
                  var list = _stockcommom.StockdisposeReport(fromdate, todate, StoreId);
                  if (list != null && list.Count() > 0)
                      grn = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in StockdisposeReport method of StockdisposeController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (grn.IsNull())
                  return Ok();
              else if (grn.Any())
                  return Ok(grn);
              else
                  return Ok(grn);
          }
      
    }
}
