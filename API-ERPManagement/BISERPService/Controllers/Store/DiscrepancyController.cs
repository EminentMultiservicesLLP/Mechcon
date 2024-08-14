using BISERPBusinessLayer.Repositories.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/dcr")]
    public class DiscrepancyController : ApiController
    {
        IDiscrepancyRepository _discrepancy;
        IDiscrepancyDtRepository _discrepancydt;
        IDiscrepancyCommonRepository _discrepancycommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(DiscrepancyController));

        public DiscrepancyController(IDiscrepancyRepository discrepancy, IDiscrepancyDtRepository discrepancydt,
                                        IDiscrepancyCommonRepository discrepancycommon)
        {
            _discrepancy = discrepancy;
            _discrepancydt = discrepancydt;
            _discrepancycommon = discrepancycommon;
        }

        [Route("createdcr")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateDiscrepancy(DiscrepancyEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _discrepancycommon.SaveDiscrepancy(entity);
                entity = newEntity;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateDiscrepancy method of DiscrepancyController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            });

            if (isSucecss)
                return Created<DiscrepancyEntity>(Request.RequestUri + entity.DiscrepancyId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("getalldiscrepancy/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllDiscrepancy(int UserId)
        {
            List<DiscrepancyEntity> discrepancy = new List<DiscrepancyEntity>();
            TryCatch.Run(() =>
            {
                var list = _discrepancy.GetAllDiscrepancy(UserId);
                if (list != null && list.Count() > 0)
                    discrepancy = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllDiscrepancy method of DiscrepancyController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (discrepancy.IsNull())
                return Ok();
            else if (discrepancy.Any())
                return Ok(discrepancy);
            else
                return Ok(discrepancy);
        }

        [Route("discrepancydt/{DiscrepancyId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllDiscrepancyDt(int DiscrepancyId)
        {
            List<DiscrepancyDtEntity> discrepancydt = new List<DiscrepancyDtEntity>();
            TryCatch.Run(() =>
            {
                var list = _discrepancydt.GetDetailByDiscrepancyId(DiscrepancyId);
                if (list != null && list.Count() > 0)
                    discrepancydt = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllDiscrepancyDt method of DiscrepancyController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (discrepancydt.IsNull())
                return Ok();
            else if (discrepancydt.Any())
                return Ok(discrepancydt);
            else
                return Ok(discrepancydt);
        }
    }
}
