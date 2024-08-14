
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IItemGroupMasterRepository
    {
        ItemGroupMasterEntities GetItemGroupById(int itemgroupId);
        IEnumerable<ItemGroupMasterEntities> GetAllItemGroup();
        bool CreateItemGroup(ItemGroupMasterEntities itemgroupEntity);
        bool UpdateItemGroup(ItemGroupMasterEntities itemgroupEntity);
        bool DeleteItemGroup(ItemGroupMasterEntities itemgroupEntity);
        IEnumerable<ItemGroupMasterEntities> GetActiveItemGroup();
    }
}
