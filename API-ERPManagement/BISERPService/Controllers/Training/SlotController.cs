using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/slot")]
    public class SlotController : ApiController
    {
        readonly ISlotRepository _slot;
        private static readonly ILogger Loggger = Logger.Register(typeof(SlotController));

        public SlotController(ISlotRepository slot)
        {
            _slot = slot;
        }

        private List<SlotEntity> AllSlot()
        {
            List<SlotEntity> type = new List<SlotEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllSlot.ToString()))
                {
                    var list = _slot.GetAllSlot();
                    if (list != null && list.Count() > 0)
                        type = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllSlot.ToString(), type);
                }
                else
                {
                    type = (List<SlotEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllSlot.ToString()));
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllSlot method of SlotController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllSlot")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllSlot()
        {
            List<SlotEntity> slot = new List<SlotEntity>();
            TryCatch.Run(() =>
            {
                slot = AllSlot();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllSlot method of SlotController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (slot.IsNotNull())
                return Ok(slot);
            else
                return BadRequest("Unknown EError! Failed to save Slot, Please contact system Administrator");
        }

        [Route("CreateSlot")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSlot(SlotEntity slot)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _slot.CheckDuplicateItem(slot.SlotCode);
                if (isDuplicate == false)
                {
                    var newId = _slot.CreateSlot(slot);
                    slot.SlotId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSlot.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateSlot method of SlotController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Slot Code Already exist in system, please use another code");
            else if (isSucecss)
                return Created<SlotEntity>(Request.RequestUri + slot.SlotId.ToString(), slot);
            else
                return BadRequest("Unknown EError! Failed to save Slot, Please contact system Administrator");
        }

        [Route("UpdateSlot")]
        [AcceptVerbs("PUT")]
        public HttpResponseMessage UpdateSlot(SlotEntity slot)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _slot.CheckDuplicateupdate(slot.SlotCode, slot.SlotId);
                if (isDuplicate == false)
                {
                    isSucecss = _slot.UpdateSlot(slot);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSlot.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateSlot method of SlotController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Slot Code already exists inn system" };
            }
            else if (!isSucecss)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Failed to save Slot, Please contact system Administrator" };
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("getactiveSlot")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActiveSlot() 
        {
            List<SlotEntity> units = new List<SlotEntity>();
            TryCatch.Run(() =>
            {
                var list = AllSlot();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveSlot method of SlotController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }



        [Route("GetTraniningWiseSlot/{TrainingTypeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTraniningWiseSlot(int TrainingTypeId)
        {
            IEnumerable<SlotEntity> slot = null;
            TryCatch.Run(() =>
            {
                slot = _slot.GetTraniningWiseSlot(TrainingTypeId);
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTraniningWiseSlot method of SlotController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (slot == null)
                 return BadRequest();
            return Ok(slot.ToList());
        }


        [Route("getdatewiseslot")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDateWiseSlot()
        {
            IEnumerable<SlotEntity> slot = null;
            TryCatch.Run(() =>
            {
                slot = _slot.GetDateWiseSlot();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetDateWiseSlot method of SlotController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (slot == null)
                return BadRequest();
            return Ok(slot.ToList());
        }
    }
}
