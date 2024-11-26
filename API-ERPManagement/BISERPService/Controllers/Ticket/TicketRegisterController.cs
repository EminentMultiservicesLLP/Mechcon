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
    [RoutePrefix("api/ticketRegister")]
    public class TicketRegisterController : ApiController
    {
        ITicketRegisterRepository _ticketRegister;
        private static readonly ILogger _loggger = Logger.Register(typeof(TicketRegisterController));

        public TicketRegisterController(ITicketRegisterRepository ticketRegister)
        {
            _ticketRegister = ticketRegister;
        }

        [Route("getProject")]
        [Route("getProject/{ClientID}")]
        [HttpGet]
        public IHttpActionResult GetProject(int? ClientID = null)
        {
            try
            {
                var data = _ticketRegister.GetProject(ClientID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetProject of TicketRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getPriority")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPriority()
        {
            try
            {
                var status = _ticketRegister.GetPriority().ToList();

                if (status == null || !status.Any())
                {
                    return NotFound();
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetPriority method of TicketRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("getStatus")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStatus()
        {
            try
            {
                var status = _ticketRegister.GetStatus().ToList();

                if (status == null || !status.Any())
                {
                    return NotFound();
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetStatus method of TicketRegisterController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("saveTicket")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveTicket(TicketRegisterEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Ticket = _ticketRegister.SaveTicket(model);
                model = Ticket;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Ticket method of TicketRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<TicketRegisterEntities>(Request.RequestUri + model.TicketID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getTicket")]
        [Route("getTicket/{statusID}")]
        [HttpGet]
        public IHttpActionResult GetTicket(int? statusID = null)
        {
            try
            {
                var data = _ticketRegister.GetTicket(statusID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified ticket ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetTicket of TicketRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}