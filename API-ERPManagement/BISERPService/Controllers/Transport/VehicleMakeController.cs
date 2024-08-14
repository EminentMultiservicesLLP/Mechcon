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
using BISERPService.Caching;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/vehicleMake")]
    public class VehicleMakeController : ApiController
    {
        IVehicleMakeRepository _vehicleMake;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleMakeController));

        public VehicleMakeController(IVehicleMakeRepository vehicleMake)
        {
            _vehicleMake = vehicleMake;
        }

        private List<VehicleMakeEntity> AllVehicleMake()
        {
            List<VehicleMakeEntity> make = new List<VehicleMakeEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllVehicleMake.ToString()))
                {
                    var list = _vehicleMake.GetAllVehicleMake();
                    if (list != null && list.Count() > 0)
                        make = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllVehicleMake.ToString(), make);
                }
                else
                {
                    make = (List<VehicleMakeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllVehicleMake.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllVehicleMake method of VehicleMakeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return make;
        }

        [Route("getvehicleMake")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleMake()
        {
            List<VehicleMakeEntity> type = new List<VehicleMakeEntity>();
            TryCatch.Run(() =>
            {
                type = AllVehicleMake();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleMake method of VehicleMakeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (type.IsNotNull())
                return Ok(type);
            else
                return BadRequest();
        }

        [Route("CreateVehicleMake")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVehicleMake(VehicleMakeEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _vehicleMake.CreateVehicleMake(Vehicle);
                    Vehicle.MakeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVehicleMake.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVehicleMake method of VehicleMakeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VehicleMakeEntity>(Request.RequestUri + Vehicle.MakeId.ToString(), Vehicle);
            else
                return BadRequest();
        }

        [Route("UpdatevehicleMake")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateVehicleMake(VehicleMakeEntity Vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _vehicleMake.UpdateVehicleMake(Vehicle);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVehicleMake.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatevehicleMake method of VehicleMakeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetActiveVehicleMake")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveVehicleMake()
        {
            List<VehicleMakeEntity> units = new List<VehicleMakeEntity>();
            TryCatch.Run(() =>
            {
                var list = AllVehicleMake();
                units = list.Where(m => m.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveVehicleMake method of VehicleMakeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
