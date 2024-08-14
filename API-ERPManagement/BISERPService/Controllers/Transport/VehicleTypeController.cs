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
    [RoutePrefix("api/vehicletype")]
    public class VehicleTypeController : ApiController
    {
        IVehicleTypeRepository _vehicletype;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleTypeController));

        public VehicleTypeController(IVehicleTypeRepository vehicletype)
        {
            _vehicletype = vehicletype;
        }

        [Route("getvehicletype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleType()
        {
            List<VehicleTypeEntity> type = new List<VehicleTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicletype.GetAllVehicleType();
                if (list != null && list.Count() > 0)
                    type = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleType method of VehicleTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (type.IsNotNull())
                return Ok(type);
            else
                return BadRequest();
        }

        [Route("CreateVehicleType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVehicleType(VehicleTypeEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _vehicletype.CreateVehicleType(Vehicle);
                    Vehicle.TypeId = newID;
                    isSucecss = true;
                    // MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVehicleType method of VehicleTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VehicleTypeEntity>(Request.RequestUri + Vehicle.TypeId.ToString(), Vehicle);
            else
                return BadRequest();
        }

        [Route("UpdatevehicleType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateVehicleType(VehicleTypeEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _vehicletype.UpdateVehicleType(Vehicle);
                    //  MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateVehicleType method of VehicleTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetActiveVehicleType/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveVehicleType(int UserId)
        {
            List<VehicleTypeEntity> units = new List<VehicleTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicletype.GetActiveVehicleType(UserId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveunits method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

      
    }
}
