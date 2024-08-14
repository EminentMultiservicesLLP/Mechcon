using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Store.Reports;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterialIssueCommonRepository
    {
        MaterialIssueEntity GetMaterialIssue(int IssueId, int UserId);
        MaterialIssueEntity SaveMaterialIssue(MaterialIssueEntity entity);
        bool AuthCancelMaterialIssue(MaterialIssueEntity entity);
        bool AcceptMaterialIssue(MaterialIssueEntity entity);
        IEnumerable<MaterialIssueEntity> GetMaterialIssueRpt(DateTime fromdate, DateTime todate, int? fStoreId, int tStoreId);
        IEnumerable<MaterialIssueEntity> GetPendingMaterialRpt(int UserId);
        IEnumerable<MaterialIssueEntity> GetCancelledMaterialRpt(DateTime fromdate, DateTime todate, int StoreId);
        IEnumerable<MaterialIssueEntity> MaterialIssueItemwise(DateTime fromdate, DateTime todate, int ItemId);
        IEnumerable<MaterialIssueAllBranchEntity> MaterialIssueDetailsAllBranch(DateTime? fromdate, DateTime? todate);
    }
}
