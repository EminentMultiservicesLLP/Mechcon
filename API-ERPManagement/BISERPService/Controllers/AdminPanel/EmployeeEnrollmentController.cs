using BISERPBusinessLayer.Entities.AdminPanel;
using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPCommon;
using System;
using BISERPCommon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.AdminPanel
{
    [RoutePrefix("api/EmployeeEnrollment")]
    public class EmployeeEnrollmentController : ApiController
    {
        IEmployeeEnrollmentRepository _EmployeeEnrollment;
        private static readonly ILogger _loggger = Logger.Register(typeof(EmployeeEnrollmentController));
        public EmployeeEnrollmentController(IEmployeeEnrollmentRepository EmployeeEnrollment)
        {
            _EmployeeEnrollment = EmployeeEnrollment;

        }

        [Route("getUserCode")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult getUserCode()
        {
            List<EmployeeEnrollmentEntity> items = new List<EmployeeEnrollmentEntity>();
            TryCatch.Run(() =>
            {
                var list = _EmployeeEnrollment.GetUserCode();
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getUserCode method of EmployeeEnrollmentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }
        [Route("GetUserDetails")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUserDetails()
        {
            List<EmployeeEnrollmentEntity> items = new List<EmployeeEnrollmentEntity>();
            TryCatch.Run(() =>
            {
                var list = _EmployeeEnrollment.GetUserDetails();
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUserDetails method of EmployeeEnrollmentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("SaveUser")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveUser(EmployeeEnrollmentEntity Items)
        {
            bool isSuccess = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _EmployeeEnrollment.CheckDuplicateItem(Items.UserCode, Items.UserID);
                if (isDuplicate == false)
                {
                    var list = _EmployeeEnrollment.SaveUser(Items);
                    isSuccess = true;
                }


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveUser method of EmployeeEnrollmentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("This User Code Is Already Assigned To Other Employee");
            else if (isSuccess)
                return Created<EmployeeEnrollmentEntity>(Request.RequestUri + Items.ToString(), Items);
            else
                return BadRequest();
        }

        [Route("DeleteUser")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult DeleteUser(EmployeeEnrollmentEntity Items)
        {
            bool isSuccess = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                if (isDuplicate == false)
                {
                    var list = _EmployeeEnrollment.DeleteUser(Items);
                    isSuccess = true;
                }


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteUser method of EmployeeEnrollmentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Unit Store Already Linked To Other Store");
            else if (isSuccess)
                return Created<EmployeeEnrollmentEntity>(Request.RequestUri + Items.ToString(), Items);
            else
                return BadRequest();
        }

        [Route("ChangePassword")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult ChangePassword(EmployeeEnrollmentEntity Items)
        {
            bool isSuccess = false;
            TryCatch.Run(() =>
            {
                var list = _EmployeeEnrollment.ChangePassword(Items);
                isSuccess = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in ChangePassword method of EmployeeEnrollmentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSuccess)
                return Created<EmployeeEnrollmentEntity>(Request.RequestUri + Items.ToString(), Items);
            else
                return BadRequest();
        }
    }

}
