using BISERPBusinessLayer.Entities.Ticket;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPBusinessLayer.Repositories.Ticket.Interfaces;
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
    [RoutePrefix("api/SM_DashBoard")]
    public class SM_DashBoardController : ApiController
    {
        ISM_DashboardRepository _dshboard;
        private static readonly ILogger _loggger = Logger.Register(typeof(SM_DashBoardController));

        public SM_DashBoardController(ISM_DashboardRepository dshboard)
        {
            _dshboard = dshboard;
        }

        #region Location Wise
        #region Received Enquiries
        [Route("getReceivedEnquiries")]
        [Route("getReceivedEnquiries/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedEnquiries(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedEnquiries(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedEnquiries of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSourceEnquiries")]
        [Route("getSourceEnquiries/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSourceEnquiries(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSourceEnquiries(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSourceEnquiries of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getEngineerEnquiries")]
        [Route("getEngineerEnquiries/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetEngineerEnquiries(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetEngineerEnquiries(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEngineerEnquiries of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSectorEnquiries")]
        [Route("getSectorEnquiries/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSectorEnquiries(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSectorEnquiries(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSectorEnquiries of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Received Enquiries

        #region Submitted Offers
        [Route("getSubmittedOffers")]
        [Route("getSubmittedOffers/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedOffers(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedOffers(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedSourceOffers")]
        [Route("getSubmittedSourceOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedSourceOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedSourceOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedSourceOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedEngineerOffers")]
        [Route("getSubmittedEngineerOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedEngineerOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedEngineerOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedEngineerOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedSectorOffers")]
        [Route("getSubmittedSectorOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedSectorOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedSectorOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedSectorOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Submitted Offers

        #region Received Offers
        [Route("getReceivedOffers")]
        [Route("getReceivedOffers/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedOffers(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedOffers(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedSourceOffers")]
        [Route("getReceivedSourceOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedSourceOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedSourceOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedSourceOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedEngineerOffers")]
        [Route("getReceivedEngineerOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedEngineerOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedEngineerOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedEngineerOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedSectorOffers")]
        [Route("getReceivedSectorOffers/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedSectorOffers(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedSectorOffers(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedSectorOffers of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Received Offers
        #endregion Location  Wise



        #region Product Wise
        #region Received Enquiries
        [Route("getReceivedEnquiriesPW")]
        [Route("getReceivedEnquiriesPW/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedEnquiriesPW(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedEnquiriesPW(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedEnquiriesPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSourceEnquiriesPW")]
        [Route("getSourceEnquiriesPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSourceEnquiriesPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSourceEnquiriesPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSourceEnquiriesPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getEngineerEnquiriesPW")]
        [Route("getEngineerEnquiriesPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetEngineerEnquiriesPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetEngineerEnquiriesPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEngineerEnquiriesPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSectorEnquiriesPW")]
        [Route("getSectorEnquiriesPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSectorEnquiriesPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSectorEnquiriesPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSectorEnquiriesPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Received Enquiries

        #region Submitted Offers
        [Route("getSubmittedOffersPW")]
        [Route("getSubmittedOffersPW/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedOffersPW(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedOffersPW(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedSourceOffersPW")]
        [Route("getSubmittedSourceOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedSourceOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedSourceOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedSourceOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedEngineerOffersPW")]
        [Route("getSubmittedEngineerOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedEngineerOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedEngineerOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedEngineerOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getSubmittedSectorOffersPW")]
        [Route("getSubmittedSectorOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetSubmittedSectorOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetSubmittedSectorOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSubmittedSectorOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Submitted Offers

        #region Received Offers
        [Route("getReceivedOffersPW")]
        [Route("getReceivedOffersPW/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedOffersPW(string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedOffersPW(ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedSourceOffersPW")]
        [Route("getReceivedSourceOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedSourceOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedSourceOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedSourceOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedEngineerOffersPW")]
        [Route("getReceivedEngineerOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedEngineerOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedEngineerOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedEngineerOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getReceivedSectorOffersPW")]
        [Route("getReceivedSectorOffersPW/{FromDate}/{ToDate}")]
        [HttpGet]
        public IHttpActionResult GetReceivedSectorOffersPW(string FromDate = null, string ToDate = null)
        {
            try
            {
                var data = _dshboard.GetReceivedSectorOffersPW(FromDate, ToDate);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetReceivedSectorOffersPW of SM_DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion Received Offers
        #endregion Product  Wise

    }
}