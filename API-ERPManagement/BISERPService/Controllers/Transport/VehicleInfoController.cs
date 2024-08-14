using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPBusinessLayer.Entities.Transport;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/vehicle")]
    public class VehicleInfoController : ApiController
    {
        IVehicleInfoRepository _vehicle;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleInfoController));

        public VehicleInfoController(IVehicleInfoRepository vehicle)
        {
            _vehicle = vehicle;
        }

        [Route("getvehicles/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicles(int UserId)
        {
            List<VehicleInfoEntity> vehicle = new List<VehicleInfoEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicle.GetAllVehicle(UserId);
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicles method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return Ok(vehicle);
        }

        [Route("getvehiclesNo/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehiclesNO(int branchId)
        {
            IEnumerable<VehicleInfoEntity> vehicle = null;
            TryCatch.Run(() =>
            {
                vehicle = _vehicle.GetAllVehicleNo(branchId);
                //if (list != null && list.Count() > 0)
                //    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehiclesNO method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (vehicle == null)
              return BadRequest();
            return Ok(vehicle.ToList());
                
        }
        [Route("getvehicleschedule/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleSchedule(int branchId)
        {
            IEnumerable<VehicleInfoEntity> vehicle = null;
            TryCatch.Run(() =>
            {
                vehicle = _vehicle.GetAllVehicleSchedule(branchId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleSchedule method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); 

            if (vehicle ==null)
                return BadRequest();
            return Ok(vehicle.ToList());
        }
        [Route("vehicleentry")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveVehicleInfo(VehicleInfoEntity vehicle)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _vehicle.CheckDuplicateVehicle(vehicle);
                if (isDuplicate == false)
                {
                    var newID = _vehicle.SaveVehicleInfo(vehicle);
                    vehicle.VehicleId = newID;
                    isSucecss = true;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveVehicleInfo method of VehicleInfoController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Vehicle already exist");
            else if (isSucecss)
                return Created<VehicleInfoEntity>(Request.RequestUri + vehicle.VehicleId.ToString(), vehicle);
            else
                return BadRequest();
        }

        [Route("vehicleupdate")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateVehicleInfo(VehicleInfoEntity vehicle)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _vehicle.UpdateVehicleInfo(vehicle);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateVehicleInfo method of VehicleInfoController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getvehicleinfo/{BranchId}/{VehicleId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult VehicleInfoReport(int BranchId, int VehicleId)
        {
            List<VehicleInfoEntity> vehicle = new List<VehicleInfoEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicle.VehicleInfoReport(BranchId, VehicleId);
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in VehicleInfoReport method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return BadRequest();
        }

        [Route("getvehicleList/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleListRpt(int BranchId)
        {
            List<VehicleInfoEntity> vehicle = new List<VehicleInfoEntity>();
            TryCatch.Run(() =>
            {
                var list = _vehicle.GetAllVehicleListRpt(BranchId);
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleListRpt method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return BadRequest();
        }
    }
}
