using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/cityStateCountryMaster")]
    public class CityStateCountryMasterController : ApiController
    {
        ICityStateCountryMasterRepository _master;

        private static readonly ILogger _loggger = Logger.Register(typeof(CityStateCountryMasterController));

        public CityStateCountryMasterController(ICityStateCountryMasterRepository master)
        {
            _master = master;

        }
        [Route("saveCity")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveCity(CityMasterEntities model)
        {
            var result = 0;
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                result = _master.SaveCity(model);
                if (result > 0)
                {
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllCity.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveCity method of  CityStateCountryMasterController  :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Ok("City Saved Successfully");
            else
                return BadRequest();
        }

        [Route("saveState")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveState(StateMasterEntities model)
        {
            var result = 0;
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                result = _master.SaveState(model);
                if (result > 0)
                {
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllState.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveState method of  CityStateCountryMasterController  :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Ok("State Saved Successfully");
            else
                return BadRequest();
        }
    }
}