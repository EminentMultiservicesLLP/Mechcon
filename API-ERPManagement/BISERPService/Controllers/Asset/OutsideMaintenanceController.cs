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
    [RoutePrefix("api/outsidemaint")]
    public class OutsideMaintenanceController : ApiController
    {
        IOutsideMaintenanceRepository _outsidemaint;
        IOutsideMaintenanceCommonRepository _oCommon;
        ISparePartsOuthouseUtilRepository _oSpareParts;
        private static readonly ILogger _loggger = Logger.Register(typeof(OutsideMaintenanceController));

        public OutsideMaintenanceController(IOutsideMaintenanceRepository outsidemaint, ISparePartsOuthouseUtilRepository oSpareParts,  IOutsideMaintenanceCommonRepository oCommon)
        {
            _outsidemaint = outsidemaint;
            _oCommon =oCommon;
            _oSpareParts = oSpareParts;

         }
        
        [Route("CreateOutsideMaintenance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateOutsideMaintenance(OutsideMaintenanceEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _oCommon.SaveOutsideMaintenance(Entity);
                    Entity.Id = newEntity.Id;
                    Entity.Code = newEntity.Code;
                    isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateOutsideMaintenance method of OutsideMaintenanceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<OutsideMaintenanceEntity>(Request.RequestUri + Entity.Id.ToString(), Entity);
            else
                return BadRequest();
        }
        [Route("GetOutsideMaintenanceNo/{MaintenanceId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetOutsideMaintenanceNodt(int MaintenanceId)
        {
            OutsideMaintenanceEntity units = new OutsideMaintenanceEntity();
            TryCatch.Run(() =>
            {
                units = _outsidemaint.GetOutsideMaintenanceNodt(MaintenanceId);
                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetOutsideMaintenanceNodt method of OutsideMaintenanceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (!units.IsNull())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("GetOutsideMaintenanceNoit/{OUTHOUSEID}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetOutsideMaintenanceNodtitem(int OUTHOUSEID)
        {
            List<SparePartsOuthouseUtilEntity> units = new List<SparePartsOuthouseUtilEntity>();
            TryCatch.Run(() =>
            {
               
                var list = _outsidemaint.GetOutsideMaintenanceNodtItem(OUTHOUSEID);
                if (list != null && list.Count() > 0)
                    units = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetOutsideMaintenanceNoit method of OutsideMaintenanceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (!units.IsNull())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
