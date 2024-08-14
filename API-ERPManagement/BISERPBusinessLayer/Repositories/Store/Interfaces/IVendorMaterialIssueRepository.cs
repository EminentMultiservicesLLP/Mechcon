using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IVendorMaterialIssueRepository
    {
        VendorMaterialIssueEntity GetByIssueId(int IssueId);
        IEnumerable<VendorMaterialIssueEntity> GetVendorMaterialIssue(int UserId);
        VendorMaterialIssueEntity CreateNewEntry(VendorMaterialIssueEntity entity, DBHelper dbhelper);
        bool AuthCancelVendorMaterialIssue(VendorMaterialIssueEntity entity, DBHelper dbhelper);
        IEnumerable<VendorMaterialIssueEntity> GetVendorMaterialIssuestore(int UserId, int StoreId);
    }
}
