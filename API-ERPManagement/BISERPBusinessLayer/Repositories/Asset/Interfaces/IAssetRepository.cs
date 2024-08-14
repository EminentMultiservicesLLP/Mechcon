using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetRepository
    {
        IEnumerable<AssetEntity> GetAllAssets();
        IEnumerable<AssetEntity> GetBranchAssets(int BranchId);
        bool CreateAsset(List<AssetEntity> entity, DBHelper dbHelper);
        bool UpdateAsset(List<AssetEntity> entity, DBHelper dbHelper);
    }
}
