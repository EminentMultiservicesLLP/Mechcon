using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/driverschedule")]
    public class DriverScheduleController : ApiController
    {
        IDriverScheduleRepository _driver;
        IFuelDetailsRepository _fuelDetail;
        private static readonly ILogger _loggger = Logger.Register(typeof(DriverScheduleController));

        public DriverScheduleController(IDriverScheduleRepository driver,IFuelDetailsRepository fuelDetail)
        {
            _driver = driver;
            _fuelDetail= fuelDetail;
        }

        [Route("getdriverschedule")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetDriverSchedule()
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _driver.GetAllDriverSchedule();
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDriverSchedule method of DriverScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }
        [Route("getdriverschedulerpt/{VehicleId}/{branchId}/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllScheduleRpt(int VehicleId, int branchId, DateTime fromdate, DateTime todate)
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _driver.GetAllScheduleRpt(VehicleId, branchId, fromdate, todate);
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllScheduleRpt method of DriverScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }

        [Route("createdriverschedule")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateDriverSchedule(DriverScheduleEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _driver.CreateDriverSchedule(tax);
                //tax.=_fuelDetail.CreateFuelDetails(tax,)
                tax.ScheduleId = newID;
                tax.fuelDetail.ScheduleId = newID;
                tax.fuelDetail.FuelDetailId = _fuelDetail.CreateFuelDetails(tax.fuelDetail);

                isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateDriverSchedule method of DriverScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<DriverScheduleEntity>(Request.RequestUri + tax.ScheduleId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("getschedule")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSchedule()
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _driver.GetAllSchedule();
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDriverSchedule method of DriverScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }

        [Route("UpdateFuelDetails")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateFuelDetails(DriverScheduleEntity Driver)
        {
            bool isSucecss = false ;
            TryCatch.Run(() =>
            {
                isSucecss = _driver.UpdateDriverSchedule(Driver);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateFuelDetails method of DriverScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Created<DriverScheduleEntity>(Request.RequestUri + Driver.ScheduleId.ToString(), Driver);
            else
                return BadRequest();


        }

        [Route("getFuelconsumptionrpt/{branchId}/{fromdate}/{todate}/{vehicletypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetFuelconsumptionrpt(int branchId, DateTime fromdate, DateTime todate, int vehicletypeId)
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _driver.GetFuelconsumptionvehiclewiserpt(branchId, fromdate, todate, vehicletypeId);
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetFuelconsumptionrpt method of DriverScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }

    }
}
