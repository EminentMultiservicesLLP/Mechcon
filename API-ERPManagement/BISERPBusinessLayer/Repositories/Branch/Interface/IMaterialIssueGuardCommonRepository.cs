using BISERPBusinessLayer.Entities.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IMaterialIssueGuardCommonRepository
    {
        MaterialIssueGuardEntity SaveMaterialIssueGuard(MaterialIssueGuardEntity entity);

        IEnumerable<MaterialIssueGuardEntity> GetGuardIssueReceipt(int branchId, int IssueId );
        IEnumerable<MaterialIssueGuardEntity> GetGuardIssueReceiptdt(DateTime fromdate, DateTime todate, int branchId);
    }
}
