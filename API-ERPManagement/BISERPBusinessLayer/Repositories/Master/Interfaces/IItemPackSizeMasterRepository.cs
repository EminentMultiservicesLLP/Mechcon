using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IItemPackSizeMasterRepository
    {
        ItemPackSizeMasterEntities GetItemPackSizeById(int Id);
        IEnumerable<ItemPackSizeMasterEntities> GetAllItemPackSize();
        IEnumerable<ItemPackSizeMasterEntities> GetActivePackSize();
        int CreateItemPackSize(ItemPackSizeMasterEntities entity);
        bool UpdateItemPackSize(ItemPackSizeMasterEntities entity);
        bool DeleteItemPackSize(ItemPackSizeMasterEntities entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
