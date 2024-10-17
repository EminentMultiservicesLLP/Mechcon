using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.SM
{
    [RoutePrefix("api/sm_Reports")]
    public class SM_ReportsController : ApiController
    {
        ISM_ReportsRepository _SM_Reports;
        private static readonly ILogger _loggger = Logger.Register(typeof(SM_ReportsController));

        public SM_ReportsController(ISM_ReportsRepository sm_Reports)
        {
            _SM_Reports = sm_Reports;
        }


        [Route("getEnquiryRegisterRpt")]
        [Route("getEnquiryRegisterRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetEnquiryRegisterRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetEnquiryRegisterRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnquiryRegisterRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getOrderBookRpt")]
        [Route("getOrderBookRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetOrderBookRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetOrderBookRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetOrderBookRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getResourceWiseMonthlyStatusRpt")]
        [Route("getResourceWiseMonthlyStatusRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetResourceWiseMonthlyStatusRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetResourceWiseMonthlyStatusRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetResourceWiseMonthlyStatusRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getSectorWiseSalesRpt")]
        [Route("getSectorWiseSalesRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetSectorWiseSalesRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetSectorWiseSalesRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetSectorWiseSalesRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getLocationWiseSalesRpt")]
        [Route("getLocationWiseSalesRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetLocationWiseSalesRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetLocationWiseSalesRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetLocationWiseSalesRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getProductWiseSalesRpt")]
        [Route("getProductWiseSalesRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetProductWiseSalesRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetProductWiseSalesRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetProductWiseSalesRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
    }
}