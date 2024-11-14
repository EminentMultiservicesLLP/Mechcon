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
    [RoutePrefix("api/enquiryRegister")]
    public class EnquiryRegisterController : ApiController
    {
        IEnquiryRegisterRepository _enquiryRegister;
        private static readonly ILogger _loggger = Logger.Register(typeof(EnquiryRegisterController));

        public EnquiryRegisterController(IEnquiryRegisterRepository enquiryRegister)
        {
            _enquiryRegister = enquiryRegister;
        }

        [Route("getSource")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSources()
        {
            try
            {
                var data = _enquiryRegister.GetSources().ToList();

                if (data == null || !data.Any())
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSources method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getProducts")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProducts()
        {
            try
            {
                var products = _enquiryRegister.GetProducts().ToList();

                if (products == null || !products.Any())
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetProducts method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getLocations")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetLocations()
        {
            try
            {
                var locations = _enquiryRegister.GetLocations().ToList();

                if (locations == null || !locations.Any())
                {
                    return NotFound();
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetLocations method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getTypes")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetTypes()
        {
            try
            {
                var types = _enquiryRegister.GetTypes().ToList();

                if (types == null || !types.Any())
                {
                    return NotFound();
                }

                return Ok(types);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetTypes method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getSectors")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSectors()
        {
            try
            {
                var types = _enquiryRegister.GetSectors().ToList();

                if (types == null || !types.Any())
                {
                    return NotFound();
                }

                return Ok(types);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSectors method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }


        [Route("getZones")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetZones()
        {
            try
            {
                var zones = _enquiryRegister.GetZones().ToList();

                if (zones == null || !zones.Any())
                {
                    return NotFound();
                }

                return Ok(zones);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetZones method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getEnqStatus")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetEnqStatus()
        {
            try
            {
                var enqStatus = _enquiryRegister.GetEnqStatus().ToList();

                if (enqStatus == null || !enqStatus.Any())
                {
                    return NotFound();
                }

                return Ok(enqStatus);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqStatus method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getStatus")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStatus()
        {
            try
            {
                var status = _enquiryRegister.GetStatus().ToList();

                if (status == null || !status.Any())
                {
                    return NotFound();
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetStatus method of EnquiryRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("saveEnquiry")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveEnquiry(EnquiryRegisterEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Enquiry = _enquiryRegister.SaveEnquiry(model);
                model = Enquiry;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<EnquiryRegisterEntities>(Request.RequestUri + model.EnquiryID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getEnquiry")]
        [Route("getEnquiry/{statusID}")]
        [HttpGet]
        public IHttpActionResult GetEnquiry(int? statusID = null)
        {
            try
            {
                var data = _enquiryRegister.GetEnquiry(statusID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnquiry of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getEnquiryDetails/{enquiryId}")]
        [HttpGet]
        public IHttpActionResult GetEnquiryDetails(int enquiryId)
        {
            try
            {
                var data = _enquiryRegister.GetEnquiryDetails(enquiryId);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnquiryDetails of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getEnqRegFollowUp")]
        [Route("getEnqRegFollowUp/{enquiryId}")]
        [HttpGet]
        public IHttpActionResult GetEnqRegFollowUp(int? enquiryId = null)
        {
            try
            {
                var data = _enquiryRegister.GetEnqRegFollowUp(enquiryId);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqRegFollowUp of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}