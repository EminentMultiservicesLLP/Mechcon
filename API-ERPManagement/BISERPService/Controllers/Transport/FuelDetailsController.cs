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
    [RoutePrefix("api/fueldetail")]
    public class FuelDetailsController : ApiController
    {
        IFuelDetailsRepository _fuel;
        private static readonly ILogger _loggger = Logger.Register(typeof(FuelDetailsController));

        public FuelDetailsController(IFuelDetailsRepository fuel)
        {
            _fuel = fuel;
        }

        [Route("getfueldetail/{ScheduleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllFuelDetails(int ScheduleId)
        {
            List<FuelDetailsEntity> schedule = new List<FuelDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _fuel.GetAllFuelDetails(ScheduleId);
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllFuelDetails method of FuelDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }

        [Route("getfuelSchedule/{ScheduleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllFuelSchedule(int ScheduleId)
        {
            List<FuelDetailsEntity> schedule = new List<FuelDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _fuel.GetAllFuelSchedule(ScheduleId);
                if (list != null && list.Count() > 0)
                    schedule = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllFuelSchedule method of FuelDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (schedule.Any())
                return Ok(schedule);
            else
                return BadRequest();
        }

        [Route("createfueldetail")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateDriverSchedule(FuelDetailsEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _fuel.CreateFuelDetails(tax);
                tax.FuelDetailId = newID;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateDriverSchedule method of FuelDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<FuelDetailsEntity>(Request.RequestUri + tax.FuelDetailId.ToString(), tax);
            else
                return BadRequest();
        }
       
    }
}
