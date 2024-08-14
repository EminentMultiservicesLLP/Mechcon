using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IItemSupplierMappingRepository
    {
        List<SupplierMasterEntities> GetItemSupplierMappingByItemId(int Id);
        List<ItemMasterEntities> GetVendorItemsByItemId(int Id);
        List<ItemMasterEntities> GetVendorItemsByItemIds(List<int> ItemIds);
        int CreateItemSupplierMapping(ItemSupplierMappingEntities entity);
    }
}
