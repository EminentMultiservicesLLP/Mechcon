using BISEPRService.Controllers;
using BISERPBusinessLayer.Repositories;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        IEmployeeRepository _Employee;
        private static readonly ILogger _loggger = Logger.Register(typeof(EmployeeController));

        public EmployeeController(IEmployeeRepository Employee)
        {
            _Employee = Employee;
        }


        [Route("getemployee/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllEmployee(int UserId)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();
            TryCatch.Run(() =>
            {
                var list = _Employee.GetAllEmployee(UserId);
                if (list != null && list.Count() > 0)
                    lstemployee = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllEmployee method of EmployeeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (lstemployee.Any())
                return Ok(lstemployee);
            else
                return BadRequest();
        }


        [Route("getemployeeguarantor/{UserId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllEmployeeForGuarantor(int UserId)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();
            TryCatch.Run(() =>
            {
                var list = _Employee.GetAllEmployeeForGuarantor(UserId);
                if (list != null && list.Count() > 0)
                    lstemployee = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getemployeeguarantor method of EmployeeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (lstemployee.Any())
                return Ok(lstemployee);
            else
                return BadRequest();
        }
    }
}
