using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetCommonRepository
    {
        bool SaveAsset(List<AssetEntity> entity);
        bool UpdateAsset(List<AssetEntity> entity);
    }
}
