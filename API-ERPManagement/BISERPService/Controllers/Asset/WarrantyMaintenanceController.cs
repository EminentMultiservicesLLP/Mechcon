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
    [RoutePrefix("api/warranty")]
    public class WarrantyMaintenanceController : ApiController
    {
        IWarrantyMaintenanceRepository _warranty;
        IWarrantySparePartsRepository _mconsumption;
        IWarrantyMaintenanceCommonRepository _common;

        private static readonly ILogger _loggger = Logger.Register(typeof(WarrantyMaintenanceController));
        public WarrantyMaintenanceController(IWarrantyMaintenanceRepository warranty, IWarrantySparePartsRepository mconsumption,
                                            IWarrantyMaintenanceCommonRepository common)
        {
            _warranty = warranty;
            _mconsumption = mconsumption;
            _common = common;
        }

        [Route("createwarrantymaintenance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveWarrantyMaintenance(WarrantyMaintenanceEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                entity = _common.SaveWarrantyMaintenance(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveWarrantyMaintenance method of WarrantyMaintenanceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<WarrantyMaintenanceEntity>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }
    }
}
