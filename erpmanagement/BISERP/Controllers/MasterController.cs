using BISERP.Models;
using BISERP.Areas.Store.Models.Store;
using BISERP.Areas.Masters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using BISERPCommon;
using BISERP.Areas.Asset.Models;
using BISERP.Filters;

namespace BISERP.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class MasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static ILogger _logger = Logger.Register(typeof(MasterController));

        public MasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllItemPackSize()
        {
            JsonResult jResult;
            string _url = url + "/ipacksizes/getactivepacksize" + Common.Constants.JsonTypeResult;
            List<ItemPackSizeMasterModel> _itempacksizes = await Common.AsyncWebCalls.GetAsync<List<ItemPackSizeMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itempacksizes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllItemType()
        {
            JsonResult jResult;
            string _url = url + "/itemtype/getactiveitemtype" + Common.Constants.JsonTypeResult;
            List<ItemTypeMasterModel> _itemtype = await Common.AsyncWebCalls.GetAsync<List<ItemTypeMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itemtype, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> AllActiveUnit()
        {
            JsonResult jResult;
            string _url = url + "/Units/getactiveUnits" + Common.Constants.JsonTypeResult;
            List<UnitMasterModel> _itemtype = await Common.AsyncWebCalls.GetAsync<List<UnitMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itemtype, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetItemTypeByStoreId(int StoreId)
        {
            JsonResult jResult;
            string _url = url + "itemtype/GetItemTypeByStoreId/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
            List<ItemTypeMasterModel> _itemtype = await Common.AsyncWebCalls.GetAsync<List<ItemTypeMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itemtype, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> AllStores()
        {
            JsonResult jResult;
            List<StoreMasterModel> _store = new List<StoreMasterModel>();
            var userid = Session["AppUserId"].ToString();
            if (userid == "1320")
            {
                string _url = url + "/stores/activestores/" + 1 + Common.Constants.JsonTypeResult;
                _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
            }
            else
            {
                string _url = url + "/stores/activestores/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
            }
            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllCentreStores()
        {
            JsonResult jResult;
            string _url = url + "/stores/activestores/" + 1 + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> BranchStores(int BranchId)
        {
            JsonResult jResult;
            string _url = url + "/stores/branchstores/" + BranchId + "/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> MainStores()
        {
            JsonResult jResult;
            string _url = url + "/stores/getallmainstore" + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> SubStores()
        {
            JsonResult jResult;
            string _url = url + "/stores/substores" + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> UnitStores()
        {
            JsonResult jResult;
            string _url = url + "/stores/unitstores" + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllStoreType()
        {
            JsonResult jResult;
            string _url = url + "/storetypes/getallstoretypemasters" + Common.Constants.JsonTypeResult;
            List<StoreTypeMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreTypeMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_store, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> AllBranches()
        {
            JsonResult jResult;
            string _url = url + "/branch/getactivebranch/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
            List<BranchModel> _branch = await Common.AsyncWebCalls.GetAsync<List<BranchModel>>(client, _url, CancellationToken.None);
            return jResult = Json(_branch, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllACtiveBranches()
        {
            JsonResult jResult;
            string _url = url + "/branch/getactivebranch" + Common.Constants.JsonTypeResult;
            List<BranchModel> _branch = await Common.AsyncWebCalls.GetAsync<List<BranchModel>>(client, _url, CancellationToken.None);
            return jResult = Json(_branch, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> AllSupplier(string SearchSupplierName)
        {
            JsonResult jResult;
            string _url = url + "/supplier/getactivesupplier" + Common.Constants.JsonTypeResult;
            List<SupplierMasterModel> _supplier = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(client, _url, CancellationToken.None);
            if (!string.IsNullOrWhiteSpace(SearchSupplierName))
            {
                _supplier = _supplier.Where(s => s.Name.ToUpper().StartsWith(SearchSupplierName.ToUpper())).ToList();
            }
            return jResult = Json(_supplier, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllCities()
        {
            JsonResult jResult;
            string _url = url + "/generic/getallcitymasters" + Common.Constants.JsonTypeResult;
            List<CityMasterModel> _cities = await Common.AsyncWebCalls.GetAsync<List<CityMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_cities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetCityById(int stateId)
        {
            JsonResult jResult;
            string _url = url + "/generic/getcitybyid/" + stateId + Common.Constants.JsonTypeResult;
            List<CityMasterModel> _cities = await Common.AsyncWebCalls.GetAsync<List<CityMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_cities, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> AllVillages()
        {
            JsonResult jResult;
            string _url = url + "/generic/getallvillagemasters" + Common.Constants.JsonTypeResult;
            List<VillageMasterModel> _villages = await Common.AsyncWebCalls.GetAsync<List<VillageMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_villages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllStates()
        {
            JsonResult jResult;

            string _url = url + "/generic/getallstatemasters" + Common.Constants.JsonTypeResult;
            List<StateMasterModel> _states = await Common.AsyncWebCalls.GetAsync<List<StateMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_states, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetStateByCountryId(int CountryId)
        {
            JsonResult jResult;

            string _url = url + "/generic/getstatebycountryId/" + CountryId + Common.Constants.JsonTypeResult;
            List<StateMasterModel> _states = await Common.AsyncWebCalls.GetAsync<List<StateMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_states, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AllCountry()
        {
            JsonResult jResult;

            string _url = url + "/generic/getallcountrymasters" + Common.Constants.JsonTypeResult;
            List<CountryMasterModel> _countries = await Common.AsyncWebCalls.GetAsync<List<CountryMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_countries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> TaxMaster()
        {
            JsonResult jResult;
            List<TaxMasterModel> records = new List<TaxMasterModel>();
            string _url = url + "/generic/getalltaxmasters" + Common.Constants.JsonTypeResult;
            records = await Common.AsyncWebCalls.GetAsync<List<TaxMasterModel>>(client, _url, CancellationToken.None);
            jResult = Json(records, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllManufacturer()
        {
            JsonResult jResult;
            List<ManufacturerMasterModel> records = new List<ManufacturerMasterModel>();
            string _url = url + "/manufacturer/getactivemanufacturer" + Common.Constants.JsonTypeResult;
            records = await Common.AsyncWebCalls.GetAsync<List<ManufacturerMasterModel>>(client, _url, CancellationToken.None);
            jResult = Json(records, JsonRequestBehavior.AllowGet);
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AllActiveItemGroup()
        {
            JsonResult jResult;
            string _url = url + "/itemgroups/getactiveitemgroup" + Common.Constants.JsonTypeResult;
            List<ItemGroupMasterModel> _itemgroup = await Common.AsyncWebCalls.GetAsync<List<ItemGroupMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itemgroup, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> AllActivelocation()
        {
            List<LocationModel> records = new List<LocationModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/location/getactiveLocation" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<LocationModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActivelocation :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
