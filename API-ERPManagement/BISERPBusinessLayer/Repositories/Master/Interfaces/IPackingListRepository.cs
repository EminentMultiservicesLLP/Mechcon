using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IPackingListRepository
    {
        PackingListEntity SavePackingList(PackingListEntity model);
        List<PackingListEntity> GetPackingList(int StoreId);
        List<PackingListEntity> GetPackingListforRpt(DateTime fromdate, DateTime todate, int StoreId);
        List<PackingListDetailModel> GetPackingListDetail(int PackingListId);
       
        PackingListEntity GetPLByID(int id);
    }
}
