using BISERPBusinessLayer.Entities.Branch;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IMaterialIssueGuardRepository
    {
        MaterialIssueGuardEntity CreateEntity(MaterialIssueGuardEntity entity, DBHelper dbHelper);
        IEnumerable<MaterialIssueGuardEntity> GetIssueList(int StoreId, int EmpId);
        IEnumerable<MaterialIssueGuardEntity> GetAllGuardIssueList(int UserId);
        bool UpdateEmployeeRecovery(MaterialIssueGuardEntity entity, DBHelper dbHelper);
        IEnumerable<MaterialIssueGuardEntity> AllGaurdMaterialIssue(int branchId);
    }
}
