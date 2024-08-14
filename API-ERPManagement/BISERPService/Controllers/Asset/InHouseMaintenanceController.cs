using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Entities.Asset;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/inside")]
    public class InHouseMaintenanceController : ApiController
    {
        IInHouseRepository _inhouse;
        IMaterialConsumptionRepository _mconsumption;
        ITechnicianEntryRepository _techEntry;
        IInHouseCommonRepository _common;

        private static readonly ILogger _loggger = Logger.Register(typeof(InHouseMaintenanceController));
        public InHouseMaintenanceController(IInHouseRepository inhouse, IMaterialConsumptionRepository mconsumption,
                        ITechnicianEntryRepository techEntry, IInHouseCommonRepository common)
        {
            _inhouse = inhouse;
            _mconsumption = mconsumption;
            _techEntry = techEntry;
            _common = common;
        }

        [Route("createinsidemaintenance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveInHouseMaintenance(InHouseEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                entity = _common.SaveInHouseMaintenance(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveInHouseMaintenance method of InHouseMaintenanceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<InHouseEntity>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("updateinsidemaintenance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateInHouseMaintenance(InHouseEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _common.UpdateInHouseMaintenance(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateInHouseMaintenance method of InHouseMaintenanceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("getimdetail/{InHouseId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetInHouseMaintenanceDetail(int InHouseId)
        {
            InHouseEntity inhouse = new InHouseEntity();
            TryCatch.Run(() =>
            {
                inhouse.TechEntry = _techEntry.GetTechnicianEntry(InHouseId);
                inhouse.consumption = _mconsumption.GetMaterialConsumption(InHouseId).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetInHouseMaintenanceDetail method of InHouseMaintenanceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (inhouse.IsNotNull())
                return Ok(inhouse);
            else
                return BadRequest();
        }
    }
}
