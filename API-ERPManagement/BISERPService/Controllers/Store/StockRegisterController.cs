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
    [RoutePrefix("api/stockregister")]
    public class StockRegisterController : ApiController
    {
        IStockRegisterRepository _stockregister;
        private static readonly ILogger _loggger = Logger.Register(typeof(StockRegisterController));

        public StockRegisterController(IStockRegisterRepository stockregister)
        {
            _stockregister = stockregister;

        }

        [Route("getStorewiseStock/{StoreId}/{ItemtypeId}/{FromDate}/{ToDate}/{IssueTo}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStorewiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate, int IssueTo)
        {
            List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.GetStorewiseStock(StoreId, ItemtypeId, FromDate, ToDate, IssueTo);
                if (list != null && list.Any())
                {
                    var wsItemid = 0;

                    foreach (var drv in list.OrderBy(o => o.ItemID))
                    {
                        if (drv.ItemID != wsItemid)
                        {
                            var batchGroup = list.Where(s => (s.ItemID == drv.ItemID)).GroupBy(g => g.BatchName);
                            foreach (var grp in batchGroup)
                            {
                                double wsOpstk = 0, wsReceivedstk = 0, wsIssuestk = 0, wsBalancestk = 0;
                                var newEntry = new StockRegisterEntity();

                                newEntry.ItemID = drv.ItemID;
                                newEntry.ItemCode = drv.ItemCode;
                                newEntry.ItemName = drv.ItemName;
                                newEntry.ItemType = drv.ItemType;
                                newEntry.BatchName = grp.Key;

                                var itemGrp = list.Where(s => (s.ItemID == drv.ItemID) && (s.BatchName == grp.Key)).ToList();
                                foreach (var item in itemGrp)
                                {
                                    if (item.Description.ToUpper() == "OPENING BALANCE")
                                        wsOpstk = wsOpstk + item.QtyReceived;
                                    else
                                        wsReceivedstk = wsReceivedstk + item.QtyReceived;

                                    wsIssuestk = wsIssuestk + item.QtyIssued;
                                }

                                newEntry.OpeningBalance = wsOpstk;
                                newEntry.QtyReceived = wsReceivedstk;
                                newEntry.QtyIssued = wsIssuestk;

                                var stockRegisterEntity = itemGrp.OrderByDescending(t => t.DocDate).FirstOrDefault();
                                if (stockRegisterEntity != null)
                                    wsBalancestk = (stockRegisterEntity.BalanceQty);

                                newEntry.BalanceQty = wsBalancestk;
                                stckRegister.Add(newEntry);
                            }
                            wsItemid = drv.ItemID;
                        }
                    }
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }


        //[Route("getStorewiseStock/{StoreId}/{ItemtypeId}/{FromDate}/{ToDate}/{IssueTo}")]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult GetStorewiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate, int IssueTo)
        //{
        //    List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _stockregister.GetStorewiseStock(StoreId, ItemtypeId, FromDate, ToDate, IssueTo);
        //        if (list != null && list.Any())
        //        {
        //            var wsItemid = 0;
        //            foreach (var drv in list.OrderBy(o => o.ItemID))
        //            {
        //                if (drv.ItemID != wsItemid)
        //                {
        //                    double wsOpstk = 0, wsReceivedstk = 0, wsIssuestk = 0, wsBalancestk = 0;
        //                    var newEntry = new StockRegisterEntity();

        //                    newEntry.ItemID = drv.ItemID;
        //                    newEntry.ItemCode = drv.ItemCode;
        //                    newEntry.ItemName = drv.ItemName;
        //                    newEntry.ItemType = drv.ItemType;

        //                   // var itemGrp = list.Where(s => s.ItemID == drv.ItemID && s.BatchName == drv.BatchName ).ToList();
        //                    var itemGrp = list.Where(s => s.ItemID == drv.ItemID).ToList();
        //                    if (itemGrp.Any()) newEntry.OpeningBalance = itemGrp.OrderByDescending(od => od.DocDate).FirstOrDefault().OpeningBalance;
        //                    foreach (var item in itemGrp)
        //                    {
        //                        if (item.Description.ToUpper() == "OPENING BALANCE")
        //                            wsOpstk = wsOpstk + item.QtyReceived;
        //                        else
        //                            wsReceivedstk = wsReceivedstk + item.QtyReceived;

        //                        wsIssuestk = wsIssuestk + item.QtyIssued;
        //                    }

        //                    //newEntry.OpeningBalance = wsOpstk;
        //                    newEntry.QtyReceived = wsReceivedstk;
        //                    newEntry.QtyIssued = wsIssuestk;

        //                    var stockRegisterEntity = itemGrp.OrderByDescending(t => t.DocDate).FirstOrDefault();
        //                    if (stockRegisterEntity != null)
        //                        wsBalancestk = (stockRegisterEntity.BalanceQty);

        //                    newEntry.BalanceQty = wsBalancestk;
        //                    stckRegister.Add(newEntry);

        //                    wsItemid = drv.ItemID;
        //                }
        //            }
        //        }
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (stckRegister.Any())
        //        return Ok(stckRegister);
        //    else
        //        return BadRequest();
        //}

        




        //[Route("getStorewiseStock/{StoreId}/{ItemtypeId}/{FromDate}/{ToDate}/{IssueTo}")]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult GetStorewiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate, int IssueTo)
        //{
        //    List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _stockregister.GetStorewiseStock(StoreId, ItemtypeId, FromDate, ToDate, IssueTo);
        //        if (list != null && list.Count() > 0)
        //            stckRegister = list.ToList();
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (stckRegister.Any())
        //        return Ok(stckRegister);
        //    else
        //        return BadRequest();
        //}
     
        [Route("storestockregister/{fromdate}/{todate}/{StoreId}/{ItemtypeId}/{itemid}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StoreStockRegisterReport(DateTime fromdate, DateTime todate, int StoreId, int ItemtypeId, int itemid)
        {
            List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.StoreStockRegisterReport(fromdate, todate, StoreId, ItemtypeId, itemid);
                if (list != null && list.Any())
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }

        [Route("stockconsumption/{fromdate}/{todate}/{StoreId}/{ItemtypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StockConsumptionReport(DateTime fromdate, DateTime todate, int StoreId, int ItemtypeId)
        {
            List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.StockConsumptionReport(fromdate, todate, StoreId, ItemtypeId);
                if (list != null && list.Any())
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in StockConsumptionReport method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }

        [Route("expiryregister/{fromdate}/{MaxDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult ExpiryRegisterReport(DateTime fromdate, int MaxDays)
        {
            List<BatchEntity> expRegister = new List<BatchEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.ExpiryRegisterReport(fromdate, MaxDays);
                if (list != null && list.Any())
                    expRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in ExpiryRegisterReport method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (expRegister.Any())
                return Ok(expRegister);
            else
                return BadRequest();
        }

        [Route("stockevaluation/{todate}/{StoreId}/{ItemtypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StockEvaluationReport(DateTime todate, int StoreId, int ItemtypeId)
        {
            List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.StockEvaluationReport(todate, StoreId, ItemtypeId);
                if (list != null && list.Any())
                    stckRegister = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }


        [Route("getStoreItemwiseStock/{StoreId}/{ItemtypeId}/{FromDate}/{ToDate}")]
        [AcceptVerbs("GET", "POST")]

        public IHttpActionResult GetStoreItemwiseStock(int StoreId, int ItemtypeId, DateTime FromDate, DateTime ToDate)
        {
             List<StockRegisterEntity> stckRegister = new List<StockRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _stockregister.GetStoreItemwiseStock(StoreId, ItemtypeId, FromDate, ToDate);
                if (list != null && list.Any())
                {
                    var wsItemid = 0;
                    foreach (var drv in list.OrderBy(o => o.ItemID))
                    {
                        if (drv.ItemID != wsItemid)
                        {
                            double wsOpstk = 0, wsReceivedstk = 0, wsIssuestk = 0, wsBalancestk = 0;
                            var newEntry = new StockRegisterEntity();

                            newEntry.ItemID = drv.ItemID;
                            newEntry.ItemCode = drv.ItemCode;
                            newEntry.ItemName = drv.ItemName;
                            newEntry.ItemType = drv.ItemType;

                            var itemGrp = list.Where(s => s.ItemID == drv.ItemID).ToList();
                            if (itemGrp.Any()) newEntry.OpeningBalance = itemGrp.OrderBy(od => od.DocDate).FirstOrDefault().OpeningBalance;
                            foreach (var item in itemGrp)
                            {
                                if (item.Description.ToUpper() == "OPENING BALANCE")
                                    wsOpstk = wsOpstk + item.QtyReceived;
                                else
                                    wsReceivedstk = wsReceivedstk + item.QtyReceived;

                                wsIssuestk = wsIssuestk + item.QtyIssued;
                            }

                            //newEntry.OpeningBalance = wsOpstk;
                            newEntry.QtyReceived = wsReceivedstk;
                            newEntry.QtyIssued = wsIssuestk;

                            var stockRegisterEntity = itemGrp.OrderByDescending(t => t.DocDate).FirstOrDefault();
                            if (stockRegisterEntity != null)
                                wsBalancestk = (stockRegisterEntity.BalanceQty);

                            newEntry.BalanceQty = wsBalancestk;
                            stckRegister.Add(newEntry);

                            wsItemid = drv.ItemID;
                        }
                    }
                }



            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreItemwiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (stckRegister.Any())
                return Ok(stckRegister);
            else
                return BadRequest();
        }


    }
}
