using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/storeunit")]
    public class StoreUnitLinkingController : ApiController
    {
        IStoreUnitLinkingMasterRepository _storeunit;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreUnitLinkingController));

        public StoreUnitLinkingController(IStoreUnitLinkingMasterRepository storeunit)
          {
              _storeunit = storeunit;          
          }
        [Route("getallunitstore")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreUnitLinkingEntities> GetAllUnitStore()
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");

            List<StoreUnitLinkingEntities> storeunit = null;

            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = _storeunit.GetAllUnitStore();
                if (list != null && list.Count() > 0)
                    storeunit = list.ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return storeunit;
        }

        [Route("createstoreunit")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Createstoreunit(StoreUnitLinkingEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _storeunit.CheckDuplicateUnitStore(entity);
                if (isDuplicate == false)
                {
                    var newID = _storeunit.CreateUnitStore(entity);
                    entity.ID = newID;
                    isSucecss = true;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Createstoreunit method of Controller : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Unit Store Already Linked To Other Store");
            else if (isSucecss)
                return Created<StoreUnitLinkingEntities>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updatestoreunit")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateStoreMaster(StoreUnitLinkingEntities entity)
        {
            bool isSucecss = false ,isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _storeunit.CheckDuplicateupdate(entity);
                if (isDuplicate == false)
                {
                    isSucecss = _storeunit.UpdateStoreUnit(entity);
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateStoreMaster method of Controller : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
              if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                  return Ok();
            else
                return BadRequest();
        }
  
    }
}
