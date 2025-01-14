using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
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
    [RoutePrefix("api/sm_Reports")]
    public class SM_ReportsController : ApiController
    {
        ISM_ReportsRepository _SM_Reports;
        IMechconMasterRepository _iGetMechconData;
        private static readonly ILogger _loggger = Logger.Register(typeof(SM_ReportsController));
        public SM_ReportsController(ISM_ReportsRepository sm_Reports, IMechconMasterRepository iGetMechconData)
        {
            _SM_Reports = sm_Reports;
            _iGetMechconData = iGetMechconData;
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

        [Route("getWorkOrderRpt/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetWorkOrderRpt(int id)
        {
            WorkOrderRptEntities workOrder = new WorkOrderRptEntities();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                workOrder = _SM_Reports.GetWorkOrderReport(id);
                workOrder.PaymentTermList = _SM_Reports.GetOrderBookPaymentTerms(id);
                workOrder.DeliveryTermList = _SM_Reports.GetOrderBookDeliveryTerms(id);
                workOrder.OtherTermList = _SM_Reports.GetOrderBookOtherTerms(id);
                workOrder.BasisTermList = _SM_Reports.GetOrderBookBasisTerms(id);
                mechconMaster = _iGetMechconData.GeMechconData();

                workOrder.companyName = mechconMaster.Name;
                workOrder.companyAddress = mechconMaster.Address;
                workOrder.companyGST = mechconMaster.GSTNumber;
                workOrder.companyCIN = mechconMaster.CINNumber;
                workOrder.companyEmail = mechconMaster.emailID;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetWorkOrderRpt method of SM_ReportsController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (workOrder.IsNotNull())
                return Ok(workOrder);
            else
                return NotFound();
        }


        [Route("getFunctionalReportList")]      
        [HttpGet]
        public IHttpActionResult GetFunctionalReportList()
        {
            try
            {
                var data = _SM_Reports.GetFunctionalReportList();

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetFunctionalReportList of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getZoneWiseSaleRpt")]
        [Route("getZoneWiseSaleRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetZoneWiseSaleRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetZoneWiseSaleRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetZoneWiseSaleRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getPersonWiseSaleRpt")]
        [Route("getPersonWiseSaleRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetPersonWiseSaleRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetPersonWiseSaleRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetPersonWiseSaleRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getStatusWiseSaleRpt")]
        [Route("getStatusWiseSaleRpt/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetStatusWiseSaleRpt(int? financialYearID = null)
        {
            try
            {
                var data = _SM_Reports.GetStatusWiseSaleRpt(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetStatusWiseSaleRpt of SM_ReportsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
    }
}