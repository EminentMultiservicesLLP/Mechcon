using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IItemTypeMasterRepository
    {
        ItemTypeMasterEntities GetItemTypeById(int itemtypeId);
        IEnumerable<ItemTypeMasterEntities> GetAllItemType();

        IEnumerable<ItemTypeMasterEntities> GetItemTypeByStoreId(int StoreId);
        IEnumerable<ItemTypeMasterEntities> GetActiveItemType();
        int CreateItemType(ItemTypeMasterEntities itemtypeEntity);
        bool UpdateItemType(ItemTypeMasterEntities itemtypeEntity);
        bool DeleteItemType(ItemTypeMasterEntities itemtypetEntity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);


    }
}
