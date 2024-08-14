using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Purchase
{
    [RoutePrefix("api/RequestForQuote")]
    public class RequestForQuoteController : ApiController
    {
        IRequestForQuoteRepository _rfq;
        IRequestForQuoteDetailRepository _rfqDetails;
        IRequestForQuoteCommonRepository _rfqCommon;
        IMechconMasterRepository _iGetMechconData;
   
        private static readonly ILogger _loggger = Logger.Register(typeof(RequestForQuoteController));

        public RequestForQuoteController(IRequestForQuoteRepository rfq, IRequestForQuoteDetailRepository rfqDetails, IRequestForQuoteCommonRepository rfqCommon, IMechconMasterRepository iGetMechconData)
        {
            _rfq = rfq;
            _rfqDetails = rfqDetails;
            _rfqCommon = rfqCommon;
            _iGetMechconData = iGetMechconData;
        }

        [Route("CreateRequestForQuote")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateRequestForQuote(RequestForQuoteEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                var newEntity = _rfqCommon.CreateRequestForQuote(entity);
                entity = newEntity;
                if (entity.IndentId == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateRequestForQuote method of RequestForQuoteController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<RequestForQuoteEntities>(Request.RequestUri + entity.IndentId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("UpdateRequestForQuote")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateRequestForQuote(RequestForQuoteEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _rfqCommon.UpdateRequestForQuote(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateRequestForQuote method of RequestForQuoteController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetAllRequestForQuote")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRequestForQuote()
        {
            List<RequestForQuoteEntities> RFQ = new List<RequestForQuoteEntities>();
            TryCatch.Run(() =>
            {
                var list = _rfq.GetAllRequestForQuote();
                if (list != null && list.Count() > 0)
                    RFQ = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllRequestForQuote method of RequestForQuoteController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (RFQ.Any())
                return Ok(RFQ);
            else
                return BadRequest();
        }

        [Route("GetRequestForQuoteById/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetRequestForQuoteById(int id)
        {
            RequestForQuoteEntities RFQ = new RequestForQuoteEntities();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                RFQ = _rfq.GetRequestForQuoteById(id);
                RFQ.IndentDetails = _rfqDetails.GetRequestForQuoteDetailById(id);
                RFQ.RFQDeliveryTerms = _rfqCommon.GetRFQDeliveryTerms(id);
                RFQ.RFQPaymenterms = _rfqCommon.GetRFQPaymenterms(id);
               
                RFQ.FinancialYear = RFQ.IndentNumber.Split('/')[1];

                mechconMaster = _iGetMechconData.GeMechconData();
                RFQ.companyName = mechconMaster.Name;
                RFQ.companyAddress = mechconMaster.Address;
                RFQ.companyGST = mechconMaster.GSTNumber;
                RFQ.companyCIN = mechconMaster.CINNumber;
                RFQ.companyEmail = mechconMaster.emailID;

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRequestForQuoteById method of RequestForQuoteController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (RFQ.IsNotNull())
                return Ok(RFQ);
            else
                return NotFound();
        }

        [Route("AuthCancelRequestForQuote")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelRequestForQuote(RequestForQuoteEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                isSucecss = _rfqCommon.AuthCancelRequestForQuote(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelRequestForQuote method of RequestForQuoteController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetAuthorizedRequestForQuote/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAuthorizedRequestForQuote(int StoreId)
        {
            List<RequestForQuoteEntities> RFQ = new List<RequestForQuoteEntities>();
            TryCatch.Run(() =>
            {
                var list = _rfq.GetAuthorizedRequestForQuote(StoreId);
                if (list.IsNotNull() && list.Any())
                    RFQ = list.ToList();
            }).IfNotNull(ex =>
            {
                RFQ = null;
                _loggger.LogError("Error in GetAuthorizedRequestForQuote method of RequestForQuoteController :" + Environment.NewLine + ex.StackTrace);

            });
            if (RFQ.IsNotNull())
                return Ok(RFQ);
            //return BadRequest();
            return InternalServerError();
        }

        [Route("RFQforReport")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult RFQforReport()
        {
            List<RequestForQuoteEntities> requestforquote = new List<RequestForQuoteEntities>();
            TryCatch.Run(() =>
            {
                var list = _rfq.RFQforReport();
                if (list != null && list.Count() > 0)
                    requestforquote = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in RFQforReport method of Request For Quote Controller :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (requestforquote.Any())
                return Ok(requestforquote);
            else
                return BadRequest();
        }
    }
}