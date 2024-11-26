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
    [RoutePrefix("api/ticketActivity")]
    public class TicketActivityController : ApiController
    {
        ITicketActivityRepository _ticketActivity;
        private static readonly ILogger _loggger = Logger.Register(typeof(TicketActivityController));

        public TicketActivityController(ITicketActivityRepository ticketActivity)
        {
            _ticketActivity = ticketActivity;
        }

        [Route("getTicketForActivity/{UserID}")]
        [Route("getTicketForActivity/{UserID}/{TicketType}")]
        [HttpGet]
        public IHttpActionResult GetTicketForActivity(int UserID, int? TicketType = null)
        {
            try
            {
                var data = _ticketActivity.GetTicketForActivity(UserID, TicketType);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetTicketForActivity of TicketRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getActivityID/{TicketID}/{InsertedBy}")]
        [HttpGet]
        public IHttpActionResult GetActivityID(int TicketID, int InsertedBy)
        {
            try
            {
                var details = _ticketActivity.GetActivityID(TicketID, InsertedBy);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetActivityID of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("saveActivity")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveActivity(ActivityListEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Activity = _ticketActivity.SaveActivity(model);
                model = Activity;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of ActivityRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<ActivityListEntities>(Request.RequestUri, model);
            else
                return BadRequest();
        }

        [Route("getActivity")]
        [Route("getActivity/{ticketID}")]
        [HttpGet]
        public IHttpActionResult GetActivity(int? ticketID = null)
        {
            try
            {
                var data = _ticketActivity.GetActivity(ticketID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetActivity of TicketRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}