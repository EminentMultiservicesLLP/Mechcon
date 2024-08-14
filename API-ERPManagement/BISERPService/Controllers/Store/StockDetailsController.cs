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
    [RoutePrefix("api/stockdetails")]
    public class StockDetailsController : ApiController
    {
        IStockDetailsRepository _stockdetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(StockDetailsController));

        public StockDetailsController(IStockDetailsRepository stockregister)
        {
            _stockdetails = stockregister;

        }

        [Route("getStockDetails/{StoreId}/{ItemtypeId}/{BranchID}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStorewiseStock(int StoreId, int ItemtypeId, int BranchID)
        {
            List<StockDetailsEntity> stckRegister = new List<StockDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockdetails.GetStoreWisestockData(StoreId, ItemtypeId, BranchID);
                if (list != null && list.Count() > 0)
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseStock method of StockDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.IsNotNull() && stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }

        [Route("storestocksummary/{StoreId}/{ItemtypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StoreStockSummary(int StoreId, int ItemtypeId)
        {
            List<StockDetailsEntity> stckRegister = new List<StockDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockdetails.StoreStockSummary(StoreId, ItemtypeId);
                if (list != null && list.Count() > 0)
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in StoreStockSummary method of StockDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.IsNotNull() && stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }

        [Route("storestockdetails/{StoreId}/{ItemtypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StoreStockDetails(int StoreId, int ItemtypeId)
        {
            List<StockDetailsEntity> stckRegister = new List<StockDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockdetails.StoreStockDetails(StoreId, ItemtypeId);
                if (list != null && list.Count() > 0)
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in StoreStockDetails method of StockDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.IsNotNull() && stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }
    }
}
