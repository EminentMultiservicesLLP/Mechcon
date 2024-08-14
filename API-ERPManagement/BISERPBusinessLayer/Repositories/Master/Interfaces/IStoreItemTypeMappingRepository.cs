using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IStoreItemTypeMappingRepository
    {
        IEnumerable<StoreItemTypeMappingMasterEntities> GetStoreItemMappingByStoreId(int Id);
        IEnumerable<StoreItemTypeMappingMasterEntities> GetAllStoreItemMappings();
        int CreateStoreItemTypeMapping(string Itemtype, StoreItemTypeMappingMasterEntities mainentity);
        bool DeleteItemTypeMapping(StoreItemTypeMappingMasterEntities entity);
        bool UpdateItemTypeMapping( StoreItemTypeMappingMasterEntities entity);
    }
}
