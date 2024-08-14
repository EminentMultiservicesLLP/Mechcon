using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IItemMasterRepository
    {
        ItemMasterEntities GetItemById(int Id);
        IEnumerable<ItemMasterEntities> GetActiveItems(int StoreId, int CategoryId);
        IEnumerable<ItemsModel> GetAllItems();
        IEnumerable<ItemMasterEntities> GetNonVendorItems();
        IEnumerable<ItemMasterEntities> GetNonIndentItems(int StoreId);
        IEnumerable<ItemMasterEntities> GetStoreStock(int StoreId);
        IEnumerable<ItemMasterEntities> GetStoreItems(int StoreId);
        IEnumerable<ItemMasterEntities> GetItemsforclientbilling(int storeId);
        IEnumerable<ItemMasterEntities> StoreItemsOPBal(int StoreId);
        IEnumerable<ItemMasterEntities> GetStorewiseVendorItems(int StoreId);
        IEnumerable<ItemMasterEntities> NEWGetActiveItems();
        IEnumerable<ItemMasterEntities> GetAllstorestockItems(int StoreID);
        int CreateItemMaster(ItemMasterEntities entity);
        bool UpdateItemMaster(ItemMasterEntities entity);
        bool CheckDuplicateItem( string name, string description);
        bool DeleteItemMaster(ItemMasterEntities entity);
        bool CheckDuplicateupdate(string code, int ID);

        IEnumerable<GetItemModal> GetItemCode(int itemtypeid);
        //GetItemModal GetItemCode(int itemtypeid);
        IEnumerable<ItemMasterEntities> GetItemDetails(string ItemIdList ,int StoreID);
    }
}
