using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.SM
{
    [RoutePrefix("api/orderBook")]
    public class OrderBookController : ApiController
    {
        IOrderBookRepository _orderBook;
        private static readonly ILogger _loggger = Logger.Register(typeof(OrderBookController));

        public OrderBookController(IOrderBookRepository orderBook)
        {
            _orderBook = orderBook;
        }


        [Route("getEnqForOrderBook/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetEnqForOrderBook(int UserID)
        {
            try
            {
                var details = _orderBook.GetEnqForOrderBook(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForOrderBook of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getConsignee")]
        [Route("getConsignee/{enquiryId}")]
        [HttpGet]
        public IHttpActionResult GetConsignee(int? enquiryId = null)
        {
            try
            {
                var data = _orderBook.GetConsignee(enquiryId);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetConsignee of OrderBookController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getFinalOffer/{EnquiryID}")]
        [HttpGet]
        public IHttpActionResult GetFinalOffer(int EnquiryID)
        {
            try
            {
                var details = _orderBook.GetFinalOffer(EnquiryID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetFinalOffer of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("saveOrderBook")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveOrderBook(OrderBookEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var OrderBook = _orderBook.SaveOrderBook(model);
                model = OrderBook;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of OrderBookController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<OrderBookEntities>(Request.RequestUri + model.EnquiryID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getOrderBook/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetOrderBook(int UserID)
        {
            try
            {
                var details = _orderBook.GetOrderBook(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForOrderBook of OrderBookController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOBOtherDetails/{OrderBookID}")]
        [HttpGet]
        public IHttpActionResult GetOBOtherDetails(int OrderBookID)
        {
            try
            {
                var details = _orderBook.GetOBOtherDetails(OrderBookID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOBOtherDetails of OrderBookController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOrderBookProjectTC/{OrderBookID}")]
        [HttpGet]
        public IHttpActionResult GetOrderBookProjectTC(int OrderBookID)
        {
            try
            {
                var details = _orderBook.GetOrderBookProjectTC(OrderBookID);

                if (details == null || !details.Any())
                {
                    return NotFound(); // or return BadRequest("No project TC details found for the specified order book ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOrderBookProjectTC of OrderBookController: parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOrderBookPaymentTerms/{OrderBookID}")]
        [HttpGet]
        public IHttpActionResult GetOrderBookPaymentTerms(int OrderBookID)
        {
            try
            {
                var details = _orderBook.GetOrderBookPaymentTerms(OrderBookID);

                if (details == null || !details.Any())
                {
                    return NotFound(); // or return BadRequest("No payment terms found for the specified order book ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOrderBookPaymentTerms of OrderBookController: parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOrderBookDeliveryTerms/{OrderBookID}")]
        [HttpGet]
        public IHttpActionResult GetOrderBookDeliveryTerms(int OrderBookID)
        {
            try
            {
                var details = _orderBook.GetOrderBookDeliveryTerms(OrderBookID);

                if (details == null || !details.Any())
                {
                    return NotFound(); // or return BadRequest("No delivery terms found for the specified order book ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOrderBookDeliveryTerms of OrderBookController: parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getIncoTerm")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetIncoTerm()
        {
            try
            {
                var incoTerm = _orderBook.GetIncoTerm().ToList();

                if (incoTerm == null || !incoTerm.Any())
                {
                    return NotFound();
                }

                return Ok(incoTerm);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetIncoTerm method of OrderBookController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

    }
}