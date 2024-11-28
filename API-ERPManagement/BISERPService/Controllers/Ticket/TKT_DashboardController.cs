using BISERPBusinessLayer.Entities.Ticket;
using BISERPBusinessLayer.Repositories.Ticket.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Ticket
{
    [RoutePrefix("api/TKT_Dashboard")]
    public class TKT_DashboardController : ApiController
    {
        IDashboardRepository _dshboard;
        private static readonly ILogger _loggger = Logger.Register(typeof(TKT_DashboardController));

        public TKT_DashboardController(IDashboardRepository dshboard)
        {
            _dshboard = dshboard;
        }
        
        [Route("getTicketStatusSummary")]
        [Route("getTicketStatusSummary/{financialYear}")]
        [HttpGet]
        public IHttpActionResult GetTicketStatusSummary(string financialYear = null)
        {
            try
            {
                var data = _dshboard.GetTicketStatusSummary(financialYear);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetTicketStatusSummary of DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getTicketStatusRpt")]
        [Route("getTicketStatusRpt/{financialYear}")]
        [HttpGet]
        public IHttpActionResult GetTicketStatusRpt(string financialYear = null)
        {
            try
            {
                var data = _dshboard.GetTicketStatusRpt(financialYear);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetTicketStatusRpt of DashboardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
    }
}