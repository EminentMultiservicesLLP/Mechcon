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
using BISERPService.Caching;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemController : ApiController
    {
        IItemMasterRepository _itemMaster;
        IItemSupplierMappingRepository _isupplierMapping;
        private static readonly ILogger _loggger = Logger.Register(typeof(ItemController));

        public ItemController(IItemMasterRepository itemMaster, IItemSupplierMappingRepository isupplierMapping)
        {
            _itemMaster = itemMaster;
            _isupplierMapping = isupplierMapping;
        }

        public string Get(int id)
        {
            return "";
        }

        private List<ItemsModel> AllItems()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllItems.ToString()))
                {
                   var list = _itemMaster.GetAllItems();
                  //  var list = _itemMaster.NewGetAllItems();
                    if (list != null && list.Count() > 0)
                        items = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.AllItems.ToString(), items);
                }
                else
                {
                    items = (List<ItemsModel>)(MemoryCaching.GetCacheValue(CachingKeys.AllItems.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
                return items;
        }

        private List<ItemsModel> NewAllItems()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllItems.ToString()))
                {
                    var list = _itemMaster.GetAllItems();
                    if (list != null && list.Count() > 0)
                        items = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.AllItems.ToString(), items);
                }
                else
                {
                    items = (List<ItemsModel>)(MemoryCaching.GetCacheValue(CachingKeys.AllItems.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return items;
        }


        [Route("getallitem")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllItems()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                items = NewAllItems();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllItemMaster method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return Ok(items);
        }

        [Route("getactiveitems")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveItems()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                var temp = AllItems();
                items = temp.Where(w => w.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getSellingItemMaster")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSellingItemMaster()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                var temp = AllItems();
                items = temp.Where(w => w.Service == true).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetSellingItemMaster method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }


        [Route("getPackingListItemMaster")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPackingListItemMaster()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                var temp = AllItems();
                items = temp.Where(w => w.PackingList == true).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPackingListItemMaster method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                    return Ok(items);
            else
                return BadRequest();
        }

        [Route("getnonvendoritems")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetNonVendorItems()
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetNonVendorItems();
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetNonVendorItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getnonindentitems/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetNonIndentItems(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetNonIndentItems(StoreId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetNonIndentItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getItembyStore/{StoreId}/{CategoryId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveItems(int StoreId, int CategoryId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetActiveItems(StoreId, CategoryId);
                if (list != null && list.Any())
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                items = null;
                _loggger.LogError("Error in GetActiveItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                //return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            //return BadRequest();
            return InternalServerError();
        }

        [Route("getItembyStore/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreItems(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetStoreItems(StoreId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getStorewisevendoritems/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreItemsStock(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetStorewiseVendorItems(StoreId);
                if (list.IsNotNull() && list.Any())
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                items = null;
                _loggger.LogError("Error in GetStoreItemsStock method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                
            });
            if (items.IsNotNull())
                return Ok(items);
            //return BadRequest();
            return InternalServerError();
        }

        [Route("getstorestock/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreStock(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetStoreStock(StoreId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getstoreitems/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult StoreItemsOPBal(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.StoreItemsOPBal(StoreId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in StoreItemsOPBal method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getItemStoreStock/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreItemStock(int StoreId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetAllstorestockItems(StoreId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getitembyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetItemById(int id)
        {
            ItemMasterEntities item = new ItemMasterEntities();
            TryCatch.Run(() =>
            {
                item = _itemMaster.GetItemById(id);
                //var temp = AllItems();
                //item = temp.Where(w => w.Deactive == false && w.ID == id).FirstOrDefault();
                item.suppliers = _isupplierMapping.GetItemSupplierMappingByItemId(id);
                item.vendorItems = _isupplierMapping.GetVendorItemsByItemId(id);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetItemMasterById method of ItemMasterController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (item.IsNotNull())
                return Ok(item);
            else
                return NotFound();
        }

        [Route("getvendoritemsbyid")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetItemById(List<int> ItemIds)
        {
            List<ItemMasterEntities> item = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                item = _isupplierMapping.GetVendorItemsByItemIds(ItemIds);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetItemById method of ItemMasterController " + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (item.IsNotNull())
                return Ok(item);
            else
                return NotFound();
        }

        [Route("createitem")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateItem(ItemMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _itemMaster.CheckDuplicateItem(entity.Name,entity.DescriptiveName);
                if (isDuplicate == false)
                {
                    var newID = _itemMaster.CreateItemMaster(entity);
                    entity.ID = newID;

                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllItems.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateItem method of ItemMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if(isDuplicate)
                return BadRequest("Same Item record Already Exist");
            else if (isSucecss)
                return Created<ItemMasterEntities>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updateitemmaster")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateItemMaster(ItemMasterEntities entity)
        {
            bool isSucecss = false,isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _itemMaster.CheckDuplicateupdate(entity.Code, entity.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _itemMaster.UpdateItemMaster(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllItems.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateItemMaster method of ItemMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("deleteitemaster")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteItemMaster(int itemID, ItemMasterEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _itemMaster.DeleteItemMaster(entity);
                    isSucecss = false;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteItemMaster method of ItemMasterController : parameter :" + itemID.ToString() + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getassetitems")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAssetItems()
        {
            List<ItemsModel> items = new List<ItemsModel>();
            TryCatch.Run(() =>
            {
                var list = AllItems();
                items = list.Where(w => w.Asset == true).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAssetItems method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getitemcode/{itemtypeid}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemCode(int itemtypeid)
        {
            List<GetItemModal> items = new List<GetItemModal>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetItemCode(itemtypeid);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetItemCode method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getItemsforclientbilling/{storeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemsforclientbilling(int storeId)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetItemsforclientbilling(storeId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getItemsforclientbilling method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            else
                return BadRequest();
        }
        [Route("getItemDetails/{ItemIdList}/{StoreID}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemDetails(string ItemIdList,int StoreID)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _itemMaster.GetItemDetails(ItemIdList, StoreID);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getItemDetails method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.IsNotNull())
                return Ok(items);
            else
                return BadRequest();
        }
    }
}
