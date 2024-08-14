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
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
        IRoomRepository _Room;
         private static readonly ILogger _loggger = Logger.Register(typeof(RoomController));

         public RoomController(IRoomRepository Room)
        {
            _Room = Room;
        }

        public string Get(int id)
        {
            return "";
        }
        [Route("GetAllRoom")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRoom()
        {
            List<RoomEntity> division = new List<RoomEntity>();
            TryCatch.Run(() =>
            {
                var list = _Room.GetAllRoom();
                       if (list != null && list.Count() > 0)
                division = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllRoom method of RoomController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (division.IsNotNull())
                return Ok(division);
            else
                return BadRequest();
        }

        [Route("CreateRoom")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateRoom(RoomEntity Floor)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateItem(unitMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _Room.CreateRoom(Floor);
                    Floor.FloorId = newID;
                    isSucecss = true;
                   // MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateRoom method of RoomController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<RoomEntity>(Request.RequestUri + Floor.FloorId.ToString(), Floor);
            else
                return BadRequest();
        }
        [Route("UpdateRoom")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateRoom(RoomEntity Floor)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _division.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _Room.UpdateRoom(Floor);
                  //  MemoryCaching.RemoveCacheValue(CachingKeys.AllDivision.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateRoom method of RoomController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveRoom/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveRoom(int UserId)
        {
            List<RoomEntity> units = new List<RoomEntity>();
            TryCatch.Run(() =>
            {
                var list = _Room.GetActiveRoom(UserId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveRoom method of RoomController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("getfloorRoom/{FloorId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetFloorRoom(int FloorId)
        {
            List<RoomEntity> units = new List<RoomEntity>();
            TryCatch.Run(() =>
            {
                var list = _Room.GetFloorRoom(FloorId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetFloorRoom method of RoomController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
