using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
   public  interface IBranchMasterRepository
    {
       IEnumerable<BranchMasterEntities> GetAllBranches();
       int CreateBranch(BranchMasterEntities Entity);
       bool UpdateBranch(BranchMasterEntities Entity);
       IEnumerable<BranchMasterEntities> GetActiveBranches(int? UserId);
       bool CheckDuplicateItem(string code);
       bool CheckDuplicateupdate(string Code, int Id);
    }
}
