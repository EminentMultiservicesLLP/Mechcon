using BISERPBusinessLayer.Entities.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IGuardDetailsRepository
    {
        int CreateGuardInfo(GuardInfoEntity Entity);
        int CreateGuardDetails(GuardDetailsEntity Entity);

        IEnumerable<GuardInfoEntity> GetAllGuardInfo(int BranchId);
    }
}
