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
    [RoutePrefix("api/storeitemtype")]
    public class StoreItemTypeMappingController : ApiController
    {
         IStoreItemTypeMappingRepository _StoreItemTypeMaster;
         IItemTypeMasterRepository _ItemTypeMaster;
         private static readonly ILogger _loggger = Logger.Register(typeof(StoreItemTypeMappingController));

        public StoreItemTypeMappingController(IStoreItemTypeMappingRepository StoreItemTypeMaster, IItemTypeMasterRepository ItemTypeMaster)
        {
            _StoreItemTypeMaster = StoreItemTypeMaster;
            _ItemTypeMaster = ItemTypeMaster;
        }

        [Route("getallstoreitemtype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllStoreItemTypeMapping()
        {
            List<StoreItemTypeMappingMasterEntities> StoreItemType = new List<StoreItemTypeMappingMasterEntities>() ;
            TryCatch.Run(() =>
            {
                var list = _StoreItemTypeMaster.GetAllStoreItemMappings();
                if (list != null && list.Count() > 0)
                    StoreItemType = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllStoreItemTypeMapping method of StoreItemTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (StoreItemType.Any())
                return Ok(StoreItemType);
            else
                return BadRequest();
        }


        [Route("getallstoreitemtypeid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
       
        public IHttpActionResult GetStoreItemTypeMappingById(int id)
        {
            List<StoreItemTypeMappingMasterEntities> StoreItemType = new List<StoreItemTypeMappingMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _StoreItemTypeMaster.GetStoreItemMappingByStoreId(id);
                if (list != null && list.Count() > 0)
                    StoreItemType = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreItemTypeMappingById method of StoreItemTypeController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (StoreItemType.IsNotNull())
                return Ok(StoreItemType);
            else
                return NotFound();
        }

        //[Route("createstoreitemtype")]
        //[AcceptVerbs("POST")]
        //public IHttpActionResult CreateStoreItemType(StoreItemTypeMappingMasterEntities entity)
        //{
        //    bool isSucecss = false;
        //    TryCatch.Run(() =>
        //    {
        //        //foreach (var itypeId in entity.Itemtype)
        //        //{
        //        //    _StoreItemTypeMaster.CreateStoreItemTypeMapping(itypeId, entity);
        //        //}
        //        var itemTypeList = string.Join(",", entity.Itemtype);
        //        _StoreItemTypeMaster.UpdateItemTypeMapping(itemTypeList, entity);
        //        isSucecss = true;
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in CreateStoreItemType method of MaterialIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    if (isSucecss)
        //        return Created<StoreItemTypeMappingMasterEntities>(Request.RequestUri + entity.StoreID.ToString(), entity);
        //    else
        //        return BadRequest();
        //}

        [Route("Updatestoreitemtype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateStoreItemType(List<StoreItemTypeMappingMasterEntities> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var SItem in entity)
                {
                    isSucecss = _StoreItemTypeMaster.UpdateItemTypeMapping(SItem);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateStoreItemType method of StoreItemTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}
