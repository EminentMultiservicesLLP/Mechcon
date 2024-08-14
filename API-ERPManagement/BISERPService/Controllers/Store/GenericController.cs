using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/generic")]
    public class GenericController : ApiController
    {
        ICityMasterRepository _cityMaster;
        IStateMasterRepository _stateMaster;
        ICountryMasterRepository _countryMaster;
        IVillageMasterRepository _villageMaster;
        IBranchMasterRepository _branchMaster;
        ITaxMasterRepository _taxMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(GenericController));

        public GenericController(ICityMasterRepository cityMaster,IStateMasterRepository stateMaster,
        ICountryMasterRepository countryMaster,
        IVillageMasterRepository villageMaster,
        IBranchMasterRepository branchMaster,
        ITaxMasterRepository taxMaster)
        {
            _cityMaster = cityMaster;
            _stateMaster = stateMaster;
            _countryMaster = countryMaster;
            _villageMaster = villageMaster;
            _branchMaster = branchMaster;
            _taxMaster = taxMaster;
        }


        [Route("getallbranches")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<BranchMasterEntities> GetAllBranches()
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");

            List<BranchMasterEntities> branches = null;

            _loggger.WatchTime(() => "Starting Branch Master processing", () =>
            {
                var list = _branchMaster.GetAllBranches();
                if (list != null && list.Count() > 0)
                    branches = list.ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return branches;
        }

        private IEnumerable<CityMasterEntities> AllCity()
        {
            List<CityMasterEntities> alllist = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllCity.ToString()))
                {
                    var list = _cityMaster.GetAllCities();
                    if (list != null && list.Count() > 0)
                        alllist = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllCity.ToString(), alllist);
                }
                else
                {
                    alllist = (List<CityMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllCity.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllCity method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return alllist;
        }

        private IEnumerable<CountryMasterEntities> AllCountry()
        {
            List<CountryMasterEntities> alllist = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllCountries.ToString()))
                {
                    var list = _countryMaster.GetAllCountrys();
                    if (list != null && list.Count() > 0)
                        alllist = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllCountries.ToString(), alllist);
                }
                else
                {
                    alllist = (List<CountryMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllCountries.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllCountry method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return alllist;
        }

        private IEnumerable<StateMasterEntities> AllState()
        {
            List<StateMasterEntities> alllist = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllState.ToString()))
                {
                    var list = _stateMaster.GetAllStates();
                    if (list != null && list.Count() > 0)
                        alllist = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllState.ToString(), alllist);
                }
                else
                {
                    alllist = (List<StateMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllState.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllState method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return alllist;
        }

        private IEnumerable<VillageMasterEntities> AllVillage()
        {
            List<VillageMasterEntities> alllist = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllVillages.ToString()))
                {
                    var list = _villageMaster.GetAllVillages();
                    if (list != null && list.Count() > 0)
                        alllist = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllVillages.ToString(), alllist);
                }
                else
                {
                    alllist = (List<VillageMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllVillages.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllVillages method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return alllist;
        }

        [Route("getallcitymasters")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<CityMasterEntities> GetAllCityMasters()
        {
            List<CityMasterEntities> cities = null;
            TryCatch.Run(() =>
            {
                cities = AllCity().ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllCityMasters method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return cities;
        }

        [Route("getcitybyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IEnumerable<CityMasterEntities> GetCityById(int id)
        {
            List<CityMasterEntities> city = null;
            TryCatch.Run(() =>
            {
                var temp = AllCity().ToList();
                city = temp.Where(c=>c.StateId==id).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetCityById method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return city;
        }

        [Route("getallvillagemasters")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<VillageMasterEntities> GetAllVillageMasters()
        {
            List<VillageMasterEntities> villages = null;
            TryCatch.Run(() =>
            {
                villages = AllVillage().ToList();
               
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVillageMasters method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return villages;
        }

        [Route("getvillagebyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public VillageMasterEntities GetVillageById(int id)
        {
            VillageMasterEntities village = null;
            TryCatch.Run(() =>
            {
                var temp = AllVillage().ToList();
                village = temp.Where(w => w.ID == id).FirstOrDefault();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetVillageById method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return village;
        }

        [Route("getallstatemasters")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<StateMasterEntities> GetAllStateMasters()
        {
            List<StateMasterEntities> states = null;
            TryCatch.Run(() =>
            {
                states = AllState().ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllStateMasters method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return states;
        }

        [Route("getstatebyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public StateMasterEntities GetStateById(int id)
        {
            StateMasterEntities states = null;
            TryCatch.Run(() =>
            {
                var temp = AllState().ToList();
                states = temp.Where(w => w.ID == id).FirstOrDefault();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStateById method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return states;
        }

        [Route("getallcountrymasters")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<CountryMasterEntities> GetAllCountryMasters()
        {
            List<CountryMasterEntities> countrys = null;
            TryCatch.Run(() =>
            {
                countrys = AllCountry().ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllCountryMasters method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return countrys;
        }

        [Route("getcountrybyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public CountryMasterEntities GetStoreTypeById(int id)
        {
            CountryMasterEntities country = null;
            TryCatch.Run(() =>
            {
                var temp = AllCountry().ToList();
                country = temp.Where(w => w.ID == id).FirstOrDefault();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStateById method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return country;
        }

        [Route("getalltaxmasters")]
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<TaxMasterEntity> TaxMaster()
        {
            List<TaxMasterEntity> taxes = null;
            TryCatch.Run(() =>
            {
                taxes = AllTaxList().ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in TaxMaster method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return taxes;
        }

        private IEnumerable<TaxMasterEntity> AllTaxList()
        {
            List<TaxMasterEntity> alllist = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllTaxList.ToString()))
                {
                    var list = _taxMaster.GetAllTaxes();
                    if (list != null && list.Count() > 0)
                        alllist = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllTaxList.ToString(), alllist);
                }
                else
                {
                    alllist = (List<TaxMasterEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllTaxList.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllTaxList method of GenericController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return alllist;
        }

    }
}
