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

namespace BISERPService.Controllers
{
    [RoutePrefix("api/itemtype")]
    public class ItemTypeController : ApiController
    {
        IItemTypeMasterRepository _itemTypeMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ItemTypeController));

        public ItemTypeController(IItemTypeMasterRepository ItemTypeMaster)
        {
            _itemTypeMaster = ItemTypeMaster;
        }

        [Route("getallitemtypes")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllItemTypes()
        {
            List<ItemTypeMasterEntities> ItemTypes = new List<ItemTypeMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemTypeMaster.GetAllItemType();
                if (list != null && list.Count() > 0)
                    ItemTypes = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllItemTypes method of ItemTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (ItemTypes.Any())
                return Ok(ItemTypes);
            else
                return Ok(ItemTypes);
        }

         [Route("getitemtypebyid/{id}")]
         [AcceptVerbs("GET", "POST")]
        // GET api/values/5
         public ItemTypeMasterEntities GetItemTypeById(int id)
        {
            ItemTypeMasterEntities ItemType = new ItemTypeMasterEntities();
            _loggger.WatchTime(() => "Starting ItemTypeMaster processing", () =>
            {
                ItemType = _itemTypeMaster.GetItemTypeById(id);
            });
            return ItemType;
        }


         [Route("GetItemTypeByStoreId/{StoreId}")]
         [AcceptVerbs("GET", "POST")]

         public IHttpActionResult GetItemTypeByStoreId(int StoreId)
         {
             List<ItemTypeMasterEntities> itemTypeMaster = new List<ItemTypeMasterEntities>();
             TryCatch.Run(() =>
             {
                 var list = _itemTypeMaster.GetItemTypeByStoreId(StoreId);
                 if (list != null && list.Count() > 0)
                     itemTypeMaster = list.ToList();
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in GetItemTypeByStoreId method of ItemTypeController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (itemTypeMaster.Any())
                 return Ok(itemTypeMaster);
             else
                 return BadRequest();

         }



         [Route("createitype")]
         [AcceptVerbs("POST")]
         public IHttpActionResult CreateItemType(ItemTypeMasterEntities itypeMaster)
         {
             bool isSucecss = false, isDuplicate = false;           
             TryCatch.Run(() =>
             {

                 isDuplicate = _itemTypeMaster.CheckDuplicateItem(itypeMaster.Code);
                 if (isDuplicate == false)
                 {
                     var newID = _itemTypeMaster.CreateItemType(itypeMaster);
                     itypeMaster.ID = newID;

                     isSucecss = true;
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in CreateItemType method of ItemTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (isDuplicate)
                 return BadRequest("Code Already Exist");
             else if (isSucecss)
                 return Created<ItemTypeMasterEntities>(Request.RequestUri + itypeMaster.ID.ToString(), itypeMaster);
             else
                 return BadRequest();
           
         }

         [Route("updateitype")]
         [AcceptVerbs("POST")]
         public IHttpActionResult UpdateItemType(ItemTypeMasterEntities itypeMaster)
         {
             bool isSucecss = false, isDuplicate = false; ;
             TryCatch.Run(() =>
             {
                 isDuplicate = _itemTypeMaster.CheckDuplicateupdate(itypeMaster.Code, itypeMaster.ID);
                 if (isDuplicate == false)
                 {
                     isSucecss = _itemTypeMaster.UpdateItemType(itypeMaster);
                 }
                 
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in UpdateItemType method of UnitController : parameter :" + Environment.NewLine + ex.StackTrace);
                 return new HttpResponseException(HttpStatusCode.InternalServerError);
             });
             if (isDuplicate)
                 return BadRequest("Code Already Exist");
             else if (isSucecss)
                 return Ok(Created<ItemTypeMasterEntities>(Request.RequestUri + itypeMaster.ID.ToString(), itypeMaster));
             else
                 return BadRequest();             
         }

         [Route("getactiveitemtype")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetActiveItems()
         {
             List<ItemTypeMasterEntities> units = new List<ItemTypeMasterEntities>();
             TryCatch.Run(() =>
             {
                 var list = _itemTypeMaster.GetActiveItemType();
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in GetActiveitemtype method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
         }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
