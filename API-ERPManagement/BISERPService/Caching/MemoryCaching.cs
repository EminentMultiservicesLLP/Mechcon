using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace BISERPService.Caching
{
    public static class MemoryCaching
    {
        public static bool CacheKeyExist(string key)
        {
            MemoryCache cache = MemoryCache.Default;
            return cache.Contains(key);
        }
        public static object GetCacheValue(string key)
        {
            MemoryCache cache = MemoryCache.Default;
            if (cache.Contains(key))
                return cache.Get(key);
            else
                return null;
        }

        public static void AddCacheValue(string key, object value)
        {
            MemoryCache cache = MemoryCache.Default;
            cache.Add(key, value, DateTimeOffset.UtcNow.AddHours(12));
        }

        public static object GetOrAddCacheValue(string key, object value)
        {
            MemoryCache cache = MemoryCache.Default;
            if(!CacheKeyExist(key))
            {
                cache.Add(key, value, DateTimeOffset.UtcNow.AddHours(12));
            }
            
            return cache.Get(key);
        }

        public static void RemoveCacheValue(string key)
        {
            MemoryCache cache = MemoryCache.Default;
            if(CacheKeyExist(key))
                cache.Remove(key);
        }

        public static void ClearAllCache()
        {
            MemoryCache cache = MemoryCache.Default;
            List<string> cacheKeys = cache.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                cache.Remove(cacheKey);
            }
        }


    }

    enum CachingKeys
    {
        AllUnits = 0,
        AllItems =1,
        AllItemType = 2,
        AllPackSize = 3,
        AllBranch = 4,
        AllStores = 5,
        AllStoreTypes = 6,
        AllManufacturer = 7,
        AllSupplier = 8,
        AllOtherTerms = 9,
        AllDeliveryTerms = 10,
        AllPaymentTerms = 11,

        AllActiveItems = 12,
        ItemsCategoryWise = 13,
        AllActiveBranch = 14,

        AllCountries =15,
        AllState = 16,
        AllCity =17,
        AllVillages =18,

        AllTaxList = 19,
        AllUserMenuRights = 2019,
        AllDivision =20,
        AllSubDivision =21,
        AllDriver = 22,
        AllTechnician = 23,
        AllAssetType = 24,
        AllAssetSubType = 25,
        AllCallType = 26,
        AllMaintainanceType = 27,
        DeliveryTerms = 28,
        PaymentTerms = 29,
        OtherTerms = 30,
        AllVehicleMake = 31,
        AllVehicleModel = 32,
        AllLocation = 33,
        AllVendor = 34,
        AllTrainingType = 35,
        AllBatchType = 36,
        AllSubject = 37,
        AllSubjectTopics = 38,
        AllTrainer = 39,
        AllRating = 40,
        AllSlot = 41,
        AllTrainingCategory = 42,
        AllBudgetHeads = 43,
        AllClient = 44,
        ProjectTC = 45

    }
}