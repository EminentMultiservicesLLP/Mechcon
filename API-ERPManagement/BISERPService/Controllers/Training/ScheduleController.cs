using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        readonly IScheduleRepository _schedule;
        private static readonly ILogger Loggger = Logger.Register(typeof(ScheduleController));

        public ScheduleController(IScheduleRepository schedule)
        {
            _schedule = schedule;
        }

        [Route("GetAllSchedules")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllSchedules()
        {
            List<ScheduleEntity> schedule = new List<ScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _schedule.GetAllSchedules();
                if (list != null && list.Any())
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllSchedules method of ScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (schedule.IsNotNull())
                return Ok(schedule);
            else
                return BadRequest();
        }

        [Route("CreateSchedule")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSchedule(ScheduleEntity schedules)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newId = _schedule.CreateSchedule(schedules);
                schedules.ScheduleId = newId;
                    isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateSchedule method of ScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created(Request.RequestUri + schedules.ScheduleId.ToString(), schedules);
            else
                return BadRequest();
        }

        [Route("UpdateSchedule")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateSchedule(ScheduleEntity schedules)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _schedule.UpdateSchedule(schedules);
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateSchedule method of ScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest("Error in Training Schedule Updation");
        }
    }
}
