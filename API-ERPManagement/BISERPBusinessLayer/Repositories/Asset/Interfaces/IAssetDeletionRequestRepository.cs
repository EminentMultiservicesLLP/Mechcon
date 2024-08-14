using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetDeletionRequestRepository
    {
        DeactivationDetailEntity CreateAssetDeletionRequest(DeactivationDetailEntity Entity);
        IEnumerable<DeactivationDetailEntity> GetAssetdeactivation(int BranchId);
    }
    
}
