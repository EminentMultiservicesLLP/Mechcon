using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Entities.Asset;
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/technician")]
    public class TechnicianController : ApiController
    {
        readonly ITechnicianRepository _technician;
        private static readonly ILogger Loggger = Logger.Register(typeof(TechnicianController));

        public TechnicianController(ITechnicianRepository technician)
        {
            _technician = technician;
        }

        private List<TechnicianEntity> AllTechnician()
        {
            List<TechnicianEntity> tech = new List<TechnicianEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllTechnician.ToString()))
                {
                    var list = _technician.GetAllTechnician();
                    if (list != null && list.Any())
                        tech = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllTechnician.ToString(), tech);
                }
                else
                {
                    tech = (List<TechnicianEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllTechnician.ToString()));
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllTechnician method of TechnicianController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return tech;
        }

        [Route("GetAlltechnician")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllTechnician()
        {
            var tech = new List<TechnicianEntity>();
            TryCatch.Run(() =>
            {
                tech = AllTechnician();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllTechnician method of TechnicianController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tech.IsNotNull())
                return Ok(tech);
            else
                return BadRequest();
        }

        [Route("Createtechnician")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateTechnician(TechnicianEntity tech)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _technician.CheckDuplicateItem(tech.TechnicianCode);
                if (isDuplicate == false)
                {
                    var newId = _technician.CreateTechnician(tech);
                    tech.TechnicianId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTechnician.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateTechnician method of TechnicianController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<TechnicianEntity>(Request.RequestUri + tech.TechnicianId.ToString(), tech);
            else
                return BadRequest("Error");
        }
        [Route("Updatetechnician")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateTechnician(TechnicianEntity tech)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _technician.CheckDuplicateupdate(tech.TechnicianCode, tech.TechnicianId);
                if (isDuplicate == false)
                {
                    isSucecss = _technician.UpdateTechnician(tech);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTechnician.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateTechnician method of TechnicianController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactivetechnician")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveTechnician()
        {
            var units = new List<TechnicianEntity>();
            TryCatch.Run(() =>
            {
                var list = _technician.GetActiveTechnician();
                if (list != null && list.Any())
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveTechnician method of TechnicianController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
