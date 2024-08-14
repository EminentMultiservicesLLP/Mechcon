using BISERPBusinessLayer.Entities.Branch;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IMaterialIssueGuardDtRepository
    {
        IEnumerable<MaterialIssueGuardDtEntity> GetIssueDetails(int IssueId);
        int CreateEntity(MaterialIssueGuardEntity mientity, MaterialIssueGuardDtEntity entity, DBHelper dbHelper);
    }
}
