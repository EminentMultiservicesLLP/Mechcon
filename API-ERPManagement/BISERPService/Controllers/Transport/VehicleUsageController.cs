using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/vehicleUsage")]
    public class VehicleUsageController : ApiController
    {
        IVehicleusageRepository _vehicleusage;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleUsageController));

        public VehicleUsageController(IVehicleusageRepository vehicleusage)
        {
            _vehicleusage = vehicleusage;
        }

        [Route("getvehicleusage")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleUsage()
        {
            List<VehicleUsageEntity> type = new List<VehicleUsageEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicleusage.GetAllVehicleUsage();
                if (list != null && list.Count() > 0)
                    type = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleUsage method of VehicleUsageController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (type.IsNotNull())
                return Ok(type);
            else
                return BadRequest();
        }

        [Route("CreateVehicleUsage")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVehicleUsage(VehicleUsageEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _vehicleusage.CreateVehicleUsage(Vehicle);
                    Vehicle.Id = newID;
                    isSucecss = true;
                    // MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVehicleUsage method of VehicleUsageController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VehicleUsageEntity>(Request.RequestUri + Vehicle.Id.ToString(), Vehicle);
            else
                return BadRequest();
        }

        [Route("UpdatevehicleUsage")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateVehicleUsage(VehicleUsageEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _vehicleusage.UpdateVehicleUsage(Vehicle);
                    //  MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateVehicleUsage method of VehicleUsageController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetActiveVehicleUsage/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveVehicleUsage(int UserId)
        {
            List<VehicleUsageEntity> units = new List<VehicleUsageEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicleusage.GetActiveVehicleUsage(UserId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveVehicleUsage method of VehicleUsageController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

      
    }
}
