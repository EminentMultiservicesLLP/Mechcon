using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/payterms")]
    public class PaymentTermsController : ApiController
    {
        IPaymentTermsMasterRepository _paymentMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(PaymentTermsController));

        public PaymentTermsController(IPaymentTermsMasterRepository paymentMaster)
        {
            _paymentMaster = paymentMaster;
        }

        private List<PaymentTermsMasterEntities> AllPaymentTerms()
        {
            List<PaymentTermsMasterEntities> payterm = new List<PaymentTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.PaymentTerms.ToString()))
                {
                    var list = _paymentMaster.GetAllPayment();
                    if (list != null && list.Count() > 0)
                        payterm = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.PaymentTerms.ToString(), payterm);
                }
                else
                {
                    payterm = (List<PaymentTermsMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.PaymentTerms.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllPaymentTerms method of PaymentTermsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            return payterm;
        }

        [Route("getallpayterms")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPayment()
        {
            List<PaymentTermsMasterEntities> payterm = new List<PaymentTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllPaymentTerms();
                if (list != null && list.Count() > 0)
                    payterm = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPayment method of PaymentTermsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (payterm.Any())
                return Ok(payterm);
            else
                return Ok(payterm);
        }

        [Route("getactive")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveTerms()
        {
            List<PaymentTermsMasterEntities> payterm = new List<PaymentTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllPaymentTerms();
                if (list != null && list.Count() > 0)
                    payterm = list.Where(p => p.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveTerms method of PaymentTermsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (payterm.Any())
                return Ok(payterm);
            else
                return BadRequest();
        }
       


        [Route("getpaytermbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPaymentById(int id)
        {
            PaymentTermsMasterEntities deliveries = new PaymentTermsMasterEntities();
            TryCatch.Run(() =>
            {
                deliveries = _paymentMaster.GetPaymentById(id);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPaymentById method of PaymentTermsController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (deliveries.IsNotNull())
                return Ok(deliveries);
            else
                return NotFound();
        }

        [Route("createpayterm")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreatePayment(PaymentTermsMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _paymentMaster.CheckDuplicateItem(entity.PaymentTermCode);
                if (isDuplicate == false)
                {
                    var newID = _paymentMaster.CreatePayment(entity);
                    entity.TermID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.PaymentTerms.ToString());
                }
              
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of  PaymentTermsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<PaymentTermsMasterEntities>(Request.RequestUri + entity.TermID.ToString(), entity);
            else
                return BadRequest();
        
        }

        [Route("updatepayterm")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePaymentTerm(PaymentTermsMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _paymentMaster.CheckDuplicateupdate(entity.PaymentTermCode, entity.TermID);
                if (isDuplicate == false)
                {
                    isSucecss = _paymentMaster.UpdatePaymentTerm(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.PaymentTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePaymentTerm method of PaymentTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<PaymentTermsMasterEntities>(Request.RequestUri + entity.TermID.ToString(), entity));
            else
                return BadRequest();
            
        }

    }
}
