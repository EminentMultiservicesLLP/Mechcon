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
    [RoutePrefix("api/Division")]
    public class DivisionController : ApiController
    {
         IDivisionRepository _division;
        private static readonly ILogger _loggger = Logger.Register(typeof(DivisionController));

        public DivisionController(IDivisionRepository division)
        {
            _division = division;
        }

        public string Get(int id)
        {
            return "";
        }
        [Route("GetAllDivision")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllDivision()
        {
            List<DivisionMaster> division = new List<DivisionMaster>();
            TryCatch.Run(() =>
            {
                var list = _division.GetAllDivision();
                       if (list != null && list.Count() > 0)
                division = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllDivision method of DivisionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (division.IsNotNull())
                return Ok(division);
            else
                return BadRequest();
        }
        //private List<DivisionMaster> AllDivision()
        //{
        //    List<DivisionMaster> division = new List<DivisionMaster>();
        //    TryCatch.Run(() =>
        //    {
        //        if (!MemoryCaching.CacheKeyExist(CachingKeys.AllDivision.ToString()))
        //        {
        //            var list = _division.GetAllDivision();
        //            if (list != null && list.Count() > 0)
        //                division = list.ToList();
        //            MemoryCaching.AddCacheValue(CachingKeys.AllDivision.ToString(), division);
        //        }
        //        else
        //        {
        //            division = (List<DivisionMaster>)(MemoryCaching.GetCacheValue(CachingKeys.AllDivision.ToString()));
        //        }
        //        division = division.Where(w => w.Deactive == false).ToList();

        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetActiveunits method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    return division;
        //}
        [Route("Createdivision")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Createdivision(DivisionMaster division)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _division.CreateDivision(division);
                    division.DivisionId = newID;
                    isSucecss = true;
                   // MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Createdivision method of DivisionController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<DivisionMaster>(Request.RequestUri + division.DivisionId.ToString(), division);
            else
                return BadRequest();
        }
        [Route("Updatedivision")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Updatedivision(DivisionMaster division)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _division.UpdateDivision(division);
                  //  MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Updatedivision method of DivisionController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactivedivision/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActivedivision(int UserId)
        {
            List<DivisionMaster> units = new List<DivisionMaster>();
            TryCatch.Run(() =>
            {
                var list = _division.GetActiveDivision( UserId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActivedivision method of DivisionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

    }
}
