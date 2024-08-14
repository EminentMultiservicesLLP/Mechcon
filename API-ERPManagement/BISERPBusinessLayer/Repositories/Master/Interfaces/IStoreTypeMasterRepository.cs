using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IStoreTypeMasterRepository
    {
        StoreTypeMasterEntities GetStoreTypeById(int Id);
        IEnumerable<StoreTypeMasterEntities> GetAllStoreTypes();
        int  CreateStoreType(StoreTypeMasterEntities entity);
        bool UpdateStoreType(StoreTypeMasterEntities entity);
        bool DeleteStoreType(StoreTypeMasterEntities entity);
    }
}
