using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/subDivision")]
    public class SubDivisionController : ApiController
    {
        ISubDivisionRepository _subdivision;
        private static readonly ILogger _loggger = Logger.Register(typeof(SubDivisionController));

        public SubDivisionController(ISubDivisionRepository Subdivision)
        {
            _subdivision = Subdivision;
        }

        public string Get(int id)
        {
            return "";
        }
        [Route("GetAllSubDivision")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllSubDivision()
        {
            List<SubDivisionMasterEntity> subdivision = new List<SubDivisionMasterEntity>();
            TryCatch.Run(() =>
            {
                var list = _subdivision.GetAllSubDivision();
                if (list != null && list.Count() > 0)
                    subdivision = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllSubDivision method of SubDivisionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (subdivision.IsNotNull())
                return Ok(subdivision);
            else
                return BadRequest();
        }
        //private List<SubDivisionMasterEntity> AllSubDivision()
        //{
        //    List<SubDivisionMasterEntity> subdivision = new List<SubDivisionMasterEntity>();
        //    TryCatch.Run(() =>
        //    {
        //        if (!MemoryCaching.CacheKeyExist(CachingKeys.AllSubDivision.ToString()))
        //        {
        //            var list = _subdivision.GetAllSubDivision();
        //            if (list != null && list.Count() > 0)
        //                subdivision = list.ToList();
        //            MemoryCaching.AddCacheValue(CachingKeys.AllSubDivision.ToString(), subdivision);
        //        }
        //        else
        //        {
        //            subdivision = (List<SubDivisionMasterEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllSubDivision.ToString()));
        //        }
        //        subdivision = subdivision.Where(w => w.Deactive == false).ToList();

        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in AllSubDivision method of SubDivisionController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    return subdivision;
        //}
        [Route("CreateSubdivision")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSubdivision(SubDivisionMasterEntity subdivision)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _subdivision.CreateSubDivision(subdivision);
                    subdivision.SubDivisionId = newID;
                    isSucecss = true;
               //     MemoryCaching.RemoveCacheValue(CachingKeys.AllUnits.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateSubdivision method of SubDivisionController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<SubDivisionMasterEntity>(Request.RequestUri + subdivision.SubDivisionId.ToString(), subdivision);
            else
                return BadRequest();
        }

        [Route("Updatedivision")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateSubdivision(SubDivisionMasterEntity subdivision)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _subdivision.UpdateSubDivision(subdivision);
                  //  MemoryCaching.RemoveCacheValue(CachingKeys.AllSubDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateSubDivision method of SubDivisionController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }

        [Route("getactivesubdivision/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActivedivision(int UserId)
        {
            List<SubDivisionMasterEntity> units = new List<SubDivisionMasterEntity>();
            TryCatch.Run(() =>
            {
                var list = _subdivision.GetActiveSubDivision(UserId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActivedivision method of SubDivisionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

    }
}
