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
    [RoutePrefix("api/offerRegister")]
    public class OfferRegisterController : ApiController
    {
        IOfferRegisterRepository _offerRegister;
        private static readonly ILogger _loggger = Logger.Register(typeof(OfferRegisterController));

        public OfferRegisterController(IOfferRegisterRepository offerRegister)
        {
            _offerRegister = offerRegister;
        }


        [Route("getEnqForOffer/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetEnqForOffer(int UserID)
        {
            try
            {
                var details = _offerRegister.GetEnqForOffer(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForOffer of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOfferID/{EnquiryID}")]
        [HttpGet]
        public IHttpActionResult GetOfferID(int EnquiryID)
        {
            try
            {
                var details = _offerRegister.GetOfferID(EnquiryID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOfferID of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("saveOffer")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveOffer(OfferRegisterEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Offer = _offerRegister.SaveOffer(model);
                model = Offer;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of OfferRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<OfferRegisterEntities>(Request.RequestUri + model.EnquiryID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getOffer/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetOffer(int UserID)
        {
            try
            {
                var details = _offerRegister.GetOffer(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForOffer of OfferRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getOfferDetails/{OfferRegisterID}")]
        [HttpGet]
        public IHttpActionResult GetOfferDetails(int OfferRegisterID)
        {
            try
            {
                var details = _offerRegister.GetOfferDetails(OfferRegisterID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOfferDetails of OfferRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}