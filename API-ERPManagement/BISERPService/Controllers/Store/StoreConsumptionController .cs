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
     [RoutePrefix("api/StockConsumption")]
    public class StoreConsumptionController : ApiController
    {
          IStoreConsumptionRepository _storeConsumption;
          IStoreConsumptionDetailsRepository _storeConsumptionDetails;
          IStoreConsumptionCommonRepository _storecommom;
          private static readonly ILogger _loggger = Logger.Register(typeof(StoreConsumptionController));

          public StoreConsumptionController(IStoreConsumptionRepository stockConsumption, IStoreConsumptionDetailsRepository stockConsumptionDetails, IStoreConsumptionCommonRepository storecommom)
        {
            _storeConsumption = stockConsumption;
            _storeConsumptionDetails = stockConsumptionDetails;
            _storecommom = storecommom;
        }

          [Route("getItemconsumption/{StoreId}")]
          [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreconsumption(int StoreId)
        {
            List<StoreConsumptionDetailsEntities> stockConsumption = new List<StoreConsumptionDetailsEntities>();
            TryCatch.Run(() =>
            {               
                 var list = _storeConsumption.GetItemforConsume(StoreId);
                if (list != null && list.Count() > 0)
                    stockConsumption = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreconsumption method of StoreConsumptionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stockConsumption.Any())
                return Ok(stockConsumption);
            else
                return BadRequest();
          
        }

          [Route("createStockConsumption")]
          [AcceptVerbs("POST")]
          public IHttpActionResult CreateStockConsumption(StoreConsumptionEntities entity)
          {
              bool isSucecss = true;
              TryCatch.Run(() =>
              {
                  entity = _storecommom.SaveDetails(entity);
                  if (entity.ConsumptionId == 0)
                  {
                      isSucecss = false;
                  }
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in CreatePurchaseRetuCreateStockConsumptionrn method of StoreConsumptionController : parameter :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });

              if (isSucecss)
                  return Created<StoreConsumptionEntities>(Request.RequestUri + entity.ConsumptionId.ToString(), entity);
              else
                  return BadRequest();
          }
          [Route("getConsumptionNO")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetConsumptionNO()
          {
              List<StoreConsumptionEntities> stockConsumption = new List<StoreConsumptionEntities>();
              TryCatch.Run(() =>
              {
                  var list = _storeConsumption.GetALLConsumptionNo();
                  if (list != null && list.Count() > 0)
                      stockConsumption = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetConsumptionNO method of StoreConsumptionController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (stockConsumption.Any())
                  return Ok(stockConsumption);
              else
                  return BadRequest();
          }
          [Route("getconsumptiondt/{Id}")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult Getconsumptiondt(int Id)
          {
              List<StoreConsumptionCancelEntities> stockConsumption = new List<StoreConsumptionCancelEntities>();
              TryCatch.Run(() =>
              {
                  var list = _storeConsumptionDetails.GetConsumptionDT(Id);
                  if (list != null && list.Count() > 0)
                      stockConsumption = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in Getconsumptiondt method of StoreConsumptionController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (stockConsumption.Any())
                  return Ok(stockConsumption);
              else
                  return BadRequest();
          }
          [Route("createStockcancel")]
          [AcceptVerbs("POST")]
          public IHttpActionResult CreateStockConsumptioncancel(List<StoreConsumptionCancelEntities> entity)
          {
              bool isSucecss = false;
              TryCatch.Run(() =>
              {
                  entity = _storeConsumptionDetails.CreateStockcancel(entity);
                  isSucecss = true;
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in CreateStockConsumptioncancel method of StoreConsumptionController : parameter :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });

              if (isSucecss)
                  return Ok();
              else
                  return BadRequest();
          }
    }
}
