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
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Entities.Asset;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/Floor")]
    public class FloorController : ApiController
    {
        IFloorRespository _Floor;
         private static readonly ILogger _loggger = Logger.Register(typeof(FloorController));

         public FloorController(IFloorRespository Floor)
        {
            _Floor = Floor;
        }

        public string Get(int id)
        {
            return "";
        }
        [Route("GetAllFloor")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllFloor()
        {
            List<FloorEntity> division = new List<FloorEntity>();
            TryCatch.Run(() =>
            {
                var list = _Floor.GetAllFloor();
                       if (list != null && list.Count() > 0)
                division = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllFloor method of FloorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (division.IsNotNull())
                return Ok(division);
            else
                return BadRequest();
        }

        [Route("CreateFloor")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateFloor(FloorEntity Floor)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _Floor.CreateFloor(Floor);
                    Floor.FloorId = newID;
                    isSucecss = true;
                   // MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateFloor method of FloorController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<FloorEntity>(Request.RequestUri + Floor.FloorId.ToString(), Floor);
            else
                return BadRequest();
        }
        [Route("UpdateFloor")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateFloor(FloorEntity Floor)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _Floor.UpdateFloor(Floor);
                  //  MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateFloor method of FloorController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactivefloor")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveFloor()
        {
            List<FloorEntity> units = new List<FloorEntity>();
            TryCatch.Run(() =>
            {
                var list = _Floor.GetActiveFloor();
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveFloor method of FloorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return Ok(units);
        }
        [Route("getbranchfloor")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBranchFloor()
        {
            List<FloorEntity> units = new List<FloorEntity>();
            TryCatch.Run(() =>
            {
                var list = _Floor.GetBranchFloor();
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetBranchFloor method of FloorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("GetActiveLocationWiseFloor/{LocationId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveLocationWiseFloor(int LocationId)
        {
            List<FloorEntity> units = new List<FloorEntity>();
            TryCatch.Run(() =>
            {
                var list = _Floor.GetActiveLocationWiseFloor(LocationId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveLocationWiseFloor method of FloorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("getbranchLocationfloor/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBranchLocationFloor(int BranchId)
        {
            List<FloorEntity> units = new List<FloorEntity>();
            TryCatch.Run(() =>
            {
                var list = _Floor.GetBranchLocationFloor(BranchId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveLocationWiseFloor method of FloorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
