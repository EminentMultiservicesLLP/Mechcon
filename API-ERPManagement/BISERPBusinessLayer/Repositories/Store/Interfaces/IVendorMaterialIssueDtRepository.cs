using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IVendorMaterialIssueDtRepository
    {
        int CreateVMIDetails(VendorMaterialIssueEntity mientity, VendorMaterialIssueDtEntity entity, DBHelper dbhelper);
        IEnumerable<VendorMaterialIssueDtEntity> GetDetailByIssueID(int IssueId);
        List<VendorMaterialIssueDtEntity> GetDetailByIssueIDGRN(int IssueId);
        bool UpdateVendorMaterialIssueAuthQty(VendorMaterialIssueEntity mientity, VendorMaterialIssueDtEntity entity, DBHelper dbhelper);

    }
}
