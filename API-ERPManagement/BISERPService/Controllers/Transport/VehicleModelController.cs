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
    [RoutePrefix("api/vehiclemodel")]
    public class VehicleModelController : ApiController
    {
        IVehicleModelRepository _vehiclemodel;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleModelController));

        public VehicleModelController(IVehicleModelRepository vehiclemodel)
        {
            _vehiclemodel = vehiclemodel;
        }

        private List<VehicleModelEntity> AllVehicleModel()
        {
            List<VehicleModelEntity> model = new List<VehicleModelEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllVehicleModel.ToString()))
                {
                    var list = _vehiclemodel.GetAllVehicleModel();
                    if (list != null && list.Count() > 0)
                        model = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllVehicleModel.ToString(), model);
                }
                else
                {
                    model = (List<VehicleModelEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllVehicleModel.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllVehicleModel method of VehicleModelController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return model;
        }

        [Route("getvehiclemodel")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleModel()
        {
            List<VehicleModelEntity> model = new List<VehicleModelEntity>();
            TryCatch.Run(() =>
            {
                var list = AllVehicleModel();
                    model = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleModel method of VehicleModelController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (model.IsNotNull())
                return Ok(model);
            else
                return BadRequest();
        }

        [Route("Createvehiclemodel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVehicleModel(VehicleModelEntity VehicleModel)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _vehiclemodel.CreateVehicleModel(VehicleModel);
                    VehicleModel.ModelId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVehicleModel.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVehicleModel method of VehicleModelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VehicleModelEntity>(Request.RequestUri + VehicleModel.ModelId.ToString(), VehicleModel);
            else
                return BadRequest();
        }
        [Route("Updatevehiclemodel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateVehicleModel(VehicleModelEntity VehicleModel)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _vehiclemodel.UpdateVehicleModel(VehicleModel);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVehicleModel.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateVehicleModel method of VehicleModelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getactivevehiclemodel")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveVehicleModel()
        {
            List<VehicleModelEntity> model = new List<VehicleModelEntity>();
            TryCatch.Run(() =>
            {
                var list = AllVehicleModel();
                model = list.Where(m => m.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveVehicleModel method of VehicleModelController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (model.Any())
                return Ok(model);
            else
                return BadRequest();
        }
    }
}
