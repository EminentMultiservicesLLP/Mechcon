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
     [RoutePrefix("api/amcprovider")]
    public class AMCProviderController : ApiController
    {
         IAMCProviderRepository _amc;
         private static readonly ILogger _loggger = Logger.Register(typeof(AMCProviderController));

        public AMCProviderController(IAMCProviderRepository amc)
          {
              _amc = amc;
          }

        [Route("getallprovider")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllAMCProviders()
        {
            List<AMCProviderEntity> amcprovider = new List<AMCProviderEntity>();
            TryCatch.Run(() =>
            {
                var list = _amc.GetAllAMCProviders();
                if (list != null && list.Count() > 0)
                    amcprovider = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllAMCProviders method of AMCProviderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (amcprovider.Any())
                return Ok(amcprovider);
            else
                return Ok(amcprovider);
        }

        [Route("createamcprovider")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateAMCProvider(AMCProviderEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _amc.CreateAMCProvider(entity);
                entity.Id = newID;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAMCProvider method of AMCProviderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<AMCProviderEntity>(Request.RequestUri + entity.Id.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updateamcprovider")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateAMCProvider(AMCProviderEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _amc.UpdateAMCProvider(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateAMCProvider method of AMCProviderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
