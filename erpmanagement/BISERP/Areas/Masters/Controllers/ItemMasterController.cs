using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Filters;
using System.Text.RegularExpressions;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ItemMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ItemMasterController));

        public ItemMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllItemMasters(string SearchName, string SearchCode, string Searchitemtypename)
        {
            JsonResult jResult;
            List<ItemsModel> records = new List<ItemsModel>();
            try
            {
                string _url = url + "/items/getallitem" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ItemsModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Searchitemtypename))
                    {
                        records = records.Where(p => p.itemtypename.ToUpper().Contains(Searchitemtypename.ToUpper())).ToList();
                    }
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Massage = "No Data Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllItemMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error in Server! Contact Administrator");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> ActiveItemMasters()
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getactiveitems" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(items.OrderBy(p => p.Name).ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ActiveItemMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetSellingItemMaster()
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getSellingItemMaster" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(new { success = true, items = items.OrderBy(p => p.Name).ToList() }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Massage = "No Data Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSellingItemMaster :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetPackingListItemMaster()
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getPackingListItemMaster" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(new { success = true, items = items.OrderBy(p => p.Name).ToList() }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Massage = "No Data Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPackingListItemMaster :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> NonVendorItemMasters()
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getnonvendoritems" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in NonVendorItemMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> NonIndentItemMasters(int StoreId)
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getnonindentitems/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in NonIndentItemMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> ItemSuppliers(int ItemId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getitembyid/" + ItemId.ToString() + Common.Constants.JsonTypeResult;
                var items = await Common.AsyncWebCalls.GetAsync<ItemMasterModel>(client, _url, CancellationToken.None);

                if (items != null)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Item Suppliers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public JsonResult VendorItems(List<int> ItemIds)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getvendoritemsbyid/" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, ItemIds, new JsonMediaTypeFormatter()).Result;
                var items = JsonConvert.DeserializeObject<List<ItemMasterModel>>(result.Content.ReadAsStringAsync().Result);
                if (items != null)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Item VendorItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpPost]

        public JsonResult SaveItemMaster(ItemMasterModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = System.DateTime.Now;
            items.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            items.InsertedMacID = BISERP.Common.Constants.MacId;
            items.InsertedMacName = BISERP.Common.Constants.MacName;

            if (items.ID > 0)
            {
                _url = url + "/items/updateitemmaster" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    items = JsonConvert.DeserializeObject<ItemMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/items/createitem" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    items = JsonConvert.DeserializeObject<ItemMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = items.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetItemsByStoreItemType(int? StoreId, int? ItemTypeId)
        {
            JsonResult jResult;
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                if (ItemTypeId == null)
                    ItemTypeId = 0;

                string _url = url + "/items/getItembyStore/" + StoreId.ToString() + "/" + ItemTypeId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    items.ForEach(m => m.storeId = StoreId);
                    items = items.OrderBy(p => p.Name).ToList();
                    // jResult = Json( items, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetItemsByStoreItemType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStorewiseVendorItems(int StoreId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getStorewisevendoritems/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    items = items.OrderBy(p => p.Name).ToList();
                    //jResult = Json(new { items }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStorewiseVendorItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetItemsByStore(int StoreId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getItembyStore/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> _items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (_items != null && _items.Count > 0)
                {
                    //jResult = Json(_items.OrderBy(i => i.Name), JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items = (_items.OrderBy(i => i.Name)) }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetItemsByStore :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStoreStock(int StoreId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getstorestock/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> _items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (_items != null && _items.Count > 0)
                {
                    jResult = Json(_items.OrderBy(i => i.Name), JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreStock :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStoreItems(int StoreId, string strSearchItemName = null)
        {
            JsonResult jResult;
            try
            {
                if (strSearchItemName == null)
                    strSearchItemName = "";

                string _url = url + "/items/getstoreitems/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    if (strSearchItemName != "")
                        items = items.Where(p => p.Name.ToUpper().Contains(strSearchItemName.ToUpper().Trim())).ToList();

                    //jResult = Json(items.OrderBy(i => i.Name), JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items = (items.OrderBy(i => i.Name)) }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetStoreItemStock(int StoreId)
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getItemStoreStock/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    //jResult = Json(entity, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json(entity, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreItemStock :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetAssetItems()
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getassetitems" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    items = items.OrderBy(p => p.Name).ToList();
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAssetItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetItemCode(int itemtypeid)
        {
            JsonResult jResult;
            List<ItemMasterModel> entity = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getitemcode/" + itemtypeid.ToString() + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetItemCode :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetItemsforclientbilling(int storeId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/items/getItemsforclientbilling/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                List<ItemMasterModel> _items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (_items != null && _items.Count > 0)
                {
                    //jResult = Json(_items.OrderBy(i => i.Name), JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, items = (_items.OrderBy(i => i.Name)) }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetItemsforclientbilling :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error in Server! Contact Administrator");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetItemDetails(string ItemIdList, int StoreID)
        {
            JsonResult jResult;
            List<ItemMasterModel> items = new List<ItemMasterModel>();
            try
            {
                string _url = url + "/items/getItemDetails/" + ItemIdList.ToString() + "/" + StoreID.ToString() + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in NonIndentItemMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }
    }
}
