using BISERPBusinessLayer.Entities.Ticket;
using BISERPBusinessLayer.Entities.TimingPlan;
using BISERPBusinessLayer.Repositories.Ticket.Interfaces;
using BISERPBusinessLayer.Repositories.TimingPlan.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.TimingPlan
{
    [RoutePrefix("api/timingPlan")]
    public class TimingPlanController : ApiController
    {
        ITimingPlanRepository _timingPlan;
        private static readonly ILogger _loggger = Logger.Register(typeof(TimingPlanController));

        public TimingPlanController(ITimingPlanRepository timingPlan)
        {
            _timingPlan = timingPlan;
        }

        #region Schedule
        [Route("getTaskMaster")]
        [HttpGet]
        public IHttpActionResult GetTaskMaster()
        {
            try
            {
                var tasks = _timingPlan.GetTaskMaster();

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetTaskMaster of TimingPlanController.{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }

        [Route("getProjectTaskSchedule/{projectId}")]
        [HttpGet]
        public IHttpActionResult GetProjectTaskSchedule(int projectId)
        {
            try
            {
                var tasks = _timingPlan.GetProjectTaskSchedule(projectId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetProjectTaskSchedule of TimingPlanController. ProjectID: {projectId}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }

        [Route("saveProjectTaskSchedule")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveProjectTaskSchedule(TP_PTSchedule model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Enquiry = _timingPlan.SaveProjectTaskSchedule(model);
                model = Enquiry;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in saveProjectTaskSchedule method of TimingPlanController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<TP_PTSchedule>(Request.RequestUri + model.LoginId.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getProjectTaskRole/{projectId}")]
        [HttpGet]
        public IHttpActionResult GetProjectTaskRole(int projectId)
        {
            try
            {
                var tasks = _timingPlan.GetProjectTaskRole(projectId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetProjectTaskRole of TimingPlanController. ProjectID: {projectId}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }

        [Route("getProjectTaskScheduleDetails/{projectId}")]
        [HttpGet]
        public IHttpActionResult GetProjectTaskScheduleDetails(int projectId)
        {
            try
            {
                var tasks = _timingPlan.GetProjectTaskScheduleDetails(projectId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetProjectTaskScheduleDetails of TimingPlanController. ProjectID: {projectId}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }
        #endregion Schedule

        #region TaskProgress
        [Route("getTaskProgress/{IsCompleted}/{LoginId}")]
        [HttpGet]
        public IHttpActionResult GetTaskProgress(int IsCompleted, int LoginId)
        {
            try
            {
                var tasks = _timingPlan.GetTaskProgress(IsCompleted, LoginId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetTaskProgress of TimingPlanController. ProjectID: {LoginId}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }

        [Route("saveTaskProgress")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveTaskProgress(TP_ProjectTaskSchedule model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var TaskProgress = _timingPlan.SaveTaskProgress(model);
                model = TaskProgress;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save TaskProgress method of TimingPlanController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<TP_ProjectTaskSchedule>(Request.RequestUri + model.ScheduleID.ToString(), model);
            else
                return BadRequest();
        }
        #endregion TaskProgress

        #region TaskMonitor
        [Route("getTaskMonitor/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult GetTaskMonitor(int ProjectID)
        {
            try
            {
                var tasks = _timingPlan.GetTaskMonitor(ProjectID);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetTaskMonitor of TimingPlanController. ProjectID: {ProjectID}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }
        #endregion TaskMonitor

        #region TaskTracking
        [Route("getTaskTracking/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult GetTaskTracking(int ProjectID)
        {
            try
            {
                var tasks = _timingPlan.GetTaskTracking(ProjectID);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggger.LogError($"Error in GetTaskTracking of TimingPlanController. ProjectID: {ProjectID}{Environment.NewLine}{ex.Message}");
                return InternalServerError();
            }
        }
        #endregion TaskTracking
    }
}