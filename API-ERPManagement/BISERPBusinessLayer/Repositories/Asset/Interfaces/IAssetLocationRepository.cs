using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetLocationRepository
    {
        int CreateAssetLocation(AssetLocationEntity Entity);
        AssetLocationEntity GetAssetLocation(int AssetId);
        bool UpdateAssetLocation(AssetLocationEntity Entity);
        IEnumerable<PurchaseOrderEntities> GetPoNoAssetLo(int ItemId);
        IEnumerable<AssetLocationEntity> AssetDetailReport(int BranchId,int AssetId);
        IEnumerable<AssetLocationEntity> AssetDetailReport(int BranchId, int ItemId, int FloorId);
        IEnumerable<AssetLocationEntity> AssetLocationReport(int locationId);
    }
}
