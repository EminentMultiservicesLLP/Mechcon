using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.SM
{
    [RoutePrefix("api/resourceTarget")]
    public class ResourceTargetController : ApiController
    {
        IResourceTargetRepository _resourceTarget;
        private static readonly ILogger _loggger = Logger.Register(typeof(ResourceTargetController));

        public ResourceTargetController(IResourceTargetRepository resourceTarget)
        {
            _resourceTarget = resourceTarget;
        }

        [Route("getFinancialYear")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetFinancialYear()
        {
            try
            {
                var data = _resourceTarget.GetFinancialYear().ToList();

                if (data == null || !data.Any())
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetFinancialYear method of ResourceTargetController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }

        [Route("saveResourceTargetDetail")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveResourceTargetDetail(ResourceTargetEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var ResourceTarget = _resourceTarget.SaveResourceTargetDetail(model);
                model = ResourceTarget;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save ResourceTarget method of ResourceTargetController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<ResourceTargetEntities>(Request.RequestUri + model.FinancialYearID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getResourceTargetDetail/{financialYearID}")]
        [HttpGet]
        public IHttpActionResult GetResourceTargetDetail(int financialYearID)
        {
            try
            {
                var data = _resourceTarget.GetResourceTargetDetail(financialYearID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetResourceTargetDetail of ResourceTargetController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
    }
}