using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/ipacksizes")]
    public class ItemPackSizeController : ApiController
    {
        IItemPackSizeMasterRepository _ItemPackSizeGroupMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ItemPackSizeController));

        public ItemPackSizeController(IItemPackSizeMasterRepository ItemPackSizeGroupMaster)
        {
            _ItemPackSizeGroupMaster = ItemPackSizeGroupMaster;
        }

        public string Get(int id)
        {
            return "";
        }

        private List<ItemPackSizeMasterEntities> AllItemPackSize()
        {
            List<ItemPackSizeMasterEntities> itypesize = new List<ItemPackSizeMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllPackSize.ToString()))
                {
                    var list = _ItemPackSizeGroupMaster.GetAllItemPackSize();
                    if (list != null && list.Count() > 0)
                        itypesize = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllPackSize.ToString(), itypesize);
                }
                else
                {
                    itypesize = (List<ItemPackSizeMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllPackSize.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllItemPackSize method of ItemPackSizeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            return itypesize;
        }


        [Route("getallitemtypesizes")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllItemTypeSize()
        {
            List<ItemPackSizeMasterEntities> itypesize = new List<ItemPackSizeMasterEntities>();
            TryCatch.Run(() =>
            {
                itypesize = AllItemPackSize();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllItemTypeSize method of ItemPackSizeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (itypesize.Any())
                return Ok(itypesize);
            else
                return Ok(itypesize);
        }

        [Route("getitypesizebyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemTypeSizeById(int id)
        {
            ItemPackSizeMasterEntities itypesize = new ItemPackSizeMasterEntities();
            TryCatch.Run(() =>
            {
                //itypesize = _ItemPackSizeGroupMaster.GetItemPackSizeById(id);
                var temp = AllItemPackSize();
                itypesize = temp.Where(w => w.ID == id && w.Deactive == false).FirstOrDefault();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetItemTypeSizeById method of PackSizeController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (itypesize.IsNotNull())
                return Ok(itypesize);
            else
                return NotFound();
        }

        [Route("createitempacksize")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateItemPackSize(ItemPackSizeMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;           
            TryCatch.Run(() =>
            {
                isDuplicate = _ItemPackSizeGroupMaster.CheckDuplicateItem(entity.Code);
                if (isDuplicate == false)
                {
                    var newID = _ItemPackSizeGroupMaster.CreateItemPackSize(entity);
                    entity.ID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllPackSize.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePackSize method of PackSizeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ItemPackSizeMasterEntities>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest();
           
        }

        [Route("updateitempacksize")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePackSize(ItemPackSizeMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _ItemPackSizeGroupMaster.CheckDuplicateupdate(entity.Code, entity.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _ItemPackSizeGroupMaster.UpdateItemPackSize(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllPackSize.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePackSize method of PackSizeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<ItemPackSizeMasterEntities>(Request.RequestUri + entity.ID.ToString(), entity));
            else
                return BadRequest();         
           
        }

        [Route("deleteitempacksize")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeletePackSize(int PackSizeID, ItemPackSizeMasterEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ItemPackSizeGroupMaster.DeleteItemPackSize(entity);
                MemoryCaching.RemoveCacheValue(CachingKeys.AllPackSize.ToString());
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeletePackSize method of PackSizeController : parameter :" + PackSizeID.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
        [Route("getactivepacksize")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllActiveItemTypeSize()
        {
            List<ItemPackSizeMasterEntities> itypesize = new List<ItemPackSizeMasterEntities>();
            TryCatch.Run(() =>
            {
                //var list = _ItemPackSizeGroupMaster.GetActivePackSize();
                //if (list != null && list.Count() > 0)
                //    itypesize = list.ToList();

                var temp = AllItemPackSize();
                itypesize = temp.Where(w => w.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllActiveItemTypeSize method of ItemPackSizeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (itypesize.Any())
                return Ok(itypesize);
            else
                return BadRequest();
        }
    }
}
