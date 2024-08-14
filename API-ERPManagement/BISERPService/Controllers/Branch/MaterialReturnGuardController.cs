using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/mreturnguard")]
    public class MaterialReturnGuardController : ApiController
    {
        IMaterialReturnGuardRepository _materialReturnguard;
        IMaterialReturnGuardDtRepository _materialdetailReturnguardDt;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialReturnGuardController));

        public MaterialReturnGuardController(IMaterialReturnGuardRepository materialReturnguard, IMaterialReturnGuardDtRepository materialdetailReturnguardDt)
        {
            _materialReturnguard = materialReturnguard;
            _materialdetailReturnguardDt = materialdetailReturnguardDt;
        }

        [Route("createReturnguard")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialReturnGuard(MaterialReturnGuardEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _materialReturnguard.CreateEntity(entity);
                entity.ReturnId = newID.ReturnId;
                entity.ReturnNo = newID.ReturnNo;
                foreach (var dt in entity.MaterialReturnGuardDt)
                {
                    dt.ReturnDtlId = _materialdetailReturnguardDt.CreateEntity(entity, dt);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaterialReturnGuard method of MaterialReturnGuardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<MaterialReturnGuardEntity>(Request.RequestUri + entity.ReturnId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }
    }
}
